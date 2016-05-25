using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Diagnostics;

namespace CircuitDesign
{
    public class Component : BaseComponent
    {
        public Component()
        {
        }

        public override void LoadFromXML(XmlElement node)
        {
            LoadFromXML(node, true);
        }

        public override void LoadFromXML(XmlElement node, bool loadid_)
        {
            base.LoadFromXML(node, loadid_);
            for (int i = 0; i < outter_connect_points_.Count; ++i)
            {
                connect_lines_.Add(null);
            }
        }

        public void AddLine(ConnectLine line, int index)
        {
            Debug.Assert(index < outter_connect_points_.Count);
            connect_lines_[index] = line;
        }

        public void RemoveLine(ConnectLine line, int index)
        {
            Debug.Assert(index < outter_connect_points_.Count);
            connect_lines_[index] = null;
        }

        public bool hasLine(int index)
        {
            Debug.Assert(index < outter_connect_points_.Count);
            return connect_lines_[index] != null;
        }
    }

    // Á¬½Óµã
    public class ComponentNode : BaseComponent
    {
        public static string TYPE = "N";

        public ComponentNode()
        {
        }
        public void AddLine(ConnectLine line)
        {
            Debug.Assert(!connect_lines_.Contains(line));
            connect_lines_.Add(line);
        }
        public void RemoveLine(ConnectLine line)
        {
            Debug.Assert(connect_lines_.Contains(line));
            connect_lines_.Remove(line);
        }
    }

    public class ComponentPower : Component
    {
        public static string TYPE = "V_V";
    }

    public class ComponentGround : Component
    {
        public static string TYPE = "GND";
    }
}
