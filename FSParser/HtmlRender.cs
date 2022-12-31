using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FSParser
{

    /// <summary>
    /// Actualiza la información del documento en una plantilla html
    /// utilizada para la confección de la factura.
    /// </summary>
    public class HtmlRender
    {

        object _Data;
        string _HtmlSource;
        string _Html;

        MatchCollection _MatchCollection;

        MatchCollection _MatchCollectionFSCollection;

        Dictionary<string, string> _FSCollectionHtmlTemplates;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="data">Objeto que contien los datos a presentar.</param>
        /// <param name="html">HTML de la plantilla.</param>
        public HtmlRender(object data, string html)
        {

            _Data = data;
            _Html = html;
            _HtmlSource = html;

        }

        /// <summary>
        /// Recupera las claves a renderizar.
        /// </summary>
        /// <returns>Claves a renderizar</returns>
        private void GetMatchCollectionFSCollection()
        {

            string pattern = @"<[^>]+fscollection=" + "('|\")[^('|\")]+('|\")" + @"[^>]*>";

            _MatchCollectionFSCollection = Regex.Matches(_HtmlSource, pattern);

        }

        /// <summary>
        /// Recupera el tag name del elemento html cuyo atributo
        /// fscollection indica que se trata de un elemento de plantilla
        /// para la colección indicada como valor del atributo.
        /// </summary>
        /// <param name="sTag">Etiqueta de apertura del elemento.</param>
        /// <returns>Tag name de elemento html.</returns>
        private string GetFSCollectionTagName(string sTag)
        {
            return Regex.Match(sTag, @"(?<=<)\w+(?=[^>]+fscollection=)").Value;
        }

        /// <summary>
        /// Devuelve el nombre de la propiedad que implementa IList.
        /// </summary>
        /// <param name="sTag">Etiqueta de apertura del elemento.</param>
        /// <returns>Nombre de la propiedad que implementa IList.</returns>
        private string GetFSCollectionListName(string sTag)
        {

            string pattern = @"(?<=<[^>]+fscollection=('|" + "\"))[^('|\")]+(?=('|\")" + @"[^>]*>)";

            return Regex.Match(sTag, pattern).Value;
        }

        /// <summary>
        /// Recupera una plantilla de colección de documento
        /// que posteriormente será utilizada para renderizar la
        /// misma.
        /// </summary>
        /// <param name="sTag">Etiqueta de apertura del elemento.</param>
        /// <returns></returns>
        private string GetFSCollectionHtmlTemplate(string sTag)
        {
            return Regex.Match(sTag, @"(?<=<)\w+(?=[^>]+fscollection=)").Value;
        }

        private void LoadFSCollectionHtmlTemplates()
        {

            _FSCollectionHtmlTemplates = new Dictionary<string, string>();

            foreach (Match match in _MatchCollectionFSCollection)
                LoadFSCollectionHtmlTemplate(match.Value);


        }

        /// <summary>
        /// Carga las plantilla de colección y sustituye el fragmanto 
        /// html de plantilla por un marcador.
        /// </summary>
        /// <param name="sTag">Etiqueta de apertura del elemento
        /// html con el atributo fscollection.</param>
        private void LoadFSCollectionHtmlTemplate(string sTag)
        {

            var tagName = GetFSCollectionTagName(sTag);
            var listName = GetFSCollectionListName(sTag);

            string pattern = $"[\\s\\t]*<{tagName}[^>]+fscollection=('|\"){listName}('|\")[^>]*>[\\s\\S]*?</{tagName}>";

            string htmlTemplate = Regex.Match(_HtmlSource, pattern).Value;
            string tab = Regex.Match(_HtmlSource, $"[\\s\\t]*(?=<{tagName})").Value;

            if (!_FSCollectionHtmlTemplates.ContainsKey(listName))
                _FSCollectionHtmlTemplates.Add(listName, htmlTemplate);

            // Elimino del html de plantilla sustituyendolo por una clave
            _Html = Regex.Replace(_Html, pattern, $"{tab}<!--{listName}-->");


        }

        /// <summary>
        /// Recupera las claves a renderizar.
        /// </summary>
        /// <returns>Claves a renderizar</returns>
        private void GetMatchCollection()
        {

            _MatchCollection = Regex.Matches(_Html, @"(?<=\{)[^\}]+(?=\})");

        }

        /// <summary>
        /// Renderiza las colecciones.
        /// </summary>
        private string RenderizaFSCollections()
        {

            string html = "";

            foreach (KeyValuePair<string, string> fsCollection in _FSCollectionHtmlTemplates)
                html = RenderizaList(fsCollection);

            return html;

        }

        /// <summary>
        /// Renderiza en el Html las listas.
        /// </summary>
        /// <param name="fsCollection">Nombre de la propiedad a renderizar.</param>
        private string RenderizaList(KeyValuePair<string, string> fsCollection)
        {

            var propInfo = _Data.GetType().GetProperty(fsCollection.Key);

            if (propInfo == null)
                throw new InvalidCastException($"No hay ninguna colección de elementos en el documento con el nombre {fsCollection.Key}.");

            IList list = propInfo.GetValue(_Data, null) as IList;

            var htmlTemplate = fsCollection.Value;

            var listMatchCollection = Regex.Matches(htmlTemplate, @"(?<=\{)[^\}]+(?=\})");

            var html = "";

            if (list != null)
            {
                foreach (var item in list)
                {

                    var htmlItem = htmlTemplate;

                    foreach (Match match in listMatchCollection)
                        htmlItem = RenderizaItemList(match.Value, item, htmlItem);

                    html += $"{htmlItem}\n";

                }
            }

            _Html = _Html.Replace($"<!--{fsCollection.Key}-->", html);

            return _Html;

        }

        /// <summary>
        /// Renderiza en el Html las listas.
        /// </summary>
        /// <param name="key">Nombre de la propiedad a renderizar.</param>
        /// <param name="item">Objeto con los datos a sustituir.</param>
        /// <param name="html">Código html a renderizar.</param>
        private string RenderizaItemList(string key, object item, string html)
        {

            var values = key.Split(':');
            var path = values[0];
            var format = values.Length == 1 ? null : values[1];

            var steps = path.Split('.');
            object currentObj = item;

            for (int s = 0; s < steps.Length; s++)
            {

                var pInf = currentObj.GetType().GetProperty(steps[s]);
                var pValue = pInf.GetValue(currentObj, null);

                currentObj = pValue;

                if (currentObj == null)
                    break;

            }

            string rendered = $"{currentObj}";

            if (format != null)
            {

                if (currentObj.GetType().IsAssignableFrom(typeof(DateTime)))
                    rendered = (currentObj as DateTime?)?.ToString(format);
                else if (currentObj.GetType().IsAssignableFrom(typeof(decimal)))
                    rendered = (currentObj as decimal?)?.ToString(format);

            }

            return html.Replace($"{{{key}}}", rendered);

        }

        /// <summary>
        /// Devuelve el Html resultado de la renderización.
        /// </summary>
        /// <returns>Html resultado de la renderización.</returns>
        public string Renderiza()
        {

            GetMatchCollectionFSCollection();

            LoadFSCollectionHtmlTemplates();

            var html = RenderizaFSCollections();

            _Html = html;

            GetMatchCollection();

            foreach (Match match in _MatchCollection)
                Renderiza(match);

            return _Html;

        }


        /// <summary>
        /// Renderiza en el Html una clave concreta.
        /// </summary>
        /// <param name="match">Clave encontrada.</param>
        private void Renderiza(Match match)
        {

            var values = match.Value.Split(':');
            var path = values[0];
            var format = values.Length == 1 ? null : values[1];

            var steps = path.Split('.');
            object currentObj = _Data;

            for (int s = 0; s < steps.Length; s++)
            {

                var pInf = currentObj.GetType().GetProperty(steps[s]);
                if (pInf != null)
                {
                    var pValue = pInf.GetValue(currentObj, null);

                    currentObj = pValue;

                    if (currentObj == null)
                        break;
                }
                else throw new Exception("Propiedad inexistente en documento HTML: " + path);
            }

            string rendered = $"{currentObj}";

            if (format != null)
            {

                if (currentObj.GetType().IsAssignableFrom(typeof(DateTime)))
                    rendered = (currentObj as DateTime?)?.ToString(format);
                else if (currentObj.GetType().IsAssignableFrom(typeof(decimal)))
                    rendered = (currentObj as decimal?)?.ToString(format);

            }

            _Html = _Html.Replace($"{{{match.Value}}}", rendered);

        }
    }
}
