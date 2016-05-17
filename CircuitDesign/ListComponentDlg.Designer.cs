namespace CircuitDesign
{
    partial class ListComponentDlg
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_add_component = new System.Windows.Forms.Button();
            this.button_remove_component = new System.Windows.Forms.Button();
            this.button_edit_component = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(291, 268);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(12, 299);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "确定";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(122, 299);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(318, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 268);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button_add_component
            // 
            this.button_add_component.Location = new System.Drawing.Point(342, 299);
            this.button_add_component.Name = "button_add_component";
            this.button_add_component.Size = new System.Drawing.Size(75, 23);
            this.button_add_component.TabIndex = 4;
            this.button_add_component.Text = "添加元件";
            this.button_add_component.UseVisualStyleBackColor = true;
            this.button_add_component.Click += new System.EventHandler(this.button_add_component_Click);
            // 
            // button_remove_component
            // 
            this.button_remove_component.Location = new System.Drawing.Point(451, 299);
            this.button_remove_component.Name = "button_remove_component";
            this.button_remove_component.Size = new System.Drawing.Size(75, 23);
            this.button_remove_component.TabIndex = 5;
            this.button_remove_component.Text = "删除元件";
            this.button_remove_component.UseVisualStyleBackColor = true;
            this.button_remove_component.Click += new System.EventHandler(this.button_remove_component_Click);
            // 
            // button_edit_component
            // 
            this.button_edit_component.Location = new System.Drawing.Point(228, 299);
            this.button_edit_component.Name = "button_edit_component";
            this.button_edit_component.Size = new System.Drawing.Size(75, 23);
            this.button_edit_component.TabIndex = 6;
            this.button_edit_component.Text = "编辑元件";
            this.button_edit_component.UseVisualStyleBackColor = true;
            this.button_edit_component.Click += new System.EventHandler(this.button_edit_component_Click);
            // 
            // ListComponentDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 334);
            this.Controls.Add(this.button_edit_component);
            this.Controls.Add(this.button_remove_component);
            this.Controls.Add(this.button_add_component);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.listBox1);
            this.Name = "ListComponentDlg";
            this.Text = "元件列表";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_add_component;
        private System.Windows.Forms.Button button_remove_component;
        private System.Windows.Forms.Button button_edit_component;
    }
}