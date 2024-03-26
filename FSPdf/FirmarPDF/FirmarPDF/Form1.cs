using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace FirmarPDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Acá tomamos el primer certificado de mi repositorio personal
                    //En producción debería elegir el certificado a utilizar

                    X509Store objStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    objStore.Open(OpenFlags.ReadOnly);
                    X509Certificate2 objCert = null;
                    if (objStore.Certificates != null)
                        foreach (X509Certificate2 objCertTemp in objStore.Certificates)
                            if (objCertTemp.HasPrivateKey)
                            {
                                objCert = objCertTemp;
                                break;
                            }

                    if (objCert == null)
                        MessageBox.Show("No posee certificados personal con clave privada");
                    else
                    {
                        PDF.SignHashed(
                            openFileDialog1.FileName,
                            saveFileDialog1.FileName,
                            objCert,
                            "Prueba",
                            "Argentina",
                            true);

                        MessageBox.Show("Proceso finalizado");
                    }
                }
            }
        }
    }
}