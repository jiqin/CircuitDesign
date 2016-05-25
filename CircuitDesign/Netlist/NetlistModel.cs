using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace CircuitDesign
{
    /*
     * 网表模型
     */
    public class NetlistModel
    {
        public List<string> node_names = new List<string>();//存放电路模型的所有“点”
        public List<NetlistComponent> components = new List<NetlistComponent>();//存放电路中所有元件
        public List<PowerStruct> power_components = new List<PowerStruct>();//存放电路中的电源
        public List<GroundStruct> ground_components = new List<GroundStruct>();//存放电路中的地
        public List<SwtichStruct> switch_components = new List<SwtichStruct>();//存放电路中的单掷刀开关
        public List<RLoadStruct> r_load_components = new List<RLoadStruct>();//存放电路中的电阻负载
        public List<ControlNode> c_node_components = new List<ControlNode>();//存放控制结点
        public List<BeControlledNode> bc_node_components = new List<BeControlledNode>();//存放受控结点

        NetlistComponentTemplateManager netlist_component_manager_;

        const string new_line = "\r\n";

        public void LoadTemplates(NetlistComponentTemplateManager netlist_component_manager)
        {
            netlist_component_manager_ = netlist_component_manager;
        }

        public void Initialize()
        {
            node_names.Clear();
            components.Clear();
            power_components.Clear();
            ground_components.Clear();
            switch_components.Clear();
            r_load_components.Clear();
            c_node_components.Clear();
            bc_node_components.Clear();
        }

        public void load_network_from_string(string s)
        {
            StreamReader sr = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(s)));
            
            Initialize();

            while (true)
            {
                String readline = sr.ReadLine();//读网表文件中的一行

                if (readline == null)//如果读取为空，则跳出
                {
                    break;
                }

                if (readline != "") //判断是否为文件的结尾
                {
                    string endtext = readline.Split(' ')[0]; //提取读入一行信息的第一个字符串

                    if (endtext == ".subckt")  //以后的内容全部为无效分析内容
                    {
                        break;
                    }
                }

                if (readline == "" || readline[0] == '*' || readline[0] == '+')//过滤无关的分析信息
                {
                    continue;
                }

                NetlistComponent ComAdd = netlist_component_manager_.GetComponent(readline);
                components.Add(ComAdd);
                for (int i1 = 0; i1 < ComAdd.NodeNames.Length; i1++)
                {
                    if (node_names.IndexOf(ComAdd.NodeNames[i1]) == -1)
                    {
                        node_names.Add(ComAdd.NodeNames[i1]);
                    }
                }
                if (ComAdd.model_set == "Load")
                {
                    RLoadStruct RAdd = new RLoadStruct();
                    r_load_components.Add(RAdd);

                    RAdd.Name = ComAdd.Name;
                }
                else if (ComAdd.model_set == "Source")
                {
                    PowerStruct PAdd = new PowerStruct();//add the power
                    power_components.Add(PAdd);

                    PAdd.Name = ComAdd.Name;

                    GroundStruct GAdd = new GroundStruct();//add the gnd
                    ground_components.Add(GAdd);

                    GAdd.Name = ComAdd.NodeNames[2];
                }
                else if (ComAdd.model_set == "Switch")
                {
                    SwtichStruct SwAdd = new SwtichStruct();
                    switch_components.Add(SwAdd);

                    SwAdd.Name = ComAdd.Name;
                }
                else if (ComAdd.model_set == "Control_0" || ComAdd.model_set == "Control_1")
                {
                    ControlNode ConAdd = new ControlNode();
                    c_node_components.Add(ConAdd);

                    ConAdd.Name = ComAdd.NodeNames[0];

                    BeControlledNode BConAdd = new BeControlledNode();
                    bc_node_components.Add(BConAdd);

                    BConAdd.Name = ComAdd.NodeNames[1];
                }
            }

            power_components.Sort();
            ground_components.Sort();
            switch_components.Sort();
            r_load_components.Sort();

            node_names.Sort();

            GetThePosition();

            sr.Close();
        }

        public string get_network_content()
        {
            String contents = "";
            foreach (NetlistComponent cs in components)
            {
                contents += cs.toString();
                contents += new_line;
            }
            return contents;
        }

        public void GetThePosition()//取得电源、地、开关、电阻负载在Nodelist中的位置
        {
            for (int i = 0; i < power_components.Count; i++)
            {
                int index = node_names.IndexOf(power_components[i].Name);
                power_components[i].PositionInNodeList = index;
            }

            for (int i = 0; i < ground_components.Count; i++)
            {
                int index = node_names.IndexOf(ground_components[i].Name);
                ground_components[i].PositionInNodeList = index;
            }

            for (int i = 0; i < switch_components.Count; i++)
            {
                int index = node_names.IndexOf(switch_components[i].Name);
                switch_components[i].PositionInNodeList = index;
            }

            for (int i = 0; i < r_load_components.Count; i++)
            {
                int index = node_names.IndexOf(r_load_components[i].Name);
                r_load_components[i].PositionInNodeList = index;
            }

            for (int i = 0; i < c_node_components.Count; i++)
            {
                int index = node_names.IndexOf(c_node_components[i].Name);
                c_node_components[i].PositionInNodeList = index;
            }

            for (int i = 0; i < bc_node_components.Count; i++)
            {
                int index = node_names.IndexOf(bc_node_components[i].Name);
                bc_node_components[i].PositionInNodeList = index;
            }
        }

        public int[,] CreateMatrix()//建立基础矩阵
        {
            int[,] base_matrix = new int[node_names.Count, node_names.Count];

            int[] NodeIndex = new int[node_names.Count];//存放点在矩阵中的索引

            for (int i = 0; i < components.Count; i++)
            {
                for (int j = 0; j < components[i].NodeNames.Length; j++)//读取元件所有点的索引
                {
                    NodeIndex[j] = node_names.IndexOf(components[i].NodeNames[j]);
                }

                for (int m = 0; m < components[i].Connections.GetLength(0); m++)
                {
                    for (int n = 0; n < components[i].Connections.GetLength(1); n++)
                    {
                        if (components[i].Connections[m, n] == 1)
                        {
                            base_matrix[NodeIndex[m], NodeIndex[n]] = 1;
                        }

                        // TODO: 连接关系为2 受控连接设置

                        //else if (components[i].Connections[m, n] == 2)//单向反向边
                        //{
                        //    base_matrix[NodeIndex[n], NodeIndex[m]] = 1;
                        //}
                        //else if (components[i].Connections[m, n] == 3)//双向边
                        //{
                        //    base_matrix[NodeIndex[m], NodeIndex[n]] = 1;
                        //    base_matrix[NodeIndex[n], NodeIndex[m]] = 1;
                        //}
                    }
                }
            }

            for (int i1 = 0; i1 < power_components.Count; i1++)
            {
                for (int j1 = 0; j1 < node_names.Count; j1++)
                {
                    base_matrix[j1, power_components[i1].PositionInNodeList] = 0;
                }
            }

            for (int i2 = 0; i2 < ground_components.Count; i2++)
            {
                for (int j2 = 0; j2 < node_names.Count; j2++)
                {
                    base_matrix[ground_components[i2].PositionInNodeList, j2] = 0;
                }
            }

            for (int k = 0; k < node_names.Count; k++)
            {
                base_matrix[k, k] = 1;
            }

            return base_matrix;
        }

        public int[,] CreateTransMatrix(int[,] base_matrix, int[] switch_states)//建立变换矩阵
        {
            int[,] trans_matrix = new int[node_names.Count, node_names.Count];

            MatrixTool.MatrixCopy(base_matrix, trans_matrix);

            for (int i = 0; i < switch_components.Count; i++)
            {
                if (switch_states[i] == 0)
                {
                    MatrixTool.MatrixClearRowCol(trans_matrix, switch_components[i].PositionInNodeList);
                }
            }
            for (int i = 0; i < bc_node_components.Count; ++i)
            {
                MatrixTool.MatrixClearRowCol(trans_matrix, bc_node_components[i].PositionInNodeList);
            }
            return trans_matrix;
        }

        public int[] CreateVectorInitial(string startnode)//建立初始状态向量
        {
            int[] vector_initial = new int[node_names.Count];

            int index = node_names.IndexOf(startnode);

            vector_initial[index] = 1;

            return vector_initial;
        }

        //public void networkflow(int[,] mt, int[] v0, int[] vn)//网络流仿真
        //{
        //    int[] vtmp = new int[v0.Length];
        //    MatrixTool.vector_assign(vtmp, v0);
        //    MatrixTool.vector_reduce(vtmp);

        //    while (true)
        //    {
        //        MatrixTool.vector_mul_matrix(vn, vtmp, mt);
        //        MatrixTool.vector_reduce(vn);

        //        if (c_node_components.Count > 0 && c_node_components.Count == bc_node_components.Count)
        //        {
        //            for (int i = 0; i < c_node_components.Count; i++)
        //            {
        //                vn[bc_node_components[i].PositionInNodeList] = vn[c_node_components[i].PositionInNodeList];
        //            }
        //        }

        //        if (MatrixTool.vector_equal(vn, vtmp))
        //        {
        //            break;
        //        }
        //        MatrixTool.vector_assign(vtmp, vn);
        //    }
        //}

        
        public void networkflow(int[,] mt, List<int[,]> mw, int[] v0, int[] vn)//网络流仿真
        {
            if (c_node_components.Count != bc_node_components.Count)
            {
                throw new System.Exception(String.Format("Controlled nodes is wrong! {} != {}", c_node_components.Count, bc_node_components.Count));
            }

            int[] vtmp = new int[v0.Length];
            MatrixTool.vector_assign(vtmp, v0);
            MatrixTool.vector_reduce(vtmp);

            while (true)
            {
                MatrixTool.vector_mul_matrix(vn, vtmp, mt);
                MatrixTool.vector_reduce(vn);

                for (int i = 0; i < c_node_components.Count; i++)
                {
                    if (mw == null)
                    {
                        vn[bc_node_components[i].PositionInNodeList] = vn[c_node_components[i].PositionInNodeList];
                    }
                    else if (vn[c_node_components[i].PositionInNodeList] == 1)
                    {
                        MatrixTool.MatirxAdd(mt, mw[i]);
                    }
                }
                MatrixTool.matrix_reduce(mt);

                if (MatrixTool.vector_equal(vn, vtmp))
                {
                    break;
                }
                MatrixTool.vector_assign(vtmp, vn);
            }
        }

        public int[] CreateVectorInitialAll()//建立初始状态向量
        {
            int[] vector_initial = new int[node_names.Count];

            for (int i = 0; i < power_components.Count; i++)
            {
                vector_initial[power_components[i].PositionInNodeList] = 1;
            }
            return vector_initial;
        }

        public void FindPath(List<string> nodes, int[,] trans_matrix, List<List<int>> all_paths)
        {
            all_paths.Clear();
            for (int i = 0; i < trans_matrix.GetLength(0); ++i)
            {
                if (nodes[i].StartsWith("V"))
                {
                    List<int> cur_path = new List<int>();
                    cur_path.Add(i);
                    FindPath(nodes, trans_matrix, all_paths, cur_path);
                }
            }
        }

        public void FindPath(List<string> nodes, int[,] trans_matrix, List<List<int>> all_paths, List<int> cur_path)
        {
            int pos = cur_path[cur_path.Count - 1];
            for (int i = 0; i < trans_matrix.GetLength(0); ++i)
            {
                if (cur_path.Contains(i))
                {
                    continue;
                }

                if (trans_matrix[pos, i] != 0)
                {
                    cur_path.Add(i);
                    if (nodes[i].StartsWith("GND"))
                    {
                        all_paths.Add(cur_path.GetRange(0, cur_path.Count));
                    }
                    else
                    {
                        FindPath(nodes, trans_matrix, all_paths, cur_path);
                    }
                    cur_path.RemoveAt(cur_path.Count - 1);
                }
            }
        }

        public List<int[,]> GetMatirxBCNode(int[,] base_matrix)
        {
            List<int[,]> mw = new List<int[,]>();
            for (int i = 0; i < bc_node_components.Count; i++)
            {
                int n = bc_node_components[i].PositionInNodeList;
                int[,] m = new int[node_names.Count, node_names.Count];
                for (int j = 0; j < node_names.Count; ++j)
                {
                    m[n, j] = base_matrix[n, j];
                    m[j, n] = base_matrix[j, n];
                }
                mw.Add(m);
            }
            return mw;
        }
    }
}
