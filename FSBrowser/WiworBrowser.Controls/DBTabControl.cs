#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace WiworBrowser.Controls
{
    [ToolboxBitmap(typeof (TabControl))]
    public class DBTabControl : TabControl
    {
        protected Dictionary<Button, TabPage> CloseButtonCollection = new Dictionary<Button, TabPage>();
        private bool _ShowCloseButtonOnTabs = true;


        [Browsable(true), DefaultValue(true), Category("Behavior"),
         Description("Indicates whether a close button should be shown on each TabPage")]
        public bool ShowCloseButtonOnTabs
        {
            get { return _ShowCloseButtonOnTabs; }

            set
            {
                _ShowCloseButtonOnTabs = value;

                foreach (Button btn in CloseButtonCollection.Keys)
                {
                    btn.Visible = _ShowCloseButtonOnTabs;
                }

                RePositionCloseButtons();
            }
        }

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            RePositionCloseButtons();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            TabPage tp = (TabPage) e.Control;
            if (TabPages.IndexOf(tp) != -1)
            {
                Rectangle rect = GetTabRect(TabPages.IndexOf(tp));
                Button btn = AddCloseButton(tp);
                btn.Size = new Size(rect.Height - 1, rect.Height - 1);
                btn.Location = new Point(rect.X + rect.Width - rect.Height - 1, rect.Y + 1);
                SetParent(btn.Handle, Handle);
                btn.Click += OnCloseButtonClick;
                CloseButtonCollection.Add(btn, tp);
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            Button btn = CloseButtonOfTabPage((TabPage) e.Control);
            btn.Click -= OnCloseButtonClick;
            CloseButtonCollection.Remove(btn);
            SetParent(btn.Handle, IntPtr.Zero);
            btn.Dispose();
            base.OnControlRemoved(e);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            RePositionCloseButtons();
        }

        public event CancelEventHandler CloseButtonClick;

        protected virtual void OnCloseButtonClick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Button btn = (Button) sender;
                TabPage tp = CloseButtonCollection[btn];

                CancelEventArgs ee = new CancelEventArgs();

                if (CloseButtonClick != null)
                {
                    CloseButtonClick(sender, ee);
                }

                if (!ee.Cancel)
                {
                    TabPages.Remove(tp);
                    RePositionCloseButtons();
                }
            }
        }

        protected virtual Button AddCloseButton(TabPage tp)
        {
            Button closeButton = new Button();

            Button _with1 = closeButton;
            _with1.Text = "X";
            _with1.FlatStyle = FlatStyle.Flat;
            _with1.BackColor = Color.FromKnownColor(KnownColor.ButtonFace);
            _with1.ForeColor = Color.White;
            _with1.Font = new Font("Microsoft Sans Serif", 6, FontStyle.Bold);

            return closeButton;
        }

        public void RePositionCloseButtons()
        {
            foreach (KeyValuePair<Button, TabPage> item in CloseButtonCollection)
            {
                RePositionCloseButtons(item.Value);
            }
        }

        private void RePositionCloseButtons(TabPage tp)
        {
            Button btn = CloseButtonOfTabPage(tp);

            tp.Text = tp.Text.Trim() + "      ";

            if (btn != null)
            {
                int tpIndex = TabPages.IndexOf(tp);

                if (tpIndex >= 0)
                {
                    Rectangle rect = GetTabRect(tpIndex);

                    if (ReferenceEquals(SelectedTab, tp))
                    {
                        btn.BackColor = Color.LightGreen;
                        btn.Size = new Size(rect.Height - 1, rect.Height - 1);
                        btn.Location = new Point(rect.X + rect.Width - rect.Height, rect.Y + 1);
                    }
                    else
                    {
                        btn.BackColor = Color.FromKnownColor(KnownColor.ButtonFace);
                        btn.Size = new Size(rect.Height - 3, rect.Height - 3);
                        btn.Location = new Point(rect.X + rect.Width - rect.Height - 1, rect.Y + 1);
                    }
                    btn.Visible = ShowCloseButtonOnTabs;
                    btn.BringToFront();
                }
            }
        }


        protected Button CloseButtonOfTabPage(TabPage tp)
        {
            Button first = null;
            foreach (KeyValuePair<Button, TabPage> item in CloseButtonCollection)
            {
                if (first == null) first = item.Key;
                if (item.Value == tp) return item.Key;
            }
            return first;
        }
    }
}