// <fileheader>
// <copyright file="default.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\default.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Configuration;
using System.Text;
using System.Data;
using FSPortal;
using FSLibrary;
using FSDatabase;

namespace FSPaginas.Admin
{
    public class Default : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
			Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            StringBuilder sb = new StringBuilder();


			if (Request["save"] == "y") {
				//AppSettingsSection section = (AppSettingsSection)config.GetSection("appSettings");
				foreach (KeyValueConfigurationElement kvce in config.AppSettings.Settings) {

					KeyValueConfigurationElement setting = config.AppSettings.Settings[kvce.Key];
					if (setting != null)
					{
						setting.Value = Request[kvce.Key];
					}
					//config.AppSettings.Settings.Remove (kvce.Key);
					//config.AppSettings.Settings.Add (kvce.Key, Request[kvce.Key]);
				}

				config.Save(ConfigurationSaveMode.Modified);
			}



            sb.Append(Ui.Lf());
            sb.Append(@"<p class=""cabepeque"">");
            if (Request["save"] == "y")
            	sb.Append("Configuración guardada. Servicio web reiniciado. Deberás volver a conectarte.");
            else
            	sb.Append(FuncionesWeb.Idioma(12));
            
            sb.Append("</p>");

            //ShowRecord sr = new ShowRecord();
            //sb.Append(sr.Inicio("Configuracion", "idConfiguracion", "1", "", false, "", "", 1));


			sb.Append (Ui.Form ("appSettings", "?save=y"));

			DataTable dt = new DataTable();
			dt.Clear();
			dt.Columns.Add("Campo");
			dt.Columns.Add("Valor");

			foreach (KeyValueConfigurationElement kvce in config.AppSettings.Settings) {
				DataRow row = dt.NewRow ();
				row ["Campo"] = kvce.Key.ToString();
                row ["Valor"] = Ui.TextBox(kvce.Key.ToString (), Functions.Valor(kvce.Value), 70);

				dt.Rows.Add (row);
			}

			sb.Append(Ui.HtmlTable(dt));
            sb.Append(Ui.Lf());
            sb.Append (Ui.Button ("Guardar cambios", ""));
			sb.Append (Ui.EndForm ());


            if (!Variables.App.modoLite)
            {
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

                if (db.TableExists(Variables.App.prefijoTablas + "Stats"))
                {
                    //estadísticas
                    Stats stats = new Stats();
                    sb.Append(stats.Inicio());
                }
            }

            return sb.ToString();
        }
    }
}