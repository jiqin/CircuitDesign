using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CircuitDesign
{
    public partial class InputSwitchLoadStatesForm : Form
    {
        private List<SwitchLoadStatesInput> _states = new List<SwitchLoadStatesInput>();

        public InputSwitchLoadStatesForm()
        {
            InitializeComponent();
        }

        public void Init(List<String> column_names, List<SwitchLoadStatesInput> values)
        {
            _states.Clear();
            _states.AddRange(values);
            init_data_grid_view(dataGridView1, false, column_names, values);
        }

        private static int _get_int_value(object v)
        {
            try
            {
                return (int)v;
            }
            catch (Exception e1)
            {
                try
                {
                    return int.Parse((string)v);
                }
                catch (Exception e2)
                {
                    return 0;
                }
            }
        }

        private static void _get_input(DataGridView data_grid_view, List<SwitchLoadStatesInput> states)
        {
            for (int i = 0; i < data_grid_view.Rows.Count; ++i)
            {
                DataGridViewRow row = data_grid_view.Rows[i];
                SwitchLoadStatesInput state = states[i];

                state.enable = (bool)row.Cells[1].Value;
                int[] values = new int[row.Cells.Count - 2];
                for (int j = 0; j < row.Cells.Count - 2; ++j)
                {
                    values[j] = _get_int_value(row.Cells[j + 2].Value);
                }
                state.set_state(values, state.get_switch_states().Length);
            }
        }

        public static void init_data_grid_view(DataGridView data_grid_view, bool read_only,
            List<String> column_names, List<SwitchLoadStatesInput> values)
        {
            data_grid_view.Columns.Clear();

            {
                DataGridViewColumn c = new DataGridViewTextBoxColumn();
                c.ReadOnly = true;
                c.Width = 60;
                data_grid_view.Columns.Add(c);
            }
            {
                DataGridViewColumn c = new DataGridViewCheckBoxColumn();
                c.ReadOnly = read_only;
                c.Width = 40;
                data_grid_view.Columns.Add(c);
            }
            for (int i = 0; i < column_names.Count; ++i)
            {
                String column_name = column_names[i];
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = column_name;
                if (values.Count > 0 && i < values[0].get_switch_states().Length)
                {
                    column.ReadOnly = true;
                }
                else
                {
                    column.ReadOnly = read_only;
                }
                column.Width = 40;

                data_grid_view.Columns.Add(column);
            }

            for (int value_index = 0; value_index < values.Count; ++value_index)
            {
                SwitchLoadStatesInput value = values[value_index];
                List<object> data = new List<object>();

                data.Add(string.Format("state {0}", value_index + 1));
                data.Add(value.enable);
                foreach (int v in value.get_all_states())
                {
                    data.Add(v);
                }
                data_grid_view.Rows.Add(data.ToArray());
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            _get_input(dataGridView1, _states);
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void get_input(List<SwitchLoadStatesInput> states)
        {
            states.Clear();
            states.AddRange(_states);
        }
    }
}