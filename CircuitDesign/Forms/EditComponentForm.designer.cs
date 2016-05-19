namespace CircuitDesign
{
    partial class EditComponentForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_cancle = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxtype_ = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_add_ellipse = new System.Windows.Forms.Button();
            this.button_undo_last_draw = new System.Windows.Forms.Button();
            this.button＿add_rectangle = new System.Windows.Forms.Button();
            this.button_add_line = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_op_tip = new System.Windows.Forms.ToolStripStatusLabel();
            this.button_add_out_connectpoint = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_undo_add_inner_point = new System.Windows.Forms.Button();
            this.button_add_out_connect_point = new System.Windows.Forms.Button();
            this.button_set_connect_relation = new System.Windows.Forms.Button();
            this.button_add_inner_connect_point = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox_model_set = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_tagname_ = new System.Windows.Forms.TextBox();
            this.textBox_position = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox_scale = new System.Windows.Forms.GroupBox();
            this.label_scale_info = new System.Windows.Forms.Label();
            this.button_zoom_in = new System.Windows.Forms.Button();
            this.button_zoom_out = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox_scale.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_cancle
            // 
            this.button_cancle.Location = new System.Drawing.Point(107, 481);
            this.button_cancle.Name = "button_cancle";
            this.button_cancle.Size = new System.Drawing.Size(75, 23);
            this.button_cancle.TabIndex = 0;
            this.button_cancle.Text = "取消";
            this.button_cancle.UseVisualStyleBackColor = true;
            this.button_cancle.Click += new System.EventHandler(this.button_cancle_Click);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(26, 481);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "确定";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type:";
            // 
            // textBoxtype_
            // 
            this.textBoxtype_.Location = new System.Drawing.Point(72, 20);
            this.textBoxtype_.Name = "textBoxtype_";
            this.textBoxtype_.Size = new System.Drawing.Size(84, 21);
            this.textBoxtype_.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_add_ellipse);
            this.groupBox1.Controls.Add(this.button_undo_last_draw);
            this.groupBox1.Controls.Add(this.button＿add_rectangle);
            this.groupBox1.Controls.Add(this.button_add_line);
            this.groupBox1.Location = new System.Drawing.Point(35, 205);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 144);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "形状";
            // 
            // button_add_ellipse
            // 
            this.button_add_ellipse.Location = new System.Drawing.Point(43, 81);
            this.button_add_ellipse.Name = "button_add_ellipse";
            this.button_add_ellipse.Size = new System.Drawing.Size(75, 23);
            this.button_add_ellipse.TabIndex = 3;
            this.button_add_ellipse.Text = "添加椭圆";
            this.button_add_ellipse.UseVisualStyleBackColor = true;
            this.button_add_ellipse.Click += new System.EventHandler(this.button_add_ellipse_Click);
            // 
            // button_undo_last_draw
            // 
            this.button_undo_last_draw.Location = new System.Drawing.Point(43, 110);
            this.button_undo_last_draw.Name = "button_undo_last_draw";
            this.button_undo_last_draw.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button_undo_last_draw.Size = new System.Drawing.Size(87, 23);
            this.button_undo_last_draw.TabIndex = 11;
            this.button_undo_last_draw.Text = "撤销上个图形";
            this.button_undo_last_draw.UseVisualStyleBackColor = true;
            this.button_undo_last_draw.Click += new System.EventHandler(this.button_undo_last_draw_Click);
            // 
            // button＿add_rectangle
            // 
            this.button＿add_rectangle.Location = new System.Drawing.Point(43, 52);
            this.button＿add_rectangle.Name = "button＿add_rectangle";
            this.button＿add_rectangle.Size = new System.Drawing.Size(75, 23);
            this.button＿add_rectangle.TabIndex = 1;
            this.button＿add_rectangle.Text = "添加矩形";
            this.button＿add_rectangle.UseVisualStyleBackColor = true;
            this.button＿add_rectangle.Click += new System.EventHandler(this.button＿add_rectangle_Click);
            // 
            // button_add_line
            // 
            this.button_add_line.Location = new System.Drawing.Point(43, 20);
            this.button_add_line.Name = "button_add_line";
            this.button_add_line.Size = new System.Drawing.Size(75, 23);
            this.button_add_line.TabIndex = 0;
            this.button_add_line.Text = "添加连线";
            this.button_add_line.UseVisualStyleBackColor = true;
            this.button_add_line.Click += new System.EventHandler(this.button_add_line_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_op_tip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 534);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(687, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_op_tip
            // 
            this.toolStripStatusLabel_op_tip.Name = "toolStripStatusLabel_op_tip";
            this.toolStripStatusLabel_op_tip.Size = new System.Drawing.Size(0, 17);
            // 
            // button_add_out_connectpoint
            // 
            this.button_add_out_connectpoint.Location = new System.Drawing.Point(14, 20);
            this.button_add_out_connectpoint.Name = "button_add_out_connectpoint";
            this.button_add_out_connectpoint.Size = new System.Drawing.Size(75, 23);
            this.button_add_out_connectpoint.TabIndex = 6;
            this.button_add_out_connectpoint.Text = "添加外连接点";
            this.button_add_out_connectpoint.UseVisualStyleBackColor = true;
            this.button_add_out_connectpoint.Click += new System.EventHandler(this.button_add_out_connectpoint_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_undo_add_inner_point);
            this.groupBox2.Controls.Add(this.button_add_out_connect_point);
            this.groupBox2.Controls.Add(this.button_set_connect_relation);
            this.groupBox2.Controls.Add(this.button_add_inner_connect_point);
            this.groupBox2.Controls.Add(this.button_add_out_connectpoint);
            this.groupBox2.Location = new System.Drawing.Point(35, 355);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 110);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "连接状态";
            // 
            // button_undo_add_inner_point
            // 
            this.button_undo_add_inner_point.Location = new System.Drawing.Point(95, 49);
            this.button_undo_add_inner_point.Name = "button_undo_add_inner_point";
            this.button_undo_add_inner_point.Size = new System.Drawing.Size(52, 23);
            this.button_undo_add_inner_point.TabIndex = 10;
            this.button_undo_add_inner_point.Text = "撤销";
            this.button_undo_add_inner_point.UseVisualStyleBackColor = true;
            this.button_undo_add_inner_point.Click += new System.EventHandler(this.button_undo_add_inner_point_Click);
            // 
            // button_add_out_connect_point
            // 
            this.button_add_out_connect_point.Location = new System.Drawing.Point(95, 20);
            this.button_add_out_connect_point.Name = "button_add_out_connect_point";
            this.button_add_out_connect_point.Size = new System.Drawing.Size(52, 23);
            this.button_add_out_connect_point.TabIndex = 9;
            this.button_add_out_connect_point.Text = "撤销";
            this.button_add_out_connect_point.UseVisualStyleBackColor = true;
            this.button_add_out_connect_point.Click += new System.EventHandler(this.button_add_out_connect_point_Click);
            // 
            // button_set_connect_relation
            // 
            this.button_set_connect_relation.Location = new System.Drawing.Point(43, 78);
            this.button_set_connect_relation.Name = "button_set_connect_relation";
            this.button_set_connect_relation.Size = new System.Drawing.Size(87, 23);
            this.button_set_connect_relation.TabIndex = 8;
            this.button_set_connect_relation.Text = "设置连接关系";
            this.button_set_connect_relation.UseVisualStyleBackColor = true;
            this.button_set_connect_relation.Click += new System.EventHandler(this.button_set_connect_relation_Click);
            // 
            // button_add_inner_connect_point
            // 
            this.button_add_inner_connect_point.Location = new System.Drawing.Point(14, 49);
            this.button_add_inner_connect_point.Name = "button_add_inner_connect_point";
            this.button_add_inner_connect_point.Size = new System.Drawing.Size(75, 23);
            this.button_add_inner_connect_point.TabIndex = 7;
            this.button_add_inner_connect_point.Text = "添加内连接点";
            this.button_add_inner_connect_point.UseVisualStyleBackColor = true;
            this.button_add_inner_connect_point.Click += new System.EventHandler(this.button_add_inner_connect_point_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button_ok);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button_cancle);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 533);
            this.panel1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "设置属性";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox_model_set);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.textBox_tagname_);
            this.groupBox3.Controls.Add(this.textBox_position);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBoxtype_);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(35, 42);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(167, 140);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "基本属性";
            // 
            // comboBox_model_set
            // 
            this.comboBox_model_set.FormattingEnabled = true;
            this.comboBox_model_set.Location = new System.Drawing.Point(72, 106);
            this.comboBox_model_set.Name = "comboBox_model_set";
            this.comboBox_model_set.Size = new System.Drawing.Size(84, 20);
            this.comboBox_model_set.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "元件类型:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "名称:";
            // 
            // textBox_tagname_
            // 
            this.textBox_tagname_.Location = new System.Drawing.Point(72, 46);
            this.textBox_tagname_.Name = "textBox_tagname_";
            this.textBox_tagname_.Size = new System.Drawing.Size(84, 21);
            this.textBox_tagname_.TabIndex = 12;
            // 
            // textBox_position
            // 
            this.textBox_position.Enabled = false;
            this.textBox_position.Location = new System.Drawing.Point(72, 73);
            this.textBox_position.Name = "textBox_position";
            this.textBox_position.Size = new System.Drawing.Size(84, 21);
            this.textBox_position.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "图形大小:";
            // 
            // groupBox_scale
            // 
            this.groupBox_scale.Controls.Add(this.label_scale_info);
            this.groupBox_scale.Controls.Add(this.button_zoom_in);
            this.groupBox_scale.Controls.Add(this.button_zoom_out);
            this.groupBox_scale.Location = new System.Drawing.Point(295, 18);
            this.groupBox_scale.Name = "groupBox_scale";
            this.groupBox_scale.Size = new System.Drawing.Size(328, 57);
            this.groupBox_scale.TabIndex = 10;
            this.groupBox_scale.TabStop = false;
            this.groupBox_scale.Text = "绘制元件图形";
            // 
            // label_scale_info
            // 
            this.label_scale_info.AutoSize = true;
            this.label_scale_info.Location = new System.Drawing.Point(218, 25);
            this.label_scale_info.Name = "label_scale_info";
            this.label_scale_info.Size = new System.Drawing.Size(59, 12);
            this.label_scale_info.TabIndex = 3;
            this.label_scale_info.Text = "缩放比例:";
            // 
            // button_zoom_in
            // 
            this.button_zoom_in.Location = new System.Drawing.Point(115, 20);
            this.button_zoom_in.Name = "button_zoom_in";
            this.button_zoom_in.Size = new System.Drawing.Size(75, 23);
            this.button_zoom_in.TabIndex = 2;
            this.button_zoom_in.Text = "缩小";
            this.button_zoom_in.UseVisualStyleBackColor = true;
            this.button_zoom_in.Click += new System.EventHandler(this.button_zoom_in_Click);
            // 
            // button_zoom_out
            // 
            this.button_zoom_out.Location = new System.Drawing.Point(19, 20);
            this.button_zoom_out.Name = "button_zoom_out";
            this.button_zoom_out.Size = new System.Drawing.Size(75, 23);
            this.button_zoom_out.TabIndex = 1;
            this.button_zoom_out.Text = "放大";
            this.button_zoom_out.UseVisualStyleBackColor = true;
            this.button_zoom_out.Click += new System.EventHandler(this.button_zoom_out_Click);
            // 
            // EditComponentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 556);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox_scale);
            this.Name = "EditComponentForm";
            this.Text = "编辑元件";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AddComponentTemplateDlg_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddComponentTemplateDlg_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AddComponentTemplateDlg_MouseMove);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox_scale.ResumeLayout(false);
            this.groupBox_scale.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancle;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxtype_;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button＿add_rectangle;
        private System.Windows.Forms.Button button_add_line;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_op_tip;
        private System.Windows.Forms.Button button_add_out_connectpoint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_add_ellipse;
        private System.Windows.Forms.Button button_undo_last_draw;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox_position;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_tagname_;
        private System.Windows.Forms.Button button_set_connect_relation;
        private System.Windows.Forms.Button button_add_inner_connect_point;
        private System.Windows.Forms.Button button_undo_add_inner_point;
        private System.Windows.Forms.Button button_add_out_connect_point;
        private System.Windows.Forms.GroupBox groupBox_scale;
        private System.Windows.Forms.Button button_zoom_in;
        private System.Windows.Forms.Button button_zoom_out;
        private System.Windows.Forms.Label label_scale_info;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_model_set;
    }
}