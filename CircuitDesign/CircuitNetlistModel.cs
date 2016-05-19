using System;
using System.Collections.Generic;
using System.Text;
using CircuitModels;
using CircuitTools;
using CircuitDesign;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CircuitDesign
{
    /*
     * 网表和电路图模块类
     * 维护一个项目所有的数据信息
     */ 
    class CircuitNetlistModel
    {
        // 网表模型
        NetlistModel netlist_model = new NetlistModel();

        // 开关负载状态
        List<SwitchLoadStatesInput> switch_load_states = new List<SwitchLoadStatesInput>();

        // 电路图模型
        private CircuitManager circuit_manager = new CircuitManager();

        const string xml_node_name_root = "root";
        const string xml_node_name_circuit = "circuit";
        const string xml_node_name_network = "network";
        const string new_line = "\r\n";

        string save_file_name;

        public CircuitNetlistModel(NetlistComponentTemplateManager netlist_component_manager_, string circuit_template_file_path)
        {
            netlist_model.LoadTemplates(netlist_component_manager_);
            circuit_manager.InitTemplate(circuit_template_file_path);
        }

        public void load_from_project_file(string file_name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file_name);
            XmlElement root_node = (XmlElement)doc.GetElementsByTagName(xml_node_name_root)[0];

            XmlElement node = (XmlElement)root_node.GetElementsByTagName(xml_node_name_circuit)[0];
            circuit_manager.load_from_xml_node(node);

            node = (XmlElement)root_node.GetElementsByTagName(xml_node_name_network)[0];
            load_network_model_from_string(node.InnerText);
        }

        public ComponentTemplate get_circuit_component_template()
        {
            return circuit_manager.GetTemplate();
        }

        public CircuitManager get_circuit_manager()
        {
            return circuit_manager;
        }

        public void set_file(string file_name)
        {
            this.save_file_name = file_name;
        }

        public void save()
        {
            if (save_file_name == null)
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            XmlElement root_node = doc.CreateElement(xml_node_name_root);

            XmlElement node = doc.CreateElement(xml_node_name_circuit);
            circuit_manager.save_to_xml_node(doc, node);
            root_node.AppendChild(node);

            node = doc.CreateElement(xml_node_name_network);
            node.InnerText = get_network_content();
            root_node.AppendChild(node);

            doc.AppendChild(root_node);
            doc.Save(save_file_name);
        }

        public void load_from_network_file(string file_name)
        {
            load_network_model_from_string(File.ReadAllText(file_name));
        }

        public void load_network_model_from_string(string s)
        {
            netlist_model.load_network_from_string(s);

            switch_load_states = new List<SwitchLoadStatesInput>();
            foreach (int[] states in CircuitTools.Utils.CreateCombineList(netlist_model.switch_components.Count))
            {
                SwitchLoadStatesInput input = new SwitchLoadStatesInput();
                input.enable = true;
                input.set_state(states, new int[netlist_model.r_load_components.Count]);
                switch_load_states.Add(input);
            }
        }

        public void load_from_circuit_file(string file_name)
        {
            circuit_manager.LoadFromFile(file_name);
            reload_network_by_circuit();
        }

        public void reload_network_by_circuit()
        {
            load_network_model_from_string(circuit_manager.SaveAsNetwork());
        }

        public string get_network_content()
        {
            return netlist_model.get_network_content();
        }

        public List<string> get_switch_load_names()
        {
            List<String> names = new List<string>();
            foreach (SwtichStruct component in netlist_model.switch_components)
            {
                names.Add(component.Name);
            }
            foreach (RLoadStruct component in netlist_model.r_load_components)
            {
                names.Add(component.Name);
            }
            return names;
        }

        public void set_switch_load_states(List<SwitchLoadStatesInput> states)
        {
            switch_load_states = states;
        }

        public List<SwitchLoadStatesInput> get_switch_load_states()
        {
            return switch_load_states;
        }

        public int get_switch_num()
        {
            return netlist_model.switch_components.Count;
        }

        public List<AnalyzeResult> analyze()
        {
            List<AnalyzeResult> analyze_results = new List<AnalyzeResult>();

            for (int switch_load_state_index = 0; switch_load_state_index < switch_load_states.Count; ++switch_load_state_index)
            {
                SwitchLoadStatesInput switch_load_state = switch_load_states[switch_load_state_index];
                if (!switch_load_state.enable)
                {
                    continue;
                }

                AnalyzeResult analyze_result = new AnalyzeResult();
                analyze_results.Add(analyze_result);

                analyze_result.network_model = netlist_model;
                analyze_result.index = switch_load_state_index;
                analyze_result.input = switch_load_state;

                int[,] base_matrix = netlist_model.CreateMatrix();
                int[] vector_initial = netlist_model.CreateVectorInitialAll();
                int[] vector_final = new int[netlist_model.node_names.Count];

                int[,] trans_matrix = netlist_model.CreateTransMatrix(base_matrix, switch_load_state.get_all_states());
                netlist_model.networkflow(trans_matrix, netlist_model.GetMatirxBCNode(base_matrix), vector_initial, vector_final);//网络流仿真
                MatrixTools.MatrixTool.SolveStarProblem(netlist_model.node_names, trans_matrix, vector_final);//解决星型连接问题

                analyze_result.load_status = new int[netlist_model.r_load_components.Count];
                for (int i = 0; i < netlist_model.r_load_components.Count; i++)
                {
                    analyze_result.load_status[i] = vector_final[netlist_model.r_load_components[i].PositionInNodeList];
                }

                analyze_result.is_expected_load_status = MatrixTools.MatrixTool.vector_equal(analyze_result.input.get_expected_load_states(), analyze_result.load_status);

                analyze_result.all_paths = new List<List<int>>();
                netlist_model.FindPath(netlist_model.node_names, trans_matrix, analyze_result.all_paths);
            }
            return analyze_results;
        }

        public string analyze_load()
        {
            List<AnalyzeResult> analyze_results = analyze();

            string results = "";
            foreach (AnalyzeResult analyze_result in analyze_results)
            {
                results += analyze_result.get_load_result_message() + new_line;
            }
            return results;
        }

        public string analyze_power_ground()
        {
            List<AnalyzeResult> analyze_results = analyze();

            string results = "";
            foreach (AnalyzeResult analyze_result in analyze_results)
            {
                results += analyze_result.get_switch_ground_message() + new_line + new_line;
            }
            return results;
        }

        public string output_text_result()
        {
            string seperate = "-----------------------------------------------------------------------------------------";

            string content = "";
            content += seperate + new_line;
            content += "网表文件" + new_line;
            content += seperate + new_line;
            content += get_network_content() + new_line;
            content += new_line + new_line;

            content += seperate + new_line;
            content += "负载分析结果" + new_line;
            content += seperate + new_line;
            content += analyze_load() + new_line;
            content += new_line + new_line;

            content += seperate + new_line;
            content += "电源与地分析结果" + new_line;
            content += seperate + new_line;
            content += analyze_power_ground() + new_line;
            content += new_line + new_line;

            return content;
        }

        public Image get_circuit_image(AnalyzeResult result)
        {
            List<string> switchs_on = new List<string>();
            List<KeyValuePair<string, string>> path_lines = new List<KeyValuePair<string, string>>();

            if (result != null)
            {
                for (int i = 0; i < result.input.get_switch_states().Length; ++i)
                {
                    if (result.input.get_switch_states()[i] > 0)
                    {
                        switchs_on.Add(netlist_model.node_names[netlist_model.switch_components[i].PositionInNodeList]);
                    }
                }

                foreach (List<int> path in result.all_paths)
                {
                    for (int j = 0; j < path.Count - 1; ++j)
                    {
                        path_lines.Add(new KeyValuePair<string, string>(
                            netlist_model.node_names[path[j]],
                            netlist_model.node_names[path[j + 1]]));
                    }
                }
            }

            return circuit_manager.get_image(switchs_on, path_lines);
        }
    }
}
