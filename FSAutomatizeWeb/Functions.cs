using FSFormControls;
using Gecko;
using System;
using System.Text;
using System.Windows.Forms;

namespace FSAutomatizeWeb
{
    class Functions
    {
        #region functions

        public static GeckoWebBrowser GetCurrentWB()
        {
            if (Program.mainFrm.tabMain.SelectedTab != null)
            {
                if (Program.mainFrm.tabMain.SelectedTab.Controls.Count > 0)
                {
                    Control ctr = Program.mainFrm.tabMain.SelectedTab.Controls[0];
                    if (ctr != null)
                    {
                        return (GeckoWebBrowser)ctr;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get short xpath
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string _GetSmallXpath(GeckoNode node)
        {
            if (node == null)
                return "";
            if (node.NodeType == NodeType.Attribute)
            {
                return String.Format("{0}/@{1}", _GetSmallXpath(((GeckoAttribute)node).OwnerDocument), node.NodeName);
            }
            if (node.ParentNode == null)
            {
                return "";
            }
            string elementId = ((GeckoHtmlElement)node).Id;
            if (!String.IsNullOrEmpty(elementId))
            {
                return String.Format("//*[@id=\"{0}\"]", elementId);
            }
            int indexInParent = 1;
            GeckoNode siblingNode = node.PreviousSibling;
            while (siblingNode != null)
            {
                if (siblingNode.NodeName == node.NodeName)
                {
                    indexInParent++;
                }
                siblingNode = siblingNode.PreviousSibling;
            }
            return String.Format("{0}/{1}[{2}]", _GetSmallXpath(node.ParentNode), node.NodeName, indexInParent);
        }

        /// <summary>
        /// Get long xpath
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string _GetXpath(GeckoNode node)
        {
            if (node == null)
                return "";
            if (node.NodeType == NodeType.Attribute)
            {
                return String.Format("{0}/@{1}", _GetXpath(((GeckoAttribute)node).OwnerDocument), node.NodeName);
            }
            if (node.ParentNode == null)
            {
                return "";
            }
            int indexInParent = 1;
            GeckoNode siblingNode = node.PreviousSibling;
            while (siblingNode != null)
            {
                if (siblingNode.NodeName == node.NodeName)
                {
                    indexInParent++;
                }
                siblingNode = siblingNode.PreviousSibling;
            }
            return String.Format("{0}/{1}[{2}]", _GetXpath(node.ParentNode), node.NodeName, indexInParent);
        }


        //public static string GetXPath(TreeNode treeNode)
        //{
        //    string xpath = "";
        //    while (treeNode != null)
        //    {
        //        int ind = GetXpathIndex(treeNode);
        //        if (ind > 1)
        //            xpath = "/" + treeNode.Text.ToLower() + "[" + ind + "]" + xpath;
        //        else
        //            xpath = "/" + treeNode.Text.ToLower() + xpath;

        //        treeNode = treeNode.Parent;
        //    }
        //    return xpath;
        //}
        public static string GetXPath(GeckoHtmlElement geckoHtmlElement)
        {
            string xpath = "";
            while (geckoHtmlElement != null)
            {
                int ind = GetXpathIndex(geckoHtmlElement);
                if (ind > 1)
                    xpath = "/" + geckoHtmlElement.TagName.ToLower() + "[" + ind + "]" + xpath;
                else
                    xpath = "/" + geckoHtmlElement.TagName.ToLower() + xpath;

                geckoHtmlElement = geckoHtmlElement.Parent;
            }
            return xpath;
        }

        public static GeckoHtmlElement GetElementByXpath(GeckoDocument doc, string xpath)
        {
            if (doc == null) return null;

            xpath = xpath.Replace("/html/", "");
            GeckoElementCollection eleColec = doc.GetElementsByTagName("html"); if (eleColec.Length == 0) return null;
            GeckoHtmlElement ele = eleColec[0];
            string[] tagList = xpath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string tag in tagList)
            {
                System.Text.RegularExpressions.Match mat = System.Text.RegularExpressions.Regex.Match(tag, "(?<tag>.+)\\[@id='(?<id>.+)'\\]");
                if (mat.Success == true)
                {
                    string id = mat.Groups["id"].Value;
                    GeckoHtmlElement tmpEle = doc.GetHtmlElementById(id);
                    if (tmpEle != null) ele = tmpEle;
                    else
                    {
                        ele = null;
                        break;
                    }
                }
                else
                {
                    mat = System.Text.RegularExpressions.Regex.Match(tag, "(?<tag>.+)\\[(?<ind>[0-9]+)\\]");
                    if (mat.Success == false)
                    {
                        GeckoHtmlElement tmpEle = null;
                        foreach (GeckoNode it in ele.ChildNodes)
                        {
                            if (it.NodeName.ToLower() == tag)
                            {
                                tmpEle = (GeckoHtmlElement)it;
                                break;
                            }
                        }
                        if (tmpEle != null) ele = tmpEle;
                        else
                        {
                            ele = null;
                            break;
                        }
                    }
                    else
                    {
                        string tagName = mat.Groups["tag"].Value;
                        int ind = int.Parse(mat.Groups["ind"].Value);
                        int count = 0;
                        GeckoHtmlElement tmpEle = null;
                        foreach (GeckoNode it in ele.ChildNodes)
                        {
                            if (it.NodeName.ToLower() == tagName)
                            {
                                count++;
                                if (ind == count)
                                {
                                    tmpEle = (GeckoHtmlElement)it;
                                    break;
                                }
                            }
                        }
                        if (tmpEle != null) ele = tmpEle;
                        else
                        {
                            ele = null;
                            break;
                        }
                    }
                }
            }

            return ele;
        }

        private static int GetXpathIndex(GeckoHtmlElement geckoHtmlElement)
        {
            if (geckoHtmlElement.Parent == null) return 0;
            int ind = 0, indEle = 0;
            string tagName = geckoHtmlElement.TagName;
            GeckoNodeCollection elecol = geckoHtmlElement.Parent.ChildNodes;
            foreach (GeckoNode it in elecol)
            {
                if (it.NodeName == tagName)
                {
                    ind++;
                    if (it.TextContent == geckoHtmlElement.TextContent)
                        indEle = ind;
                }
            }
            if (ind > 1) return indEle;
            return 0;
        }

        //private static int GetXpathIndex(TreeNode treeNode)
        //{
        //    if (treeNode.Parent == null) return 0;
        //    int ind = 0, indEle = 0;
        //    string tagName = treeNode.Text;
        //    TreeNodeCollection elecol = treeNode.Parent.Nodes;
        //    foreach (TreeNode it in elecol)
        //    {
        //        if (it.Text == tagName)
        //        {
        //            ind++;
        //            if (it.Tag == treeNode.Tag)
        //                indEle = ind;
        //        }
        //    }
        //    if (ind > 1) return indEle;
        //    return 0;
        //}

        //public static TreeNode FindNodeByXPath(string xpath)
        //{
        //    return FindNodeByXPath(xpath, Program.mainFrm.tvHtml.Nodes);
        //}

        //public static TreeNode FindNodeByXPath(string xpath, TreeNodeCollection nodes)
        //{
        //    foreach(TreeNode node in nodes)
        //    {
        //        if (GetXPath(node) == xpath)
        //            return node;

        //        if (node.Nodes.Count > 0)
        //        {
        //            TreeNode tn = FindNodeByXPath(xpath, node.Nodes);
        //            if (tn != null)
        //                return tn;
        //        }
        //    }

        //    return null;
        //}

        public static void InitContextMenu()
        {
            Program.mainFrm.contextMenuBrowser.Items.Clear();

            var goItem = new ToolStripMenuItem(Language.Resource.Go);
            var sleepItem = new ToolStripMenuItem(Language.Resource.Sleep);
            var extractItem = new ToolStripMenuItem(Language.Resource.Extract);
            var fillItem = new ToolStripMenuItem(Language.Resource.Fill);
            var clickItem = new ToolStripMenuItem(Language.Resource.Click);

            goItem.Click += item_Click;
            sleepItem.Click += item_Click;

            var attributeItem = new ToolStripMenuItem(Language.Resource.Attribute);
            var htmlItem = new ToolStripMenuItem(Language.Resource.Html);
            var srcItem = new ToolStripMenuItem(Language.Resource.Src);
            var textItem = new ToolStripMenuItem(Language.Resource.Text);
            var urlItem = new ToolStripMenuItem(Language.Resource.Url);
            var repeatItem = new ToolStripMenuItem(Language.Resource.RepeatElement);

            attributeItem.Click += item_Click;
            htmlItem.Click += item_Click;
            srcItem.Click += item_Click;
            textItem.Click += item_Click;
            urlItem.Click += item_Click;
            repeatItem.Click += item_Click;

            extractItem.DropDownItems.Add(attributeItem);
            extractItem.DropDownItems.Add(htmlItem);
            extractItem.DropDownItems.Add(srcItem);
            extractItem.DropDownItems.Add(textItem);
            extractItem.DropDownItems.Add(urlItem);
            extractItem.DropDownItems.Add(repeatItem);

            var textboxItem = new ToolStripMenuItem(Language.Resource.Textbox);
            var dropdownItem = new ToolStripMenuItem(Language.Resource.Dropdown);
            var iframeItem = new ToolStripMenuItem(Language.Resource.iFrame);

            textboxItem.Click += item_Click;
            dropdownItem.Click += item_Click;
            iframeItem.Click += item_Click;

            fillItem.DropDownItems.Add(textboxItem);
            fillItem.DropDownItems.Add(dropdownItem);
            fillItem.DropDownItems.Add(iframeItem);

            var elementItem = new ToolStripMenuItem(Language.Resource.Element);
            var fileUploadItem = new ToolStripMenuItem(Language.Resource.FileUpload);

            elementItem.Click += item_Click;
            fileUploadItem.Click += item_Click;

            clickItem.DropDownItems.Add(elementItem);
            clickItem.DropDownItems.Add(fileUploadItem);

            var mouseItem = new ToolStripMenuItem(Language.Resource.Mouse);
            var currentMouseItem = new ToolStripMenuItem(Language.Resource.GetCurrentMouse);
            var mouseMoveItem = new ToolStripMenuItem(Language.Resource.MouseMove);
            var mouseDownItem = new ToolStripMenuItem(Language.Resource.MouseDown);
            var mouseUpItem = new ToolStripMenuItem(Language.Resource.MouseUp);
            var mouseClickItem = new ToolStripMenuItem(Language.Resource.MouseClick);
            var mouseDoubleClickItem = new ToolStripMenuItem(Language.Resource.MouseDoubleClick);
            var mouseWheelItem = new ToolStripMenuItem(Language.Resource.MouseWheel);

            currentMouseItem.Click += item_Click;
            mouseMoveItem.Click += item_Click;
            mouseDownItem.Click += item_Click;
            mouseUpItem.Click += item_Click;
            mouseClickItem.Click += item_Click;
            mouseDoubleClickItem.Click += item_Click;
            mouseWheelItem.Click += item_Click;

            mouseItem.DropDownItems.Add(currentMouseItem);
            mouseItem.DropDownItems.Add(mouseMoveItem);
            mouseItem.DropDownItems.Add(mouseDownItem);
            mouseItem.DropDownItems.Add(mouseUpItem);
            mouseItem.DropDownItems.Add(mouseClickItem);
            mouseItem.DropDownItems.Add(mouseDoubleClickItem);
            mouseItem.DropDownItems.Add(mouseWheelItem);

            var keyboardItem = new ToolStripMenuItem(Language.Resource.Keyboard);
            var keyDownItem = new ToolStripMenuItem(Language.Resource.KeyDown);
            var keyUpItem = new ToolStripMenuItem(Language.Resource.KeyUp);

            keyDownItem.Click += item_Click;
            keyUpItem.Click += item_Click;

            keyboardItem.DropDownItems.Add(keyDownItem);
            keyboardItem.DropDownItems.Add(keyUpItem);

            var sqlItem = new ToolStripMenuItem(Language.Resource.Sql);
            var getDatabaseItem = new ToolStripMenuItem(Language.Resource.GetDatabase);
            var getTableItem = new ToolStripMenuItem(Language.Resource.GetTable);
            var getColumnItem = new ToolStripMenuItem(Language.Resource.GetColumn);
            var getRowItem = new ToolStripMenuItem(Language.Resource.GetRow);
            var executeQueryItem = new ToolStripMenuItem(Language.Resource.ExecuteQuery);

            getDatabaseItem.Click += item_Click;
            getTableItem.Click += item_Click;
            getColumnItem.Click += item_Click;
            getRowItem.Click += item_Click;
            executeQueryItem.Click += item_Click;

            sqlItem.DropDownItems.Add(getDatabaseItem);
            sqlItem.DropDownItems.Add(getTableItem);
            sqlItem.DropDownItems.Add(getColumnItem);
            sqlItem.DropDownItems.Add(getRowItem);
            sqlItem.DropDownItems.Add(executeQueryItem);

            var utilityItem = new ToolStripMenuItem(Language.Resource.Utility);
            var imageToTextItem = new ToolStripMenuItem(Language.Resource.ImageToText);
            var takesnapshotItem = new ToolStripMenuItem(Language.Resource.TakeSnapShot);
            var textToJsonItem = new ToolStripMenuItem(Language.Resource.TextToJson);
            var textToXmlItem = new ToolStripMenuItem(Language.Resource.TextToXml);
            var getAccountByItem = new ToolStripMenuItem(Language.Resource.GetAccount);
            var sendEmailItem = new ToolStripMenuItem(Language.Resource.SendEmail);

            imageToTextItem.Click += item_Click;
            takesnapshotItem.Click += item_Click;
            textToJsonItem.Click += item_Click;
            textToXmlItem.Click += item_Click;
            getAccountByItem.Click += item_Click;
            sendEmailItem.Click += item_Click;

            utilityItem.DropDownItems.Add(imageToTextItem);
            utilityItem.DropDownItems.Add(takesnapshotItem);
            utilityItem.DropDownItems.Add(textToJsonItem);
            utilityItem.DropDownItems.Add(textToXmlItem);
            utilityItem.DropDownItems.Add(getAccountByItem);
            utilityItem.DropDownItems.Add(sendEmailItem);

            var excelItem = new ToolStripMenuItem(Language.Resource.Excel);
            var readExcelItem = new ToolStripMenuItem(Language.Resource.ReadCellExcel);
            var writeExcelItem = new ToolStripMenuItem(Language.Resource.WriteCellExcel);

            readExcelItem.Click += item_Click;
            writeExcelItem.Click += item_Click;

            excelItem.DropDownItems.Add(readExcelItem);
            excelItem.DropDownItems.Add(writeExcelItem);

            var exploreItem = new ToolStripMenuItem(Language.Resource.Explore);
            var runCommandItem = new ToolStripMenuItem(Language.Resource.RunCommand);
            var createfolderItem = new ToolStripMenuItem(Language.Resource.CreateFolder);
            var getfoldersItem = new ToolStripMenuItem(Language.Resource.GetFolders);
            var removefolderItem = new ToolStripMenuItem(Language.Resource.RemoveFolder);
            var createfileItem = new ToolStripMenuItem(Language.Resource.CreateFile);
            var getfilesItem = new ToolStripMenuItem(Language.Resource.GetFiles);
            var removefileItem = new ToolStripMenuItem(Language.Resource.RemoveFile);
            var downloadFileItem = new ToolStripMenuItem(Language.Resource.Download);
            var openExploreItem = new ToolStripMenuItem(Language.Resource.OpenExplore);
            var showCodeItem = new ToolStripMenuItem(Language.Resource.ShowCode);
            var showInTreeItem = new ToolStripMenuItem(Language.Resource.ShowInTree);

            runCommandItem.Click += item_Click;
            createfolderItem.Click += item_Click;
            getfoldersItem.Click += item_Click;
            removefolderItem.Click += item_Click;
            createfileItem.Click += item_Click;
            getfilesItem.Click += item_Click;
            removefileItem.Click += item_Click;
            downloadFileItem.Click += item_Click;
            openExploreItem.Click += item_Click;
            showCodeItem.Click += item_Click;
            showInTreeItem.Click += item_Click;

            exploreItem.DropDownItems.Add(runCommandItem);
            exploreItem.DropDownItems.Add(createfolderItem);
            exploreItem.DropDownItems.Add(getfoldersItem);
            exploreItem.DropDownItems.Add(removefolderItem);
            exploreItem.DropDownItems.Add(createfileItem);
            exploreItem.DropDownItems.Add(getfilesItem);
            exploreItem.DropDownItems.Add(removefileItem);
            exploreItem.DropDownItems.Add(downloadFileItem);
            exploreItem.DropDownItems.Add(openExploreItem);

            Program.mainFrm.contextMenuBrowser.Items.Add(goItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(sleepItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(extractItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(fillItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(clickItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(mouseItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(keyboardItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(sqlItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(utilityItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(excelItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(exploreItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(showCodeItem);
            Program.mainFrm.contextMenuBrowser.Items.Add(showInTreeItem);
        }

        static int repeatCount = 0;
        static string repeatItem = string.Empty;

        static void item_Click(object sender, EventArgs e)
        {
            Program.mainFrm.tabControlCode.SelectedTab = Program.mainFrm.tabScript;

            string xpath = Program.mainFrm.wbMainBrowser.htmlElm.GetAttribute("id");
            Program.mainFrm.contextMenuBrowser.Hide();

            if (string.IsNullOrEmpty(xpath))
            {
                xpath = GetXPath(Program.mainFrm.wbMainBrowser.htmlElm);
            }

            string item = sender.ToString();


            if (item == Language.Resource.GetCurrentMouse)
            {
                Program.mainFrm.tbxCode.AppendText("var x = getCurrentMouseX();" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var y = getCurrentMouseY();" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("//Show current position of mouse in " + Language.Resource.Preview + " tab" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(x + ',' + y);" + Environment.NewLine);
            }
            else if (item == Language.Resource.MouseMove)
            {
                var x = Program.mainFrm.wbMainBrowser.GetCurrentMouseX();
                var y = Program.mainFrm.wbMainBrowser.GetCurrentMouseY();
                Program.mainFrm.tbxCode.AppendText("//MouseMove(Position X, Position Y, IsShowMouseWhenMove, Delay Time)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("//IsShowMouseWhenMove: true/false" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("MouseMove(" + x + ", " + y + ", true, 0);" + Environment.NewLine);
            }
            else if (item == Language.Resource.MouseDown)
            {
                Program.mainFrm.tbxCode.AppendText("//MouseDown(MouseButton, Delay Time)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("//MouseButton: Left, Right or Middle" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("MouseDown('Left', 0);" + Environment.NewLine);
            }
            else if (item == Language.Resource.MouseUp)
            {
                Program.mainFrm.tbxCode.AppendText("//MouseUp(MouseButton, Delay Time)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("//MouseButton: Left, Right or Middle" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("MouseUp('Left', 0);" + Environment.NewLine);
            }
            else if (item == Language.Resource.MouseClick)
            {
                Program.mainFrm.tbxCode.AppendText("//MouseClick(MouseButton, Delay Time)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("//MouseButton: Left, Right or Middle" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("MouseClick('Left', 0);" + Environment.NewLine);
            }
            else if (item == Language.Resource.MouseDoubleClick)
            {
                Program.mainFrm.tbxCode.AppendText("//MouseDoubleClick(MouseButton, Delay Time)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("//MouseButton: Left, Right or Middle" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("MouseDoubleClick('Left', 0);" + Environment.NewLine);
            }
            else if (item == Language.Resource.MouseWheel)
            {
                Program.mainFrm.tbxCode.AppendText("//MouseWheel(Delta, Delay Time)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("MouseWheel(-15, 0);" + Environment.NewLine);
            }
            else if (item == Language.Resource.KeyDown)
            {
                Program.mainFrm.tbxCode.AppendText("//KeyDown(KeyCode, Delay Time)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("//KeyCode: Keyboard characters" + Environment.NewLine);

                Program.mainFrm.tbxCode.AppendText("KeyDown('A', 0);" + Environment.NewLine);
            }
            else if (item == Language.Resource.KeyUp)
            {
                Program.mainFrm.tbxCode.AppendText("//KeyUp(KeyCode, Delay Time)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("//KeyCode: Keyboard characters" + Environment.NewLine);

                Program.mainFrm.tbxCode.AppendText("KeyUp('A', 0);" + Environment.NewLine);
            }
            else if (item == Language.Resource.GetDatabase)
            {
                Program.mainFrm.tbxCode.AppendText("//List All Database" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("//Please login and create new item in Connection tab before use this function" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var listDatabases = getDatabases('localhost');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(listDatabases);" + Environment.NewLine);
            }
            else if (item == Language.Resource.GetTable)
            {
                Program.mainFrm.tbxCode.AppendText("//List Tables from Database" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var tables = getTables('name', 'dbName');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(tables);" + Environment.NewLine);
            }
            else if (item == Language.Resource.GetColumn)
            {
                Program.mainFrm.tbxCode.AppendText("//List columns from table" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var columns = getColumns('name', 'dbName', 'table');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(columns);" + Environment.NewLine);
            }
            else if (item == Language.Resource.GetRow)
            {
                Program.mainFrm.tbxCode.AppendText("//List rows from table" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var rows = getRows('name', 'dbName', 'sql');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(rows);" + Environment.NewLine);
            }
            else if (item == Language.Resource.ExecuteQuery)
            {
                Program.mainFrm.tbxCode.AppendText("//Execute query from database" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var query = excuteQuery('name', 'dbName', 'sql');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(query);" + Environment.NewLine);
            }
            else if (item == Language.Resource.ImageToText)
            {
                Program.mainFrm.tbxCode.AppendText("var text = imageToText('" + xpath + "', 'vie');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(text);" + Environment.NewLine);
            }
            else if (item == Language.Resource.TakeSnapShot)
            {
                Program.mainFrm.tbxCode.AppendText("//Take Snapshot (Still not implement)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var location = getCurrentPath() + '\\image.png';" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("takesnapshot(location);" + Environment.NewLine);

            }
            else if (item == Language.Resource.TextToJson)
            {
                Program.mainFrm.tbxCode.AppendText("//Text to JSON" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var text = textToJSON('{data: 123}');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(text.data);" + Environment.NewLine);
            }
            else if (item == Language.Resource.TextToXml)
            {
                Program.mainFrm.tbxCode.AppendText("//Text to XML (Not test)" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var text = stringtoXML('text');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(text);" + Environment.NewLine);
            }
            else if (item == Language.Resource.GetAccount)
            {
                Program.mainFrm.tbxCode.AppendText("//Get Account By Name" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var account = getAccountBy('name');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(account.Username + ' - ' + account.Password);" + Environment.NewLine);
            }
            else if (item == Language.Resource.SendEmail)
            {
                Program.mainFrm.tbxCode.AppendText("//Send Email" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var email = sendEmail('name', 'email', 'subject', 'content');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(email);" + Environment.NewLine);
            }
            else if (item == Language.Resource.ReadCellExcel)
            {
                Program.mainFrm.tbxCode.AppendText("//Read Cell in Excel file" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var readItem = readCellExcel('filePath', 'sheetname', 'row', 'column');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(readItem);" + Environment.NewLine);
            }
            else if (item == Language.Resource.WriteCellExcel)
            {
                Program.mainFrm.tbxCode.AppendText("//Write Cell in Excel file" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("writeCellExcel('filePath', 'sheetname', 'A1', 'value');" + Environment.NewLine);
            }
            else if (item == Language.Resource.RunCommand)
            {
                Program.mainFrm.tbxCode.AppendText("//Run Command Line" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("runcommand('path', 'parameters');" + Environment.NewLine);
            }
            else if (item == Language.Resource.CreateFolder)
            {
                Program.mainFrm.tbxCode.AppendText("//Create Folder" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("createfolder('path');" + Environment.NewLine);
            }
            else if (item == Language.Resource.GetFolders)
            {
                Program.mainFrm.tbxCode.AppendText("//List Folder" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var folders = getfolders('path');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(folders);" + Environment.NewLine);
            }
            else if (item == Language.Resource.RemoveFolder)
            {
                Program.mainFrm.tbxCode.AppendText("//Remove Folder" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("removefolder('path');" + Environment.NewLine);
            }
            else if (item == Language.Resource.CreateFile)
            {
                Program.mainFrm.tbxCode.AppendText("//Create File" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("save('content', 'path', 'isAppend');" + Environment.NewLine);
            }
            else if (item == Language.Resource.GetFiles)
            {
                Program.mainFrm.tbxCode.AppendText("//List File" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("var files= getfiles('path');" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(files);" + Environment.NewLine);
            }
            else if (item == Language.Resource.RemoveFile)
            {
                Program.mainFrm.tbxCode.AppendText("//Remove File" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("remove('path');" + Environment.NewLine);
            }
            else if (item == Language.Resource.Download)
            {
                Program.mainFrm.tbxCode.AppendText("//Download File" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("download('url', 'path');" + Environment.NewLine);
            }
            else if (item == Language.Resource.OpenExplore)
            {
                Program.mainFrm.tbxCode.AppendText("//Open Explorer" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("explorer('path');" + Environment.NewLine);
            }
            else if (item == Language.Resource.Go)
            {
                Program.mainFrm.tbxCode.AppendText("//Open Website" + Environment.NewLine);
                if (MessageBox.Show(Language.Resource.ConfirmGoWebsite, Language.Resource.Message, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string address = Program.mainFrm.tbxAddress.Text;

                    FSFormControls.frmInputBox frmInput = new frmInputBox(Language.Resource.Website, Language.Resource.Message, address, false);
                    frmInput.ShowDialog();

                    string promptValue = frmInput.InputText;
                    if (!string.IsNullOrEmpty(promptValue))
                    {
                        Program.mainFrm.tbxCode.AppendText("go(\"" + promptValue + "\");\n");
                    }
                }
                else
                {
                    Program.mainFrm.tbxCode.AppendText("go(\"" + xpath + "\");\n");
                }
            }
            else if (item == Language.Resource.Sleep)
            {
                FSFormControls.frmInputBox frmInput = new frmInputBox(Language.Resource.Sleep, Language.Resource.Message, "1", false);
                frmInput.ShowDialog();

                string promptValue = frmInput.InputText;
                if (MessageBox.Show(Language.Resource.ConfirmSleep, Language.Resource.Message, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Program.mainFrm.tbxCode.AppendText("sleep(" + promptValue + ",true);\n");
                }
                else
                {
                    Program.mainFrm.tbxCode.AppendText("sleep(" + promptValue + ",false);\n");
                }
            }
            else if (item == Language.Resource.Attribute)
            {
                FSFormControls.frmInputBox frmInput = new frmInputBox(Language.Resource.Attribute, Language.Resource.Message, "", false);
                frmInput.ShowDialog();

                string promptValue = frmInput.InputText;
                if (!string.IsNullOrEmpty(promptValue))
                {
                    Program.mainFrm.tbxCode.AppendText("var attribute = extract(\"" + xpath + "\", \"" + promptValue + "\");" + Environment.NewLine);
                    Program.mainFrm.tbxCode.AppendText("log(attribute);" + Environment.NewLine);
                }
            }
            else if (item == Language.Resource.Html)
            {
                Program.mainFrm.tbxCode.AppendText("var html = extract(\"" + xpath + "\", \"html\");" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(html);" + Environment.NewLine);
            }
            else if (item == Language.Resource.Src)
            {
                Program.mainFrm.tbxCode.AppendText("var src = extract(\"" + xpath + "\", \"src\");" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(src);" + Environment.NewLine);
            }
            else if (item == Language.Resource.Text)
            {
                Program.mainFrm.tbxCode.AppendText("var text = extract(\"" + xpath + "\", \"text\");" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(text);" + Environment.NewLine);
            }
            else if (item == Language.Resource.Url)
            {
                Program.mainFrm.tbxCode.AppendText("var url = extract(\"" + xpath + "\", \"href\");" + Environment.NewLine);
                Program.mainFrm.tbxCode.AppendText("log(url);" + Environment.NewLine);
            }
            else if (item == Language.Resource.ShowCode)
            {
                Program.mainFrm.txtSourceCode.Clear();
                Program.mainFrm.txtSourceCode.AppendText(Program.mainFrm.wbMainBrowser.DocumentText);
                Program.mainFrm.tabSourceCode.Select();
                Program.mainFrm.tabControlCode.SelectedTab = Program.mainFrm.tabSourceCode;
            }
            else if (item == Language.Resource.ShowInTree)
            {
                xpath = GetXPath(Program.mainFrm.wbMainBrowser.htmlElm);

                DBTreeViewNode node = Program.mainFrm.tvHtml.FindNodeByXPath(xpath);

                if (node != null)
                {
                    Program.mainFrm.tabControlCode.SelectedTab = Program.mainFrm.tabHtmlTree;
                    Program.mainFrm.tvHtml.SelectedNode = node;
                    Program.mainFrm.tvHtml.Focus();
                    Program.mainFrm.ShowNode(node);
                }
            }
            else if (item == Language.Resource.Textbox)
            {
                FSFormControls.frmInputBox frmInput = new frmInputBox(Language.Resource.Textbox, Language.Resource.Message, "", false);
                frmInput.ShowDialog();

                string promptValue = frmInput.InputText;
                if (!string.IsNullOrEmpty(promptValue))
                    Program.mainFrm.tbxCode.AppendText("fill(\"" + xpath + "\", \"" + promptValue + "\");" + Environment.NewLine);
            }
            else if (item == Language.Resource.Dropdown)
            {
                FSFormControls.frmInputBox frmInput = new frmInputBox(Language.Resource.Dropdown, Language.Resource.Message, "", false);
                frmInput.ShowDialog();

                string promptValue = frmInput.InputText;
                if (!string.IsNullOrEmpty(promptValue))
                    Program.mainFrm.tbxCode.AppendText("filldropdown(\"" + xpath + "\", \"" + promptValue + "\");" + Environment.NewLine);
            }
            else if (item == Language.Resource.iFrame)
            {
                FSFormControls.frmInputBox frmInput = new frmInputBox(Language.Resource.iFrame, Language.Resource.Message, "", false);
                frmInput.ShowDialog();

                string promptValue = frmInput.InputText;
                if (!string.IsNullOrEmpty(promptValue))
                    Program.mainFrm.tbxCode.AppendText("filliframe(\"title\", \"" + promptValue + "\");" + Environment.NewLine);
            }
            else if (item == Language.Resource.Element)
            {
                Program.mainFrm.tbxCode.AppendText("click(\"" + xpath + "\");" + Environment.NewLine);
            }
            else if (item == Language.Resource.FileUpload)
            {

            }
            else if (item == Language.Resource.RepeatElement)
            {
                xpath = GetXPath(Program.mainFrm.wbMainBrowser.htmlElm);

                if (repeatCount == 0)
                {
                    repeatItem = xpath;
                    repeatCount++;
                }
                else
                {
                    int index = 0;

                    string begin = string.Empty;
                    string end = string.Empty;

                    int max = Math.Min(repeatItem.Length, xpath.Length);
                    while (index < max && repeatItem[index] == xpath[index]) index++;
                    if (index > 0 && index < repeatItem.Length)
                    {
                        if (repeatItem[index] == '/')
                        {
                            end = string.Empty;

                            var sb = new StringBuilder(repeatItem);
                            sb.Remove(index, 1);
                            begin = "1";
                            sb.Insert(index, "[\"+ i +\"]/");

                            int endIndex = 0;

                            for (int i = index + 1; i < xpath.Length; i++)
                            {
                                if (xpath[i] == ']')
                                {
                                    break;
                                }
                                else
                                {
                                    end += xpath[i];
                                }
                                endIndex++;
                            }

                            Program.mainFrm.tbxCode.AppendText("for(i = 1; i <= " + end + "; i++)" + Environment.NewLine);
                            Program.mainFrm.tbxCode.AppendText("{" + Environment.NewLine);
                            Program.mainFrm.tbxCode.AppendText("\t" + "var text = extract(\"" + sb.ToString() + "\", \"text\");" + Environment.NewLine);
                            Program.mainFrm.tbxCode.AppendText("\tlog(text);" + Environment.NewLine);
                            Program.mainFrm.tbxCode.AppendText("}" + Environment.NewLine);
                        }
                        else if (repeatItem[index - 1] == '[')
                        {
                            int endIndex = 0;
                            end = string.Empty;

                            for (int i = index; i < repeatItem.Length; i++)
                            {
                                if (repeatItem[i] == ']')
                                {
                                    break;
                                }
                                else
                                {
                                    begin += repeatItem[i];
                                }
                                endIndex++;
                            }

                            for (int j = index; j < xpath.Length; j++)
                            {
                                if (xpath[j] == ']')
                                {
                                    break;
                                }
                                else
                                {
                                    end += xpath[j];
                                }
                            }

                            var sb = new StringBuilder(repeatItem);
                            sb.Remove(index, endIndex);
                            sb.Insert(index, "\"+ i +\"");
                            Program.mainFrm.tbxCode.AppendText("for(i = " + begin + "; i <= " + end + "; i++)" + Environment.NewLine);
                            Program.mainFrm.tbxCode.AppendText("{" + Environment.NewLine);
                            Program.mainFrm.tbxCode.AppendText("\t" + "var text = extract(\"" + sb.ToString() + "\", \"text\");" + Environment.NewLine);
                            Program.mainFrm.tbxCode.AppendText("\tlog(text);" + Environment.NewLine);
                            Program.mainFrm.tbxCode.AppendText("}" + Environment.NewLine);
                        }

                        repeatCount = 0;
                        repeatItem = string.Empty;
                    }
                }
            }

            Program.mainFrm.developerToolsToolStripMenuItem.Checked = false;
            Program.mainFrm.developerToolsToolStripMenuItem_Click(sender, null);
        }

        public static void AddDownload(string title, string url, string folder, int segnments)
        {
            Program.mainFrm.downloadList1.NewFileDownload(title, url, folder, segnments);
        }

        #endregion

    }
}
