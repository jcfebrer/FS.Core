
using System;
using System.IO;
using System.Windows.Forms;
using WiworBrowser.Objects;


namespace WiworBrowser.UI.Controls
{
    public partial class OrganizeFavorites : Form
    {
        private readonly ContextMenuStrip favContext;
        private readonly ContextMenuStrip linkContext;
        private readonly ToolStrip linkbar;
        private readonly string linksXml = TreeControl.linksXml;
        private readonly TreeView tree;

        public OrganizeFavorites(TreeView tree, ToolStrip linkbar, ContextMenuStrip linkContext,
                                 ContextMenuStrip favContext)
        {
            this.tree = tree;
            this.linkbar = linkbar;
            this.linkContext = linkContext;
            this.favContext = favContext;
            InitializeComponent();
        }

        private void OrganizeFavorites_Load(object sender, EventArgs e)
        {
            organizeFavTreeView.ImageList = tree.ImageList;
            
            foreach(Group gc in Common.Configuration.FavoritesGroups)
            {
                TreeNode parent = new TreeNode(gc.Name);
                parent.ToolTipText = gc.Description;
                parent.Expand();
                
                foreach (Favorite fav in Common.Configuration.Favorites)
                {
                    if (fav.Parent == gc)
                    {
                        Uri url = new Uri(fav.Url);
                        TreeNode node = new TreeNode(fav.Description, tree.ImageList.Images.IndexOfKey(url.Host),
                                                     tree.ImageList.Images.IndexOfKey(url.Host));
                        node.ToolTipText = fav.Url;
                        node.Name = fav.Url;
                        node.ContextMenuStrip = organizeContextMenu;

                        parent.Nodes.Add(node);
                    }
                }

                organizeFavTreeView.Nodes.Add(parent);
            }
        }

        //rename method
        private void rename()
        {
            if (organizeFavTreeView.SelectedNode.Index >= 0)
            {
                RenameLink rl = new RenameLink(organizeFavTreeView.SelectedNode.Text);
                TreeNode node = organizeFavTreeView.SelectedNode;

                if (rl.ShowDialog() == DialogResult.OK)
                {
                    node.Text = rl.newName.Text;

                    if (organizeFavTreeView.Nodes[0].Nodes.Contains(node))
                    {
                        if (tree.Visible)
                            tree.Nodes[0].Nodes[node.Name].Text = rl.newName.Text;
                        
                        if (linkbar.Visible)
                            linkbar.Items[node.Name].Text = rl.newName.Text;
                    }
                    else
                    {
                        if (tree.Visible)
                            tree.Nodes[node.Name].Text = rl.newName.Text;
                    }
                }

                rl.Close();
            }
        }

//delete method       
        private void delete()
        {
            if (organizeFavTreeView.SelectedNode.Index >= 0)
            {
                TreeNode node = organizeFavTreeView.SelectedNode;

                if (organizeFavTreeView.Nodes[0].Nodes.Contains(node))
                {
                    if (tree.Visible)
                        tree.Nodes[0].Nodes[node.Name].Remove();
                    
                    if (linkbar.Visible)
                        linkbar.Items[node.Name].Dispose();
                }
                else
                {
                    if (tree.Visible)
                        tree.Nodes[node.Name].Remove();
                }

                node.Remove();
            }
        }

        public void move()
        {
            if (organizeFavTreeView.SelectedNode.Index >= 0)
            {
                TreeNode node = organizeFavTreeView.SelectedNode;

                if (organizeFavTreeView.Nodes[0].Nodes.Contains(node))
                {
                    organizeFavTreeView.SelectedNode.Remove();
                    organizeFavTreeView.Nodes.Add(node);

                    
                    if (tree.Visible)
                    {
                        tree.Nodes[0].Nodes.RemoveByKey(node.Name);
                        node.ContextMenuStrip = favContext;
                        tree.Nodes.Add(node);
                    }

                    if (linkbar.Visible)
                        linkbar.Items.RemoveByKey(node.Name);
                }
                else
                {
                    organizeFavTreeView.SelectedNode.Remove();
                    organizeFavTreeView.Nodes[0].Nodes.Add(node);
                    if (tree.Visible)
                    {
                        tree.Nodes.RemoveByKey(node.Name);
                        node.ContextMenuStrip = linkContext;
                        tree.Nodes[0].Nodes.Add(node);
                    }
                    //if (linkbar.Visible == true)
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rename();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rename();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void organizeFavTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                organizeFavTreeView.SelectedNode = e.Node;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            move();
        }
    }
}