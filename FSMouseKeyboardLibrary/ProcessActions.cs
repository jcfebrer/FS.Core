using FSException;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace FSMouseKeyboardLibrary
{
    public class ProcessActions
    {
        public delegate void ActionEntryEventHandler(MouseActionEntry action, int position);
        public static event ActionEntryEventHandler OnEntryProcess;

        public static void Do(MouseActionsEntry actions, bool repeat, int repeatCount)
        {
            if (!repeat)
            {
                repeatCount = 1;
            }

            for (int f = 0; f <= repeatCount; f++)
            {
                ProcessActions.Do(actions);
            }
        }
        public static void Do(MouseActionsEntry actions)
        {
            int f = 0;
            foreach (MouseActionEntry action in actions)
            {
                OnEntryProcess(action, f++);

                Thread.Sleep(action.Interval);

                switch (action.Type)
                {
                    case MouseActionEntry.EventType.MouseMove:
                        {
                            MouseSimulator.X = action.X;
                            MouseSimulator.Y = action.Y;
                        }
                        break;
                    case MouseActionEntry.EventType.MouseDown:
                        {
                            MouseSimulator.X = action.X;
                            MouseSimulator.Y = action.Y;
                            MouseSimulator.Click(action.Button);
                        }
                        break;
                    case MouseActionEntry.EventType.MouseUp:
                        {
                            MouseSimulator.MouseUp(action.Button);
                        }
                        break;
                    case MouseActionEntry.EventType.KeyDown:
                        {
                            KeyboardSimulator.KeyDown(action.KeyCode);
                        }
                        break;
                    case MouseActionEntry.EventType.KeyUp:
                        {
                            KeyboardSimulator.KeyUp(action.KeyCode);
                        }
                        break;
                    case MouseActionEntry.EventType.KeyPress:
                        {
                            KeyboardSimulator.KeyPress(action.KeyCode);
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        public static MouseActionsEntry OpenFileXml(string file)
        {
            //Get data from XML file
            XmlSerializer ser = new XmlSerializer(typeof(MouseActionsEntry));
            using (FileStream fs = System.IO.File.Open(file, FileMode.Open))
            {
                try
                {
                    MouseActionsEntry entries = (MouseActionsEntry)ser.Deserialize(fs);
                    return entries;
                }
                catch (ExceptionUtil ex)
                {
                    MessageBox.Show(ex.Message, "Abrir XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        public static bool SaveFileXml(string file, MouseActionsEntry actionsEntry)
        {
            XmlSerializer ser = new XmlSerializer(typeof(MouseActionsEntry));

            using (XmlWriter writer = XmlWriter.Create(file))
            {
                try
                {
                    ser.Serialize(writer, actionsEntry);
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Abrir XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
