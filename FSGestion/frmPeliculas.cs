
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
	public class frmPeliculas : System.Windows.Forms.Form
	{
		
		FSFormControls.DBHtmlDocument mDocument;
		
		public class pelicula
		{
			string m_titulo;
			string m_codigo;
			
			public pelicula()
			{
			}
			public string titulo
			{
				get
				{
					return m_titulo;
				}
				set
				{
					m_titulo = value;
				}
			}
			public string codigo
			{
				get
				{
					return m_codigo;
				}
				set
				{
					m_codigo = value;
				}
			}
		}
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmPeliculas()
		{
			
			//El Diseñador de Windows Forms requiere esta llamada.
			InitializeComponent();
			
			//Agregar cualquier inicialización después de la llamada a InitializeComponent()
			
		}
		
		//Form reemplaza a Dispose para limpiar la lista de componentes.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!(components == null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		//Requerido por el Diseñador de Windows Forms
		private System.ComponentModel.Container components = null;
		
		//NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
		//Puede modificarse utilizando el Diseñador de Windows Forms.
		//No lo modifique con el editor de código.
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.Button cmdBuscar;
		internal System.Windows.Forms.TextBox txtIMDBTitulo;
		internal System.Windows.Forms.TextBox txtIMDBNombre;
		internal System.Windows.Forms.TextBox txtResultados;
		internal System.Windows.Forms.TextBox txtTitulo;
		internal System.Windows.Forms.TextBox txtIMDBFind;
		internal System.Windows.Forms.TextBox txtIMDBPath;
		internal System.Windows.Forms.ListBox lstResultados;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.ListBox lstNombres;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.Label Label10;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.TextBox txtDirector;
		internal System.Windows.Forms.TextBox txtGenero;
		internal System.Windows.Forms.Label Label12;
		internal System.Windows.Forms.TextBox txtDescripcion;
		internal System.Windows.Forms.Label Label13;
		internal System.Windows.Forms.TextBox txtDuracion;
		internal System.Windows.Forms.Label Label14;
		internal System.Windows.Forms.PictureBox picPortada;
		internal System.Windows.Forms.Label lblTitulo;
		internal System.Windows.Forms.TextBox TextBox1;
		internal System.Windows.Forms.Button Button1;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.txtIMDBPath = new System.Windows.Forms.TextBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.txtIMDBTitulo = new System.Windows.Forms.TextBox();
			this.Label3 = new System.Windows.Forms.Label();
			this.txtIMDBNombre = new System.Windows.Forms.TextBox();
			this.Label4 = new System.Windows.Forms.Label();
			this.txtResultados = new System.Windows.Forms.TextBox();
			this.cmdBuscar = new System.Windows.Forms.Button();
			this.cmdBuscar.Click += new System.EventHandler(this.cmdBuscar_Click);
			this.lstResultados = new System.Windows.Forms.ListBox();
			this.lstResultados.DoubleClick += new System.EventHandler(this.lstResultados_DoubleClick);
			this.Label5 = new System.Windows.Forms.Label();
			this.txtTitulo = new System.Windows.Forms.TextBox();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label7 = new System.Windows.Forms.Label();
			this.txtIMDBFind = new System.Windows.Forms.TextBox();
			this.Label8 = new System.Windows.Forms.Label();
			this.lstNombres = new System.Windows.Forms.ListBox();
			this.Label9 = new System.Windows.Forms.Label();
			this.Panel1 = new System.Windows.Forms.Panel();
			this.lblTitulo = new System.Windows.Forms.Label();
			this.txtDuracion = new System.Windows.Forms.TextBox();
			this.Label14 = new System.Windows.Forms.Label();
			this.txtDescripcion = new System.Windows.Forms.TextBox();
			this.Label13 = new System.Windows.Forms.Label();
			this.txtGenero = new System.Windows.Forms.TextBox();
			this.Label12 = new System.Windows.Forms.Label();
			this.txtDirector = new System.Windows.Forms.TextBox();
			this.Label11 = new System.Windows.Forms.Label();
			this.Label10 = new System.Windows.Forms.Label();
			this.picPortada = new System.Windows.Forms.PictureBox();
			this.TextBox1 = new System.Windows.Forms.TextBox();
			this.Button1 = new System.Windows.Forms.Button();
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Panel1.SuspendLayout();
			this.SuspendLayout();
			//
			//txtIMDBPath
			//
			this.txtIMDBPath.Location = new System.Drawing.Point(184, 24);
			this.txtIMDBPath.Name = "txtIMDBPath";
			this.txtIMDBPath.Size = new System.Drawing.Size(176, 20);
			this.txtIMDBPath.TabIndex = 0;
			this.txtIMDBPath.Text = "http://www.imdb.com";
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(32, 24);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(86, 16);
			this.Label1.TabIndex = 1;
			this.Label1.Text = "Dirección IMDB:";
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(32, 48);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(78, 16);
			this.Label2.TabIndex = 3;
			this.Label2.Text = "Path peliculas:";
			//
			//txtIMDBTitulo
			//
			this.txtIMDBTitulo.Location = new System.Drawing.Point(184, 48);
			this.txtIMDBTitulo.Name = "txtIMDBTitulo";
			this.txtIMDBTitulo.Size = new System.Drawing.Size(176, 20);
			this.txtIMDBTitulo.TabIndex = 2;
			this.txtIMDBTitulo.Text = "/title/tt";
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(32, 72);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(71, 16);
			this.Label3.TabIndex = 5;
			this.Label3.Text = "Path actores:";
			//
			//txtIMDBNombre
			//
			this.txtIMDBNombre.Location = new System.Drawing.Point(184, 72);
			this.txtIMDBNombre.Name = "txtIMDBNombre";
			this.txtIMDBNombre.Size = new System.Drawing.Size(176, 20);
			this.txtIMDBNombre.TabIndex = 4;
			this.txtIMDBNombre.Text = "/name/nm";
			//
			//Label4
			//
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(376, 24);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(143, 16);
			this.Label4.TabIndex = 7;
			this.Label4.Text = "Título página de resultados:";
			//
			//txtResultados
			//
			this.txtResultados.Location = new System.Drawing.Point(528, 24);
			this.txtResultados.Name = "txtResultados";
			this.txtResultados.Size = new System.Drawing.Size(176, 20);
			this.txtResultados.TabIndex = 6;
			this.txtResultados.Text = "IMDB  Search";
			//
			//cmdBuscar
			//
			this.cmdBuscar.Location = new System.Drawing.Point(368, 128);
			this.cmdBuscar.Name = "cmdBuscar";
			this.cmdBuscar.Size = new System.Drawing.Size(72, 24);
			this.cmdBuscar.TabIndex = 8;
			this.cmdBuscar.Text = "Buscar";
			//
			//lstResultados
			//
			this.lstResultados.Location = new System.Drawing.Point(32, 208);
			this.lstResultados.Name = "lstResultados";
			this.lstResultados.Size = new System.Drawing.Size(256, 95);
			this.lstResultados.TabIndex = 9;
			//
			//Label5
			//
			this.Label5.AutoSize = true;
			this.Label5.Location = new System.Drawing.Point(32, 128);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(81, 16);
			this.Label5.TabIndex = 11;
			this.Label5.Text = "Título a buscar:";
			//
			//txtTitulo
			//
			this.txtTitulo.Location = new System.Drawing.Point(184, 128);
			this.txtTitulo.Name = "txtTitulo";
			this.txtTitulo.Size = new System.Drawing.Size(176, 20);
			this.txtTitulo.TabIndex = 10;
			this.txtTitulo.Text = "";
			//
			//Label6
			//
			this.Label6.AutoSize = true;
			this.Label6.Location = new System.Drawing.Point(32, 184);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(106, 16);
			this.Label6.TabIndex = 12;
			this.Label6.Text = "Títulos encontrados:";
			//
			//Label7
			//
			this.Label7.AutoSize = true;
			this.Label7.Location = new System.Drawing.Point(376, 48);
			this.Label7.Name = "Label7";
			this.Label7.Size = new System.Drawing.Size(83, 16);
			this.Label7.TabIndex = 14;
			this.Label7.Text = "Path busqueda:";
			//
			//txtIMDBFind
			//
			this.txtIMDBFind.Location = new System.Drawing.Point(528, 48);
			this.txtIMDBFind.Name = "txtIMDBFind";
			this.txtIMDBFind.Size = new System.Drawing.Size(176, 20);
			this.txtIMDBFind.TabIndex = 13;
			this.txtIMDBFind.Text = "/find?q=%1";
			//
			//Label8
			//
			this.Label8.AutoSize = true;
			this.Label8.Location = new System.Drawing.Point(32, 312);
			this.Label8.Name = "Label8";
			this.Label8.Size = new System.Drawing.Size(119, 16);
			this.Label8.TabIndex = 16;
			this.Label8.Text = "Nombres encontrados:";
			//
			//lstNombres
			//
			this.lstNombres.Location = new System.Drawing.Point(32, 328);
			this.lstNombres.Name = "lstNombres";
			this.lstNombres.Size = new System.Drawing.Size(256, 95);
			this.lstNombres.TabIndex = 15;
			//
			//Label9
			//
			this.Label9.AutoSize = true;
			this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", (float) (6.75F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.Label9.Location = new System.Drawing.Point(528, 72);
			this.Label9.Name = "Label9";
			this.Label9.Size = new System.Drawing.Size(221, 14);
			this.Label9.TabIndex = 17;
			this.Label9.Text = "Con %1 indicamos el nombre de la pelicula a buscar.";
			//
			//Panel1
			//
			this.Panel1.Controls.Add(this.lblTitulo);
			this.Panel1.Controls.Add(this.txtDuracion);
			this.Panel1.Controls.Add(this.Label14);
			this.Panel1.Controls.Add(this.txtDescripcion);
			this.Panel1.Controls.Add(this.Label13);
			this.Panel1.Controls.Add(this.txtGenero);
			this.Panel1.Controls.Add(this.Label12);
			this.Panel1.Controls.Add(this.txtDirector);
			this.Panel1.Controls.Add(this.Label11);
			this.Panel1.Controls.Add(this.Label10);
			this.Panel1.Controls.Add(this.picPortada);
			this.Panel1.Location = new System.Drawing.Point(304, 208);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(504, 184);
			this.Panel1.TabIndex = 18;
			//
			//lblTitulo
			//
			this.lblTitulo.AutoSize = true;
			this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", (float) (9.75F), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.lblTitulo.Location = new System.Drawing.Point(136, 16);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(0, 18);
			this.lblTitulo.TabIndex = 20;
			//
			//txtDuracion
			//
			this.txtDuracion.Location = new System.Drawing.Point(120, 152);
			this.txtDuracion.Name = "txtDuracion";
			this.txtDuracion.Size = new System.Drawing.Size(248, 20);
			this.txtDuracion.TabIndex = 8;
			this.txtDuracion.Text = "";
			//
			//Label14
			//
			this.Label14.AutoSize = true;
			this.Label14.Location = new System.Drawing.Point(24, 152);
			this.Label14.Name = "Label14";
			this.Label14.Size = new System.Drawing.Size(53, 16);
			this.Label14.TabIndex = 7;
			this.Label14.Text = "Duración:";
			//
			//txtDescripcion
			//
			this.txtDescripcion.Location = new System.Drawing.Point(120, 88);
			this.txtDescripcion.Multiline = true;
			this.txtDescripcion.Name = "txtDescripcion";
			this.txtDescripcion.Size = new System.Drawing.Size(248, 56);
			this.txtDescripcion.TabIndex = 6;
			this.txtDescripcion.Text = "";
			//
			//Label13
			//
			this.Label13.AutoSize = true;
			this.Label13.Location = new System.Drawing.Point(24, 88);
			this.Label13.Name = "Label13";
			this.Label13.Size = new System.Drawing.Size(67, 16);
			this.Label13.TabIndex = 5;
			this.Label13.Text = "Descripción:";
			//
			//txtGenero
			//
			this.txtGenero.Location = new System.Drawing.Point(120, 64);
			this.txtGenero.Name = "txtGenero";
			this.txtGenero.Size = new System.Drawing.Size(248, 20);
			this.txtGenero.TabIndex = 4;
			this.txtGenero.Text = "";
			//
			//Label12
			//
			this.Label12.AutoSize = true;
			this.Label12.Location = new System.Drawing.Point(24, 64);
			this.Label12.Name = "Label12";
			this.Label12.Size = new System.Drawing.Size(45, 16);
			this.Label12.TabIndex = 3;
			this.Label12.Text = "Género:";
			//
			//txtDirector
			//
			this.txtDirector.Location = new System.Drawing.Point(120, 40);
			this.txtDirector.Name = "txtDirector";
			this.txtDirector.Size = new System.Drawing.Size(248, 20);
			this.txtDirector.TabIndex = 2;
			this.txtDirector.Text = "";
			//
			//Label11
			//
			this.Label11.AutoSize = true;
			this.Label11.Location = new System.Drawing.Point(24, 40);
			this.Label11.Name = "Label11";
			this.Label11.Size = new System.Drawing.Size(47, 16);
			this.Label11.TabIndex = 1;
			this.Label11.Text = "Director:";
			//
			//Label10
			//
			this.Label10.AutoSize = true;
			this.Label10.Font = new System.Drawing.Font("Microsoft Sans Serif", (float) (9.75F), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.Label10.Location = new System.Drawing.Point(24, 16);
			this.Label10.Name = "Label10";
			this.Label10.Size = new System.Drawing.Size(104, 18);
			this.Label10.TabIndex = 0;
			this.Label10.Text = "Detalle pelicula:";
			//
			//picPortada
			//
			this.picPortada.Location = new System.Drawing.Point(384, 40);
			this.picPortada.Name = "picPortada";
			this.picPortada.Size = new System.Drawing.Size(104, 112);
			this.picPortada.TabIndex = 19;
			this.picPortada.TabStop = false;
			//
			//TextBox1
			//
			this.TextBox1.Location = new System.Drawing.Point(488, 128);
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.Size = new System.Drawing.Size(144, 20);
			this.TextBox1.TabIndex = 19;
			this.TextBox1.Text = "2000";
			//
			//Button1
			//
			this.Button1.Location = new System.Drawing.Point(640, 128);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(176, 24);
			this.Button1.TabIndex = 20;
			this.Button1.Text = "Buscar en BaseCine.com";
			//
			//frmPeliculas
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(824, 462);
			this.Controls.Add(this.Button1);
			this.Controls.Add(this.TextBox1);
			this.Controls.Add(this.Panel1);
			this.Controls.Add(this.Label9);
			this.Controls.Add(this.Label8);
			this.Controls.Add(this.lstNombres);
			this.Controls.Add(this.Label7);
			this.Controls.Add(this.txtIMDBFind);
			this.Controls.Add(this.Label6);
			this.Controls.Add(this.Label5);
			this.Controls.Add(this.txtTitulo);
			this.Controls.Add(this.lstResultados);
			this.Controls.Add(this.cmdBuscar);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.txtResultados);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.txtIMDBNombre);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.txtIMDBTitulo);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.txtIMDBPath);
			this.Name = "frmPeliculas";
			this.Text = "Base de datos IMDB";
			this.Panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			
		}
		
#endregion
		
		
		
		private void cmdBuscar_Click(System.Object sender, System.EventArgs e)
		{
			BuscaPelicula(this.txtTitulo.Text, "");
		}
		
		
		private void lstResultados_DoubleClick(object sender, System.EventArgs e)
		{
			BuscaPelicula("", this.txtIMDBPath.Text + ((pelicula) (lstResultados.Items[this.lstResultados.SelectedIndex])).codigo);
		}
		
		private void MuestraPelicula(FSFormControls.DBHtmlDocument data)
		{
			FSFormControls.DBHtmlNodeCollection nodos = default(FSFormControls.DBHtmlNodeCollection);
			
			this.picPortada.Image = null;
			this.lblTitulo.Text = "";
			this.txtDuracion.Text = "";
			this.txtDirector.Text = "";
			this.txtGenero.Text = "";
			this.txtDescripcion.Text = "";
			
			nodos = data.Nodes.FindByAttributeNameValue("alt", "cover", true);
			
			if (nodos.Count != 0)
			{
				System.IO.Stream ImageStream = new System.Net.WebClient().OpenRead((string) (((FSFormControls.DBHtmlElement) (nodos[0])).Attributes["src"].Value));
				this.picPortada.Image = Image.FromStream(ImageStream);
			}
			
			nodos = data.Nodes.FindByAttributeNameValue("class", "title", true);
			if (nodos.Count != 0)
			{
				this.lblTitulo.Text = ((FSFormControls.DBHtmlElement) (nodos[0])).Text;
			}
			
			nodos = data.Nodes.FindByText("directed");
			if (nodos.Count != 0)
			{
				this.txtDirector.Text = ((FSFormControls.DBHtmlElement) (nodos[0].Next.Next)).Text;
			}
			
			nodos = data.Nodes.FindByText("genre");
			if (nodos.Count != 0)
			{
				this.txtGenero.Text = ((FSFormControls.DBHtmlElement) (nodos[0].Next)).Text;
			}
			
			nodos = data.Nodes.FindByText("plot outline");
			if (nodos.Count != 0)
			{
				this.txtDescripcion.Text = ((FSFormControls.DBHtmlText) (nodos[0].Next)).Text;
			}
			
			nodos = data.Nodes.FindByText("runtime");
			if (nodos.Count != 0)
			{
				this.txtDuracion.Text = ((FSFormControls.DBHtmlText) (nodos[0].Next)).Text;
			}
		}
		
		private void BuscaPelicula(string nombre, string path)
		{
			pelicula p = default(pelicula);
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			
			string pathFind = default(string);
			
			if (path == "")
			{
				pathFind = this.txtIMDBPath.Text + this.txtIMDBFind.Text;
				pathFind = pathFind.Replace("%1", nombre);
			}
			else
			{
				pathFind = path;
			}
			
			string html = FSNetwork.Http.GetHTTP(pathFind);
			
			mDocument = FSFormControls.DBHtmlDocument.Create(html, false);
			
			FSFormControls.DBHtmlNodeCollection n = new FSFormControls.DBHtmlNodeCollection();
			FSFormControls.DBHtmlNode node = default(FSFormControls.DBHtmlNode);
			
			n = mDocument.Nodes.FindByName("title");
			
			if (((FSFormControls.DBHtmlElement) (n[0])).Text.ToLower() != this.txtResultados.Text.ToLower())
			{
				MuestraPelicula(mDocument);
			}
			else
			{
				//página de resultados de titulos
				n = mDocument.Nodes.FindByAttributeNameValue("href", this.txtIMDBTitulo.Text, true);
				
				this.lstResultados.DisplayMember = "titulo";
				this.lstResultados.ValueMember = "codigo";
				this.lstResultados.Items.Clear();
				foreach (FSFormControls.DBHtmlNode tempLoopVar_node in n)
				{
					node = tempLoopVar_node;
					p = new pelicula();
					p.titulo = ((FSFormControls.DBHtmlElement) node).Text;
					p.codigo = ((FSFormControls.DBHtmlElement) node).Attributes["href"].Value;
					
					this.lstResultados.Items.Add(p);
				}
				
				//página de resultados de nombres
				this.lstNombres.Items.Clear();
				n = mDocument.Nodes.FindByAttributeNameValue("href", this.txtIMDBNombre.Text, true);
				
				foreach (FSFormControls.DBHtmlNode tempLoopVar_node in n)
				{
					node = tempLoopVar_node;
					this.lstNombres.Items.Add(((FSFormControls.DBHtmlElement) node).Text);
				}
			}
			
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}
		
		private void Button1_Click(System.Object sender, System.EventArgs e)
		{
			
		}
	}
	
}
