using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;

namespace CircuitDesign
{
    /*
     * 点坐标定义:
     * X轴向右为正
     * Y轴向下为正
     */
    public class DesignTools
    {
        /*
         * 缩放点
         * d, pt: 要缩放的点
         * scale: 缩放倍数
         * origenPt: 原点
         */
        private static float Zoom(float d, float scale, float origenD)
        {
            return (d - origenD) * scale + origenD;
        }

        public static Point ZoomPoint(Point pt, float scale)
        {
            return ZoomPoint(pt, scale, new Point(0, 0));
        }

        public static Point ZoomPoint(Point pt, float scale, Point originPt)
        {
            return new Point((int)(Zoom(pt.X, scale, originPt.X)), (int)(Zoom(pt.Y, scale, originPt.Y)));
        }

        /*
         * 移动某个点
         */
        public static Point OffsetPoint(Point pt, Point offset)
        {
            return new Point(pt.X + offset.X, pt.Y + offset.Y);
        }

        public static Point SubPoint(Point pt1, Point pt2)
        {
            return new Point(pt1.X - pt2.X, pt1.Y - pt2.Y);
        }

        /*
         * 缩放矩形
         * rect: 要缩放的矩形
         * scale: 缩放倍数
         * origenPt: 原点
         */
        public static Rectangle ZoomRectangle(Rectangle rect, float scale)
        {
            return ZoomRectangle(rect, scale, new Point(0, 0));
        }

        public static Rectangle ZoomRectangle(Rectangle rect, float scale, Point originPt)
        {
            return new Rectangle((int)Zoom(rect.Left, scale, originPt.X), (int)Zoom(rect.Top, scale, originPt.Y), (int)(rect.Width * scale), (int)(rect.Height * scale));
        }

        public static Rectangle OffsetRectangle(Rectangle rect, Point offset)
        {
            Rectangle tmpRect = new Rectangle(rect.Location, rect.Size);
            tmpRect.Offset(offset);
            return tmpRect;
        }

        //得到矩形中点坐标
        public static Point GetRectangleCenterPt(Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
        }

        //两个矩形求并集
        public static Rectangle UnionRectangle(Rectangle rect1, Rectangle rect2)
        {
            Rectangle rect = new Rectangle();
            rect.X = Math.Min(rect1.Left, rect2.Left);
            rect.Y = Math.Min(rect1.Top, rect2.Top);
            rect.Width = Math.Max(rect1.Right, rect2.Right) - rect.X;
            rect.Height = Math.Max(rect1.Bottom, rect2.Bottom) - rect.Y;
            return rect;
        }

        /*
         * 创建矩形
         * pt: 矩形中心点
         * radius: 半径
         */
        public static Rectangle CreateRectangelByCenterPt(Point pt, int radius)
        {
            return new Rectangle(pt.X - radius, pt.Y - radius, radius * 2, radius * 2);
        }

        /*
         * 旋转点
         * pt: 要旋转的点
         * originPt: 原点
         * direct: 0, 1(顺时针90度), 2(顺时针180度), 3, ...
         * 
         * 示例
         * pt = (1, 0), originPt = (0, 0), direct = 1
         * 返回 (0, 1)
         */
        public static Point RotatePoint(Point pt, Point originPt, int direct)
        {
            direct = NormalizeDirection(direct);

            Point tmpPt = new Point(pt.X, pt.Y);
            tmpPt.Offset(-originPt.X, -originPt.Y);

            switch (direct)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        tmpPt = new Point(-tmpPt.Y, tmpPt.X);
                        break;
                    }
                case 2:
                    {
                        tmpPt = new Point(-tmpPt.X, -tmpPt.Y);
                        break;
                    }
                case 3:
                    {
                        tmpPt = new Point(tmpPt.Y, -tmpPt.X);
                        break;
                    }
            }

            tmpPt.Offset(originPt.X, originPt.Y);
            return tmpPt;
        }

        /*
         * 旋转矩形
         * rect: 要旋转的矩形
         * originPt: 原点
         * direct: 0, 1(顺时针90度), 2(顺时针180度), 3, ...
         */
        public static Rectangle RotateRectangle(Rectangle rect, Point originPt, int direct)
        {
            direct = NormalizeDirection(direct);
            /*
             * pt1 pt2
             * pt4 pt3
             */
            Point pt1 = RotatePoint(new Point(rect.Left, rect.Top), originPt, direct);
            Point pt2 = RotatePoint(new Point(rect.Right, rect.Top), originPt, direct);
            Point pt3 = RotatePoint(new Point(rect.Right, rect.Bottom), originPt, direct);
            Point pt4 = RotatePoint(new Point(rect.Left, rect.Bottom), originPt, direct);
            Size size1 = new Size (rect.Width, rect.Height);
            Size size2 = new Size (rect.Height, rect.Width);

            Rectangle rect2 = new Rectangle(pt1, size1);
            switch (direct)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        rect2 = new Rectangle(pt4, size2);
                        break;
                    }
                case 2:
                    {
                        rect2 = new Rectangle(pt3, size1);
                        break;
                    }
                case 3:
                    {
                        rect2 = new Rectangle(pt2, size2);
                        break;
                    }
            }

            return rect2;
        }

        /*
         * 将direct转为0 - 3的值
         * 0: 顺时针旋转0度
         * 1: 顺时针旋转90度
         * 2: 顺时针旋转180度
         * 3: 顺时针旋转270度
         */
        public static int NormalizeDirection(int direct)
        {
            direct %= 4;
            if (direct < 0) direct += 4;
            if (direct >= 4) direct -= 4;
            return direct;
        }

        /*
         * 从点 pt1 和 pt2 生成一个矩形
         */
        public static Rectangle CreateRectangle(Point pt1, Point pt2)
        {
            int x = Math.Min(pt1.X, pt2.X);
            int y = Math.Min(pt1.Y, pt2.Y);
            int width = Math.Abs(pt1.X - pt2.X);
            //width = (width > 0) ? width : 1;
            int height = Math.Abs(pt1.Y - pt2.Y);
            //height = (height > 0) ? height : 1;

            Rectangle rect = new Rectangle(x, y, width, height);
            return rect;
        }

        public static Point loadPointNode(XmlElement node)
        {
            Point pt = new Point();
            pt.X = int.Parse(node.GetAttribute("x"));
            pt.Y = int.Parse(node.GetAttribute("y"));
            return pt;
        }

        public static XmlElement SavePointNode(XmlDocument doc, string name, Point pt)
        {
            XmlElement node = doc.CreateElement(name);
            node.SetAttribute("x", pt.X.ToString());
            node.SetAttribute("y", pt.Y.ToString());
            return node;
        }

        public static Rectangle loadRectangleNode(XmlElement node)
        {
            Rectangle rect = new Rectangle();
            rect.X = int.Parse(node.GetAttribute("x"));
            rect.Y = int.Parse(node.GetAttribute("y"));
            rect.Width = int.Parse(node.GetAttribute("width"));
            rect.Height = int.Parse(node.GetAttribute("height"));
            return rect;
        }

        public static XmlElement SaveRectangleNode(XmlDocument doc, string name, Rectangle rect)
        {
            XmlElement node = doc.CreateElement(name);
            node.SetAttribute("x", rect.X.ToString());
            node.SetAttribute("y", rect.Y.ToString());
            node.SetAttribute("width", rect.Width.ToString());
            node.SetAttribute("height", rect.Height.ToString());
            return node;
        }

        public static bool IsPointInLine(Point pS, Point pE, Point p)
        {
            const int span = 4;
            Point _StarPoint1 = new Point(pS.X + span, pS.Y - span);
            Point _StarPoint2 = new Point(pS.X - span, pS.Y + span);
            Point _EndPoint1 = new Point(pE.X + span, pE.Y - span);
            Point _EndPoint2 = new Point(pE.X - span, pE.Y + span);

            return IsPointInRegion(new Point[] { _StarPoint1, _StarPoint2, _EndPoint2, _EndPoint1}, p);
        }

        public static bool IsPointInRegion(Point[] pts, Point p)
        {
            System.Drawing.Drawing2D.GraphicsPath _GraphicsPath = new System.Drawing.Drawing2D.GraphicsPath(); //获得一个多边形状
            _GraphicsPath.Reset();
            _GraphicsPath.AddPolygon(pts);

            Region _Region = new Region();
            _Region.MakeEmpty();
            _Region.Union(_GraphicsPath);

            return _Region.IsVisible(p); //返回判断点是否在多边形里
        }

        /*
         * 判断点p是否在直线 (p1 -> p2)的右侧
         */
        public static bool IsPointInLineRight(Point p1, Point p2, Point p)
        {
            int r = p1.X * p2.Y + p.X * p1.Y + p2.X * p.Y - p.X * p2.Y - p2.X * p1.Y - p1.X * p.Y;
            return r > 0;
        }
    }
}
