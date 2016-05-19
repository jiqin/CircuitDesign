using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MatrixTools
{
    public class MatrixTool
    {
        public static bool IsMatrixEqual(int[,] m1, int[,] m2)
        {
            if (m1.GetLength(0) != m2.GetLength(0) ||
                m1.GetLength(1) != m2.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m1.GetLength(1); j++)
                {
                    if (m1[i,j] != m2[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void MatrixCopy(int[,] matrix1, int[,] matrix2)//矩阵复制
        {
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrix2[i, j] = matrix1[i, j];
                }
            }
        }

        public static void MatrixClearRowCol(int[,] matrix, int index)//行、列清零
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[index, i] = 0;
                matrix[i, index] = 0;
            }
        }

        public static void MatirxAdd(int[,] m1, int[,] m2)
        {
            //Debug.Assert(m1.GetLength(0) == m2.GetLength(0) &&
            //    m1.GetLength(1) == m2.GetLength(1));

            for (int i = 0; i < m1.GetLength(0); ++i)
            {
                for (int j = 0; j < m1.GetLength(1); ++j)
                {
                    m1[i, j] += m2[i, j];
                }
            }
        }

        public static void matrix_reduce(int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); ++i)
            {
                for (int j = 0; j < m.GetLength(1); ++j)
                {
                    m[i, j] = (m[i,j] >= 1 ? 1 : 0);
                }
            }
        }


        public static void vector_mul_matrix(int[] vout, int[] vin, int[,] matrix)//向量 * 矩阵
        {
            //vout = vin * matrix

            for (int i = 0; i < vout.Length; ++i)
            {
                vout[i] = 0;
                for (int j = 0; j < vin.Length; ++j)
                {
                    vout[i] += vin[j] * matrix[j, i];
                }
            }
        }

        public static void vector_reduce(int[] v)//将向量中大于1的数都归1
        {
            for (int i = 0; i < v.Length; ++i)
            {
                v[i] = (v[i] >= 1 ? 1 : 0);
            }
        }

        
        public static void vector_assign(int[] vout, int[] vin)//向量复制 vout = vin
        {
            for (int i = 0; i < vout.Length; ++i)
            {
                vout[i] = vin[i];
            }
        }

        public static bool vector_equal(int[] v1, int[] v2)//判断向量是否相等
        {
            bool b = true;
            for (int i = 0; i < v1.Length; ++i)
            {
                if (v1[i] != v2[i])
                {
                    b = false;
                    break;
                }
            }
            return b;
        }

        public static bool IsEndNode(int[,] matrix, int index)
        {
            //Debug.Assert(matrix.GetLength(0) == matrix.GetLength(1));
            const int INVALID_INDEX = -1;
            int row = INVALID_INDEX;
            int col = INVALID_INDEX;

            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                if (i == index)
                {
                    continue;
                }
                if (matrix[i, index] == 1)
                {
                    if (row != INVALID_INDEX)
                    {
                        return false;
                    }
                    row = i;
                }
                if (matrix[index, i] == 1)
                {
                    if (col != INVALID_INDEX)
                    {
                        return false;
                    }
                    col = i;
                }
            }
            if (row != INVALID_INDEX && col != INVALID_INDEX && row != col)
            {
                return false;
            }
            return true;
        }

        public static void SolveStarProblem(List<string> Nodelist, int[,] matrix, int[] vn)//解决星型连接问题
        {
            for (int i = 0; i < vn.Length; i++)
            {
                if (vn[i] == 0)
                {
                    MatrixClearRowCol(matrix, i);
                }
            }

            List<int> handled_end_nodes = new List<int>();
            bool quit = false;
            while (!quit)
            {
                quit = true;

                for (int i = 0; i < Nodelist.Count; ++i)
                {
                    if (!IsEndNode(matrix, i))
                    {
                        continue;
                    }
                    if (handled_end_nodes.Contains(i))
                    {
                        continue;
                    }
                    handled_end_nodes.Add(i);
                    quit = false;

                    if (Nodelist[i].StartsWith("V") || Nodelist[i].StartsWith("GND"))
                    {
                        continue;
                    }
                    MatrixClearRowCol(matrix, i);
                    vn[i] = 0;
                }
            }
        }

        public static void copy_array<T>(T[] from, T[] to, int start, int len)
        {
            for (int i = 0; i < len; ++i)
            {
                to[i] = from[start + i];
            }
        }

        public static string join_array<T>(T[] arr, string sep)
        {
            string[] v = new string[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                v[i] = arr[i].ToString();
            }
            return string.Join(sep, v);
        }
    }
}
