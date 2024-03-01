using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FSLibrary
{
    /// <summary>
    ///     Información de la aplicación
    /// </summary>
    public static class Info
    {
        private static readonly FileVersionInfo fvi;
        private static readonly Assembly ensamblado;
        private static readonly AssemblyName an;

        static Info()
        {
            ensamblado = Assembly.GetExecutingAssembly();
            fvi = FileVersionInfo.GetVersionInfo(ensamblado.Location);
            an = ensamblado.GetName();
        }

        /// <summary>
        ///     La versión del ensamblado
        ///     Equivale al atributo AssemblyVersion
        /// </summary>
        public static Version Version => an.Version;

        /// <summary>
        ///     La versión del ensamblado (FileVersion)
        ///     equivale al atributo: AssemblyFileVersion
        /// </summary>
        public static Version FileVersion => new Version(fvi.FileVersion);

        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        /// <value>
        /// The name of the assembly.
        /// </value>
        public static string AssemblyName => an.FullName;

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public static string Title => fvi.FileDescription;

        /// <summary>
        /// Gets the copyright.
        /// </summary>
        /// <value>
        /// The copyright.
        /// </value>
        public static string Copyright => fvi.LegalCopyright;

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public static string ProductName => fvi.ProductName;

        /// <summary>
        /// Gets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public static string CompanyName => fvi.CompanyName;

        /// <summary>
        /// Gets the trademark.
        /// </summary>
        /// <value>
        /// The trademark.
        /// </value>
        public static string Trademark => fvi.LegalTrademarks;

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public static string Description => fvi.Comments;


        /// <summary>
        /// Devuelve todos métodos, eventos, propiedades y campos de un tipo.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string ClassInfo(Type type)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly |
                        BindingFlags.Static;
            var s = new StringBuilder("");
            s.Append(string.Format("Los miembros públicos de: {0}", type.Name));
            s.Append(s);
            s.Append(new string('=', s.Length));
            s.Append("Los campos");
            var cs = type.GetFields(flags);
            foreach (var m in cs) s.Append(m.Name);
            s.Append("Los métodos");
            var mi = type.GetMethods(flags);
            foreach (var m in mi)
                if (m.IsSpecialName == false)
                    s.Append(m.Name);
            s.Append("Las propiedades");
            var pr = type.GetProperties(flags);
            foreach (var m in pr) s.Append(m.Name);
            s.Append("Los eventos");
            var evs = type.GetEvents(flags);
            foreach (var m in evs) s.Append(m.Name);

            return s.ToString();
        }
    }
}