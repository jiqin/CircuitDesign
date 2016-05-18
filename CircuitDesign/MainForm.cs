using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CircuitModels;
using CircuitTools;
using CircuitDesign;

namespace CircuitDesign
{
    public partial class MainForm : Form
    {
        string component_model_file_path = "Resources\\component_model.mdb";
        string circuit_template_file_path = "Resources\\component_template.xml";

        string init_dir_proj = "results\\Projlist";
        string file_name_filter_proj = "项目文件(*.proj)|*.proj";
        string init_dir_network = "results\\Netlist";
        string file_name_filter_network = "网表文件(*.net)|*.net";
        string init_dir_circuit = "results\\Circuit";
        string file_name_filter_circuit = "电路图文件 (*.xml)|*.xml";
        string file_name_output_text = "文本输出文件 (*.txt)|*.txt";

        CircuitNetworkManager circuit_network_manager;

        public MainForm()
        {
            InitializeComponent();
            circuit_network_manager = new CircuitNetworkManager(component_model_file_path, circuit_template_file_path);
        }

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
            return get_save_file_name(init_dir_proj, file_name_filter_proj);
        }
        
        private void _save_project()
        {
            string save_file_name = get_save_project_file_name();
            if (save_file_name != null)
            {
                circuit_network_manager.set_file(save_file_name);
                circuit_network_manager.save();
            }
        }

        private void init_data_grid_view_circuit_states()
        {
            InputSwitchLoadStatesForm.init_data_grid_view(
                dataGridView_circuit_states,
                true,
                circuit_network_manager.get_switch_load_names(),
                circuit_network_manager.get_switch_load_states());
        }

        private void init_project()
        {
            richTextBox_network.Text = circuit_network_manager.get_network_content();
            init_data_grid_view_circuit_states();
            draw_pannel_cricuit(null);
            treeView_reslt.Nodes.Clear();
        }

        private void draw_pannel_cricuit(AnalyzeResult result)
        {
            Image im_src = circuit_network_manager.get_circuit_image(result);

            if (im_src != null)
            {
                Image im_to = new Bitmap(this.panel_cricuit.Width, this.panel_cricuit.Height);
                Graphics g = Graphics.FromImage(im_to);
                this.panel_cricuit.BackgroundImageLayout = ImageLayout.Stretch;
                this.panel_cricuit.BackgroundImage = im_to;

                GraphicsUnit unit = new GraphicsUnit();
                RectangleF rect_src = im_src.GetBounds(ref unit);
                g.DrawImage(im_src, panel_cricuit.ClientRectangle, rect_src, unit);
            }
        }

        private void ToolStripMenuItem_new_project_Click(object sender, EventArgs e)
        {
            circuit_network_manager.new_project();
            init_project();
            _save_project();
        }

        private void ToolStripMenuItem_save_project_Click(object sender, EventArgs e)
        {
            _save_project();
        }

        private void ToolStripMenuItem_open_project_Click(object sender, EventArgs e)
        {
            string load_file_name = get_load_project_file_name(init_dir_proj, file_name_filter_proj);
            if (load_file_name != null)
            {
                circuit_network_manager.load_from_project(load_file_name);
                init_project();

                circuit_network_manager.set_file(load_file_name);
            }
        }

        private void ToolStripMenuItem_add_new_network_Click(object sender, EventArgs e)
        {
            string load_file_name = get_load_project_file_name(init_dir_network, file_name_filter_network);
            if (load_file_name != null)
            {
                circuit_network_manager.load_from_network(load_file_name);
                init_project();
                _save_project();
            }
        }

        private void ToolStripMenuItem_add_circuit_file_Click(object sender, EventArgs e)
        {
            string load_file_name = get_load_project_file_name(init_dir_circuit, file_name_filter_circuit);
            if (load_file_name != null)
            {
                circuit_network_manager.load_from_circuit(load_file_name);
                init_project();
                _save_project();
            }
        }

        private void ToolStripMenuItem_exit_Click(object sender, EventArgs e)
        {
            circuit_network_manager.save();
            this.Close();
        }

        private void ToolStripMenuItem_draw_circuit_Click(object sender, EventArgs e)
        {
            CircuitManager cm = circuit_network_manager.get_circuit_manager();
            if (cm != null)
            {
                CircuitDesign.CircuitDesignForm form = new CircuitDesign.CircuitDesignForm();
                form.init_circuit_manager(cm);

                circuit_network_manager.save();
                form.ShowDialog();

                draw_pannel_cricuit(null);
                if (MessageBox.Show("是否通过电路图重新生成网表数据?\n这将会覆盖现有网表数据!", 
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    circuit_network_manager.reload_network_by_circuit();
                    init_project();
                }
            }
        }

        private void ToolStripMenuItem_edit_component_Click(object sender, EventArgs e)
        {
            ComponentTemplate ct = circuit_network_manager.get_circuit_component_template();
            if (ct != null)
            {
                CircuitDesign.ListComponentForm dlg = new CircuitDesign.ListComponentForm();
                dlg.InitComponents(ct, true);
                dlg.ShowDialog();
            }
        }

        private void ToolStripMenuItem_input_states_Click(object sender, EventArgs e)
        {
            InputSwitchLoadStatesForm input_dlg = new InputSwitchLoadStatesForm();
            input_dlg.Init(circuit_network_manager.get_switch_load_names(), circuit_network_manager.get_switch_load_states());
            input_dlg.ShowDialog();

            List<SwitchLoadStatesInput> states = new List<SwitchLoadStatesInput>();
            input_dlg.get_input(states);
            circuit_network_manager.set_switch_load_states(states);
            init_data_grid_view_circuit_states();
        }

        private void ToolStripMenuItem_analyze_load_Click(object sender, EventArgs e)
        {
            List<AnalyzeResult> results = circuit_network_manager.analyze();
            treeView_reslt.Nodes.Clear();
            foreach (AnalyzeResult result in results)
            {
                TreeNode tn = new TreeNode(result.get_load_result_message());
                tn.Tag = result;
                treeView_reslt.Nodes.Add(tn);

                if (result.is_expected_load_status)
                {
                    //tn.BackColor = Color.Green;
                }
                else
                {
                    tn.BackColor = Color.Red;
                }
            }
            treeView_reslt.ExpandAll();
        }

        private void ToolStripMenuItem_analyze_power_ground_Click(object sender, EventArgs e)
        {
            List<AnalyzeResult> results = circuit_network_manager.analyze();
            treeView_reslt.Nodes.Clear();
            foreach (AnalyzeResult result in results)
            {
                TreeNode tn = new TreeNode(result.get_result_message_head());
                tn.Tag = result;
                treeView_reslt.Nodes.Add(tn);

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
            treeView_reslt.ExpandAll();
        }
        
        private void ToolStripMenuItem_output_text_Click(object sender, EventArgs e)
        {
            string file_name = get_save_file_name(".", file_name_output_text);
            if (file_name == null)
            {
                return;
            }

            string output_text = circuit_network_manager.output_text_result();
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

            List<AnalyzeResult> results = circuit_network_manager.analyze();
            foreach (AnalyzeResult result in results)
            {
                string file_name = String.Format("{0}\\状态_{1}.png", file_path, result.index);
                Image im_src = circuit_network_manager.get_circuit_image(result);
                im_src.Save(file_name);

                if (im_src != null)
                {
                    im_src.Save(file_name, System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            MessageBox.Show("保存图形报告成功!");
        }

        private void treeView_reslt_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView_reslt_select(e.Node);
        }

        private void treeView_reslt_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView_reslt_select(e.Node);
        }

        private void treeView_reslt_select(TreeNode tn)
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
    }
}
