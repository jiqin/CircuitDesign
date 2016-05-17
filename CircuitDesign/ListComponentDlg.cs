using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CircuitDesign
{
    public partial class ListComponentDlg : Form
    {
        private ComponentTemplate template_manager_ = null;
        private List<BaseComponent> components_ = null;
        public BaseComponent selected_component = null;
        public ListComponentDlg()
        {
            InitializeComponent();
        }

        public void InitComponents(ComponentTemplate template_manager, bool is_edit_component)
        {
            if (!is_edit_component)
            {
                button_add_component.Visible = false;
                button_remove_component.Visible = false;
            }
            template_manager_ = template_manager;
            ResetComponentList();
        }

        private void ResetComponentList()
        {
            listBox1.Items.Clear();
            components_ = template_manager_.GetTemplateComponents();
            foreach (BaseComponent c in components_)
            {
                string s = string.Format("{0} : {1}", c.Type, c.TagName);
                listBox1.Items.Add(s);
            }
            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
            listBox1.Invalidate();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                return;
            }
            BaseComponent c = components_[listBox1.SelectedIndex];
            c.SetCenterPos(new Point(panel1.Width / 2, panel1.Height / 2));
            c.Draw(e.Graphics);
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                selected_component = null;
            }
            else
            {
                selected_component = components_[listBox1.SelectedIndex];
            }
            this.Hide();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            selected_component = null;
            this.Hide();
        }

        private void button_add_component_Click(object sender, EventArgs e)
        {
            EditComponentDlg dlg = new EditComponentDlg();
            dlg.InitData(template_manager_.GetTypes(), null);
            dlg.ShowDialog();

            if (dlg.new_component == null)
            {
                return;
            }

            template_manager_.AddComponentTemplate(dlg.new_component);
            template_manager_.Save();
            template_manager_.Load();
            ResetComponentList();

            if (dlg.connection_relation_msg != "")
            {
                SaveRelationsToDatabase(Application.StartupPath + "\\component_model.mdb", dlg.connection_relation_msg);
            }
        }

        private void SaveRelationsToDatabase(string data_base_file, string msg)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + data_base_file);
                conn.Open();
                string[] datas = msg.Split(',');
                OleDbCommand command = new OleDbCommand(
                    string.Format("delete component where Type = \"{0}\"", datas[0]), conn);
                command.ExecuteNonQuery();

                command = new OleDbCommand(
                    string.Format("insert into component values(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\")", datas[0], datas[1], datas[2], datas[3], datas[4]), conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        private string LoadRelationsFromDatabase(string data_base_file, string type)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + data_base_file);
                conn.Open();

                OleDbCommand command = new OleDbCommand(
                    string.Format("select * from component where Type = \"{0}\"", type), conn);
                OleDbDataReader r = command.ExecuteReader();
                String relation = null;
                if (r.HasRows)
                {
                    r.Read();
                    relation = r.GetString(3);
                }
                conn.Close();
                return relation;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void button_remove_component_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                return;
            }

            template_manager_.removeComponentTemplate(components_[listBox1.SelectedIndex].Type);
            template_manager_.Save();
            template_manager_.Load();
            ResetComponentList();
        }

        private void button_edit_component_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                return;
            }

            EditComponentDlg dlg = new EditComponentDlg();
            dlg.InitData(template_manager_.GetTypes(), components_[listBox1.SelectedIndex]);
            dlg.ShowDialog();

            if (dlg.new_component == null)
            {
                return;
            }

            template_manager_.removeComponentTemplate(components_[listBox1.SelectedIndex].Type);
            template_manager_.AddComponentTemplate(dlg.new_component);
            template_manager_.Save();
            template_manager_.Load();
            ResetComponentList();

            if (dlg.connection_relation_msg != "")
            {
                SaveRelationsToDatabase(Application.StartupPath + "\\component_model.mdb", dlg.connection_relation_msg);
            }
        }
    }
}