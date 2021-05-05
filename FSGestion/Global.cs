
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;
using FSTrace;

namespace FSGestion
{
	sealed class Global
	{
		//puertos diponibles
		public static bool[] mComs = new bool[4];
		
		public static int tipoUsuario = -1;
		public static Form mdiP;
		public static FSFormControls.Error Err = new FSFormControls.Error();
		public static string appTitle = "FSGestión - (c)" + DateTime.Now.Year + " Febrer Software";
		
		//public static System.Data.OleDb.OleDbConnection dbconn = new System.Data.OleDb.OleDbConnection();
		
		//[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]public static  extern bool SetForegroundWindow(IntPtr hWnd);
		
		//[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow")]public static  extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		
		public static IntPtr CeroIntPtr = new IntPtr(0);

        [STAThread]
		public static void Main()
		{
			//Configuramos el destino de LogUtil
			Log.OnMessageLog += LogUtil_OnMessageLog;

			frmSpash s = new frmSpash();
			s.ShowDialog();
			s.Close();
			s = null;
			
			try
			{
				//IntPtr nWnd;
				////
				//nWnd = FindWindow(null, appTitle);
				
				//si queremos controlar que no esten varias aplicaciones a la vez
				//If nWnd.Equals(CeroIntPtr) Then
				//    ' Si no está, se carga la actual
				//    Application.Run(New mdiPrincipal)
				//Else
				//    ' Si ya está, se activa
				//    SetForegroundWindow(nWnd)
				//    ' y se sale de la actual
				//    Application.Exit()
				//End If
				
				Application.Run(new mdiPrincipal());
			}
			catch (System.Exception ex)
			{
				Err.ErrorMessage(ex);
			}
		}

		private static void LogUtil_OnMessageLog(object sender, Log.LogMessage e)
		{
			if (e.TraceLevel == System.Diagnostics.TraceLevel.Error)
			{
				FSMail.SendMail.SendErrorMail(e.Message);
			}
		}

		public static void AplicaSeguridad(FSFormControls.DBForm frm)
		{
			if (tipoUsuario != 0)
			{
				frm.AllowAddNew = false;
				frm.AllowDelete = false;
				frm.AllowEdit = false;
				frm.AllowSave = false;
				frm.AllowCancel = false;
			}
		}
		
		public static void AplicaToolbar(FSFormControls.DBForm frm)
		{
			frm.ShowRecord = false;
			frm.ShowScrollBar = false;
			frm.ShowPrint = false;
			frm.ShowList = false;
            frm.Mode = FSFormControls.DBUserControlBase.AccessMode.WriteMode;
		}
		
		public static void MuestraCliente(string Cliente)
		{
			frmDetalleCliente frm = new frmDetalleCliente();
			//frm.DBConnection = FSFormControls.Global.DBconnection;
			
			if (Cliente == "")
			{
                MessageBox.Show("Debe cerrar el formulario para actualizar los datos del cliente.","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}

			FSTrace.Log.TraceInfo("Carga de tabla clientes");
			frm.DbControl1.Selection = "select * from clientes"; //frm.DbControl1.Selection.Replace("?", Cliente);
            frm.DbControl1.Connect();
            frm.DbControl1.Go("idCliente", Cliente);
			Log.TraceInfo("Fin carga");

			frm.Show();
		}
		
		public static void MuestraContrato(string contrato)
		{
			frmDetalleContrato frm = new frmDetalleContrato();
			//frm.DBConnection = FSFormControls.Global.DBconnection;
			
			if (contrato == "")
			{
				MessageBox.Show("Debe cerrar el formulario para actualizar los datos del contrato.","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}
			
			frm.DbControl1.Selection = frm.DbControl1.Selection.Replace("?", contrato);
			frm.Show();
		}
		
		public static void MuestraFactura(string factura)
		{
			frmFactura frm = new frmFactura();
			//frm.DBConnection = FSFormControls.Global.DBconnection;
			
			if (factura == "")
			{
				MessageBox.Show("Debe cerrar el formulario para actualizar los datos de la factura.", "", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}
			
			frm.DbControl1.Selection = frm.DbControl1.Selection.Replace("?", factura);
			frm.Show();
		}
		
		public static void MuestraArticulos(string articulo)
		{
			frmDetalleArticulo frm = new frmDetalleArticulo();
			//frm.DBConnection = FSFormControls.Global.DBconnection;
			
			if (articulo == "")
			{
				MessageBox.Show("Debe cerrar el formulario para actualizar los datos del Artículo.","",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			frm.DbControl1.Selection = frm.DbControl1.Selection.Replace("?", articulo);
			frm.Show();
		}
	}
}
