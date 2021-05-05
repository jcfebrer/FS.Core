using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WiworBrowser.Objects;

namespace WiworBrowser.UI.Controls
{
    public partial class TreeControl : UserControl
    {
        #region Delegates

        public delegate void FavoritesMouseClickHandler(object sender, TreeNodeMouseClickEventArgs e);

        public delegate void HistoryMouseClickHandler(object sender, TreeNodeMouseClickEventArgs e);

        #endregion

        public static string linksXml = "links.xml";
        
        private string adress = "", name = "";

        public TreeControl()
        {
            InitializeComponent();

            favTreeView.NodeMouseClick += favTreeView_NodeMouseClick;
            historyTreeView.NodeMouseClick += historyTreeView_NodeMouseClick;

            cmbHistory.SelectedValueChanged += comboBox1_SelectedValueChanged;

            cmbHistory.SelectedIndex = 0;
        }


        public void AddFavorit(string url, string name, string groupName)
        {
            if(url == "" || name == "")
            {
                MessageBox.Show("Información insuficiente para crear nuevo favorito.", "Favorito", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }
            Group group = Common.Configuration.GetFavoritesGroupByName(groupName);
            if (group == null)
            {
                TreeNode groupNode = new TreeNode(groupName);
                groupNode.ToolTipText = groupName;
                groupNode.ContextMenuStrip = groupContextMenu;
                favTreeView.Nodes.Add(groupNode);

                group = new Group(groupName);
                Common.Configuration.FavoritesGroups.Add(group);
            }

            TreeNode parent = FindNodeByName(favTreeView.Nodes, groupName);
            AddFavorit(url, name, parent);

            Common.Configuration.AddFavorite(new Favorite(url, name, group));
        }

        //addFavorit method
        public void AddFavorit(string url, string name, TreeNode parentNode)
        {
            TreeNode node = new TreeNode(name, FaviconIndex(url),
                                         FaviconIndex(url));
            node.ToolTipText = url;
            node.Name = url;
            node.ContextMenuStrip = favContextMenu;

            if (parentNode == null)
                favTreeView.Nodes.Add(node);
            else
                parentNode.Nodes.Add(node);

            favTreeView.SelectedNode = node;
        }

        //delete favorit method
        public void DeleteFavorit()
        {
            Common.Configuration.DeleteFavorite(Common.Configuration.Favorites.GetFavorite(favTreeView.SelectedNode.ToolTipText));

            favTreeView.SelectedNode.Remove();
        }


        public void OrganizeFavorites()
        {
            (new OrganizeFavorites(favTreeView, null, linkContextMenu, favContextMenu)).ShowDialog();
        }

        //renameFavorit method
        public void RenameFavorit()
        {
            RenameLink rl = new RenameLink(name);
            if (rl.ShowDialog() == DialogResult.OK)
            {
                TreeNode tn = FindNodeByName(favTreeView.Nodes, name);
                tn.Text = rl.newName.Text;

                Common.Configuration.Favorites.GetFavorite(name).Description = rl.newName.Text;
            }
            rl.Close();

            Common.Configuration.SaveFavorites();
        }


        //renameGroup method
        public void RenameGroup()
        {
            RenameLink rl = new RenameLink(name);
            if (rl.ShowDialog() == DialogResult.OK)
            {
                TreeNode tn = FindNodeByName(favTreeView.Nodes, name);
                tn.Text = rl.newName.Text;

                Common.Configuration.GetFavoritesGroupByName(name).Name = rl.newName.Text;
            }
            rl.Close();

            Common.Configuration.SaveFavorites();
        }

        public TreeNode FindNodeByName(TreeNodeCollection tn, string name)
        {
            return FindNodeByName(tn, name, null);
        }

        //find node
        public TreeNode FindNodeByName(TreeNodeCollection tn, string name, string parentName)
        {
            foreach (TreeNode node in tn)
            {
                if (parentName != null && node.Parent != null)
                {
                    if (node.Text == name && node.Parent.Text == parentName) return node;
                }
                else
                {
                    if (node.Text == name) return node;
                }

                if (node.Nodes.Count > 0)
                {
                    TreeNode n = FindNodeByName(node.Nodes, name, parentName);
                    if (n != null) return n;
                } 
            }

            return null;
        }

        //addHistory method
        public void AddHistory(Uri url, DateTime date)
        {
            int i = Common.Configuration.AddHistory(new History(url.ToString(), date, 1));

            //if (splitContainer1.Panel1Collapsed == true)
            //{
            /*Visitas realizadas hoy*/
            if (cmbHistory.Text.Equals("Visitas realizadas hoy"))
            {
                if (!historyTreeView.Nodes.ContainsKey(url.ToString()))
                {
                    TreeNode node =
                        new TreeNode(url.ToString(), 3, 3);
                    node.ToolTipText = url + "\nÚltima visita: " + date + "\nVeces visitado :" + i;
                    node.Name = url.ToString();
                    node.ContextMenuStrip = histContextMenu;
                    historyTreeView.Nodes.Insert(0, node);
                }
                else
                    historyTreeView.Nodes[url.ToString()].ToolTipText
                        = url + "\nÚltima visita: " + date + "\nVeces visitado: " + i;
            }
            /*Visitas por página*/
            if (cmbHistory.Text.Equals("Visitas por página"))
            {
                if (!historyTreeView.Nodes.ContainsKey(url.Host))
                {
                    historyTreeView.Nodes.Add(url.Host, url.Host, 0, 0);

                    TreeNode node =
                        new TreeNode(url.ToString(), 3, 3);
                    node.ToolTipText = url + "\nÚltima visita: " + date + "\nVeces visitado: " + i;
                    node.Name = url.ToString();
                    node.ContextMenuStrip = histContextMenu;
                    historyTreeView.Nodes[url.Host].Nodes.Add(node);
                }

                else if (!historyTreeView.Nodes[url.Host].Nodes.ContainsKey(url.ToString()))
                {
                    TreeNode node =
                        new TreeNode(url.ToString(), 3, 3);
                    node.ToolTipText = url + "\nÚltima visita: " + date + "\nVeces visitado: " + i;
                    node.Name = url.ToString();
                    node.ContextMenuStrip = histContextMenu;
                    historyTreeView.Nodes[url.Host].Nodes.Add(node);
                }
                else
                    historyTreeView.Nodes[url.Host].Nodes[url.ToString()].ToolTipText
                        = url + "\nÚltima visita: " + date + "\nVeces visitado" + i;
            }
            /* Visitas por fecha*/
            if (cmbHistory.Text.Equals("Visitas por fecha"))
            {
                if (historyTreeView.Nodes[4].Nodes.ContainsKey(url.ToString()))
                    historyTreeView.Nodes[4].Nodes[url.ToString()].ToolTipText
                        = url + "\nÚltima visita: " + date + "\nVeces visitado: " + i;
                else
                {
                    TreeNode node =
                        new TreeNode(url.ToString(), 3, 3);
                    node.ToolTipText = url + "\nÚltima visita: " + date + "\nVeces visitado :" + i;
                    node.Name = url.ToString();
                    node.ContextMenuStrip = histContextMenu;
                    historyTreeView.Nodes[4].Nodes.Add(node);
                }
            }
        }

        //delete history
        public void DeleteHistory()
        {
            Common.Configuration.DeleteHistory(Common.Configuration.Historys.GetHistory(historyTreeView.SelectedNode.ToolTipText));

            historyTreeView.SelectedNode.Remove();
        }

        #region FAVICON

        // favicon
        public static Image FavIcon(string u, string file)
        {
            try
            {
                Uri url = new Uri(u);
                if (url.Host != "")
                {
                    string iconurl = "http://" + url.Host + "/favicon.ico";

                    WebRequest request = WebRequest.Create(iconurl);

                    WebResponse response = request.GetResponse();

                    Stream s = response.GetResponseStream();
                    return Image.FromStream(s);
                }
                else
                {
                    return Image.FromFile(file);
                }
            }
            catch
            {
                return Image.FromFile(file);
            }
        }

        //favicon index
        private int FaviconIndex(string url)
        {
            try
            {
                Uri key = new Uri(url);
                if (!imgList.Images.ContainsKey(key.Host))
                    imgList.Images.Add(key.Host, FavIcon(url, "link.png"));
                return imgList.Images.IndexOfKey(key.Host);
            }
            catch
            {
                return 1;
            }
            
        }

        //getFavicon from key
        public Image GetFavicon(string key)
        {
            try
            {
                Uri url = new Uri(key);
                if (!imgList.Images.ContainsKey(url.Host))
                    imgList.Images.Add(url.Host, FavIcon(key, "link.png"));
                return imgList.Images[url.Host];
            }
            catch
            {
                return imgList.Images[1];
            }
        }

        #endregion


        private void CreateGroupTree(TreeNodeCollection nc, Group group)
        {
            string groupName;
            string groupDescription;

            TreeNode g = new TreeNode();
            if (group.Parent != null)
            {
                //CreateGroupTree(nc, group.Parent);
                g = FindNodeByName(nc, group.Parent.Name);
            }
            else
            {
                g = FindNodeByName(nc, group.Name);    
            }

            groupName = group.Name;
            groupDescription = group.Description;

            TreeNode n = new TreeNode(groupName);
            n.ToolTipText = groupDescription;
            n.ContextMenuStrip = groupContextMenu;
            n.Expand();

            if (g == null)
            {

                nc.Add(n);
            }
            else
            {
                g.Nodes.Add(n);
            }

            return;
        }

        private void ShowFavorites()
        {
            CreateGroup();

            CreateLinks(); 
        }

        private void CreateGroup()
        {
            favTreeView.Nodes.Clear();

            foreach (Group gc in Common.Configuration.FavoritesGroups)
            {
                CreateGroupTree(favTreeView.Nodes, gc);
            }
        }

        private void CreateLinks()
        {
            foreach (Favorite fav in Common.Configuration.Favorites)
            {
                TreeNode parentGroupNode = FindNodeByName(favTreeView.Nodes, fav.Parent.Name);

                Uri url = new Uri(fav.Url);
                TreeNode node = new TreeNode(fav.Description, FaviconIndex(fav.Url),
                                             FaviconIndex(fav.Url));
                node.ToolTipText = fav.Url;
                node.Name = fav.Url;
                node.ContextMenuStrip = favContextMenu;

                parentGroupNode.Nodes.Add(node);
            }
        }

        //node click
        private void favTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                favTreeView.SelectedNode = e.Node;
                adress = e.Node.ToolTipText;
                name = e.Node.Text;
            }
            else if (e.Node != favTreeView.Nodes[0])
            {
                if(e.Node.Nodes.Count == 0)
                    FavoritesMouseClick(sender, e);
            }
        }


        //show history in tree wiew
        private void ShowHistory()
        {
            historyTreeView.Nodes.Clear();
            
                DateTime now = DateTime.Now;
                if (cmbHistory.Text.Equals("Visitas realizadas hoy"))
                {
                    historyTreeView.ShowRootLines = false;
                    foreach (History his in Common.Configuration.Historys)
                    {
                        if (his.LastVisited != now.Date) return;

                        TreeNode node =
                            new TreeNode(his.Url, 3, 3);
                        node.ToolTipText = his.Url + "\nÚltima visita: " + his.LastVisited.ToString() +
                                           "\nVeces visitado: " + his.Times;
                        node.Name = his.Url;
                        node.ContextMenuStrip = histContextMenu;
                        historyTreeView.Nodes.Add(node);
                    }
                }

                if (cmbHistory.Text.Equals("Visitas por página"))
                {
                    historyTreeView.ShowRootLines = true;
                    foreach (History his in Common.Configuration.Historys)
                    {
                        Uri site = new Uri(his.Url);

                        if (!historyTreeView.Nodes.ContainsKey(site.Host))
                            historyTreeView.Nodes.Add(site.Host, site.Host, 0, 0);
                        TreeNode node = new TreeNode(his.Url, 3, 3);
                        node.ToolTipText = his.Url + "\nÚltima visita: " + his.LastVisited.ToString() +
                                           "\nVeces visitado: " + his.Times;
                        node.Name = his.Url;
                        node.ContextMenuStrip = histContextMenu;
                        historyTreeView.Nodes[site.Host].Nodes.Add(node);
                    }
                }

                if (cmbHistory.Text.Equals("Visitas por fecha"))
                {
                    historyTreeView.ShowRootLines = true;
                    historyTreeView.Nodes.Add("Hace 2 semanas", "Hace 2 semanas", 2, 2);
                    historyTreeView.Nodes.Add("Semana pasada", "Semana pasada", 2, 2);
                    historyTreeView.Nodes.Add("Esta semana", "Esta semana", 2, 2);
                    historyTreeView.Nodes.Add("Ayer", "Ayer", 2, 2);
                    historyTreeView.Nodes.Add("Hoy", "Hoy", 2, 2);
                    foreach (History his in Common.Configuration.Historys)
                    {
                        TreeNode node = new TreeNode(his.Url, 3, 3);
                        node.ToolTipText = his.Url + "\nÚltima visita: " + his.LastVisited.ToString() +
                                           "\nVeces visitado: " + his.Times;
                        node.Name = his.Url;
                        node.ContextMenuStrip = histContextMenu;

                        if (his.LastVisited == now.Date)
                            historyTreeView.Nodes[4].Nodes.Add(node);
                        else if (his.LastVisited.AddDays(1).ToShortDateString().Equals(now.ToShortDateString()))
                            historyTreeView.Nodes[3].Nodes.Add(node);
                        else if (his.LastVisited.AddDays(7) > now)
                            historyTreeView.Nodes[2].Nodes.Add(node);
                        else if (his.LastVisited.AddDays(14) > now)
                            historyTreeView.Nodes[1].Nodes.Add(node);
                        else if (his.LastVisited.AddDays(21) > now)
                            historyTreeView.Nodes[0].Nodes.Add(node);
                        //else if (his.LastVisited.AddDays(22) > now)
                        //    myXml.DocumentElement.RemoveChild(el);
                    }
                }
            
        }

        //history nodes click
        private void historyTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                historyTreeView.SelectedNode = e.Node;
                adress = e.Node.Text;
            }
            else if (!cmbHistory.Text.Equals("Visitas realizadas hoy"))
            {
                if (!historyTreeView.Nodes.Contains(e.Node))
                {
                    //getCurrentBrowser().Navigate(e.Node.Text);
                    HistoryMouseClick(sender, e);
                }
            }
            else
            {
                //getCurrentBrowser().Navigate(e.Node.Text);
                HistoryMouseClick(sender, e);
            }
        }


        public void ShowFavHist()
        {
            ShowFavorites();
            ShowHistory();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowHistory();
        }


        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeleteFavorit();
        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RenameFavorit();
        }


        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DeleteHistory();
        }


        public event HistoryMouseClickHandler HistoryMouseClick;

        public event FavoritesMouseClickHandler FavoritesMouseClick;

        public void ClearHistoryTreeViewNodes()
        {
            historyTreeView.Nodes.Clear();
        }

        private void nuevoEnlaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFavorites dlg = new AddFavorites("", name);
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                AddFavorit(dlg.favUrl, dlg.favName, dlg.favFile);
            }
            dlg.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RenameGroup();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeleteGroup();
        }

        private void DeleteGroup()
        {
            bool sure = true;
            Group group = Common.Configuration.GetFavoritesGroupByName(name);

            if(Common.Configuration.TotalFavorites(group) > 0)
            {
                if(MessageBox.Show("Existen enlaces en este grupo, ¿esta seguro de querer eliminarlos?","Borrar",MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    sure = true;
                }
                else
                {
                    sure = false;
                }
            }

            if(sure)
            {
                Common.Configuration.DeleteFavorites(group);
                Common.Configuration.DeleteFavoritesGroup(group, true);
                ShowFavorites();
            }
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGroup();
        }

        //NewGroup method
        public void NewGroup()
        {
            NewGroup rl = new NewGroup(name);
            if (rl.ShowDialog() == DialogResult.OK)
            {
                Group groupParent = Common.Configuration.FavoritesGroups.GetGroupByName(name);
                Group group = new Group(rl.groupName.Text);
                group.Parent = groupParent;
                Common.Configuration.AddFavoritesGroup(group);

                ShowFavorites();
            }
            rl.Close();

            Common.Configuration.SaveFavorites();
        }
    }
}