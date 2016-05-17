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

            throw new Exception(string.Format("�ַ�������������Χ : {0} {1}", s, num));
        }

        void SearchComponent(string PSpice_net_name)
        {
            //��ȡ�����ļ�
            System.IO.StreamReader sr = new StreamReader(PSpice_net_name);
            Node_StringList.Clear();

            while(true)
            {
             //ÿ�ζ�һ��
                string s = sr.ReadLine ();
                if (s == null)
                {
                    break;
                }
                if(s == "" || s[0] == 'U' || s[0]=='.' || s[0]=='*' || s[0] == '+')  //���ǿմ����һ���ַ�Ϊ* �ţ����ٶ�һ��
                {
                    continue;
                }
                string ch = s.Substring(0, 2).ToUpper();
                //�黯�أ����ͣ���ЧӦ�ܡ������ܻ�MOS��
                if(ch == "B_" || ch == "J_" || ch == "Q_" || ch== "M_")
                {
                    ComponentStruct node = new ComponentStruct();
                    Node.Add(node);

                    node.Name = sfind(s, 1);
                    node.Number=3;    //�����˿�
                    node.Array[0]=sfind(s,3);  //դ��
                    node.Array[1]=sfind(s,2);  //©��
                    node.Array[2]=sfind(s,4);  //Դ��
                    //�����ظ������뵽���������
                    for(int i2=0; i2<3; i2++)
                    {
                        if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                        {
                           Node_StringList.Add(node.Array[i2]);
                        }
                    }
                    node.Connect[0,1]=0;    //դ����©����Ϊ��ͨ
                    node.Connect[0,2]=0;    //դ����Դ����Ϊ��ͨ
                    node.Connect[1,2]=2; //©����Դ��һ��Ϊ˫��ͨ
                  }
                  else if(ch == "C_") //����
                  {
                      ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                    node.Name = sfind(s,1);
                    node.Number=2;
                    node.Array[0]=sfind(s,1);
                    node.Array[1]=sfind(s,2);

                    //����Ϣ������ݽṹ��
                      CapacitorStruct cs = new CapacitorStruct();
                      Capasitor.Add (cs);

                    cs.Name = node.Name;
                    cs.Array[0] = node.Array[0];
                    cs.Array[1] = node.Array[1];

                    //�����ظ������뵽���������
                    for(int i2=0; i2<2; i2++)
                    {
                        if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                        {
                         Node_StringList.Add(node.Array[i2]);
                        }
                    }
                    //
                    node.Connect[0,1]=0; //Ĭ�ϵ������˲�ͨ
                  }
              else if(ch == "D_") //������
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                node.Name = sfind(s,1);
                node.Number=2;
                node.Array[0]=sfind(s,2);   //�����
                node.Array[1]=sfind(s,3);   //�����
                //�����ظ������뵽���������
                for(int i2=0; i2<2; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                node.Connect[0,1]=1;   //0.1:ͨ
              }
              else if(ch == "E_" || ch == "G_")   //��ѹ���Ƶ�ѹ������Դ
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);
                node.Name = sfind(s,1);
                node.Number=4;
                node.Array[0]=sfind(s,4); //���ؽ��
                node.Array[1]=sfind(s,5); //���ؽ��
                node.Array[2]=sfind(s,2); //��������
                node.Array[3]=sfind(s,3); //��������

                //�����Դ�б���
                  PowerStruct ps = new PowerStruct();
                  Power.Add(ps);
                ps.Name = node.Name;
                ps.OutputName = node.Array[2];

                //�����ظ������뵽���������
                for(int i2=0; i2<4; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                //���ӹ�ϵ
                node.Connect[0,1]=1; //��Ϊ���ؽ�㵽���ؽ��ͨ
                node.Connect[0,2]=0; //��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[0,3]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[1,2]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[1,3]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[2,3]=0;//��Ϊ�������㵽�������㲻ͨ
              }

              else if(ch == "F_" || ch == "H_")  //�������Ƶ�����ѹ��Դ;�����ж�ȡ��Ϣ
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);
                node.Name = sfind(s,1);
                node.Number=4;
                node.Array[2]=sfind(s,2); //�����
                node.Array[3]=sfind(s,3); //�����
                //
                //��ȡ��һ����Ϣ
                  s = sr.ReadLine();
                node.Array[0]=sfind(s,2);  //���ؽ��
                node.Array[1]=sfind(s,3);  //���ؽ��
                //
                //�����Դ�б���
                  PowerStruct ps = new PowerStruct();
                  Power.Add(ps);
                ps.Name = node.Name;
                ps.OutputName = node.Array[2];
                //�����ظ������뵽���������
                for(int i2=0; i2<4; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                //���ӹ�ϵ
                node.Connect[0,1]=1; //��Ϊ���ؽ�㵽���ؽ��ͨ
                node.Connect[0,2]=0; //��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[0,3]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[1,2]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[1,3]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[2,3]=0;//��Ϊ�������㵽�������㲻ͨ
                //
              }
              else if(ch == "I_" || ch == "V_") //����������ѹ��Դ
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);
                node.Name = sfind(s,1);
                node.Number=2;
                node.Array[0]=sfind(s,2); //�����
                node.Array[1]=sfind(s,3);
                //
                 //�����Դ�б���
                  PowerStruct ps = new PowerStruct();
                  Power.Add(ps);
                ps.Name = node.Name;
                ps.OutputName = node.Array[0];
                //

               //�����ظ������뵽���������
                for(int i2=0; i2<2; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                //���ӹ�ϵ
                node.Connect[0,1]=0; //��Ϊ����㵽����㲻ͨ
              }

              else if(ch == "L_" ||ch =="R_")  //��л����
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                node.Name = sfind(s,1);
                node.Number=2;
                node.Array[0]=sfind(s,2);
                node.Array[1]=sfind(s,3);
                //�����ظ������뵽���������
                for(int i2=0; i2<2; i2++)
                {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                }
                //���ӹ�ϵ
                node.Connect[0,1]=2;
              }
              else if(ch == "S_") //��ѹ���ƿ���
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                node.Name = sfind(s,1);
                node.Number=4;
                node.Array[0]=sfind(s,4); //���ؽ��
                node.Array[1]=sfind(s,5); //���ؽ��
                node.Array[2]=sfind(s,2); //�����
                node.Array[3]=sfind(s,3); //�����
                //����Ϣ���뿪�ؽṹ��
                  SwtichStruct ss = new SwtichStruct();
                  Switch.Add(ss);
                ss.Name = node.Name;
                ss.Num = 2;
                ss.Array[0] = node.Array[2];
                ss.Array[1] = node.Array[3];
                //
                //�����ظ������뵽���������
                for(int i2=0; i2<4; i2++)
                {
                  if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                  {
                    Node_StringList.Add(node.Array[i2]);
                  }
                }
                //���ӹ�ϵ
                node.Connect[0,1]=1; //��Ϊ���ؽ�㵽���ؽ��ͨ
                node.Connect[0,2]=0; //��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[0,3]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[1,2]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[1,3]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                node.Connect[2,3]=0;//��Ϊ�������㵽�������㲻ͨ
                //
                }

              else if(ch == "SW") //�Զ��嵥��˫������
              {
                  ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                node.Name = sfind(s,1);
                node.Number=3;
                node.Array[0]=sfind(s,2); //���ع̶��˿�
                node.Array[1]=sfind(s,3); //���س��ն˿�
                node.Array[2]=sfind(s,4); //���س����˿�
                //����Ϣ���뿪�ؽṹ��
                  SwtichStruct ss = new SwtichStruct();
                  Switch.Add(ss);
                ss.Name = node.Name;
                ss.Num = 3;
                for(int i2=0; i2<3; i2++)
                   ss.Array[i2] = node.Array[i2];

                //�����ظ������뵽���������
                for(int i2=0; i2<3; i2++)
                {
                  if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                  {
                    Node_StringList.Add(node.Array[i2]);
                  }
                }
                //���ӹ�ϵ
                node.Connect[0,1]=0; //��ʼʱ����һ��Ҫ��Ϊ��
                node.Connect[0,2]=0; //��ͨ
                node.Connect[1,2]=0;//��ͨ
                //
                }
                else if(ch == "X_")  //�Ŵ����򿪹أ����ǿ��أ���������ж�ȡ��Ϣ
                {
                    ComponentStruct node = new ComponentStruct();
                      Node.Add(node);
                  
                  node.Name = sfind(s,1);

                    string x_s = sfind(s, 4).ToLower();
                  if(x_s=="sw_tclose"||x_s=="sw_topen")
                  {
                    //���ǿ���
                    node.Number=2;
                    node.Array[0]=sfind(s,2);
                    node.Array[1]=sfind(s,3);
                    //
                    //����Ϣ���뿪�ؽṹ��
                      SwtichStruct ss = new SwtichStruct();
                      Switch.Add(ss);

                    ss.Name = node.Name;
                    ss.Num = 2;
                    ss.Array[0] = node.Array[0];
                    ss.Array[1] = node.Array[1];
                    //
                    //�����ظ������뵽���������
                    for(int i2=0; i2<2; i2++)
                    {
                     if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                     {
                      Node_StringList.Add(node.Array[i2]);
                     }
                    }
                    //���ӹ�ϵ
                    node.Connect[0,1]=0;  //��ʼ��Ϊ��
                  }
                  // ���ǷŴ���uA741
                  else{
                   node.Number=3;
                   node.Array[0]=sfind(s,2);//����߶�
                   node.Array[1]=sfind(s,3);//����Ͷ�
                   node.Array[2]=sfind(s,6);//�����
                   //
                   //�����ظ������뵽���������
                   for(int i2=0; i2<3; i2++)
                  {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                  }
                   //
                   node.Connect[0,1]=0;
                   node.Connect[0,2]=1;  //��Ϊ��������������ͨ
                   node.Connect[1,2]=0;
                  }
                }
                else if(ch == "W_")  //�������ƿ���;�����ж�ȡ��Ϣ
                {
                    ComponentStruct node = new ComponentStruct();
                      Node.Add(node);

                  node.Name = sfind(s,1);
                  node.Number=4;
                  node.Array[2]=sfind(s,2); //�����
                  node.Array[3]=sfind(s,3); //�����
                  //
                  //
                    s = sr.ReadLine();
                  node.Array[0]=sfind(s,2);  //���ؽ��
                  node.Array[1]=sfind(s,3);  //���ؽ��
                  //
                    //����Ϣ���뿪�ؽṹ��
                    SwtichStruct ss = new SwtichStruct();
                      Switch.Add(ss);

                  ss.Name = node.Name;
                  ss.Num = 2;
                  ss.Array[0] = node.Array[2];
                  ss.Array[1] = node.Array[3];
                  //
                  //�����ظ������뵽���������
                  for(int i2=0; i2<4; i2++)
                  {
                    if(Node_StringList.IndexOf(node.Array[i2]) == -1)
                    {
                     Node_StringList.Add(node.Array[i2]);
                    }
                  }
                  //���ӹ�ϵ
                  node.Connect[0,1]=1; //��Ϊ���ؽ�㵽���ؽ��ͨ
                  node.Connect[0,2]=0; //��Ϊ���ؽ�㵽�������㲻ͨ
                  node.Connect[0,3]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                  node.Connect[1,2]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                  node.Connect[1,3]=0;//��Ϊ���ؽ�㵽�������㲻ͨ
                  node.Connect[2,3]=0;//��Ϊ�������㵽�������㲻ͨ
                  //
                }
                else
                {
                    throw new Exception( string.Format("����Ԫ�� : {0}", ch));
                }
              }
              sr.Close();
             //ȷ���صı�ǩ
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
             //�궨��Դ���ء����ء����ݽ���ھ����е�λ��
             GetThePosition();
        }

        void GetThePosition()
        {
          //�궨��Դλ��
            for (int i = 0; i < Power.Count; i++)
            {
                int index = Node_StringList.IndexOf(Power[i].OutputName);
                System.Diagnostics.Debug.Assert(index != -1);
                Power[i].Position = index;
            }
          //�궨��λ�á�ע��ؽ���������ͬ
            for (int i = 0; i < Egnd.Count; i++)
            {
                int index = Node_StringList.IndexOf(Egnd[i].Name);
                System.Diagnostics.Debug.Assert(index != -1);
                Egnd[i].Position = index;
            }
          //�궨����λ��
            for (int i = 0; i < Switch.Count; i++)
            {
                for (int j = 0; j < Switch[i].Num; j++)
                {
                    int index = Node_StringList.IndexOf(Switch[i].Array[j]);
                    System.Diagnostics.Debug.Assert(index != -1);
                    Switch[i].Position[j] = index;
                }
            }
          //�궨����λ��
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
            //i:���ѭ����������0��Node_number-1��ΪԪ������
            int []Port_num = new int[6];  //���Ԫ���˿��ڽ���ַ����б��е�λ��
            for(int i=0; i<Node.Count; i++) //�������Ԫ���ĵ�ͨ��ϵ
            {
                for(int j=0; j<Node[i].Number; j++) //��Ԫ�����н��
                {
                   //������ھ����е�λ�÷���������
                   Port_num[j] = Node_StringList.IndexOf(Node[i].Array[j]);
                }
                for (int m = 0; m < Node[i].Number - 1; m++) //�˿�0��n-2
                {
                    for (int n = 1; n < Node[i].Number; n++)//�˿�1��n-1
                    {
                        if (Node[i].Connect[m, n] == 1)  //����ͨ
                        {
                            Adj_matrix[Port_num[m], Port_num[n]] = 1;
                        }
                        else if (Node[i].Connect[m, n] == 2) //˫��ͨ
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
