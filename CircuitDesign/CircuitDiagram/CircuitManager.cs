using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace CircuitDesign
{
    /*
     * 电路图管理类
     */
    public class CircuitManager
    {
        private ComponentTemplateManager _template;

        private Dictionary<string, Component> components_by_id_ = new Dictionary<string, Component>();
        private Dictionary<string, ComponentNode> nodes_by_id_ = new Dictionary<string, ComponentNode>();
        private List<BaseComponent> components_ = new List<BaseComponent>();
        private System.Collections.Generic.Dictionary<string, string> component_name_set_ = new Dictionary<string, string>();

        private List<ConnectLine> connet_lines_ = new List<ConnectLine>();

        const string xml_node_name_components = "components_";
        const string xml_node_name_component = "component";
        const string xml_node_name_connection_lines = "connection_lines";
        const string xml_node_name_connection_line = "line";

        const string xml_node_name_connection_line_node = "node";
        const string xml_node_name_connection_line_component = "component";
        const string xml_node_name_connection_line_component_connection_point_index = "component_link_index";
        const string xml_node_name_connection_line_node_line_to_x = "node_line_to_x";
        
        public CircuitManager()
        {
        }

        public void Clear()
        {
            components_by_id_.Clear();
            nodes_by_id_.Clear();
            components_.Clear();
            component_name_set_.Clear();
            connet_lines_.Clear();
        }

        public void InitTemplate(ComponentTemplateManager circuit_component_manager)
        {
            _template = circuit_component_manager;
        }

        public ComponentTemplateManager GetTemplate()
        {
            return _template;
        }

        private bool is_component_in_switch_on(BaseComponent component, List<string> switchs_on)
        {
            foreach (string sw in switchs_on)
            {
                if (component.Name.EndsWith(sw))
                {
                    return true;
                }
            }
            return false;
        }

        private bool is_component_in_path(BaseComponent component, List<KeyValuePair<string, string>> path_lines)
        {
            foreach (KeyValuePair<string, string> path_line in path_lines)
            {
                if (component.Name.EndsWith(path_line.Key) || component.Name.EndsWith(path_line.Value))
                {
                    return true;
                }
            }
            return false;
        }

        private bool is_line_in_path(ConnectLine line, List<KeyValuePair<string, string>> path_lines)
        {
            foreach (KeyValuePair<string, string> path_line in path_lines)
            {
                if ((line.node.Name.EndsWith(path_line.Key) && line.component.Name.EndsWith(path_line.Value)) ||
                    (line.node.Name.EndsWith(path_line.Value) && line.component.Name.EndsWith(path_line.Key)))
                {
                    return true;
                }
            }
            return false;
        }

        public void DrawComponents(Graphics g, List<string> switchs_on, List<KeyValuePair<string, string>> path_lines)
        {
            for (int i = 0; i < components_.Count; ++i)
            {
                components_[i].Draw(g);
            }
            for (int i = 0; i < connet_lines_.Count; ++i)
            {
                connet_lines_[i].ResetPoints();
                connet_lines_[i].Draw(g);
            }

            if (switchs_on == null)
            {
                switchs_on = new List<string>();
            }
            if (path_lines == null)
            {
                path_lines = new List<KeyValuePair<string, string>>();
            }
            for (int i = 0; i < components_.Count; ++i)
            {
                if (is_component_in_path(components_[i], path_lines))
                {
                    components_[i].DrawAsPath(g);
                }

                if (is_component_in_switch_on(components_[i], switchs_on))
                {
                    components_[i].DrawAsSelected(g);
                }
            }
            for (int i = 0; i < connet_lines_.Count; ++i)
            {
                connet_lines_[i].ResetPoints();

                if (is_line_in_path(connet_lines_[i], path_lines))
                {
                    connet_lines_[i].DrawAsPath(g);
                }
            }
        }

        private Rectangle get_raw_rect()
        {
            Rectangle rect = new Rectangle(0, 0, 1, 1);
            if (components_.Count == 0)
            {
                return rect;
            }

            rect = components_[0].Position;
            for (int i = 1; i < components_.Count; ++i)
            {
                rect = DesignTools.UnionRectangle(rect, components_[i].Position);
            }
            return rect;
        }

        public Image get_image(List<string> switchs_on, List<KeyValuePair<string, string>> path_lines)
        {
            Rectangle raw_rect = get_raw_rect();
            raw_rect.Width += 30;
            raw_rect.Height += 30;
            Image im = new Bitmap(raw_rect.Right, raw_rect.Bottom);
            Graphics g = Graphics.FromImage(im);

            // g.FillRectangle(Brushes.White, raw_rect);
            DrawComponents(g, switchs_on, path_lines);

            return im;
        }

        public Component CreateComponent(string type)
        {
            Debug.Assert(type != ComponentNode.TYPE && type != ComponentPower.TYPE && type != ComponentGround.TYPE);
            Component c = _template.CreateComponent(type);
            c.Name = GetName(c.Type);
            return c;
        }

        public ComponentNode CreateComponentNode()
        {
            ComponentNode c = _template.CreateComponentNode();
            c.Name = GetName(c.Type);
            return c;
        }

        public ComponentPower CreateComponentPower()
        {
            ComponentPower c = _template.CreateComponentPower();
            c.Name = GetName(c.Type);
            return c;
        }

        public ComponentGround CreateComponentGround()
        {
            ComponentGround c = _template.CreateComponentGround();
            c.Name = GetName(c.Type);
            return c;
        }

        public ConnectLine CreateConnectLine()
        {
            return new ConnectLine();
        }

        private string GetName(string type)
        {
            string name = "";
            for (int i = 1; i < 10000; i++)
            {
                if (type == ComponentNode.TYPE)
                {
                    name = string.Format("{0}{1:0000}", type, i);
                }
                else
                {
                    name = string.Format("{0}{1}", type, i);
                }
                if (!component_name_set_.ContainsKey(name))
                {
                    break;
                }
            }
            return name;
        }

        public bool HasName(string name)
        {
            return component_name_set_.ContainsKey(name);
        }

        public void AddComponent(BaseComponent component)
        {
            Debug.Assert(!components_by_id_.ContainsKey(component.ID) && !nodes_by_id_.ContainsKey(component.ID));

            if (component.Type == ComponentNode.TYPE)
            {
                  nodes_by_id_[component.ID] = (ComponentNode)component;
            }
            else
            {
                components_by_id_[component.ID] = (Component)component;
            }

            components_.Add(component);
            component_name_set_[component.Name] = component.Name;
        }

        public void RemoveComponent(BaseComponent component)
        {
            Debug.Assert(components_by_id_.ContainsKey(component.ID) || nodes_by_id_.ContainsKey(component.ID));

            ConnectLine[] lines = component.ConnectLines.ToArray();
            foreach (ConnectLine line in lines)
            {
                if (line != null)
                {
                    RemoveComponentLink(line);
                }
            }

            if (component.Type == ComponentNode.TYPE)
            {
                    nodes_by_id_.Remove(component.ID);
            }
            else
            {
                components_by_id_.Remove(component.ID);
            }
            components_.Remove(component);
        }

        public void AddComponentLink(ConnectLine line)
        {
            Debug.Assert(line.node != null & line.component != null);
            Debug.Assert(nodes_by_id_.ContainsKey(line.node.ID));
            Debug.Assert(components_by_id_.ContainsKey(line.component.ID));

            connet_lines_.Add(line);

            line.node.AddLine(line);
            line.component.AddLine(line, line.component_connection_point_index);

            if (line.component.Type == ComponentGround.TYPE)
            {
                line.node.Name = line.component.Name;
            }
        }

        public void RemoveComponentLink(ConnectLine line)
        {
            Debug.Assert(line.node != null & line.component != null);

            line.node.RemoveLine(line);
            line.component.RemoveLine(line, line.component_connection_point_index);
            connet_lines_.Remove(line);
        }

        //指定点是否有组件被选择
        public bool HasComponentBeenSelectedInPoint(out BaseComponent selectedComponent, out int selectedLinkPoint, Point pt)
        {
            selectedComponent = null;
            selectedLinkPoint = -1;

            Dictionary<string, Component>.Enumerator it = components_by_id_.GetEnumerator();
            while (it.MoveNext())
            {
                if (IsComponentBeenSelectedInPoint(it.Current.Value, ref selectedLinkPoint, pt))
                {
                    selectedComponent = it.Current.Value;
                    return true;
                }
            }

            Dictionary<string, ComponentNode>.Enumerator it2 = nodes_by_id_.GetEnumerator();
            while (it2.MoveNext())
            {
                if (IsComponentBeenSelectedInPoint(it2.Current.Value, ref selectedLinkPoint, pt))
                {
                    selectedComponent = it2.Current.Value;
                    return true;
                }
            }

            return false;
        }

        public bool IsComponentBeenSelectedInPoint(BaseComponent component, ref int selectedLinkPoint, Point pt)
        {
            bool ret = false;

            int index = component.TestSelectLinkPoint(pt);
            if (index != -1) //选择了某个连接点
            {
                selectedLinkPoint = index;
                ret = true;
            }
            else if (component.TestSelected(pt)) //选择某个组件
            {
                ret = true;
            }

            return ret;
        }

        //指定点是否有连线被选择
        public bool HasConnectLineBeenSelectedInPoint(out ConnectLine connectLine, Point pt)
        {
            connectLine = null;
            bool ret = false;

            for (int i = 0; i < connet_lines_.Count; ++i)
            {
                if (connet_lines_[i].TestSelected(pt)) //选择某个组件
                {
                    connectLine = connet_lines_[i];
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        public void SaveToFile(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement node = doc.CreateElement("root");
            save_to_xml_node(doc, node);
            doc.AppendChild(node);
            doc.Save(fileName);
        }

        public void save_to_xml_node(XmlDocument doc, XmlNode root_node)
        {
            root_node.AppendChild(SaveComponentsToXml(doc));
            root_node.AppendChild(SaveConnectionLinesToXml(doc));
        }

        private XmlElement SaveComponentsToXml(XmlDocument doc)
        {
            XmlElement node = doc.CreateElement(xml_node_name_components);
            foreach (BaseComponent component in components_)
            {
                XmlElement node1 = component.SaveToXML(doc);
                node.AppendChild(node1);
            }
            return node;
        }

        private XmlElement SaveConnectionLinesToXml(XmlDocument doc)
        {
            XmlElement node = doc.CreateElement(xml_node_name_connection_lines);
            foreach (ConnectLine line in connet_lines_)
            {
                XmlElement node1 = doc.CreateElement(xml_node_name_connection_line);
                node1.SetAttribute(xml_node_name_connection_line_node, line.node.ID);
                node1.SetAttribute(xml_node_name_connection_line_component, line.component.ID);
                node1.SetAttribute(xml_node_name_connection_line_component_connection_point_index, line.component_connection_point_index.ToString());
                node1.SetAttribute(xml_node_name_connection_line_node_line_to_x, line.node_line_to_x.ToString());
                node.AppendChild(node1);
            }
            return node;
        }

        public void LoadFromFile(string fileName)
        {
            Clear();

            if (!System.IO.File.Exists(fileName))
            {
                System.Windows.Forms.MessageBox.Show("Can't open file : " + fileName);
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlElement node = (XmlElement)doc.GetElementsByTagName("root")[0];
            load_from_xml_node(node);
        }

        public void load_from_xml_node(XmlElement root_node)
        {
            LoadComponentsFromXml((XmlElement)root_node.GetElementsByTagName(xml_node_name_components)[0]);
            LoadConnectionLinesFromXml((XmlElement)root_node.GetElementsByTagName(xml_node_name_connection_lines)[0]);
        }

        private void LoadComponentsFromXml(XmlElement node)
        {
            Clear();
            XmlNodeList componentList = node.GetElementsByTagName(xml_node_name_component);
            foreach (XmlNode subNode in componentList)
            {
                XmlElement element = (XmlElement)subNode;

                if (element.GetAttribute("type") == ComponentNode.TYPE)
                {
                    ComponentNode c = new ComponentNode();
                    c.LoadFromXML(element);
                    AddComponent(c);
                }
                else
                {
                    Component c = new Component();
                    c.LoadFromXML(element);
                    AddComponent(c);
                }
            }
        }
        private void LoadConnectionLinesFromXml(XmlElement node)
        {
            XmlNodeList componentList = node.GetElementsByTagName(xml_node_name_connection_line);
            foreach (XmlNode subNode in componentList)
            {
                XmlElement element = (XmlElement)subNode;
                ConnectLine line = new ConnectLine();
                line.node = nodes_by_id_[element.GetAttribute(xml_node_name_connection_line_node)];
                line.component = components_by_id_[element.GetAttribute(xml_node_name_connection_line_component)];
                line.component_connection_point_index = int.Parse(element.GetAttribute(xml_node_name_connection_line_component_connection_point_index));
                line.node_line_to_x = bool.Parse(element.GetAttribute(xml_node_name_connection_line_node_line_to_x));
                AddComponentLink(line);
            }
        }

        public string SaveAsNetwork()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, Component>.Enumerator it = components_by_id_.GetEnumerator();
            while (it.MoveNext())
            {
                Component c = it.Current.Value;
                if (c.Type == ComponentGround.TYPE)
                {
                    continue;
                }
                sb.AppendFormat("{0}", c.Name);
                foreach (ConnectLine line in c.ConnectLines)
                {
                    string s = "-";
                    if (line != null)
                    {
                        s = line.node.Name;
                    }
                    sb.AppendFormat(" {0}", s);
                }
                sb.AppendFormat("\n");
            }
            return sb.ToString();
        }

        public void SaveAsNetworkToFile(string filename_)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filename_);
            sw.Write(SaveAsNetwork());
            sw.Close();
        }
    }
}