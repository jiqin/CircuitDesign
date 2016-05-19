using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CircuitDesign
{
    /*
     * 仿真结果
     */
    class AnalyzeResult
    {
        public int index;
        public SwitchLoadStatesInput input;
        public int[] load_status;
        public bool is_expected_load_status;
        public List<List<int>> all_paths;
        public NetlistModel network_model;

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
                MatrixTool.join_array(input.get_expected_load_states(), " "),
                op,
                MatrixTool.join_array(load_status, " ")
                );
            return message;
        }

        public string get_result_message_head()
        {
            return String.Format(
                "状态{0} 开关状态 ({1})",
                index + 1,
                MatrixTool.join_array(input.get_switch_states(), " ")
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
}
