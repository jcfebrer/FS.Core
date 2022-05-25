using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FSLibrary;

namespace FSMouseKeyboardLibrary
{

    /// <summary>
    /// Standard Keyboard Shortcuts used by most applications
    /// </summary>
    public enum StandardShortcut
    {
        Copy,
        Cut,
        Paste,
        SelectAll,
        Save,
        Open,
        New,
        Close,
        Print
    }

    /// <summary>
    /// Simulate keyboard key presses
    /// </summary>
    public static class KeyboardSimulator
    {
        #region Methods

        public static void KeyDown(Keys key)
        {
            Win32API.keybd_event(ParseKey(key), 0, 0, 0);
        }

        public static void SendText(string text)
        {
            foreach (char ch in text)
            {
                bool shift = Char.IsUpper(ch);
                Keys k;

                switch (Char.ToUpper(ch))
                {
                    case 'Á':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.A;
                        break;
                    case 'É':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.E;
                        break;
                    case 'Í':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.I;
                        break;
                    case 'Ó':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.O;
                        break;
                    case 'Ú':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.U;
                        break;
                    case ' ':
                        k = Keys.Space;
                        break;
                    case '?':
                        shift = true;
                        k = Keys.OemOpenBrackets;
                        break;
                    case '¿':
                        shift = true;
                        k = Keys.Oem6;
                        break;
                    case '.':
                        k = Keys.OemPeriod;
                        break;
                    case ',':
                        k = Keys.Oemcomma;
                        break;
                    case '_':
                        shift = true;
                        k = Keys.Subtract;
                        break;
                    case '-':
                        k = Keys.Subtract;
                        break;
                    case '+':
                        k = Keys.Add;
                        break;
                    case '/':
                        k = Keys.Divide;
                        break;
                    case '*':
                        k = Keys.Multiply;
                        break;
                    default:
                        k = (Keys)Char.ToUpper(ch);
                        break;
                }    

                if (shift)
                    KeyDown(Keys.ShiftKey);

                KeyPress(k);

                if (shift)
                    KeyUp(Keys.ShiftKey);
            }
        }

        public static void KeyUp(Keys key)
        {
            Win32API.keybd_event(ParseKey(key), 0, Win32API.KEYEVENTF_KEYUP, 0);
        }

        public static void KeyPress(Keys key)
        {
            KeyDown(key);
            KeyUp(key);
        }

        public static void SimulateStandardShortcut(StandardShortcut shortcut)
        {
            switch (shortcut)
            {
                case StandardShortcut.Copy:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.C);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Cut:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.X);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Paste:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.V);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.SelectAll:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.A);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Save:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.S);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Open:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.O);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.New:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.N);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Close:
                    KeyDown(Keys.Alt);
                    KeyPress(Keys.F4);
                    KeyUp(Keys.Alt);
                    break;
                case StandardShortcut.Print:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.P);
                    KeyUp(Keys.Control);
                    break;
            }
        }

        static byte ParseKey(Keys key)
        {

            // Alt, Shift, and Control need to be changed for API function to work with them
            switch (key)
            {
                case Keys.Alt:
                    return (byte)18;
                case Keys.Control:
                    return (byte)17;
                case Keys.Shift:
                    return (byte)16;
                default:
                    return (byte)key;
            }

        } 

        #endregion

    }

}
