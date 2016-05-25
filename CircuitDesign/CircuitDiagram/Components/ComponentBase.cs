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

        public BaseComponent Clone()
        {
            BaseComponent other = new BaseComponent();
            return other;
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

        public List<ConnectPoint> GetOutterConnectPoint()
        {
            return new List<ConnectPoint>(outter_connect_points_);
        }

        public void SetInnerConnectPoint(List<ConnectPoint> pts)
        {
            inner_connect_points_.Clear();
            inner_connect_points_.AddRange(pts);
        }

        public List<ConnectPoint> GetInnerConnectPoint()
        {
            return new List<ConnectPoint>(inner_connect_points_);
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
            direct_ = DesignTools.NormalizeDirection(direct_ + direct);
        }
    }
}
