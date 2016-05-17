using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CircuitDesign
{
    public interface IButtonComponentNotify
    {
        void OnClickComponentButton (ButtonComponent button);
        void OnClickConnectButton(ButtonComponent button);
    }

    public partial class ButtonComponent : UserControl
    {
        private bool _isConnectButton = false;
        private ConnectLine _connectLine = null;
        private BaseComponent _component = null;
        private IButtonComponentNotify _parent = null;
        private bool _isSelected = false;

        public ButtonComponent(IButtonComponentNotify parent, Rectangle rect)
        {
            InitializeComponent();

            _isConnectButton = true;

            _parent = parent;
            SetBounds(rect.X, rect.Y, rect.Width, rect.Height);
            
            _connectLine = new ConnectLine();
            _connectLine.turnPts.Add(new Point(5, rect.Height / 2));
            _connectLine.turnPts.Add(new Point(rect.Width - 5, rect.Height / 2));
        }

        public ButtonComponent(IButtonComponentNotify parent, BaseComponent component, Rectangle rect)
        {
            InitializeComponent();

            _parent = parent;
            SetBounds(rect.X, rect.Y, rect.Width, rect.Height);

            _component = component.Clone();
            _component.ShowName = false;
            _component.Position = DesignTools.OffsetRectangle(_component.Position,
                DesignTools.SubPoint(new Point(rect.Width / 2, rect.Height / 2), DesignTools.GetRectangleCenterPt(_component.Position)));
        }

        public bool Selected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
            }
        }

        public BaseComponent GetComponent()
        {
            BaseComponent component = _component.Clone();
            Point centerPt = DesignTools.GetRectangleCenterPt (component.Position);
            component.Offset(new Point (-centerPt.X, -centerPt.Y));
            return component;
        }

        private void UserControlButton_Paint(object sender, PaintEventArgs e)
        {
            if (!_isConnectButton)
            {
                if (_isSelected)
                {
                    _component.DrawAsSelect(e.Graphics);
                }
                else
                {
                    _component.Draw(e.Graphics);
                }
            }
            else
            {
                if (_isSelected)
                {
                    _connectLine.DrawAsSelect(e.Graphics);
                }
                else
                {
                    _connectLine.Draw(e.Graphics);
                }
            }
        }

        private void UserControlButton_Click(object sender, EventArgs e)
        {
            if (! _isConnectButton)
            {
                _parent.OnClickComponentButton(this);
            }
            else
            {
                _parent.OnClickConnectButton(this);
            }
            Selected = true;
        }
    }
}
