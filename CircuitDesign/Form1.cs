using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace CircuitDesign
{
    public partial class Form1 : Form
    {
        private CircuitManager _componentManager;

        enum State
        {
            mouse_op = 0,
            add_component,
            add_connect_line,
            select_component,
            select_connect_line,
        };
        struct StateMessage
        {
            public static string mouse_op = "鼠标选取";
            public static string add_connect_line = "连接元件";
            public static string add_connect_node = "添加连接点";
            public static string add_power = "添加电源";
            public static string add_ground = "添加地";
            public static string add_component = "添加组件";
            public static string select_component = "选取元件";
            public static string select_connect_line = "选取连接线";
        };

        private State state = State.mouse_op;
        private BaseComponent _toBeAddComponent;
        private BaseComponent _selectedComponent;
        private ConnectLine _toBeAddConnectLine;
        private ConnectLine _selectedConnectLine;

        // string _last_filename_ = Application.StartupPath + "\\Circuit\\_last.xml";
        // string _template_filename_ = Application.StartupPath + "\\component_template.xml";

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            resetComponent();

            /*
            _componentManager.InitTemplate(_template_filename_);
            if (System.IO.File.Exists(_last_filename_))
            {
                try
                {
                    LoadFromFile(_last_filename_);
                }
                catch (Exception ex)
                {
                }
            }
             */
            toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
        }

        public void init_circuit_manager(CircuitManager component_manager)
        {
            this._componentManager = component_manager;
        }

        private void resetComponent()
        {
            _toBeAddComponent = null;
            _selectedComponent = null;
            _toBeAddConnectLine = null;
            _selectedConnectLine = null;

            state = State.mouse_op;
            toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
        }

        //private void LoadFromFile(string filename_)
        //{
        //    _componentManager.LoadFromFile(filename_);
        //    toolStripStatusLabel_filepath.Text = filename_;
        //}

        //private void LoadFromFile()
        //{
        //    OpenFileDialog dlg = new OpenFileDialog();
        //    dlg.InitialDirectory = Application.StartupPath + "\\Circuit";
        //    dlg.Filter = "xml files (*.xml)|*.xml";

        //    if (dlg.ShowDialog() == DialogResult.Cancel)
        //        return;

        //    LoadFromFile(dlg.FileName);
        //}

        //private void SaveToFile(string filename_)
        //{
        //    _componentManager.SaveToFile(filename_);
        //    toolStripStatusLabel_filepath.Text = filename_;
        //}

        //private void SaveToFile()
        //{
        //    SaveFileDialog dlg = new SaveFileDialog();
        //    dlg.InitialDirectory = Application.StartupPath + "\\Circuit";
        //    dlg.Filter = "xml files (*.xml)|*.xml";

        //    if (dlg.ShowDialog() == DialogResult.Cancel)
        //        return;

        //    SaveToFile(dlg.FileName);
        //}

        //private void SaveAsNetworkToFile()
        //{
        //    SaveFileDialog dlg = new SaveFileDialog();
        //    dlg.InitialDirectory = Application.StartupPath + "\\Netlist";
        //    dlg.Filter = "net files (*.net)|*.net";

        //    if (dlg.ShowDialog() == DialogResult.Cancel)
        //        return;

        //    _componentManager.SaveAsNetworkToFile(dlg.FileName);
        //}

        //private void ViewNetworkToFile()
        //{
        //    OpenFileDialog dlg = new OpenFileDialog();
        //    dlg.InitialDirectory = Application.StartupPath + "\\Netlist";
        //    dlg.Filter = "net files (*.net)|*.net";

        //    if (dlg.ShowDialog() == DialogResult.Cancel)
        //        return;

        //    Process.Start("notepad", dlg.FileName);
        //}

        private void DrawComponents(Graphics g)
        {
            _componentManager.DrawComponents(g, null, null);

            if (_toBeAddComponent != null)
            {
                _toBeAddComponent.DrawAsSelected(g);
            }

            if (_selectedComponent != null)
            {
                _selectedComponent.DrawAsSelected(g);
            }

            if (_toBeAddConnectLine != null)
            {
                _toBeAddConnectLine.DrawAsSelected(g);
            }

            if (_selectedConnectLine != null)
            {
                _selectedConnectLine.DrawAsSelected(g);
            }
        }


        //private void TrySave(bool save_to_last)
        //{
        //    if (save_to_last)
        //    {
        //        SaveToFile(_last_filename_);
        //    }
        //    else
        //    {
        //        if (MessageBox.Show("保存现有图形?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            SaveToFile();
        //        }
        //    }
        //}

        //private void OnCloseFile()
        //{
        //    resetComponent();
        //    TrySave(true);
        //    _componentManager.Clear();
        //    toolStripStatusLabel_filepath.Text = "";
        //    Invalidate();
        //}

        //private void OnOpenFile()
        //{
        //    OnCloseFile();
        //    LoadFromFile();
        //    Invalidate();
        //}

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // OnCloseFile();
        }

        //private void ToolStripMenuItem_New_Click(object sender, EventArgs e)
        //{
        //    OnCloseFile();
        //}

        //private void ToolStripMenuItem_Load_Click(object sender, EventArgs e)
        //{
        //    OnOpenFile();
        //}

        //private void ToolStripMenuItem_Save_Click(object sender, EventArgs e)
        //{
        //    resetComponent();
        //    TrySave(false);
        //}

        //private void ToolStripMenuItem_close_Click(object sender, EventArgs e)
        //{
        //    OnCloseFile();
        //}

        //private void ToolStripMenuItem_quit_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //private void ToolStripMenuItem_view_component_Click(object sender, EventArgs e)
        //{
        //    resetComponent();
        //}

        //private void ToolStripMenuItem_edit_component_Click(object sender, EventArgs e)
        //{
        //    resetComponent();
        //}

        //private void ToolStripMenuItem_add_remove_component_Click(object sender, EventArgs e)
        //{
        //    resetComponent();
        //    ListComponentDlg dlg = new ListComponentDlg();
        //    dlg.InitComponents(_componentManager.GetTemplate(), true);
        //    dlg.ShowDialog();
        //}

        //private void ToolStripMenuItem_create_network_Click(object sender, EventArgs e)
        //{
        //    resetComponent();
        //    SaveAsNetworkToFile();
        //}

        //private void ToolStripMenuItem_view_network_Click(object sender, EventArgs e)
        //{
        //    resetComponent();
        //    ViewNetworkToFile();
        //}

        //private void toolStripButton_new_Click(object sender, EventArgs e)
        //{
        //    OnCloseFile();
        //}

        //private void toolStripButton_open_Click(object sender, EventArgs e)
        //{
        //    OnOpenFile();
        //}

        //private void toolStripButton_view_component_Click(object sender, EventArgs e)
        //{
        //    resetComponent();
        //    ListComponentDlg dlg = new ListComponentDlg();
        //    dlg.InitComponents(_componentManager.GetTemplate(), false);
        //    dlg.ShowDialog();
        //}

        //private void toolStripButton_create_newwork_Click(object sender, EventArgs e)
        //{
        //    resetComponent();
        //    SaveAsNetworkToFile();
        //}

        private void toolStripButton_mouse_op_Click(object sender, EventArgs e)
        {
            resetComponent();
        }

        private void toolStripButton_add_connect_line_Click(object sender, EventArgs e)
        {
            resetComponent();
            _toBeAddConnectLine = _componentManager.CreateConnectLine();
            state = State.add_connect_line;
            toolStripStatusLabel_operation.Text = StateMessage.add_connect_line;
        }

        private void toolStripButton_add_connect_point_Click(object sender, EventArgs e)
        {
            resetComponent();
            _toBeAddComponent = _componentManager.CreateComponentNode();
            _toBeAddComponent.SetCenterPos(this.PointToClient(Cursor.Position));
            state = State.add_component;
            toolStripStatusLabel_operation.Text = StateMessage.add_connect_node;
        }

        private void toolStripButton_add_power_Click(object sender, EventArgs e)
        {
            resetComponent();
            _toBeAddComponent = _componentManager.CreateComponentPower();
            _toBeAddComponent.SetCenterPos(this.PointToClient(Cursor.Position));
            state = State.add_component;
            toolStripStatusLabel_operation.Text = StateMessage.add_power;
        }

        private void toolStripButton_add_ground_Click(object sender, EventArgs e)
        {
            resetComponent();
            _toBeAddComponent = _componentManager.CreateComponentGround();
            _toBeAddComponent.SetCenterPos(this.PointToClient(Cursor.Position));
            state = State.add_component;
            toolStripStatusLabel_operation.Text = StateMessage.add_ground;
        }

        private void toolStripButton_add_component_Click(object sender, EventArgs e)
        {
            resetComponent();
            ListComponentDlg dlg = new ListComponentDlg();
            dlg.InitComponents(_componentManager.GetTemplate(), false);
            dlg.ShowDialog();
            if (dlg.selected_component != null)
            {
                _toBeAddComponent = _componentManager.CreateComponent(dlg.selected_component.Type);
                _toBeAddComponent.SetCenterPos(this.PointToClient(Cursor.Position));
                state = State.add_component;
                toolStripStatusLabel_operation.Text = StateMessage.add_component;
            }
        }

        private void form_Paint(object sender, PaintEventArgs e)
        {
            DrawComponents(e.Graphics);
        }

        private void form_MouseDown(object sender, MouseEventArgs e)
        {
            switch (state)
            {
                case State.mouse_op:
                    {
                        Debug.Assert(_selectedComponent == null);
                        Debug.Assert(_selectedConnectLine == null);

                        int currentSelectedLinkPointIndex;
                        if (_componentManager.HasComponentBeenSelectedInPoint(out _selectedComponent, out currentSelectedLinkPointIndex, e.Location))
                        {
                            state = State.select_component;
                            toolStripStatusLabel_operation.Text = StateMessage.select_component;
                        }
                        else if (_componentManager.HasConnectLineBeenSelectedInPoint(out _selectedConnectLine, e.Location))
                        {
                            state = State.select_connect_line;
                            toolStripStatusLabel_operation.Text = StateMessage.select_connect_line;
                        }
                    }
                    break;
                case State.add_component:
                    {
                        Debug.Assert(_toBeAddComponent != null);

                        _toBeAddComponent.SetCenterPos(e.Location);
                        _componentManager.AddComponent(_toBeAddComponent);

                        resetComponent();
                    }
                    break;
                case State.add_connect_line:
                    {
                        Debug.Assert(_toBeAddConnectLine != null);

                        BaseComponent component;
                        int currentSelectedLinkPointIndex;
                        if (_componentManager.HasComponentBeenSelectedInPoint(out component, out currentSelectedLinkPointIndex, e.Location)
                            && currentSelectedLinkPointIndex != -1)
                        {
                            if (component.Type != ComponentNode.TYPE && component.ConnectLines[currentSelectedLinkPointIndex] != null)
                            {
                                component = component.ConnectLines[currentSelectedLinkPointIndex].node;
                            }

                            if (component.Type == ComponentNode.TYPE)
                            {
                                ComponentNode node = (ComponentNode)component;
                                if (_toBeAddConnectLine.node == null)
                                {
                                    _toBeAddConnectLine.node = node;
                                }
                                else
                                {
                                    MessageBox.Show("不能连接节点和节点");
                                }
                            }
                            else
                            {
                                Component c1 = (Component)component;
                                if (_toBeAddConnectLine.component == null)
                                {
                                    _toBeAddConnectLine.component = c1;
                                    _toBeAddConnectLine.component_connection_point_index = currentSelectedLinkPointIndex;
                                }
                                else
                                {
                                    Point p1 = c1.LinkPoint(currentSelectedLinkPointIndex);
                                    Point p2 = _toBeAddConnectLine.component.LinkPoint(_toBeAddConnectLine.component_connection_point_index);

                                    ComponentNode node = _componentManager.CreateComponentNode();
                                    node.SetCenterPos(new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2));
                                    _componentManager.AddComponent(node);

                                    _toBeAddConnectLine.node = node;
                                    _componentManager.AddComponentLink(_toBeAddConnectLine);

                                    _toBeAddConnectLine = _componentManager.CreateConnectLine();
                                    _toBeAddConnectLine.node = node;
                                    _toBeAddConnectLine.component = c1;
                                    _toBeAddConnectLine.component_connection_point_index = currentSelectedLinkPointIndex;
                                    _componentManager.AddComponentLink(_toBeAddConnectLine);

                                    _toBeAddConnectLine = null;
                                }
                            }

                            if (_toBeAddConnectLine != null && _toBeAddConnectLine.node != null && _toBeAddConnectLine.component != null)
                            {
                                _componentManager.AddComponentLink(_toBeAddConnectLine);
                                _toBeAddConnectLine = null;
                            }
                        }

                        if (_toBeAddConnectLine == null)
                        {
                            resetComponent();
                        }
                    }
                    break;
                case State.select_component:
                    {
                        //Debug.Assert(false);
                    }
                    break;
                case State.select_connect_line:
                    {
                        //Debug.Assert(false);
                    }
                    break;
            }
            Invalidate();
        }

        private void form_MouseMove(object sender, MouseEventArgs e)
        {
            switch (state)
            {
                case State.mouse_op:
                    {
                    }
                    break;
                case State.add_component:
                    {
                        Debug.Assert(_toBeAddComponent != null);
                        _toBeAddComponent.SetCenterPos(e.Location);
                    }
                    break;
                case State.add_connect_line:
                    {
                        Debug.Assert(_toBeAddConnectLine != null);
                        _toBeAddConnectLine.ResetPoints(e.Location);
                    }
                    break;
                case State.select_component:
                    {
                        Debug.Assert(_selectedComponent != null);
                        _selectedComponent.SetCenterPos(e.Location);
                    }
                    break;
                case State.select_connect_line:
                    {
                        Debug.Assert(_selectedConnectLine != null);
                        _selectedConnectLine.ChangeShapeToPoint(e.Location);
                    }
                    break;
            }
            Invalidate();
        }

        private void form_MouseUp(object sender, MouseEventArgs e)
        {
            switch (state)
            {
                case State.mouse_op:
                    {
                    }
                    break;
                case State.add_component:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.add_connect_line:
                    {
                        Debug.Assert(_toBeAddConnectLine != null);
                    }
                    break;
                case State.select_component:
                    {
                        Debug.Assert(_selectedComponent != null);

                        if (e.Button == MouseButtons.Right)
                        {
                            componentPopMenu.Show(PointToScreen(e.Location));
                        }
                        else
                        {
                            _selectedComponent.SetCenterPos(e.Location);
                            _selectedComponent = null;
                            state = State.mouse_op;
                            toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                        }
                    }
                    break;
                case State.select_connect_line:
                    {
                        Debug.Assert(_selectedConnectLine != null);
                        if (e.Button == MouseButtons.Right)
                        {
                            componentPopMenu.Show(PointToScreen(e.Location));
                        }
                        else
                        {
                            _selectedConnectLine = null;
                            state = State.mouse_op;
                            toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                        }
                    }
                    break;
            }
            Invalidate();
        }

        private void ToolStripMenuItem_TurnLeft_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.mouse_op:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.add_component:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.add_connect_line:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.select_component:
                    {
                        Debug.Assert(_selectedComponent != null);
                        _selectedComponent.TurnLeft();
                        _selectedComponent = null;
                        state = State.mouse_op;
                        toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                    }
                    break;
                case State.select_connect_line:
                    {
                        Debug.Assert(_selectedConnectLine != null);
                        _selectedConnectLine = null;
                        state = State.mouse_op;
                        toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                    }
                    break;
            }
        }

        private void ToolStripMenuItem_TurnRight_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.mouse_op:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.add_component:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.add_connect_line:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.select_component:
                    {
                        Debug.Assert(_selectedComponent != null);
                        _selectedComponent.TurnRight();
                        _selectedComponent = null;
                        state = State.mouse_op;
                        toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                    }
                    break;
                case State.select_connect_line:
                    {
                        Debug.Assert(_selectedConnectLine != null);
                        _selectedConnectLine = null;
                        state = State.mouse_op;
                        toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                    }
                    break;
            }
        }

        private void ToolStripMenuItem_Delete_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.mouse_op:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.add_component:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.add_connect_line:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.select_component:
                    {
                        Debug.Assert(_selectedComponent != null);
                        _componentManager.RemoveComponent(_selectedComponent);
                        _selectedComponent = null;
                        state = State.mouse_op;
                        toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                    }
                    break;
                case State.select_connect_line:
                    {
                        Debug.Assert(_selectedConnectLine != null);
                        _componentManager.RemoveComponentLink(_selectedConnectLine);
                        _selectedConnectLine = null;
                        state = State.mouse_op;
                        toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                    }
                    break;
            }
        }

        private void ToolStripMenuItemChangeName_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.mouse_op:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.add_component:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.add_connect_line:
                    {
                        Debug.Assert(false);
                    }
                    break;
                case State.select_component:
                    {
                        Debug.Assert(_selectedComponent != null);

                        ChangeNameDlg dlg = new ChangeNameDlg();
                        dlg.name = _selectedComponent.Name;
                        dlg.ShowDialog();
                        if (!dlg.name.StartsWith("GND") && _componentManager.HasName(dlg.name))
                        {
                            MessageBox.Show(string.Format("元件名称({0})已存在", dlg.name));
                        }
                        else
                        {
                            _selectedComponent.Name = dlg.name;
                        }

                        _selectedComponent = null;
                        state = State.mouse_op;
                        toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                    }
                    break;
                case State.select_connect_line:
                    {
                        Debug.Assert(_selectedConnectLine != null);
                        _selectedConnectLine = null;
                        state = State.mouse_op;
                        toolStripStatusLabel_operation.Text = StateMessage.mouse_op;
                    }
                    break;
            }
        }

        private void toolStripButton_add_remove_component_Click(object sender, EventArgs e)
        {
            resetComponent();
            ListComponentDlg dlg = new ListComponentDlg();
            dlg.InitComponents(_componentManager.GetTemplate(), true);
            dlg.ShowDialog();
        }
    }
}