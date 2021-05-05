using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModFavoritos : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Favoritos();
        }

        public string Name
        {
            get { return "ModFavoritos"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Favoritos()
        {
            string modFavoritosReturn = "" + "\r\n";

            if (Functions.Valor(Variables.User.Usuario) != "")
            {
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

                string ssql = "SELECT favoritoid,url,comentarios,titulo from " + Variables.App.prefijoTablas +
                              "Favoritos where usuarioID=" + Variables.User.UsuarioId;
                DataTable dtFavoritos = db.Execute(ssql);

                if (dtFavoritos.Rows.Count > 0)
                {
                    foreach (DataRow row in dtFavoritos.Rows)
                    {
                        modFavoritosReturn = modFavoritosReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                             "imagenes/bullet.gif' alt='' />" +
                                             Ui.EditPage("favoritos", "favoritoid", row["favoritoid"].ToString(),
                                                 "Editar favorito", "Borrar favorito") +
                                             Ui.Link(row["titulo"].ToString(), row["URL"].ToString()) + Ui.Lf() + "\r\n";
                    }
                }
                else
                {
                    modFavoritosReturn = "No tienes favoritos.";
                }
            }
            else
            {
                modFavoritosReturn = "Debes registrarte para acceder a este modulo.";
            }
            return modFavoritosReturn;
        }
    }
}