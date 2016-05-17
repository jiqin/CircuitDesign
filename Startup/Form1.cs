using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Startup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_design_Click(object sender, EventArgs e)
        {
            CircuitDesign.Form1 form = new CircuitDesign.Form1();
            form.ShowDialog();
        }

        private void button_load_analysis_Click(object sender, EventArgs e)
        {
            Form form = new SCA_Load.FormLoadMain();
            form.ShowDialog();
        }

        private void button_pg_analysis_Click(object sender, EventArgs e)
        {
            Form form = new SCA_PowerGround.FormPGMain();
            form.ShowDialog();
        }
    }
}