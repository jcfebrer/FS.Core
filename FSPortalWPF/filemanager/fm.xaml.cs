using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FSPortalWPF.filemanager
{
    /// <summary>
    /// Interaction logic for fm.xaml
    /// </summary>

    public partial class fm : Window, System.Windows.Navigation.IProvideCustomContentState
    {

        public fm()
        {
            InitializeComponent();
        }

        void uploadButton_Click(object sender, RoutedEventArgs e)
        {

            // Configure OpenFileDialog to open images
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

            // Open file, if user clicked "Open" button on OpenFileDialog
            if (dlg.ShowDialog() == true)
            {

                // If existing image, put into back navigation history
                //if (this.viewImage.Source != null)
                //{
                //    ImageCustomContentState iccs = new ImageCustomContentState(this.viewImage.Source, (string)this.nameLabel.Content);
                //    this.NavigationService.AddBackEntry(iccs);
                //}

                // Show new image
                using (System.IO.Stream stream = dlg.OpenFile())
                {
                    BitmapDecoder bitmapDecoder = BitmapDecoder.Create(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                    this.viewImage.Source = bitmapDecoder.Frames[0];
                    this.nameLabel.Content = dlg.SafeFileName;
                }
            }
        }

        #region IProvideCustomContentState Members

        public System.Windows.Navigation.CustomContentState GetContentState()
        {
            // Add to history that is in the opposite direction to which we are navigating
            return new ImageCustomContentState(this.viewImage.Source, (string)this.nameLabel.Content);
        }

        #endregion

    }
}