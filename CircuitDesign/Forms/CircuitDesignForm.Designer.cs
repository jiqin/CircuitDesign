namespace CircuitDesign
{
    partial class CircuitDesignForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CircuitDesignForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_filepath = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_operation = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_time = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_mouse_op = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_add_connect_line = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_add_connect_point = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_add_power = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_add_ground = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_add_component = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.componentPopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemChangeName = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_TurnLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_TurnRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.componentPopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_filepath,
            this.toolStripStatusLabel_operation,
            this.toolStripStatusLabel_time});
            this.statusStrip1.Location = new System.Drawing.Point(0, 488);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(878, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_filepath
            // 
            this.toolStripStatusLabel_filepath.Name = "toolStripStatusLabel_filepath";
            this.toolStripStatusLabel_filepath.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel_operation
            // 
            this.toolStripStatusLabel_operation.Name = "toolStripStatusLabel_operation";
            this.toolStripStatusLabel_operation.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel_time
            // 
            this.toolStripStatusLabel_time.Name = "toolStripStatusLabel_time";
            this.toolStripStatusLabel_time.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel_time.Text = "系统时间";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_mouse_op,
            this.toolStripButton_add_connect_line,
            this.toolStripButton_add_connect_point,
            this.toolStripButton_add_power,
            this.toolStripButton_add_ground,
            this.toolStripButton_add_component});
            this.toolStrip2.Location = new System.Drawing.Point(9, 9);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(444, 25);
            this.toolStrip2.TabIndex = 0;
            // 
            // toolStripButton_mouse_op
            // 
            this.toolStripButton_mouse_op.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_mouse_op.Image")));
            this.toolStripButton_mouse_op.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_mouse_op.Name = "toolStripButton_mouse_op";
            this.toolStripButton_mouse_op.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton_mouse_op.Text = "鼠标操作";
            this.toolStripButton_mouse_op.Click += new System.EventHandler(this.toolStripButton_mouse_op_Click);
            // 
            // toolStripButton_add_connect_line
            // 
            this.toolStripButton_add_connect_line.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_add_connect_line.Image")));
            this.toolStripButton_add_connect_line.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_add_connect_line.Name = "toolStripButton_add_connect_line";
            this.toolStripButton_add_connect_line.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton_add_connect_line.Text = "连接线";
            this.toolStripButton_add_connect_line.Click += new System.EventHandler(this.toolStripButton_add_connect_line_Click);
            // 
            // toolStripButton_add_connect_point
            // 
            this.toolStripButton_add_connect_point.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_add_connect_point.Image")));
            this.toolStripButton_add_connect_point.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_add_connect_point.Name = "toolStripButton_add_connect_point";
            this.toolStripButton_add_connect_point.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton_add_connect_point.Text = "添加点";
            this.toolStripButton_add_connect_point.Click += new System.EventHandler(this.toolStripButton_add_connect_point_Click);
            // 
            // toolStripButton_add_power
            // 
            this.toolStripButton_add_power.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_add_power.Image")));
            this.toolStripButton_add_power.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_add_power.Name = "toolStripButton_add_power";
            this.toolStripButton_add_power.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton_add_power.Text = "添加电源";
            this.toolStripButton_add_power.Click += new System.EventHandler(this.toolStripButton_add_power_Click);
            // 
            // toolStripButton_add_ground
            // 
            this.toolStripButton_add_ground.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_add_ground.Image")));
            this.toolStripButton_add_ground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_add_ground.Name = "toolStripButton_add_ground";
            this.toolStripButton_add_ground.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton_add_ground.Text = "添加地";
            this.toolStripButton_add_ground.Click += new System.EventHandler(this.toolStripButton_add_ground_Click);
            // 
            // toolStripButton_add_component
            // 
            this.toolStripButton_add_component.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_add_component.Image")));
            this.toolStripButton_add_component.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_add_component.Name = "toolStripButton_add_component";
            this.toolStripButton_add_component.Size = new System.Drawing.Size(88, 22);
            this.toolStripButton_add_component.Text = "添加元器件";
            this.toolStripButton_add_component.Click += new System.EventHandler(this.toolStripButton_add_component_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(345, 0);
            this.toolStripContainer1.Location = new System.Drawing.Point(4, 28);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(345, 25);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // componentPopMenu
            // 
            this.componentPopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemChangeName,
            this.ToolStripMenuItem_TurnLeft,
            this.ToolStripMenuItem_TurnRight,
            this.ToolStripMenuItem_Delete});
            this.componentPopMenu.Name = "componentPopMenu";
            this.componentPopMenu.Size = new System.Drawing.Size(151, 92);
            // 
            // ToolStripMenuItemChangeName
            // 
            this.ToolStripMenuItemChangeName.Name = "ToolStripMenuItemChangeName";
            this.ToolStripMenuItemChangeName.Size = new System.Drawing.Size(150, 22);
            this.ToolStripMenuItemChangeName.Text = "更改元件名称";
            this.ToolStripMenuItemChangeName.Click += new System.EventHandler(this.ToolStripMenuItemChangeName_Click);
            // 
            // ToolStripMenuItem_TurnLeft
            // 
            this.ToolStripMenuItem_TurnLeft.Name = "ToolStripMenuItem_TurnLeft";
            this.ToolStripMenuItem_TurnLeft.Size = new System.Drawing.Size(150, 22);
            this.ToolStripMenuItem_TurnLeft.Text = "向左旋转90度";
            this.ToolStripMenuItem_TurnLeft.Click += new System.EventHandler(this.ToolStripMenuItem_TurnLeft_Click);
            // 
            // ToolStripMenuItem_TurnRight
            // 
            this.ToolStripMenuItem_TurnRight.Name = "ToolStripMenuItem_TurnRight";
            this.ToolStripMenuItem_TurnRight.Size = new System.Drawing.Size(150, 22);
            this.ToolStripMenuItem_TurnRight.Text = "向右旋转90度";
            this.ToolStripMenuItem_TurnRight.Click += new System.EventHandler(this.ToolStripMenuItem_TurnRight_Click);
            // 
            // ToolStripMenuItem_Delete
            // 
            this.ToolStripMenuItem_Delete.Name = "ToolStripMenuItem_Delete";
            this.ToolStripMenuItem_Delete.Size = new System.Drawing.Size(150, 22);
            this.ToolStripMenuItem_Delete.Text = "删除";
            this.ToolStripMenuItem_Delete.Click += new System.EventHandler(this.ToolStripMenuItem_Delete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 510);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "电路设计";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.form_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.form_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.form_MouseUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.componentPopMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip componentPopMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_TurnLeft;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_TurnRight;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Delete;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemChangeName;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton_mouse_op;
        private System.Windows.Forms.ToolStripButton toolStripButton_add_connect_line;
        private System.Windows.Forms.ToolStripButton toolStripButton_add_connect_point;
        private System.Windows.Forms.ToolStripButton toolStripButton_add_power;
        private System.Windows.Forms.ToolStripButton toolStripButton_add_ground;
        private System.Windows.Forms.ToolStripButton toolStripButton_add_component;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_filepath;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_operation;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_time;
    }
}

