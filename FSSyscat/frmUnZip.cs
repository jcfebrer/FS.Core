using System;
using System.Threading;
using System.Windows.Forms;

namespace FSSyscat
{
    public partial class frmUnZip : Form
    {
        private bool Running = false;
        public frmUnZip()
        {
            InitializeComponent();

            toolStripStatusLabel1.Text = "";
        }

        private void cmdUnzip_Click(object sender, EventArgs e)
        {
            Running = true;
            toolStripStatusLabel1.Text = "Descomprimir fichero: " + txtSourceFile.Text;

            Thread UnzipThread = new Thread(new ThreadStart(delegate
            {
                FSCompress.Gz.OnProgress += Gz_OnProgress;
                FSCompress.Gz.DecompressToDirectory(txtSourceFile.Text, txtTargetFolder.Text);

                Program.SynchronizationContext.Post((state) =>
                {
                    if (!Program.frmMain.IsDisposed)
                    {
                        toolStripStatusLabel1.Text = "Fichero descomprimido en " + txtTargetFolder.Text;
                    }
                }, null);

                Running = false;
            }));

            UnzipThread.IsBackground = true;
            UnzipThread.Start();
        }

        private void Gz_OnProgress(object sender, string e)
        {
            Program.SynchronizationContext.Post((state) =>
            {
                if (!Program.frmMain.IsDisposed)
                {
                    toolStripStatusLabel1.Text = "Descomprimiendo fichero: " + e;
                }
            }, null);
        }

        private void cmdTargetFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog solicitarFolder = new FolderBrowserDialog();

            if (solicitarFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtTargetFolder.Text = solicitarFolder.SelectedPath;
            }
            solicitarFolder.Dispose();
        }

        private void cmdSourceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog solicitarFichero = new OpenFileDialog();
            solicitarFichero.Filter = "Archivos FSZ|*.fsz";

            if (solicitarFichero.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSourceFile.Text = solicitarFichero.FileName;
            }
            solicitarFichero.Dispose();
        }

        private void frmUnZip_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Running)
            {
                MessageBox.Show("Tarea sin finalizar. Espere por favor.", "Cerrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }
    }
}
