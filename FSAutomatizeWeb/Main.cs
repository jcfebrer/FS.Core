using System.Windows.Forms;
using System;
using System.IO;
using System.Globalization;
using FSAutomatizeWeb.UI;
using Gecko;
using FSFormControls;
using System.Configuration;

namespace FSAutomatizeWeb
{
    public partial class frmMain : Form
    {
        #region static variable

        public WebBrowserFunctions wbMainBrowser = null;
        public TabPage currentTab = null;


        private string LastScriptFile = "";
        private string LastTemplateFile = "";

        public string Version = Application.ProductVersion;

        #endregion      

        #region main

        public frmMain()
        {
            InitializeComponent();
            
            Hook.InitMouseKeyBoardEvent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit();
        }

        private void KeyEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 123:
                    ToolbarFunctions.TooglePanel();
                    if (developerToolsToolStripMenuItem.Checked) tbxCode.Focus();
                    break;
                case 116:
                    toolStripRunning_Click(this, null);
                    break;
                default:
                    break;
            }            
        }

        #endregion

        #region Menu Events

        private void newScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewScript();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenScript();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveScript();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsScript();
        }

        private void hideWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MinimizeWindow();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit();
        }

        public void developerToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeveloperToolsClick();
        }


        private void scrollToViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrollViewClick();
        }

        private void colorElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorElementClick();
        }

        private void cfgShowImages_Click(object sender, EventArgs e)
        {
            ShowImageClick();
        }

        private void notifyIconAutomation_Click(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void userAgentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FSFormControls.frmInputBox frmInput = new frmInputBox(Language.Resource.UserAgentString, Language.Resource.UserAgent, ConfigurationManager.AppSettings["UserAgent"], false);
            frmInput.ShowDialog();

            Program.userAgent = frmInput.InputText;
        }

        #endregion

        #region ToolBar Events

        private void btnRunTwo_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.RunCode();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.RecordEvents();
        }

        private void toolStripBack_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.Back();
        }

        private void toolStripNext_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.Next();
        }

        private void toolStripReload_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.Reload();
        }

        private void btnNewTab_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.tabnew();
        }

        private void btnCloseTab_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.tabclose();
        }

        private void btnCloseAllTab_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.tabcloseall();
        }

        private void btnShowHideDeveloperTool_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.ShowHideDeveloperTools(sender);
        }

        private void btnDetection_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.DetectionClick();
        }

        private void sddLanguage_Click(object sender, EventArgs e)
        {
            string idioma = ((ToolStripMenuItem)sender).Text;
            if(idioma == "Spain")
                ToolbarFunctions.ChangeLanguage("es-ES");
            else
                ToolbarFunctions.ChangeLanguage("en-US");
        }

        #endregion

        #region TextBox and Go Events

        private void btnGo_Click(object sender, EventArgs e)
        {
            GoClick();
        }

        private void tbxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.btnGo_Click(sender, e);
            }
        }

        #endregion

        #region Main Browser Events

        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            TabSelected();
        }

        #endregion

        #region Developer Tools Events

        #region Script Code

        private void toolStripRunning_Click(object sender, EventArgs e)
        {
            ToolbarFunctions.RunCode();
        }

        private void btnNewScript_Click(object sender, EventArgs e)
        {
            NewScript();
        }

        private void btnOpenScript_Click(object sender, EventArgs e)
        {
            OpenScript();
        }

        private void btnSaveScript_Click(object sender, EventArgs e)
        {
            SaveScript();
        }

        private void btnSaveAsScript_Click(object sender, EventArgs e)
        {
            SaveAsScript();
        }

        private void btnScriptClear_Click(object sender, EventArgs e)
        {
            ClearScript();
        }

        private void tbxCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyData != (Keys.RButton | Keys.ShiftKey | Keys.Alt | Keys.Control) && e.KeyValue == 83)
            {
                btnSaveScript_Click(this, null);
            }
            else if (e.Control && e.KeyData != (Keys.RButton | Keys.ShiftKey | Keys.Alt | Keys.Control) && e.KeyValue == 79)
            {
                btnOpenScript_Click(this, null);
            }
            else if (e.Control && e.KeyData != (Keys.RButton | Keys.ShiftKey | Keys.Alt | Keys.Control) && e.KeyValue == 78)
            {
                btnNewScript_Click(this, null);
            }
        }

        #endregion

        #region Template Code

        private void btnNewTemplate_Click(object sender, EventArgs e)
        {
            NewTemplate();
        }

        private void btnOpenTemplate_Click(object sender, EventArgs e)
        {
            OpenTemplate();
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            SaveTemplate();
        }

        private void btnSaveAsTemplate_Click(object sender, EventArgs e)
        {
            SaveAsTemplate();
        }

        private void btnTemplateClear_Click(object sender, EventArgs e)
        {
            ClearTemplate();
        }

        private void tbxTemplate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyData != (Keys.RButton | Keys.ShiftKey | Keys.Alt | Keys.Control) && e.KeyValue == 83)
            {
                btnSaveTemplate_Click(this, null);
            }
            else if (e.Control && e.KeyData != (Keys.RButton | Keys.ShiftKey | Keys.Alt | Keys.Control) && e.KeyValue == 79)
            {
                btnOpenTemplate_Click(this, null);
            }
            else if (e.Control && e.KeyData != (Keys.RButton | Keys.ShiftKey | Keys.Alt | Keys.Control) && e.KeyValue == 78)
            {
                btnNewTemplate_Click(this, null);
            }
        }

        #endregion

        #region AutoBot

        private void tbxAsk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13)
                return;
            string user_input = tbxAsk.Text;
        }

        #endregion

        #region Download

        private void toolNewDownload_Click(object sender, EventArgs e)
        {
            NewDownload download = new NewDownload();
            download.ShowDialog();
        }

        private void toolStart_Click(object sender, EventArgs e)
        {
            downloadList1.StartSelections();
        }

        private void toolPause_Click(object sender, EventArgs e)
        {
            downloadList1.Pause();
        }

        private void toolPauseAll_Click(object sender, EventArgs e)
        {
            downloadList1.PauseAll();
        }

        private void toolRemove_Click(object sender, EventArgs e)
        {
            downloadList1.RemoveSelections();
        }

        private void toolRemoveCompleted_Click(object sender, EventArgs e)
        {
            downloadList1.RemoveCompleted();
        }

        #endregion

        #endregion

        #region Event Funtions

        #region Menu Event Functions

        public void NewScript()
        {
            tbxCode.Text = "";
            LastScriptFile = "";
            toolStripStatus.Text = Language.Resource.NewScript;
        }

        public void OpenScript()
        {
            openFileDialog1.Filter = Language.Resource.UnicodeScriptFile;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = Language.Resource.OpenScriptTitle;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string code = File.ReadAllText(openFileDialog1.FileName);
                    tbxCode.Text = "";
                    if (!string.IsNullOrEmpty(code))
                    {
                        tbxCode.Text = code;
                    }

                    LastScriptFile = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    Program.mainFrm.wbMainBrowser.log(string.Format(Language.Resource.OpenFileError, ex.Message));
                }
            }
        }

        public void SaveScript()
        {
            toolStripStatus.Text = Language.Resource.Saving;
            if (string.IsNullOrEmpty(LastScriptFile))
            {
                saveFileDialog1.Filter = Language.Resource.UnicodeScriptFile;
                saveFileDialog1.Title = Language.Resource.SaveScriptTitle;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName, false))
                        {
                            file.Write(tbxCode.Text);
                        }

                        toolStripStatus.Text = Language.Resource.Saved;
                        LastScriptFile = saveFileDialog1.FileName;
                    }
                    catch (Exception ex)
                    {
                        Program.mainFrm.wbMainBrowser.log(string.Format(Language.Resource.SaveFileError, ex.Message));
                    }
                }
                else
                {
                    toolStripStatus.Text = "";
                }
            }
            else
            {
                FileInfo fileInfo = new FileInfo(LastScriptFile);
                if (fileInfo.IsReadOnly)
                {
                    Program.mainFrm.wbMainBrowser.log(Language.Resource.ReadOnlyFile);
                }
                else
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(LastScriptFile, false))
                    {
                        file.Write(tbxCode.Text);
                    }
                    toolStripStatus.Text = Language.Resource.Saved;
                }
            }
        }

        public void SaveAsScript()
        {
            LastScriptFile = "";
            btnSaveScript_Click(this, null);
        }

        public void MinimizeWindow()
        {
            notifyIconAutomation.Visible = true;
            notifyIconAutomation.ShowBalloonTip(500);

            this.Hide();
        }

        public void exit()
        {
            this.Dispose();
            Application.Exit();
        }

        public void DeveloperToolsClick()
        {
            if (developerToolsToolStripMenuItem.Checked == false)
            {
                developerToolsToolStripMenuItem.Checked = true;
            }
            else
            {
                developerToolsToolStripMenuItem.Checked = false;
            }
            ToolbarFunctions.TooglePanel();
        }


        public void ScrollViewClick()
        {
            if (scrollToViewToolStripMenuItem.Checked == false)
            {
                scrollToViewToolStripMenuItem.Checked = true;
            }
            else
            {
                scrollToViewToolStripMenuItem.Checked = false;
            }
        }

        public void ColorElementClick()
        {
            if (colorElementToolStripMenuItem.Checked == false)
            {
                colorElementToolStripMenuItem.Checked = true;
            }
            else
            {
                colorElementToolStripMenuItem.Checked = false;
            }
        }

        public void ShowImageClick()
        {
            if (cfgShowImages.Checked == false)
            {   
                cfgShowImages.Checked = true;
            }
            else
            {
                cfgShowImages.Checked = false;
            }
        }


        public void ShowWindow()
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
        }
        
        #endregion

        #region TextBox and Go Event Functions

        public void GoClick()
        {
            wbMainBrowser.go(tbxAddress.Text);
        }


        private void TabSelected()
        {
            Program.mainFrm.wbMainBrowser.TabSelectedWebBrowser();
        }

        #endregion

        #region Script Code Event Funtions

        public void ClearScript()
        {
            tbxCode.Text = "";
        }

        #endregion

        #region Template Event Functions

        public void OpenTemplate()
        {
            openFileDialog1.Filter = Language.Resource.UnicodeTemplateFile;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = Language.Resource.OpenTemplateTitle;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string code = File.ReadAllText(openFileDialog1.FileName);

                    if (!string.IsNullOrEmpty(code))
                    {
                        tbxTemplate.Text = code;
                    }

                    LastTemplateFile = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.Resource.OpenFileError + ex.Message, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SaveTemplate()
        {
            toolStripStatus.Text = Language.Resource.Saving;
            if (string.IsNullOrEmpty(LastTemplateFile))
            {
                saveFileDialog1.Filter = Language.Resource.UnicodeTemplateFile;
                saveFileDialog1.Title = Language.Resource.SaveTemplateTitle;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string result = "";
                        result = tbxTemplate.Text;

                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName, false))
                        {
                            file.Write(result);
                        }
                        toolStripStatus.Text = Language.Resource.Saved;
                        LastTemplateFile = saveFileDialog1.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Language.Resource.SaveFileError + ex.Message, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    toolStripStatus.Text = "";
                }
            }
            else
            {
                FileInfo fileInfo = new FileInfo(LastTemplateFile);
                if (fileInfo.IsReadOnly)
                {
                    MessageBox.Show(Language.Resource.ReadOnlyFile, Language.Resource.Message);
                }
                else
                {
                    string result = "";
                    result = tbxTemplate.Text;

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(LastTemplateFile, false))
                    {
                        file.Write(result);
                    }

                    toolStripStatus.Text = Language.Resource.Saved;
                }
            }
        }

        public void NewTemplate()
        {
            tbxTemplate.Text = "";
            LastTemplateFile = "";
            toolStripStatus.Text = Language.Resource.NewTemplate;
        }

        public void SaveAsTemplate()
        {
            LastTemplateFile = "";
            btnSaveTemplate_Click(this, null);
        }

        public void ClearTemplate()
        {
            tbxTemplate.Text = "";
        }

        #endregion

        #endregion

        #region Delegate

        delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl != null)
            {
                if (oControl.InvokeRequired)
                {
                    SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                    oControl.Invoke(d, new object[] { oControl, propName, propValue });
                }
                else
                {
                    Type t = oControl.GetType();
                    System.Reflection.PropertyInfo[] props = t.GetProperties();
                    foreach (System.Reflection.PropertyInfo p in props)
                    {
                        if (p.Name.ToUpper() == propName.ToUpper())
                        {
                            p.SetValue(oControl, propValue, null);
                        }
                    }
                }
            }
        }

        #endregion

        #region System Function

        private void FormLoad()
        {
            wbMainBrowser = new WebBrowserFunctions();

            ToolbarFunctions.TooglePanel();

            this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);
            
            Language.Resource.Culture = CultureInfo.CreateSpecificCulture("es-ES");
            InitLanguage();
            

            EnableDownloadButton(false);
            downloadList1.SelectionChange += downloadList1_SelectionChange;

            GoClick();
        }

        void downloadList1_SelectionChange(object sender, EventArgs e)
        {
            var isSelected = (sender as DownloadList).IsSelected;
            EnableDownloadButton(isSelected);
        }

        void EnableDownloadButton(bool isSelected)
        {
            toolRemove.Enabled = isSelected;
            toolStart.Enabled = isSelected;
            toolPause.Enabled = isSelected;
            toolPauseAll.Enabled = isSelected;
            toolRemoveCompleted.Enabled = isSelected;
        }

        #endregion

        #region Init Language

        public void InitLanguage()
        {
            fileToolStripMenuItem.Text = Language.Resource.File;
            exitToolStripMenuItem.Text = Language.Resource.Exit; ;
            toolStripReload.Text = Language.Resource.Reload;
            btnNewTab.Text = Language.Resource.NewTab;
            btnCloseTab.Text = Language.Resource.CloseTab;
            btnCloseAllTab.Text = Language.Resource.CloseAllTab;
            toolsToolStripMenuItem.Text = Language.Resource.Tools;
            configToolStripMenuItem.Text = Language.Resource.Configs;
            developerToolsToolStripMenuItem.Text = Language.Resource.DeveloperTools;
            scrollToViewToolStripMenuItem.Text = Language.Resource.ScrollToView;
            colorElementToolStripMenuItem.Text = Language.Resource.ColorElement;
            cfgShowImages.Text = Language.Resource.ShowImages;
            btnGo.Text = Language.Resource.Go;
            userAgentToolStripMenuItem.Text = Language.Resource.UserAgent;

            btnShowHideDeveloperTool.Text = Language.Resource.ShowDeveloperTools;
            btnDetection.Text = Language.Resource.ShowDetection;

            tabScript.Text = Language.Resource.Code;
            tabTemplate.Text = Language.Resource.Template;
            tabPreview.Text = Language.Resource.Preview;
            tabAutoBot.Text = Language.Resource.Autobot;

            toolStripRunning.Text = Language.Resource.Run;
            btnRunTwo.Text = Language.Resource.Run;

            btnNewScript.Text = Language.Resource.New;
            btnOpenScript.Text = Language.Resource.Open;
            btnSaveScript.Text = Language.Resource.Save;
            btnSaveAsScript.Text = Language.Resource.SaveAs;
            btnScriptClear.Text = Language.Resource.Clear;

            newScriptToolStripMenuItem.Text = Language.Resource.New;
            openToolStripMenuItem.Text = Language.Resource.Open;
            saveToolStripMenuItem.Text = Language.Resource.Save;
            saveAsToolStripMenuItem.Text = Language.Resource.SaveAs;

            btnNewTemplate.Text = Language.Resource.New;
            btnOpenTemplate.Text = Language.Resource.Open;
            btnSaveTemplate.Text = Language.Resource.Save;
            btnSaveAsTemplate.Text = Language.Resource.SaveAs;
            btnTemplateClear.Text = Language.Resource.Clear;
            btnRecord.Text = Language.Resource.Record;

            hideWindowToolStripMenuItem.Text = Language.Resource.HideWindow;

            notifyIconAutomation.BalloonTipText = Language.Resource.BallonTipText;
            notifyIconAutomation.BalloonTipTitle = Language.Resource.BallonTipText;

            toolStripStatus.Text = "";

            toolStripDropDownButton1.Text = Language.Resource.English;

            tabDownload.Text = Language.Resource.Download;
            toolNewDownload.Text = Language.Resource.NewDownload;
            toolStart.Text = Language.Resource.Start;
            toolPause.Text = Language.Resource.Pause;
            toolPauseAll.Text = Language.Resource.PauseAll;
            toolRemove.Text = Language.Resource.Delete;
            toolRemoveCompleted.Text = Language.Resource.DeleteCompleted;

            downloadList1.InitLanguage();

            this.Text = Language.Resource.FSAutomatizeWeb + " - " + Language.Resource.Version + ": " + Version;

            Functions.InitContextMenu();
        }

        #endregion

        #region Treeview events
        private void tvHtml_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ShowNode((DBTreeViewNode)e.Node);
        }

        public void ShowNode(DBTreeViewNode node)
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            string xpath = node.GetXPath();
            GeckoHtmlElement geckoElement = Functions.GetElementByXpath(wb.Document, xpath);
            propertyGrid1.SelectedObject = geckoElement;
            //Text = xpath;
        }

        private void tvHtml_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if (e.Node != null)
            {
                string xpath = ((DBTreeViewNode)e.Node).GetXPath();
                //Program.mainFrm.toolStripStatus.Text = xpath + " (" + ((DBTreeViewNode)e.Node).Value + ")";
                e.Node.ToolTipText = xpath;
                //toolTip1.SetToolTip(tvHtml, xpath);
            }
        }

        #endregion
    }
}
