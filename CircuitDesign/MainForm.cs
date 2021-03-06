﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CircuitDesign
{
    public partial class MainForm : Form
    {
        string component_model_file_path = "Resources\\component_model.mdb";
        string circuit_template_file_path = "Resources\\component_template.xml";

        string init_dir_project = "results\\Projlist";
        string file_name_filter_project = "项目文件(*.proj)|*.proj";
        string init_dir_netlist = "results\\Netlist";
        string file_name_filter_network = "网表文件(*.net)|*.net";
        string init_dir_circuit = "results\\Circuit";
        string file_name_filter_circuit = "电路图文件 (*.xml)|*.xml";
        string file_name_output_text = "文本输出文件 (*.txt)|*.txt";

        CircuitNetlistManager circuit_netlist_manager;

        public MainForm()
        {
            InitializeComponent();
            resize_controls();
            circuit_netlist_manager = new CircuitNetlistManager(component_model_file_path, circuit_template_file_path);
        }

        /*
         * 弹出打开文件对话框，返回选择的文件
         */
        private string get_load_project_file_name(string init_dir, string filter)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = init_dir;
            dlg.Filter = filter;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            return dlg.FileName;
        }

        /*
         * 弹出保存文件对话框，返回选择的文件
         */
        private string get_save_file_name(string init_dir, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = init_dir;
            dlg.Filter = filter;
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            return dlg.FileName;
        }

        private string get_save_project_file_name()
        {
            return get_save_file_name(init_dir_project, file_name_filter_project);
        }

        private void _save_project()
        {
            string save_file_name = get_save_project_file_name();
            if (save_file_name != null)
            {
                circuit_netlist_manager.set_file(save_file_name);
                circuit_netlist_manager.save();
            }
        }

        private void init_data_grid_view_circuit_states()
        {
            InputSwitchLoadStatesForm.init_data_grid_view(
                dataGridView_circuit_states,
                true,
                circuit_netlist_manager.get_switch_load_names(),
                circuit_netlist_manager.get_switch_load_states());
        }

        private void init_project()
        {
            richTextBox_netlist.Text = circuit_netlist_manager.get_netlist_content();
            init_data_grid_view_circuit_states();
            draw_pannel_cricuit(null);
            treeView_simulate_result.Nodes.Clear();
        }

        private void draw_pannel_cricuit(AnalyzeResult result)
        {
            Image im_src = circuit_netlist_manager.get_circuit_image(result);

            if (im_src != null)
            {
                Image im_to = new Bitmap(this.panel_cricuit_diagram.Width, this.panel_cricuit_diagram.Height);
                Graphics g = Graphics.FromImage(im_to);
                this.panel_cricuit_diagram.BackgroundImageLayout = ImageLayout.Stretch;
                this.panel_cricuit_diagram.BackgroundImage = im_to;

                GraphicsUnit unit = new GraphicsUnit();
                RectangleF rect_src = im_src.GetBounds(ref unit);
                g.DrawImage(im_src, panel_cricuit_diagram.ClientRectangle, rect_src, unit);
            }
        }

        private void ToolStripMenuItem_new_project_Click(object sender, EventArgs e)
        {
            circuit_netlist_manager.new_project();
            init_project();
            _save_project();
        }

        private void ToolStripMenuItem_save_project_Click(object sender, EventArgs e)
        {
            _save_project();
        }

        private void ToolStripMenuItem_open_project_Click(object sender, EventArgs e)
        {
            string load_file_name = get_load_project_file_name(init_dir_project, file_name_filter_project);
            if (load_file_name != null)
            {
                circuit_netlist_manager.load_from_project(load_file_name);
                init_project();

                circuit_netlist_manager.set_file(load_file_name);
            }
        }

        private void ToolStripMenuItem_add_new_network_Click(object sender, EventArgs e)
        {
            string load_file_name = get_load_project_file_name(init_dir_netlist, file_name_filter_network);
            if (load_file_name != null)
            {
                circuit_netlist_manager.load_from_network(load_file_name);
                init_project();
                _save_project();
            }
        }

        private void ToolStripMenuItem_add_circuit_file_Click(object sender, EventArgs e)
        {
            string load_file_name = get_load_project_file_name(init_dir_circuit, file_name_filter_circuit);
            if (load_file_name != null)
            {
                circuit_netlist_manager.load_from_circuit(load_file_name);
                init_project();
                _save_project();
            }
        }

        private void ToolStripMenuItem_exit_Click(object sender, EventArgs e)
        {
            circuit_netlist_manager.save();
            this.Close();
        }

        private void ToolStripMenuItem_draw_circuit_Click(object sender, EventArgs e)
        {
            CircuitManager cm = circuit_netlist_manager.get_circuit_manager();
            if (cm != null)
            {
                CircuitDesign.CircuitDesignForm form = new CircuitDesign.CircuitDesignForm();
                form.init_circuit_manager(cm);

                circuit_netlist_manager.save();
                form.ShowDialog();

                draw_pannel_cricuit(null);
                if (MessageBox.Show("是否通过电路图重新生成网表数据?\n这将会覆盖现有网表数据!",
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    circuit_netlist_manager.reload_network_by_circuit();
                    init_project();
                }
            }
        }

        private void ToolStripMenuItem_edit_component_Click(object sender, EventArgs e)
        {
            ComponentTemplateManager ct = circuit_netlist_manager.get_circuit_component_template();
            NetlistComponentTemplateManager nt = circuit_netlist_manager.get_netlist_component_template();
            ListComponentForm dlg = new ListComponentForm();
            dlg.InitComponents(ct, nt, true);
            dlg.ShowDialog();
        }

        private void ToolStripMenuItem_input_states_Click(object sender, EventArgs e)
        {
            InputSwitchLoadStatesForm input_dlg = new InputSwitchLoadStatesForm();
            input_dlg.Init(circuit_netlist_manager.get_switch_load_names(), circuit_netlist_manager.get_switch_load_states());
            input_dlg.ShowDialog();

            List<SwitchLoadStatesInput> states = new List<SwitchLoadStatesInput>();
            input_dlg.get_input(states);
            circuit_netlist_manager.set_switch_load_states(states);
            init_data_grid_view_circuit_states();
        }

        private void ToolStripMenuItem_analyze_load_Click(object sender, EventArgs e)
        {
            List<AnalyzeResult> results = circuit_netlist_manager.analyze();
            treeView_simulate_result.Nodes.Clear();
            foreach (AnalyzeResult result in results)
            {
                TreeNode tn = new TreeNode(result.get_load_result_message());
                tn.Tag = result;
                treeView_simulate_result.Nodes.Add(tn);

                if (result.is_expected_load_status)
                {
                    //tn.BackColor = Color.Green;
                }
                else
                {
                    tn.BackColor = Color.Red;
                }
            }
            treeView_simulate_result.ExpandAll();
        }

        private void ToolStripMenuItem_analyze_power_ground_Click(object sender, EventArgs e)
        {
            List<AnalyzeResult> results = circuit_netlist_manager.analyze();
            treeView_simulate_result.Nodes.Clear();
            foreach (AnalyzeResult result in results)
            {
                TreeNode tn = new TreeNode(result.get_result_message_head());
                tn.Tag = result;
                treeView_simulate_result.Nodes.Add(tn);

                if (result.all_paths.Count == 0)
                {
                    TreeNode tn_child = new TreeNode("无通路");
                    tn.Nodes.Add(tn_child);
                }
                else
                {
                    for (int i = 0; i < result.all_paths.Count; i++)
                    {
                        TreeNode tn_child = new TreeNode(result.get_path_message(result.all_paths[i], i));
                        tn.Nodes.Add(tn_child);
                    }
                }
            }
            treeView_simulate_result.ExpandAll();
        }

        private void ToolStripMenuItem_output_text_Click(object sender, EventArgs e)
        {
            string file_name = get_save_file_name(".", file_name_output_text);
            if (file_name == null)
            {
                return;
            }

            string output_text = circuit_netlist_manager.output_text_result();
            File.WriteAllText(file_name, output_text);
            MessageBox.Show("保存文字报告成功!");
        }

        private void ToolStripMenuItem_output_pic_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = ".";

            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string file_path = fbd.SelectedPath;
            if (file_path == null)
            {
                return;
            }

            List<AnalyzeResult> results = circuit_netlist_manager.analyze();
            foreach (AnalyzeResult result in results)
            {
                string file_name = String.Format("{0}\\状态_{1}.png", file_path, result.index);
                Image im_src = circuit_netlist_manager.get_circuit_image(result);
                im_src.Save(file_name);

                if (im_src != null)
                {
                    im_src.Save(file_name, System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            MessageBox.Show("保存图形报告成功!");
        }

        private void treeView_simulate_result_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView_simulate_result_select(e.Node);
        }

        private void treeView_simulate_result_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView_simulate_result_select(e.Node);
        }

        private void treeView_simulate_result_select(TreeNode tn)
        {
            while (tn.Parent != null)
            {
                tn = tn.Parent;
            }
            if (tn.Tag == null)
            {
                return;
            }

            AnalyzeResult result = (AnalyzeResult)tn.Tag;
            draw_pannel_cricuit(result);
        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {

        }

        private void resize_control(Control control, Size containerSize)
        {
            control.Size = new Size(containerSize.Width - control.Location.X - 4, containerSize.Height - control.Location.Y - 4);
        }

        private void resize_controls()
        {
            resize_control(splitContainerOutter, this.ClientSize);

            Control[] controls = new Control[] { 
                richTextBox_netlist, dataGridView_circuit_states, panel_cricuit_diagram, treeView_simulate_result};
            SplitterPanel[] panels = new SplitterPanel[] { 
                splitContainerLeft.Panel1, splitContainerLeft.Panel2, splitContainerRight.Panel1, splitContainerRight.Panel2};

            for (int i = 0; i < controls.Length; ++i)
            {
                resize_control(controls[i], panels[i].Size);
            }
        }

        private void splitContainer2_SizeChanged(object sender, EventArgs e)
        {
            resize_controls();
        }

        private void splitContainer2_Panel1_SizeChanged(object sender, EventArgs e)
        {
            resize_controls();
        }

        private void splitContainer2_Panel2_SizeChanged(object sender, EventArgs e)
        {
            resize_controls();
        }

        private void splitContainer3_SizeChanged(object sender, EventArgs e)
        {
            resize_controls();
        }

        private void splitContainer3_Panel1_SizeChanged(object sender, EventArgs e)
        {
            resize_controls();
        }

        private void splitContainer3_Panel2_SizeChanged(object sender, EventArgs e)
        {
            resize_controls();
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            resize_controls();
        }
    }
}
