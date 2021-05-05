using FSFormControls;
using Gecko;
using Gecko.DOM;
using Gecko.JQuery;
using Gecko.WebIDL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FSAutomatizeWeb
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class WebBrowserFunctions
    {
        private WebBrowser wbMain;
        public GeckoHtmlElement htmlElm;
        public bool IsStop = false;

        public WebBrowserFunctions()
        {
            //Xpcom.EnableProfileMonitoring = false;
            //var programDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //Xpcom.Initialize(Path.Combine(programDirectory, "Firefox"));
            //Xpcom.Initialize(@"../../xulrunner64");
            Xpcom.Initialize(@"Firefox");

            CallBackWinAppWebBrowser();
        }


        private void CallBackWinAppWebBrowser()
        {
            wbMain = new WebBrowser();
            
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.webbrowser.objectforscripting?view=net-5.0
            wbMain.ObjectForScripting = this;
            
            wbMain.ScriptErrorsSuppressed = false;

            wbMain.DocumentText = @"<html>
                                        <head>
                                            <script type='text/javascript'>
                                                var isAborted = false;

                                                function UnAbort() {isAborted = false; window.external.UnAbort();}
                                                function Abort() {isAborted = true; release(); window.external.Abort();}
                                                function CheckAbort() {if(isAborted == true) { window.external.Abort(); throw new Error('Aborted');} }

                                                /*var isAborted = false;
                                                function UnAbort() {isAborted = false;}
                                                function Abort() {isAborted = true;}
                                                function CheckAbort() {if(isAborted == true) throw new Error('Aborted');}*/

                                                function stringtoXML(data){ if (window.ActiveXObject){ var doc = new ActiveXObject('Microsoft.XMLDOM'); doc.async='false'; doc.loadXML(data); } else { var parser = new DOMParser(); var doc = parser.parseFromString(data,'text/xml'); }	return doc; }

                                                /* Open new tab */

                                                function release() { CheckAbort(); window.external.ReleaseMR();  }

                                                function countNodes(xpath) { CheckAbort(); return window.external.countNodes(xpath); } 

                                                function tabnew() { CheckAbort(); window.external.tabnew();}
                                                /* Close current tab  */
                                                function tabclose() { CheckAbort(); window.external.tabclose();}
                                                /* Close all tab  */
                                                function tabcloseall() { CheckAbort(); window.external.tabcloseall();}
                                                /* Go to website by url or xpath  */
                                                function go(a) { CheckAbort(); window.external.go(a);}

                                                function back() { CheckAbort(); window.external.Back(); }
                                                function next() { CheckAbort(); window.external.Next(); }
                                                function reload() { CheckAbort(); window.external.Reload(); }
                                                function stop() { CheckAbort(); window.external.Stop(); }

                                                /* Sleep with a = miliseconds to sleep, b = true if wait until browser loading finished, b = false wait until timeout miliseconds  */
                                                function sleep(a, b) { CheckAbort(); window.external.sleep(a,b);}
                                                /* Quit application  */
                                                function exit() { CheckAbort(); window.external.exit();}
                                                /* Click by xpath  */
                                                function click(a) { CheckAbort(); window.external.click(a);}
                                                /* write a log to preview, a = content of log  */
                                                function log(a) { CheckAbort(); window.external.log(a);}
                                                /* clear log on the preview  */
                                                function clearlog() { CheckAbort(); window.external.clearlog();}
                                                /* extract data from xpath  */
                                                //function extract(a) {CheckAbort(); return window.external.extract(a);}
                                                function extract(xpath, type) {CheckAbort(); return window.external.extract(xpath, type);}

                                                function filliframe(title, value) { CheckAbort(); window.external.filliframe(title, value); }                                                

                                                /* fill xpath by value, a = xpath, b = value  */
                                                function fill(a,b) { CheckAbort(); window.external.fill(a,b);}
                                                /* convert extract string to object  */

                                                /*function filldropdown(a, b) { CheckAbort(); window.external.filldropdown(a, b); }*/
                                                function filldropdown(xpath, value) { CheckAbort(); window.external.filldropdown(xpath, value); }
                                                function toObject(a) {CheckAbort(); var wrapper= document.createElement('div'); wrapper.innerHTML= a; return wrapper;}
                                                function blockFlash(isBlock) { CheckAbort(); window.external.BlockFlash(isBlock); }

                                                /* browser get all link in the area of xpath, it will stop until program go all of link , a = xpath */
                                                function browser(a) {CheckAbort(); window.external.browser(a);}
                                                /* reset list website to unread so program can go back and browser continue */
                                                function resetlistwebsite() {CheckAbort(); window.external.ResetListWebsite();}
                                                /* take a snapshot from current website on current tab, a = location to save a snapshot */

                                                function takesnapshot(a) {CheckAbort(); window.external.TakeSnapshot(a);}
                                                /* reconigze text of image from url, a = url of image  */
                                                function imageToText(xpath, language) { CheckAbort(); return window.external.imgToText(xpath, language);}
                                                /* set value to file upload (not work in ie)  */
                                                function fileupload(a,b){CheckAbort(); window.external.FileUpload(a,b);}

                                                /* create folder, a = location  */
                                                function createfolder(a) { CheckAbort(); window.external.createfolder(a);}
                                                /* download file from url, a = url to download, b = location where file located  */
                                                function download(a,b) {CheckAbort(); window.external.download(a,b);}

                                                function downloadWebsite(url) { CheckAbort(); return window.external.DownloadWebsite(url); } 

                                                function getfiles(a) { CheckAbort(); return window.external.getfiles(a); }
                                                function getfolders(a) { CheckAbort(); return window.external.getfolders(a); }

                                                /* read a file, a = location of file  */
                                                function read(a) { CheckAbort(); return window.external.read(a);}
                                                /* save file, a = content of file, b = location of file to save, c = is file override (true: fill will be override, false: not override)  */
                                                function save(a,b,c) { CheckAbort(); return window.external.save(a,b,c);}
                                                /* remove a file, a = location of file will be removed */
                                                function remove(a) { CheckAbort(); window.external.remove(a);}
                                                function removefolder(a) {CheckAbort(); window.external.removefolder(a);}

                                                function explorer(a) { CheckAbort(); window.external.explorer(a); }

                                                /* run code from string, a = code to run  */
                                                function excute(a) { CheckAbort(); window.external.excute(a);}

                                                function logoff() { CheckAbort(); window.external.logoff();} 
                                                function lockworkstation() {CheckAbort(); window.external.lockworkstation();} 
                                                function forcelogoff() { CheckAbort(); window.external.forcelogoff();} 
                                                function reboot() { CheckAbort(); window.external.reboot();} 
                                                function shutdown() { CheckAbort(); window.external.shutdown();} 
                                                function hibernate() { CheckAbort(); window.external.hibernate();} 
                                                function standby() { CheckAbort(); window.external.standby();} 


                                                /* run application, a = location of application */
                                                function runcommand(path, parameters) { CheckAbort(); window.external.runcommand(path, parameters); }

                                                function createtask(a,b,c,d,e,f) { CheckAbort(); window.external.createtask(a,b,c,d,e,f); }
                                                function removetask(a) { CheckAbort(); window.external.removetask(a);}

                                                function generatekeys() { CheckAbort(); window.external.generatekeys();}
                                                function encrypt(a, b) { CheckAbort(); return window.external.encrypt(a, b);}
                                                function decrypt(a, b) { CheckAbort(); return window.external.decrypt(a, b);}

                                                function showpicture(a,b) { CheckAbort(); window.external.showimage(a,b); }
                                                function savefilterimage(a) { CheckAbort(); window.external.savefilterimage(a); }

                                                function writetextimage(a, b) {CheckAbort(); window.external.writetextimage(a,b); } 

                                                function getcurrenturl() {CheckAbort(); return window.external.getCurrentUrl();}

                                                function scrollto(a) {CheckAbort(); window.external.scrollto(a); }

                                                function getheight() { CheckAbort(); return window.external.getheight(); }

                                                function gettitle() { CheckAbort(); return window.external.gettitle(); } 

                                                function getlinks(a) { CheckAbort(); return window.external.getlinks(a); } 

                                                function getCurrentContent() { CheckAbort(); return window.external.getCurrentContent(); } 

                                                function getCurrentPath() { CheckAbort(); return window.external.getCurrentPath(); } 

                                                function checkelement(a) { CheckAbort(); return window.external.checkelement(a);}

                                                function readCellExcel(a, b, c, d) { CheckAbort(); return window.external.readCellExcel(a,b,c,d);}

                                                function writeCellExcel(a, b, c, d) { CheckAbort(); window.external.writeCellExcel(a,b,c,d); }

                                                function replaceMsWord(a, b, c, d) { CheckAbort(); window.external.replaceMsWord(a,b,c,d); } 

                                                function loadHTML(a) { CheckAbort(); window.external.loadHTML(a); }" +

                                                "function textToJSON(a) { CheckAbort(); var b = eval(\"(\" + window.external.textToJSON(a) + \")\"); return b; }" +

                                                @"function getCurrentLogin() { return textToJSON(window.external.getCurrentUser());}

                                                function login(a, b) { return window.external.login(a,b); }

                                                function register(a, b, c, d) { return window.external.register(a, b, c, d);}

                                                function getAccount(a) { CheckAbort(); var b = window.external.GetAccount(a); if(b == '') return ''; else return textToJSON(b); }

                                                function captchaborder(a,b) { CheckAbort(); window.external.CaptchaBorder(a,b); } 

                                                function saveImageFromElement(a,b) { CheckAbort(); window.external.SaveImageFromElement(a,b);}

                                                function getControlText(a,b,c) { CheckAbort(); return window.external.GetControlText(a,b,c); }

                                                function setControlText(a,b,c,d) { CheckAbort(); window.external.SetControlText(a,b,c,d); }

                                                function clickControl(a,b,c) { CheckAbort(); window.external.ClickControl(a,b,c); } 

                                                function getCurrentMouseX() { CheckAbort(); return window.external.GetCurrentMouseX(); } 

                                                function getCurrentMouseY() { CheckAbort(); return window.external.GetCurrentMouseY(); } 

                                                function MouseDown(a,b) { CheckAbort(); window.external.Mouse_Down(a,b); }

                                                function MouseUp(a,b) { CheckAbort(); window.external.Mouse_Up(a,b); }

                                                function MouseClick(a,b) { CheckAbort(); window.external.Mouse_Click(a,b); }

                                                function MouseDoubleClick(a,b) { CheckAbort(); window.external.Mouse_Double_Click(a,b); }

                                                function MouseMove(a,b,c,d) {CheckAbort(); window.external.Mouse_Show(a,b,c,d); }

                                                function MouseWheel(a,b) { CheckAbort(); window.external.Mouse_Wheel(a,b); }

                                                function KeyDown(a,b) { CheckAbort(); window.external.Key_Down(a,b); }

                                                function KeyUp(a,b) { CheckAbort(); window.external.Key_Up(a,b); }

                                                function Reload() { CheckAbort(); window.external.Reload(); }

                                                function Help() { CheckAbort(); window.external.Help(); }

                                                function sendEmail(name, email, subject, content) { CheckAbort(); return window.external.sendEmail(name, email, subject, content); }" +

                                                "function getAccountBy(name) { CheckAbort(); var a = window.external.GetAccountBy(name); if(a != '') { return eval(\"(\" + a + \")\"); } else { return ''; } }" +

                                                @"function getDatabases(name) { CheckAbort(); return window.external.GetDatabases(name); } 

                                                function getTables(name, dbName) { CheckAbort(); return window.external.GetTables(name, dbName); }

                                                function getColumns(name, dbName, table) { CheckAbort(); return window.external.GetColumns(name, dbName, table); }

                                                function getRows(name, dbName, sql) { CheckAbort(); return window.external.GetRows(name, dbName, sql); }

                                                function excuteQuery(name, dbName, sql) { CheckAbort(); return window.external.ExcuteQuery(name, dbName, sql); } 

                                                function removeStopWords(text) { CheckAbort(); return window.external.RemoveStopWords(text); }

                                                function addElement(path, node1, node2, text) { CheckAbort(); return window.external.AddElement(path, node1, node2, text); }

                                                function checkXmlElement(path, node, text) { CheckAbort(); return window.external.CheckXmlElement(path, node, text); }

                                                function getXmlElement(path, node) { CheckAbort(); return window.external.GetXmlElement(path, node); }

                                                function getParentElement(path, node, text) { CheckAbort(); return window.external.GetParentElement(path, node, text); }
                                                
                                                function extractbyRegularExpression(pattern, groupName) { CheckAbort(); return window.external.ExtractUsingRegularExpression(pattern, groupName); }

                                                function addToDownload(fileName, url, folder) { CheckAbort(); return window.external.AddToDownload(fileName, url, folder); }

                                                function startDownload() { CheckAbort(); return window.external.StartDownload(); }
                                            </script>
                                        </head>
                                        <body>
                                            
                                        </body>
                                    </html>";

            Program.mainFrm.Controls.Add(wbMain);
        }

        public string DocumentText
        {
            get { return wbMain.DocumentText; }
            set { wbMain.DocumentText = value; }
        }

        public void InvokeScript(string script)
        {
            wbMain.Document.InvokeScript(script);
        }

        public void ExecuteJSCode(string code)
        {
            ExecuteJSCodeWebBrowser(code);
        }

        public void ExecuteJSCodeWebBrowser(string code)
        {
            wbMain.Document.InvokeScript("UnAbort");
            object obj = wbMain.Document.InvokeScript("eval", new object[] { code });
        }

        public void GoWebBrowser(string url)
        {
            if (String.IsNullOrEmpty(url)) return;
            if (url.Equals("about:blank")) return;

            if (!url.ToLower().StartsWith("http") && !url.Contains("."))
                url = "https://www.google.com/search?q=" + url;

            GeckoWebBrowser wbBrowser = new GeckoWebBrowser();

            wbBrowser.ProgressChanged -= wbBrowser_ProgressChanged;
            wbBrowser.ProgressChanged += wbBrowser_ProgressChanged;
            wbBrowser.Navigated -= wbBrowser_Navigated;
            wbBrowser.Navigated += wbBrowser_Navigated;
            wbBrowser.DocumentCompleted -= wbBrowser_DocumentCompleted;
            wbBrowser.DocumentCompleted += wbBrowser_DocumentCompleted;
            wbBrowser.CanGoBackChanged -= wbBrowser_CanGoBackChanged;
            wbBrowser.CanGoBackChanged += wbBrowser_CanGoBackChanged;
            wbBrowser.CanGoForwardChanged -= wbBrowser_CanGoForwardChanged;
            wbBrowser.CanGoForwardChanged += wbBrowser_CanGoForwardChanged;
            wbBrowser.NavigationError += wbBrowser_NavigationError;

            wbBrowser.DomContextMenu -= wbBrowser_DomContextMenu;
            wbBrowser.DomContextMenu += wbBrowser_DomContextMenu;
            wbBrowser.NoDefaultContextMenu = true;

            //permitimos la navegación HTTPS
            Gecko.CertOverrideService.GetService().ValidityOverride += wbBrowser_ValidityOverride;

            //wbBrowser.NSSError += (s, e) => {
            //    CertOverrideService.GetService().RememberRecentBadCert(e.Uri, e.SSLStatus);
            //    Uri refUrl = wbBrowser.Url;
            //    wbBrowser.Navigate(e.Uri.AbsoluteUri, GeckoLoadFlags.FirstLoad, refUrl.AbsoluteUri, null);
            //    e.Handled = true;
            //};

            //cambiar el agente
            if (!String.IsNullOrEmpty(Program.userAgent))
                GeckoPreferences.User["general.useragent.override"] = Program.userAgent;

            //Disable WebRTC
            //GeckoPreferences.Default["media.peerconnection.enabled"] = false;

            //Varios
            //GeckoPreferences.User["places.history.enabled"] = false;
            //GeckoPreferences.User["security.warn_viewing_mixed"] = false;
            //GeckoPreferences.User["plugin.state.flash"] = 0;
            //GeckoPreferences.User["browser.cache.disk.enable"] = false;
            //GeckoPreferences.User["browser.cache.memory.enable"] = false;
            //GeckoPreferences.User["browser.xul.error_pages.enabled"] = false;
            //GeckoPreferences.User["dom.max_script_run_time"] = 0; //let js run as long as it needs to; prevents timeout errors
            //GeckoPreferences.User["browser.download.manager.showAlertOnComplete"] = false;
            //GeckoPreferences.User["privacy.popups.showBrowserMessage"] = false;

            if (Program.mainFrm.cfgShowImages.Checked)
            {
                GeckoPreferences.Default["image.animation_mode"] = "none";
                GeckoPreferences.Default["browser.display.show_image_placeholders"] = false;
                GeckoPreferences.Default["extensions.blocklist.enabled"] = true;
                GeckoPreferences.User["permissions.default.image"] = 2;
            }
            else
            {
                GeckoPreferences.Default["image.animation_mode"] = "once";
                GeckoPreferences.Default["browser.display.show_image_placeholders"] = true;
                GeckoPreferences.Default["extensions.blocklist.enabled"] = false;
                GeckoPreferences.User["permissions.default.image"] = 1;
            }

            Program.mainFrm.currentTab.Controls.Add(wbBrowser);
            wbBrowser.Dock = DockStyle.Fill;
            wbBrowser.Navigate(url);
        }

        private void wbBrowser_NavigationError(object sender, Gecko.Events.GeckoNavigationErrorEventArgs e)
        {
            MessageBox.Show("Error code: " + e.ErrorCode.ToString("X"));
        }

        private void wbBrowser_ProgressChanged(object sender, GeckoProgressEventArgs e)
        {
            Program.mainFrm.progressbar.Maximum = (int)e.MaximumProgress;
            int value = (int)e.CurrentProgress;
            if(value <= (int)e.MaximumProgress)
                Program.mainFrm.progressbar.Value = value;
        }

        private void wbBrowser_CanGoForwardChanged(object sender, EventArgs e)
        {
            GeckoWebBrowser wbBrowser = (GeckoWebBrowser)sender;
            if (wbBrowser != null)
            {
                Program.mainFrm.toolStripNext.Enabled = wbBrowser.CanGoForward;
            }
            else
            {
                Program.mainFrm.toolStripNext.Enabled = false;
            }
        }

        private void wbBrowser_CanGoBackChanged(object sender, EventArgs e)
        {
            GeckoWebBrowser wbBrowser = (GeckoWebBrowser)sender;
            if (wbBrowser != null)
            {
                Program.mainFrm.toolStripBack.Enabled = wbBrowser.CanGoBack;
            }
            else
            {
                Program.mainFrm.toolStripBack.Enabled = false;
            }
        }

        private void wbBrowser_Navigated(object sender, GeckoNavigatedEventArgs e)
        {
            string url = ((GeckoWebBrowser)sender).Url.ToString();
            if (url != "about:blank")
                Program.mainFrm.tbxAddress.Text = url;
        }

        private void wbBrowser_DocumentCompleted(object sender, EventArgs e)
        {
            GeckoWebBrowser wbBrowser = (GeckoWebBrowser)sender;

            string title = wbBrowser.DocumentTitle;
            Program.mainFrm.currentTab.Text = (title.Length > 10 ? title.Substring(0, 10) + "..." : title);
            Program.mainFrm.tbxAddress.Text = wbBrowser.Url.ToString();

            Program.mainFrm.progressbar.Value = 0;

            //mostramos el arbol html
            CreateTreeViewHtml(wbBrowser);
        }
        private void CreateTreeViewHtml(GeckoWebBrowser wbBrowser)
        {
            Program.mainFrm.propertyGrid1.SelectedObject = null;
            Program.mainFrm.tvHtml.Nodes.Clear();
            Program.mainFrm.tvHtml.Nodes.Add(LoadNode(wbBrowser.Document.Body.Parent, 0));
        }

        private DBTreeViewNode LoadNode(GeckoHtmlElement htmlElm, int index)
        {
            if (htmlElm != null)
            {
                DBTreeViewNode tn = new DBTreeViewNode(htmlElm.TagName.ToLower());
                tn.Value = index.ToString();
                foreach (GeckoNode geckoNode in htmlElm.ChildNodes)
                {
                    GeckoHtmlElement geckoElement = geckoNode as GeckoHtmlElement;
                    DBTreeViewNode node = LoadNode(geckoElement, index + 1);
                    if (node != null)
                    {
                        node.Value = index.ToString();
                        tn.Nodes.Add(node);
                    }
                }

                return tn;
            }
            else
                return null;
        }

        private void wbBrowser_DomContextMenu(object sender, DomMouseEventArgs e)
        {
            if (e.Button.ToString().IndexOf("Right") != -1)
            {
                Program.mainFrm.contextMenuBrowser.Show(Cursor.Position);

                GeckoWebBrowser wb = Functions.GetCurrentWB();
                if (wb != null)
                {
                    htmlElm = (GeckoHtmlElement)wb.Document.ElementFromPoint(e.ClientX, e.ClientY);

                    string xpath = Functions.GetXPath(htmlElm);
                    //Program.mainFrm.toolStripStatus.Text = xpath;
                    ShowItemInTreeView(xpath);
                }
            }
        }

        private void ShowItemInTreeView(string xpath)
        {
            DBTreeViewNode node = Program.mainFrm.tvHtml.FindNodeByXPath(xpath);

            if (node != null)
            {
                Program.mainFrm.tabControlCode.SelectedTab = Program.mainFrm.tabHtmlTree;
                Program.mainFrm.tvHtml.SelectedNode = node;
                Program.mainFrm.tvHtml.Focus();
                Program.mainFrm.ShowNode(node);
            }
        }

        private void wbBrowser_ValidityOverride(object sender, Gecko.Events.CertOverrideEventArgs e)
        {
            e.OverrideResult = Gecko.CertOverride.Mismatch | Gecko.CertOverride.Time | Gecko.CertOverride.Untrusted;
            e.Temporary = true;
            e.Handled = true;
        }

        public void GoWebBrowserByXpath(string xpath)
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                GeckoHtmlElement elm = Functions.GetElementByXpath(wb.Document, xpath);
                if (elm != null)
                {
                    UpdateUrlAbsolute(wb.Document, elm);
                    string url = elm.GetAttribute("href");
                    if (!string.IsNullOrEmpty(url))
                        wb.Navigate(url);
                }
            }
        }

        public void NextWebBrowser()
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                wb.GoForward();
            }
        }

        public void BackWebBrowser()
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                wb.GoBack();
            }
        }

        public void ReloadWebBrowser()
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                wb.Reload();
            }
        }

        public void StopWebBrowser()
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                wb.Stop();
            }
        }

        public void TabSelectedWebBrowser()
        {
            if (Program.mainFrm.tabMain.TabCount > 0)
            {
                GeckoWebBrowser wb = Functions.GetCurrentWB();
                if (wb != null)
                {
                    Program.mainFrm.tbxAddress.Text = wb.Url.ToString();
                    string title = wb.DocumentTitle;
                    Program.mainFrm.currentTab.Text = (title.Length > 10 ? title.Substring(0, 10) + "..." : title);
                    Program.mainFrm.Text = title;
                }
            }
        }

        

        public void UpdateUrlAbsolute(GeckoDocument doc, GeckoHtmlElement ele)
        {
            string link = doc.Url.GetLeftPart(UriPartial.Authority);

            var eleColec = ele.GetElementsByTagName("IMG");
            foreach (GeckoHtmlElement it in eleColec)
            {
                if (!it.GetAttribute("src").StartsWith("http://"))
                    it.SetAttribute("src", link + it.GetAttribute("src"));
            }
            eleColec = ele.GetElementsByTagName("A");
            foreach (GeckoHtmlElement it in eleColec)
            {
                if (!it.GetAttribute("href").StartsWith("http://"))
                    it.SetAttribute("href", link + it.GetAttribute("href"));
            }
        }


        /*
         * 
         * 
         * FUNCIONES LLAMADAS DESDE WEBBROWSER
         * 
         * 
         */

        public void Help()
        {
            log("UnAbort();");
            log("Abort();");
            log("ReleaseMR();");
            log("countNodes(xpath);");
            log("tabnew();");
            log("tabclose();");
            log("tabcloseall();");
            log("go(a);");
            log("Back();");
            log("Next();");
            log("Reload();");
            log("Stop();");
            log("sleep(a,b);");
            log("exit();");
            log("click(a);");
            log("log(a);");
            log("clearlog();");
            log("extract(xpath, type);");
            log("filliframe(title, value);");
            log("fill(a,b);");
            log("filldropdown(xpath, value);");
            log("BlockFlash(isBlock);");
            log("browser(a);");
            log("ResetListWebsite();");
            log("TakeSnapshot(a);");
            log("imgToText(xpath, language);");
            log("FileUpload(a,b);");
            log("createfolder(a);");
            log("download(a,b);");
            log("DownloadWebsite(url);");
            log("getfiles(a);");
            log("getfolders(a);");
            log("read(a);");
            log("save(a,b,c);");
            log("remove(a);");
            log("removefolder(a);");
            log("explorer(a);");
            log("excute(a);");
            log("logoff();");
            log("lockworkstation();");
            log("forcelogoff();");
            log("reboot();");
            log("shutdown();");
            log("hibernate();");
            log("standby();");
            log("runcommand(path, parameters);");
            log("createtask(a,b,c,d,e,f);");
            log("removetask(a);");
            log("generatekeys();");
            log("encrypt(a, b);");
            log("decrypt(a, b);");
            log("showimage(a,b);");
            log("savefilterimage(a);");
            log("writetextimage(a,b);");
            log("getCurrentUrl();");
            log("scrollto(a);");
            log("getheight();");
            log("gettitle();");
            log("getlinks(a);");
            log("getCurrentContent();");
            log("getCurrentPath();");
            log("checkelement(a);");
            log("readCellExcel(a,b,c,d);");
            log("writeCellExcel(a,b,c,d);");
            log("replaceMsWord(a,b,c,d);");
            log("loadHTML(a);");
            log("textToJSON(a);");
            log("getCurrentUser();");
            log("login(a,b);");
            log("register(a, b, c, d);");
            log("GetAccount(a);");
            log("CaptchaBorder(a,b);");
            log("SaveImageFromElement(a,b);");
            log("GetControlText(a,b,c);");
            log("SetControlText(a,b,c,d);");
            log("ClickControl(a,b,c);");
            log("GetCurrentMouseX();");
            log("GetCurrentMouseY();");
            log("Mouse_Down(a,b);");
            log("Mouse_Up(a,b);");
            log("Mouse_Click(a,b);");
            log("Mouse_Double_Click(a,b);");
            log("Mouse_Show(a,b,c,d);");
            log("Mouse_Wheel(a,b);");
            log("Key_Down(a,b);");
            log("Key_Up(a,b);");
            log("Reload();");
            log("Help();");
            log("sendEmail(name, email, subject, content);");
            log("GetAccountBy(name);");
            log("GetDatabases(name);");
            log("GetTables(name, dbName);");
            log("GetColumns(name, dbName, table);");
            log("GetRows(name, dbName, sql);");
            log("ExcuteQuery(name, dbName, sql);");
            log("RemoveStopWords(text);");
            log("AddElement(path, node1, node2, text);");
            log("CheckXmlElement(path, node, text);");
            log("GetXmlElement(path, node);");
            log("GetParentElement(path, node, text);");
            log("ExtractUsingRegularExpression(pattern, groupName);");
            log("AddToDownload(fileName, url, folder);");
            log("StartDownload();");
        }


        public void go(string url)
        {
            if (Program.mainFrm.currentTab == null)
            {
                ToolbarFunctions.tabnew();
            }

            //WebBrowser
            if (!url.StartsWith("/"))
            {
                if (Program.mainFrm.currentTab.Controls.Count > 0)
                {
                    Program.mainFrm.currentTab.Controls.RemoveAt(0);
                }

                GoWebBrowser(url);
            }
            else
            {
                GoWebBrowserByXpath(url);
            }
        }

        public void Abort()
        {
            IsStop = true;
            ToolbarFunctions.Stop();
        }

        public void UnAbort()
        {
            IsStop = false;
        }

        public string extract(string xpath, string type)
        {
            string result = string.Empty;
            GeckoHtmlElement elm;

            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                if (xpath.StartsWith("/"))
                {
                    elm = Functions.GetElementByXpath(wb.Document, xpath);

                    if (elm != null)
                        UpdateUrlAbsolute(wb.Document, elm);

                }
                else
                {
                    var id = xpath;
                    elm = wb.Document.GetHtmlElementById(id);
                    if (elm != null)
                        UpdateUrlAbsolute(wb.Document, elm);
                }

                if (elm != null)
                {
                    switch (type)
                    {
                        case "html":
                            result = elm.OuterHtml;
                            break;
                        case "src":
                            string strScr = elm.GetAttribute("src");
                            if (!String.IsNullOrEmpty(strScr))
                                result = strScr.Trim();
                            else
                                result = "Imposible acceder al atributo: " + type;
                            break;
                        case "text":
                            if (elm.GetType().Name == "GeckoTextAreaElement")
                            {
                                result = ((GeckoTextAreaElement)elm).Value;
                            }
                            else
                            {
                                result = elm.TextContent.Trim();
                            }
                            break;
                        case "href":
                            string strHref = elm.GetAttribute("href");
                            if (!String.IsNullOrEmpty(strHref))
                                result = strHref.Trim();
                            else
                                result = "Imposible acceder al atributo: " + type;
                            break;
                        default:
                            string strType = elm.GetAttribute(type);
                            if (!String.IsNullOrEmpty(strType))
                                result = strType.Trim();
                            else
                                result = "Imposible acceder al atributo: " + type;
                            break;
                    }
                }
            }

            return result;
        }

        public void filliframe(string title, string value)
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                foreach (GeckoFrameElement ifr in wb.Document.GetElementsByTagName("frame"))
                {
                    if (ifr.ContentDocument.Title == title)
                    {
                        foreach (GeckoNode item in ifr.ChildNodes)
                        {
                            if (item.NodeName == "HTML")
                            {
                                foreach (GeckoNode it in item.ChildNodes)
                                {
                                    if (it.NodeName == "BODY")
                                    {
                                        GeckoBodyElement elem = (GeckoBodyElement)it;
                                        elem.InnerHtml = value;
                                        elem.Focus();
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        public void fill(string id, string value)
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                if (id.StartsWith("/"))
                {
                    string xpath = id;
                    GeckoHtmlElement elm = Functions.GetElementByXpath(wb.Document, xpath);
                    if (elm != null)
                    {
                        switch (elm.TagName)
                        {
                            case "IFRAME":
                                foreach (GeckoFrameElement ifr in wb.Document.GetElementsByTagName("frame"))
                                {
                                    if (ifr == elm.DOMElement)
                                    {
                                        ifr.TextContent = value;
                                        break;
                                    }
                                }
                                break;
                            case "INPUT":
                                GeckoInputElement input = (GeckoInputElement)elm;
                                input.Value = value;
                                input.Focus();
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    Byte[] bytes = Encoding.UTF32.GetBytes(value);
                    StringBuilder asAscii = new StringBuilder();
                    for (int idx = 0; idx < bytes.Length; idx += 4)
                    {
                        uint codepoint = BitConverter.ToUInt32(bytes, idx);
                        if (codepoint <= 127)
                            asAscii.Append(Convert.ToChar(codepoint));
                        else
                            asAscii.AppendFormat("\\u{0:x4}", codepoint);
                    }

                    using (AutoJSContext context = new AutoJSContext(wb.Window))
                    {
                        context.EvaluateScript("document.getElementById('" + id + "').value = '" + asAscii.ToString() + "';");
                        context.EvaluateScript("document.getElementById('" + id + "').scrollIntoView();");
                    }
                }
            }

        }

        public void filldropdown(string id, string value)
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                if (id.StartsWith("/"))
                {
                    string xpath = id;
                    GeckoHtmlElement elm = Functions.GetElementByXpath(wb.Document, xpath);
                    elm.SetAttribute("selectedIndex", value);

                    elm.Focus();
                }
                else
                {
                    using (AutoJSContext context = new AutoJSContext(wb.Window))
                    {
                        string javascript = string.Empty;
                        context.EvaluateScript("document.getElementById('" + id + "').selectedIndex = " + value + ";");
                        JQueryExecutor jquery = new JQueryExecutor(wb.Window);
                        jquery.ExecuteJQuery("$('#" + id + "').trigger('change');");
                        context.EvaluateScript("document.getElementById('" + id + "').scrollIntoView();");
                    }
                }
            }
        }

        public void click(string id)
        {
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                if (id.StartsWith("/"))
                {
                    string xpath = id;
                    GeckoHtmlElement elm = Functions.GetElementByXpath(wb.Document, xpath);
                    if (elm != null)
                        elm.Click();
                    else
                        MessageBox.Show("Click error. No se puede acceder al elemento: " + id);
                }
                else
                {
                    using (AutoJSContext context = new AutoJSContext(wb.Window))
                    {
                        context.EvaluateScript("document.getElementById('" + id + "').click();");
                        context.EvaluateScript("document.getElementById('" + id + "').scrollIntoView();");
                    }
                }
            }
        }

        public void sleep(int seconds, bool isBreakWhenWBCompleted)
        {
            for (int i = 0; i < seconds * 10; i++)
            {
                if (IsStop == false)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(100);

                    Program.mainFrm.toolStripStatus.Text = "Sleep: " + ((i + 1) * 100) + "/" + (seconds * 1000);
                }
                else
                {
                    break;
                }
            }
            Program.mainFrm.toolStripStatus.Text = "";
        }

        public void log(string text)
        {
            Program.mainFrm.tbxPreview.Text += text + "\n";
            //tbxPreview.SelectionStart = tbxPreview.Text.Length;
            //tbxPreview.ScrollToCaret();
            //tbxPreview.Scrolling.ScrollBy(0, tbxPreview.Lines.Count);
            Program.mainFrm.tabControlCode.SelectedTab = Program.mainFrm.tabPreview;
        }

        public void clearlog()
        {
            Program.mainFrm.tbxPreview.Text = "";
        }

        public void createfolder(string path)
        {
            try
            {
                if (System.IO.Directory.Exists(path) == false)
                    System.IO.Directory.CreateDirectory(path);
            }
            catch { }
        }

        public string getfiles(string path)
        {
            string result = "";
            string r = "";

            string[] filePaths = System.IO.Directory.GetFiles(path);

            try
            {
                foreach (string f in filePaths)
                {
                    r += f + ",";
                }
                if (!string.IsNullOrEmpty(r))
                {
                    result = r.Substring(0, r.Length - 1);
                }
                else
                {
                    result = r;
                }
            }
            catch { }

            return result;
        }

        public string getfolders(string path)
        {
            string result = "";
            string r = "";

            try
            {
                string[] directoryPaths = System.IO.Directory.GetDirectories(path);

                foreach (string f in directoryPaths)
                {
                    r += f + ",";
                }
                if (!string.IsNullOrEmpty(r))
                {
                    result = r.Substring(0, r.Length - 1);
                }
                else
                {
                    result = r;
                }
            }
            catch { }

            return result;
        }

        public void download(string savePath, string url)
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.Credentials = System.Net.CredentialCache.DefaultCredentials;
                wc.Proxy = null;
                wc.Headers.Add(System.Net.HttpRequestHeader.UserAgent, "anything");
                try
                {
                    Uri uri = new Uri(url);
                    wc.DownloadFileAsync(uri, savePath);
                }
                catch
                {

                }
            }
        }

        public string read(string path)
        {
            string result = "";
            try
            {
                string[] list = System.IO.File.ReadAllLines(path);
                foreach (string l in list)
                {
                    result += l + "\n";
                }
            }
            catch { }
            return result;
        }

        public void save(string content, string path, bool isOverride)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, isOverride))
                {
                    file.WriteLine(content);
                }
            }
            catch { }
        }

        public void remove(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch { }
        }

        public void removefolder(string path)
        {
            try
            {
                if (System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.Delete(path);
                }
            }
            catch { }
        }

        public void execute(string script)
        {
            ExecuteJSCodeWebBrowser(script);
        }

        public void runcommand(string path, string parameters)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WorkingDirectory = getCurrentPath();
                startInfo.FileName = path;
                startInfo.Arguments = parameters;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                try
                {
                    Process p = Process.Start(startInfo);
                    p.WaitForExit();
                }
                catch { }
            }
            catch { }
        }

        public void reboot()
        {
            System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
        }

        public void shutdown()
        {
            System.Diagnostics.Process.Start("shutdown", "/s /t 0");
        }

        public void hibernate()
        {
            Application.SetSuspendState(PowerState.Hibernate, true, true);
        }

        public void standby()
        {
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }

        public void generatekeys()
        {
            System.Security.Cryptography.CspParameters cspParams = null;
            System.Security.Cryptography.RSACryptoServiceProvider rsaProvider = null;

            string publicKey = "";
            string privateKey = "";

            try
            {
                cspParams = new System.Security.Cryptography.CspParameters();
                cspParams.ProviderType = 1;
                cspParams.Flags = System.Security.Cryptography.CspProviderFlags.UseArchivableKey;
                cspParams.KeyNumber = (int)System.Security.Cryptography.KeyNumber.Exchange;
                rsaProvider = new System.Security.Cryptography.RSACryptoServiceProvider(cspParams);

                publicKey = rsaProvider.ToXmlString(false);
                privateKey = rsaProvider.ToXmlString(true);

                log("Public Key");
                log(publicKey);
                log("");
                log("Private Key");
                log(privateKey);

                Program.mainFrm.tabControlCode.SelectedIndex = 2;
            }
            catch
            {

            }
        }

        public string encrypt(string publicKey, string plainText)
        {
            System.Security.Cryptography.CspParameters cspParams = null;
            System.Security.Cryptography.RSACryptoServiceProvider rsaProvider = null;
            byte[] plainBytes = null;
            byte[] encryptedBytes = null;

            string result = "";
            try
            {
                cspParams = new System.Security.Cryptography.CspParameters();
                cspParams.ProviderType = 1;
                rsaProvider = new System.Security.Cryptography.RSACryptoServiceProvider(cspParams);

                rsaProvider.FromXmlString(publicKey);

                plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                encryptedBytes = rsaProvider.Encrypt(plainBytes, false);
                result = Convert.ToBase64String(encryptedBytes);
            }
            catch { }
            return result;
        }

        public string decrypt(string privateKey, string encrypted)
        {
            System.Security.Cryptography.CspParameters cspParams = null;
            System.Security.Cryptography.RSACryptoServiceProvider rsaProvider = null;
            byte[] encryptedBytes = null;
            byte[] plainBytes = null;

            string result = "";
            try
            {
                cspParams = new System.Security.Cryptography.CspParameters();
                cspParams.ProviderType = 1;
                rsaProvider = new System.Security.Cryptography.RSACryptoServiceProvider(cspParams);

                rsaProvider.FromXmlString(privateKey);

                encryptedBytes = Convert.FromBase64String(encrypted);
                plainBytes = rsaProvider.Decrypt(encryptedBytes, false);

                result = System.Text.Encoding.UTF8.GetString(plainBytes);
            }
            catch { }
            return result;
        }

        public void TakeSnapshot(string location)
        {
            try
            {
                GeckoWebBrowser wbBrowser = Functions.GetCurrentWB();
                ImageCreator creator = new ImageCreator(wbBrowser);
                byte[] rs = creator.CanvasGetPngImage((uint)wbBrowser.Document.ActiveElement.ScrollWidth, (uint)wbBrowser.Document.ActiveElement.ScrollHeight);


                MemoryStream ms = new MemoryStream(rs);
                Image returnImage = Image.FromStream(ms);

                returnImage.Save(location);

            }
            catch { }
        }

        public string imgToText(string xpath, string language)
        {
            string data = string.Empty;
            string path = string.Empty;
            path = Application.StartupPath + "\\captcha\\image.png";
            bool isSaveSuccess = saveImage(xpath, path);

            if (isSaveSuccess)
            {
                string text = Application.StartupPath + "\\captcha\\output.txt";

                string param = "";
                if (language == "vie")
                {
                    param = "\"" + path + "\" \"" + Application.StartupPath + "\\captcha\\output" + "\" -l vie";
                }
                else
                {
                    param = "\"" + path + "\" \"" + Application.StartupPath + "\\captcha\\output" + "\" -l eng";
                }


                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Application.StartupPath + "\\tesseract.exe";
                process.StartInfo.Arguments = param;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();

                data = read(text).Replace("\n", "");

            }

            return data;
        }

        private bool saveImage(string xpath, string location)
        {
            bool result = false;
            try
            {
                GeckoWebBrowser wbBrowser = Functions.GetCurrentWB();
                if (wbBrowser != null)
                {
                    GeckoImageElement element = null;
                    if (xpath.StartsWith("/"))
                        element = (GeckoImageElement)Functions.GetElementByXpath(wbBrowser.Document, xpath);
                    else
                        element = (GeckoImageElement)wbBrowser.Document.GetElementById(xpath);
                    GeckoSelection selection = wbBrowser.Window.Selection;
                    selection.SelectAllChildren(element);
                    wbBrowser.CopyImageContents();
                    if (Clipboard.ContainsImage())
                    {
                        Image img = Clipboard.GetImage();
                        img.Save(location, System.Drawing.Imaging.ImageFormat.Png);
                        result = true;
                    }
                }
            }
            catch { result = false; }

            return result;
        }

        public void FileUpload(string path, string xpath)
        {

        }

        public string getCurrentUrl()
        {
            string url = string.Empty;
            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                url = wb.Url.ToString();
            }
            return url;
        }

        public void scrollto(int value)
        {

        }

        public int getheight()
        {
            int result = 0;



            return result;
        }

        public string gettitle()
        {
            string result = "";

            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                result = wb.DocumentTitle;
            }

            return result;
        }

        public bool checkelement(string xpath)
        {
            bool result = false;



            return result;
        }

        public string getCurrentContent()
        {
            string result = "";

            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                result = wb.Document.Body.InnerHtml;
            }

            return result;
        }

        public string getCurrentPath()
        {
            string result = "";
            try
            {
                result = Application.StartupPath;
            }
            catch { }
            return result;
        }

        public void explorer(string path)
        {
            string argument = "/select, \"" + path + "\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        public void loadHTML(string path)
        {
            go(path);
        }

        public string textToJSON(string text)
        {
            return text;
        }


        [ComImport, InterfaceType((short)1), Guid("3050F669-98B5-11CF-BB82-00AA00BDCE0B")]
        private interface IHTMLElementRenderFixed
        {
            void DrawToDC(IntPtr hdc);
            void SetDocumentPrinter(string bstrPrinterName, IntPtr hdc);
        }

        public void SaveImageFromElement(string xpath, string path)
        {

        }

        public string GetCurrentMouseX()
        {
            return frmMain.MousePosition.X.ToString();
        }

        public string GetCurrentMouseY()
        {
            return frmMain.MousePosition.Y.ToString();
        }

        public void Mouse_Down(string mouseButton, int LastTime)
        {
            WaitApp(LastTime);

            if (mouseButton == "Left")
            {
                FSMouseKeyboardLibrary.MouseSimulator.MouseDown(FSMouseKeyboardLibrary.MouseButton.Left);
            }
            else if (mouseButton == "Right")
            {
                FSMouseKeyboardLibrary.MouseSimulator.MouseDown(FSMouseKeyboardLibrary.MouseButton.Right);
            }
            else if (mouseButton == "Middle")
            {
                FSMouseKeyboardLibrary.MouseSimulator.MouseDown(FSMouseKeyboardLibrary.MouseButton.Middle);
            }
        }

        public void Mouse_Up(string mouseButton, int LastTime)
        {
            WaitApp(LastTime);

            if (mouseButton == "Left")
            {
                FSMouseKeyboardLibrary.MouseSimulator.MouseUp(FSMouseKeyboardLibrary.MouseButton.Left);
            }
            else if (mouseButton == "Right")
            {
                FSMouseKeyboardLibrary.MouseSimulator.MouseUp(FSMouseKeyboardLibrary.MouseButton.Right);
            }
            else if (mouseButton == "Middle")
            {
                FSMouseKeyboardLibrary.MouseSimulator.MouseUp(FSMouseKeyboardLibrary.MouseButton.Middle);
            }

        }

        public void Mouse_Click(string mouseButton, int LastTime)
        {
            WaitApp(LastTime);

            if (mouseButton == "Left")
            {
                FSMouseKeyboardLibrary.MouseSimulator.Click(FSMouseKeyboardLibrary.MouseButton.Left);
            }
            else if (mouseButton == "Right")
            {
                FSMouseKeyboardLibrary.MouseSimulator.Click(FSMouseKeyboardLibrary.MouseButton.Right);
            }
            else if (mouseButton == "Middle")
            {
                FSMouseKeyboardLibrary.MouseSimulator.Click(FSMouseKeyboardLibrary.MouseButton.Middle);
            }
        }

        public void Mouse_Double_Click(string mouseButton, int LastTime)
        {
            WaitApp(LastTime);

            if (mouseButton == "Left")
            {
                FSMouseKeyboardLibrary.MouseSimulator.DoubleClick(FSMouseKeyboardLibrary.MouseButton.Left);
            }
            else if (mouseButton == "Right")
            {
                FSMouseKeyboardLibrary.MouseSimulator.DoubleClick(FSMouseKeyboardLibrary.MouseButton.Right);
            }
            else if (mouseButton == "Middle")
            {
                FSMouseKeyboardLibrary.MouseSimulator.DoubleClick(FSMouseKeyboardLibrary.MouseButton.Middle);
            }

        }

        public void Mouse_Show(int x, int y, bool isShow, int LastTime)
        {
            WaitApp(LastTime);

            FSMouseKeyboardLibrary.MouseSimulator.X = x;
            FSMouseKeyboardLibrary.MouseSimulator.Y = y;

            if (isShow)
            {
                FSMouseKeyboardLibrary.MouseSimulator.Show();
            }
            else if (isShow == false)
            {
                FSMouseKeyboardLibrary.MouseSimulator.Hide();
            }
        }

        public void Mouse_Wheel(int delta, int LastTime)
        {
            WaitApp(LastTime);

            FSMouseKeyboardLibrary.MouseSimulator.MouseWheel(delta);
        }

        public void Key_Down(string key, int LastTime)
        {
            WaitApp(LastTime);

            KeysConverter k = new KeysConverter();
            Keys mykey = (Keys)k.ConvertFromString(key);
            FSMouseKeyboardLibrary.KeyboardSimulator.KeyDown(mykey);
        }

        public void Key_Up(string key, int LastTime)
        {
            WaitApp(LastTime);

            KeysConverter k = new KeysConverter();
            Keys mykey = (Keys)k.ConvertFromString(key);
            FSMouseKeyboardLibrary.KeyboardSimulator.KeyUp(mykey);
        }

        private void WaitApp(int seconds)
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(seconds);
        }



        public string RemoveStopWords(string text)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("\n", "");
                List<string> data = new List<string>();
                string[] array = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string stopwordPath = getCurrentPath() + "\\resources\\stopword.xml";
                XDocument xdoc = XDocument.Load(stopwordPath);
                foreach (string arr in array)
                {
                    var rs = (from w in xdoc.Descendants("w") where w.Value == arr select w).FirstOrDefault();
                    if (rs == null)
                    {
                        data.Add(arr);
                    }
                }
                result = string.Join(",", data);
            }

            return result;
        }

        public void AddElement(string path, string node1, string node2, string text)
        {
            XDocument xdoc = XDocument.Load(path);
            var item = xdoc.Element(node1).Element(node2);
            if (item != null)
                item.Add(new XElement("w", text));
            else
                xdoc.Element(node1).Add(new XElement(node2, new XElement("w", text)));

            xdoc.Save(path);
        }

        public bool CheckXmlElement(string path, string node, string text)
        {
            bool result = false;

            XDocument xdoc = XDocument.Load(path);

            var rs = (from w in xdoc.Descendants(node) where w.Value == text select w).FirstOrDefault();
            if (rs != null) result = true;

            return result;
        }

        public string GetParentElement(string path, string node, string text)
        {
            string result = string.Empty;

            XDocument xdoc = XDocument.Load(path);

            var rs = (from w in xdoc.Descendants(node) where w.Value == text select w).FirstOrDefault();
            if (rs != null) result = rs.Parent.Name.LocalName;

            return result;
        }

        public string GetXmlElement(string path, string node)
        {
            string result = string.Empty;
            List<string> data = new List<string>();
            XDocument xdoc = XDocument.Load(path);
            var list = xdoc.Root.Nodes();
            foreach (XElement elem in list)
            {
                data.Add(elem.Name.LocalName);
            }
            result = string.Join(",", data);
            return result;
        }

        public string ExtractUsingRegularExpression(string pattern, string groupName)
        {
            string result = string.Empty;

            GeckoWebBrowser wb = Functions.GetCurrentWB();
            if (wb != null)
            {
                string doc = wb.Document.Body.TextContent;
                Match m = Regex.Match(doc, pattern);
                if (m.Success)
                {
                    if (m.Groups.Count > 0)
                    {
                        result = m.Groups[groupName].Value;
                    }
                }
            }

            return result;
        }

        public void AddToDownload(string fileName, string url, string folder)
        {
            Program.mainFrm.downloadList1.NewFileDownload(fileName, url, folder, 1);
        }

        public void StartDownload()
        {
            Program.mainFrm.tabMain.SelectedTab = Program.mainFrm.tabDownload;
            Program.mainFrm.downloadList1.SelectAll();
            Program.mainFrm.downloadList1.StartSelections();
        }
    }
}
