using System.Collections.Generic;
using System.Data.OleDb;
using System.Collections;

namespace CircuitDesign
{
    public class NetlistComponent
    {
        public string Type; // Example: V_V
        public string Name; // Example: V1
        public string[] NodeNames;  // 从内节点到外节点名称 Example: [V1, N0004, N0003]
        public string[] inner_names;
        public string[] outter_names;
        public int[,] Connections;  // 节点间连接关系 Example: [[0 1 0], [0 0 0], [0 0 0]]
        public string model_set;    // Example: Source
        public string _raw_string;  // Example: V_V1 N0004 N0003

        public NetlistComponent Clone()
        {
            NetlistComponent com_new = new NetlistComponent();
            com_new.Type = this.Type;
            com_new.model_set = this.model_set;
            com_new.Name = this.Name;

            com_new.NodeNames = new string[this.NodeNames.Length];
            this.NodeNames.CopyTo(com_new.NodeNames, 0);

            com_new.Connections = new int[this.NodeNames.Length, this.NodeNames.Length];
            for (int i = 0; i < this.Connections.GetLength(0); ++i)
            {
                for (int j = 0; j < this.Connections.GetLength(1); ++j)
                {
                    com_new.Connections[i, j] = this.Connections[i, j];
                }
            }
            return com_new;
        }

        public string toString()
        {
            return _raw_string;
        }
    }

    public class NetlistComponentTemplateManager
    {
        private string _data_base_file;
        private Dictionary<string, NetlistComponent> components_by_type_ = new Dictionary<string,NetlistComponent>();

        public List<NetlistComponent> load_components(string type)
        {
            List<NetlistComponent> components = new List<NetlistComponent>();

            System.Data.DataSet ds = new System.Data.DataSet();
            {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _data_base_file);
                conn.Open();
                string sql = "select * from component";
                if (type != null)
                {
                    sql += string.Format(" where Type = \"{0}\"", type);
                }
                OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
                da.Fill(ds);
                conn.Close();
            }

            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                /*
                 * Row: Type, Model Set, Inner Nodes, Outter Nodes, Connection Relations
                 * 
                 * Example:
                 * R_R, Load, R#, N1:N2, 0-1-3:0-2-3
                 */
                NetlistComponent cs = new NetlistComponent();
                cs.Type = (string)row[0];
                cs.model_set = (string)row[1];
                cs.inner_names = ((string)row[2]).Split(':');
                cs.outter_names = ((string)row[3]).Split(':');
                int n = cs.inner_names.Length + cs.outter_names.Length;
                cs.NodeNames = new string[n];
                cs.inner_names.CopyTo(cs.NodeNames, 0);
                cs.Connections = ConvertStringToConnectionRelation((string)row[4], n);

                components.Add(cs);
            }
            return components;
        }

        public void SaveRelationsToDatabase(NetlistComponent component)
        {
            string sep = ":";
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _data_base_file);
            conn.Open();
            OleDbCommand command = new OleDbCommand(
                string.Format("delete component where Type = \"{0}\"", component.Type), conn);
            command.ExecuteNonQuery();

            command = new OleDbCommand(
                string.Format("insert into component values(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\")",
                component.Type, 
                component.model_set, 
                string.Join(sep, component.inner_names),
                string.Join(sep, component.outter_names),
                ConvertConnectionRelationToString(component.Connections)), 
                conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public string ConvertConnectionRelationToString(int[,] connect_relations)
        {
                List<string> s = new List<string>();
                for (int i = 0; i < connect_relations.GetLength(0); ++i)
                {
                    for (int j = i + 1; j < connect_relations.GetLength(1); ++j)
                    {
                        System.Diagnostics.Debug.Assert(connect_relations[i, j] <= 2 && connect_relations[j, i] <= 2);
                        int v = 0;
                        if (connect_relations[i, j] == 1)
                        {
                            v |= 0x1;
                        }
                        if (connect_relations[i, j] == 2)
                        {
                            v |= 0x4;
                        }
                        if (connect_relations[j, i] == 1)
                        {
                            v |= 0x2;
                        }
                        if (connect_relations[j, i] == 2)
                        {
                            v |= 0x8;
                        }
                        if (v > 0)
                        {
                            s.Add(string.Format("{0}-{1}-{2}", i, j, v));
                        }
                    }
                }
                return string.Join(":", s.ToArray());
        }

        public int[,] ConvertStringToConnectionRelation(string msg, int n)
        {
            int[,] r = new int[n, n];
            string[] connections = msg.Split(':');
            foreach (string connection in connections)
            {
                if (connection.Trim() == "")
                {
                    continue;
                }
                string[] ss = connection.Split('-');
                int n1 = int.Parse(ss[0]);
                int n2 = int.Parse(ss[1]);
                int v = int.Parse(ss[2]);
                if ((v & 0x1) != 0)
                {
                    r[n1, n2] = 1;
                }
                if ((v & 0x4) != 0)
                {
                    r[n1, n2] = 2;
                }
                if ((v & 0x2) != 0)
                {
                    r[n2, n1] = 1;
                }
                if ((v & 0x8) != 0)
                {
                    r[n2, n1] = 2;
                }
            }
            return r;
        }

        public void LoadTemplates(string data_base_file)
        {
            _data_base_file = data_base_file;
            List<NetlistComponent> components = load_components(null);

            foreach (NetlistComponent cs in components)
            {
                components_by_type_[cs.Type] = cs;
            }
        }

        /*
         * input string format:
         * Type
         * 
         * Example:
         * V_V1 N0004 N0003
         */
        public NetlistComponent GetComponent(string raw_string)
        {
            List<string> s1 = new List<string>(raw_string.Split(' '));
            string type = raw_string.Substring(0, 3);
            if (!components_by_type_.ContainsKey(type))
            {
                throw new System.Exception(string.Format("不存在的类型 {0}", type));
            }
            NetlistComponent com_new = components_by_type_[type].Clone();
            com_new.Name = s1[0].Substring(2);
            com_new._raw_string = raw_string;

            string component_id = s1[0].Substring(3);

            int inner_node_num = 0;
            for (int i = 0; i < com_new.NodeNames.Length; ++i)
            {
                string node = com_new.NodeNames[i];
                if (node != null)
                {
                    // Example: V# => V1, V2, V3
                    inner_node_num++;
                    com_new.NodeNames[i] = node.Replace("#", component_id);
                }
                else
                {
                    // Example: N0004, N0003
                    com_new.NodeNames[i] = s1[i - inner_node_num + 1];
                }
            }
            return com_new;
        }
    }
}