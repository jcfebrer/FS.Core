using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSAutomatizeWeb
{
    class Hook
    {
        #region Mouse Keyboard Library

        public static int lastTimeRecorded = 0;
        public static FSMouseKeyboardLibrary.MouseHook mouseHook = new FSMouseKeyboardLibrary.MouseHook();
        public static FSMouseKeyboardLibrary.KeyboardHook keyboardHook = new FSMouseKeyboardLibrary.KeyboardHook();

        #endregion

        #region Mouse Keyboard Init Event

        public static void InitMouseKeyBoardEvent()
        {
            //Record
            mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);
            mouseHook.MouseWheel += new MouseEventHandler(mouseHook_MouseWheel);

            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
        }

        #endregion
        #region Mouse Keyboard Library Event

        static void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {
            Program.mainFrm.tbxCode.AppendText("MouseMove(" + e.X + "," + e.Y + ",true, " + (Environment.TickCount - lastTimeRecorded) + ");" + Environment.NewLine);
            lastTimeRecorded = Environment.TickCount;
        }

        static void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {
            Program.mainFrm.tbxCode.AppendText("MouseDown('" + e.Button.ToString() + "', " + (Environment.TickCount - lastTimeRecorded) + ");" + Environment.NewLine);
            lastTimeRecorded = Environment.TickCount;
        }

        static void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {
            Program.mainFrm.tbxCode.AppendText("MouseUp('" + e.Button.ToString() + "', " + (Environment.TickCount - lastTimeRecorded) + ");" + Environment.NewLine);
            lastTimeRecorded = Environment.TickCount;
        }

        static void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            Program.mainFrm.tbxCode.AppendText("KeyDown('" + e.KeyCode + "', " + (Environment.TickCount - lastTimeRecorded) + ");" + Environment.NewLine);
            lastTimeRecorded = Environment.TickCount;
        }

        static void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            Program.mainFrm.tbxCode.AppendText("KeyUp('" + e.KeyCode + "', " + (Environment.TickCount - lastTimeRecorded) + ");" + Environment.NewLine);
            lastTimeRecorded = Environment.TickCount;
        }

        static void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        {
            Program.mainFrm.tbxCode.AppendText("MouseWheel(" + e.Delta + ", " + (Environment.TickCount - lastTimeRecorded) + ");" + Environment.NewLine);
            lastTimeRecorded = Environment.TickCount;
        }

        #endregion
    }
}
