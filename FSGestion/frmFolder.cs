
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;


using System.IO;
using System.Resources;
using System.ComponentModel;
using System.Runtime.InteropServices;

//------------------------------------------------------------------------------
/// <copyright from='1997' to='2001' company='Microsoft Corporation'>
///    Copyright (c) Microsoft Corporation. All Rights Reserved.
///
///    This source code is intended only as a supplement to Microsoft
///    Development Tools and/or on-line documentation.  See these other
///    materials for detailed information regarding Microsoft code samples.
///
/// </copyright>
//------------------------------------------------------------------------------


// <doc>
// <desc>
//     This sample demonstrates how to use the Treeview control
// </desc>
// </doc>
//
namespace FSGestion
{
	public class TreeViewCtl : System.Windows.Forms.Form
	{
		
		public TreeViewCtl()
		{
			
			
			TreeViewCtl_Renamed = this;
			
			//This call is required by the Windows Forms Designer.
			InitializeComponent();
			
			FillDirectoryTree();
			imageListComboBox.SelectedIndex = 1;
			indentUpDown.Value = directoryTree.Indent;
		}
		
		
		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			components.Dispose();
		}
		
		
		
		// <doc>
		// <desc>
		//     For a given root directory (or drive), add the directories to the
		//     directoryTree.
		// </desc>
		// </doc>
		//
		private void AddDirectories(TreeNode node)
		{
			try
			{
				DirectoryInfo dir = new DirectoryInfo(GetPathFromNode(node));
				DirectoryInfo[] e = dir.GetDirectories();
				int i = default(int);
				
				for (i = 0; i <= e.Length - 1; i++)
				{
					string name = e[i].Name;
					if (!name.Equals(".") && !name.Equals(".."))
					{
						node.Nodes.Add(new DirectoryNode(name));
					}
				}
			}
			catch (System.Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
		
		
		// <doc>
		// <desc>
		//     For a given node, add the sub-directories for node's children in the
		//     directoryTree.
		// </desc>
		// </doc>
		//
		private void AddSubDirectories(DirectoryNode node)
		{
			int i = default(int);
			for (i = 0; i <= node.Nodes.Count - 1; i++)
			{
				AddDirectories(node.Nodes[i]);
			}
			node.SubDirectoriesAdded = true;
		}
		
		
		// <doc>
		// <desc>
		//     Event handler for the afterSelect event on the directoryTree. Change the
		//     title bar to show the path of the selected directoryNode.
		// </desc>
		// </doc>
		//
		private void directoryTree_AfterSelect(System.Object source, System.Windows.Forms.TreeViewEventArgs e)
		{
			Text = "Explorador de archivos de Windows.Forms - " + e.Node.Text;
		}
		
		
		// <doc>
		// <desc>
		//     Event handler for the beforeExpand event on the directoryTree. If the
		//     node is not already expanded, expand it.
		// </desc>
		// </doc>
		//
		private void directoryTree_BeforeExpand(System.Object source, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			DirectoryNode nodeExpanding = (DirectoryNode) e.Node;
			if (!nodeExpanding.SubDirectoriesAdded)
			{
				AddSubDirectories(nodeExpanding);
			}
		}
		
		// <doc>
		// <desc>
		//      For initializing the directoryTree upon creation of the TreeViewCtl form.
		// </desc>
		// </doc>
		//
		private void FillDirectoryTree()
		{
			int i = default(int);
			string[] drives = Environment.GetLogicalDrives();
			for (i = 0; i <= drives.Length - 1; i++)
			{
				if (PlatformInvokeKernel32.GetDriveType(drives[i]) == PlatformInvokeKernel32.DRIVE_FIXED)
				{
					DirectoryNode cRoot = new DirectoryNode(drives[i]);
					directoryTree.Nodes.Add(cRoot);
					AddDirectories(cRoot);
				}
			}
		}
		
		
		// <doc>
		// <desc>
		//        Returns the directory path of the node.
		// </desc>
		// <retvalue>
		//        Directory path of node.
		// </retvalue>
		// </doc>
		//
		private string GetPathFromNode(TreeNode node)
		{
			if (node.Parent == null)
			{
				return node.Text;
			}
			return Path.Combine(GetPathFromNode(node.Parent), node.Text);
		}
		
		
		// <doc>
		// <desc>
		//        Refresh helper functions to get all expanded nodes under the given
		//        node.
		// </desc>
		// <param term='expandedNodes'>
		//        Reference to an array of paths containing all nodes which were in the
		//        expanded state when Refresh was requested.
		// </param>
		// <param term='startIndex'>
		//        Array index of ExpandedNodes to start adding entries to.
		// </param>
		// <retvalue>
		//        New StartIndex, i.e. given value of StartIndex + number of entries
		//        added to ExpandedNodes.
		// </retvalue>
		// </doc>
		//
		private int Refresh_GetExpanded(TreeNode Node, string[] ExpandedNodes, int StartIndex)
		{
			if (StartIndex < ExpandedNodes.Length)
			{
				if (Node.IsExpanded)
				{
					
					ExpandedNodes[StartIndex] = Node.Text;
					StartIndex++;
					int i = default(int);
					for (i = 0; i <= Node.Nodes.Count - 1; i++)
					{
						StartIndex = Refresh_GetExpanded(Node.Nodes[i], ExpandedNodes, StartIndex);
					}
				}
				return StartIndex;
			}
			return -1;
		}
		
		// <doc>
		// <desc>
		//        Refresh helper function to expand all nodes whose paths are in parameter
		//        ExpandedNodes.
		// </desc>
		// <param term='node'>
		//        Node from which to start expanding.
		// </param>
		// <param term='expandedNodes'>
		//        Array of strings with the path names of all nodes to expand.
		// </param>
		// </doc>
		//
		private void Refresh_Expand(TreeNode Node, string[] ExpandedNodes)
		{
			int i = default(int);
			
			for (i = ExpandedNodes.Length - 1; i >= 0; i--)
			{
				if (ExpandedNodes[i] == Node.Text)
				{
					// For the expand button to show properly, one level of
					// invisible children have to be added to the tree.
					AddSubDirectories((DirectoryNode) Node);
					Node.Expand();
					int j = default(int);
					
					// If the node is expanded, expand any children that were
					// expanded before the refresh.
					for (j = 0; j <= Node.Nodes.Count - 1; j++)
					{
						Refresh_Expand(Node.Nodes[j], ExpandedNodes);
					}
					
					return ;
				}
			}
		}
		
		
		// <doc>
		// <desc>
		//     Refreshes the view by deleting all the nodes and restoring them by
		//     reading the disk(s). Any expanded nodes in the directoryView will be
		//     expanded after the refresh.
		// </desc>
		// <param term='node'>
		//     - Node from which the refresh begins. Generally, this is
		//     the root.
		// </param>
		// </doc>
		//
		private void Refresh(TreeNode node)
		{
			if (node.Nodes.Count > 0)
			{
				if (node.IsExpanded)
				{
					string[] tooBigExpandedNodes = new string[node.GetNodeCount(true) + 1];
					int iExpandedNodes = Refresh_GetExpanded(node, tooBigExpandedNodes, 0);
					string[] expandedNodes = new string[iExpandedNodes + 1];
					// Update the directoryTree
					// Save all expanded nodes rooted at node, even those that are
					// indirectly rooted.
					Array.Copy(tooBigExpandedNodes, 0, expandedNodes, 0, iExpandedNodes);
					
					node.Nodes.Clear();
					AddDirectories(node);
					
					// so children with subdirectories show up with expand/collapse
					// button.
					AddSubDirectories((DirectoryNode) node);
					node.Expand();
					int j = default(int);
					
					// check all children. Some might have had sub-directories added
					// from an external application so previous childless nodes
					// might now have children.
					for (j = 0; j <= node.Nodes.Count - 1; j++)
					{
						if (node.Nodes[j].Nodes.Count > 0)
						{
							// If the child has subdirectories. If it was expanded
							// before the refresh, then expand after the refresh.
							Refresh_Expand(node.Nodes[j], expandedNodes);
						}
					}
				}
				else
				{
					// If the node is not expanded, then there is no need to check
					// if any of the children were expanded. However, we should
					// update the tree by reading the drive in case an external
					// application add/removed any directories.
					node.Nodes.Clear();
					AddDirectories(node);
				}
			}
			else
			{
				// Again, if there are no children, then there is no need to
				// worry about expanded nodes but if an external application
				// add/removed any directories we should reflect that.
				node.Nodes.Clear();
				AddDirectories(node);
			}
		}
		
		private void checkBox1_Click(System.Object source, System.EventArgs e)
		{
			this.directoryTree.Sorted = checkBox1.Checked;
			int i = default(int);
			for (i = 0; i <= directoryTree.Nodes.Count - 1; i++)
			{
				Refresh(directoryTree.Nodes[i]);
			}
			
		}
		
		private void imageListComboBox_SelectedIndexChanged(System.Object source, System.EventArgs e)
		{
			int index = imageListComboBox.SelectedIndex;
			if (index == 0)
			{
				directoryTree.ImageList = null;
			}
			else
			{
				if (index == 1)
				{
					directoryTree.ImageList = imageList1;
				}
				else
				{
					directoryTree.ImageList = imageList2;
				}
				
			}
		}
		
		private void indentUpDown_ValueChanged(System.Object source, System.EventArgs e)
		{
			directoryTree.Indent = (int) indentUpDown.Value;
		}
		
		private void CheckBox2_click(System.Object source, System.EventArgs e)
		{
			this.directoryTree.HotTracking = checkBox2.Checked;
		}
		
		private void CheckBox3_click(System.Object source, System.EventArgs e)
		{
			this.directoryTree.ShowLines = checkBox3.Checked;
		}
		
		private void CheckBox4_click(System.Object source, System.EventArgs e)
		{
			this.directoryTree.ShowRootLines = checkBox4.Checked;
		}
		
		private void CheckBox5_click(System.Object source, System.EventArgs e)
		{
			this.directoryTree.ShowPlusMinus = checkBox5.Checked;
		}
		
		private void CheckBox6_click(System.Object source, System.EventArgs e)
		{
			this.directoryTree.CheckBoxes = checkBox6.Checked;
		}
		
		private void CheckBox7_click(System.Object source, System.EventArgs e)
		{
			this.directoryTree.HideSelection = checkBox7.Checked;
		}
		private System.ComponentModel.Container components = null;
		
		
#region  El Diseñador de Windows Forms generó código
		
		//Required by the Windows Form Designer
		protected System.Windows.Forms.TreeView directoryTree;
		protected System.Windows.Forms.ImageList imageList1;
		protected System.Windows.Forms.ImageList imageList2;
		protected System.Windows.Forms.GroupBox grpTreeView;
		protected System.Windows.Forms.CheckBox checkBox1;
		protected System.Windows.Forms.ComboBox imageListComboBox;
		protected System.Windows.Forms.Label label1;
		protected System.Windows.Forms.NumericUpDown indentUpDown;
		protected System.Windows.Forms.Label label4;
		protected System.Windows.Forms.CheckBox checkBox2;
		protected System.Windows.Forms.CheckBox checkBox3;
		protected System.Windows.Forms.CheckBox checkBox4;
		protected System.Windows.Forms.CheckBox checkBox5;
		protected System.Windows.Forms.CheckBox checkBox6;
		protected System.Windows.Forms.CheckBox checkBox7;
		protected System.Windows.Forms.ToolTip toolTip1;
		
		private System.Windows.Forms.Form TreeViewCtl_Renamed;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TreeViewCtl));
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox7.Click += new System.EventHandler(this.CheckBox7_click);
			this.directoryTree = new System.Windows.Forms.TreeView();
			this.directoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.directoryTree_AfterSelect);
			this.directoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.directoryTree_BeforeExpand);
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox5.Click += new System.EventHandler(this.CheckBox5_click);
			this.label4 = new System.Windows.Forms.Label();
			this.indentUpDown = new System.Windows.Forms.NumericUpDown();
			this.indentUpDown.ValueChanged += new System.EventHandler(this.indentUpDown_ValueChanged);
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox6.Click += new System.EventHandler(this.CheckBox6_click);
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox3.Click += new System.EventHandler(this.CheckBox3_click);
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox4.Click += new System.EventHandler(this.CheckBox4_click);
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox2.Click += new System.EventHandler(this.CheckBox2_click);
			this.imageListComboBox = new System.Windows.Forms.ComboBox();
			this.imageListComboBox.SelectedIndexChanged += new System.EventHandler(this.imageListComboBox_SelectedIndexChanged);
			this.grpTreeView = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize) this.indentUpDown).BeginInit();
			this.grpTreeView.SuspendLayout();
			this.SuspendLayout();
			//
			//checkBox7
			//
			this.checkBox7.Checked = true;
			this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox7.Location = new System.Drawing.Point(16, 160);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(100, 23);
			this.checkBox7.TabIndex = 6;
			this.checkBox7.Text = "hideSelected";
			this.toolTip1.SetToolTip(this.checkBox7, "Quita el resalte del nodo seleccionado cuando el control no tiene foco.");
			//
			//directoryTree
			//
			this.directoryTree.AllowDrop = true;
			this.directoryTree.ForeColor = System.Drawing.SystemColors.WindowText;
			this.directoryTree.ImageList = this.imageList1;
			this.directoryTree.Indent = 19;
			this.directoryTree.Location = new System.Drawing.Point(24, 16);
			this.directoryTree.Name = "directoryTree";
			this.directoryTree.SelectedImageIndex = 1;
			this.directoryTree.Size = new System.Drawing.Size(200, 264);
			this.directoryTree.TabIndex = 0;
			this.directoryTree.Text = "treeView1";
			this.toolTip1.SetToolTip(this.directoryTree, "Indica si aparecen líneas entre nodos relacionados y entre nodos " + "primarios y secundarios");
			//
			//imageList1
			//
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList1.ImageStream"));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			//
			//checkBox5
			//
			this.checkBox5.Checked = true;
			this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox5.Location = new System.Drawing.Point(16, 112);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(120, 23);
			this.checkBox5.TabIndex = 4;
			this.checkBox5.Text = "showPlusMinus";
			this.toolTip1.SetToolTip(this.checkBox5, "Indica si se deben mostrar botones más o menos junto a los primarios.");
			//
			//label4
			//
			this.label4.Location = new System.Drawing.Point(16, 224);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "indent:";
			//
			//indentUpDown
			//
			this.indentUpDown.Location = new System.Drawing.Point(88, 224);
			this.indentUpDown.Maximum = new decimal(new int[] {150, 0, 0, 0});
			this.indentUpDown.Minimum = new decimal(new int[] {18, 0, 0, 0});
			this.indentUpDown.Name = "indentUpDown";
			this.indentUpDown.Size = new System.Drawing.Size(136, 20);
			this.indentUpDown.TabIndex = 10;
			this.toolTip1.SetToolTip(this.indentUpDown, "Ancho de sangría de los nodos secundarios en píxeles.");
			this.indentUpDown.Value = new decimal(new int[] {18, 0, 0, 0});
			//
			//imageList2
			//
			this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList2.ImageStream"));
			this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
			//
			//checkBox6
			//
			this.checkBox6.Location = new System.Drawing.Point(16, 136);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(100, 23);
			this.checkBox6.TabIndex = 5;
			this.checkBox6.Text = "checkBoxes";
			this.toolTip1.SetToolTip(this.checkBox6, "Indica si se muestran casillas de verificación junto a los nodos");
			//
			//checkBox1
			//
			this.checkBox1.Location = new System.Drawing.Point(16, 16);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(100, 23);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "sorted";
			this.toolTip1.SetToolTip(this.checkBox1, "Indica si los nodos están ordenados.");
			//
			//checkBox3
			//
			this.checkBox3.Checked = true;
			this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox3.Location = new System.Drawing.Point(16, 64);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(100, 23);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "showLines";
			this.toolTip1.SetToolTip(this.checkBox3, "Indica si se muestran líneas entre nodos relacionados y entre nodos " + "primarios y secundarios.");
			//
			//checkBox4
			//
			this.checkBox4.Checked = true;
			this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox4.Location = new System.Drawing.Point(16, 88);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(120, 23);
			this.checkBox4.TabIndex = 3;
			this.checkBox4.Text = "showRootLines";
			this.toolTip1.SetToolTip(this.checkBox4, "Indica si se muestran las líneas entre nodos de raíz.");
			//
			//checkBox2
			//
			this.checkBox2.Location = new System.Drawing.Point(16, 40);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(100, 23);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "hotTracking";
			this.toolTip1.SetToolTip(this.checkBox2, "Indica si los nodos proporcionan comentarios cuando el mouse se mueve sobre ellos.");
			//
			//imageListComboBox
			//
			this.imageListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.imageListComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
			this.imageListComboBox.Items.AddRange(new object[] {"(ninguno)", "imágenes del sistema", "bitmaps"});
			this.imageListComboBox.Location = new System.Drawing.Point(88, 192);
			this.imageListComboBox.Name = "imageListComboBox";
			this.imageListComboBox.Size = new System.Drawing.Size(136, 21);
			this.imageListComboBox.TabIndex = 8;
			//
			//grpTreeView
			//
			this.grpTreeView.Controls.AddRange(new System.Windows.Forms.Control[] {this.checkBox7, this.checkBox6, this.checkBox5, this.checkBox4, this.checkBox3, this.checkBox2, this.label4, this.indentUpDown, this.label1, this.imageListComboBox, this.checkBox1});
			this.grpTreeView.Location = new System.Drawing.Point(248, 16);
			this.grpTreeView.Name = "grpTreeView";
			this.grpTreeView.Size = new System.Drawing.Size(248, 264);
			this.grpTreeView.TabIndex = 1;
			this.grpTreeView.TabStop = false;
			this.grpTreeView.Text = "TreeView";
			//
			//label1
			//
			this.label1.Location = new System.Drawing.Point(16, 194);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "imageList:";
			//
			//TreeViewCtl
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(502, 293);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {this.grpTreeView, this.directoryTree});
			this.Name = "TreeViewCtl";
			this.Text = "TreeView";
			((System.ComponentModel.ISupportInitialize) this.indentUpDown).EndInit();
			this.grpTreeView.ResumeLayout(false);
			this.ResumeLayout(false);
			
		}
		
#endregion
		
	}
	
	public class DirectoryNode : System.Windows.Forms.TreeNode
	{
		
		public bool SubDirectoriesAdded;
		
		public DirectoryNode(string text) : base(text)
		{
		}
		
	}
	
	public class PlatformInvokeKernel32
	{
		[DllImport("KERNEL32", ExactSpelling=true, CharSet=CharSet.Auto, SetLastError=true)]
		public static extern int GetDriveType(string lpRootPathName);
		public static int DRIVE_FIXED = 3;
	}
	
	
	
	
}
