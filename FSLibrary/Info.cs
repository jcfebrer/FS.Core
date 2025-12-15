using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FSLibrary
{
    /// <summary>
    ///     Información de la aplicación
    /// </summary>
    public class Info
    {
        private readonly FileVersionInfo fvi;
        private readonly Assembly ensamblado;
        private readonly AssemblyName an;

        /// <summary>
        /// Constructor estático
        /// </summary>
        /// <param name="ensamblado"></param>
        public Info(Assembly ensamblado)
        {
            this.ensamblado = ensamblado;
            this.fvi = FileVersionInfo.GetVersionInfo(ensamblado.Location);
            this.an = ensamblado.GetName();
        }

        /// <summary>
        ///     La versión del ensamblado
        ///     Equivale al atributo AssemblyVersion
        /// </summary>
        public Version Version => an.Version;

        /// <summary>
        ///     La versión del ensamblado (FileVersion)
        ///     equivale al atributo: AssemblyFileVersion
        /// </summary>
        public Version FileVersion
        {
            get
            {
                string version = GetAssemblyAttribute<AssemblyVersionAttribute>();
                return string.IsNullOrEmpty(version) ? new Version(fvi.FileVersion) : new Version(version);
            }
        }

        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        /// <value>
        /// The name of the assembly.
        /// </value>
        public string AssemblyName => an.FullName.Trim();

        /// <summary>
        /// Gets the title. (Lee de AssemblyTitleAttribute si está definido)
        /// </summary>
        public string Title
        {
            get
            {
                string title = GetAssemblyAttribute<AssemblyTitleAttribute>();
                return string.IsNullOrEmpty(title) ? fvi.FileDescription.Trim() : title;
            }
        }

        /// <summary>
        /// Gets the copyright.
        /// </summary>
        /// <value>
        /// The copyright.
        /// </value>
        public string Copyright
        {
            get
            {
                string copyright = GetAssemblyAttribute<AssemblyCopyrightAttribute>();
                return string.IsNullOrEmpty(copyright) ? fvi.LegalCopyright.Trim() : copyright;
            }
        }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public string ProductName
        {
            get
            {
                string productName = GetAssemblyAttribute<AssemblyProductAttribute>();
                return string.IsNullOrEmpty(productName) ? fvi.ProductName.Trim() : productName;
            }
        }

        /// <summary>
        /// Gets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName
        {
            get
            {
                string companyName = GetAssemblyAttribute<AssemblyCompanyAttribute>();
                return string.IsNullOrEmpty(companyName) ? fvi.CompanyName.Trim() : companyName;
            }
        }

        /// <summary>
        /// Gets the trademark.
        /// </summary>
        /// <value>
        /// The trademark.
        /// </value>
        public string Trademark
        {
            get
            {
                string trademark = GetAssemblyAttribute<AssemblyTrademarkAttribute>();
                return string.IsNullOrEmpty(trademark) ? fvi.LegalTrademarks.Trim() : trademark;
            }
        }

        /// <summary>
        /// La descripción. (Lee de AssemblyDescriptionAttribute)
        /// </summary>
        public string Description
        {
            get
            {
                string description = GetAssemblyAttribute<AssemblyDescriptionAttribute>();
                return string.IsNullOrEmpty(description) ? fvi.Comments.Trim() : description;
            }
        }

        /// <summary>
        /// Gets the Authors. (Generalmente mapeado a AssemblyCompany o AssemblyInformationalVersion)
        /// </summary>
        public string Authors
        {
            get
            {
                // En .NET SDK, <Authors> a menudo se mapea a InformationalVersion o Company
                string authors = GetAssemblyAttribute<AssemblyCompanyAttribute>();
                if (!string.IsNullOrEmpty(authors)) return authors;

                // Como alternativa, puedes intentar buscar directamente la propiedad
                // en el .csproj, que puede estar mapeada a InformationalVersion si no se usa Company
                string infoVersion = "";
#if NET45_OR_GREATER || NETCOREAPP
                infoVersion = ensamblado.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
#endif

                // Esto solo funciona si <InformationalVersion> se configuró con los autores
                // o si un proceso de CI/CD inyectó el valor allí.
                return string.IsNullOrEmpty(infoVersion) ? fvi.CompanyName.Trim() : infoVersion;
            }
        }

        /// <summary>
        /// Helper para obtener atributos de ensamblado de forma segura
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetAssemblyAttribute<T>() where T : Attribute
        {
            Attribute attribute = null;
#if NET45_OR_GREATER || NETCOREAPP
            // Usa GetCustomAttribute en el ensamblado que pasaste al constructor
            attribute = ensamblado.GetCustomAttribute<T>();
#endif

            // Devuelve el valor del atributo (maneja casos especiales)
            if (attribute is AssemblyTitleAttribute titleAttr)
                return titleAttr.Title.Trim();
            if (attribute is AssemblyDescriptionAttribute descAttr)
                return descAttr.Description.Trim();
            if (attribute is AssemblyCompanyAttribute companyAttr)
                return companyAttr.Company.Trim();
            if (attribute is AssemblyCopyrightAttribute copyrightAttr)
                return copyrightAttr.Copyright.Trim();

            // Para Authors, se usa AssemblyInformationalVersionAttribute o AssemblyCompanyAttribute
            return string.Empty;
        }

        /// <summary>
        /// Devuelve todos métodos, eventos, propiedades y campos de un tipo.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public string ClassInfo(Type type)
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