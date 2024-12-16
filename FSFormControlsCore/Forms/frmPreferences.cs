using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FSLibraryCore;

namespace FSFormControlsCore {

    public partial class frmPreferences : Form {

        string nodeToEdit = "";

        public frmPreferences() {
            InitializeComponent();

            LoadTree();
        }

        private void LoadTree() {
            try {
                tvNodes.Nodes.Clear();

                TreeNode root = new TreeNode("Configuración");
                tvNodes.Nodes.Add(root);

                List<KeyValuePair<string, string>> list = Config.ReadProperties();

                foreach (KeyValuePair<string, string> keys in list) {
                    TreeNode eltNode = new TreeNode(keys.Key);
                    root.Nodes.Add(eltNode);
                }
                tvNodes.ExpandAll();
            } catch { }
        }

        private void tvNodes_AfterSelect(object sender, TreeViewEventArgs e) {

            //Si es el primer nodo, no lo mostramos
            if (e.Node.Level == 0)
            {
                nodeToEdit = "";
                txtValue.Text = "";
                lblValue.Text = "";
                btnUpdate.Enabled = false;
                return;
            }

            btnUpdate.Enabled = true;

            nodeToEdit = e.Node.Text;

            lblValue.Text = nodeToEdit + " valor:";
            lblValue.ForeColor = Color.Blue;
            txtValue.Text = Config.ValueProperty(nodeToEdit);
        }

        private void btnUpdate_Click(object sender, EventArgs e) {

            if(nodeToEdit != "")
                Config.SetProperty(nodeToEdit, txtValue.Text);
        }

        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}