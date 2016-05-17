namespace CircuitDesign
{
    partial class FormMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_new_project = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_open_project = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_add_new_network = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_add_circuit_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_save_project = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.电路图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_draw_circuit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_edit_component = new System.Windows.Forms.ToolStripMenuItem();
            this.电路分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_input_states = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_analyze_load = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_analyze_power_ground = new System.Windows.Forms.ToolStripMenuItem();
            this.仿真结果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_output_text = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_output_pic = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox_network = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel_cricuit = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView_circuit_states = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.treeView_reslt = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_circuit_states)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工程ToolStripMenuItem,
            this.电路图ToolStripMenuItem,
            this.电路分析ToolStripMenuItem,
            this.仿真结果ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1041, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 工程ToolStripMenuItem
            // 
            this.工程ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_new_project,
            this.ToolStripMenuItem_open_project,
            this.ToolStripMenuItem_add_new_network,
            this.ToolStripMenuItem_add_circuit_file,
            this.ToolStripMenuItem_save_project,
            this.ToolStripMenuItem_exit});
            this.工程ToolStripMenuItem.Name = "工程ToolStripMenuItem";
            this.工程ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工程ToolStripMenuItem.Text = "工程";
            // 
            // ToolStripMenuItem_new_project
            // 
            this.ToolStripMenuItem_new_project.Name = "ToolStripMenuItem_new_project";
            this.ToolStripMenuItem_new_project.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem_new_project.Text = "新建";
            this.ToolStripMenuItem_new_project.Click += new System.EventHandler(this.ToolStripMenuItem_new_project_Click);
            // 
            // ToolStripMenuItem_open_project
            // 
            this.ToolStripMenuItem_open_project.Name = "ToolStripMenuItem_open_project";
            this.ToolStripMenuItem_open_project.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem_open_project.Text = "打开项目文件";
            this.ToolStripMenuItem_open_project.Click += new System.EventHandler(this.ToolStripMenuItem_open_project_Click);
            // 
            // ToolStripMenuItem_add_new_network
            // 
            this.ToolStripMenuItem_add_new_network.Name = "ToolStripMenuItem_add_new_network";
            this.ToolStripMenuItem_add_new_network.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem_add_new_network.Text = "从网表文件载入";
            this.ToolStripMenuItem_add_new_network.Click += new System.EventHandler(this.ToolStripMenuItem_add_new_network_Click);
            // 
            // ToolStripMenuItem_add_circuit_file
            // 
            this.ToolStripMenuItem_add_circuit_file.Name = "ToolStripMenuItem_add_circuit_file";
            this.ToolStripMenuItem_add_circuit_file.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem_add_circuit_file.Text = "从电路图载入";
            this.ToolStripMenuItem_add_circuit_file.Click += new System.EventHandler(this.ToolStripMenuItem_add_circuit_file_Click);
            // 
            // ToolStripMenuItem_save_project
            // 
            this.ToolStripMenuItem_save_project.Name = "ToolStripMenuItem_save_project";
            this.ToolStripMenuItem_save_project.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem_save_project.Text = "保存";
            this.ToolStripMenuItem_save_project.Click += new System.EventHandler(this.ToolStripMenuItem_save_project_Click);
            // 
            // ToolStripMenuItem_exit
            // 
            this.ToolStripMenuItem_exit.Name = "ToolStripMenuItem_exit";
            this.ToolStripMenuItem_exit.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem_exit.Text = "退出";
            this.ToolStripMenuItem_exit.Click += new System.EventHandler(this.ToolStripMenuItem_exit_Click);
            // 
            // 电路图ToolStripMenuItem
            // 
            this.电路图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_draw_circuit,
            this.ToolStripMenuItem_edit_component});
            this.电路图ToolStripMenuItem.Name = "电路图ToolStripMenuItem";
            this.电路图ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.电路图ToolStripMenuItem.Text = "电路图";
            // 
            // ToolStripMenuItem_draw_circuit
            // 
            this.ToolStripMenuItem_draw_circuit.Name = "ToolStripMenuItem_draw_circuit";
            this.ToolStripMenuItem_draw_circuit.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItem_draw_circuit.Text = "绘制电路图";
            this.ToolStripMenuItem_draw_circuit.Click += new System.EventHandler(this.ToolStripMenuItem_draw_circuit_Click);
            // 
            // ToolStripMenuItem_edit_component
            // 
            this.ToolStripMenuItem_edit_component.Name = "ToolStripMenuItem_edit_component";
            this.ToolStripMenuItem_edit_component.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItem_edit_component.Text = "编辑元件";
            this.ToolStripMenuItem_edit_component.Click += new System.EventHandler(this.ToolStripMenuItem_edit_component_Click);
            // 
            // 电路分析ToolStripMenuItem
            // 
            this.电路分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_input_states,
            this.ToolStripMenuItem_analyze_load,
            this.ToolStripMenuItem_analyze_power_ground});
            this.电路分析ToolStripMenuItem.Name = "电路分析ToolStripMenuItem";
            this.电路分析ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.电路分析ToolStripMenuItem.Text = "电路分析";
            // 
            // ToolStripMenuItem_input_states
            // 
            this.ToolStripMenuItem_input_states.Name = "ToolStripMenuItem_input_states";
            this.ToolStripMenuItem_input_states.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_input_states.Text = "输入分析状态";
            this.ToolStripMenuItem_input_states.Click += new System.EventHandler(this.ToolStripMenuItem_input_states_Click);
            // 
            // ToolStripMenuItem_analyze_load
            // 
            this.ToolStripMenuItem_analyze_load.Name = "ToolStripMenuItem_analyze_load";
            this.ToolStripMenuItem_analyze_load.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_analyze_load.Text = "负载响应分析";
            this.ToolStripMenuItem_analyze_load.Click += new System.EventHandler(this.ToolStripMenuItem_analyze_load_Click);
            // 
            // ToolStripMenuItem_analyze_power_ground
            // 
            this.ToolStripMenuItem_analyze_power_ground.Name = "ToolStripMenuItem_analyze_power_ground";
            this.ToolStripMenuItem_analyze_power_ground.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_analyze_power_ground.Text = "电源与地分析";
            this.ToolStripMenuItem_analyze_power_ground.Click += new System.EventHandler(this.ToolStripMenuItem_analyze_power_ground_Click);
            // 
            // 仿真结果ToolStripMenuItem
            // 
            this.仿真结果ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_output_text,
            this.ToolStripMenuItem_output_pic});
            this.仿真结果ToolStripMenuItem.Name = "仿真结果ToolStripMenuItem";
            this.仿真结果ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.仿真结果ToolStripMenuItem.Text = "仿真结果";
            // 
            // ToolStripMenuItem_output_text
            // 
            this.ToolStripMenuItem_output_text.Name = "ToolStripMenuItem_output_text";
            this.ToolStripMenuItem_output_text.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_output_text.Text = "生成文本报告";
            this.ToolStripMenuItem_output_text.Click += new System.EventHandler(this.ToolStripMenuItem_output_text_Click);
            // 
            // ToolStripMenuItem_output_pic
            // 
            this.ToolStripMenuItem_output_pic.Name = "ToolStripMenuItem_output_pic";
            this.ToolStripMenuItem_output_pic.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_output_pic.Text = "生成图形报告";
            this.ToolStripMenuItem_output_pic.Click += new System.EventHandler(this.ToolStripMenuItem_output_pic_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox_network);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 240);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "网表文件";
            // 
            // richTextBox_network
            // 
            this.richTextBox_network.Location = new System.Drawing.Point(6, 20);
            this.richTextBox_network.Name = "richTextBox_network";
            this.richTextBox_network.Size = new System.Drawing.Size(431, 214);
            this.richTextBox_network.TabIndex = 0;
            this.richTextBox_network.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel_cricuit);
            this.groupBox2.Location = new System.Drawing.Point(461, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(568, 240);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "网表文件对应电路图";
            // 
            // panel_cricuit
            // 
            this.panel_cricuit.Location = new System.Drawing.Point(6, 20);
            this.panel_cricuit.Name = "panel_cricuit";
            this.panel_cricuit.Size = new System.Drawing.Size(556, 220);
            this.panel_cricuit.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView_circuit_states);
            this.groupBox3.Location = new System.Drawing.Point(12, 274);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(443, 296);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "电路状态及负载相应";
            // 
            // dataGridView_circuit_states
            // 
            this.dataGridView_circuit_states.AllowUserToAddRows = false;
            this.dataGridView_circuit_states.AllowUserToDeleteRows = false;
            this.dataGridView_circuit_states.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_circuit_states.Location = new System.Drawing.Point(6, 15);
            this.dataGridView_circuit_states.Name = "dataGridView_circuit_states";
            this.dataGridView_circuit_states.ReadOnly = true;
            this.dataGridView_circuit_states.RowTemplate.Height = 23;
            this.dataGridView_circuit_states.Size = new System.Drawing.Size(431, 274);
            this.dataGridView_circuit_states.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.treeView_reslt);
            this.groupBox4.Location = new System.Drawing.Point(461, 274);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(568, 302);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "仿真结果";
            // 
            // treeView_reslt
            // 
            this.treeView_reslt.Location = new System.Drawing.Point(6, 20);
            this.treeView_reslt.Name = "treeView_reslt";
            this.treeView_reslt.Size = new System.Drawing.Size(556, 269);
            this.treeView_reslt.TabIndex = 2;
            this.treeView_reslt.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_reslt_NodeMouseClick);
            this.treeView_reslt.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_reslt_NodeMouseDoubleClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 575);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "工程项目";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_circuit_states)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_new_project;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_open_project;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_save_project;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_add_new_network;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_exit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBox_network;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_add_circuit_file;
        private System.Windows.Forms.DataGridView dataGridView_circuit_states;
        private System.Windows.Forms.ToolStripMenuItem 电路图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_draw_circuit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_edit_component;
        private System.Windows.Forms.ToolStripMenuItem 电路分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_input_states;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_analyze_load;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_analyze_power_ground;
        private System.Windows.Forms.ToolStripMenuItem 仿真结果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_output_text;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_output_pic;
        private System.Windows.Forms.Panel panel_cricuit;
        private System.Windows.Forms.TreeView treeView_reslt;
    }
}

