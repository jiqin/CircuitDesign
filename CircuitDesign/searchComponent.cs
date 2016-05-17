using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CircuitDesign
{
    class searchComponent
    {
        List<string> Node_StringList = new List<string>();
        List<ComponentStruct> Node = new List<ComponentStruct>();
        List<SwtichStruct> Switch = new List<SwtichStruct>();
        List<CapacitorStruct> Capasitor = new List<CapacitorStruct>();
        List<PowerStruct> Power = new List<PowerStruct>();
        List<GroundStruct> Egnd = new List<GroundStruct>();
        int[,] Adj_matrix;

        string sfind(string s, int num)
        {
            string[] s1 = s.Split(' ');
            int i = 0;
            foreach (string s2 in s1) 
            {
                if (s2 == "")
                {
                    continue;
                }
                else if (i == num) {
                    return s2;
                }
                else {
                    i++;
                }
            }

            throw new Exception(string.Format("字符串个数超出范围 : {0} {1}", s, num));
        }

        void SearchComponent(string PSpice_net_name)
        {
            //读取网表文件
            System.IO.StreamReader sr = new StreamReader(PSpice_net_name);
            Node_StringList.Clear();

            while(true)
            {
             //每次读一行
                string s = sr.ReadLine ();
                if (s == null)
                {
                    break;
                }
                if(s == "" || s[0] == 'U' || s[0]=='.' || s[0]=='*' || s[0] == '+')  //若是空串或第一个字符为* 号，则再读一行
                {
                    continue;
                }
                string ch = s.Substring(0, 2).ToUpper();
                //砷化镓（结型）场效应管、三极管或MOS管
                if(ch == "B_" || ch == "J_" || ch == "Q_" || ch== "M_")
                {
                    ComponentStruct node = new ComponentStruct();
                    Node.Add(node);

                    node.Name = sfind(s, 1);
                    node.Number=3;    //三个端口
                    node.Array[0]=sfind(s,3);  //栅极
                    node.Array[1]=sfind(s,2);  //漏极
                    node.Array[2]=sfind(s,4);  //源极
                    //将不重复结点加入到结点链表中
                    for(int i2=0; i2<3; i2++)
                    {
                        if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                        {
                           Node_StringList.Add(node.Array[i2]);
                        }
                    }
                    node.Connect[0,1]=0;    //栅极和漏极认为不通
                    node.Connect[0,2]=0;    //栅极和源极认为不通
                    node.Connect[1,2]=2; //漏极和源极一般为双向导通
                  }
                  else if(ch == "C_") //电容
                  {
                      ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                    node.Name = sfind(s,1);
                    node.Number=2;
                    node.Array[0]=sfind(s,1);
                    node.Array[1]=sfind(s,2);

                    //将信息放入电容结构中
                      CapacitorStruct cs = new CapacitorStruct();
                      Capasitor.Add (cs);

                    cs.Name = node.Name;
                    cs.Array[0] = node.Array[0];
                    cs.Array[1] = node.Array[1];

                    //将不重复结点加入到结点链表中
                    for(int i2=0; i2<2; i2++)
                    {
                        if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                        {
                         Node_StringList.Add(node.Array[i2]);
                        }
                    }
                    //
                    node.Connect[0,1]=0; //默认电容两端不通
                  }
              else if(ch == "D_") //二极管
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                node.Name = sfind(s,1);
                node.Number=2;
                node.Array[0]=sfind(s,2);   //正结点
                node.Array[1]=sfind(s,3);   //负结点
                //将不重复结点加入到结点链表中
                for(int i2=0; i2<2; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                node.Connect[0,1]=1;   //0.1:通
              }
              else if(ch == "E_" || ch == "G_")   //电压控制电压（流）源
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);
                node.Name = sfind(s,1);
                node.Number=4;
                node.Array[0]=sfind(s,4); //正控结点
                node.Array[1]=sfind(s,5); //负控结点
                node.Array[2]=sfind(s,2); //输出正结点
                node.Array[3]=sfind(s,3); //输出负结点

                //加入电源列表中
                  PowerStruct ps = new PowerStruct();
                  Power.Add(ps);
                ps.Name = node.Name;
                ps.OutputName = node.Array[2];

                //将不重复结点加入到结点链表中
                for(int i2=0; i2<4; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                //连接关系
                node.Connect[0,1]=1; //认为正控结点到负控结点通
                node.Connect[0,2]=0; //认为正控结点到输出正结点不通
                node.Connect[0,3]=0;//认为正控结点到输出负结点不通
                node.Connect[1,2]=0;//认为负控结点到输出正结点不通
                node.Connect[1,3]=0;//认为负控结点到输出负结点不通
                node.Connect[2,3]=0;//认为输出正结点到输出负结点不通
              }

              else if(ch == "F_" || ch == "H_")  //电流控制电流（压）源;分两行读取信息
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);
                node.Name = sfind(s,1);
                node.Number=4;
                node.Array[2]=sfind(s,2); //正结点
                node.Array[3]=sfind(s,3); //负结点
                //
                //读取下一行信息
                  s = sr.ReadLine();
                node.Array[0]=sfind(s,2);  //正控结点
                node.Array[1]=sfind(s,3);  //负控结点
                //
                //加入电源列表中
                  PowerStruct ps = new PowerStruct();
                  Power.Add(ps);
                ps.Name = node.Name;
                ps.OutputName = node.Array[2];
                //将不重复结点加入到结点链表中
                for(int i2=0; i2<4; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                //连接关系
                node.Connect[0,1]=1; //认为正控结点到负控结点通
                node.Connect[0,2]=0; //认为正控结点到输出正结点不通
                node.Connect[0,3]=0;//认为正控结点到输出负结点不通
                node.Connect[1,2]=0;//认为负控结点到输出正结点不通
                node.Connect[1,3]=0;//认为负控结点到输出负结点不通
                node.Connect[2,3]=0;//认为输出正结点到输出负结点不通
                //
              }
              else if(ch == "I_" || ch == "V_") //独立电流（压）源
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);
                node.Name = sfind(s,1);
                node.Number=2;
                node.Array[0]=sfind(s,2); //正结点
                node.Array[1]=sfind(s,3);
                //
                 //加入电源列表中
                  PowerStruct ps = new PowerStruct();
                  Power.Add(ps);
                ps.Name = node.Name;
                ps.OutputName = node.Array[0];
                //

               //将不重复结点加入到结点链表中
                for(int i2=0; i2<2; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                //连接关系
                node.Connect[0,1]=0; //认为正结点到负结点不通
              }

              else if(ch == "L_" ||ch =="R_")  //电感或电阻
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                node.Name = sfind(s,1);
                node.Number=2;
                node.Array[0]=sfind(s,2);
                node.Array[1]=sfind(s,3);
                //将不重复结点加入到结点链表中
                for(int i2=0; i2<2; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                //连接关系
                node.Connect[0,1]=2;
              }
              else if(ch == "S_") //电压控制开关
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                node.Name = sfind(s,1);
                node.Number=4;
                node.Array[0]=sfind(s,4); //正控结点
                node.Array[1]=sfind(s,5); //负控结点
                node.Array[2]=sfind(s,2); //正结点
                node.Array[3]=sfind(s,3); //负结点
                //将信息放入开关结构中
                  SwtichStruct ss = new SwtichStruct();
                  Switch.Add(ss);
                ss.Name = node.Name;
                ss.Num = 2;
                ss.Array[0] = node.Array[2];
                ss.Array[1] = node.Array[3];
                //
                //将不重复结点加入到结点链表中
                for(int i2=0; i2<4; i2++)
                {
                  if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                  {
                    Node_StringList.Add(node.Array[i2]);
                  }
                }
                //连接关系
                node.Connect[0,1]=1; //认为正控结点到负控结点通
                node.Connect[0,2]=0; //认为正控结点到输出正结点不通
                node.Connect[0,3]=0;//认为正控结点到输出负结点不通
                node.Connect[1,2]=0;//认为负控结点到输出正结点不通
                node.Connect[1,3]=0;//认为负控结点到输出负结点不通
                node.Connect[2,3]=0;//认为输出正结点到输出负结点不通
                //
                }

              else if(ch == "SW") //自定义单刀双掷开关
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                node.Name = sfind(s,1);
                node.Number=3;
                node.Array[0]=sfind(s,2); //开关固定端口
                node.Array[1]=sfind(s,3); //开关常闭端口
                node.Array[2]=sfind(s,4); //开关常开端口
                //将信息放入开关结构中
                  SwtichStruct ss = new SwtichStruct();
                  Switch.Add(ss);
                ss.Name = node.Name;
                ss.Num = 3;
                for(int i2=0; i2<3; i2++)
                   ss.Array[i2] = node.Array[i2];

                //将不重复结点加入到结点链表中
                for(int i2=0; i2<3; i2++)
                {
                  if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                  {
                    Node_StringList.Add(node.Array[i2]);
                  }
                }
                //连接关系
                node.Connect[0,1]=0; //开始时开关一定要设为断
                node.Connect[0,2]=0; //不通
                node.Connect[1,2]=0;//不通
                //
                }
                else if(ch == "X_")  //放大器或开关；若是开关，则需分两行读取信息
                {
                    ComponentStruct node = new ComponentStruct();
                      Node.Add(node);
                  
                  node.Name = sfind(s,1);

                    string x_s = sfind(s, 4).ToLower();
                  if(x_s=="sw_tclose"||x_s=="sw_topen")
                  {
                    //若是开关
                    node.Number=2;
                    node.Array[0]=sfind(s,2);
                    node.Array[1]=sfind(s,3);
                    //
                    //将信息放入开关结构中
                      SwtichStruct ss = new SwtichStruct();
                      Switch.Add(ss);

                    ss.Name = node.Name;
                    ss.Num = 2;
                    ss.Array[0] = node.Array[0];
                    ss.Array[1] = node.Array[1];
                    //
                    //将不重复结点加入到结点链表中
                    for(int i2=0; i2<2; i2++)
                    {
                     if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                     {
                      Node_StringList.Add(node.Array[i2]);
                     }
                    }
                    //连接关系
                    node.Connect[0,1]=0;  //初始认为断
                  }
                  // 若是放大器uA741
                  else{
                   node.Number=3;
                   node.Array[0]=sfind(s,2);//输入高端
                   node.Array[1]=sfind(s,3);//输入低端
                   node.Array[2]=sfind(s,6);//输出端
                   //
                   //将不重复结点加入到结点链表中
                   for(int i2=0; i2<3; i2++)
                  {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                  }
                   //
                   node.Connect[0,1]=0;
                   node.Connect[0,2]=1;  //认为正输入端与输出相通
                   node.Connect[1,2]=0;
                  }
                }
                else if(ch == "W_")  //电流控制开关;分两行读取信息
                {
                    ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                  node.Name = sfind(s,1);
                  node.Number=4;
                  node.Array[2]=sfind(s,2); //正结点
                  node.Array[3]=sfind(s,3); //负结点
                  //
                  //
                    s = sr.ReadLine();
                  node.Array[0]=sfind(s,2);  //正控结点
                  node.Array[1]=sfind(s,3);  //负控结点
                  //
                    //将信息放入开关结构中
                    SwtichStruct ss = new SwtichStruct();
                      Switch.Add(ss);

                  ss.Name = node.Name;
                  ss.Num = 2;
                  ss.Array[0] = node.Array[2];
                  ss.Array[1] = node.Array[3];
                  //
                  //将不重复结点加入到结点链表中
                  for(int i2=0; i2<4; i2++)
                  {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                  }
                  //连接关系
                  node.Connect[0,1]=1; //认为正控结点到负控结点通
                  node.Connect[0,2]=0; //认为正控结点到输出正结点不通
                  node.Connect[0,3]=0;//认为正控结点到输出负结点不通
                  node.Connect[1,2]=0;//认为负控结点到输出正结点不通
                  node.Connect[1,3]=0;//认为负控结点到输出负结点不通
                  node.Connect[2,3]=0;//认为输出正结点到输出负结点不通
                  //
                }
                else
                {
                    throw new Exception( string.Format("错误元素 : {0}", ch));
                }
              }
              sr.Close();
             //确定地的标签
            /*
             if(Egnd_State == false)
             {
              EgendDlg.Enumber =1;
              EgendDlg.Lnum.Caption = 1;
             }
             if(EgendDlg.ShowModal()==mrOk)
             {
              Egnd_State = true;
             }
             */
             //标定电源、地、开关、电容结点在矩阵中的位置
             GetThePosition();
        }

        void GetThePosition()
        {
          //标定电源位置
            for (int i = 0; i < Power.Count; i++)
            {
                int index = Node_StringList.IndexOf(Power[i].OutputName);
                System.Diagnostics.Debug.Assert(index != -1);
                Power[i].Position = index;
            }
          //标定地位置。注意地结点和名称相同
            for (int i = 0; i < Egnd.Count; i++)
            {
                int index = Node_StringList.IndexOf(Egnd[i].Name);
                System.Diagnostics.Debug.Assert(index != -1);
                Egnd[i].Position = index;
            }
          //标定开关位置
            for (int i = 0; i < Switch.Count; i++)
            {
                for (int j = 0; j < Switch[i].Num; j++)
                {
                    int index = Node_StringList.IndexOf(Switch[i].Array[j]);
                    System.Diagnostics.Debug.Assert(index != -1);
                    Switch[i].Position[j] = index;
                }
            }
          //标定电容位置
            for (int i = 0; i < Capasitor.Count; i++)
          {
              int index = Node_StringList.IndexOf(Switch[i].Array[0]);
              System.Diagnostics.Debug.Assert(index != -1);
            Capasitor[i].Position[0] = index;
            index = Node_StringList.IndexOf(Switch[i].Array[1]);
            System.Diagnostics.Debug.Assert(index != -1);
            Capasitor[i].Position[1] = index;
          }
        }

        void CreateMatrix()
        {
            int[,] Adj_matrix = new int[Node.Count, Node.Count];
            //i:外层循环变量，从0到Node_number-1。为元件记数
            int []Port_num = new int[6];  //存放元件端口在结点字符串列表中的位置
            for(int i=0; i<Node.Count; i++) //检查所有元件的导通关系
            {
                for(int j=0; j<Node[i].Number; j++) //该元件所有结点
                {
                   //将结点在矩阵中的位置放入数组中
                   Port_num[j] = Node_StringList.IndexOf(Node[i].Array[j]);
                }
                for (int m = 0; m < Node[i].Number - 1; m++) //端口0到n-2
                {
                    for (int n = 1; n < Node[i].Number; n++)//端口1到n-1
                    {
                        if (Node[i].Connect[m, n] == 1)  //单向导通
                        {
                            Adj_matrix[Port_num[m], Port_num[n]] = 1;
                        }
                        else if (Node[i].Connect[m, n] == 2) //双向导通
                        {
                            Adj_matrix[Port_num[m], Port_num[n]] = 1;
                            Adj_matrix[Port_num[n], Port_num[m]] = 1;
                        }
                    }
                }
            }

        }
    }

    
}
