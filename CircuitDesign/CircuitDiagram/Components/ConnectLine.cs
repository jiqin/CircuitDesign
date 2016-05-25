using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Diagnostics;

namespace CircuitDesign
{
    public struct ConnectPoint
    {
        public string name;
        public Point pt;

        public XmlElement SaveToXML(XmlDocument doc)
        {
            XmlElement node = doc.CreateElement("connect_point");
            node.SetAttribute("name", name);
            node.SetAttribute("x", pt.X.ToString());
            node.SetAttribute("y", pt.Y.ToString());
            return node;
        }
        public void LoadFromXML(XmlElement node)
        {
            name = node.GetAttribute("name");
            pt.X = int.Parse(node.GetAttribute("x"));
            pt.Y = int.Parse(node.GetAttribute("y"));
        }
    }

    //连接线要求一段是元件，一段是连接点
    public class ConnectLine
    {
        public ComponentNode node;
        public Component component;
        public int component_connection_point_index;
        private List<Point> points = new List<Point>();
        public bool node_line_to_x = true;

        protected Pen normal_pen_ = new Pen(Color.Blue);
        protected Pen select_pen_ = new Pen(Color.Red);
        protected Pen path_pen_ = new Pen(Color.Green);

        public ConnectLine()
        {
        }

        public bool TestSelected(Point pt)
        {
            for (int i = 0; i < points.Count - 1; ++i)
            {
                if (DesignTools.IsPointInLine(points[i], points[i + 1], pt))
                    return true;
            }
            return false;
        }

        public void Draw(Graphics g)
        {
            _innerDraw(g, normal_pen_);
        }

        public void DrawAsSelected(Graphics g)
        {
            _innerDraw(g, select_pen_);
        }

        public void DrawAsPath(Graphics g)
        {
            _innerDraw(g, path_pen_);
        }

        private void _innerDraw(Graphics g, Pen pen)
        {
            if (points.Count >= 2)
            {
                g.DrawLines(pen, points.ToArray());
            }
        }

        public void ResetPoints(Point pt)
        {
            points.Clear();
            if (node != null)
            {
                Point p1 = node.LinkPoint(0, 1);
                points.Add(p1);
                if (node_line_to_x)
                {
                    points.Add(new Point(pt.X, p1.Y));
                }
                else
                {
                    points.Add(new Point(p1.X, pt.Y));
                }
            }
            else if (component != null)
            {
                Point p1 = component.LinkPoint(component_connection_point_index, 1);
                points.Add(p1);
                if (node_line_to_x)
                {
                    points.Add(new Point(p1.X, pt.Y));
                }
                else
                {
                    points.Add(new Point(pt.X, p1.Y));
                }
            }
            points.Add(pt);
        }

        public void ResetPoints()
        {
            points.Clear();
            if (node != null && component != null)
            {
                Point p1 = node.LinkPoint(0, 1);
                Point p2 = component.LinkPoint(component_connection_point_index, 1);
                points.Add(p1);
                if (node_line_to_x)
                {
                    points.Add(new Point(p2.X, p1.Y));
                }
                else
                {
                    points.Add(new Point(p1.X, p2.Y));
                }
                points.Add(p2);
            }
        }

        public void ChangeShapeToPoint(Point pt)
        {
            ResetPoints();
            if (points.Count != 3)
            {
                return;
            }
            if (points[0].X == points[2].X || points[0].Y == points[2].Y)
            {
                return;
            }
            bool r1 = DesignTools.IsPointInLineRight(points[0], points[2], points[1]);
            bool r2 = DesignTools.IsPointInLineRight(points[0], points[2], pt);
            if (r1 != r2)
            {
                node_line_to_x = !node_line_to_x;
                ResetPoints();
            }
        }
    }
}
