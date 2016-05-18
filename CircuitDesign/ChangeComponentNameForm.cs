using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CircuitDesign
{
    public partial class ChangeComponentNameForm : Form
    {
        public string name = "";
        public ChangeComponentNameForm()
        {
            InitializeComponent();
        }

        private void ChangeNameDlg_Load(object sender, EventArgs e)
        {
            textBoxName.Text = name;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            name = textBoxName.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}