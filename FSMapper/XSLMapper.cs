using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace XML2XML
{
	public partial class XSLMapper : Form
	{
		public XSLMapper()
		{
			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			Load += XSLMapper_Load;
			tvTargetXML.DragOver += tvTargetXML_DragOver;
			tvSourceXML.DragEnter += tvSourceXML_DragEnter;
			tvTargetXML.DragDrop += tvTargetXML_DragDrop;
			tvSourceXML.ItemDrag += tvSourceXML_ItemDrag;
			tvTargetXML.MouseUp += tvTargetXML_MouseUp;
			tvTargetXML.MouseDown += tvTargetXML_MouseDown;
			tvSourceXML.MouseDown += tvSourceXML_MouseDown;
			tvTargetXML.NodeMouseClick += tvTargetXML_NodeMouseClick;
			tvSourceXML.NodeMouseClick += tvSourceXML_NodeMouseClick;
			mnuItemSaveXSL.Click += mnuItemSaveXSL_Click;
			cmnuItemRemove.Click += mnuItemRemove_Click;
			cmnuItemAdd.Click += mnuItemAdd_Click;
			mnuItemRemoveAllMap.Click += mnuItemRemoveAllMap_Click;
			mnuItmSourceXml.Click += mnuItmSourceXml_Click;
			mnuItmTargetXml.Click += mnuItmTargetXml_Click;
			mnuItmExit.Click += mnuItmExit_Click;
			mnuItemSaveMappg.Click += mnuItemSaveMappg_Click;
			mnuLoadMappg.Click += mnuLoadMappg_Click;
			tbcXSLEditor.SelectedIndexChanged += tbcXSLEditor_SelectedIndexChanged;
			mnuItemCopyPathSource.Click += mnuItemCopyPathSource_Click;
			mnuItemCopyPath.Click += mnuItemCopyPathTarget_Click;
		}


		#region "Tree Loading"
		private void LoadTreeViewFromXmlDoc(XmlDocument xml_doc, TreeView trv, bool blank)
		{

			if (blank)
				XML2XMLHelper.BlankNodes(xml_doc.DocumentElement);
			
			// Add the root node's children to the TreeView.
			trv.Nodes.Clear();
			AddTreeViewChildNodes(trv.Nodes, xml_doc);
			
			if (trv.Nodes.Count > 0) {
				// Select the root node
				trv.SelectedNode = trv.Nodes[0];
			}
		}

		// Load a TreeView control from an XML file.
		// Add the children of this XML node
		// to this child nodes collection.
		private void AddTreeViewChildNodes(TreeNodeCollection parent_nodes, XmlNode xml_node)
		{
			foreach (XmlNode child_node in xml_node.ChildNodes) {
				if (child_node.NodeType == XmlNodeType.Element) {
					// Make the new TreeView node.
					TreeNode new_node = parent_nodes.Add(child_node.Name);
					
					string path = XML2XMLHelper.FindXPath(child_node);
					
					if (!String.IsNullOrEmpty(child_node.InnerText)) {
						new_node.ToolTipText = path + " (" + child_node.InnerText + ")";
					} else {
						new_node.ToolTipText = path;
					}
					new_node.Tag = path;
					new_node.ImageIndex = 0;
					// Recursively make this node's descendants.
					AddTreeViewChildNodes(new_node.Nodes, child_node);

					// If this is a leaf node, make sure it's visible.
					if (new_node.Nodes.Count == 0)
						new_node.EnsureVisible();
					
				}

			}
		}
		#endregion

		#region "Drag and Drop"
		private void tvTargetXML_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			try {
				//Check that there is a TreeNode being dragged
				if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true) == false)
					return;

				//Get the TreeView raising the event (incase multiple on form)
				TreeView selectedTreeview = (TreeView)sender;

				//As the mouse moves over nodes, provide feedback to the user
				//by highlighting the node that is the current drop target
				Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
				TreeNode targetNode = selectedTreeview.GetNodeAt(pt);

				//See if the targetNode is currently selected, if so no need to validate again
				if ((!object.ReferenceEquals(selectedTreeview, targetNode))) {
					//Select the node currently under the cursor
					selectedTreeview.SelectedNode = targetNode;

					//Check that the selected node is not the dropNode and also that it
					//is not a child of the dropNode and therefore an invalid target
					TreeNode dropNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
					while (targetNode != null) {
						if (object.ReferenceEquals(targetNode, dropNode)) {
							e.Effect = DragDropEffects.None;
							return;
						}
						targetNode = targetNode.Parent;
					}
				}

				//Currently selected node is a suitable target, allow the move
				e.Effect = DragDropEffects.Copy;
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}

		}

		private void tvTargetXML_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			try {
				//Check that there is a TreeNode being dragged
				if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true) == false)
					return;

				//Get the TreeView raising the event (incase multiple on form)
				TreeView selectedTreeview = (TreeView)sender;

				//Get the TreeNode being dragged
				TreeNode dropNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
				//The target node should be selected from the DragOver event
				TreeNode targetNode = selectedTreeview.SelectedNode;
				
				if (AddNewMapping(dropNode.Tag.ToString(), targetNode.Tag.ToString())) {
					//Change the back color of the nodes(source and target)
					ChangeSourceNodeIcon(dropNode, 1);
					ChangeTargetNodeIcon(targetNode, 1);
				}
				
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}
		private void tvSourceXML_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			try {
				//Set the drag node and initiate the DragDrop
				DoDragDrop(e.Item, DragDropEffects.Copy);
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}

		private void tvSourceXML_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			try {
				//IsDropped = false;
				//See if there is a TreeNode being dragged
				if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true)) {
					TreeNode selectedNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
					tvSourceXML.SelectedNode = selectedNode;
					//TreeNode found allow move effect
					e.Effect = DragDropEffects.Copy;
				} else {
					//No TreeNode found, prevent move
					e.Effect = DragDropEffects.None;
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}

		#endregion

		#region "Other Tree Events"
		
		private void tvTargetXML_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNode selectedNode = e.Node;
			
			if ((selectedNode != null)) {
				NodeMapping nodeM = new NodeMapping();
				nodeM = XML2XMLHelper.FindKeyNodeMapping(XML2XMLHelper.nodeMappings, selectedNode.Tag.ToString());
				
				propertyGrid1.SelectedObject = nodeM;
			}
		}
		
		private void tvSourceXML_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNode selectedNode = e.Node;
			
			if ((selectedNode != null)) {
				NodeMapping nodeM = new NodeMapping();
				nodeM = XML2XMLHelper.FindValueNodeMapping(XML2XMLHelper.nodeMappings, selectedNode.Tag.ToString());
				
				propertyGrid1.SelectedObject = nodeM;
			}
		}
		
		private void tvTargetXML_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				tvTargetXML.SelectedNode = tvTargetXML.GetNodeAt(e.Location);
		}
		
		private void tvSourceXML_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				tvSourceXML.SelectedNode = tvSourceXML.GetNodeAt(e.Location);
		}

		private void tvTargetXML_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try {
				TreeNode selectedNode = tvTargetXML.GetNodeAt(e.X, e.Y);
				if ((selectedNode != null)) {
					NodeMapping nodeM = new NodeMapping();
					nodeM = XML2XMLHelper.FindKeyNodeMapping(XML2XMLHelper.nodeMappings, selectedNode.Tag.ToString());
					
					if (nodeM != null) {

						//SelectMappedNode(tvSourceXML, e.Node.Tag.ToString)
						TreeNode node = default(TreeNode);
						node = FindNode(tvSourceXML, nodeM.Value);
						if ((node != null)) {
							tvSourceXML.SelectedNode = node;
							tvSourceXML.SelectedNode.EnsureVisible();
							//tvTargetXML.SelectedImageIndex = 1
							//tvSourceXML.SelectedImageIndex = 1
						}
					} else {
						tvSourceXML.SelectedNode = tvSourceXML.Nodes[0];
						//tvSourceXML.ExpandAll()
						//tvTargetXML.SelectedImageIndex = 0
						//tvSourceXML.SelectedImageIndex = 0
					}
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}
		#endregion

		#region "Menu Actions"
		private void mnuItemSaveXSL_Click(System.Object sender, System.EventArgs e)
		{
			try {
				SaveFileDlgXSL.Filter = "Ficheros XSL(*.XSL)|*.xsl";
				DialogResult result = SaveFileDlgXSL.ShowDialog();
				if (result != DialogResult.Cancel) {
					System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.File.Create(SaveFileDlgXSL.FileName));
					writer.Write(BuildXSL());
					writer.Flush();
					writer.Close();
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}
		
		private void mnuItemAdd_Click(System.Object sender, System.EventArgs e)
		{
			try {
				NodeMapping nodeM = XML2XMLHelper.FindKeyNodeMapping(XML2XMLHelper.nodeMappings, tvTargetXML.SelectedNode.Tag.ToString());
				
				if (nodeM == null) {
					ChangeTargetNodeIcon(tvTargetXML.SelectedNode, 1);
					NodeMapping addNode = new NodeMapping(tvTargetXML.SelectedNode.Tag.ToString(), ".");
					XML2XMLHelper.nodeMappings.Add(addNode);
					//_MappingChanged = true;
					
					propertyGrid1.SelectedObject = addNode;
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}
		
		private void mnuItemRemove_Click(System.Object sender, System.EventArgs e)
		{
			try {
				NodeMapping nodeM = XML2XMLHelper.FindKeyNodeMapping(XML2XMLHelper.nodeMappings, tvTargetXML.SelectedNode.Tag.ToString());
				
				if (nodeM != null) {
					if (!string.IsNullOrEmpty(nodeM.Value)) {
						ChangeSourceNodeIcon(FindNode(tvSourceXML, nodeM.Value), 0);
					}
					
					ChangeTargetNodeIcon(tvTargetXML.SelectedNode, 0);
					XML2XMLHelper.nodeMappings.Remove(nodeM);
					//_MappingChanged = true;
					
					propertyGrid1.SelectedObject = null;
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}
		
		private void mnuItemRemoveAllMap_Click(System.Object sender, System.EventArgs e)
		{
			try {
				if (MessageBox.Show("¿Deseas eliminar todos los mapeos ya definidos?", "Eliminar mapeos", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes) {
					ClearMappings();
					//reload source xml
					LoadTreeViewFromXmlDoc(XML2XMLHelper.SourceXML, tvSourceXML, false);
					//reload target xml
					LoadTreeViewFromXmlDoc(XML2XMLHelper.TargetXML, tvTargetXML, true);
					//_MappingChanged = true;
					
					propertyGrid1.SelectedObject = null;
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}
		
		private void mnuItmSourceXml_Click(System.Object sender, System.EventArgs e)
		{
			try {
				OpenFileDlgXML.Filter = "Ficheros XML(*.xml)|*.xml";
				DialogResult result = OpenFileDlgXML.ShowDialog();
				if (result == DialogResult.OK) {
					LoadSourceXML(OpenFileDlgXML.FileName);
					//Reset node back color
					if (XML2XMLHelper.nodeMappings.Count > 0 & !string.IsNullOrEmpty(XML2XMLHelper.TargetXMLFilePath)) {
						LoadTargetXML(XML2XMLHelper.TargetXMLFilePath);
					}
					//clear previuos mappings
					ClearMappings();
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}

		private void mnuItmTargetXml_Click(System.Object sender, System.EventArgs e)
		{
			try {
				OpenFileDlgXML.Filter = "Ficheros XML(*.xml)|*.xml";
				DialogResult result = OpenFileDlgXML.ShowDialog();
				if (result == DialogResult.OK) {
					LoadTargetXML(OpenFileDlgXML.FileName);
					//Reset node back color
					if (XML2XMLHelper.nodeMappings.Count > 0 & !string.IsNullOrEmpty(XML2XMLHelper.SourceXMLFilePath)) {
						LoadTargetXML(XML2XMLHelper.SourceXMLFilePath);
					}
					//clear previuos mappings
					ClearMappings();
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}
			
		private void mnuItmExit_Click(System.Object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void mnuItemSaveMappg_Click(System.Object sender, System.EventArgs e)
		{
			try {
				DialogResult result = default(DialogResult);
				SaveFileDlgXSL.Filter = "Ficheros de mapeo(*.mapg)|*.mapg";
				result = SaveFileDlgXSL.ShowDialog();
				if (result == DialogResult.OK) {
					SaveMappings(SaveFileDlgXSL.FileName);
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}

		private void mnuLoadMappg_Click(System.Object sender, System.EventArgs e)
		{
			try {
				DialogResult result = default(DialogResult);
				OpenFileDlgXML.Filter = "Ficheros de mapeo(*.mapg)|*.mapg";
				result = OpenFileDlgXML.ShowDialog();
				if (result == DialogResult.OK) {
					Trace("Abriendo fichero de mapeo:" + OpenFileDlgXML.FileName, 2);
					XML2XMLHelper.nodeMappings = LoadMappings(OpenFileDlgXML.FileName);
					//Change the mapped node back colors
					ChangeNodeIcon();
					//_MappingChanged = true;
					//target tree loaded with existing mappings
				}
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}

		private void mnuItemCopyPathTarget_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(tvTargetXML.SelectedNode.Tag.ToString());
		}

		private void mnuItemCopyPathSource_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(tvSourceXML.SelectedNode.Tag.ToString());
		}
		#endregion

		#region "Form methods"
		private void InitValues()
		{
			tvSourceXML.AllowDrop = true;
			tvSourceXML.HideSelection = false;
			tvTargetXML.AllowDrop = true;
			tvTargetXML.HideSelection = false;
		}

		private bool AddNewMapping(string sourcePath, string targetPath)
		{
			NodeMapping nodeM = XML2XMLHelper.FindKeyNodeMapping(XML2XMLHelper.nodeMappings, targetPath);
			if (nodeM == null) {
				XML2XMLHelper.nodeMappings.Add(new NodeMapping(targetPath, sourcePath));
				//IsDropped = true;
				//_MappingChanged = true;
				return true;
			} else {
				
				ChangeSourceNodeIcon(FindNode(tvSourceXML, nodeM.Value), 0);
				
				nodeM.Key = targetPath;
				nodeM.Value = sourcePath;
				
				return true;
			}
		}
		

		private void ClearMappings()
		{
			//clear mappings
			XML2XMLHelper.nodeMappings.Clear();
		}

		private void LoadSourceXML(string filePath)
		{
			XML2XMLHelper.SourceXMLFilePath = filePath;
			Trace("Cargando origen XML:" + XML2XMLHelper.SourceXMLFilePath, 2);
			if (File.Exists(filePath)) {
				
				XML2XMLHelper.SourceXML.Load(filePath);
				
				if (XML2XMLHelper.SourceXML != null) {
					XML2XMLHelper.SourceXMLPrefix = XML2XMLHelper.SourceXML.DocumentElement.Prefix;
					XML2XMLHelper.SourceXMLNameSpaceURI = XML2XMLHelper.SourceXML.DocumentElement.NamespaceURI;
					
					LoadTreeViewFromXmlDoc(XML2XMLHelper.SourceXML, tvSourceXML, false);
					
					rtxtSource.Text = XML2XMLHelper.Beautify(XML2XMLHelper.SourceXML);
				}
			} else {
				Trace("Fichero no encontrado: " + XML2XMLHelper.SourceXMLFilePath, 5);
			}
		}

		private void LoadTargetXML(string filePath)
		{
			XML2XMLHelper.TargetXMLFilePath = filePath;
			Trace("Cargando destino XML:" + XML2XMLHelper.TargetXMLFilePath, 2);
			if (File.Exists(filePath)) {
				
				XML2XMLHelper.TargetXML.Load(filePath);
				
				if (XML2XMLHelper.TargetXML != null) {
					XML2XMLHelper.TargetXMLPrefix = XML2XMLHelper.TargetXML.DocumentElement.Prefix;
					XML2XMLHelper.TargetXMLNameSpaceURI = XML2XMLHelper.TargetXML.DocumentElement.NamespaceURI;
				
					LoadTreeViewFromXmlDoc(XML2XMLHelper.TargetXML, tvTargetXML, true);
				
					rtxtTarget.Text = XML2XMLHelper.Beautify(XML2XMLHelper.TargetXML);
				}
			} else {
				Trace("Fichero no encontrado: " + XML2XMLHelper.TargetXMLFilePath, 5);
			}
		}

		private void ChangeSourceNodeIcon(TreeNode sourceNode, int colorType)
		{
			//default
			if (colorType == 0) {
				//sourceNode.BackColor = Color.Empty
				sourceNode.ImageIndex = 0;
				sourceNode.SelectedImageIndex = 0;
			} else {
				//sourceNode.BackColor = System.Drawing.Color.DarkOrange
				sourceNode.ImageIndex = 1;
				sourceNode.SelectedImageIndex = 1;
			}

		}

		private void ChangeTargetNodeIcon(TreeNode targetNode, int colorType)
		{
			//default
			if (colorType == 0) {
				//targetNode.BackColor = System.Drawing.Color.Empty
				targetNode.ImageIndex = 0;
				targetNode.SelectedImageIndex = 0;
			} else {
				//targetNode.BackColor = System.Drawing.Color.Gold
				targetNode.ImageIndex = 1;
				targetNode.SelectedImageIndex = 1;
			}
		}

		private void ChangeNodeIcon()
		{
			string nodepath = null;
			TreeNode treNode = default(TreeNode);
			
			foreach (NodeMapping nm in XML2XMLHelper.nodeMappings) {
				nodepath = nm.Value;
				treNode = FindNode(tvSourceXML, nodepath);
				if ((treNode != null)) {
					ChangeSourceNodeIcon(treNode, 1);
				}
			}
			foreach (NodeMapping nm in XML2XMLHelper.nodeMappings) {
				nodepath = nm.Key;
				treNode = FindNode(tvTargetXML, nodepath);
				if ((treNode != null)) {
					ChangeTargetNodeIcon(treNode, 1);
				}
			}
		}

		private void Trace(string msg, int seviority)
		{
			if (seviority == 0) {
				rtxtTrace.SelectionColor = Color.Black;
				//Sucess Message
			} else if (seviority == 2) {
				rtxtTrace.SelectionColor = Color.Green;
			} else if (seviority == 3) {
				rtxtTrace.SelectionColor = Color.Maroon;
			} else if (seviority == 4) {
				rtxtTrace.SelectionColor = Color.PaleVioletRed;
			} else {
				rtxtTrace.SelectionColor = Color.Red;
			}
			rtxtTrace.AppendText("> " + msg + Environment.NewLine);
		}
		#endregion

		#region "Tree access methods"
		//		private TreeNode CheckMappedNode(TreeNode activeNode, string nodeText)
		//		{
		//			foreach (TreeNode _treeNode in activeNode.Nodes) {
		//				if (nodeText == _treeNode.Tag.ToString()) {
		//					return _treeNode;
		//				}
		//			}
		//			return null;
		//		}
		//		private TreeNode CheckMappedNode(TreeView tv, string nodeText)
		//		{
		//			foreach (TreeNode _treeNode in tv.Nodes) {
		//				if (nodeText == _treeNode.Tag.ToString()) {
		//					return _treeNode;
		//				}
		//			}
		//			return null;
		//		}
		//		private TreeNode GetMappedNode(TreeView tv, string nodeFullPath)
		//		{
		//			TreeNode tempNode = default(TreeNode);
		//			foreach (TreeNode node in tv.Nodes) {
		//				tempNode = GetMappedNode(node, nodeFullPath);
		//				if ((tempNode != null)) {
		//					break; // TODO: might not be correct. Was : Exit For
		//				}
		//			}
		//			return tempNode;
		//		}
		//		private TreeNode GetMappedNode(TreeNode activeNode, string nodeFullPath)
		//		{
		//			//string firstNodeText = null;
		//			//int firstSlash = 0;
		//			TreeNode currentNode = default(TreeNode);
		//			//firstSlash = nodeFullPath.IndexOf("/");
		//			//if (firstSlash != -1) {
		//			//firstNodeText = nodeFullPath.Substring(0, firstSlash);
		//			//nodeFullPath = nodeFullPath.Substring(firstSlash + 1);
		//			if (activeNode.Tag.ToString() == nodeFullPath) {
		//				currentNode = activeNode;
		//			} else {
		//				currentNode = CheckMappedNode(activeNode, nodeFullPath);
		//
		//			}
		//			if ((currentNode != null) && nodeFullPath.Length > 0) {
		//				currentNode = GetMappedNode(currentNode, nodeFullPath);
		//			}
		//			//} else if (nodeFullPath.Trim().Length > 0) {
		//			//	firstNodeText = nodeFullPath;
		//			//	currentNode = CheckMappedNode(activeNode, firstNodeText);
		//			//	if ((currentNode != null)) {
		//			//		return currentNode;
		//			//	}
		//			//}
		//			return currentNode;
		//		}
		
		private TreeNode FindNode(TreeView tv, string matchText)
		{
			return FindNode(tv.Nodes, matchText);
		}
		
		
		private TreeNode FindNode(TreeNodeCollection nodes, string matchText)
		{
			foreach (TreeNode node in nodes) {
				if (node.Tag.ToString() == matchText) {
					return node;
				} else {
					TreeNode nodeChild = FindNode(node.Nodes, matchText);
					if (nodeChild != null)
						return nodeChild;
				}
			}
			return null;
		}

		private string BuildXSL()
		{
			if (String.IsNullOrEmpty(XML2XMLHelper.TargetXMLFilePath)) {
				Trace("No esta definido el fichero XML de destino.", 5);
				return "";
			}

			XmlDocument xslDoc = new XmlDocument();
			XmlNode rootNode = default(XmlNode);
			XmlNode tempNode = default(XmlNode);
			XmlAttribute tempAttribute = default(XmlAttribute);

			//Setting up NSManager
			XmlNamespaceManager nmManger = new XmlNamespaceManager(xslDoc.NameTable);
			nmManger.AddNamespace("xsl", XML2XMLHelper.xslNameSpaceURI);
			if (!string.IsNullOrEmpty(XML2XMLHelper.TargetXMLPrefix)) {
				//add target xml namepace to the current
				nmManger.AddNamespace(XML2XMLHelper.TargetXMLPrefix, XML2XMLHelper.TargetXMLNameSpaceURI);
			}
			if (!string.IsNullOrEmpty(XML2XMLHelper.SourceXMLPrefix)) {
				//add target xml namepace to the current
				nmManger.AddNamespace(XML2XMLHelper.SourceXMLPrefix, XML2XMLHelper.SourceXMLNameSpaceURI);
			}

			//<xsl:stylesheet
			tempNode = xslDoc.CreateElement(XML2XMLHelper.xslPrefix, "stylesheet", XML2XMLHelper.xslNameSpaceURI);
			tempAttribute = xslDoc.CreateAttribute("version");
			tempAttribute.InnerText = "1.0";
			tempNode.Attributes.Append(tempAttribute);
			
			if (!string.IsNullOrEmpty(XML2XMLHelper.SourceXMLPrefix)) {
				tempAttribute = xslDoc.CreateAttribute("xmlns:" + XML2XMLHelper.SourceXMLPrefix);
				tempAttribute.InnerText = XML2XMLHelper.SourceXMLNameSpaceURI;
				tempNode.Attributes.Append(tempAttribute);
			}

			if (!string.IsNullOrEmpty(XML2XMLHelper.TargetXMLPrefix)) {
				tempAttribute = xslDoc.CreateAttribute("xmlns:" + XML2XMLHelper.TargetXMLPrefix);
				tempAttribute.InnerText = XML2XMLHelper.TargetXMLNameSpaceURI;
				tempNode.Attributes.Append(tempAttribute);
			}
			
			if (!string.IsNullOrEmpty(XML2XMLHelper.xslPrefix)) {
				tempAttribute = xslDoc.CreateAttribute("xmlns:" + XML2XMLHelper.xslPrefix);
				tempAttribute.InnerText = XML2XMLHelper.xslNameSpaceURI;
				tempNode.Attributes.Append(tempAttribute);
			}

			rootNode = xslDoc.AppendChild(tempNode);
			//<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
			tempNode = xslDoc.CreateElement(XML2XMLHelper.xslPrefix, "output", XML2XMLHelper.xslNameSpaceURI);
			tempAttribute = xslDoc.CreateAttribute("method");
			tempAttribute.InnerText = "xml";
			tempNode.Attributes.Append(tempAttribute);
			tempAttribute = xslDoc.CreateAttribute("encoding");
			tempAttribute.InnerText = "UTF-8";
			tempNode.Attributes.Append(tempAttribute);
			tempAttribute = xslDoc.CreateAttribute("indent");
			tempAttribute.InnerText = "yes";
			tempNode.Attributes.Append(tempAttribute);
			rootNode.AppendChild(tempNode);

			//target XML
			XmlDocument targetXmlDoc = new XmlDocument();
			targetXmlDoc.Load(XML2XMLHelper.TargetXMLFilePath);
			
			XML2XMLHelper.BlankNodes(targetXmlDoc.DocumentElement);

			//<xsl:template match="/mstns:Root">
			tempNode = xslDoc.CreateElement(XML2XMLHelper.xslPrefix, "template", XML2XMLHelper.xslNameSpaceURI);
			tempAttribute = xslDoc.CreateAttribute("match");
			tempAttribute.InnerText = "/";
			tempNode.Attributes.Append(tempAttribute);

			tempNode.InnerXml = targetXmlDoc.DocumentElement.OuterXml;

			rootNode.AppendChild(tempNode);

			string mappedKey = null;
			XmlNode targetNode = default(XmlNode);
			foreach (NodeMapping nm in XML2XMLHelper.nodeMappings) {
				mappedKey = "/" + nm.Key;
				
				targetNode = rootNode.SelectSingleNode(mappedKey, nmManger);

				if ((targetNode != null)) {
					
					if (!String.IsNullOrEmpty(nm.Formula)) {

						targetNode.InnerXml = ProcessFormula.Start(nm);
						
					} else {
						if (!nm.Loop) {
							//Uncomment the following lines if you want node to node mapping
							tempNode = MapAsSingleNode(xslDoc, nm.Value);
							targetNode.AppendChild(tempNode);
						} else {
							//Comment the following lines if you don't want for loop mapping
							tempNode = MapAsLoopNode(xslDoc, targetNode.CloneNode(true), nm.Value);
							targetNode.ParentNode.ReplaceChild(tempNode, targetNode);
						}
					}
				}
			}

			return XML2XMLHelper.Beautify(xslDoc);
		}
		
		private XmlNode MapAsSingleNode(XmlDocument xslDoc, string mappedValue)
		{
			XmlNode tempNode = default(XmlNode);
			XmlAttribute tempAttribute = default(XmlAttribute);
			//<xsl:value-of select=""" + mappedValue + """/>
			tempNode = xslDoc.CreateElement(XML2XMLHelper.xslPrefix, "value-of", XML2XMLHelper.xslNameSpaceURI);
			tempAttribute = xslDoc.CreateAttribute("select");
			tempAttribute.InnerText = mappedValue;
			tempNode.Attributes.Append(tempAttribute);
			return tempNode;
		}
		

		private XmlNode MapAsLoopNode(XmlDocument xslDoc, XmlNode selectedNode, string mappedValue)
		{
			XmlNode forLoopNode = default(XmlNode);
			XmlNode tempNode = default(XmlNode);
			XmlAttribute tempAttribute = default(XmlAttribute);
			//<xsl:for-each select=""" + mappedValue + """/>
			forLoopNode = xslDoc.CreateElement(XML2XMLHelper.xslPrefix, "for-each", XML2XMLHelper.xslNameSpaceURI);
			tempAttribute = xslDoc.CreateAttribute("select");
			tempAttribute.InnerText = mappedValue;
			forLoopNode.Attributes.Append(tempAttribute);
			//<xsl:value-of select="."/>
			tempNode = xslDoc.CreateElement(XML2XMLHelper.xslPrefix, "value-of", XML2XMLHelper.xslNameSpaceURI);
			tempAttribute = xslDoc.CreateAttribute("select");
			tempAttribute.InnerText = ".";
			tempNode.Attributes.Append(tempAttribute);
			selectedNode.AppendChild(tempNode);
			forLoopNode.AppendChild(selectedNode);
			return forLoopNode;
		}
		#endregion

		#region "Serialization Methods"
		private void SaveMappings(string fileName)
		{
			try {
				Collection<NodeMapping> mappingSettings = new Collection<NodeMapping>();
				XmlSerializer serializer = new XmlSerializer(mappingSettings.GetType());
				mappingSettings = XML2XMLHelper.nodeMappings;
				mappingSettings.Add(new NodeMapping("SourceXMLFile", XML2XMLHelper.SourceXMLFilePath));
				mappingSettings.Add(new NodeMapping("TargetXMLFile", XML2XMLHelper.TargetXMLFilePath));
				System.IO.Stream writer = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite);
				serializer.Serialize(writer, mappingSettings);
				writer.Flush();
				writer.Close();
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}

		private Collection<NodeMapping> LoadMappings(string fileName)
		{
			try {
				Collection<NodeMapping> mapNodeColl = new Collection<NodeMapping>();
				XmlSerializer serializer = new XmlSerializer(mapNodeColl.GetType());
				FileStream inputFile = System.IO.File.Open(fileName, FileMode.Open);
				mapNodeColl = (Collection<NodeMapping>)serializer.Deserialize(inputFile);
				
				NodeMapping nmSource = XML2XMLHelper.FindKeyNodeMapping(mapNodeColl, "SourceXMLFile");
				NodeMapping nmTarget = XML2XMLHelper.FindKeyNodeMapping(mapNodeColl, "TargetXMLFile");
				
				LoadSourceXML(nmSource.Value);
				LoadTargetXML(nmTarget.Value);
				mapNodeColl.Remove(nmSource);
				mapNodeColl.Remove(nmTarget);
				inputFile.Close();
				return mapNodeColl;
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
				return null;
			}
		}
		#endregion

		#region "Form events"
		private void XSLMapper_Load(System.Object sender, System.EventArgs e)
		{
			try {
				InitValues();
			} catch (Exception ex) {
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}

		private void tbcXSLEditor_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			try {
				if (tbcXSLEditor.SelectedIndex == 1) {
					//generate xsl
					rtxtXSL.Clear();
					rtxtXSL.Text = BuildXSL();
					//Reset mappings status
					//_MappingChanged = false;
				} else if (tbcXSLEditor.SelectedIndex == 2) {
					XmlDocument xmlDoc = new XmlDocument();
					XmlDocument xslDoc = new XmlDocument();
					if (!string.IsNullOrEmpty(XML2XMLHelper.SourceXMLFilePath)) {
						if (rtxtXSL.Text != "") {
							xmlDoc.Load(XML2XMLHelper.SourceXMLFilePath);
							//generate xsl
							//txtXSL.Text = BuildXSL()
							xslDoc.InnerXml = rtxtXSL.Text;
							XML2XMLTransformer xmlGen = new XML2XMLTransformer(xslDoc);
							rtxtOutput.Text = xmlGen.Transform(xmlDoc);
						}
					}
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
				Trace(ex.Message, 5);
				Trace(ex.StackTrace, 5);
			}
		}
		#endregion
	}
}