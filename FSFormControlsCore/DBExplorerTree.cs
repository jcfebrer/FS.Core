using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace FSFormControlsCore
{
	/// <summary>
	/// Summary description for ExplorerTree.
	/// </summary>
	/// 
	[ToolboxBitmapAttribute(typeof(FSFormControlsCore.DBExplorerTree), "tree.gif"),DefaultEvent("PathChanged")	]
	public class DBExplorerTree : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TreeView tvwMain;
		private System.ComponentModel.IContainer components;

		private bool goflag = false ;
		private bool showMyDocuments = true;
		private bool showMyFavorites = true;
		private bool showMyNetwork = true;

		private bool showAddressbar = true;
		private bool showToolbar = true;



		private bool GoFlag
		{
			get
			{
				return goflag;
			}
			set
			{
				goflag=value;
			}
		}
		public bool ShowAddressbar
		{
			get
			{
				return showAddressbar;
			}
			set
			{
				showAddressbar=value;
			}
		}
		public bool ShowToolbar
		{
			get
			{
				return showToolbar;
			}
			set
			{
				showToolbar=value;
			}
		}
		public bool ShowMyDocuments
		{
			get
			{
				return showMyDocuments;
			}
			set
			{
				showMyDocuments=value;
				this.Refresh(); 
			}
		}

		public bool ShowMyFavorites
		{
			get
			{
				return showMyFavorites;
			}
			set
			{
				showMyFavorites=value;
				this.Refresh();
			}
		}

		public bool ShowMyNetwork
		{
			get
			{
				return showMyNetwork;
			}
			set
			{
				showMyNetwork=value;
				this.Refresh();
			}
		}

		
		TreeNode node;
		TreeNode TreeNodeMyComputer ;
		TreeNode TreeNodeRootNode ;


		//ListViewItem comunalItem;
		private System.Windows.Forms.Button btnGo;
		
		//SHFILEINFO [] iconList = new SHFILEINFO[1];	//used icons
		public delegate void PathChangedEventHandler(object sender, EventArgs e);
		private PathChangedEventHandler PathChangedEvent;
		public event PathChangedEventHandler PathChanged
		{
			add
			{
				PathChangedEvent = (PathChangedEventHandler) System.Delegate.Combine(PathChangedEvent, value);
			}
			remove
			{
				PathChangedEvent = (PathChangedEventHandler) System.Delegate.Remove(PathChangedEvent, value);
			}
		}
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader Path;
		private System.Windows.Forms.ColumnHeader Status;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenuStrip cMShortcut;
		private System.Windows.Forms.ToolStripMenuItem mnuShortcut;
		private System.Windows.Forms.TextBox txtPath;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripAdd;
        private ToolStripButton toolStripBack;
        private ToolStripButton toolStripNext;
        private ToolStripButton toolStripUp;
        private ToolStripButton toolStripRefresh;
        private ToolStripButton toolStripHome;
        private ToolStripButton toolStripInfo;
        private string selectedPath ="home";
		

		
		[
		Category("Appearance"),
		Description("Selected Path of Image")
		]
		public string SelectedPath
		{
			get
			{
				return this.selectedPath;
			}
			set
			{
				this.selectedPath = value;
				this.Invalidate();
			}
		}

		
		public DBExplorerTree()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(DBExplorerTree));
            txtPath = new TextBox();
            btnGo = new Button();
            tvwMain = new TreeView();
            imageList1 = new ImageList(components);
            toolTip1 = new ToolTip(components);
            listView1 = new ListView();
            Path = new ColumnHeader();
            Status = new ColumnHeader();
            cMShortcut = new ContextMenuStrip(components);
            mnuShortcut = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            toolStripAdd = new ToolStripButton();
            toolStripBack = new ToolStripButton();
            toolStripNext = new ToolStripButton();
            toolStripUp = new ToolStripButton();
            toolStripRefresh = new ToolStripButton();
            toolStripHome = new ToolStripButton();
            toolStripInfo = new ToolStripButton();
            cMShortcut.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPath.Location = new Point(0, 28);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(220, 23);
            txtPath.TabIndex = 61;
            toolTip1.SetToolTip(txtPath, "Current directory");
            txtPath.TextChanged += txtPath_TextChanged;
            txtPath.KeyPress += txtPath_KeyPress;
            txtPath.KeyUp += txtPath_KeyUp;
            // 
            // btnGo
            // 
            btnGo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGo.Cursor = Cursors.Hand;
            btnGo.FlatStyle = FlatStyle.Flat;
            btnGo.ForeColor = Color.White;
            btnGo.Image = (Image)resources.GetObject("btnGo.Image");
            btnGo.Location = new Point(216, 26);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(24, 22);
            btnGo.TabIndex = 60;
            toolTip1.SetToolTip(btnGo, "Go to the directory");
            btnGo.Click += btnGo_Click;
            // 
            // tvwMain
            // 
            tvwMain.AllowDrop = true;
            tvwMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvwMain.BackColor = Color.White;
            tvwMain.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tvwMain.ImageIndex = 0;
            tvwMain.ImageList = imageList1;
            tvwMain.Location = new Point(0, 54);
            tvwMain.Name = "tvwMain";
            tvwMain.SelectedImageIndex = 2;
            tvwMain.ShowLines = false;
            tvwMain.ShowRootLines = false;
            tvwMain.Size = new Size(240, 307);
            tvwMain.TabIndex = 59;
            tvwMain.AfterExpand += tvwMain_AfterExpand;
            tvwMain.AfterSelect += tvwMain_AfterSelect;
            tvwMain.DoubleClick += tvwMain_DoubleClick;
            tvwMain.MouseUp += tvwMain_MouseUp;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "");
            imageList1.Images.SetKeyName(1, "");
            imageList1.Images.SetKeyName(2, "");
            imageList1.Images.SetKeyName(3, "");
            imageList1.Images.SetKeyName(4, "");
            imageList1.Images.SetKeyName(5, "");
            imageList1.Images.SetKeyName(6, "");
            imageList1.Images.SetKeyName(7, "");
            imageList1.Images.SetKeyName(8, "");
            imageList1.Images.SetKeyName(9, "");
            imageList1.Images.SetKeyName(10, "");
            imageList1.Images.SetKeyName(11, "");
            imageList1.Images.SetKeyName(12, "");
            imageList1.Images.SetKeyName(13, "");
            imageList1.Images.SetKeyName(14, "");
            imageList1.Images.SetKeyName(15, "");
            imageList1.Images.SetKeyName(16, "");
            imageList1.Images.SetKeyName(17, "");
            imageList1.Images.SetKeyName(18, "");
            imageList1.Images.SetKeyName(19, "");
            imageList1.Images.SetKeyName(20, "");
            imageList1.Images.SetKeyName(21, "");
            imageList1.Images.SetKeyName(22, "");
            imageList1.Images.SetKeyName(23, "");
            imageList1.Images.SetKeyName(24, "");
            imageList1.Images.SetKeyName(25, "");
            imageList1.Images.SetKeyName(26, "");
            imageList1.Images.SetKeyName(27, "");
            imageList1.Images.SetKeyName(28, "");
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { Path, Status });
            listView1.Location = new Point(8, 208);
            listView1.Name = "listView1";
            listView1.Size = new Size(224, 75);
            listView1.TabIndex = 68;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.Visible = false;
            // 
            // cMShortcut
            // 
            cMShortcut.Items.AddRange(new ToolStripItem[] { mnuShortcut });
            cMShortcut.Name = "cMShortcut";
            cMShortcut.Size = new Size(166, 26);
            // 
            // mnuShortcut
            // 
            mnuShortcut.Name = "mnuShortcut";
            mnuShortcut.Size = new Size(165, 22);
            mnuShortcut.Text = "Remove Shortcut";
            mnuShortcut.Click += mnuShortcut_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripHome, toolStripBack, toolStripNext, toolStripUp, toolStripRefresh, toolStripInfo, toolStripAdd });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(240, 25);
            toolStrip1.TabIndex = 72;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripAdd
            // 
            toolStripAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripAdd.Image = (Image)resources.GetObject("toolStripAdd.Image");
            toolStripAdd.ImageTransparentColor = Color.Magenta;
            toolStripAdd.Name = "toolStripAdd";
            toolStripAdd.Size = new Size(23, 22);
            toolStripAdd.Text = "toolStripButton1";
            toolStripAdd.Visible = false;
            toolStripAdd.Click += btnAdd_Click;
            // 
            // toolStripBack
            // 
            toolStripBack.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBack.Image = (Image)resources.GetObject("toolStripBack.Image");
            toolStripBack.ImageTransparentColor = Color.Magenta;
            toolStripBack.Name = "toolStripBack";
            toolStripBack.Size = new Size(23, 22);
            toolStripBack.Text = "toolStripButton2";
            toolStripBack.Click += btnBack_Click;
            // 
            // toolStripNext
            // 
            toolStripNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripNext.Image = (Image)resources.GetObject("toolStripNext.Image");
            toolStripNext.ImageTransparentColor = Color.Magenta;
            toolStripNext.Name = "toolStripNext";
            toolStripNext.Size = new Size(23, 22);
            toolStripNext.Text = "toolStripButton3";
            toolStripNext.Click += btnNext_Click;
            // 
            // toolStripUp
            // 
            toolStripUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripUp.Image = (Image)resources.GetObject("toolStripUp.Image");
            toolStripUp.ImageTransparentColor = Color.Magenta;
            toolStripUp.Name = "toolStripUp";
            toolStripUp.Size = new Size(23, 22);
            toolStripUp.Text = "toolStripButton4";
            toolStripUp.Click += btnUp_Click;
            // 
            // toolStripRefresh
            // 
            toolStripRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripRefresh.Image = (Image)resources.GetObject("toolStripRefresh.Image");
            toolStripRefresh.ImageTransparentColor = Color.Magenta;
            toolStripRefresh.Name = "toolStripRefresh";
            toolStripRefresh.Size = new Size(23, 22);
            toolStripRefresh.Text = "toolStripButton5";
            toolStripRefresh.Click += btnRefresh_Click;
            // 
            // toolStripHome
            // 
            toolStripHome.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripHome.Image = (Image)resources.GetObject("toolStripHome.Image");
            toolStripHome.ImageTransparentColor = Color.Magenta;
            toolStripHome.Name = "toolStripHome";
            toolStripHome.Size = new Size(23, 22);
            toolStripHome.Text = "toolStripButton6";
            toolStripHome.Click += btnHome_Click;
            // 
            // toolStripInfo
            // 
            toolStripInfo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripInfo.Image = (Image)resources.GetObject("toolStripInfo.Image");
            toolStripInfo.ImageTransparentColor = Color.Magenta;
            toolStripInfo.Name = "toolStripInfo";
            toolStripInfo.Size = new Size(23, 22);
            toolStripInfo.Text = "toolStripButton7";
            toolStripInfo.Visible = false;
            toolStripInfo.Click += btnInfo_Click;
            // 
            // DBExplorerTree
            // 
            BackColor = Color.White;
            Controls.Add(toolStrip1);
            Controls.Add(btnGo);
            Controls.Add(listView1);
            Controls.Add(txtPath);
            Controls.Add(tvwMain);
            Name = "DBExplorerTree";
            Size = new Size(240, 363);
            Load += ExplorerTree_Load;
            cMShortcut.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private void ExplorerTree_Load(object sender, System.EventArgs e)
		{
			GetDirectory();
			
			if (Directory.Exists(selectedPath))
			{
				setCurrentPath(selectedPath);
				
			}
			else
			{
				setCurrentPath("home");
			}
			btnGo_Click(this,e); 

			refreshView();

		}
		public void refreshView()
		{
			if ((!showAddressbar )&& (!showToolbar ))
			{
				tvwMain.Top = 0;
				txtPath.Visible = false;
				btnGo.Visible = false ; 
				toolStrip1.Visible = false; 
				tvwMain.Height = this.Height;
			}
			else
			{
				if (showToolbar&&(!showAddressbar))
				{
					tvwMain.Top = 20;
					txtPath.Visible = false;
					btnGo.Visible = false; 
					tvwMain.Height = this.Height- 20;
					toolStrip1.Visible = true; 
				}
				else if (showAddressbar&&(!showToolbar))
				{
					tvwMain.Top = 20;
					txtPath.Top = 1;
					btnGo.Top = -2;
					txtPath.Visible = true;
					btnGo.Visible = true ; 
					tvwMain.Height = this.Height - 20;
					toolStrip1.Visible = false; 
				}
				else 
				{
					tvwMain.Top = 40;
					txtPath.Visible = true;
					btnGo.Visible = true ; 
					txtPath.Top = 19;
					btnGo.Top = 16;
					toolStrip1.Visible = true; 
					tvwMain.Height = this.Height- 40;
				}
			}
		}

		public void GetDirectory()
		{
			tvwMain.Nodes.Clear();  
			
		
			string [] drives = Environment.GetLogicalDrives();
			TreeNode nodeD;
			//Environment.UserDomainName .GetFolderPath( 
			//Environment.GetFolderPath (Environment.SystemDirectory);

			TreeNode nodemd;
			TreeNode nodemf;
			TreeNode nodemyC;
			TreeNode nodemNc;

			TreeNode nodemyN;

			TreeNode nodeEN;
			TreeNode nodeNN;
			
			nodeD = new TreeNode();
			nodeD.Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			nodeD.Text = "Desktop";
			nodeD.ImageIndex = 10;
			nodeD.SelectedImageIndex = 10;

			tvwMain.Nodes.Add(nodeD);
			TreeNodeRootNode = nodeD ;
			
			
			if (ShowMyDocuments) 
			{
				//Add My Documents and Desktop folder outside
				nodemd = new TreeNode();
				nodemd.Tag = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				nodemd.Text = "My Documents";
				nodemd.ImageIndex = 9;
				nodemd.SelectedImageIndex = 9;
				nodeD.Nodes.Add(nodemd);
				FillFilesandDirs(nodemd);
			}

			nodemyC = new TreeNode();
			nodemyC.Tag = "My Computer";
			nodemyC.Text = "My Computer";
			nodemyC.ImageIndex = 12;
			nodemyC.SelectedImageIndex = 12;
			nodeD.Nodes.Add(nodemyC);
			nodemyC.EnsureVisible(); 

			TreeNodeMyComputer = nodemyC ;

			nodemNc = new TreeNode();
							nodemNc.Tag = "my Node";
							nodemNc.Text = "my Node";//dir.Substring(dir.LastIndexOf(@"\") + 1);
							nodemNc.ImageIndex = 12;
							nodemNc.SelectedImageIndex = 12;
							nodemyC.Nodes.Add(nodemNc);
						

			
			if (ShowMyNetwork) 
			{
				
				nodemyN = new TreeNode();
				nodemyN.Tag = "My Network Places";
				nodemyN.Text = "My Network Places";
				nodemyN.ImageIndex = 13;
				nodemyN.SelectedImageIndex = 13;
				nodeD.Nodes.Add(nodemyN);
				nodemyN.EnsureVisible();

				nodeEN = new TreeNode();
				nodeEN.Tag = "Entire Network";
				nodeEN.Text = "Entire Network";
				nodeEN.ImageIndex = 14;
				nodeEN.SelectedImageIndex = 14;
				nodemyN.Nodes.Add(nodeEN);

				nodeNN = new TreeNode();
				nodeNN.Tag = "Network Node";
				nodeNN.Text = "Network Node";
				nodeNN.ImageIndex = 15;
				nodeNN.SelectedImageIndex = 15;
				nodeEN.Nodes.Add(nodeNN);
				
				nodeEN.EnsureVisible();
			}
			
			if (ShowMyFavorites) 
			{
				nodemf = new TreeNode();
				nodemf.Tag = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
				nodemf.Text = "My Favorites";
				nodemf.ImageIndex = 26;
				nodemf.SelectedImageIndex = 26;
				nodeD.Nodes.Add(nodemf);
				FillFilesandDirs(nodemf);
			}
			ExploreTreeNode(nodeD);
			
		}
		private void ExploreTreeNode(TreeNode n)
		{
			Cursor.Current = Cursors.WaitCursor;

			try
			{
				//get dirs
				FillFilesandDirs(n);
				
				//get dirs one more level deep in current dir so user can see there is
				//more dirs underneath current dir
				foreach(TreeNode node in n.Nodes)
				{
					if (String.Compare(n.Text,"Desktop")==0) 
					{
						if ((String.Compare(node.Text ,"My Documents")==0) ||(String.Compare(node.Text ,"My Computer")==0) ||(String.Compare(node.Text ,"Microsoft Windows Network")==0)|| (String.Compare(node.Text ,"My Network Places")==0))
						{}
						else
						{
							FillFilesandDirs(node);
						}
					}
					else
					{
						FillFilesandDirs(node);
					}
				}
			}
			
			catch
			{}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void GetDirectories(TreeNode parentNode)
		{
// added after suggestion
			string[] dirList;

			dirList = Directory.GetDirectories(parentNode.Tag.ToString());
			Array.Sort(dirList);

			//check if dir already exists in case click same dir twice
			if (dirList.Length == parentNode.Nodes.Count)
				return;
			//add each dir in selected dir
			for (int i = 0; i < dirList.Length; i++)
			{
				node = new TreeNode();
				node.Tag = dirList[i]; //store path in tag
				node.Text = dirList[i].Substring(dirList[i].LastIndexOf(@"\") + 1);
				node.ImageIndex = 1;
				parentNode.Nodes.Add(node);
			}

// old code
//			bool check = false;
//
//			//add each dir in selected dir
//			foreach(string dir in Directory.GetDirectories(parentNode.Tag.ToString()))
//			{
//				check = false;
//
//				//check if dir already exists in case click same dir twice
//				if(Directory.GetDirectories(parentNode.Tag.ToString()).Length == parentNode.Nodes.Count)
//					check = true;
//
//				if(!check)	//if not there add
//				{
//					node = new TreeNode();
//					node.Tag = dir;	//store path in tag
//					node.Text = dir.Substring(dir.LastIndexOf(@"\") + 1);
//					node.ImageIndex = 1;
//					parentNode.Nodes.Add(node);
//				}
//			}
			
		}

		
		private void FillFilesandDirs(TreeNode comunalNode)
		{
			try 
			{
				GetDirectories(comunalNode);
			}
			catch(Exception)
			{
				return;
			}
		}

		public void setCurrentPath(string strPath)
		{
			SelectedPath = strPath;
			
			if (String.Compare(strPath,"home")==0)
			{
				txtPath.Text = Application.StartupPath;
			}
			else
			{
				DirectoryInfo inf = new DirectoryInfo(strPath);
				if(inf.Exists)
				{
					txtPath.Text =  strPath;
				
				}
				else
					txtPath.Text = Application.StartupPath;
			}
			

		}

		private void tvwMain_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			string [] drives = Environment.GetLogicalDrives();
			string dir2 ="";

			Cursor.Current = Cursors.WaitCursor;   
			TreeNode n;
			TreeNode nodeNN;
			TreeNode nodemN;
			TreeNode nodemyC;
			TreeNode nodeNNode;
			TreeNode nodeDrive;
			nodemyC = e.Node;   

			n = e.Node;
			
			if (n.Text.IndexOf(":",1)>0)   
			{
				ExploreTreeNode(n);
			}
			else
			{//(String.Compare(n.Text ,"My Documents")==0) || (String.Compare(n.Text,"Desktop")==0) || 

				if ((String.Compare(n.Text,"Desktop" )==0)||(String.Compare(n.Text,"Microsoft Windows Network" )==0)||(String.Compare(n.Text ,"My Computer")==0) || (String.Compare(n.Text ,"My Network Places")==0)|| (String.Compare(n.Text ,"Entire Network")==0)||((n.Parent!=null)&&(String.Compare(n.Parent.Text,"Microsoft Windows Network")==0)) )
				{
					if((String.Compare(n.Text ,"My Computer")==0)&&(nodemyC.GetNodeCount(true) <2))
						///////////
						//add each drive and files and dirs
					{
						nodemyC.FirstNode.Remove();
 
					foreach(string drive in drives)
					{
				
						nodeDrive = new TreeNode();
						nodeDrive.Tag = drive;
					
						nodeDrive.Text = drive ;
					
						//Determine icon to display by drive
						switch(Win32.GetDriveType(drive))
						{
							case 2:
								nodeDrive.ImageIndex = 17;
								nodeDrive.SelectedImageIndex  = 17;
								break;
							case 3:
								nodeDrive.ImageIndex = 0;
								nodeDrive.SelectedImageIndex  = 0;
								break;
							case 4:
								nodeDrive.ImageIndex = 8;
								nodeDrive.SelectedImageIndex = 8;
								break;
							case 5:
								nodeDrive.ImageIndex = 7;
								nodeDrive.SelectedImageIndex = 7;
								break;
							default:
								nodeDrive.ImageIndex = 0;
								nodeDrive.SelectedImageIndex = 0;
								break;
						}
					
						nodemyC.Nodes.Add(nodeDrive);
						nodeDrive.EnsureVisible();
						tvwMain.Refresh(); 
						try
						{
							//add dirs under drive
							if (Directory.Exists (drive))
							{
								foreach(string dir in Directory.GetDirectories(drive))
								{
									dir2 = dir;
									node = new TreeNode();
									node.Tag = dir;
									node.Text = dir.Substring(dir.LastIndexOf(@"\") + 1);
									node.ImageIndex = 1;
									nodeDrive.Nodes.Add(node);
								}
							}
				
							//fill those dirs
							//					foreach(TreeNode curNode in 
							//						tvwMain.Nodes[tvwMain.Nodes.Count - 1].Nodes)
							//					{
							//						FillFilesandDirs(curNode);
							//					}
						}
						catch(Exception)	//error just add blank dir
						{
							// MessageBox.Show ("Error while Filling the Explorer:" + ex.Message );
							//					node = new TreeNode();
							//					node.Tag = dir2;
							//					node.Text = dir2.Substring(dir2.LastIndexOf(@"\") + 1);
							//					node.ImageIndex = 1;
							//					tvwMain.Nodes.Add(node);
						}
						nodemyC.Expand(); 
						}
					
					}				
					if((String.Compare(n.Text ,"Entire Network")==0))
					{
						if (n.FirstNode.Text == "Network Node")
						{
							n.FirstNode.Remove();
							//NETRESOURCE netRoot = new NETRESOURCE();
			
							ServerEnum servers = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET, ResourceType.RESOURCETYPE_DISK, ResourceUsage.RESOURCEUSAGE_ALL, ResourceDisplayType.RESOURCEDISPLAYTYPE_NETWORK,"" );
							
							foreach	(string	s1 in servers)
							{
								string s2="";
								s2 = s1.Substring(0,s1.IndexOf("|",1));
									
								if(s1.IndexOf("NETWORK",1) > 0 ) 
								{
									nodeNN = new TreeNode();
									nodeNN.Tag =  s2;
									nodeNN.Text = s2;//dir.Substring(dir.LastIndexOf(@"\") + 1);
									nodeNN.ImageIndex = 15;
									nodeNN.SelectedImageIndex = 15;
									n.Nodes.Add(nodeNN);
								}
								else
								{
									TreeNode nodemNc;
									nodemN = new TreeNode();
									nodemN.Tag = s2;//"my Node";
									nodemN.Text = s2;//"my Node";//dir.Substring(dir.LastIndexOf(@"\") + 1);
									nodemN.ImageIndex = 16;
									nodemN.SelectedImageIndex = 16;
									n.LastNode.Nodes.Add(nodemN);

									nodemNc = new TreeNode();
									nodemNc.Tag = "my netNode";
									nodemNc.Text = "my netNode";//dir.Substring(dir.LastIndexOf(@"\") + 1);
									nodemNc.ImageIndex = 12;
									nodemNc.SelectedImageIndex = 12;
									nodemN.Nodes.Add(nodemNc);
								}
							}
						}
					}
					if ((n.Parent!=null)&&(String.Compare(n.Parent.Text,"Microsoft Windows Network")==0))

					{
						if (n.FirstNode.Text == "my netNode")
						{
							n.FirstNode.Remove();
							
							string pS=n.Text ;
							
							//NETRESOURCE netRoot = new NETRESOURCE();
			
							ServerEnum servers = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET,
								ResourceType.RESOURCETYPE_DISK, 
								ResourceUsage.RESOURCEUSAGE_ALL, 
								ResourceDisplayType.RESOURCEDISPLAYTYPE_SERVER,pS);


							foreach	(string	s1 in servers)
							{
								string s2="";


								if((s1.Length <6)||(String.Compare(s1.Substring(s1.Length-6,6),"-share")!=0))
								{
									s2 = s1;//.Substring(s1.IndexOf("\\",2));
									nodeNN = new TreeNode();
									nodeNN.Tag =  s2;
									nodeNN.Text = s2.Substring(2) ;
									nodeNN.ImageIndex = 12;
									nodeNN.SelectedImageIndex = 12;
									n.Nodes.Add(nodeNN);
									foreach	(string	s1node in servers)
									{
										if (s1node.Length >6)
										{
											if(String.Compare(s1node.Substring(s1node.Length-6,6),"-share")==0)
											{
												if (s2.Length <=s1node.Length )
												{
													try
													{
														if (String.Compare(s1node.Substring(0,s2.Length+1),s2 + @"\")==0)  
														{
															nodeNNode = new TreeNode();
															nodeNNode.Tag =  s1node.Substring(0,s1node.Length -6);
															nodeNNode.Text = s1node.Substring(s2.Length+1,s1node.Length -s2.Length-7) ;
															nodeNNode.ImageIndex = 28;
															nodeNNode.SelectedImageIndex = 28;
															nodeNN.Nodes.Add(nodeNNode);
														}
													}
													catch(Exception)
													{}
												}
											}
										}

									}
								}

							}
						}
					}
				}
				else
				{	
					ExploreTreeNode(n); 
				}
			}
			Cursor.Current = Cursors.Default;
		}

		private void tvwMain_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode n;
			n =  e.Node ;
				
			try
			{
				if ((String.Compare(n.Text ,"My Computer")==0) ||(String.Compare(n.Text ,"My Network Places")==0)||(String.Compare(n.Text ,"Entire Network")==0) )
				{
				}
				else
				{
					txtPath.Text = n.Tag.ToString() ; 
					
			
				}
			}
			catch{}
		}

		private void tvwMain_DoubleClick(object sender, System.EventArgs e)
		{
			
			TreeNode n;
			n = tvwMain.SelectedNode ;
			
			if (!tvwMain.SelectedNode.IsExpanded) 
				tvwMain.SelectedNode.Collapse();
			else
			{
					ExploreTreeNode(n);
			}
		}
		public void refreshFolders()
		{
			listView1.Items.Clear();   
			tvwMain.Nodes.Clear();
			setCurrentPath(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
			GetDirectory();
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;   
			refreshView();
			
			try
			{
				refreshFolders();
			}
			catch(Exception e1)
			{
				MessageBox.Show ("Error: " + e1.Message); 
			}
			finally
			{
				setCurrentPath("home");
				Cursor.Current = Cursors.Default;
				ExploreMyComputer();
			}

		}
		private void ExploreMyComputer()
		{
			
			string [] drives = Environment.GetLogicalDrives();
			string dir2 ="";

			Cursor.Current = Cursors.WaitCursor;   
			TreeNode nodeDrive;

			if(TreeNodeMyComputer.GetNodeCount(true) <2)
			{
				TreeNodeMyComputer.FirstNode.Remove();
 
				foreach(string drive in drives)
				{
					nodeDrive = new TreeNode();
					nodeDrive.Tag = drive;
				
					nodeDrive.Text = drive ;
					
					switch(Win32.GetDriveType(drive))
					{
						case 2:
							nodeDrive.ImageIndex = 17;
							nodeDrive.SelectedImageIndex  = 17;
							break;
						case 3:
							nodeDrive.ImageIndex = 0;
							nodeDrive.SelectedImageIndex  = 0;
							break;
						case 4:
							nodeDrive.ImageIndex = 8;
							nodeDrive.SelectedImageIndex = 8;
							break;
						case 5:
							nodeDrive.ImageIndex = 7;
							nodeDrive.SelectedImageIndex = 7;
							break;
						default:
							nodeDrive.ImageIndex = 0;
							nodeDrive.SelectedImageIndex = 0;
							break;
					}
						
					TreeNodeMyComputer.Nodes.Add(nodeDrive);
					try
					{
						//add dirs under drive
						if (Directory.Exists (drive))
						{
							foreach(string dir in Directory.GetDirectories(drive))
							{
								dir2 = dir;
								node = new TreeNode();
								node.Tag = dir;
								node.Text = dir.Substring(dir.LastIndexOf(@"\") + 1);
								node.ImageIndex = 1;
								nodeDrive.Nodes.Add(node);
							}
						}
					
					
					}
					catch(Exception ex)	//error just add blank dir
					{
						 MessageBox.Show ("Error while Filling the Explorer:" + ex.Message );
					}
				}
			}
			
			TreeNodeMyComputer.Expand();
		}

		private void UpdateListAddCurrent()
		{
			int i =0;
			int j =0;
			
			int icount =0;
            icount = listView1.Items.Count + 1;

				for (i = 0;i< listView1.Items.Count-1;i++)
				{
					if (String.Compare(listView1.Items[i].SubItems[1].Text,"Selected")==0)
					{
						for (j = listView1.Items.Count-1;j> i + 1;j--)
     						listView1.Items[j].Remove();
						break;	
					}				  
					
				}		
		}
		private void UpdateListGoBack() 
		{	
			if ((listView1.Items.Count >0)&&(String.Compare(listView1.Items[0].SubItems[1].Text,"Selected")==0))
				return;
 			int i=0;
			for (i = 0;i< listView1.Items.Count;i++)
			{
				if (String.Compare(listView1.Items[i].SubItems[1].Text,"Selected")==0)
				{
					if (i != 0)
					{
						listView1.Items[i - 1].SubItems[1].Text = "Selected";
						txtPath.Text =listView1.Items[i - 1].Text;
					}
				}
				if (i != 0)
				{
					listView1.Items[i].SubItems[1].Text = " -/- ";
				}
			}
			}
		private void UpdateListGoFwd()
		{
			if ((listView1.Items.Count >0)&&(String.Compare(listView1.Items[listView1.Items.Count -1 ].SubItems[1].Text,"Selected")==0))
				return;
			int i=0;
			for (i = listView1.Items.Count-1;i >= 0;i--)
			{
				if (String.Compare(listView1.Items[i].SubItems[1].Text,"Selected")==0)
				{
					if (i != listView1.Items.Count) 
					{
						listView1.Items[i + 1].SubItems[1].Text = "Selected";
						txtPath.Text =listView1.Items[i + 1].Text;   
					}
				}

				if (i != listView1.Items.Count-1) listView1.Items[i].SubItems[1].Text = " -/- ";
			}
		}
		private void updateList(string f)
{
	int i=0;
	ListViewItem listviewitem;		// Used for creating listview items.

	int icount =0;
	UpdateListAddCurrent();
	icount = listView1.Items.Count + 1;
	try
	{
		if (listView1.Items.Count> 0)
		{    
			if (String.Compare(listView1.Items[listView1.Items.Count-1].Text, f)==0)
			{
				return;
			}
		}
	
		for (i = 0;i<listView1.Items.Count;i++)
		{
			listView1.Items[i].SubItems[1].Text = " -/- ";
		}
		listviewitem = new ListViewItem(f);
		listviewitem.SubItems.Add("Selected");
		listviewitem.Tag = f;
		this.listView1.Items.Add(listviewitem);
	}
	catch(Exception e)
	{
	MessageBox.Show(e.Message);   
	}
}
		public void btnGo_Click(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;   
			try
			{
				ExploreMyComputer(); 
				string myString ="";
				int h=1;
				myString = txtPath.Text.ToLower()  ;
				//if (String.Compare(myString.Substring(myString.Length-1,1),@"\")==0)
				//{
				//	myString = myString.Substring(0,myString.Length-1);
				//	txtPath.Text = myString	;

				//}
				TreeNode tn = TreeNodeMyComputer  ;

			StartAgain:
			
				do
				{
					//Strom = (tvwMain.GetNodeCount(true)).ToString() ;	
					
					foreach(TreeNode t in tn.Nodes) 
					{
						string mypath =  t.Tag.ToString()  ;
						//mypath =  mypath.Replace("Desktop\\","") ;
						//mypath =  mypath.Replace("My Computer\\","") ;
						//mypath =  mypath.Replace(@"\\",@"\") ;

						//mypath =  mypath.Replace("My Documents\\",Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\") ;
						mypath=mypath.ToLower();
						string mypathf =mypath;
						if (!mypath.EndsWith(@"\"))  
						{
						if (myString.Length > mypathf.Length )	mypathf  =mypath + @"\";
						}

						if (myString.StartsWith(mypathf))
						{
							t.TreeView.Focus(); 
							t.TreeView.SelectedNode =  t; 
							t.EnsureVisible(); 
							t.Expand();
							if (t.Nodes.Count>=1)
							{
								t.Expand();
								tn = t;
							}
							else
							{
								if (String.Compare (myString,mypath)==0)
								{
									h = -1;
									break;
								}
								else
								{
									continue;  
								}
							}

							if (mypathf.StartsWith(myString))
							{
								h = -1;
								break;
							}
							else
							{
								goto  StartAgain;
								//return;
							}
						}
					}
				
					try
					{
						tn = tn.NextNode;
					}
					catch(Exception)
					{}
 
				}while(h>=0);

			}
			catch(Exception e1)
			{
				MessageBox.Show ("Error: " + e1.Message); 
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void btnHome_Click(object sender, System.EventArgs e)
		{
			setCurrentPath("home"); 
			ExploreMyComputer(); 
			btnGo_Click(sender,e); 
			
		}


		private void btnNext_Click(object sender, System.EventArgs e)
		{
			GoFlag = true;
			string cpath = txtPath.Text ; 
			UpdateListGoFwd();  
			
			if (String.Compare( cpath,txtPath.Text)==0)
			{}
			else
			{
				btnGo_Click(sender,e); 
			}
			GoFlag = false;
		}

		private void btnBack_Click(object sender, System.EventArgs e)
		{
			GoFlag = true;
			string cpath = txtPath.Text ; 
			UpdateListGoBack();  
			
			if (String.Compare( cpath,txtPath.Text)==0)
			{}
			else
			{
				btnGo_Click(sender,e); 
			}
			GoFlag = false;
		}

		private void tvwMain_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			updateList(txtPath.Text);   
			if (tvwMain.SelectedNode != null)
			{

				if ((tvwMain.SelectedNode.ImageIndex == 18)&&(e.Button==MouseButtons.Right))
					cMShortcut.Show(tvwMain ,new Point(e.X,e.Y)); 
			}
		}

		private void btnUp_Click(object sender, System.EventArgs e)
		{
			try
			{
				DirectoryInfo MYINFO = new DirectoryInfo(txtPath.Text);
				
				if (MYINFO.Parent.Exists)
					txtPath.Text = MYINFO.Parent.FullName;
				updateList( txtPath.Text);
				btnGo_Click(sender,e); 
			}
			catch(Exception)
			{
				//MessageBox.Show ("Parent directory does not exists","Directory Not Found",MessageBoxButtons.OK,MessageBoxIcon.Information ); 
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			string myname="";
			string mypath="";


			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Add Folder in Explorer Tree";
			dialog.ShowNewFolderButton = true;
			dialog.SelectedPath = txtPath.Text  ;

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				mypath = dialog.SelectedPath;
				myname = mypath.Substring(mypath.LastIndexOf("\\")+1);
				
				AddFolderNode(myname,mypath);
				
			}
		}
		private void AddFolderNode(string name, string path)
		{

			try
			{
				TreeNode nodemyC = new TreeNode();
			
				nodemyC.Tag = path;
				nodemyC.Text = name;

				nodemyC.ImageIndex = 18;
				nodemyC.SelectedImageIndex = 18;

				TreeNodeRootNode.Nodes.Add(nodemyC); 

				try
				{
					//add dirs under drive
					if (Directory.Exists (path))
					{
						foreach(string dir in Directory.GetDirectories(path))
						{
							TreeNode node = new TreeNode();
							node.Tag = dir;
							node.Text = dir.Substring(dir.LastIndexOf(@"\") + 1);
							node.ImageIndex = 1;
							nodemyC.Nodes.Add(node);
						}
					}
				}
				catch(Exception ex)	//error just add blank dir
				{
					MessageBox.Show ("Error while Filling the Explorer:" + ex.Message );
				}
			}
			catch(Exception e)
			{
				MessageBox.Show (e.Message);  
			}
		}

		private void mnuShortcut_Click(object sender, System.EventArgs e)
		{
			if (tvwMain.SelectedNode.ImageIndex ==18)  
				tvwMain.SelectedNode.Remove();
		}

		private void txtPath_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				if( Directory.Exists(txtPath.Text))
				{
					SelectedPath = txtPath.Text;
					PathChangedEvent(this,EventArgs.Empty); 
				}
			}
			catch(Exception)
			{}
		}

		public void AboutExplorerTree()
		{
			frmDBExplorerTreeOptions form = new frmDBExplorerTreeOptions(showMyDocuments,showMyFavorites,showMyNetwork,showAddressbar,showToolbar   );
			if (form.ShowDialog() == DialogResult.OK)
			{
				showMyDocuments = form.myDocument;
				showMyNetwork  = form.myNetwork;
				ShowMyFavorites = form.myFavorite;
				ShowAddressbar = form.myAddressbar;
				ShowToolbar = form.myToolbar;
  
				btnRefresh_Click(this,null); 
 			}
		}

		private void txtPath_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//MessageBox.Show(e.KeyChar.ToString());   
		}

		private void txtPath_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(	e.KeyValue ==13)
			{
				btnGo_Click(sender,e); 
				txtPath.Focus();
			}
  
		}

		private void btnInfo_Click(object sender, System.EventArgs e)
		{
			AboutExplorerTree();
		
		}

		private void grptoolbar_Enter(object sender, System.EventArgs e)
		{		
		}

	}
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	/// 
	
									
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct SHQUERYRBINFO
	{
		public uint cbSize;     
		public ulong i64Size;
		public ulong i64NumItems;
	};
								
	//Shell functions
	public class Win32
	{
		public const uint SHGFI_ICON = 0x100;
		//public const uint SHGFI_LARGEICON = 0x0;    // 'Large icon
		public const uint SHGFI_SMALLICON = 0x1;    // 'Small icon
								
		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(
			string pszPath,
			uint dwFileAttributes,
			ref SHFILEINFO psfi,
			uint cbSizeFileInfo,
			uint uFlags);
		
		[DllImport("kernel32")]
		public static extern uint GetDriveType(
			string lpRootPathName);

		[DllImport("shell32.dll")]
		public static extern bool SHGetDiskFreeSpaceEx(          
			string pszVolume,
			ref ulong pqwFreeCaller,
			ref ulong pqwTot,
			ref ulong pqwFree);

		[DllImport("shell32.Dll")]
		public static extern int SHQueryRecycleBin(          
			string pszRootPath,
			ref SHQUERYRBINFO pSHQueryRBInfo);

		[StructLayout(LayoutKind.Sequential)]
			public struct SHFILEINFO 
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

	

		[StructLayout( LayoutKind.Sequential )]
		public class BITMAPINFO
		{
			public Int32 biSize;
			public Int32 biWidth;
			public Int32 biHeight;
			public Int16 biPlanes;
			public Int16 biBitCount;
			public Int32 biCompression;
			public Int32 biSizeImage;
			public Int32 biXPelsPerMeter;
			public Int32 biYPelsPerMeter;
			public Int32 biClrUsed;
			public Int32 biClrImportant;
			public Int32 colors;
		};
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_Add( IntPtr hImageList, IntPtr hBitmap, IntPtr hMask );
		[DllImport("kernel32.dll")]
		private static extern bool RtlMoveMemory( IntPtr dest, IntPtr source, int dwcount );
		[DllImport("shell32.dll")]
		public static extern IntPtr DestroyIcon( IntPtr hIcon );
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDIBSection( IntPtr hdc, [In, MarshalAs(UnmanagedType.LPStruct)]BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset );

		
	}

	public enum ResourceScope
	{
		RESOURCE_CONNECTED = 1,
		RESOURCE_GLOBALNET,
		RESOURCE_REMEMBERED,
		RESOURCE_RECENT,
		RESOURCE_CONTEXT
	};

	public enum ResourceType
	{
		RESOURCETYPE_ANY,
		RESOURCETYPE_DISK,
		RESOURCETYPE_PRINT,
		RESOURCETYPE_RESERVED
	};

	public enum ResourceUsage
	{
		RESOURCEUSAGE_CONNECTABLE   = 0x00000001,
		RESOURCEUSAGE_CONTAINER     = 0x00000002,
		RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
		RESOURCEUSAGE_SIBLING       = 0x00000008,
		RESOURCEUSAGE_ATTACHED      = 0x00000010,
		RESOURCEUSAGE_ALL           = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
	};
	
	public enum ResourceDisplayType
	{
		RESOURCEDISPLAYTYPE_GENERIC,
		RESOURCEDISPLAYTYPE_DOMAIN,
		RESOURCEDISPLAYTYPE_SERVER,
		RESOURCEDISPLAYTYPE_SHARE,
		RESOURCEDISPLAYTYPE_FILE,
		RESOURCEDISPLAYTYPE_GROUP,
		RESOURCEDISPLAYTYPE_NETWORK,
		RESOURCEDISPLAYTYPE_ROOT,
		RESOURCEDISPLAYTYPE_SHAREADMIN,
		RESOURCEDISPLAYTYPE_DIRECTORY,
		RESOURCEDISPLAYTYPE_TREE,
		RESOURCEDISPLAYTYPE_NDSCONTAINER
	};

	public class ServerEnum : IEnumerable
	{
		enum ErrorCodes
		{
			NO_ERROR = 0,
			ERROR_NO_MORE_ITEMS = 259
		};

		[StructLayout(LayoutKind.Sequential)]
			private class NETRESOURCE 
		{
			public ResourceScope       dwScope = 0;
			public ResourceType        dwType = 0;
			public ResourceDisplayType dwDisplayType = 0;
			public ResourceUsage       dwUsage = 0;
			public string              lpLocalName = null;
			public string              lpRemoteName = null;
			public string              lpComment = null;
			public string              lpProvider = null;
		};
	

		private ArrayList aData = new ArrayList();
		

		public int Count
		{
			get { return aData.Count; }
		}
	
		[DllImport("Mpr.dll", EntryPoint="WNetOpenEnumA", CallingConvention=CallingConvention.Winapi)]
		private static extern ErrorCodes WNetOpenEnum(ResourceScope dwScope, ResourceType dwType, ResourceUsage dwUsage, NETRESOURCE p, out IntPtr lphEnum);

		[DllImport("Mpr.dll", EntryPoint="WNetCloseEnum", CallingConvention=CallingConvention.Winapi)]
		private static extern ErrorCodes WNetCloseEnum(IntPtr hEnum);

		[DllImport("Mpr.dll", EntryPoint="WNetEnumResourceA", CallingConvention=CallingConvention.Winapi)]
		private static extern ErrorCodes WNetEnumResource(IntPtr hEnum, ref uint lpcCount, IntPtr buffer, ref uint lpBufferSize);

	
		private	void EnumerateServers(NETRESOURCE pRsrc, ResourceScope scope, ResourceType type, ResourceUsage usage, ResourceDisplayType displayType,string kPath)
		{
		uint		bufferSize = 16384;
		IntPtr		buffer	= Marshal.AllocHGlobal((int) bufferSize);
		IntPtr		handle = IntPtr.Zero;
		ErrorCodes	result;
		uint		cEntries = 1;
		bool serverenum = false;

		result = WNetOpenEnum(scope, type, usage, pRsrc, out handle);

		if (result == ErrorCodes.NO_ERROR)
		{
			do
			{
				result = WNetEnumResource(handle, ref cEntries,	buffer,	ref	bufferSize);

				if ((result == ErrorCodes.NO_ERROR))
				{
					Marshal.PtrToStructure(buffer, pRsrc);

					if(String.Compare(kPath,"")==0)
					{
						if ((pRsrc.dwDisplayType	== displayType) || (pRsrc.dwDisplayType	== ResourceDisplayType.RESOURCEDISPLAYTYPE_DOMAIN))
							aData.Add(pRsrc.lpRemoteName + "|" + pRsrc.dwDisplayType );

						if ((pRsrc.dwUsage & ResourceUsage.RESOURCEUSAGE_CONTAINER )== ResourceUsage.RESOURCEUSAGE_CONTAINER )
						{	
							if ((pRsrc.dwDisplayType	== displayType))
							{
								EnumerateServers(pRsrc,	scope, type, usage,	displayType,kPath);
								
							}
								
						}
					}
					else
					{
						if (pRsrc.dwDisplayType	== displayType)
						{
							aData.Add(pRsrc.lpRemoteName);
							EnumerateServers(pRsrc,	scope, type, usage,	displayType,kPath);
							//return;
							serverenum = true;
						}
						if (!serverenum)
						{
							if (pRsrc.dwDisplayType	== ResourceDisplayType.RESOURCEDISPLAYTYPE_SHARE)
							{
								aData.Add(pRsrc.lpRemoteName + "-share");
							}
						}
						else
						{
							serverenum =false;
						}
						if((kPath.IndexOf(pRsrc.lpRemoteName)>=0)||(String.Compare(pRsrc.lpRemoteName,"Microsoft Windows Network")==0))
						{
							EnumerateServers(pRsrc,	scope, type, usage,	displayType,kPath);
							//return;
							
						}
						//}
					}
				
				}
				else if	(result	!= ErrorCodes.ERROR_NO_MORE_ITEMS)
					break;
			} while	(result	!= ErrorCodes.ERROR_NO_MORE_ITEMS);

			WNetCloseEnum(handle);
		}

		Marshal.FreeHGlobal((IntPtr) buffer);
		}

		public ServerEnum(ResourceScope scope, ResourceType type, ResourceUsage usage, ResourceDisplayType displayType,string kPath)
		{
			
			NETRESOURCE netRoot = new NETRESOURCE();
			EnumerateServers(netRoot, scope, type, usage, displayType,kPath);
		
		}
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return aData.GetEnumerator();
		}

		#endregion
	}
}
