using System;
using System.Diagnostics;
using System.Windows.Forms;
using FSSyscat.Properties;
using System.Drawing;
using System.Management;
using System.ServiceProcess;

namespace FSSyscat
{
	/// <summary>
	/// 
	/// </summary>
	class ContextMenus
	{
		/// <summary>
		/// Is the About box displayed?
		/// </summary>
		bool isAboutLoaded = false;

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns>ContextMenuStrip</returns>
		public ContextMenuStrip Create()
		{
			// Add the default menu options.
			ContextMenuStrip menu = new ContextMenuStrip();
			ToolStripMenuItem item;
            ToolStripSeparator sep = new ToolStripSeparator();

            // Mostrar syscat.
            item = new ToolStripMenuItem();
            item.Text = "Mostrar aplicación";
            item.Click += new EventHandler(Mostrar_Click);
            item.Image = Resources.FSSyscat.ToBitmap();
            menu.Items.Add(item);

            //// Windows Explorer.
            //item = new ToolStripMenuItem();
            //item.Text = "Explorer";
            //item.Click += new EventHandler(Explorer_Click);
            //item.Image = Resources.Explorer;
            //menu.Items.Add(item);

            // Separator.
            menu.Items.Add(sep);


            // Detener servicio FSVncService
            item = new ToolStripMenuItem();
            item.Text = "Detener servicio";
            item.Click += new EventHandler(StopService_Click);
            item.Image = Resources.stop;
            menu.Items.Add(item);

            // Iniciar servicio FSVncService
            item = new ToolStripMenuItem();
            item.Text = "Iniciar servicio";
            item.Click += new EventHandler(PlayService_Click);
            item.Image = Resources.play;
            menu.Items.Add(item);

            // Separator.
			menu.Items.Add(sep);

            // Activar sonidos teclas
            item = new ToolStripMenuItem();
            item.Text = "Sonidos teclas";
            item.Click += new EventHandler(SonidosTeclas_Click);
            item.Checked = !Program.KeyboardSound;
            menu.Items.Add(item);


            // Separator.
            menu.Items.Add(sep);

            // About.
            item = new ToolStripMenuItem();
            item.Text = "About";
            item.Click += new EventHandler(About_Click);
            item.Image = Resources.About;
            menu.Items.Add(item);

            // Exit.
            item = new ToolStripMenuItem();
			item.Text = "Exit";
			item.Click += new System.EventHandler(Exit_Click);
			item.Image = Resources.salir;
			menu.Items.Add(item);

			return menu;
		}

		/// <summary>
		/// Handles the Click event of the Explorer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Explorer_Click(object sender, EventArgs e)
		{
			Process.Start("explorer", null);
		}

        void Mostrar_Click(object sender, EventArgs e)
        {
            Program.frmMain.ShowApp();
        }

        void SonidosTeclas_Click(object sender, EventArgs e)
        {
            Program.KeyboardSound = !Program.KeyboardSound;
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.Checked = Program.KeyboardSound;
        }
        /// <summary>
        /// Handles the Click event of the About control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void About_Click(object sender, EventArgs e)
		{
			if (!isAboutLoaded)
			{
				isAboutLoaded = true;
				new AboutBox().ShowDialog();
				isAboutLoaded = false;
			}
		}


        /// <summary>
		/// Handles the Click event of the StopService control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void StopService_Click(object sender, EventArgs e)
        {
            try
            {
                FSLibrary.Services.Stop("FSVncService");
                MessageBox.Show("Servicio detenido.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
		/// Handles the Click event of the PlayService control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void PlayService_Click(object sender, EventArgs e)
        {
            try
            {
                FSLibrary.Services.Start("FSVncService");
                MessageBox.Show("Servicio iniciado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Processes a menu item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Exit_Click(object sender, EventArgs e)
		{
            // Quit without further ado.
            Program.Stop();
			Application.Exit();
		}
	}
}