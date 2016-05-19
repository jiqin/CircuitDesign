using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Diagnostics;

namespace CircuitDesign
{
    public class BaseComponent
    {
        protected string id_;   // UUID, 唯一标准该组件
        protected string type_; // 元件类型, 例如: N, R_R, L_L, ...
        protected string tagname_;  // 元件类型, 例如: 连接点, 电阻, 电感
        protected string name_; // 元件名, 例如: N1, N2, R_R1, R_R2

        protected string model_set_;   // 元件类别, 例如 Const (不变状态元件), Load (不变状态元件(负载))

        protected Color normal_color_ = Color.Blue; // 正常显示颜色
        protected Color select_color_ = Color.Red;  // 被选中颜色
        protected Color path_color_ = Color.Green;  // 通路颜色

        protected List<BaseShape> shapes_ = new List<BaseShape>();  // 形状列表
        protected Rectangle base_position_; // 外框
        protected Point real_center_point_; // 位置中心
        protected int direct_ = 0; //0: Up, 1:Right, 2:Down, 3:Left

        protected List<ConnectPoint> outter_connect_points_ = new List<ConnectPoint>(); // 外连接点
        protected List<ConnectPoint> inner_connect_points_ = new List<ConnectPoint>(); // 内连接点
        protected const int LINK_POINT_RADIUS = 4;

        protected List<ConnectLine> connect_lines_ = new List<ConnectLine>(); // 元件相关的连接线

        public string ID
        {
            get
            {
                return id_;
            }
        }
        public string Type
        {
            get
            {
                return type_;
            }
            set
            {
                type_ = value;
            }
        }
        public string Name
        {
            get
            {
                return name_;
            }
            set
            {
                name_ = value;
            }
        }
        public string TagName
        {
            get
            {
                return tagname_;
            }
            set
            {
                tagname_ = value;
            }
        }
        public string ModelSet
        {
            get
            {
                return model_set_;
            }
            set
            {
                model_set_ = value;
            }
        }
        public List<ConnectLine> ConnectLines
        {
            get
            {
                return connect_lines_;
            }
        }
        public BaseComponent()
        {
            id_ = System.Guid.NewGuid().ToString();
        }

        public XmlElement SaveToXML(XmlDocument doc)
        {
            XmlElement node = doc.CreateElement("component");
            node.SetAttribute("id", id_);
            node.SetAttribute("type", type_);
            node.SetAttribute("name", name_);
            node.SetAttribute("tagname", tagname_);

            node.SetAttribute("direct", direct_.ToString ());
            node.AppendChild (DesignTools.SaveRectangleNode (doc, "base_position", base_position_));
            node.AppendChild(DesignTools.SavePointNode(doc, "real_center_point", real_center_point_));

            {
                XmlElement subNode = doc.CreateElement("outter_connect_points");
                foreach (ConnectPoint cp in outter_connect_points_)
                {
                    subNode.AppendChild(cp.SaveToXML(doc));
                }
                node.AppendChild(subNode);
            }
            {
                XmlElement subNode = doc.CreateElement("inner_connect_points");
                foreach (ConnectPoint cp in inner_connect_points_)
                {
                    subNode.AppendChild(cp.SaveToXML(doc));
                }
                node.AppendChild(subNode);
            }

            {
                XmlElement subNode = doc.CreateElement("shapes");
                foreach (BaseShape shape in shapes_)
                {
                    subNode.AppendChild(shape.SaveNode(doc));
                }
                node.AppendChild(subNode);
            }

            return node;
        }

        public virtual void LoadFromXML(XmlElement node)
        {
            LoadFromXML(node, true);
        }

        public virtual void LoadFromXML(XmlElement node, bool loadid_)
        {
            if (loadid_)
            {
                id_ = node.GetAttribute("id");
            }
            type_ = node.GetAttribute("type");
            name_ = node.GetAttribute("name");
            tagname_ = node.GetAttribute("tagname");

            direct_ = int.Parse(node.GetAttribute("direct"));
            base_position_ = DesignTools.loadRectangleNode((XmlElement)node.GetElementsByTagName("base_position")[0]);
            real_center_point_ = DesignTools.loadPointNode((XmlElement)node.GetElementsByTagName("real_center_point")[0]);

            {
                outter_connect_points_.Clear();
                XmlElement subNode = (XmlElement)node.GetElementsByTagName("outter_connect_points")[0];
                foreach (XmlElement e in subNode.ChildNodes)
                {
                    ConnectPoint cp = new ConnectPoint();
                    cp.LoadFromXML(e);
                    outter_connect_points_.Add(cp);
                }
            }
            {
                inner_connect_points_.Clear();
                XmlElement subNode = (XmlElement)node.GetElementsByTagName("inner_connect_points")[0];
                foreach (XmlElement e in subNode.ChildNodes)
                {
                    ConnectPoint cp = new ConnectPoint();
                    cp.LoadFromXML(e);
                    inner_connect_points_.Add(cp);
                }
            }

            {
                shapes_.Clear();
                XmlElement subNode = (XmlElement)node.GetElementsByTagName("shapes")[0];
                foreach (XmlElement e in subNode.ChildNodes)
                {
                    BaseShape shape = null;
                    if (e.Name.ToUpper() == ShapeLine.name_.ToUpper())
                    {
                        shape = new ShapeLine();
                    }
                    else if (e.Name.ToUpper() == ShapeRectangle.name_.ToUpper())
                    {
                        shape = new ShapeRectangle();
                    }
                    else if (e.Name.ToUpper() == ShapeEllipse.name_.ToUpper())
                    {
                        shape = new ShapeEllipse();
                    }
                    else if (e.Name.ToUpper() == ShapeArc.name_.ToUpper())
                    {
                        shape = new ShapeArc();
                    }
                    if (shape != null)
                    {
                        shape.LoadNode(e);
                        shapes_.Add(shape);
                    }
                }
            }
        }

        public void Draw(Graphics g)
        {
            _inerDraw(g, normal_color_, 1.0f);
        }

        public void Draw(Graphics g, float scale)
        {
            _inerDraw(g, normal_color_, scale);
        }

        public void DrawAsSelected(Graphics g)
        {
            _inerDraw(g, select_color_, 1.0f);
        }

        public void DrawAsPath(Graphics g)
        {
            _inerDraw(g, path_color_, 1.0f);
        }

        protected virtual void _inerDraw(Graphics g, Color c, float scale)
        {
            Pen pen = new Pen(c, 2.0f);
            SolidBrush brush = new SolidBrush(c);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            Point offset = GetCenterPt();
            foreach (BaseShape shape in shapes_)
            {
                shape.Draw(g, pen, direct_, scale, offset);
            }

            for (int i = 0; i < outter_connect_points_.Count; i++)
            {
                g.FillEllipse(brush, LinkPointRect(i, scale));
                if (LinkPointNum > 1)
                {
                    g.DrawString(outter_connect_points_[i].name, 
                        SystemFonts.DefaultFont, brush, 
                        LinkPoint(i, scale).X,
                        LinkPoint(i, scale).Y + 4, sf);
                }
            }

            Rectangle rect = DesignTools.RotateRectangle(Position, real_center_point_, direct_);
            Point textPt = new Point();
            if (direct_ % 2 == 0)
            {
                textPt = new Point(rect.Left + rect.Width / 2, rect.Bottom + LINK_POINT_RADIUS);
            }
            else
            {
                textPt = new Point(rect.Right + LINK_POINT_RADIUS, rect.Top + rect.Height / 2);
            }
            g.DrawString(name_, SystemFonts.DefaultFont, brush, textPt.X, textPt.Y, sf);
        }

        public virtual bool TestSelected(Point pt)
        {
            return Position.Contains(pt);
        }

        public virtual int TestSelectLinkPoint(Point pt)
        {
            for (int i = 0; i < outter_connect_points_.Count; ++i)
            {
                if (LinkPointRect(i, 1).Contains(pt))
                {
                    return i;
                }
            }
            return -1;
        }

        public int LinkPointNum
        {
            get
            {
                return outter_connect_points_.Count;
            }
        }

        public Point LinkPoint(int index, float scale)
        {
            return DesignTools.OffsetPoint (
                DesignTools.ZoomPoint(
                DesignTools.RotatePoint(outter_connect_points_[index].pt, new Point(0, 0), direct_), scale), GetCenterPt());
        }

        public Rectangle LinkPointRect(int index, float scale)
        {
            return DesignTools.CreateRectangelByCenterPt(LinkPoint(index, scale), LINK_POINT_RADIUS);
        }

        public void SetOutConnectPoint(List<ConnectPoint> pts)
        {
            outter_connect_points_.Clear();
            outter_connect_points_.AddRange(pts);
        }

        public void SetInnerConnectPoint(List<ConnectPoint> pts)
        {
            inner_connect_points_.Clear();
            inner_connect_points_.AddRange(pts);
        }

        public Rectangle Position
        {
            get
            {
                return DesignTools.OffsetRectangle(base_position_, real_center_point_);
            }
        }

        public Point GetCenterPt()
        {
            return real_center_point_;
        }

        public void Offset(Point pt)
        {
            real_center_point_.Offset(pt);
        }

        public void SetCenterPos(Point pt)
        {
            real_center_point_ = pt;
        }

        public void AddShape(BaseShape shape)
        {
            shapes_.Add(shape);
            CreateBasePosition();
        }

        public void RemoveLastShape()
        {
            if (shapes_.Count > 0)
            {
                shapes_.RemoveAt(shapes_.Count - 1);
                CreateBasePosition();
            }
        }

        private void CreateBasePosition()
        {
            base_position_ = new Rectangle();
            foreach (BaseShape bs in shapes_)
            {
                base_position_ = DesignTools.UnionRectangle(base_position_, bs.GetRect());
            }
        }

        public void TurnLeft()
        {
            Turn(-1);
        }

        public void TurnRight()
        {
            Turn(1);
        }

        private void Turn(int direct)
        {
            direct_ = DesignTools.SetToRange(direct_ + direct, 4);
        }
    }

    public abstract class BaseShape
    {
        public abstract void Draw(Graphics g, Pen pen, int direct, float scale, Point offset);
        public abstract XmlElement SaveNode (XmlDocument doc);
        public abstract void LoadNode (XmlElement node);
        public abstract Rectangle GetRect();
    }

    public class ShapeLine : BaseShape
    {
        public static string name_ = "line";
        public Point p1 = new Point();
        public Point p2 = new Point();

        public override void Draw(Graphics g, Pen pen, int direct, float scale, Point offset)
        {
            Point pt1 = DesignTools.RotatePoint(DesignTools.ZoomPoint(p1, scale), new Point(0, 0), direct);
            pt1.Offset(offset);

            Point pt2 = DesignTools.RotatePoint(DesignTools.ZoomPoint(p2, scale), new Point(0, 0), direct);
            pt2.Offset(offset);

            g.DrawLine(pen, pt1, pt2);
        }

        public override XmlElement SaveNode(XmlDocument doc)
        {
            XmlElement node = doc.CreateElement(name_);
            node.AppendChild(DesignTools.SavePointNode(doc, "p1", p1));
            node.AppendChild(DesignTools.SavePointNode(doc, "p2", p2));
            return node;
        }

        public override void LoadNode(XmlElement node)
        {
            p1 = DesignTools.loadPointNode((XmlElement)node.ChildNodes[0]);
            p2 = DesignTools.loadPointNode((XmlElement)node.ChildNodes[1]);
        }

        public override Rectangle GetRect()
        {
            return DesignTools.CreateRectangle(p1, p2);
        }
    }

    public class ShapeRectangle : BaseShape
    {
        public static string name_ = "rectangle";
        public Rectangle rect = new Rectangle();

        public override void Draw(Graphics g, Pen pen, int direct, float scale, Point offset)
        {
            Rectangle rect2 = DesignTools.RotateRectangle(DesignTools.ZoomRectangle(rect, scale), new Point(0, 0), direct);
            rect2.Offset(offset);

            g.DrawRectangle(pen, rect2);
        }

        public override XmlElement SaveNode(XmlDocument doc)
        {
            return DesignTools.SaveRectangleNode(doc, name_, rect);
        }

        public override void LoadNode(XmlElement node)
        {
            rect = DesignTools.loadRectangleNode(node);
        }

        public override Rectangle GetRect()
        {
            return rect;
        }
    }

    public class ShapeEllipse : BaseShape
    {
        public static string name_ = "ellipse";
        public Rectangle rect = new Rectangle();

        public override void Draw(Graphics g, Pen pen, int direct, float scale, Point offset)
        {
            Rectangle rect2 = DesignTools.RotateRectangle(DesignTools.ZoomRectangle(rect, scale), new Point(0, 0), direct);
            rect2.Offset(offset);

            g.DrawEllipse(pen, rect2);
        }

        public override XmlElement SaveNode(XmlDocument doc)
        {
            return DesignTools.SaveRectangleNode(doc, name_, rect);
        }

        public override void LoadNode(XmlElement node)
        {
            rect = DesignTools.loadRectangleNode(node);
        }

        public override Rectangle GetRect()
        {
            return rect;
        }
    }

    public class ShapeArc : BaseShape
    {
        public static string name_ = "arc";
        public Rectangle rect = new Rectangle();
        public float startAngle;
        public float stopAngle;

        public override void Draw(Graphics g, Pen pen, int direct, float scale, Point offset)
        {
            Rectangle rect2 = DesignTools.RotateRectangle(DesignTools.ZoomRectangle(rect, scale), new Point(0, 0), direct);
            rect2.Offset(offset);

            g.DrawArc(pen, rect2, startAngle, stopAngle);
        }

        public override XmlElement SaveNode(XmlDocument doc)
        {
            XmlElement node = doc.CreateElement(name_);
            XmlElement node1 = DesignTools.SaveRectangleNode(doc, "Rectangle", rect);
            XmlElement node2 = doc.CreateElement("startAngle");
            node2.InnerText = startAngle.ToString();
            XmlElement node3 = doc.CreateElement("stopAngle");
            node3.InnerText = stopAngle.ToString();

            node.AppendChild(node1);
            node.AppendChild(node2);
            node.AppendChild(node3);

            return node;
        }

        public override void LoadNode(XmlElement node)
        {
            rect = DesignTools.loadRectangleNode((XmlElement)node.ChildNodes[0]);
            startAngle = float.Parse(node.ChildNodes[1].InnerText);
            stopAngle = float.Parse(node.ChildNodes[2].InnerText);
        }

        public override Rectangle GetRect()
        {
            return rect;
        }
    }

    
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

    // 连接点
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
