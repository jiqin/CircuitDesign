using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Diagnostics;

namespace CircuitDesign
{
    public class CircuitComponentTemplateManager
    {
        private string filename_;
        private Dictionary<string, XmlElement> component_xml_nodes_ = new Dictionary<string, XmlElement>();
        private XmlDocument doc_ = new XmlDocument();

        public CircuitComponentTemplateManager(string filename)
        {
            filename_ = filename;
        }

        public void Clear()
        {
            component_xml_nodes_.Clear();
        }

        public List<string> GetTypes()
        {
            List<string> types = new List<string>();
            Dictionary<string, XmlElement>.Enumerator it = component_xml_nodes_.GetEnumerator();
            while (it.MoveNext())
            {
                string type = it.Current.Key;
                if (type != ComponentNode.TYPE && type != ComponentPower.TYPE && type != ComponentGround.TYPE)
                {
                    types.Add(type);
                }
            }
            return types;
        }

        public List<BaseComponent> GetTemplateComponents()
        {
            List<BaseComponent> components = new List<BaseComponent>();
            Dictionary<string, XmlElement>.Enumerator it = component_xml_nodes_.GetEnumerator();
            while (it.MoveNext())
            {
                string type = it.Current.Key;
                if (type != ComponentNode.TYPE && type != ComponentPower.TYPE && type != ComponentGround.TYPE)
                {
                    components.Add(CreateComponent(type));
                }
            }
            return components;
        }

        public Component CreateComponent(string type)
        {
            Component c = new Component();
            c.LoadFromXML(component_xml_nodes_[type], false);
            return c;
        }

        public ComponentNode CreateComponentNode()
        {
            ComponentNode c = new ComponentNode();
            c.LoadFromXML(component_xml_nodes_[ComponentNode.TYPE], false);
            return c;
        }

        public ComponentPower CreateComponentPower()
        {
            ComponentPower c = new ComponentPower();
            c.LoadFromXML(component_xml_nodes_[ComponentPower.TYPE], false);
            return c;
        }

        public ComponentGround CreateComponentGround()
        {
            ComponentGround c = new ComponentGround();
            c.LoadFromXML(component_xml_nodes_[ComponentGround.TYPE], false);
            return c;
        }

        public void AddComponentTemplate(BaseComponent componnet)
        {
            Debug.Assert(!component_xml_nodes_.ContainsKey(componnet.Type));
            component_xml_nodes_[componnet.Type] = componnet.SaveToXML(doc_);
        }

        public void removeComponentTemplate(string type)
        {
            component_xml_nodes_.Remove(type);
        }

        public void Load()
        {
            Clear();

            if (!System.IO.File.Exists(filename_))
            {
                System.Windows.Forms.MessageBox.Show("Can't open template file : " + filename_);
                return;
            }

            doc_.Load(filename_);
            XmlElement rootNode = (XmlElement)doc_.GetElementsByTagName("components")[0];

            XmlNodeList componentList = rootNode.GetElementsByTagName("component");
            foreach (XmlNode subNode in componentList)
            {
                XmlElement element = (XmlElement)subNode;
                string type = element.GetAttribute("type");
                Debug.Assert(!component_xml_nodes_.ContainsKey(type));
                component_xml_nodes_[type] = element;
            }
        }

        public void Save()
        {
            doc_.RemoveAll();
            XmlElement node = doc_.CreateElement("components");
            Dictionary<string, XmlElement>.Enumerator it = component_xml_nodes_.GetEnumerator();
            while (it.MoveNext())
            {
                XmlElement node1 = it.Current.Value;
                node.AppendChild(node1);
            }
            doc_.AppendChild(node);
            doc_.Save(filename_);
            doc_.RemoveAll();
        }
    }
}
