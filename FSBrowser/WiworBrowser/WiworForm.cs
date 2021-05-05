using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using WiworBrowser.UI.Controls;
using WiworBrowser.Objects;
using System.Drawing;


namespace WiworBrowser
{
    /// <summary>
    /// Basado en navegador de Claudia Goga: http://www.codeproject.com/KB/cs/WBrowser.aspx?msg=3532498
    /// </summary>
    public partial class WiworForm : Form
    {
        private readonly List<String> urls = new List<String>();
        
        public WiworForm()
        {
            InitializeComponent();

            treeControl1.HistoryMouseClick += treeControl1_HistoryMouseClick;
            treeControl1.FavoritesMouseClick += treeControl1_FavoritesMouseClick;

            dbTabControl1.CloseButtonClick += new System.ComponentModel.CancelEventHandler(dbTabControl1_CloseButtonClick);

            Common.Configuration.DeleteTempFiles();
        }

        void dbTabControl1_CloseButtonClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(dbTabControl1.TabCount == 1)
            {
                GetCurrentBrowser().Navigate(Common.Configuration.Settings.HomePage);
                e.Cancel = true;
            }
            
        }

        private void treeControl1_FavoritesMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            GetCurrentBrowser().Navigate(e.Node.ToolTipText);
        }

        private void treeControl1_HistoryMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            GetCurrentBrowser().Navigate(e.Node.Text);
        }

        private void img_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Navigate(Common.Configuration.Settings.HomePage);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (GetCurrentBrowser().Url != null)
            {
                string url = GetCurrentBrowser().Url.ToString();
                SubmitUrl suburl = new SubmitUrl(url);
                DialogResult dr = suburl.ShowDialog();
                //if (
                //    MessageBox.Show("¿Marcar es página como no recomendable para menores de edad?", "No recomendable",
                //                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                //    //guardar como no recomendable la página seleccionada
                //    MessageBox.Show("Página: " + getCurrentBrowser().Url.DnsSafeHost + ", marcada como no recomendable.");
                //}
            }
        }

        #region Form load/Closing/Closed

//visible items
        private void SetVisibility()
        {
            menuBar.Visible = Common.Configuration.Settings.MenuBar;
            adrBar.Visible = Common.Configuration.Settings.AdressBar;
            linkBar.Visible = Common.Configuration.Settings.LinkBar;
            splitContainer1.Panel1Collapsed = Common.Configuration.Settings.FavoritesPanel;
            splashScreenToolStripMenuItem.Checked = Common.Configuration.Settings.SplashScreen;

            linksBarToolStripMenuItem.Checked = linkBar.Visible;
            menuBarToolStripMenuItem.Checked = menuBar.Visible;
            commandBarToolStripMenuItem.Checked = adrBar.Visible;
        }

        // form load
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Done";
            //comboBox1.SelectedItem = comboBox1.Items[0];
            SetVisibility();
            AddNewTab();
            if (splashScreenToolStripMenuItem.Checked)
                (new About(true)).Show();

            treeControl1.ShowFavHist();
        }

        //form closing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Common.Configuration.Settings.SettingsPassword != "")
            {
                if (Common.Configuration.Settings.PasswordOnExit)
                {
                    Password sp = new Password();
                    if (sp.ShowDialog() == DialogResult.OK)
                    {
                        if (sp.txtPassword.Text.ToLower() != Common.Configuration.Settings.SettingsPassword.ToLower())
                        {
                            MessageBox.Show("Clave de acceso incorrecta.", "Password", MessageBoxButtons.OK,
                                            MessageBoxIcon.Stop);
                            e.Cancel = true;
                            return;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            if (dbTabControl1.TabCount > 1)
            {
                DialogResult dlg_res = (new Close()).ShowDialog();

                if (dlg_res == DialogResult.No)
                {
                    e.Cancel = true;
                    closeTab();
                }
                else if (dlg_res == DialogResult.Cancel) e.Cancel = true;
                else Application.ExitThread();
            }
        }

        //form closed
        private void WiworBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Common.Configuration.SaveSettings();
            Common.Configuration.SaveFavorites();
            Common.Configuration.SaveHistorys();
        }

        #endregion

        #region TABURI

        /*TAB-uri*/

        //addNewTab method
        private void AddNewTab()
        {
            WiworBrowser.Controls.DBTabPage tpage = new WiworBrowser.Controls.DBTabPage();
            tpage.BorderStyle = BorderStyle.Fixed3D;

            int pos = dbTabControl1.TabCount - 1;
            if (pos < 0) pos = 0;

            dbTabControl1.TabPages.Insert(pos, tpage);

            tpage.Controls.Add(CreateWebBrowser());
            dbTabControl1.SelectTab(tpage);
        }

        private WiworBrowser.Controls.DBWebBrowser CreateWebBrowser()
        {
            WiworBrowser.Controls.DBWebBrowser browser = new WiworBrowser.Controls.DBWebBrowser();

            browser.Navigate(Common.Configuration.Settings.HomePage);

            browser.Dock = DockStyle.Fill;
            browser.ProgressChanged += Form1_ProgressChanged;
            browser.DocumentCompleted += Form1_DocumentCompleted;
            browser.Navigating += Form1_Navigating;
            browser.CanGoBackChanged += browser_CanGoBackChanged;
            browser.CanGoForwardChanged += browser_CanGoForwardChanged;
            browser.StatusTextChanged += browser_StatusTextChanged;

            browser.EmulDocMode(WiworBrowser.Controls.DBWebBrowser.EmulMode.Ie7);

            //no permitimos nuevas ventanas
            browser.AllowNewWindow = Common.Configuration.Settings.AllowNewWindow;
            //browser.AllowNavigation = false;
            //no permitimos "drop" en el control
            browser.AllowWebBrowserDrop = false;
            //no mostramos errores de scripts
            browser.ScriptErrorsSuppressed = Common.Configuration.Settings.ScriptErrorsSuppressed;
            //no permitimos la descarga de ficheros
            browser.AllowFileDownload = Common.Configuration.Settings.AllowFileDownload;
            //no permitimos IFRAMES
            browser.AllowIFrames = Common.Configuration.Settings.AllowIFrames;
            //eliminamos banner del flash (YouTube)
            browser.RemoveFlashBanner = false;
            //no permitimos la navegación por páginas para adultos
            browser.CheckIsChildValidPage = Common.Configuration.Settings.CheckIsChildValidPage;
            //Quitamos el menu contextual
            browser.RemoveContextMenu = Common.Configuration.Settings.RemoveContextMenu;
            //no visualizamos imagenes con links externos
            browser.AllowImageExternalLinks = Common.Configuration.Settings.AllowImageExternalLinks;
            //ocultamos las imgenes de desnudos (en pruebas)
            browser.NudeDetect = false;

            //no permitimos atajos del navegador como ctrl+N
            browser.WebBrowserShortcutsEnabled = Common.Configuration.Settings.WebBrowserShortcutsEnabled;

            browser.IsWebBrowserContextMenuEnabled = Common.Configuration.Settings.IsWebBrowserContextMenuEnabled;

            browser.CheckBadWords = Common.Configuration.Settings.CheckContent;

            browser.BlackList = Common.Configuration.Settings.BlackList;

            if (Common.Configuration.Settings.CheckContent)
                if (Common.Configuration.Settings.BadWords != null && Common.Configuration.Settings.BadWords.Count > 1)
                    browser.BadWords = Common.Configuration.Settings.BadWords.ToArray();

            return browser;
        }

        //DocumentCompleted
        private void Form1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WiworBrowser.Controls.DBWebBrowser currentBrowser = GetCurrentBrowser();


            toolStripStatusLabel1.Text = "Completo";
            String text = "Página en blanco";

            if (!currentBrowser.Url.ToString().Equals("about:blank"))
            {
                text = currentBrowser.Url.Host;
            }

            adrBarTextBox.Text = currentBrowser.Url.ToString();
            dbTabControl1.SelectedTab.Text = text;


            //img.Image = favicon(currentBrowser.Url.ToString(), "net.png");

            if (!urls.Contains(currentBrowser.Url.Host))
                urls.Add(currentBrowser.Url.Host);

            if (!currentBrowser.Url.ToString().Equals("about:blank") &&
                (currentBrowser.StatusText.Equals("Finalizado") || currentBrowser.StatusText.Equals("Done")))
            {
                Bitmap image = currentBrowser.CaptureImage(true);

                DateTime date = DateTime.Now;
                image.Save("history/hist_" + date.Ticks.ToString() + ".jpg");
                treeControl1.AddHistory(currentBrowser.Url, date);
            }
        }

        //ProgressChanged    
        private void Form1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            int value = (int) e.CurrentProgress;
            int max = (int) e.MaximumProgress;
            if (value > max) value = max;
            if (value < 0) value = 0;
            toolStripProgressBar1.Maximum = (int) e.MaximumProgress;
            toolStripProgressBar1.Value = value;
        }

        //Navigating
        private void Form1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            WiworBrowser.Controls.DBWebBrowser currentBrowser = GetCurrentBrowser();

            //System.Net.WebRequest request = System.Net.WebRequest.Create(e.Url);

            //// we need only header part of http response
            //request.Method = "HEAD";

            //System.Net.WebResponse response = request.GetResponse();

            //if (response != null)
            //{
            //    // only text/html, text/xml, text/plain are allowed... extend as required
            //    if (!response.ContentType.StartsWith("text/"))
            //    {
            //        e.Cancel = true;
            //        MessageBox.Show(Resources.ACCESS_NOT_ALLOWED);
            //    }
            //}

            toolStripStatusLabel1.Text = currentBrowser.StatusText;
        }


        //closeTab method
        private void closeTab()
        {
            if (dbTabControl1.TabCount > 1)
            {
                dbTabControl1.TabPages.RemoveAt(dbTabControl1.SelectedIndex);
            }
            else
            {
                GetCurrentBrowser().Navigate(Common.Configuration.Settings.HomePage);
            }
        }

        //selected index changed
        private void browserTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (browserTabControl.SelectedIndex == browserTabControl.TabPages.Count - 1)
            //{
            //    addNewTab();
            //}
            //else
            //{
            if (GetCurrentBrowser().Url != null)
                adrBarTextBox.Text = GetCurrentBrowser().Url.ToString();
            else adrBarTextBox.Text = "about:blank";

            if (GetCurrentBrowser().CanGoBack) toolStripButton1.Enabled = true;
            else toolStripButton1.Enabled = false;

            if (GetCurrentBrowser().CanGoForward) toolStripButton2.Enabled = true;
            else toolStripButton2.Enabled = false;
            //}
        }

        /* tab context menu */

        private void closeTabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            closeTab();
        }

        private void duplicateTabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (GetCurrentBrowser().Url != null)
            {
                Uri dup_url = GetCurrentBrowser().Url;
                AddNewTab();
                GetCurrentBrowser().Url = dup_url;
            }
            else AddNewTab();
        }

        #endregion

        #region     TOOL CONTEXT MENU

        /* TOOL CONTEXT MENU*/

        //link bar
        private void linksBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linkBar.Visible = !linkBar.Visible;
            linksBarToolStripMenuItem.Checked = linkBar.Visible;
            Common.Configuration.Settings.LinkBar = linkBar.Visible;
        }

        //menu bar
        private void menuBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuBar.Visible = !menuBar.Visible;
            menuBarToolStripMenuItem.Checked = menuBar.Visible;
            Common.Configuration.Settings.MenuBar = menuBar.Visible;
        }

        //address bar
        private void commandBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adrBar.Visible = !adrBar.Visible;
            commandBarToolStripMenuItem.Checked = adrBar.Visible;
            Common.Configuration.Settings.AdressBar = adrBar.Visible;
        }

        #endregion

        #region ADDRESS BAR

        /*ADDRESS BAR*/

        private WiworBrowser.Controls.DBWebBrowser GetCurrentBrowser()
        {
            WiworBrowser.Controls.DBWebBrowser browser;

            if (dbTabControl1.SelectedTab.Controls.Count == 0)
            {
                browser = CreateWebBrowser();
                dbTabControl1.SelectedTab.Controls.Add(browser);
            }
            else
            {
                browser = (WiworBrowser.Controls.DBWebBrowser)dbTabControl1.SelectedTab.Controls[0];
            }

            ////esto ocurre cuando se cierra una página con js
            //if(browser.ReadyState == WebBrowserReadyState.Uninitialized && browser.Document == null)
            //{
            //    dbTabControl1.SelectedTab.Controls.Clear();
            //    browser = CreateWebBrowser();
            //    dbTabControl1.SelectedTab.Controls.Add(browser);
            //}

            return browser;
        }

        //ENTER
        private void adrBarTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetCurrentBrowser().Navigate(adrBarTextBox.Text);
            }
        }

        //select all from adr bar
        private void adrBarTextBox_Click(object sender, EventArgs e)
        {
            adrBarTextBox.SelectAll();
        }

        //show urls

        private void showUrl()
        {
            int i = 0;
            foreach (History his in Common.Configuration.Historys)
            {
                if (i > Common.Configuration.Settings.DropDown) break;

                adrBarTextBox.Items.Add(his.Url);
                i++;
            }
        }

        private void adrBarTextBox_DropDown(object sender, EventArgs e)
        {
            adrBarTextBox.Items.Clear();
            showUrl();
        }

        //navigate on selected url 
        private void adrBarTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCurrentBrowser().Navigate(adrBarTextBox.SelectedItem.ToString());
        }

        //canGoForwardChanged
        private void browser_CanGoForwardChanged(object sender, EventArgs e)
        {
            toolStripButton2.Enabled = !toolStripButton2.Enabled;
        }

        //StatusTextChanged
        private void browser_StatusTextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = GetCurrentBrowser().StatusText;
        }

        //canGoBackChanged
        private void browser_CanGoBackChanged(object sender, EventArgs e)
        {
            toolStripButton1.Enabled = !toolStripButton1.Enabled;
        }

        //back  
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().GoBack();
        }

        //forward
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().GoForward();
        }

        //go
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Navigate(adrBarTextBox.Text);
        }


        //refresh
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Refresh();
        }

        //stop
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Stop();
        }

        //favorits
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            Common.Configuration.Settings.FavoritesPanel = splitContainer1.Panel1Collapsed;
        }

        //add to favorits
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            SaveFavorite();
        }

        //search
        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    if (googleSearch.Checked == true)
            //        getCurrentBrowser().Navigate("http://google.com/search?q=" + searchTextBox.Text);
            //    else
            //        getCurrentBrowser().Navigate("http://search.live.com/results.aspx?q="+searchTextBox.Text);

            if (e.KeyCode == Keys.Enter)
                GetCurrentBrowser().Navigate("http://google.com/search?q=" + searchTextBox.Text);
        }

        //private void googleSearch_Click(object sender, EventArgs e)
        //{
        //    liveSearch.Checked =!googleSearch.Checked;
        //}

        //private void liveSearch_Click(object sender, EventArgs e)
        //{
        //    googleSearch.Checked = !liveSearch.Checked;
        //}

        #endregion

        #region LINKS BAR

        /*LINKS BAR*/

        //favorits button
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            Common.Configuration.Settings.FavoritesPanel = splitContainer1.Panel1Collapsed;
        }

        ////add to favorits bar button
        //private void toolStripButton9_Click(object sender, EventArgs e)
        //{
        //    if (getCurrentBrowser().Url != null)
        //        treeControl1.addLink(getCurrentBrowser().Url.ToString(), getCurrentBrowser().Url.ToString());
        //}

        //showLinks on link bar
        //private void showLinks()
        //{
        //    if (File.Exists(TreeControl.linksXml))
        //    {
        //        XmlDocument myXml = new XmlDocument();
        //        myXml.Load(TreeControl.linksXml);
        //        XmlElement root = myXml.DocumentElement;
        //        foreach (XmlElement el in root.ChildNodes)
        //        {
        //            ToolStripButton b =
        //                new ToolStripButton(el.InnerText, treeControl1.getFavicon(el.GetAttribute("url")), items_Click, el.GetAttribute("url"));

        //            b.ToolTipText = el.GetAttribute("url");
        //            b.MouseUp += new MouseEventHandler(b_MouseUp);
        //            linkBar.Items.Add(b);
        //        }
        //    }
        //}
        //click link button
        //private void items_Click(object sender, EventArgs e)
        //{
        //    ToolStripButton b = (ToolStripButton)sender;
        //    getCurrentBrowser().Navigate(b.ToolTipText);
        //}
        //show context menu on button
        //private void b_MouseUp(object sender, MouseEventArgs e)
        //{
        //    ToolStripButton b = (ToolStripButton)sender;
        //    adress = b.ToolTipText;
        //    name = b.Text;

        //    if (e.Button == MouseButtons.Right)
        //        linkContextMenu.Show(MousePosition);
        //}
//visible change
        //private void linkBar_VisibleChanged(object sender, EventArgs e)
        //{
        //    if (linkBar.Visible == true) showLinks();
        //    else while (linkBar.Items.Count > 3) linkBar.Items[linkBar.Items.Count - 1].Dispose();
        //}

        #endregion

        #region FAVORITS

        /*FAVORITES*/

        //add to favorits
        private void addToFavoritsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFavorite();
        }

        private void SaveFavorite()
        {
            if (GetCurrentBrowser().Url != null)
            {
                AddFavorites dlg = new AddFavorites(GetCurrentBrowser().Url.ToString(), "");
                DialogResult res = dlg.ShowDialog();

                if (res == DialogResult.OK)
                {
                    treeControl1.AddFavorit(dlg.favUrl, dlg.favName, dlg.favFile);

                    //guardamos el link en el servidor

                    WiworBrowser.Controls.DBHttp http = new WiworBrowser.Controls.DBHttp();
                    
                    string ret = http.PostData(Common.Configuration.Settings.UpdatePage + "/prog/addFavorite.aspx", "group="+ dlg.favFile + "&url=" + dlg.favUrl + "&desc=" + dlg.favName);

                    if (ret == "OK")
                    {
                        MessageBox.Show("Dirección URL guardada correctamente.", "URL", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Problemas al guardar la información referente a la URL.", "URL", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                dlg.Close();
            }
        }

        ////add to favorits bar
        //private void addToFavoritsBarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    treeControl1.addLink(getCurrentBrowser().Url.ToString(), getCurrentBrowser().Url.ToString());
        //}

        //organize favorites
        private void organizeFavoritsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeControl1.OrganizeFavorites();
        }

        //show favorites in menu
        private void favoritesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            for (int i = favoritesToolStripMenuItem.DropDownItems.Count - 1; i > 5; i--)
            {
                favoritesToolStripMenuItem.DropDownItems.RemoveAt(i);
            }
            foreach (Favorite fav in Common.Configuration.Favorites)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(fav.Description,
                                                               treeControl1.GetFavicon(fav.Url),
                                                               fav_Click);
                item.ToolTipText = fav.Url;
                favoritesToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        //show links in menu
        private void linksMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //if (File.Exists(TreeControl.linksXml))
            //{
            //    XmlDocument myXml = new XmlDocument();
            //    myXml.Load(TreeControl.linksXml);
            //    linksMenuItem.DropDownItems.Clear();
            //    foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
            //    {
            //        ToolStripMenuItem item = new ToolStripMenuItem(el.InnerText,
            //                                                       treeControl1.getFavicon(el.GetAttribute("url")),
            //                                                       fav_Click);
            //        item.ToolTipText = el.GetAttribute("url");
            //        linksMenuItem.DropDownItems.Add(item);
            //    }
            //}
        }

        private void fav_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem m = (ToolStripMenuItem) sender;
            GetCurrentBrowser().Navigate(m.ToolTipText);
        }

        #endregion

        #region FILE

        /*FILE*/

        //new tab
        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTab();
        }

        //duplicate tab
        private void duplicateTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetCurrentBrowser().Url != null)
            {
                Uri dup_url = GetCurrentBrowser().Url;
                AddNewTab();
                GetCurrentBrowser().Url = dup_url;
            }
            else AddNewTab();
        }

        //new window
        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new WiworForm()).Show();
        }

        //close tab
        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeTab();
        }

        //open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Open(GetCurrentBrowser())).Show();
        }

        //page setup
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().ShowPageSetupDialog();
        }

        //save as
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().ShowSaveAsDialog();
        }

        //print
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().ShowPrintDialog();
        }

        //print preview
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().ShowPrintPreviewDialog();
        }

        //properties
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().ShowPropertiesDialog();
        }

        //send page by email
        private void pageByEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //getCurrentBrowser().Navigate("https://login.yahoo.com/config/login_verify2?&.src=ym");
            Process.Start("msimn.exe");
        }

        //send link by email
        private void linkByEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // getCurrentBrowser().Navigate("https://login.yahoo.com/config/login_verify2?&.src=ym");
            Process.Start("msimn.exe");
        }

        #endregion

        #region EDIT

        /*EDIT*/
        //cut
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Document.ExecCommand("Cut", false, null);
        }

        //copy
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Document.ExecCommand("Copy", false, null);
        }

        //paste
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Document.ExecCommand("Paste", false, null);
        }

        //select all
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Document.ExecCommand("SelectAll", true, null);
        }

        #endregion

        #region VIEW

        /* VIEW */

//explorer bars
        //private void favoritsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    splitContainer1.Panel1Collapsed = true;
        //    favoritesTabControl.SelectedTab = favTabPage;

        //}

        //private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    splitContainer1.Panel1Collapsed = true;
        //    favoritesTabControl.SelectedTab = historyTabPage;
        //}
//favorites,history checked
        private void explorerBarsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //favoritesViewMenuItem.Checked =
            //    (splitContainer1.Panel1Collapsed == true && favoritesTabControl.SelectedTab == favTabPage);

            //historyViewMenuItem.Checked =
            //    (splitContainer1.Panel1Collapsed == true && favoritesTabControl.SelectedTab == historyTabPage);
        }

        /*Go to*/
//drop down opening
        private void goToToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            backToolStripMenuItem.Enabled = GetCurrentBrowser().CanGoBack;
            forwardToolStripMenuItem.Enabled = GetCurrentBrowser().CanGoForward;

            while (goToMenuItem.DropDownItems.Count > 5)
                goToMenuItem.DropDownItems.RemoveAt(goToMenuItem.DropDownItems.Count - 1);

            foreach (string a in urls)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(a, null, goto_click);

                item.Checked = (GetCurrentBrowser().Url.Host.Equals(a));

                goToMenuItem.DropDownItems.Add(item);
            }
        }

        private void goto_click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Navigate(sender.ToString());
        }

        //back
        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().GoBack();
        }

        //forward
        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().GoForward();
        }

        //home
        private void homePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Navigate(Common.Configuration.Settings.HomePage);
        }

        /*Stop*/

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Stop();
        }

        /*Refresh*/

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBrowser().Refresh();
        }

        /*view source*/

        private void sourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String source = ("source.txt");
            StreamWriter writer = File.CreateText(source);
            writer.Write(GetCurrentBrowser().DocumentText);
            writer.Close();
            Process.Start("notepad.exe", source);
        }

        //text size 
        private void textSizeToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string level = e.ClickedItem.ToString();
            smallerToolStripMenuItem.Checked = false;
            smallestToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            largerToolStripMenuItem.Checked = false;
            largestToolStripMenuItem.Checked = false;
            switch (level)
            {
                case "Smallest":
                    GetCurrentBrowser().Document.ExecCommand("FontSize", true, "0");
                    smallestToolStripMenuItem.Checked = true;
                    break;
                case "Smaller":
                    GetCurrentBrowser().Document.ExecCommand("FontSize", true, "1");
                    smallerToolStripMenuItem.Checked = true;
                    break;
                case "Medium":
                    GetCurrentBrowser().Document.ExecCommand("FontSize", true, "2");
                    mediumToolStripMenuItem.Checked = true;
                    break;
                case "Larger":
                    GetCurrentBrowser().Document.ExecCommand("FontSize", true, "3");
                    largerToolStripMenuItem.Checked = true;
                    break;
                case "Largest":
                    GetCurrentBrowser().Document.ExecCommand("FontSize", true, "4");
                    largestToolStripMenuItem.Checked = true;
                    break;
            }
        }

        //full screen
        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(FormBorderStyle == FormBorderStyle.None && WindowState == FormWindowState.Maximized))
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                TopMost = true;
                menuBar.Visible = false;
                linkBar.Visible = false;
                adrBar.Visible = false;
                splitContainer1.Panel1Collapsed = false;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.Sizable;
                TopMost = false;
                menuBar.Visible = Common.Configuration.Settings.MenuBar;
                adrBar.Visible = Common.Configuration.Settings.AdressBar;
                linkBar.Visible = Common.Configuration.Settings.LinkBar;
                splitContainer1.Panel1Collapsed = Common.Configuration.Settings.FavoritesPanel;
            }
        }

        //splash screen
        private void splashScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.Configuration.Settings.SplashScreen = splashScreenToolStripMenuItem.Checked;
        }

        #endregion

        #region TOOLS

//delete browsing history
        private void deleteBrowserHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteBrowsingHistory b = new DeleteBrowsingHistory();
            if (b.ShowDialog() == DialogResult.OK)
            {
                if (b.History.Checked == true)
                {
                    Common.Configuration.DeleteHistorys();
                    treeControl1.ClearHistoryTreeViewNodes();
                }
                if (b.TempFiles.Checked == true)
                {
                    urls.Clear();
                    //while (imgList.Images.Count > 4)
                    //    imgList.Images.RemoveAt(imgList.Images.Count - 1);
                    File.Delete("source.txt");
                }
            }
        }
//internet options
        private void internetOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.Configuration.Settings.SettingsPassword != "")
            {
                Password sp = new Password();
                if (sp.ShowDialog() == DialogResult.OK)
                {
                    if (sp.txtPassword.Text.ToLower() != Common.Configuration.Settings.SettingsPassword.ToLower())
                    {
                        MessageBox.Show("Clave de acceso incorrecta.", "Password", MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            InternetOption intOp = new InternetOption(GetCurrentBrowser().Url.ToString());
            if (intOp.ShowDialog() == DialogResult.OK)
            {
                if (intOp.chkRemoveHistory.Checked)
                {
                    Common.Configuration.DeleteHistorys();
                    treeControl1.ClearHistoryTreeViewNodes();
                }

                Common.Configuration.Settings.DropDown = (int)intOp.num.Value;
                ActiveForm.ForeColor = intOp.forecolor;
                ActiveForm.BackColor = intOp.backcolor;
                linkBar.BackColor = intOp.backcolor;
                adrBar.BackColor = intOp.backcolor;
                ActiveForm.Font = intOp.font;
                linkBar.Font = intOp.font;
                menuBar.Font = intOp.font;

                Common.Configuration.SaveSettings();
            }
        }

        #endregion

        #region HELP

        //about
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new About(false)).Show();
        }

        #endregion

        private void comprobarVersiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AutoUpdate(false)).Show();
        }
    }
}