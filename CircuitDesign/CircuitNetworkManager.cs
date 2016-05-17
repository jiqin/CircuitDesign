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
    class AnalyzeResult
    {
        public int index;
        public SwitchLoadStatesInput input;
        public int[] load_status;
        public bool is_expected_load_status;
        public List<List<int>> all_paths;
        public NetworkModel network_model;

        const string new_line = "\r\n";

        public string get_load_result_message()
        {
            string result = "";
            string op = "";
            if (is_expected_load_status)
            {
                result = "通过";
                op = "==";
            }
            else
            {
                result = "失败";
                op = "!=";
            }

            string message = String.Format(
                "{0}: {1} 预期负载状态 ({2}) {3} 实际负载状态 ({4})",
                get_result_message_head(),
                result,
                MatrixTools.MatrixTool.join_array(input.get_expected_load_states(), " "),
                op,
                MatrixTools.MatrixTool.join_array(load_status, " ")
                );
            return message;
        }

        public string get_result_message_head()
        {
            return String.Format(
                "状态{0} 开关状态 ({1})",
                index + 1,
                MatrixTools.MatrixTool.join_array(input.get_switch_states(), " ")
                );
        }

        public string get_path_message(List<int> path, int index)
        {
            string s = String.Format("通路{0} : ", index + 1);
            for (int i = 0; i < path.Count; ++i)
            {
                if (i != 0)
                {
                    s += " -> ";
                }
                s += network_model.node_names[path[i]];
            }
            return s;
        }

        public string get_switch_ground_message()
        {
            string message = get_result_message_head() + new_line;
            if (all_paths.Count == 0)
            {
                message += "无通路" + new_line;
            }
            else
            {
                for (int i = 0; i < all_paths.Count; i++)
                {
                    message += get_path_message(all_paths[i], i) + new_line;
                }
            }
            return message;
        }
    };

    class CircuitNetworkModel
    {
        NetworkModel network_model = new NetworkModel();
        List<SwitchLoadStatesInput> switch_load_states = new List<SwitchLoadStatesInput>();

        private CircuitManager circuit_manager = new CircuitManager();

        const string xml_node_name_root = "root";
        const string xml_node_name_circuit = "circuit";
        const string xml_node_name_network = "network";
        const string new_line = "\r\n";

        string save_file_name;

        public CircuitNetworkModel(string component_model_file_path, string circuit_template_file_path)
        {
            network_model.LoadTemplates(component_model_file_path);
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
            network_model.load_network_from_string(s);

            switch_load_states = new List<SwitchLoadStatesInput>();
            foreach (int[] states in CircuitTools.Utils.CreateCombineList(network_model.switch_components.Count))
            {
                SwitchLoadStatesInput input = new SwitchLoadStatesInput();
                input.enable = true;
                input.set_state(states, new int[network_model.r_load_components.Count]);
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
            return network_model.get_network_content();
        }

        public List<string> get_switch_load_names()
        {
            List<String> names = new List<string>();
            foreach (SwtichStruct component in network_model.switch_components)
            {
                names.Add(component.Name);
            }
            foreach (RLoadStruct component in network_model.r_load_components)
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
            return network_model.switch_components.Count;
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

                analyze_result.network_model = network_model;
                analyze_result.index = switch_load_state_index;
                analyze_result.input = switch_load_state;

                int[,] base_matrix = network_model.CreateMatrix();
                int[] vector_initial = network_model.CreateVectorInitialAll();
                int[] vector_final = new int[network_model.node_names.Count];

                int[,] trans_matrix = network_model.CreateTransMatrix(base_matrix, switch_load_state.get_all_states());
                network_model.networkflow(trans_matrix, network_model.GetMatirxBCNode(base_matrix), vector_initial, vector_final);//网络流仿真
                MatrixTools.MatrixTool.SolveStarProblem(network_model.node_names, trans_matrix, vector_final);//解决星型连接问题

                analyze_result.load_status = new int[network_model.r_load_components.Count];
                for (int i = 0; i < network_model.r_load_components.Count; i++)
                {
                    analyze_result.load_status[i] = vector_final[network_model.r_load_components[i].PositionInNodeList];
                }

                analyze_result.is_expected_load_status = MatrixTools.MatrixTool.vector_equal(analyze_result.input.get_expected_load_states(), analyze_result.load_status);

                analyze_result.all_paths = new List<List<int>>();
                network_model.FindPath(network_model.node_names, trans_matrix, analyze_result.all_paths);
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
                        switchs_on.Add(network_model.node_names[network_model.switch_components[i].PositionInNodeList]);
                    }
                }

                foreach (List<int> path in result.all_paths)
                {
                    for (int j = 0; j < path.Count - 1; ++j)
                    {
                        path_lines.Add(new KeyValuePair<string, string>(
                            network_model.node_names[path[j]],
                            network_model.node_names[path[j + 1]]));
                    }
                }
            }

            return circuit_manager.get_image(switchs_on, path_lines);
        }
    }

    class CircuitNetworkManager
    {
        CircuitNetworkModel circuit_network_model;
        string _component_model_file_path;
        string _circuit_template_file_path;

        public CircuitNetworkManager(string component_model_file_path, string circuit_template_file_path)
        {
            _component_model_file_path = component_model_file_path;
            _circuit_template_file_path = circuit_template_file_path;
        }

        private void pre_load()
        {
            if (circuit_network_model != null)
            {
                circuit_network_model.save();
            }
            circuit_network_model = new CircuitNetworkModel(_component_model_file_path, _circuit_template_file_path);
        }

        public void new_project()
        {
            pre_load();
        }

        public void load_from_project(string file_name)
        {
            pre_load();
            circuit_network_model.load_from_project_file(file_name);
        }

        public void load_from_network(string file_name)
        {
            pre_load();
            circuit_network_model.load_from_network_file(file_name);
        }

        public void load_from_circuit(string file_name)
        {
            pre_load();
            circuit_network_model.load_from_circuit_file(file_name);
        }

        public void set_file(string file_name)
        {
            if (circuit_network_model == null)
            {
                return;
            }
            circuit_network_model.set_file(file_name);
        }

        public void save()
        {
            if (circuit_network_model == null)
            {
                return;
            }
            circuit_network_model.save();
        }

        public string get_network_content()
        {
            if (circuit_network_model == null)
            {
                return "";
            }
            return circuit_network_model.get_network_content();
        }

        public List<string> get_switch_load_names()
        {
            if (circuit_network_model == null)
            {
                return new List<string>();
            }
            return circuit_network_model.get_switch_load_names();
        }

        public void set_switch_load_states(List<SwitchLoadStatesInput> states)
        {
            if (circuit_network_model == null)
            {
                return;
            }
            circuit_network_model.set_switch_load_states(states);
        }

        public List<SwitchLoadStatesInput> get_switch_load_states()
        {
            if (circuit_network_model == null)
            {
                return new List<SwitchLoadStatesInput>();
            }
            return circuit_network_model.get_switch_load_states();
        }

        public int get_switch_num()
        {
            if (circuit_network_model == null)
            {
                return 0;
            }
            return circuit_network_model.get_switch_num();
        }

        public List<AnalyzeResult> analyze()
        {
            if (circuit_network_model == null)
            {
                return new List<AnalyzeResult>();
            }
            return circuit_network_model.analyze();
        }

        public string analyze_load()
        {
            if (circuit_network_model == null)
            {
                return "";
            }
            return circuit_network_model.analyze_load();
        }

        public string analyze_power_ground()
        {
            if (circuit_network_model == null)
            {
                return "";
            }
            return circuit_network_model.analyze_power_ground();
        }

        public string output_text_result()
        {
            if (circuit_network_model == null)
            {
                return "";
            }
            return circuit_network_model.output_text_result();
        }

        public Image get_circuit_image(AnalyzeResult result)
        {
            if (circuit_network_model == null)
            {
                return null;
            }
            return circuit_network_model.get_circuit_image(result);
        }

        public ComponentTemplate get_circuit_component_template()
        {
            if (circuit_network_model == null)
            {
                return null;
            }
            return circuit_network_model.get_circuit_component_template();
        }

        public CircuitManager get_circuit_manager()
        {
            if (circuit_network_model == null)
            {
                return null;
            }
            return circuit_network_model.get_circuit_manager();
        }

        public void reload_network_by_circuit()
        {
            if (circuit_network_model == null)
            {
                return;
            }
            circuit_network_model.reload_network_by_circuit();
        }
    }
}
