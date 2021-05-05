using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace WiworBrowser.UI.Controls
{
    public partial class AutoUpdate : Form
    {
        private string fileName;
        private string fileVersion;

        public bool Restart { get; set; }
        public bool AutomaticStart { get; set; }

        public AutoUpdate(bool automaticStart)
        {
            InitializeComponent();

            AutomaticStart = automaticStart;
            if(automaticStart)
            {
                cmdCheckVersion.Visible = false;
                CheckVersion();
            }
        }

        private void cmdCheckVersion_Click(object sender, EventArgs e)
        {
            if (cmdCheckVersion.Text == "Cerrar")
            {
                this.Close();
            }
            else
            {
                CheckVersion();
            }
        }

        private void CheckVersion()
        {
            if(Common.Configuration.RestartPending())
            {
                lblEstado.Text = "Debes reiniciar la aplicación para que lo cambios se apliquen.";
                return;
            }

            lblEstado.Text = "Comprobando versión ...";
             
            WebClient wc = new WebClient();

            lblEstado.Text = "Descargando fichero de versión ...";

            fileVersion = Common.Configuration.CheckServerVersion();
            fileName = "wiwor_" + fileVersion + ".zip";

            lblEstado.Text = string.Format("Versión existente en el servidor: {0}", fileVersion);

            if (Common.AssemblyFunctions.AssemblyVersion != fileVersion)
            {
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);

                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);

                wc.DownloadFileAsync(new Uri(Common.Configuration.Settings.UpdatePage + "/AutoUpdate/" + fileVersion + "/" + fileName), fileName);
            }
            else
            {
                lblEstado.Text = "La versión actual ya esta correctamente actualizada.";
            }

            if (AutomaticStart)
            {
                cmdCheckVersion.Visible = true;
            }

            cmdCheckVersion.Text = "Cerrar";
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (new System.IO.FileInfo(fileName).Exists)
            {
                try
                {
                    Common.Configuration.ExtractZipFile(fileName);
                    lblEstado.Text = "Aplicación actualizada correctamente.";
                    if (AutomaticStart)
                    {
                        cmdCheckVersion.Visible = true;
                    }
                    else
                    {
                        lblEstado.Text += " Debes salir y volver a entrar de la aplicación para que los cambios se apliquen.";
                    }

                    Restart = true;
                }
                catch (Exception ex)
                {
                    lblEstado.Text = "Problemas con la extracción del fichero. Error: " + ex.Message;
                } 
            }
            else
            {
                lblEstado.Text =
                    "Problemas con la actualización. Pruebe de nuevo más tarde, o utilize internet para descargarte la última versión.";
            }

            cmdCheckVersion.Text = "Cerrar";
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.ProgressPercentage;
        }
    }
}
