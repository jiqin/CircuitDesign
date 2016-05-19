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
     * 网表和电路图管理类
     */
    class CircuitNetlistManager
    {
        CircuitNetlistModel _circuit_list_model;
        string _component_model_file_path;
        string _circuit_template_file_path;

        public CircuitNetlistManager(string component_model_file_path, string circuit_template_file_path)
        {
            _component_model_file_path = component_model_file_path;
            _circuit_template_file_path = circuit_template_file_path;
        }

        private void pre_load()
        {
            if (_circuit_list_model != null)
            {
                _circuit_list_model.save();
            }
            _circuit_list_model = new CircuitNetlistModel(_component_model_file_path, _circuit_template_file_path);
        }

        public void new_project()
        {
            pre_load();
        }

        public void load_from_project(string file_name)
        {
            pre_load();
            _circuit_list_model.load_from_project_file(file_name);
        }

        public void load_from_network(string file_name)
        {
            pre_load();
            _circuit_list_model.load_from_network_file(file_name);
        }

        public void load_from_circuit(string file_name)
        {
            pre_load();
            _circuit_list_model.load_from_circuit_file(file_name);
        }

        public void set_file(string file_name)
        {
            if (_circuit_list_model == null)
            {
                return;
            }
            _circuit_list_model.set_file(file_name);
        }

        public void save()
        {
            if (_circuit_list_model == null)
            {
                return;
            }
            _circuit_list_model.save();
        }

        public string get_netlist_content()
        {
            if (_circuit_list_model == null)
            {
                return "";
            }
            return _circuit_list_model.get_network_content();
        }

        public List<string> get_switch_load_names()
        {
            if (_circuit_list_model == null)
            {
                return new List<string>();
            }
            return _circuit_list_model.get_switch_load_names();
        }

        public void set_switch_load_states(List<SwitchLoadStatesInput> states)
        {
            if (_circuit_list_model == null)
            {
                return;
            }
            _circuit_list_model.set_switch_load_states(states);
        }

        public List<SwitchLoadStatesInput> get_switch_load_states()
        {
            if (_circuit_list_model == null)
            {
                return new List<SwitchLoadStatesInput>();
            }
            return _circuit_list_model.get_switch_load_states();
        }

        public int get_switch_num()
        {
            if (_circuit_list_model == null)
            {
                return 0;
            }
            return _circuit_list_model.get_switch_num();
        }

        public List<AnalyzeResult> analyze()
        {
            if (_circuit_list_model == null)
            {
                return new List<AnalyzeResult>();
            }
            return _circuit_list_model.analyze();
        }

        public string analyze_load()
        {
            if (_circuit_list_model == null)
            {
                return "";
            }
            return _circuit_list_model.analyze_load();
        }

        public string analyze_power_ground()
        {
            if (_circuit_list_model == null)
            {
                return "";
            }
            return _circuit_list_model.analyze_power_ground();
        }

        public string output_text_result()
        {
            if (_circuit_list_model == null)
            {
                return "";
            }
            return _circuit_list_model.output_text_result();
        }

        public Image get_circuit_image(AnalyzeResult result)
        {
            if (_circuit_list_model == null)
            {
                return null;
            }
            return _circuit_list_model.get_circuit_image(result);
        }

        public ComponentTemplate get_circuit_component_template()
        {
            if (_circuit_list_model == null)
            {
                return null;
            }
            return _circuit_list_model.get_circuit_component_template();
        }

        public CircuitManager get_circuit_manager()
        {
            if (_circuit_list_model == null)
            {
                return null;
            }
            return _circuit_list_model.get_circuit_manager();
        }

        public void reload_network_by_circuit()
        {
            if (_circuit_list_model == null)
            {
                return;
            }
            _circuit_list_model.reload_network_by_circuit();
        }
    }
}
