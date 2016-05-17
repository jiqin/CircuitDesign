using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SCA_Load
{
    public partial class FormErrorInfo : Form
    {
        public bool has_error = false;
        public FormErrorInfo()
        {
            InitializeComponent();
        }

        public void AddErrorMessage(string s)
        {
            has_error = true;
            richTextBox_error.Text += s + "\n";
        }
    }
}