using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitDesign
{
    public class Utils
    {
        /*
         * 连接字符串
         */
        public static string Join(int[,] m, string sp_col, string sp_row)
        {
            string s = "";
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    s += m[i, j].ToString() + sp_col;
                }
                s += sp_row;
            }
            return s;
        }

        public static string sfind(string s, int num)//寻找相应参数
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

        /*
         * 生成全排列数组
         */
        public static List<int[]> CreateCombineList(int num)
        {
            List<int[]> states = new List<int[]>();
            if (num <= 0)
            {
                return states;
            }

            int[] d = new int[num];//生成初始开关组合变量
            for (int i = 0; i < num; ++i)
            {
                d[i] = 0;
            }

            while (true)
            {
                {
                    int[] tmp = new int[num];//临时存储变量
                    for (int i = 0; i < num; ++i)
                    {
                        tmp[i] = d[i];
                    }
                    states.Add(tmp);//保存临时存储变量=当前开关状态变量
                }

                {
                    int i = 0;
                    for (i = 0; i < num; ++i)
                    {
                        if (d[i] == 0)
                        {
                            d[i] = 1;
                            break;
                        }
                        else
                        {
                            d[i] = 0;
                        }
                    }
                    if (i >= num)
                    {
                        break;
                    }
                }
            }
            return states;
        }
    }
}
