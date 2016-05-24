using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using CircuitDesign;

namespace UnitTestProject1
{
    [TestClass]
    public class DesignToolsTest
    {
        [TestMethod]
        public void TestMethodLoadNetwork()
        {
        }

        [TestMethod]
        public void RotatePoint()
        {
            object[,] testcases = new object[,]{
                // pt, originPt, direct, resultPt
                {new Point(1, 0), new Point(0, 0), 1, new Point(0, 1)},
                {new Point(1, 0), new Point(0, 0), 2, new Point(-1, 0)},
                {new Point(1, 0), new Point(0, 0), 3, new Point(0, -1)},
                {new Point(1, 0), new Point(0, 0), 4, new Point(1, 0)},
                {new Point(1, 0), new Point(0, 0), 5, new Point(0, 1)},
                {new Point(1, 0), new Point(0, 0), 6, new Point(-1, 0)},
                {new Point(1, 0), new Point(0, 0), 7, new Point(0, -1)},
                {new Point(1, 0), new Point(0, 0), 8, new Point(1, 0)},

                {new Point(1, 0), new Point(0, 0), -1, new Point(0, -1)},
                {new Point(1, 0), new Point(0, 0), -2, new Point(-1, 0)},
                {new Point(1, 0), new Point(0, 0), -3, new Point(0, 1)},
                {new Point(1, 0), new Point(0, 0), -4, new Point(1, 0)},
                {new Point(1, 0), new Point(0, 0), -5, new Point(0, -1)},
                {new Point(1, 0), new Point(0, 0), -6, new Point(-1, 0)},
                {new Point(1, 0), new Point(0, 0), -7, new Point(0, 1)},
                {new Point(1, 0), new Point(0, 0), -8, new Point(1, 0)},

                {new Point(-1, -1), new Point(0, 0), 1, new Point(1, -1)},
                {new Point(-1, -1), new Point(0, 0), 2, new Point(1, 1)},
                {new Point(-1, -1), new Point(0, 0), 3, new Point(-1, 1)},
                {new Point(-1, -1), new Point(0, 0), 4, new Point(-1, -1)},

                {new Point(1, 0), new Point(1, 1), 1, new Point(2, 1)},
                {new Point(1, 0), new Point(1, 1), 2, new Point(1, 2)},
                {new Point(1, 0), new Point(1, 1), 3, new Point(0, 1)},
                {new Point(1, 0), new Point(1, 1), 4, new Point(1, 0)},
                
                {new Point(1, 0), new Point(1, 1), -1, new Point(0, 1)},
                {new Point(1, 0), new Point(1, 1), -2, new Point(1, 2)},
                {new Point(1, 0), new Point(1, 1), -3, new Point(2, 1)},
                {new Point(1, 0), new Point(1, 1), -4, new Point(1, 0)},
            };

            for (int i = 0; i < testcases.GetLength(0); ++i)
            {
                Assert.AreEqual(
                    DesignTools.RotatePoint((Point)testcases[i, 0], (Point)testcases[i, 1], (int)testcases[i, 2]), 
                    (Point)testcases[i, 3],
                    String.Format("Case {0}", i));
            }
        }

        [TestMethod]
        public void RotateRectagle()
        {
            object[,] testcases = new object[,]{
                // rect, originPt, direct, resultRect
                { new Rectangle(-1, -1, 2, 2), new Point(0, 0), 1,  new Rectangle(-1, -1, 2, 2)},
                { new Rectangle(-1, -1, 2, 2), new Point(0, 0), 2,  new Rectangle(-1, -1, 2, 2)},
                { new Rectangle(-1, -1, 2, 2), new Point(0, 0), 3,  new Rectangle(-1, -1, 2, 2)},
                { new Rectangle(-1, -1, 2, 2), new Point(0, 0), 4,  new Rectangle(-1, -1, 2, 2)},

                { new Rectangle(-1, -1, 2, 2), new Point(0, 0), -1,  new Rectangle(-1, -1, 2, 2)},
                { new Rectangle(-1, -1, 2, 2), new Point(0, 0), -2,  new Rectangle(-1, -1, 2, 2)},
                { new Rectangle(-1, -1, 2, 2), new Point(0, 0), -3,  new Rectangle(-1, -1, 2, 2)},
                { new Rectangle(-1, -1, 2, 2), new Point(0, 0), -4,  new Rectangle(-1, -1, 2, 2)},

                { new Rectangle(-1, -1, 1, 1), new Point(0, 0), 1,  new Rectangle(0, -1, 1, 1)},
                { new Rectangle(-1, -1, 1, 1), new Point(0, 0), 2,  new Rectangle(0, 0, 1, 1)},
                { new Rectangle(-1, -1, 1, 1), new Point(0, 0), 3,  new Rectangle(-1, 0, 1, 1)},

                { new Rectangle(-1, -1, 1, 1), new Point(1, 1), 1,  new Rectangle(2, -1, 1, 1)},
                { new Rectangle(-1, -1, 1, 1), new Point(1, 1), 2,  new Rectangle(2, 2, 1, 1)},
                { new Rectangle(-1, -1, 1, 1), new Point(1, 1), 3,  new Rectangle(-1, 2, 1, 1)},

            };

            for (int i = 0; i < testcases.GetLength(0); ++i)
            {
                Assert.AreEqual(
                    DesignTools.RotateRectangle((Rectangle)testcases[i, 0], (Point)testcases[i, 1], (int)testcases[i, 2]),
                    (Rectangle)testcases[i, 3],
                    String.Format("Case {0}", i));
            }
        }

        [TestMethod]
        public void NormalizeDirection()
        {
            object[,] testcases = new object[,]{
                // rect, result
                {0, 0},
                {1, 1},
                {2, 2},
                {3, 3},
                {4, 0},
                {5, 1},
                {6, 2},
                {7, 3},
                {16, 0},
                {17, 1},
                {18, 2},
                {19, 3},

                {-1, 3},
                {-2, 2},
                {-3, 1},
                {-4, 0},
                {-5, 3},
                {-6, 2},
                {-7, 1},
                {-8, 0},
                {-17, 3},
                {-18, 2},
                {-19, 1},
                {-20, 0},
            };

            for (int i = 0; i < testcases.GetLength(0); ++i)
            {
                Assert.AreEqual(
                    DesignTools.NormalizeDirection((int)testcases[i, 0]),
                    (int)testcases[i, 1],
                    String.Format("Case {0}", i));
            }
        }

        [TestMethod]
        public void CreateRectangle()
        {
            object[,] testcases = new object[,]{
                // pt1, pt2, resultRect
                { new Point(-1, -2), new Point(0, 0), new Rectangle(-1, -2, 1, 2)},
                { new Point(-1, -2), new Point(1, -7), new Rectangle(-1, -7, 2, 5)},
                { new Point(-1, -2), new Point(-2, -7), new Rectangle(-2, -7, 1, 5)},
                { new Point(-1, -2), new Point(-2, 7), new Rectangle(-2, -2, 1, 9)},
                { new Point(-1, -2), new Point(-1, 7), new Rectangle(-1, -2, 0, 9)},
            };

            for (int i = 0; i < testcases.GetLength(0); ++i)
            {
                Assert.AreEqual(
                    DesignTools.CreateRectangle((Point)testcases[i, 0], (Point)testcases[i, 1]),
                    (Rectangle)testcases[i, 2],
                    String.Format("Case {0}", i));
            }
        }
    }
}
