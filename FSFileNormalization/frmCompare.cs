using FSFile;
using FSFormControls;
using FSLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNormalization
{
    public partial class frmCompare : Form
    {
        public frmMain main;

        public Files ficheros;
        public frmCompare()
        {
            InitializeComponent();
        }

        private void frmCompare_Load(object sender, EventArgs e)
        {
            FillSourceListBox();

            cmbFuzzyValue.Items.Add("0.50");
            cmbFuzzyValue.Items.Add("0.60");
            cmbFuzzyValue.Items.Add("0.70");
            cmbFuzzyValue.Items.Add("0.75");
            cmbFuzzyValue.Items.Add("0.80");
            cmbFuzzyValue.Items.Add("0.85");
            cmbFuzzyValue.Items.Add("0.90");
            cmbFuzzyValue.Items.Add("0.95");
            cmbFuzzyValue.SelectedIndex = 3;

            cmbLevenshteinValue.Items.Add("2");
            cmbLevenshteinValue.Items.Add("3");
            cmbLevenshteinValue.Items.Add("4");
            cmbLevenshteinValue.Items.Add("5");
            cmbLevenshteinValue.Items.Add("6");
            cmbLevenshteinValue.Items.Add("7");
            cmbLevenshteinValue.Items.Add("8");
            cmbLevenshteinValue.SelectedIndex = 2;

            cmbSoundExValue.Items.Add("3");
            cmbSoundExValue.Items.Add("4");
            cmbSoundExValue.Items.Add("5");
            cmbSoundExValue.Items.Add("6");
            cmbSoundExValue.Items.Add("7");
            cmbSoundExValue.Items.Add("8");
            cmbSoundExValue.SelectedIndex = 1;
        }

        private void FillSourceListBox()
        {
            lstFileNames.View = View.Details;
            lstFileNames.GridLines = true;
            lstFileNames.FullRowSelect = true;

            DBListViewColumnSorter lvwColumnSorter = new DBListViewColumnSorter();
            lstFileNames.ListViewItemSorter = lvwColumnSorter;

            lstFileNames.Columns.Add("Nombre del fichero", 500);

            lstFileNames.Items.Clear();

            foreach (File fichero in ficheros)
            {

                string[] arr = new string[1];
                ListViewItem itm;

                arr[0] = fichero.Nombre;

                itm = new ListViewItem(arr);
                lstFileNames.Items.Add(itm);
            }
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstFileNames_Click(object sender, EventArgs e)
        {
            if (this.lstDuplicates.SelectedItems.Count == 0)
                return;

            SearchDuplicates(lstFileNames.SelectedItems[0]);
        }

        private void SearchDuplicates(ListViewItem lvi)
        {
            Files ficherosSoundEx = null;

            if (optFuzzy.Checked)
                ficherosSoundEx = ficheros.FindFuzzy(lvi.Text, Convert.ToDouble(cmbFuzzyValue.SelectedItem));
            if (optSoundEx.Checked)
                ficherosSoundEx = ficheros.FindSoundEx(lvi.Text, Convert.ToInt32(cmbSoundExValue.SelectedItem));
            if (optLevenshtein.Checked)
                ficherosSoundEx = ficheros.FindLevenshtein(lvi.Text, Convert.ToInt32(cmbLevenshteinValue.SelectedItem));

            if (ficherosSoundEx == null)
                return;

            lstDuplicates.View = View.Details;
            lstDuplicates.GridLines = true;
            lstDuplicates.FullRowSelect = true;

            DBListViewColumnSorter lvwColumnSorter = new DBListViewColumnSorter();
            lstDuplicates.ListViewItemSorter = lvwColumnSorter;

            lstDuplicates.Columns.Add("Nombre del fichero", 500);

            lstDuplicates.Items.Clear();

            foreach (File fichero in ficherosSoundEx)
            {

                string[] arr = new string[1];
                ListViewItem itm;

                arr[0] = fichero.Nombre;

                itm = new ListViewItem(arr);

                lstDuplicates.Items.Add(itm);
            }
        }

        private void lstFileNames_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SearchDuplicates(e.Item);
        }

        private void renombrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void optSimilar_CheckedChanged(object sender, EventArgs e)
        {
            FillSimilar();
        }

        private void FillSimilar()
        {
            lstFileNames.Items.Clear();

            foreach (File fichero in ficheros)
            {

                string[] arr = new string[1];
                ListViewItem itm;

                arr[0] = fichero.Nombre;

                if (FSLibrary.TextUtil.Like(fichero.Nombre,"%" + FSLibrary.TextUtil.Replace(fichero.Nombre," ","%") + "%"))
                {
                    itm = new ListViewItem(arr);

                    lstFileNames.Items.Add(itm);
                }
            }
        }

        private void FillSoundEx()
        {
            lstFileNames.Items.Clear();

            foreach (File fichero in ficheros)
            {

                string[] arr = new string[1];
                ListViewItem itm;

                arr[0] = fichero.Nombre;

                if (ficheros.FindSoundEx(fichero.Nombre, Convert.ToInt32(cmbSoundExValue.SelectedItem)).Count > 1)
                {
                    itm = new ListViewItem(arr);

                    lstFileNames.Items.Add(itm);
                }
            }
        }

        private void optSoundEx_CheckedChanged(object sender, EventArgs e)
        {
            FillSoundEx();
        }

        private void optFuzzy_CheckedChanged(object sender, EventArgs e)
        {
            FillFuzzy();
        }

        private void FillFuzzy()
        {
            lstFileNames.Items.Clear();

            foreach (File fichero in ficheros)
            {

                string[] arr = new string[1];
                ListViewItem itm;

                arr[0] = fichero.Nombre;

                if (ficheros.FindFuzzy(fichero.Nombre, Convert.ToDouble(cmbFuzzyValue.SelectedItem)).Count > 1)
                {
                    itm = new ListViewItem(arr);

                    lstFileNames.Items.Add(itm);
                }
            }
        }

        private void optLevenshtein_CheckedChanged(object sender, EventArgs e)
        {
            FillLevenshtein();
        }

        private void FillLevenshtein()
        {
            lstFileNames.Items.Clear();

            foreach (File fichero in ficheros)
            {

                string[] arr = new string[1];
                ListViewItem itm;

                arr[0] = fichero.Nombre;

                if (ficheros.FindLevenshtein(fichero.Nombre, Convert.ToInt32(cmbLevenshteinValue.SelectedItem)).Count > 1)
                {
                    itm = new ListViewItem(arr);

                    lstFileNames.Items.Add(itm);
                }
            }
        }
    }
}
