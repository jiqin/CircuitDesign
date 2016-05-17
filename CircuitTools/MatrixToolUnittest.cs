using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MatrixTools
{
    class MatrixToolUnittest
    {
        static void Main()
        {
            IsEndNodeTest();
            SolveStarProblemTest();
        }

        static void Check(bool value)
        {
            if (!value)
            {
                throw new Exception("error");
            }
        }

        static void IsEndNodeTest()
        {
            int[,] m = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 1, 1 }, { 0, 1, 1, 0 }, { 0, 1, 0, 1 } };
            Check(MatrixTool.IsEndNode(m, 0));
            Check(!MatrixTool.IsEndNode(m, 1));
            Check(MatrixTool.IsEndNode(m, 2));
            Check(MatrixTool.IsEndNode(m, 3));
        }

        static void SolveStarProblemTest()
        {
            // List<string> node_list = new List<string>(new string[] { "", "" });
            // int[,] m = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 1, 1 }, { 0, 1, 1, 0 }, { 0, 1, 0, 1 } };
        }
    }
}
