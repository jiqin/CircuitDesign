using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using CircuitDesign;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private NetlistModel init_network_model()
        {
            NetlistComponentTemplateManager netlist_component_template_manager = new NetlistComponentTemplateManager();
            netlist_component_template_manager.LoadTemplates(".\\component_model.mdb");

            NetlistModel network_model = new NetlistModel();
            network_model.LoadTemplates(netlist_component_template_manager);
            network_model.load_network_from_string(File.ReadAllText("Netlist\\Door0.net"));
            return network_model;
        }

        [TestMethod]
        public void TestMethodLoadNetwork()
        {
            NetlistModel network_model = init_network_model();

            Assert.AreEqual(6, network_model.components.Count);
            Assert.AreEqual("V_V1 A GND", network_model.components[0].toString());

            Assert.AreEqual(1, network_model.power_components.Count);
            Assert.AreEqual(1, network_model.ground_components.Count);

            Assert.AreEqual(3, network_model.switch_components.Count);
            Assert.AreEqual("U1", network_model.switch_components[0].Name);
            Assert.AreEqual("U2", network_model.switch_components[1].Name);
            Assert.AreEqual("U3", network_model.switch_components[2].Name);

            Assert.AreEqual(2, network_model.r_load_components.Count);
            Assert.AreEqual(10, network_model.node_names.Count);
        }

        [TestMethod]
        public void TestCreateCombineList()
        {
            List<int[]> values = Utils.CreateCombineList(3);
            Assert.AreEqual(8, values.Count);
            Assert.AreEqual(3, values[0].Length);

            Assert.AreEqual(0, values[0][0]);
            Assert.AreEqual(0, values[0][1]);
            Assert.AreEqual(0, values[0][2]);

            Assert.AreEqual(1, values[7][0]);
            Assert.AreEqual(1, values[7][1]);
            Assert.AreEqual(1, values[7][2]);
        }

        [TestMethod]
        public void IsEndNodeTest()
        {
            int[,] m = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 1, 1 }, { 0, 1, 1, 0 }, { 0, 1, 0, 1 } };
            Assert.IsTrue(MatrixTool.IsEndNode(m, 0));
            Assert.IsTrue(!MatrixTool.IsEndNode(m, 1));
            Assert.IsTrue(MatrixTool.IsEndNode(m, 2));
            Assert.IsTrue(MatrixTool.IsEndNode(m, 3));
        }

        [TestMethod]
        public void SolveStarProblemTest()
        {
            // List<string> node_list = new List<string>(new string[] { "", "" });
            // int[,] m = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 1, 1 }, { 0, 1, 1, 0 }, { 0, 1, 0, 1 } };
        }
    }
}
