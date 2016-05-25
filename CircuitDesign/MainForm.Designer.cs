namespace CircuitDesign
{
    partial class MainForm
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
            this.richTextBox_netlist = new System.Windows.Forms.RichTextBox();
            this.panel_cricuit_diagram = new System.Windows.Forms.Panel();
            this.dataGridView_circuit_states = new System.Windows.Forms.DataGridView();
            this.treeView_simulate_result = new System.Windows.Forms.TreeView();
            this.splitContainerOutter = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainerRight = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_circuit_states)).BeginInit();
            this.splitContainerOutter.Panel1.SuspendLayout();
            this.splitContainerOutter.Panel2.SuspendLayout();
            this.splitContainerOutter.SuspendLayout();
            this.splitContainerLeft.Panel1.SuspendLayout();
            this.splitContainerLeft.Panel2.SuspendLayout();
            this.splitContainerLeft.SuspendLayout();
            this.splitContainerRight.Panel1.SuspendLayout();
            this.splitContainerRight.Panel2.SuspendLayout();
            this.splitContainerRight.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1050, 25);
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
            // richTextBox_netlist
            // 
            this.richTextBox_netlist.Location = new System.Drawing.Point(6, 20);
            this.richTextBox_netlist.Name = "richTextBox_netlist";
            this.richTextBox_netlist.Size = new System.Drawing.Size(100, 100);
            this.richTextBox_netlist.TabIndex = 0;
            this.richTextBox_netlist.Text = "";
            // 
            // panel_cricuit_diagram
            // 
            this.panel_cricuit_diagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_cricuit_diagram.Location = new System.Drawing.Point(6, 20);
            this.panel_cricuit_diagram.Name = "panel_cricuit_diagram";
            this.panel_cricuit_diagram.Size = new System.Drawing.Size(100, 100);
            this.panel_cricuit_diagram.TabIndex = 0;
            // 
            // dataGridView_circuit_states
            // 
            this.dataGridView_circuit_states.AllowUserToAddRows = false;
            this.dataGridView_circuit_states.AllowUserToDeleteRows = false;
            this.dataGridView_circuit_states.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_circuit_states.Location = new System.Drawing.Point(6, 20);
            this.dataGridView_circuit_states.Name = "dataGridView_circuit_states";
            this.dataGridView_circuit_states.ReadOnly = true;
            this.dataGridView_circuit_states.RowTemplate.Height = 23;
            this.dataGridView_circuit_states.Size = new System.Drawing.Size(100, 100);
            this.dataGridView_circuit_states.TabIndex = 0;
            // 
            // treeView_simulate_result
            // 
            this.treeView_simulate_result.Location = new System.Drawing.Point(6, 20);
            this.treeView_simulate_result.Name = "treeView_simulate_result";
            this.treeView_simulate_result.Size = new System.Drawing.Size(100, 100);
            this.treeView_simulate_result.TabIndex = 2;
            this.treeView_simulate_result.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_simulate_result_NodeMouseClick);
            this.treeView_simulate_result.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_simulate_result_NodeMouseDoubleClick);
            // 
            // splitContainerOutter
            // 
            this.splitContainerOutter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerOutter.Location = new System.Drawing.Point(4, 27);
            this.splitContainerOutter.Name = "splitContainerOutter";
            // 
            // splitContainerOutter.Panel1
            // 
            this.splitContainerOutter.Panel1.Controls.Add(this.splitContainerLeft);
            // 
            // splitContainerOutter.Panel2
            // 
            this.splitContainerOutter.Panel2.Controls.Add(this.splitContainerRight);
            this.splitContainerOutter.Size = new System.Drawing.Size(1041, 549);
            this.splitContainerOutter.SplitterDistance = 384;
            this.splitContainerOutter.SplitterWidth = 1;
            this.splitContainerOutter.TabIndex = 2;
            // 
            // splitContainerLeft
            // 
            this.splitContainerLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeft.Name = "splitContainerLeft";
            this.splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeft.Panel1
            // 
            this.splitContainerLeft.Panel1.Controls.Add(this.richTextBox_netlist);
            this.splitContainerLeft.Panel1.Controls.Add(this.label1);
            this.splitContainerLeft.Panel1.SizeChanged += new System.EventHandler(this.splitContainer2_Panel1_SizeChanged);
            // 
            // splitContainerLeft.Panel2
            // 
            this.splitContainerLeft.Panel2.Controls.Add(this.dataGridView_circuit_states);
            this.splitContainerLeft.Panel2.Controls.Add(this.label2);
            this.splitContainerLeft.Panel2.SizeChanged += new System.EventHandler(this.splitContainer2_Panel2_SizeChanged);
            this.splitContainerLeft.Size = new System.Drawing.Size(384, 549);
            this.splitContainerLeft.SplitterDistance = 269;
            this.splitContainerLeft.SplitterWidth = 1;
            this.splitContainerLeft.TabIndex = 0;
            this.splitContainerLeft.SizeChanged += new System.EventHandler(this.splitContainer2_SizeChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "网表文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "电路状态及负载响应";
            // 
            // splitContainerRight
            // 
            this.splitContainerRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRight.Name = "splitContainerRight";
            this.splitContainerRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRight.Panel1
            // 
            this.splitContainerRight.Panel1.Controls.Add(this.panel_cricuit_diagram);
            this.splitContainerRight.Panel1.Controls.Add(this.label3);
            this.splitContainerRight.Panel1.SizeChanged += new System.EventHandler(this.splitContainer3_Panel1_SizeChanged);
            // 
            // splitContainerRight.Panel2
            // 
            this.splitContainerRight.Panel2.Controls.Add(this.treeView_simulate_result);
            this.splitContainerRight.Panel2.Controls.Add(this.label4);
            this.splitContainerRight.Panel2.SizeChanged += new System.EventHandler(this.splitContainer3_Panel2_SizeChanged);
            this.splitContainerRight.Size = new System.Drawing.Size(656, 549);
            this.splitContainerRight.SplitterDistance = 269;
            this.splitContainerRight.SplitterWidth = 1;
            this.splitContainerRight.TabIndex = 0;
            this.splitContainerRight.SizeChanged += new System.EventHandler(this.splitContainer3_SizeChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "网表文件对应电路图";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "仿真结果";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 575);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainerOutter);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "工程项目";
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_circuit_states)).EndInit();
            this.splitContainerOutter.Panel1.ResumeLayout(false);
            this.splitContainerOutter.Panel2.ResumeLayout(false);
            this.splitContainerOutter.ResumeLayout(false);
            this.splitContainerLeft.Panel1.ResumeLayout(false);
            this.splitContainerLeft.Panel1.PerformLayout();
            this.splitContainerLeft.Panel2.ResumeLayout(false);
            this.splitContainerLeft.Panel2.PerformLayout();
            this.splitContainerLeft.ResumeLayout(false);
            this.splitContainerRight.Panel1.ResumeLayout(false);
            this.splitContainerRight.Panel1.PerformLayout();
            this.splitContainerRight.Panel2.ResumeLayout(false);
            this.splitContainerRight.Panel2.PerformLayout();
            this.splitContainerRight.ResumeLayout(false);
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
        private System.Windows.Forms.RichTextBox richTextBox_netlist;
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
        private System.Windows.Forms.Panel panel_cricuit_diagram;
        private System.Windows.Forms.TreeView treeView_simulate_result;
        private System.Windows.Forms.SplitContainer splitContainerOutter;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.SplitContainer splitContainerRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

