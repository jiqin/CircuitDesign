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

namespace matrix_reload
{
    public partial class Form1 : Form
    {
        List<string> Nodelist = new List<string>();//存放电路模型的所有“点”
        List<ComponentStruct> Components = new List<ComponentStruct>();//存放电路中所有元件
        List<PowerStruct> Powers = new List<PowerStruct>();//存放电路中的电源
        List<GroundStruct> Gnds = new List<GroundStruct>();//存放电路中的地
        List<SwtichStruct> Swtiches = new List<SwtichStruct>();//存放电路中的单掷刀开关
        List<RLoadStruct> RLoads = new List<RLoadStruct>();//存放电路中的电阻负载
        List<ControlNode> CNodes = new List<ControlNode>();//存放控制结点
        List<BeControlledNode> BCNodes = new List<BeControlledNode>();//存放受控结点
        int matrix_width = 0;

        private int[,] base_matrix;//基础矩阵MB
        
        public Form1()
        {
            InitializeComponent();
        }

        private void InitialSoftware()//初始化软件
        {
            Nodelist.Clear();
            Components.Clear();
            Powers.Clear();
            Gnds.Clear();
            Swtiches.Clear();
            RLoads.Clear();
            CNodes.Clear();
            BCNodes.Clear();

            richTextBox1.Clear();
            richTextBox2.Clear();

            listBox1.Items.Clear();

            textBox1.Text = "";
            textBox2.Text = "";
        }
        
        private string sfind(string s, int num)//寻找相应参数
        {
            string[] s1 = s.Split(' ');//以空格为分界标准，区分相应信息
            int i = 1;

            foreach (string s2 in s1)//查找对应位置的字符串
            {
                if (s2 == "")
                {
                    continue;
                }
                else if (i == num)
                {
                    return s2;
                }
                else
                {
                    i++;
                }
            }

            throw new Exception(string.Format("字符串个数超出范围 : {0} {1}", s, num));
        }

        private void GetThePosition()//取得电源、地、开关、电阻负载在Nodelist中的位置
        {
            for (int i = 0; i < Powers.Count; i++)
            {
                int index = Nodelist.IndexOf(Powers[i].Name);
                Powers[i].Position = index;
            }

            for (int i = 0; i < Gnds.Count; i++)
            {
                int index = Nodelist.IndexOf(Gnds[i].Name);
                Gnds[i].Position = index;
            }

            for (int i = 0; i < Swtiches.Count; i++)
            {
                int index = Nodelist.IndexOf(Swtiches[i].Name);
                Swtiches[i].Position = index;
            }

            for (int i = 0; i < RLoads.Count; i++)
            {
                int index = Nodelist.IndexOf(RLoads[i].Name);
                RLoads[i].Position = index;
            }

            for (int i = 0; i < CNodes.Count; i++)
            {
                int index = Nodelist.IndexOf(CNodes[i].Name);
                CNodes[i].Position = index;
            }

            for (int i = 0; i < BCNodes.Count; i++)
            {
                int index = Nodelist.IndexOf(BCNodes[i].Name);
                BCNodes[i].Position = index;
            }
        }

        private void CreateMatrix(int[,] matrix, List<string> node_list)//建立基础矩阵
        {
            MatrixClear(matrix, matrix.GetLength(0));
            int[] NodeIndex = new int[10];//存放点在矩阵中的索引

            for (int i = 0; i < Components.Count; i++)
            {
                for (int j = 0; j < Components[i].NodeNumber; j++)//读取元件所有点的索引
                {
                    NodeIndex[j] = node_list.IndexOf(Components[i].Node[j]);
                }

                for (int m = 0; m < 9; m++)
                {
                    for (int n = 1; n < 10; n++)
                    {
                        if (Components[i].Connection[m, n] == 1)//单向正向边
                        {
                            matrix[NodeIndex[m], NodeIndex[n]] = 1;
                        }
                        else if (Components[i].Connection[m, n] == 2)//单向反向边
                        {
                            matrix[NodeIndex[n], NodeIndex[m]] = 1;
                        }
                        else if (Components[i].Connection[m, n] == 3)//双向边
                        {
                            matrix[NodeIndex[m], NodeIndex[n]] = 1;
                            matrix[NodeIndex[n], NodeIndex[m]] = 1;
                        }
                    }
                }
            }

            for (int i1 = 0; i1 < Powers.Count; i1++)
            {
                for (int j1 = 0; j1 < node_list.Count; j1++)
                {
                    matrix[j1, Powers[i1].Position] = 0;
                }
            }

            for (int i2 = 0; i2 < Gnds.Count; i2++)
            {
                for (int j2 = 0; j2 < node_list.Count; j2++)
                {
                    matrix[Gnds[i2].Position, j2] = 0;
                }
            }

            for (int k = 0; k < node_list.Count; k++)
            {
                matrix[k, k] = 1;
            }
        }

        private void NodelistRank(List<string> list)//给Nodelist排序
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    int itmp1 = list[i].CompareTo(list[j]);

                    if (itmp1 > 0)
                    {
                        string tmp = list[i];
                        list[i] = list[j];
                        list[j] = tmp;
                    }
                }
            }
        }

        private void ShowMatrix()
        {
            richTextBox2.Text = "";
            for (int i = 0; i < Nodelist.Count; i++)
            {
                for (int j = 0; j < Nodelist.Count; j++)
                {
                    richTextBox2.Text += base_matrix[i, j].ToString() + "  ";//"\t"可以变为"  "
                }

                richTextBox2.Text += "\n";
            }
        }

        private void ReloadNodeListBox()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < Nodelist.Count; i++)
            {
                listBox1.Items.Add(Nodelist[i]);
            }
        }

        private void SwapNode(int index1, int index2)
        {
            if (index1 >= 0 && index1 < Nodelist.Count && index2 >= 0 && index2 < Nodelist.Count)
            {
                string s = Nodelist[index1];
                Nodelist[index1] = Nodelist[index2];
                Nodelist[index2] = s;

                ReloadNodeListBox();
            }
        }

        private void MatrixClear(int[,] matrix, int num)
        {
            for (int index = 0; index < num; index++)
            {
                for (int i = 0; i < Nodelist.Count; i++)
                {
                    matrix[index, i] = 0;
                    matrix[i, index] = 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\Netlist";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "网表文件(*.net)|*.net";//对文件的扩展名进行过滤

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //定义读取网表文件变量
                StreamReader ReadNetlist = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default);

                //定义读取进程序的网表文件信息
                string readline;

                try
                {
                    ReadNetlist.BaseStream.Seek(0, SeekOrigin.Begin);//确定读取文件位置为文件的开头

                    richTextBox1.Text = "";

                    InitialSoftware();

                    while (true)
                    {
                        readline = ReadNetlist.ReadLine();//读网表文件中的一行

                        if (readline == null)//如果读取为空，则跳出
                        {
                            break;
                        }

                        if (readline != "") //判断是否为文件的结尾
                        {
                            string endtext = sfind(readline, 1);//提取读入一行信息的第一个字符串

                            if (endtext == ".subckt")  //以后的内容全部为无效分析内容
                            {
                                break;
                            }
                        }

                        if (readline == "" || readline[0] == '*' || readline[0] == '+')//过滤无关的分析信息
                        {
                            continue;
                        }

                        richTextBox1.Text += readline + "\n";//将读入的网表信息写入文本框中

                        string ch = readline.Substring(0, 3).ToUpper();//定义确定元器件类型的变量

                        if (ch == "R_R")
                        {
                            ComponentStruct ComAdd = new ComponentStruct();
                            Components.Add(ComAdd);

                            ComAdd.Name = sfind(readline, 1);
                            ComAdd.NodeNumber = 3;
                            ComAdd.Node[0] = sfind(readline, 1).Substring(2, 2);//R#
                            ComAdd.Node[1] = sfind(readline, 2);//Node1
                            ComAdd.Node[2] = sfind(readline, 3);//Node2

                            for (int i1 = 0; i1 < 3; i1++)
                            {
                                if (Nodelist.IndexOf(ComAdd.Node[i1]) == -1)
                                {
                                    Nodelist.Add(ComAdd.Node[i1]);
                                }
                            }

                            ComAdd.Connection[0, 1] = 3;//认为R#到Node1之间为双向边
                            ComAdd.Connection[0, 2] = 3;//认为R#到Node2之间为双向边
                            ComAdd.Connection[1, 2] = 0;//认为Node1到Node2之间无边

                            RLoadStruct RAdd = new RLoadStruct();
                            RLoads.Add(RAdd);

                            RAdd.Name = sfind(readline, 1).Substring(2, 2);
                        }
                        else if (ch == "V_V")
                        {
                            ComponentStruct ComAdd = new ComponentStruct();
                            Components.Add(ComAdd);

                            ComAdd.Name = sfind(readline, 1);
                            ComAdd.NodeNumber = 3;
                            ComAdd.Node[0] = sfind(readline, 1).Substring(2, 2);//V#
                            ComAdd.Node[1] = sfind(readline, 2);//Node1
                            ComAdd.Node[2] = sfind(readline, 3);//Node2 = Gnds

                            for (int i1 = 0; i1 < 3; i1++)
                            {
                                if (Nodelist.IndexOf(ComAdd.Node[i1]) == -1)
                                {
                                    Nodelist.Add(ComAdd.Node[i1]);
                                }
                            }

                            ComAdd.Connection[0, 1] = 1;//认为V#到Node1之间为单向正向边
                            ComAdd.Connection[0, 2] = 0;//认为V#到Node2之间无边
                            ComAdd.Connection[1, 2] = 0;//认为Node1到Node2之间无边

                            PowerStruct PAdd = new PowerStruct();//add the power
                            Powers.Add(PAdd);

                            PAdd.Name = sfind(readline, 1).Substring(2, 2);

                            GroundStruct GAdd = new GroundStruct();//add the gnd
                            Gnds.Add(GAdd);

                            GAdd.Name = sfind(readline, 3);
                        }
                        else if (ch == "X_U")
                        {
                            ComponentStruct ComAdd = new ComponentStruct();
                            Components.Add(ComAdd);

                            ComAdd.Name = sfind(readline, 1);
                            ComAdd.NodeNumber = 3;
                            ComAdd.Node[0] = sfind(readline, 1).Substring(2, 2);//U#
                            ComAdd.Node[1] = sfind(readline, 2);//Node1
                            ComAdd.Node[2] = sfind(readline, 3);//Node2

                            for (int i1 = 0; i1 < 3; i1++)
                            {
                                if (Nodelist.IndexOf(ComAdd.Node[i1]) == -1)
                                {
                                    Nodelist.Add(ComAdd.Node[i1]);
                                }
                            }

                            ComAdd.Connection[0, 1] = 3;//认为U#到Node1之间为双向边
                            ComAdd.Connection[0, 2] = 3;//认为U#到Node2之间为双向边
                            ComAdd.Connection[1, 2] = 0;//认为Node1到Node2之间无边

                            SwtichStruct SwAdd = new SwtichStruct();
                            Swtiches.Add(SwAdd);

                            SwAdd.Name = sfind(readline, 1).Substring(2, 2);
                        }
                        else if (ch == "D_D")
                        {
                            ComponentStruct ComAdd = new ComponentStruct();
                            Components.Add(ComAdd);

                            ComAdd.Name = sfind(readline, 1);
                            ComAdd.NodeNumber = 3;
                            ComAdd.Node[0] = sfind(readline, 1).Substring(2, 2);//D#
                            ComAdd.Node[1] = sfind(readline, 2);//Node1
                            ComAdd.Node[2] = sfind(readline, 3);//Node2

                            for (int i1 = 0; i1 < 3; i1++)
                            {
                                if (Nodelist.IndexOf(ComAdd.Node[i1]) == -1)
                                {
                                    Nodelist.Add(ComAdd.Node[i1]);
                                }
                            }

                            ComAdd.Connection[0, 1] = 2;//认为D#到Node1之间为单向反向边
                            ComAdd.Connection[0, 2] = 1;//认为D#到Node2之间为单向正向边
                            ComAdd.Connection[1, 2] = 0;//认为Node1到Node2之间无边
                        }
                        else if (ch == "X_W")
                        {
                            ComponentStruct ComAdd = new ComponentStruct();
                            Components.Add(ComAdd);

                            ComAdd.Name = sfind(readline, 1);
                            ComAdd.NodeNumber = 6;
                            ComAdd.Node[0] = sfind(readline, 1).Substring(2, 2) + "C";//控制点W#C
                            ComAdd.Node[1] = sfind(readline, 1).Substring(2, 2) + "SW";//受控点W#SW
                            ComAdd.Node[2] = sfind(readline, 2);//Node1
                            ComAdd.Node[3] = sfind(readline, 3);//Node2
                            ComAdd.Node[4] = sfind(readline, 4);//Node3
                            ComAdd.Node[5] = sfind(readline, 5);//Node4

                            for (int i1 = 0; i1 < 6; i1++)
                            {
                                if (Nodelist.IndexOf(ComAdd.Node[i1]) == -1)
                                {
                                    Nodelist.Add(ComAdd.Node[i1]);
                                }
                            }

                            ComAdd.Connection[0, 1] = 0;//认为W#C到W#SW之间无边
                            ComAdd.Connection[0, 2] = 3;//认为W#C到Node1之间为双向边
                            ComAdd.Connection[0, 3] = 3;//认为W#C到Node2之间为双向边
                            ComAdd.Connection[0, 4] = 0;//认为W#C到Node3之间无边
                            ComAdd.Connection[0, 5] = 0;//认为W#C到Node4之间无边
                            ComAdd.Connection[1, 2] = 0;//认为W#SW到Node1之间无边
                            ComAdd.Connection[1, 3] = 0;//认为W#SW到Node2之间无边
                            ComAdd.Connection[1, 4] = 3;//认为W#SW到Node3之间为双向边
                            ComAdd.Connection[1, 5] = 3;//认为W#SW到Node4之间为双向边
                            ComAdd.Connection[2, 3] = 0;//认为Node1到Node2之间无边
                            ComAdd.Connection[2, 4] = 0;//认为Node1到Node3之间无边
                            ComAdd.Connection[2, 5] = 0;//认为Node1到Node4之间无边
                            ComAdd.Connection[3, 4] = 0;//认为Node2到Node3之间无边
                            ComAdd.Connection[3, 5] = 0;//认为Node2到Node4之间无边
                            ComAdd.Connection[4, 5] = 0;//认为Node3到Node4之间无边

                            ControlNode ConAdd = new ControlNode();
                            CNodes.Add(ConAdd);

                            ConAdd.Name = sfind(readline, 1).Substring(2, 2) + "C";

                            BeControlledNode BConAdd = new BeControlledNode();
                            BCNodes.Add(BConAdd);

                            BConAdd.Name = sfind(readline, 1).Substring(2, 2) + "SW";
                        }
                    }

                    //NodelistRank(Nodelist);//对Nodelist进行排序的程序

                    GetThePosition();

                    textBox1.Text = Nodelist.Count.ToString();

                    ReloadNodeListBox();
                }
                catch
                {
                    MessageBox.Show("read netlist error!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";

            base_matrix = new int[Nodelist.Count, Nodelist.Count];
            CreateMatrix(base_matrix, Nodelist);

            ShowMatrix();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selected_index = listBox1.SelectedIndex;

            SwapNode(selected_index, selected_index - 1);

            if (selected_index > 0)
            {
                listBox1.SelectedIndex = selected_index - 1;
            }

            GetThePosition();

            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int selected_index = listBox1.SelectedIndex;

            SwapNode(selected_index, selected_index + 1);

            if (selected_index < Nodelist.Count - 1)
            {
                listBox1.SelectedIndex = selected_index + 1;
            }

            GetThePosition();

            textBox2.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            matrix_width = 0;

            for (int i = 0; i < Nodelist.Count; ++i)
            {
                int index = 0;
                for (index = 0; index < i; ++index)
                {
                    if (base_matrix[i, index] == 1 || base_matrix[index, i] == 1)
                    {
                        break;
                    }
                }

                int rank = i - index;

                matrix_width = Math.Max(matrix_width, rank);
            }

            textBox2.Text = matrix_width.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int Number = Convert.ToInt16(textBox3.Text);

            MatrixClear(base_matrix, Number);

            richTextBox2.Text = "";
            
            ShowMatrix();
        }

        private void NodeListSort(int[,] matrix, List<string> node_list, int n)
        {
            int row_begin = n;
            for (int col = n; col < node_list.Count; ++col)
            {
                for (int row = row_begin; row < node_list.Count; ++row)
                {
                    if (matrix[row, col] == 1)
                    {
                        ShiftUpMatrixRow(matrix, node_list, row, row_begin);
                        row_begin++;
                    }
                }
            }
        }

        private void button_sort_Click(object sender, EventArgs e)
        {
            if (base_matrix == null)
            {
                return;
            }

            int number = int.Parse(textBox3.Text);
            NodeListSort(base_matrix, Nodelist, number);
            MatrixClear(base_matrix, number);
            ReloadNodeListBox();
            ShowMatrix();
        }

        private void ShiftUpMatrixRow(int[,] matrix, List<string> node_list, int row_from, int row_to)
        {
            Debug.Assert(row_from >= 0 && row_from < node_list.Count &&
                row_to >= 0 && row_to < node_list.Count &&
                row_from >= row_to);

            if (row_from == row_to)
            {
                return;
            }

            string node_name = node_list[row_from];
            for (int row = row_from - 1; row >= row_to; --row)
            {
                node_list[row + 1] = node_list[row];
            }
            node_list[row_to] = node_name;

            CreateMatrix(matrix, node_list);
        }

        public void MatrixCopy(int[,] matrix1, int[,] matrix2)//矩阵复制
        {
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrix2[i, j] = matrix1[i, j];
                }
            }
        }

        private void button_partition_Click(object sender, EventArgs e)
        {
            int number = int.Parse(textBox3.Text);
            List<string> results = new List<string>();
            for (int i = number; i < Nodelist.Count; ++i)
            {
                int[,] tmp_matrix = new int[base_matrix.GetLength(0), base_matrix.GetLength(1)];
                MatrixCopy(base_matrix, tmp_matrix);
                List<string> tmp_node_list = Nodelist.GetRange(0, Nodelist.Count);

                ShiftUpMatrixRow(tmp_matrix, tmp_node_list, i, number);
                NodeListSort(tmp_matrix, tmp_node_list, number + 1);
                MatrixClear(tmp_matrix, number + 1);
                List<List<string>> partitions = new List<List<string>>();
                if (HasPartition(tmp_matrix, tmp_node_list, number + 1, partitions))
                {
                    string s = string.Format("{0} :  {1}  : ", tmp_node_list[number], partitions.Count);
                    for (int j = 0; j < partitions.Count; ++j)
                    {
                        for (int k = 0; k < partitions[j].Count; ++k)
                        {
                            s += partitions[j][k];
                            if (k != partitions[j].Count - 1)
                            {
                                s += ", ";
                            }
                        }
                        if (j != partitions.Count - 1)
                        {
                            s += "     |     ";
                        }
                    }
                    results.Add(s);
                }
            }
            textBox_partition_result.Text = string.Format("result num : {0}\r\n", results.Count) ;
            for (int i = 0; i < results.Count; ++i)
            {
                textBox_partition_result.Text += results[i] + "\r\n";
            }
        }

        private bool HasPartition(int[,] matrix, List<string> node_names, int start_index, List<List<string>> partitions)
        {
            partitions.Clear();
            int last_index = matrix.GetLength(0);
            for (int i = matrix.GetLength(0) - 1; i >= start_index; i--)
            {
                if (IsMatrixPartAllZero(matrix, 0, i, i, last_index) && IsMatrixPartAllZero(matrix, i, 0, last_index, i))
                {
                    partitions.Add(node_names.GetRange(i, last_index - i));
                    last_index = i;
                }
            }
            partitions.Reverse();

            return partitions.Count > 1;
        }

        private bool IsMatrixPartAllZero(int[,] matrix, int l, int t, int r, int b)
        {
            for (int i = l; i < r; ++i)
            {
                for (int j = t; j < b; ++j)
                {
                    if (matrix[j, i] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}