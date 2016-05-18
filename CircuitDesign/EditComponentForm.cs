using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CircuitDesign
{
    public partial class EditComponentForm : Form
    {
        private List<string> componentname_s_ = new List<string>();
        public BaseComponent new_component = null;
        public string connection_relation_msg = "";

        private Rectangle draw_shape_rect_;
        private float scale_ = 1.0f;

        private State state_ = State.None;
        private Point shape_first_pt;
        private ShapeLine shape_line_to_add_;
        private ShapeRectangle shape_rectangle_to_add_;
        private ShapeEllipse shape_ellipse_to_add_;

        private List<ConnectPoint> inner_points_ = new List<ConnectPoint>();
        private List<ConnectPoint> outter_points_ = new List<ConnectPoint>();
        private int[,] connect_relations_;

        private List<string> model_set_name_ = new List<string>();

        enum State
        {
            None = 0,
            DrawLinePt1,
            DrawLinePt2,
            DrawRectPt1,
            DrawRectPt2,
            DrawEllipsePt1,
            DrawEllipsePt2,
            AddInnerPoint,
            AddOutterPoint,
        };

        public EditComponentForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            model_set_name_.Add("Const");
            comboBox_model_set.Items.Add("不变状态元件");
            model_set_name_.Add("Load");
            comboBox_model_set.Items.Add("不变状态元件(负载)");
            model_set_name_.Add("Switch");
            comboBox_model_set.Items.Add("开关");
            model_set_name_.Add("Source");
            comboBox_model_set.Items.Add("电源");
            model_set_name_.Add("Control_0");
            comboBox_model_set.Items.Add("常闭受控结点");
            model_set_name_.Add("Control_1");
            comboBox_model_set.Items.Add("常开受控结点");
            comboBox_model_set.SelectedIndex = 0;

            int left = panel1.Right;
            int top = groupBox_scale.Bottom;
            int span = 20;
            draw_shape_rect_ = new Rectangle(left + span, top + span, this.Right - left - span * 2, this.Bottom - 50 - top - span * 2);
            int radius = Math.Min(draw_shape_rect_.Width, draw_shape_rect_.Height);
            draw_shape_rect_ = DesignTools.CreateRectangelByCenterPt(DesignTools.GetRectangleCenterPt(draw_shape_rect_), radius / 2);
        }

        public void InitData(List<string> componentname_s, BaseComponent component)
        {
            componentname_s_.AddRange(componentname_s);
            if (component != null)
            {
                new_component = component;
            }
            else
            {
                new_component = new BaseComponent();
            }
            new_component.SetCenterPos(DesignTools.GetRectangleCenterPt(draw_shape_rect_));
            textBoxtype_.Text = new_component.Type;
            textBox_tagname_.Text = new_component.TagName;
            AutoSetTextboxPosition();
            label_scale_info.Text = string.Format("缩放比例: {0}", scale_);
        }

        private void AutoSetTextboxPosition()
        {
            textBox_position.Text = string.Format("{0},{1}", new_component.Position.Width, new_component.Position.Height);
        }

        private void ResetComponent()
        {
            state_ = State.None;
            shape_first_pt = new Point(0, 0);
            shape_line_to_add_ = null;
            shape_rectangle_to_add_ = null;
            shape_ellipse_to_add_ = null;
            toolStripStatusLabel_op_tip.Text = "";
        }

        private Point DeviceToLogicalPoint(Point pt)
        {
            Point center_pt = DesignTools.GetRectangleCenterPt(draw_shape_rect_);
            Point p1 = DesignTools.OffsetPoint(pt, new Point(-center_pt.X, -center_pt.Y));
            return DesignTools.ZoomPoint(p1, 1.0f/scale_);
        }

        private Point LogicalToDevicePoint(Point pt)
        {
            Point center_pt = DesignTools.GetRectangleCenterPt(draw_shape_rect_);
            Point p1 = DesignTools.ZoomPoint(pt, scale_);
            return DesignTools.OffsetPoint(p1, center_pt);
        }
        
        private void button_add_line_Click(object sender, EventArgs e)
        {
            ResetComponent();
            state_ = State.DrawLinePt1;
            shape_line_to_add_ = new ShapeLine();
            toolStripStatusLabel_op_tip.Text = "添加线，右击结束";
        }

        private void button＿add_rectangle_Click(object sender, EventArgs e)
        {
            ResetComponent();
            state_ = State.DrawRectPt1;
            shape_rectangle_to_add_ = new ShapeRectangle();
            toolStripStatusLabel_op_tip.Text = "添加矩形，右击结束";
        }

        private void button_add_ellipse_Click(object sender, EventArgs e)
        {
            ResetComponent();
            state_ = State.DrawEllipsePt1;
            shape_ellipse_to_add_ = new ShapeEllipse();
            toolStripStatusLabel_op_tip.Text = "添加椭圆，右击结束";
        }

        private void button_undo_last_draw_Click(object sender, EventArgs e)
        {
            ResetComponent();
            new_component.RemoveLastShape();
            AutoSetTextboxPosition();
            Invalidate();
        }

        private void button_add_out_connectpoint_Click(object sender, EventArgs e)
        {
            ResetComponent();
            state_ = State.AddOutterPoint;
            toolStripStatusLabel_op_tip.Text = "添加外连接点，右击结束";
        }

        private void button_add_inner_connect_point_Click(object sender, EventArgs e)
        {
            ResetComponent();
            state_ = State.AddInnerPoint;
            toolStripStatusLabel_op_tip.Text = "添加内连接点，右击结束";
        }
        
        private void button_add_out_connect_point_Click(object sender, EventArgs e)
        {
            ResetComponent();
            if (outter_points_.Count > 0)
            {
                outter_points_.RemoveAt(outter_points_.Count - 1);
                Invalidate();
            }
        }

        private void button_undo_add_inner_point_Click(object sender, EventArgs e)
        {
            ResetComponent();
            if (inner_points_.Count > 0)
            {
                inner_points_.RemoveAt(inner_points_.Count - 1);
                Invalidate();
            }
        }

        private void AddComponentTemplateDlg_MouseClick(object sender, MouseEventArgs e)
        {
            Point pt = DeviceToLogicalPoint(e.Location);
            switch (state_)
            {
                case State.DrawLinePt1:
                    {
                        shape_first_pt = pt;
                        shape_line_to_add_.p1 = pt;
                        state_ = State.DrawLinePt2;
                    }
                    break;
                case State.DrawLinePt2:
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            ResetComponent();
                        }
                        else
                        {
                            shape_line_to_add_.p2 = pt;
                            new_component.AddShape(shape_line_to_add_);
                            AutoSetTextboxPosition();

                            shape_line_to_add_ = new ShapeLine();
                            shape_line_to_add_.p1 = pt;
                        }
                    }
                    break;
                case State.DrawRectPt1:
                    {
                        shape_first_pt = pt;
                        shape_rectangle_to_add_.rect.Location = pt;
                        state_ = State.DrawRectPt2;
                    }
                    break;
                case State.DrawRectPt2:
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            ResetComponent();
                        }
                        else
                        {
                            shape_rectangle_to_add_.rect = DesignTools.CreateRectangle(shape_first_pt, pt);
                            new_component.AddShape(shape_rectangle_to_add_);
                            AutoSetTextboxPosition();
                            ResetComponent();
                        }
                    }
                    break;
                case State.DrawEllipsePt1:
                    {
                        shape_first_pt = pt;
                        shape_ellipse_to_add_.rect.Location = pt;
                        state_ = State.DrawEllipsePt2;
                    }
                    break;
                case State.DrawEllipsePt2:
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            ResetComponent();
                        }
                        else
                        {
                            shape_ellipse_to_add_.rect = DesignTools.CreateRectangle(shape_first_pt, pt);
                            new_component.AddShape(shape_ellipse_to_add_);
                            AutoSetTextboxPosition();
                            ResetComponent();
                        }
                    }
                    break;
                case State.AddInnerPoint:
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            ResetComponent();
                        }
                        else
                        {
                            ChangeComponentNameForm dlg = new ChangeComponentNameForm();
                            dlg.Text = "设置内部连接点名称";
                            dlg.ShowDialog();

                            bool good_name = false;
                            if (dlg.name != "")
                            {
                                good_name = true;
                                if (dlg.name.IndexOf("#") == -1)
                                {
                                    good_name = false;
                                    MessageBox.Show(string.Format("名字需要包含(#)"));
                                }
                                for (int i = 0; i < inner_points_.Count; ++i)
                                {
                                    if (inner_points_[i].name == dlg.name)
                                    {
                                        good_name = false;
                                        MessageBox.Show(string.Format("连接点名({0})已存在", dlg.name));
                                        break;
                                    }
                                }
                            }
                            if (good_name)
                            {
                                ConnectPoint cp = new ConnectPoint();
                                cp.pt = pt;
                                cp.name = dlg.name;
                                inner_points_.Add(cp);
                            }
                        }
                    }
                    break;
                case State.AddOutterPoint:
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            ResetComponent();
                        }
                        else
                        {
                            ConnectPoint cp = new ConnectPoint();
                            cp.pt = pt;
                            cp.name = string.Format("P{0}", outter_points_.Count + 1);
                            outter_points_.Add(cp);
                        }
                    }
                    break;
            }
            Invalidate();
        }

        private void AddComponentTemplateDlg_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = DeviceToLogicalPoint(e.Location);
            switch (state_)
            {
                case State.DrawLinePt2:
                    {
                        shape_line_to_add_.p2 = pt;
                    }
                    break;
                case State.DrawRectPt2:
                    {
                        shape_rectangle_to_add_.rect = DesignTools.CreateRectangle(shape_first_pt, pt);
                    }
                    break;
                case State.DrawEllipsePt2:
                    {
                        shape_ellipse_to_add_.rect = DesignTools.CreateRectangle(shape_first_pt, pt);
                    }
                    break;
            }
            Invalidate();
        }

        private void AddComponentTemplateDlg_Paint(object sender, PaintEventArgs e)
        {
            DrawBaseBackground(e.Graphics);
            DrawShape(e.Graphics);
        }

        private void DrawBaseBackground(Graphics g)
        {
            Rectangle rect = draw_shape_rect_;
            Color c = Color.Gray;
            Pen pen_solid1 = new Pen(c, 2.0f);
            Pen pen_solid2 = new Pen(c, 0.5f);
            Pen pen_dot = new Pen(c, 0.5f);
            pen_dot.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            Point center_pt = DesignTools.GetRectangleCenterPt(rect);
            g.DrawRectangle(pen_solid1, draw_shape_rect_);
            for (int x = 0; x < draw_shape_rect_.Width / 2; x += 10)
            {
                Pen pen;
                if (x == 0)
                {
                    pen = pen_solid1;
                }
                else if (x % 50 == 0)
                {
                    pen = pen_solid2;
                }
                else
                {
                    pen = pen_dot;
                }

                g.DrawLine(pen, new Point(rect.Left, center_pt.Y - x), new Point(rect.Right, center_pt.Y - x));
                g.DrawLine(pen, new Point(rect.Left, center_pt.Y + x), new Point(rect.Right, center_pt.Y + x));
                g.DrawLine(pen, new Point(center_pt.X - x, rect.Top), new Point(center_pt.X - x, rect.Bottom));
                g.DrawLine(pen, new Point(center_pt.X + x, rect.Top), new Point(center_pt.X + x, rect.Bottom));
            }
        }

        private void DrawShape(Graphics g)
        {
            Rectangle rect = draw_shape_rect_;
            Color c = Color.Black;
            Pen pen1 = new Pen(c, 2.0f);
            Pen pen2 = new Pen(c, 1.0f);
            pen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            Point center_pt = DesignTools.GetRectangleCenterPt(rect);
            g.DrawRectangle(pen2, DesignTools.ZoomRectangle(new_component.Position, scale_, center_pt));
            new_component.Draw(g, scale_);

            if (shape_line_to_add_ != null)
            {
                shape_line_to_add_.Draw(g, pen1, 0, scale_, center_pt);
            }
            if (shape_rectangle_to_add_ != null)
            {
                shape_rectangle_to_add_.Draw(g, pen1, 0, scale_, center_pt);
            }
            if (shape_ellipse_to_add_ != null)
            {
                shape_ellipse_to_add_.Draw(g, pen1, 0, scale_, center_pt);
            }

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            foreach(ConnectPoint cp in inner_points_)
            {
                SolidBrush brush = new SolidBrush(Color.Blue);
                Point pt = LogicalToDevicePoint(cp.pt);
                g.FillEllipse(brush, DesignTools.CreateRectangelByCenterPt(pt, 4));
                g.DrawString(cp.name, SystemFonts.DefaultFont, brush, pt.X, pt.Y + 5, sf);
            }

            foreach (ConnectPoint cp in outter_points_)
            {
                SolidBrush brush = new SolidBrush(Color.Blue);
                Point pt = LogicalToDevicePoint(cp.pt);
                g.FillEllipse(brush, DesignTools.CreateRectangelByCenterPt(pt, 4));
                g.DrawString(cp.name, SystemFonts.DefaultFont, brush, pt.X, pt.Y + 5, sf);
            }
        }

        private void button_set_connect_relation_Click(object sender, EventArgs e)
        {
            List<string> node_names = new List<string>();
            foreach (ConnectPoint cp in inner_points_)
            {
                node_names.Add(cp.name);
            }
            foreach (ConnectPoint cp in outter_points_)
            {
                node_names.Add(cp.name);
            }

            CreateConnectionRelationForm dlg = new CreateConnectionRelationForm();
            dlg.InitData(node_names, connect_relations_);
            dlg.ShowDialog();
            dlg.GetRelation(ref connect_relations_);
        }

        private void button_zoom_out_Click(object sender, EventArgs e)
        {
            scale_ *= 2;
            label_scale_info.Text = string.Format("缩放比例: {0}", scale_);
            Invalidate();
        }

        private void button_zoom_in_Click(object sender, EventArgs e)
        {
            scale_ /= 2;
            label_scale_info.Text = string.Format("缩放比例: {0}", scale_);
            Invalidate();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (textBoxtype_.Text == "")
            {
                MessageBox.Show("Type 为空");
                return;
            }
            else if (componentname_s_.Contains(textBoxtype_.Text))
            {
                MessageBox.Show("Type 已存在");
                return;
            }
            else if (textBox_tagname_.Text == "")
            {
                MessageBox.Show("Tag Name 为空");
                return;
            }
            else if (connect_relations_ == null)
            {
                MessageBox.Show("请设置连接关系");
                return;
            }

            new_component.Type = textBoxtype_.Text;
            new_component.TagName = textBox_tagname_.Text;
            new_component.ModelSet = model_set_name_[comboBox_model_set.SelectedIndex];
            new_component.SetInnerConnectPoint(inner_points_);
            new_component.SetOutConnectPoint(outter_points_);

            SetConnectionRelation();
        }

        private void SetConnectionRelation()
        {
            string sep1 = ",";
            string sep2 = ":";
            connection_relation_msg = new_component.Type;
            connection_relation_msg += sep1 + new_component.ModelSet;
            {
                List<string> s = new List<string>();
                foreach (ConnectPoint cp in inner_points_)
                {
                    s.Add(cp.name);
                }
                connection_relation_msg += sep1 + string.Join(sep2, s.ToArray());
            }
            {
                List<string> s = new List<string>();
                foreach (ConnectPoint cp in outter_points_)
                {
                    s.Add(cp.name);
                }
                connection_relation_msg += sep1 + string.Join(sep2, s.ToArray());
            }

            {
                List<string> s = new List<string>();
                for (int i = 0; i < connect_relations_.GetLength(0); ++i)
                {
                    for (int j = i + 1; j < connect_relations_.GetLength(1); ++j)
                    {
                        System.Diagnostics.Debug.Assert(connect_relations_[i, j] <= 2 && connect_relations_[j, i] <= 2);
                        int v = 0;
                        if (connect_relations_[i, j] == 1)
                        {
                            v |= 0x1;
                        }
                        if (connect_relations_[i, j] == 2)
                        {
                            v |= 0x4;
                        }
                        if (connect_relations_[j, i] == 1)
                        {
                            v |= 0x2;
                        }
                        if (connect_relations_[j, i] == 2)
                        {
                            v |= 0x8;
                        }
                        if (v > 0)
                        {
                            s.Add(string.Format("{0}-{1}-{2}", i, j, v));
                        }
                    }
                }
                connection_relation_msg += sep1 + string.Join(sep2, s.ToArray());
            }

            this.Close();
        }

        private void button_cancle_Click(object sender, EventArgs e)
        {
            new_component = null;
            this.Close();
        }
    }
}