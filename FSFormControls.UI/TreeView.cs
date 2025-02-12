using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace FSFormControls.UI
{
    public class TreeView : System.Windows.Controls.TreeView
    {
        public TreeView()
        {
            this.PreviewMouseWheel += TreeView_PreviewMouseWheel;
        }

        /// <summary>
        /// Permitimos mover el ScrollView donde esta alojado el TreeView con la rueda del ratón.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (sender is TreeView && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
