// <fileheader>
// <copyright file="cast.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: encuestas\cast.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSDatabase;

namespace FSPortalEncuestas
{
    /// <summary>
    ///     Encuestas
    /// </summary>
    public class Cast : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            EncuestaController ec = new EncuestaController();

            StringBuilder sb = new StringBuilder("");
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);


            int lEncuestaId = NumberUtils.NumberInt(Request["ID"]);
            int lSecenekId = NumberUtils.NumberInt(Request["optSecenek"]);
            string userIp = Functions.Valor(Request.ServerVariables["REMOTE_ADDR"]);

            if (lSecenekId == 0)
            {
                sb.Append("<p class=\"textopeque\">");
                sb.Append("Debes seleccionar una opción." + Ui.Lf());
                sb.Append("<a href=\"" + Request["http_referer"] + "\">Volver</a>");
            }
            else
            {
                string sSql = "SELECT IP FROM " + Variables.App.prefijoTablas + "RespuestasEncuestas WHERE IP ='" + userIp +
                              "' AND EncuestaID = " + lEncuestaId;
                DataTable dt = db.Execute(sSql);

                bool chek = (dt.Rows.Count > 0);

                if (!chek)
                {
                    //sSql = "SELECT EncuestaID, OpcionID, IP FROM " + Var.prefijoTablas +
                    //       "RespuestasEncuestas WHERE RespuestaID = 0";
                    //dt = db.Execute(sSql);

                    Register reg = new Register
                    {
                        new Field("EncuestaID", lEncuestaId.ToString(), typeof (Int32)),
                        new Field("OpcionID", lSecenekId.ToString(), typeof (Int32)),
                        new Field("IP", userIp, typeof (String))
                    };

                    sSql = db.InsertSql("RespuestasEncuestas", reg, Variables.User.UsuarioId);
                    db.Execute(sSql);

                    sb.Append(ec.ShowData(lEncuestaId, "Gracias por participar en nuestra encuesta."));
                }
                else
                {
                    sb.Append(ec.ShowData(lEncuestaId, "Perdona, alguien con tu misma dirección IP ya ha votado."));
                }
            }
            return sb.ToString();
        }
    }
}