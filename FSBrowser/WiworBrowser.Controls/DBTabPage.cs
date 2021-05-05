#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace WiworBrowser.Controls
{
    [ToolboxBitmap(typeof (TabPage))]
    public class DBTabPage : TabPage
    {
        protected override void OnTextChanged(EventArgs e)
        {
            if (base.Parent is DBTabControl)
            {
                ((DBTabControl) (base.Parent)).RePositionCloseButtons();
            }
            base.OnTextChanged(e);
        }
    }
}