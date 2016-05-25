using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Diagnostics;

namespace CircuitDesign
{
    public abstract class BaseShape
    {
        /*
         * 绘制自己
         * 先旋转，再缩放，最后偏移
         */
        public abstract void Draw(Graphics g, Pen pen, int direct, float scale, Point offset);

        /*
         * 序列化与反序列化
         */
        public abstract XmlElement SaveNode (XmlDocument doc);
        public abstract void LoadNode (XmlElement node);

        /*
         * 获取图形所占矩形
         */
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
}
