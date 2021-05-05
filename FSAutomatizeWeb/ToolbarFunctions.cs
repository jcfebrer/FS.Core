using Gecko;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSAutomatizeWeb
{
    class ToolbarFunctions
    {
        #region Toolbar Event Functions

        public static void RunCode()
        {
            if (Program.mainFrm.toolStripRunning.Text.Equals(Language.Resource.Run))
            {
                Program.mainFrm.wbMainBrowser.IsStop = false;
                Program.mainFrm.btnRecord.Enabled = false;

                Program.mainFrm.toolStripStatus.Text = Language.Resource.RunBegin;
                Program.mainFrm.toolStripRunning.Text = Language.Resource.Stop;
                Program.mainFrm.btnRunTwo.Text = Language.Resource.Stop;
                Program.mainFrm.wbMainBrowser.InvokeScript("UnAbort");
                if (!string.IsNullOrEmpty(Program.mainFrm.tbxCode.Text))
                {
                    Program.mainFrm.wbMainBrowser.ExecuteJSCode(Program.mainFrm.tbxCode.Text);
                }

                Program.mainFrm.toolStripRunning.Text = Language.Resource.Run;
                Program.mainFrm.btnRunTwo.Text = Language.Resource.Run;
                Program.mainFrm.toolStripStatus.Text = Language.Resource.RunComplete;
                Program.mainFrm.btnRecord.Enabled = true;
            }
            else
            {
                Program.mainFrm.wbMainBrowser.IsStop = true;
                Program.mainFrm.toolStripRunning.Text = Language.Resource.Run;
                Program.mainFrm.btnRunTwo.Text = Language.Resource.Run;
                Program.mainFrm.toolStripStatus.Text = "";

                Program.mainFrm.wbMainBrowser.InvokeScript("Abort");
            }
        }

        public static void RecordEvents()
        {
            Hook.lastTimeRecorded = Environment.TickCount;

            if (Program.mainFrm.btnRecord.Text.Equals(Language.Resource.Record))
            {
                Program.mainFrm.tbxCode.Text = "";
                Program.mainFrm.toolStripRunning.Enabled = false;
                Program.mainFrm.btnRunTwo.Enabled = false;
                Hook.mouseHook.Start();
                Hook.keyboardHook.Start();
                Program.mainFrm.btnRecord.Text = Language.Resource.Stop;
            }
            else
            {
                Program.mainFrm.toolStripRunning.Enabled = true;
                Program.mainFrm.btnRunTwo.Enabled = true;
                Hook.mouseHook.Stop();
                Hook.keyboardHook.Stop();
                Program.mainFrm.btnRecord.Text = Language.Resource.Record;
            }
        }

        public static void Back()
        {
            //Back WebBrowser
            Program.mainFrm.wbMainBrowser.BackWebBrowser();
        }

        public static void Next()
        {
            //Next WebBrowser
            Program.mainFrm.wbMainBrowser.NextWebBrowser();
        }

        public static void Reload()
        {
            //Reload WebBrowser
            Program.mainFrm.wbMainBrowser.ReloadWebBrowser();
        }

        public static void Stop()
        {
            Program.mainFrm.wbMainBrowser.StopWebBrowser();
        }

        public static void tabnew()
        {
            TabPage tab = new TabPage(Language.Resource.NewTab);
            Program.mainFrm.tabMain.Controls.Add(tab);
            Program.mainFrm.currentTab = tab;
            Program.mainFrm.tabMain.SelectedTab = Program.mainFrm.currentTab;
        }

        public static void tabclose()
        {
            if (Program.mainFrm.tabMain.TabPages.Count > 0)
            {
                if (Program.mainFrm.tabMain.SelectedTab.Controls.Count > 0)
                {
                    Program.mainFrm.tabMain.SelectedTab.Controls[0].Dispose();
                }
                Program.mainFrm.tabMain.SelectedTab.Dispose();

                if (Program.mainFrm.tabMain.TabPages.Count > 1)
                {
                    Program.mainFrm.tabMain.SelectTab(Program.mainFrm.tabMain.TabPages.Count - 1);
                }
                Program.mainFrm.currentTab = Program.mainFrm.tabMain.SelectedTab;
            }
            else
            {
                Program.mainFrm.currentTab = null;
            }
        }

        public static void tabcloseall()
        {

            while (Program.mainFrm.tabMain.TabPages.Count > 0)
            {
                try
                {
                    Application.DoEvents();
                    if (Program.mainFrm.tabMain.TabPages[0].Controls.Count > 0)
                    {
                        Program.mainFrm.tabMain.TabPages[0].Controls[0].Dispose();
                    }
                    Program.mainFrm.tabMain.TabPages[0].Dispose();
                }
                catch { }
            }
            Program.mainFrm.currentTab = null;
        }

        public static void ShowHideDeveloperTools(object sender)
        {
            Program.mainFrm.developerToolsToolStripMenuItem_Click(sender, null);
            TooglePanel();
            if (Program.mainFrm.developerToolsToolStripMenuItem.Checked)
            {
                if (Program.mainFrm.tbxCode != null)
                    Program.mainFrm.tbxCode.Focus();
            }
        }

        public static void DetectionClick()
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (Program.mainFrm.btnDetection.Text == Language.Resource.ShowDetection)
            {
                Program.mainFrm.btnDetection.Text = Language.Resource.HideDetection;

                if (wb != null)
                {
                    GeckoDocument doc = wb.Document;
                }
            }
            else
            {
                Program.mainFrm.btnDetection.Text = Language.Resource.ShowDetection;

                if (wb != null)
                {
                    GeckoDocument doc = wb.Document;
                    Program.mainFrm.toolStripStatus.Text = "";
                }
            }
        }

        public static void ChangeLanguage(string culture)
        {
            //Language.Resource.Culture.Name
            switch (culture)
            {
                case "en-US":
                    {
                        Language.Resource.Culture = CultureInfo.CreateSpecificCulture("en-US");
                        Program.mainFrm.toolStripDropDownButton1.Text = Language.Resource.English;

                        System.IO.Stream file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("FSAutomatizeWeb.Resources.usa.png");
                        Program.mainFrm.toolStripDropDownButton1.Image = Image.FromStream(file);

                        break;
                    }
                case "es-ES":
                    {
                        Language.Resource.Culture = CultureInfo.CreateSpecificCulture("es-ES");
                        Program.mainFrm.toolStripDropDownButton1.Text = Language.Resource.Spanish;

                        System.IO.Stream file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("FSAutomatizeWeb.Resources.spain.gif");
                        Program.mainFrm.toolStripDropDownButton1.Image = Image.FromStream(file);

                        break;
                    }
            }
            

            Program.mainFrm.InitLanguage();
        }

        public static void TooglePanel()
        {
            Program.mainFrm.splitContainer1.Panel1.SuspendLayout();
            Program.mainFrm.splitContainer1.Panel2.SuspendLayout();

            if (Program.mainFrm.developerToolsToolStripMenuItem.Checked)
            {
                Program.mainFrm.btnShowHideDeveloperTool.Text = Language.Resource.HideDeveloperTools;
                Program.mainFrm.splitContainer1.Panel2Collapsed = false;
                Program.mainFrm.splitContainer1.Panel2.Show();
            }
            else
            {
                Program.mainFrm.btnShowHideDeveloperTool.Text = Language.Resource.ShowDeveloperTools;
                Program.mainFrm.splitContainer1.Panel2Collapsed = true;
                Program.mainFrm.splitContainer1.Panel2.Hide();
            }

            Program.mainFrm.splitContainer1.Panel1.ResumeLayout(false);
            Program.mainFrm.splitContainer1.Panel1.PerformLayout();
            Program.mainFrm.splitContainer1.Panel2.ResumeLayout(false);
            Program.mainFrm.splitContainer1.Panel2.PerformLayout();
        }

        #endregion
    }
}
