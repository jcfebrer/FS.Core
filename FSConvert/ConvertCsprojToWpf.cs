#if NET47_OR_GREATER || NETCOREAPP

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FSConvert
{
    public class ConvertCsprojToWpf
    {
        /// <summary>
        /// Convierte el contenido de un .csproj antiguo de WinForms al formato SDK moderno de WPF.
        /// </summary>
        /// <param name="oldCsprojContent">Contenido XML del archivo .csproj original.</param>
        /// <param name="targetFramework">Framework de destino (ej: "net8.0-windows" o "net48").</param>
        /// <returns>Cadena con el nuevo formato XML del .csproj.</returns>
        public static string Convert(string oldCsprojContent, string targetFramework = "net8.0-windows")
        {
            // Cargamos el XML. Quitamos el espacio de nombres por defecto temporalmente para facilitar la manipulación de nodos lúdicos.
            XElement root = XElement.Parse(oldCsprojContent);
            XNamespace ns = root.Name.Namespace;

            // 1. Crear la estructura limpia del nuevo formato SDK
            // El formato SDK moderno es sumamente compacto y no requiere guías heredadas.
            XElement newRoot = new XElement("Project",
                new XAttribute("Sdk", "Microsoft.NET.Sdk")
            );

            XElement propertyGroup = new XElement("PropertyGroup");

            // Asignar el nuevo TargetFramework lógico
            propertyGroup.Add(new XElement("TargetFramework", targetFramework));

            // Activar la compatibilidad nativa de WPF
            propertyGroup.Add(new XElement("UseWPF", "true"));

            // Configurar la salida como aplicación de escritorio ejecutable (WinExe)
            propertyGroup.Add(new XElement("OutputType", "WinExe"));

            // Opcional: Si el proyecto original tenía habilitado el tipado nulo o plataformas específicas, intentamos preservarlo
            var oldOutputType = root.DescendantNodes().OfType<XElement>().FirstOrDefault(x => x.Name.LocalName == "OutputType");
            if (oldOutputType != null && oldOutputType.Value == "Library")
            {
                // Si originalmente era una librería de clases de controles personalizados (DLL), mantenemos ese comportamiento
                propertyGroup.Element("OutputType").Value = "Library";
            }

            newRoot.Add(propertyGroup);

            // 2. PRESERVAR REFERENCIAS DE PAQUETES (NuGet PackageReferences)
            // Buscamos si el proyecto viejo ya utilizaba el formato de PackageReference para migrar sus dependencias
            var itemGroups = root.DescendantNodes().OfType<XElement>().Where(x => x.Name.LocalName == "ItemGroup");
            XElement packageItemGroup = new XElement("ItemGroup");

            foreach (var itemGroup in itemGroups)
            {
                var packages = itemGroup.Elements().Where(x => x.Name.LocalName == "PackageReference");
                foreach (var pkg in packages)
                {
                    // Clonamos la referencia limpia eliminando los prefijos de namespaces antiguos
                    XElement newPkg = new XElement("PackageReference",
                        new XAttribute("Include", pkg.Attribute("Include")?.Value ?? "")
                    );
                    if (pkg.Attribute("Version") != null)
                    {
                        newPkg.Add(new XAttribute("Version", pkg.Attribute("Version").Value));
                    }
                    else if (pkg.Element(ns + "Version") != null)
                    {
                        newPkg.Add(new XAttribute("Version", pkg.Element(ns + "Version").Value));
                    }
                    packageItemGroup.Add(newPkg);
                }

                // Preservar también referencias directas a otros proyectos de la solución (.csproj hermanos)
                var projectReferences = itemGroup.Elements().Where(x => x.Name.LocalName == "ProjectReference");
                foreach (var projRef in projectReferences)
                {
                    XElement newProjRef = new XElement("ProjectReference",
                        new XAttribute("Include", projRef.Attribute("Include")?.Value ?? "")
                    );
                    packageItemGroup.Add(newProjRef);
                }
            }

            // Si se encontraron dependencias externas o de proyectos, añadimos el bloque de ItemGroup
            if (packageItemGroup.HasElements)
            {
                newRoot.Add(packageItemGroup);
            }

            // Retornamos el XML formateado de manera limpia
            return newRoot.ToString();
        }
    }
}

#endif