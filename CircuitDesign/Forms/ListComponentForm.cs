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
    public partial class ListComponentForm : Form
    {
        private CircuitComponentTemplateManager _circuit_component_template_manager = null;
        private NetlistComponentTemplateManager _netlist_component_template_manager = null;
        private List<BaseComponent> components_ = null;
        public BaseComponent selected_component = null;
        public ListComponentForm()
        {
            InitializeComponent();
        }

        public void InitComponents(
            CircuitComponentTemplateManager circuit_component_template_manager, 
            NetlistComponentTemplateManager netlist_component_template_manager,
            bool is_edit_component)
        {
            if (!is_edit_component)
            {
                button_add_component.Visible = false;
                button_remove_component.Visible = false;
                button_edit_component.Visible = false;
            }
            _netlist_component_template_manager = netlist_component_template_manager;
            _circuit_component_template_manager = circuit_component_template_manager;

            ResetComponentList();
        }

        private void ResetComponentList()
        {
            listBox1.Items.Clear();
            components_ = _circuit_component_template_manager.GetTemplateComponents();
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
            add_or_edit_component(_circuit_component_template_manager.GetTypes(), null, null);
        }

        private void button_edit_component_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                return;
            }

            BaseComponent component = components_[listBox1.SelectedIndex];
            List<NetlistComponent> netlist_components = _netlist_component_template_manager.load_components(component.Type.Substring(0, 3));
            add_or_edit_component(_circuit_component_template_manager.GetTypes(), component, netlist_components[0]);
        }

        private void add_or_edit_component(List<string> componentname_s, BaseComponent component, NetlistComponent netlist_component)
        {
            EditComponentForm dlg = new EditComponentForm();
            dlg.InitData(componentname_s, component, netlist_component);
            dlg.ShowDialog();

            if (dlg.new_component == null)
            {
                return;
            }

            if (component != null)
            {
                _circuit_component_template_manager.removeComponentTemplate(component.Type);
            }
            _circuit_component_template_manager.AddComponentTemplate(dlg.new_component);
            _circuit_component_template_manager.Save();
            _circuit_component_template_manager.Load();

            if (dlg.connection_relation_msg != "")
            {
                _netlist_component_template_manager.SaveRelationsToDatabase(dlg.connection_relation_msg);
            }

            ResetComponentList();
        }

        private void button_remove_component_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                return;
            }

            _circuit_component_template_manager.removeComponentTemplate(components_[listBox1.SelectedIndex].Type);
            _circuit_component_template_manager.Save();
            _circuit_component_template_manager.Load();
            ResetComponentList();
        }
    }
}