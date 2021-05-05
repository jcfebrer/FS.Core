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
using System.Data;
using FSDatabase;

#endregion

namespace FSPortal
{
    /// <summary>
    ///     Clase pública con las variables globales del portal.
    /// </summary>
    public class VariablesUsuario
    {
        public string idiomaSel = "";
        public string HTTP_USER_AGENT;
        public Exception LastError;
        public string Usuario = "";
        public int UsuarioId = 0;
        public int GroupId = 0;
        public string NombreCompleto = "";
        public string sessionID = "";
        public string ultimaConexion;
        public bool Administrador = false;
        public string PrecioAMostrar = "PrecioA";
        public double Dto = 0;
        public int paginaInicio;
        public string Campo1 = "";
        public string Campo2 = "";
        public string Campo3 = "";
        public string Campo4 = "";

        //permisos editor por usuario
        public bool bRecAdd;
        public bool bRecEdit;
        public bool bRecDel;
        public bool bQueryExec;
        public bool bSQLExec;
        public bool bTableAdd;
        public bool bTableEdit;
        public bool bTableDel;
        public bool bFldAdd;
        public bool bFldEdit;
        public bool bFldDel;


        public string ComeBack = "";
        public string ComeBack2 = "";
        public string ComeBackTo = "";

        public DataRow UserData;
    }
}