using System;
using System.Configuration;
using System.Windows.Forms;

namespace FSAutomatizeWeb
{
    static class Program
    {
        public static frmMain mainFrm;
        public static string userAgent = ConfigurationManager.AppSettings["UserAgent"];
        //public static frmMain mainFrm
        //{
        //    get { return (frmMain)Application.OpenForms["frmMain"]; }
        //}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainFrm = new frmMain();

            Application.Run(mainFrm);
        }
    }
}
