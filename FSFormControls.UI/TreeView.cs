using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace FSFormControls.UI
{
    public class TreeView : System.Windows.Controls.TreeView
    {
        public bool OpenFirstTreeViewItem {get; set; } = false;
        public bool AllFoldersOpen { get; set; } = false;

        public TreeView()
        {
            this.PreviewMouseWheel += TreeView_PreviewMouseWheel;
            this.Loaded += TreeView_Loaded; ;
        }

        private void TreeView_Loaded(object sender, RoutedEventArgs e)
        {
            if (AllFoldersOpen)
            {
                // Expandir todos los elementos del TreeView al cargar el control.
                foreach (var item in this.Items)
                {
                    TreeViewItem treeViewItem = this.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                    
                    if (treeViewItem != null)
                        treeViewItem.IsExpanded = true;
                }
            }

            if (OpenFirstTreeViewItem)
            {
                // Hacer que el primer elemento del TreeView se expanda al cargar el control.
                object firstDataItem = this.Items[0];
                TreeViewItem firstTreeViewItem = this.ItemContainerGenerator.ContainerFromItem(firstDataItem) as TreeViewItem;

                if (firstTreeViewItem != null)
                    firstTreeViewItem.IsExpanded = true;
            }
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
