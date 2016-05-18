using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CircuitDesign
{
    public partial class CreateConnectionRelationForm : Form
    {
        private string[] nodenames_;
        private int[,] relations_;
        private int data_index0_ = -1;
        private int data_index1_ = -1;
        private const int SPAN = 20;
        private const int CELL_W = 80;
        private const int CELL_H = 30;

        public CreateConnectionRelationForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        public void InitData(List<string> nodenames, int[,] relations)
        {
            nodenames_ = new string[nodenames.Count];
            nodenames.CopyTo(nodenames_);

            relations_ = new int[nodenames_.Length, nodenames_.Length];
            //if (relations != null)
            //{
            //    for (int i = 0; i < nodenames_.Length; ++i)
            //    {
            //        for (int j = 0; j < nodenames_.Length; ++j)
            //        {
            //            relations_[i, j] = relations[i, j];
            //        }
            //    }
            //}
            //else
            {
                for (int i = 0; i < nodenames_.Length; ++i)
                {
                    relations_[i, i] = 1;
                }
            }
            this.Width = CELL_W * (1 + nodenames_.Length) + SPAN * 2;
            this.Height = CELL_H * (1 + nodenames_.Length) + SPAN * 2 + 30;

            Invalidate();
        }

        public void GetRelation(ref int[,] relations)
        {
            relations = relations_;
        }

        private Rectangle GetRect(int index0, int index1)
        {
            return new Rectangle(index1 * CELL_W + SPAN, index0 * CELL_H + SPAN, CELL_W, CELL_H);
        }

        private Rectangle GetDataRect(int index0, int index1)
        {
            return GetRect(index0 + 1, index1 + 1);
        }

        private void GetDataIndex(Point pt, out int index0, out int index1)
        {
            index0 = (pt.Y - SPAN) / CELL_H - 1;
            index1 = (pt.X - SPAN) / CELL_W - 1;
        }

        private void CreateConnectionRelation_MouseClick(object sender, MouseEventArgs e)
        {
            int index0, index1;
            GetDataIndex(e.Location, out index0, out index1);
            if (index0 >= 0 && index0 < nodenames_.Length && 
                index1 >= 0 && index1 < nodenames_.Length &&
                index0 != index1)
            {
                data_index0_ = index0;
                data_index1_ = index1;
                contextMenuStrip_connection_relation.Show(PointToScreen(e.Location));
            }
        }

        private void CreateConnectionRelation_Paint(object sender, PaintEventArgs e)
        {
            Brush[] brushes = new Brush[10];
            brushes[0] = Brushes.Gray;
            brushes[1] = Brushes.Blue;
            brushes[2] = Brushes.LightBlue;
            brushes[3] = Brushes.Red;
            brushes[4] = Brushes.Purple;

            Pen pen = new Pen(Color.Blue, 2.0f);
            Font font = new Font(FontFamily.GenericSerif, CELL_H * 0.4f);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            e.Graphics.DrawRectangle(pen, GetRect(0, 0));
            for (int i = 0; i < nodenames_.Length; ++i)
            {
                Rectangle rect = GetRect(i + 1, 0);
                e.Graphics.DrawRectangle(pen, rect);
                e.Graphics.DrawString(nodenames_[i], font, Brushes.Green, rect, sf);

                rect = GetRect(0, i + 1);
                e.Graphics.DrawRectangle(pen, rect);
                e.Graphics.DrawString(nodenames_[i], font, Brushes.Green, rect, sf);
            }
            for (int index0 = 0; index0 < nodenames_.Length; ++index0)
            {
                for (int index1 = 0; index1 < nodenames_.Length; ++index1)
                {
                    Rectangle rect = GetDataRect(index0, index1);
                    e.Graphics.DrawRectangle(pen, rect);
                    int n = relations_[index0, index1];
                    if (n < 0 || n >= 5)
                    {
                        e.Graphics.FillRectangle(Brushes.Gray, rect);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(brushes[n], rect);
                    }
                    e.Graphics.DrawString(relations_[index0, index1].ToString(), font, Brushes.Red, rect, sf);
                }
            }
        }

        private void HandleRelation(int value)
        {
            if (data_index0_ >= 0 && data_index0_ < nodenames_.Length &&
                data_index1_ >= 0 && data_index1_ < nodenames_.Length)
            {
                relations_[data_index0_, data_index1_] = value;
                if (value == 2)
                {
                    relations_[data_index1_, data_index0_] = 3;
                }
                else if (relations_[data_index1_, data_index0_] == 3 || relations_[data_index1_, data_index0_] == 2)
                {
                    relations_[data_index1_, data_index0_] = 0;
                }
                data_index0_ = -1;
                data_index1_ = -1;
                Invalidate();
            }
        }

        private void ToolStripMenuItem_0_Click(object sender, EventArgs e)
        {
            HandleRelation(0);
        }

        private void ToolStripMenuItem_1_Click(object sender, EventArgs e)
        {
            HandleRelation(1);
        }

        private void ToolStripMenuItem_2_Click(object sender, EventArgs e)
        {
            HandleRelation(2);
        }
    }
}