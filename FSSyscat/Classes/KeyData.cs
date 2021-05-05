using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FSSyscat.Classes
{
    #region "KeyData"
    public class KeyData
    {
        public string key;
        public string process;
        public string app;
        public string image;
        public string imageGuid;
        public DateTime time;
        public Image img;
        public Boolean sendByEmail;

        public KeyData()
        {
        }

        public KeyData(string key, string process, string app, DateTime time, string image, string imageGuid, Image img, Boolean sendByEmail)
        {
            this.key = key;
            this.process = process;
            this.app = app;
            this.time = time;
            this.image = image;
            this.imageGuid = imageGuid;
            this.img = img;
            this.sendByEmail = sendByEmail;
        }
    }
    #endregion
}
