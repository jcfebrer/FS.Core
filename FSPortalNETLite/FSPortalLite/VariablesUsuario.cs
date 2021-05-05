// // <fileheader>
// // <copyright file="VariablesUsuario.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Collections.Generic;
using System.Data;

#endregion

namespace FSPortalLite
{
    /// <summary>
    ///     Clase pública con las variables globales del portal.
    /// </summary>
    public class VariablesUsuario
    {
#pragma warning disable 1591

        //
        //variables de usuario
        //
        public enum FormMod
        {
            Nada,
            Login
        }
        public string HTTP_USER_AGENT;
        public Exception LastError;
        public string Usuario = "";
        public int UsuarioId = 0;
        public int GroupId = 0;
        public string sessionID = "";
        public bool Administrador = false;
        public string Campo1 = "";
        public string Campo2 = "";
        public string Campo3 = "";
        public string Campo4 = "";


        // variables de Script.cs
        public string[] frmVariable = new string[100];
        public string frmTabla = "";
        public string frmSeleccion = "";
        public string frmSelect = "";
        public string frmOrden = "";
        public DataTable frmData = null;
        public DataTable frmDataTemp = null;
        public string frmEmailTo = "";
        public string frmPagina = "";
        public string frmEmailSubject = "";
        public long frmPosicion = 0;
        public long frmPosicionTemp = 0;
        public string frmRequest = "";
        public int frmPaginas = 0;
        public int frmPaginaActual = 0;
        public long frmTotalRegistros = 0;
        public int frmMaxRegistrosPagina = 0;
        public string frmLinkSiguiente = "";
        public string frmLinkAnterior = "";
        public string frmLinkPrimero = "";
        public string frmLinkUltimo = "";
        public int frmFileMaxSize = 100;
        public string frmFileTypes = "jpg,gif,bmp,jpeg,png";
        public string frmFileUploadPath = "";
        public string frmMensajeSinRegistros = "No hay registros.";
        public string frmMensajeCombo = "Selecciona ...";
        public string frmComboOnChange = "";
        public string frmComboSelected = "";
        public string frmConn = "";
        public string frmRedirige = "";
        public bool frmTruncar = true;
        public string frmMensajeOK = "Formulario procesado correctamente.";
        public string frmMensajeNoOK = "Problemas al procesar el formulario.";
        public string frmVolverAtras = @"<a href=""javascript:history.back()"">Volver atrás</a>";
        public long frmIdentity = 0;

        public string ComeBack = "";

        public DataRow UserData;
        public FormMod frmModo;
        public IDictionary<string, string> contenidoRepite = new Dictionary<string, string>();

#pragma warning restore 1591
    }
}