using System.Collections.Generic;
using System.Data.OleDb;
using CircuitTools;
using System.Collections;

namespace CircuitModels
{
    public class ComponentStruct
    {
        public string Type;
        public string Name;
        public string[] NodeNames;
        public int[,] Connections;
        public string model_set;
        public string _raw_string;

        public ComponentStruct Clone()
        {
            ComponentStruct com_new = new ComponentStruct();
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
            //string s = this.Type + " ";
            //s += string.Join(" ", this.NodeNames);
            //return s;
            return _raw_string;
        }
    }

    public class BaseStruct : System.IComparable
    {
        public string Name;
        public int PositionInNodeList;

        public int CompareTo(object y)
        {
            return string.Compare(this.Name, ((BaseStruct)y).Name);
        }
    }

    public class SwtichStruct : BaseStruct
    {
    }

    public class PowerStruct : BaseStruct
    {
    }

    public class GroundStruct : BaseStruct
    {
    }

    public class RLoadStruct : BaseStruct
    {
    }

    public class ControlNode : BaseStruct
    {
    }

    public class BeControlledNode : BaseStruct
    {
    }

    public class ComponentStructManager
    {
        private Dictionary<string, ComponentStruct> components_by_type_ = new Dictionary<string,ComponentStruct>();

        public static List<ComponentStruct> load_components(string data_base_file, string type)
        {
            List<ComponentStruct> components = new List<ComponentStruct>();

            System.Data.DataSet ds = new System.Data.DataSet();
            {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + data_base_file);
                conn.Open();
                string sql = "select * from component";
                if (type != null)
                {
                    sql += string.Format(" where Type = \"{}\"", type);
                }
                OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
                da.Fill(ds);
                conn.Close();
            }

            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                ComponentStruct cs = new ComponentStruct();
                cs.Type = (string)row[0];
                cs.model_set = (string)row[1];
                string[] inner_node = ((string)row[2]).Split(':');
                string[] outter_node = ((string)row[3]).Split(':');
                int n = inner_node.Length + outter_node.Length;
                cs.NodeNames = new string[n];
                inner_node.CopyTo(cs.NodeNames, 0);

                cs.Connections = new int[n, n];
                string[] connections = ((string)row[4]).Split(':');
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
                    cs.Connections[n1, n2] = v;
                }

                components.Add(cs);
            }
            return components;
        }

        public void LoadTemplates(string data_base_file)
        {
            List<ComponentStruct> components = load_components(data_base_file, null);

            foreach (ComponentStruct cs in components)
            {
                components_by_type_[cs.Type] = cs;
            }
        }

        public ComponentStruct GetComponent(string raw_string)
        {
            List<string> s1 = new List<string>(raw_string.Split(' '));
            string type = raw_string.Substring(0, 3);
            if (!components_by_type_.ContainsKey(type))
            {
                throw new System.Exception(string.Format("不存在的类型 {0}", type));
                //System.Windows.Forms.MessageBox.Show("不存在的类型 {0}", type);
                //return null;
            }
            ComponentStruct com_new = components_by_type_[type].Clone();
            com_new.Name = s1[0].Substring(2); ;
            com_new._raw_string = raw_string;

            string component_id = s1[0].Substring(3);

            int inner_node_num = 0;
            for (int i = 0; i < com_new.NodeNames.Length; ++i)
            {
                string node = com_new.NodeNames[i];
                if (node != null)
                {
                    inner_node_num++;
                    com_new.NodeNames[i] = node.Replace("#", component_id);
                }
                else
                {
                    com_new.NodeNames[i] = s1[i - inner_node_num + 1];
                }
            }
            return com_new;
        }
    }
}