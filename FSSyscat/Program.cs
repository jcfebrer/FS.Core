using FSDatabase;
using FSException;
using FSSyscat.Classes;
using FSTrace;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace FSSyscat
{
	/// <summary>
	/// 
	/// </summary>
	static class Program
	{
        public static SynchronizationContext SynchronizationContext { get; private set; }
        public static Hook hook = new Hook();
        public static FrmMain frmMain;
        public static Dictionary<string, object> soundConfig;
        public static bool KeyboardSound = false;

        static Thread threadVNC;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main()
		{
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            frmMain = new FrmMain();

            // Guardamos el thread de sincronización
            EventHandler mainFormHandleCreated = null;
            mainFormHandleCreated = (sender, e) =>
            {
                Program.SynchronizationContext = SynchronizationContext.Current;
                frmMain.HandleCreated -= mainFormHandleCreated;
                Inicializar();
            };
            frmMain.HandleCreated += mainFormHandleCreated;


            // Show the system tray icon.					
            using (ProcessIcon pi = new ProcessIcon())
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["ShowSystray"]))
                {
                    pi.Display();
                }
                else
                {
                    Log.TraceInfo("No mostramos Systray Icon");
                }

                Application.Run(frmMain);
            }
        }

        private static void Inicializar()
        {
            //Configuramos el destino de LogUtil
            Log.OnMessageLog += LogUtil_OnMessageLog;

            //registramos la aplicación en el registro
            Autorun.Install();

            hook.Start(Convert.ToBoolean(ConfigurationManager.AppSettings["HookKeys"]));

            //Inicializamos el chat
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["ConnectUHP"]))
            {
                Chat.StartUhp();
            }
            else
            {
                Log.TraceInfo("No iniciamos aplicación UHP");
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["StartVNC"]))
            {
                StartVNC();
            }
            else
            {
                Log.TraceInfo("No iniciamos aplicación VNC");
            }

            //cargamos la configuración para el sonido de las teclas
            LoadKeysSound();
            //Leemos la configuración de activación de los sonidos de las teclas
            KeyboardSound = Convert.ToBoolean(ConfigurationManager.AppSettings["SoundKeys"]);


            //Iniciamos el formulario principal
            frmMain.Start();
        }

        private static void LoadKeysSound()
        {
            FSDatabase.Json jason = new FSDatabase.Json();
            string soundFileConfig = Application.StartupPath + "\\" + ConfigurationManager.AppSettings["SoundPath"];
            soundFileConfig = (soundFileConfig.EndsWith("\\") ? soundFileConfig : soundFileConfig + "\\") + "config.json";
            soundConfig = Json.Load(soundFileConfig);
        }

        private static void LogUtil_OnMessageLog(object sender, Log.LogMessage e)
        {
            frmMain.Log(e.Message, e.TraceLevel);

            if(e.TraceLevel == System.Diagnostics.TraceLevel.Error)
            {
                FSMail.SendMail.SendErrorMail(e.Message);
            }
        }


        static void StartVNC()
        {
            Log.TraceInfo("Inicio de aplicación VNC...");
            threadVNC = new Thread(StartVnc);

            threadVNC.IsBackground = true;
            threadVNC.Start();
        }

        public static void Stop()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["StartVNC"]))
            {
                //el hilo se cierra automáticamente al cerrar la aplicación. No es necesario Abort.
                //threadVNC.Abort();
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["StartUHP"]))
            {
                //Cerramos el cliente UHP
                if (Chat.clientUHP != null)
                {
                    if (Chat.clientUHP.Connected)
                        Chat.clientUHP.Disconnect();

                    //Cerramos UPNP
                    Chat.clientUHP.ClearUpUPnP();
                }

                //Invoke(new MethodInvoker(() =>
                //{
                //    for (int c = 0; c < Chat.FrmChats.Count; c++)
                //        Chat.FrmChats[c].Close();
                //}));
            }

            hook.Close();
        }

        static void StartVnc()
        {
            try
            {
                VncServerUhp.VncServer vnc;

                if (Chat.Connected)
                {
                    vnc = new VncServerUhp.VncServer(Crypt.Decrypt(ConfigurationManager.AppSettings["VNCPassword"]), System.Environment.MachineName, Chat.clientUHP);

                    vnc.Start();

                    Log.TraceInfo("Servidor VNC: " + vnc.Name + " inicado.");
                }
            }
            catch (System.Exception ex)
            {
                Log.TraceError(ex);
            }
        }
    }
}