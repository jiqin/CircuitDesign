using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using CircuitModels;
using CircuitTools;

namespace SCA_Load
{
    public partial class FormLoadMain : Form
    {
        NetworkModel network_model = new NetworkModel();
        List<int[]> input_results = new List<int[]>();

        private void InitialSoftware()//初始化软件
        {
            network_model.Initialize();

            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            richTextBox5.Clear();
            richTextBox6.Clear();

            listBox1.Items.Clear();

            textBox1.Text = "";
            textBox2.Text = "";
        }

        public FormLoadMain()
        {
            InitializeComponent();

            network_model.LoadTemplates(Application.StartupPath + "\\component_model.mdb");
        }

        private void button1_Click(object sender, EventArgs e)//打开网表文件
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\Netlist";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "网表文件(*.net)|*.net";//对文件的扩展名进行过滤

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                InitialSoftware();

                network_model.load_network_from_string(File.ReadAllText(openFileDialog1.FileName));

                richTextBox1.Text = network_model.get_network_content();

                foreach (string node_name in network_model.node_names)
                {
                    listBox1.Items.Add(node_name);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//生成MB，V0，开关状态组合
        {
            // network_model.CreateMatrix();
            // network_model.CreateVectorInitialAll();
            // richTextBox2.Text += CircuitTools.Utils.Join(network_model.base_matrix, " ", "\n");
            // textBox1.Text += FormInput.Join(network_model.vector_initial, " ");

            FormInput input_dlg = new FormInput();
            // input_dlg.Init(switch_names.ToArray(), node_names.ToArray());
            // input_dlg.ShowDialog();
            // input_dlg.GetInput(network_model.SwtichState, input_results);
            //richTextBox4.Text = "";
            //for (int i = 0; i < network_model.SwtichState.Count; ++i)
            //{
            //    richTextBox4.Text += FormInput.Join(network_model.SwtichState[i], " ") + " "
            //        + FormInput.Join(input_results[i], " ") + "\n";
            //}

            richTextBox6.Text = string.Join(" ", network_model.node_names.ToArray()) + "\n";
        }

        private void button3_Click(object sender, EventArgs e)//进行潜通路分析
        {
            textBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox5.Text = "";
            richTextBox6.Text = "";
            FormErrorInfo error_dlg = new FormErrorInfo();

            for (int i = 0; i < network_model.switch_components.Count; i++)
            {
                textBox2.Text += network_model.switch_components[i].Name + " ";
            }

            for (int j = 0; j < network_model.r_load_components.Count; j++)
            {
                textBox2.Text += network_model.r_load_components[j].Name + " ";
            }

            //for (int tmpstate = 0; tmpstate < network_model.SwtichState.Count; tmpstate++)
            //{
            //    int[,] trans_matrix = network_model.CreateTransMatrix(tmpstate);

            //    richTextBox3.Text += "Trans Matrix " + (tmpstate + 1).ToString() + "\n";
            //    richTextBox3.Text += CircuitTools.Utils.Join(trans_matrix, " ", "\n");

            //    int[] vector_final = new int[network_model.node_names.Count];
            //    network_model.networkflow(trans_matrix, network_model.GetMatirxBCNode(), network_model.vector_initial, vector_final);//网络流仿真
            //    MatrixTools.MatrixTool.SolveStarProblem(network_model.node_names, trans_matrix, vector_final);//解决星型连接问题

            //    // richTextBox6.Text += FormInput.Join(vector_final, " ");

            //    int[] switch_result = new int[network_model.switch_components.Count];
            //    int[] load_result = new int[network_model.r_load_components.Count];

            //    for (int i = 0; i < network_model.switch_components.Count; i++)
            //    {
            //        switch_result[i] = network_model.SwtichState[tmpstate][i];
            //    }
            //    for (int j = 0; j < network_model.r_load_components.Count; j++)
            //    {
            //        load_result[j] = vector_final[network_model.r_load_components[j].PositionInNodeList];
            //    }
            //    // richTextBox5.Text += FormInput.Join(switch_result, " ") + " " + FormInput.Join(load_result, " ") + "\n";

            //    if (input_results[tmpstate].GetLength(0) == load_result.GetLength(0) &&
            //        !MatrixTools.MatrixTool.vector_equal(input_results[tmpstate], load_result))
            //    {
            //        error_dlg.AddErrorMessage("");
            //        //error_dlg.AddErrorMessage("开关状态 : " + FormInput.Join(switch_result, " "));
            //        //error_dlg.AddErrorMessage("运行结果 : " + FormInput.Join(load_result, " ")
            //        //    + " != " + FormInput.Join(input_results[tmpstate], " "));
            //        List<List<int>> all_paths = new List<List<int>>();
            //        network_model.FindPath(network_model.node_names, trans_matrix, all_paths);
            //        for (int i = 0; i < all_paths.Count; i++)
            //        {
            //            List<int> path = all_paths[i];
            //            string s = i.ToString() + " : ";
            //            for (int j = 0; j < path.Count; ++j)
            //            {
            //                if (j != 0)
            //                {
            //                    s += " -> ";
            //                }
            //                s += network_model.node_names[path[j]];
            //            }
            //            error_dlg.AddErrorMessage(s);
            //        }
            //    }
            //}
            //if (error_dlg.has_error)
            //{
            //    error_dlg.ShowDialog();
            //}
            //else
            //{
            //    System.Windows.Forms.MessageBox.Show("运行结果符合预期");
            //}
        }
    }
}