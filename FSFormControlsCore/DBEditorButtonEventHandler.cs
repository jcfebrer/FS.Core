using System;
using System.Drawing;

namespace FSFormControlsCore
{
    public delegate void DBEditorButtonEventHandler(object sender, DBEditorButtonEventArgs e);

    public class DBEditorButtonEventArgs : EventArgs
    {
        public DBButton Button;
        public Element Element;

        public DBEditorButtonEventArgs(DBButton button)
        {
        }

        public DBEditorButtonEventArgs()
        {
        }
    }

    public class Element
    {
        public Rectangle Rect;
    }
}