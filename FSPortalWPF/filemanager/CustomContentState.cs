using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FSPortalWPF.filemanager
{
    [Serializable]
    public class ImageCustomContentState : CustomContentState
    {

        ImageSource imageSource;
        string filename;

        public ImageCustomContentState(ImageSource imageSource, string filename)
        {
            this.imageSource = imageSource;
            this.filename = filename;
        }

        public override string JournalEntryName
        {
            get { return this.filename; }
        }

        public override void Replay(NavigationService navigationService, NavigationMode mode)
        {
            fm homePage = (fm)navigationService.Content;
            homePage.viewImage.Source = this.imageSource;
            homePage.nameLabel.Content = this.filename;
        }
    }
}
