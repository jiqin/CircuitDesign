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

namespace SCA_PowerGround
{
    public partial class FormPGMain : Form
    {
        NetworkModel network_model = new NetworkModel();

        private void InitialSoftware()//初始化软件
        {
            network_model.Initialize();

            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            richTextBox5.Clear();

            listBox1.Items.Clear();
        }
        
        public FormPGMain()
        {
            InitializeComponent();

            network_model.LoadTemplates(Application.StartupPath + "\\component_model.mdb");
        }

        private void button1_Click(object sender, EventArgs e)//Open the Netlist
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

        private void button2_Click(object sender, EventArgs e)//Create M0, SwtichState
        {
            //network_model.CreateMatrix();

            //network_model.CreateSwtichCombine(network_model.switch_components.Count, network_model.SwtichState);

            //// network_model.SwtichStateNumber = network_model.SwtichState.Count;

            //for (int i = 0; i < network_model.node_names.Count; i++)
            //{
            //    for (int j = 0; j < network_model.node_names.Count; j++)
            //    {
            //        richTextBox2.Text += network_model.base_matrix[i, j].ToString() + "  ";//"\t"可以变为"  "
            //    }

            //    richTextBox2.Text += "\n";
            //}

            //for (int i = 0; i < network_model.SwtichState.Count; i++)
            //{
            //    int[] tmpshow = new int[network_model.switch_components.Count];

            //    tmpshow = network_model.SwtichState[i];

            //    for (int j = 0; j < network_model.switch_components.Count; j++)
            //    {
            //        richTextBox4.Text += tmpshow[j].ToString() + "  ";             
            //    }

            //    richTextBox4.Text += "\n";
            //}
        }

        private void button3_Click(object sender, EventArgs e)//Analysis
        {
        //    network_model.vector_final = new int[network_model.node_names.Count];

        //    richTextBox3.Text = "";

        //    for (int i = 0; i < network_model.Startlist.Count; i++)
        //    {
        //        richTextBox5.Text += network_model.Startlist[i].ToString() + "  ";
        //    }
        //    richTextBox5.Text += "\n";

        //    int tmpstate = 0;
        //    for (tmpstate = 0; tmpstate < network_model.SwtichState.Count; tmpstate++)
        //    {
        //        network_model.CreateTransMatrix(tmpstate);

        //        richTextBox3.Text += "Switch State " + (tmpstate + 1).ToString() + "\n";

        //        for (int tmpstart = 0; tmpstart < network_model.Startlist.Count; tmpstart++)
        //        {
        //            network_model.CreateVectorInitial(network_model.Startlist[tmpstart]);

        //            network_model.networkflow(network_model.trans_matrix, network_model.vector_initial, network_model.vector_final);//网络流仿真

        //            //SolveStarProblem(trans_matrix, vector_final);//解决星型连接问题

        //            for (int j = 0; j < network_model.vector_final.Length; j++)//Show current Vn
        //            {
        //                richTextBox3.Text += network_model.vector_final[j].ToString() + "  ";
        //            }
        //            richTextBox3.Text += "\n";

        //            int[] CurrentResult = new int[network_model.switch_components.Count + network_model.Startlist.Count];

        //            for (int k1 = 0; k1 < network_model.switch_components.Count; k1++)
        //            {
        //                CurrentResult[k1] = network_model.SwtichState[tmpstate][k1];
        //            }

        //            for (int k2 = 0; k2 < network_model.Startlist.Count; k2++)
        //            {
        //                int index = 0;

        //                index = network_model.node_names.IndexOf(network_model.Startlist[k2]);

        //                CurrentResult[k2 + network_model.switch_components.Count] = network_model.vector_final[index];
        //            }

        //            network_model.analysis_result.Add(CurrentResult);
        //        }
        //    }

        //    MessageBox.Show("Analysis is over!");

        //    for (int i = 0; i < network_model.analysis_result.Count; i++)
        //    {
        //        int[] tmpshow = new int[network_model.switch_components.Count + network_model.Startlist.Count];

        //        tmpshow = network_model.analysis_result[i];

        //        for (int j = 0; j < tmpshow.Length; j++)
        //        {
        //            richTextBox5.Text += tmpshow[j].ToString() + "  ";
        //        }

        //        richTextBox5.Text += "\n";
        //    }
        }
    }
}