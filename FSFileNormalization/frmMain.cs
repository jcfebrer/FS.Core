using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FSFormControls;
using FSLibrary;
using FSFile;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace FileNormalization
{
    public partial class frmMain : Form
    {
        private FSFormControls.DBListViewColumnSorter lvwColumnSorter;
        Files files = new Files();
        string lastDirectory = "";
        string actualDirectory = "";
        CancellationTokenSource tokenSource = null;


        public frmMain()
        {
            InitializeComponent();

            InitializeGrid();
        }

        void InitializeGrid()
        {
            //lstSource.ShowItemToolTips = true;
            lstSource.View = View.Details;
            lstSource.GridLines = true;
            lstSource.FullRowSelect = true;

            lstSource.Columns.Add("Path", 200);
            lstSource.Columns.Add("Nombre original", 400);
            lstSource.Columns.Add("Nombre normalizado", 400);
            lstSource.Columns.Add("Label", 200);
            lstSource.Columns.Add("Tamaño", 200);
            lstSource.Columns.Add("Fecha", 200);
            lstSource.Columns.Add("Crc32", 100);
            lstSource.Columns.Add("Veces", 100);

            FileUtils.FileAdded += FileUtils_FileAdded;
        }

        private void OpenFolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (!String.IsNullOrEmpty(fbd.SelectedPath))
            {
                LoadFolders(fbd.SelectedPath);
                LoadFiles(fbd.SelectedPath);
            }
        }

        private void LoadFolders(string path)
        {
            if (tokenSource != null)
                tokenSource.Cancel();

            lastDirectory = actualDirectory;
            actualDirectory = path;

            dbFileExplorer1.LoadFolders(path);
        }

        private void LoadFiles(string path)
        {
            if (tokenSource != null)
                tokenSource.Cancel();

            //Para mejorar el rendimiento del ListView. Primero inicializamos a null...
            lstSource.ListViewItemSorter = null;

            lstSource.Items.Clear();
            lstSource.BeginUpdate();

            this.toolStripStatusLabel1.Text = "Cargando ficheros ...";
            this.toolStripProgressBar1.Maximum = FileUtils.TotalFiles(path, "*.*", false);
            this.toolStripProgressBar1.Minimum = 0;
            this.toolStripProgressBar1.Value = 0;

            files = FileUtils.GetFiles(path, "*.*", false, chkCalcCRC32.Checked, false); //"*.avi;*.mpg;*.wma;*.mkv;*.srt");

            //SetupGridAsync(files);

            //CalcCrc32Async();
            //SoundExAsync();

            //Y después de la carga del llistview, asignamos el ListViewColumnSorter.
            lvwColumnSorter = new FSFormControls.DBListViewColumnSorter();
            lstSource.ListViewItemSorter = lvwColumnSorter;

            lstSource.EndUpdate();

            this.toolStripStatusTotal.Text = "Total registros: " + files.Count;

            this.toolStripStatusLabel1.Text = "";
            this.toolStripProgressBar1.Value = 0;
        }

        private void FileUtils_FileAdded(object sender, EventArgs e)
        {
            AddFile((File)sender);
            this.toolStripProgressBar1.Value += 1;
        }


        //public async void CalcCrc32Async()
        //{
        //    await Task.Run(() =>
        //    {
        //        lstSource.Invoke(new MethodInvoker(() =>
        //        {
        //            CalcCrc32();
        //        }));
        //    });
        //}


        public async void CalcCrc32()
        {
            tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            this.toolStripStatusLabel1.Text = "Calculando CRC32 ...";
            this.toolStripProgressBar1.Maximum = lstSource.Items.Count;
            this.toolStripProgressBar1.Minimum = 0;
            this.toolStripProgressBar1.Value = 0;

            int f = 0;
            foreach (ListViewItem item in lstSource.Items)
            {
                if (!token.IsCancellationRequested)
                {
                    try
                    {
                        this.toolStripProgressBar1.Value = f++;

                        File file = (File)item.Tag;
                        string crc32 = await FileUtils.CalcCrc32Async(file.FullName, token);
                        item.SubItems[6].Text = crc32;
                        file.Crc32 = crc32;
                        int veces = await FindByCRC32Async(crc32, token);
                        item.SubItems[7].Text = veces.ToString();
                        file.Veces = veces;
                    }
                    catch(TaskCanceledException)
                    {
                        return;
                    }
                }
            }

            this.toolStripProgressBar1.Value = 0;
            this.toolStripStatusLabel1.Text = "";
        }

        private async Task<int> FindByCRC32Async(string crc32, CancellationToken token)
        {
            int times = 0;
            await Task.Run(() =>
            {
                    foreach (File file in files)
                    {
                        if (file.Crc32!= null && file.Crc32.Equals(crc32))
                            times++;
                    }
            }, token);

            return times;
        }

        public async void SoundExAsync()
        {
            await Task.Run(() => {
                foreach (File fichero in files)
                    fichero.SoundEx = FSFuzzyStrings.SoundExEsp.Do(fichero.Nombre);
            
                });
        }

        public async void SetupGridAsync(Files ficheros)
        {
            await Task.Run(() => {
                lstSource.Invoke(new MethodInvoker(() =>
                {
                SetupGrid(ficheros);
                }));
            });
        }


        /// <summary>
        /// Establecemos las peliculas en el ListView
        /// </summary>
        private void SetupGrid(Files files)
        {
            //Para mejorar el rendimiento del ListView. Primero inicializamos a null...
            lstSource.ListViewItemSorter = null;

            lstSource.Items.Clear();
            lstSource.BeginUpdate();

            this.toolStripStatusLabel1.Text = "Actualizando lista de ficheros ...";
            this.toolStripProgressBar1.Maximum = files.Count;
            this.toolStripProgressBar1.Minimum = 0;

            int f = 0;
            foreach (File file in files)
            {

                AddFile(file);

                toolStripProgressBar1.Value = f;
                f++;
            }

            //Y después de la carga del llistview, asignamos el ListViewColumnSorter.
            lvwColumnSorter = new FSFormControls.DBListViewColumnSorter();
            lstSource.ListViewItemSorter = lvwColumnSorter;

            lstSource.EndUpdate();

            this.toolStripStatusTotal.Text = "Total registros: " + files.Count;

            this.toolStripProgressBar1.Value = 0;
            this.toolStripStatusLabel1.Text = "";
        }

        private void AddFile(File file)
        {
            string[] arr = new string[8];
            ListViewItem itm;

            arr[0] = file.Dir;
            arr[1] = file.Nombre;
            arr[2] = file.NombreNormalizado;
            arr[3] = file.Label;
            arr[4] = (file.Tamaño / 1000).ToString("000000.00");
            arr[5] = FSLibrary.DateTimeUtil.ShortDate(file.FechaArchivo);
            arr[6] = file.Crc32;
            arr[7] = "0";


            ListViewGroup lvgDest = null;
            foreach (ListViewGroup lvg in lstSource.Groups)
            {
                if (lvg.Name == file.Crc32)
                {
                    lvgDest = lvg;
                    break;
                }
            }
            if (lvgDest == null)
            {
                lvgDest = new ListViewGroup(file.Crc32, file.Crc32);
                lstSource.Groups.Add(lvgDest);
            }

            itm = new ListViewItem(arr, lvgDest);
            itm.BackColor = file.ColorFondo;
            itm.Tag = file;

            lstSource.Items.Add(itm);
        }

        private void cmdGuardar_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            saveFileDialog1.FileName = "export.csv";
            saveFileDialog1.Filter = "Ficheros CSV (*.csv)|*.csv|Todos los ficheros (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileUtils.SaveAsCsv(saveFileDialog1.FileName, files);

                MessageBox.Show("Guardado de catálogo finalizado", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstSource_DoubleClick(object sender, EventArgs e)
        {
            string nombre = lstSource.SelectedItems[0].SubItems[1].Text;

            File pel = files.Find(nombre);

            frmFileDetail frmFil = new frmFileDetail(pel);
            frmFil.ShowDialog();

            lstSource.SelectedItems[0].SubItems[1].Text = pel.Nombre;
            lstSource.SelectedItems[0].SubItems[2].Text = pel.NombreNormalizado;
            lstSource.SelectedItems[0].SubItems[3].Text = pel.Label;
            lstSource.SelectedItems[0].BackColor = pel.ColorFondo;

            //frmInputBox inputBox = new frmInputBox();


            //inputBox.Caption = "Nuevo nombre para el fichero:";
            //inputBox.InputText = fichero1;
            //if(inputBox.ShowDialog()== DialogResult.OK)
            //{
            //    lstSource.SelectedItems[0].SubItems[2].Text = inputBox.InputText;
            //}

            //inputBox.Dispose();
        }

        private void cmdRenombrar_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void Rename()
        {
            if (MessageBox.Show("¿Estás seguro de querer renombrar los ficheros seleccionados?", "Renombrar", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK) return;

            foreach (ListViewItem lvi in lstSource.Items)
            {
                try
                {
                    FileUtils.Rename(lvi.Text + "\\" + lvi.SubItems[1].Text, lvi.Text + "\\" + lvi.SubItems[2].Text);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.Write(ex.Message);
                }
            }

            MessageBox.Show("Renombrado finalizado", "Renombrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void RemoveComments()
        {
            if (MessageBox.Show("¿Estás seguro de querer eliminar comentarios en los ficheros seleccionados?", "Eliminar comentarios", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK) return;

            foreach (ListViewItem lvi in lstSource.Items)
            {
                try
                {
                    //RemoveComments rc = new RemoveComments(true);
                    //rc.RemoveFileComments(lvi.Text + "\\" + lvi.SubItems[1].Text);

                    string code = System.IO.File.ReadAllText(lvi.Text + "\\" + lvi.SubItems[1].Text);
                    string result = TextUtil.RemoveComments(code);

                    //MessageBox.Show("Líneas eliminadas: " + rc.LinesRemoved, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show("Partes eliminadas: " + rc.PartsRemoved, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.Write(ex.Message);
                }
            }

            MessageBox.Show("Ficheros procesados: " + lstSource.Items.Count, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        void CmdLimpiarClick(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            lstSource.Items.Clear();
            MessageBox.Show("Limpieza realizada", "Limpiar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void CmdCargarClick(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "export.csv";
            openFileDialog1.Filter = "Ficheros CSV (*.csv)|*.csv|Todos los ficheros (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable table = FSDatabase.CsvReader.ReadCSVFile(openFileDialog1.FileName, true);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow dr = table.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr[0].ToString());
                    listitem.SubItems.Add(dr[1].ToString());
                    listitem.SubItems.Add(dr[2].ToString());
                    listitem.SubItems.Add(dr[3].ToString());
                    listitem.SubItems.Add(dr[4].ToString());
                    listitem.SubItems.Add(dr[5].ToString());
                    listitem.SubItems.Add(dr[6].ToString());
                    listitem.SubItems.Add(dr[7].ToString());
                    lstSource.Items.Add(listitem);
                }

                this.toolStripStatusTotal.Text = "Total registros: " + lstSource.Items.Count;

                MessageBox.Show("Carga de catálogo realizado", "Carga", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void BtnBuscarClick(object sender, EventArgs e)
        {
            int totalFind = 0;
            if (txtBuscar.Text != "")
            {
                for (int i = lstSource.Items.Count - 1; i >= 0; i--)
                {
                    ListViewItem item = lstSource.Items[i];
                    if (item.SubItems[0].Text.ToLower().Contains(txtBuscar.Text.ToLower()) ||
                       item.SubItems[1].Text.ToLower().Contains(txtBuscar.Text.ToLower()) ||
                      item.SubItems[2].Text.ToLower().Contains(txtBuscar.Text.ToLower()) ||
                     item.SubItems[3].Text.ToLower().Contains(txtBuscar.Text.ToLower()))
                    {
                        item.BackColor = SystemColors.Highlight;
                        item.ForeColor = SystemColors.HighlightText;

                        totalFind++;
                    }
                    else
                    {
                        item.BackColor = SystemColors.Window;
                        item.ForeColor = SystemColors.WindowText;
                    }
                }

                this.toolStripStatusTotal.Text = "Encontrados: " + totalFind;
            }
        }
        void lst_Enter(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstSource.Items)
            {
                item.BackColor = SystemColors.Window;
                item.ForeColor = SystemColors.WindowText;
            }
        }
        void LstSourceColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lvwColumnSorter != null)
            {
                if (e.Column == lvwColumnSorter.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (lvwColumnSorter.Order == SortOrder.Ascending)
                    {
                        lvwColumnSorter.Order = SortOrder.Descending;
                    }
                    else
                    {
                        lvwColumnSorter.Order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    lvwColumnSorter.SortColumn = e.Column;
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }

                // Perform the sort with these new sort options.
                lstSource.Sort();
            }
        }

        private void cmdNormalizar_Click(object sender, EventArgs e)
        {
            Normalize();
        }

        private void Normalize()
        {
            SetupGrid(files);
            this.toolStripStatusTotal.Text = "Total registros: " + files.Count;
        }

        private void cmbExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            Files ficherosExt = files.FindExtension(cmbExtension.Text);
            SetupGrid(ficherosExt);
            this.toolStripStatusTotal.Text = "Total registros: " + ficherosExt.Count;
        }

        private void añadirDirectorioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFolder();
        }

        private void limpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void normalizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Normalize();
        }

        private void buscarDuplicadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCompare frmcompare = new frmCompare();

            frmcompare.main = this;
            frmcompare.ficheros = this.files;

            frmcompare.Show();
        }

        private void ejecutarConToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FSLibrary.ProcessUtil.ShowOpenWithDialog(lstSource.SelectedItems[0].SubItems[0].Text + "\\" + lstSource.SelectedItems[0].SubItems[1].Text);
        }


        private void cmdBack_Click(object sender, EventArgs e)
        {
            if (lastDirectory != "")
            {
                LoadFolders(lastDirectory);
                LoadFiles(lastDirectory);
            }
        }

        private void cmdCalcCRC32_Click(object sender, EventArgs e)
        {
            CalcCrc32();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadFiles(dbFileExplorer1.SelectedDrive.RootDirectory.FullName);

            dbFileExplorer1.NodeMouseClick += DbFileExplorer1_NodeMouseClick;
        }

        private void DbFileExplorer1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            LoadFiles(((DBTreeViewNode)dbFileExplorer1.SelectedDBNode).Value.ToString());
        }

        private void cmdRemoveComments_Click(object sender, EventArgs e)
        {
            RemoveComments();
        }
    }
}
