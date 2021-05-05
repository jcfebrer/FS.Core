#region

using System;
using System.Windows.Forms;
using WiworBrowser.UI.Controls;

#endregion

namespace WiworBrowser
{
    internal static class Program
    {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Common.Configuration.DeleteTempFiles();

            Common.Configuration.LoadSettings();
            Common.Configuration.LoadFavorites();
            Common.Configuration.LoadHistory();

            if(Common.Configuration.CheckServerVersion() != Common.AssemblyFunctions.AssemblyVersion)
            {
                if( MessageBox.Show("Dispone de una nueva versión de la aplicación en el servidor.\n¿Desea descargarla?","Nueva versión",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AutoUpdate autoUpdate = new AutoUpdate(true);
                    autoUpdate.ShowDialog();
                    if(autoUpdate.Restart)
                    {
                        Application.Restart();
                    }
                    else
                    {
                        Application.Run(new WiworForm());
                    }
                }
                else
                {
                    Application.Run(new WiworForm());
                }
            }
            else
            {
                Application.Run(new WiworForm());
            }
        }
    }
}