#if NET47_OR_GREATER || NETCOREAPP

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FSConvert
{
    public class ConvertToWPF
    {
        private static readonly Dictionary<string, string> ControlTypeMapping = new Dictionary<string, string>()
        {
            { "TabControl", "fs:TabControl" },
            { "DBTabControl", "fs:TabControl" },
            { "TabPage", "fs:TabItem" },
            { "DBTabPage", "fs:TabItem" },
            { "GroupBox", "fs:GroupBox" },
            { "DBGroupBox", "fs:GroupBox" },
            { "TextBox", "fs:TextBox" },
            { "DBTextBox", "fs:TextBox" },
            { "DBTextBoxEx", "fs:TextBox" },
            { "Label", "fs:Label" },
            { "DBLabel", "fs:Label" },
            { "DBLabelEx", "fs:Label" },
            { "Button", "fs:Button" },
            { "DBButton", "fs:Button" },
            { "DBButtonEx", "fs:Button" },
            { "CheckBox", "fs:CheckBox" },
            { "DBCheckBox", "fs:CheckBox" },
            { "DBCheckBoxEx", "fs:CheckBox" },
            { "Combo", "fs:ComboBox" },
            { "DBCombo", "fs:ComboBox" },
            { "DBComboEx", "fs:ComboBox" },
            { "GridView", "fs:DataGrid" },
            { "DBGridView", "fs:DataGrid" },
            { "DBGridViewEx", "fs:DataGrid" },
            { "Date", "fs:DatePicker" },
            { "DBDate", "fs:DatePicker" }
        };

        // Diccionario de traducción de eventos comunes de WinForms a WPF
        private static readonly Dictionary<string, string> EventMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Click", "Click" },
            { "TextChanged", "TextChanged" },
            { "SelectedIndexChanged", "SelectionChanged" },
            { "CheckedChanged", "Checked" },
            { "Load", "Loaded" },
            { "Leave", "LostFocus" },
            { "Enter", "GotFocus" },
            { "KeyDown", "KeyDown" },
            { "KeyPress", "TextInput" },
            { "KeyUp", "KeyUp" },
            { "MouseClick", "MouseDown" },
            { "DoubleClick", "MouseDoubleClick" }
        };

        public static string Convert(string designerCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(designerCode);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            var controlTypes = new Dictionary<string, string>();
            var controlProperties = new Dictionary<string, Dictionary<string, string>>();
            var controlRelations = new Dictionary<string, List<string>>();
            var controlEvents = new Dictionary<string, Dictionary<string, string>>();

            string windowTitle = "Window Title";
            string windowWidth = "1220";
            string windowHeight = "850";

            var allNodes = root.DescendantNodes();

            // ------------------------------------------------------------------
            // EXTRACCIÓN DINÁMICA DE CLASE Y NAMESPACE DESDE EL ARCHIVO FUENTE
            // ------------------------------------------------------------------
            string extractedClassName = "ClassName";
            string extractedNamespace = "Namespace";

            // 1. Extraer el nombre de la clase
            var classDeclaration = allNodes.OfType<ClassDeclarationSyntax>().FirstOrDefault();
            if (classDeclaration != null)
            {
                extractedClassName = classDeclaration.Identifier.Text;
            }

            // 2. Extraer el Namespace original (Soporta bloques tradicionales y File-Scoped de C# 10+)
            var namespaceDeclaration = allNodes.OfType<BaseNamespaceDeclarationSyntax>().FirstOrDefault();
            if (namespaceDeclaration != null)
            {
                extractedNamespace = namespaceDeclaration.Name.ToString().Trim();
            }

            // Combinamos dinámicamente para estructurar la propiedad x:Class completa
            string fullWpfXClass = extractedNamespace + "." + extractedClassName;

            // ------------------------------------------------------------------
            // PASO 1: REGISTRO DE TIPOS Y RELACIONES JERÁRQUICAS (Roslyn AST nativo)
            // ------------------------------------------------------------------
            foreach (var node in allNodes)
            {
                if (node is AssignmentExpressionSyntax assignExpr && assignExpr.Right is ObjectCreationExpressionSyntax createExpr)
                {
                    string leftStr = assignExpr.Left.ToString().Trim();
                    string controlName = ExtractControlNameFromLeft(leftStr);
                    string fullType = createExpr.Type.ToString();
                    string shortType = fullType.Split('.').Last();

                    if (ControlTypeMapping.ContainsKey(shortType) && !string.IsNullOrEmpty(controlName))
                    {
                        controlTypes[controlName] = ControlTypeMapping[shortType];
                    }
                }

                if (node is InvocationExpressionSyntax invokeExpr && invokeExpr.Expression is MemberAccessExpressionSyntax methodAccess)
                {
                    string methodName = methodAccess.Name.ToString();
                    string methodLeft = methodAccess.Expression.ToString();

                    if (methodName == "Add" && methodLeft.EndsWith(".Controls"))
                    {
                        string parentName = ExtractControlNameFromLeft(methodLeft.Substring(0, methodLeft.Length - 9));
                        var argument = invokeExpr.ArgumentList.Arguments.FirstOrDefault();
                        if (argument != null)
                        {
                            string childName = ExtractControlNameFromLeft(argument.Expression.ToString());
                            RegisterRelation(controlRelations, parentName, childName);
                        }
                    }
                    else if (methodName == "AddRange" && methodLeft.EndsWith(".Controls"))
                    {
                        string parentName = ExtractControlNameFromLeft(methodLeft.Substring(0, methodLeft.Length - 9));
                        var argument = invokeExpr.ArgumentList.Arguments.FirstOrDefault();
                        if (argument != null && argument.Expression is ArrayCreationExpressionSyntax arrayCreate && arrayCreate.Initializer != null)
                        {
                            foreach (var expr in arrayCreate.Initializer.Expressions)
                            {
                                string childName = ExtractControlNameFromLeft(expr.ToString());
                                RegisterRelation(controlRelations, parentName, childName);
                            }
                        }
                    }
                }
            }

            // ------------------------------------------------------------------
            // PASO 2: PARSER HÍBRIDO LÍNEA POR LÍNEA (Propiedades y Eventos +=)
            // ------------------------------------------------------------------
            using (StringReader reader = new StringReader(designerCode))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith("//")) continue;

                    // DETECCIÓN DE EVENTOS WINFORMS (Patrón: control.Evento += ...)
                    if (line.Contains("+="))
                    {
                        int opIdx = line.IndexOf("+=");
                        string leftSide = line.Substring(0, opIdx).Trim();
                        string rightSide = line.Substring(opIdx + 2).Trim().TrimEnd(';');

                        if (leftSide.Contains("."))
                        {
                            string controlName = ExtractControlNameFromLeft(leftSide);
                            string wfEventName = leftSide.Split('.').Last();

                            string handlerMethod = rightSide;
                            int lastDot = rightSide.LastIndexOf('.');
                            if (lastDot != -1)
                            {
                                handlerMethod = rightSide.Substring(lastDot + 1).Replace(")", "").Replace(";", "").Trim();
                            }

                            if (!string.IsNullOrEmpty(controlName) && controlName != "this")
                            {
                                string wpfEventName = EventMapping.ContainsKey(wfEventName) ? EventMapping[wfEventName] : wfEventName;

                                if (!controlEvents.ContainsKey(controlName))
                                    controlEvents[controlName] = new Dictionary<string, string>();

                                controlEvents[controlName][wpfEventName] = handlerMethod;
                            }
                        }
                        continue;
                    }

                    // DETECCIÓN DE ASIGNACIONES ESTÁNDAR (=)
                    if (line.Contains("="))
                    {
                        int assignIdx = line.IndexOf('=');
                        string leftSide = line.Substring(0, assignIdx).Trim();
                        string rightSide = line.Substring(assignIdx + 1).Trim().TrimEnd(';');

                        if (leftSide.Contains("."))
                        {
                            string controlName = ExtractControlNameFromLeft(leftSide);
                            string propertyName = leftSide.Split('.').Last();

                            if (!string.IsNullOrEmpty(controlName) && controlName != "this")
                            {
                                if (!controlTypes.ContainsKey(controlName))
                                {
                                    if (controlName.ToLower().Contains("button")) controlTypes[controlName] = "fs:Button";
                                    else controlTypes[controlName] = "Grid";
                                }

                                if (!controlProperties.ContainsKey(controlName))
                                    controlProperties[controlName] = new Dictionary<string, string>();

                                controlProperties[controlName][propertyName] = rightSide;
                            }
                            else if (controlName == "this")
                            {
                                if (propertyName == "Text") windowTitle = rightSide.Trim('"');
                                else if (propertyName == "ClientSize" || propertyName == "Size")
                                {
                                    var sizeParts = ExtractCoordinates(rightSide);
                                    if (sizeParts.Length >= 2)
                                    {
                                        windowWidth = sizeParts[0];
                                        windowHeight = sizeParts[1];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // ------------------------------------------------------------------
            // PASO 3: CONSTRUCCIÓN DEL ÁRBOL XML DE WPF
            // ------------------------------------------------------------------
            List<string> rootContainers = new List<string>();
            if (controlRelations.ContainsKey("this"))
            {
                rootContainers = controlRelations["this"];
            }
            else
            {
                var allChildren = controlRelations.Values.SelectMany(x => x).ToHashSet();
                rootContainers = controlRelations.Keys.Where(parent => !allChildren.Contains(parent) && parent != "this").ToList();
            }

            foreach (var rName in rootContainers)
            {
                if (!controlTypes.ContainsKey(rName)) controlTypes[rName] = "Grid";
            }

            XNamespace xmlw = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
            XNamespace xmlx = "http://schemas.microsoft.com/winfx/2006/xaml";
            XNamespace xmlfs = "clr-namespace:FSFormControls.UI;assembly=FSFormControls.UI";

            XElement xamlRoot = new XElement(xmlw + "Window",
                new XAttribute("xmlns", xmlw.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "x", xmlx.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "fs", xmlfs.NamespaceName),
                new XAttribute(xmlx + "Class", fullWpfXClass),
                new XAttribute("Title", windowTitle),
                new XAttribute("Width", windowWidth),
                new XAttribute("Height", windowHeight)
            );

            XElement mainGrid = new XElement(xmlw + "Grid");
            xamlRoot.Add(mainGrid);

            foreach (var rootName in rootContainers)
            {
                mainGrid.Add(BuildXamlElement(rootName, controlTypes, controlProperties, controlRelations, controlEvents, xmlw, xmlfs));
            }

            return xamlRoot.ToString();
        }

        private static XElement BuildXamlElement(
            string name,
            Dictionary<string, string> types,
            Dictionary<string, Dictionary<string, string>> properties,
            Dictionary<string, List<string>> relations,
            Dictionary<string, Dictionary<string, string>> events,
            XNamespace nsWpf,
            XNamespace nsFs)
        {
            string rawType = types.ContainsKey(name) ? 
                types[name] : 
                "FrameworkElement";

            XElement element;
            string cleanType = rawType;

            if (rawType.StartsWith("fs:"))
            {
                cleanType = rawType.Substring(3);
                element = new XElement(nsFs + cleanType);
            }
            else
            {
                element = new XElement(nsWpf + rawType);
            }

            element.Add(new XAttribute("Name", name));

            if (properties.ContainsKey(name))
            {
                var props = properties[name];

                if (props.ContainsKey("Text"))
                {
                    string textValue = props["Text"].Trim('"');
                    if (cleanType == "TabItem" || cleanType == "GroupBox") element.Add(new XAttribute("Header", textValue));
                    else if (cleanType == "Label" || cleanType == "Button" || cleanType == "CheckBox") element.Add(new XAttribute("Content", textValue));
                    else if (cleanType == "TextBox") element.Add(new XAttribute("Text", textValue));
                }

                bool isHiddenByCode = props.ContainsKey("Visible") &&
                                     (props["Visible"].ToLower() == "false" || props["Visible"] == "False");

                if (isHiddenByCode)
                {
                    element.Add(new XAttribute("Visibility", "Collapsed"));
                }

                // REGLA DE EXCLUSIÓN: Si es un TabItem, NO inyectamos márgenes, dimensiones ni alineaciones
                if (cleanType != "TabItem")
                {
                    string locKey = props.ContainsKey("Location") ? "Location" : (props.ContainsKey("Bounds") ? "Bounds" : null);
                    string sizeKey = props.ContainsKey("Size") ? "Size" : (props.ContainsKey("Bounds") ? "Bounds" : null);

                    if (locKey != null && sizeKey != null)
                    {
                        var marginParts = ExtractCoordinates(props[locKey]);
                        var sizeParts = ExtractCoordinates(props[sizeKey]);

                        if (marginParts.Length >= 2 && sizeParts.Length >= 2)
                        {
                            int.TryParse(marginParts[0], out int left);
                            int.TryParse(marginParts[1], out int top);

                            int widthIdx = sizeKey == "Bounds" ? 2 : 0;
                            int heightIdx = sizeKey == "Bounds" ? 3 : 1;
                            int.TryParse(sizeParts[widthIdx], out int width);
                            int.TryParse(sizeParts[heightIdx], out int height);

                            if (left < -2000 || left > 2000 || top < -2000 || top > 2000)
                            {
                                if (element.Attribute("Visibility") == null) element.Add(new XAttribute("Visibility", "Collapsed"));
                                left = Math.Abs(left) % 1000;
                                top = Math.Abs(top) % 1000;
                            }

                            if (left == 0) left = 10;
                            if (top == 0) top = 10;

                            element.Add(new XAttribute("HorizontalAlignment", "Left"));
                            element.Add(new XAttribute("VerticalAlignment", "Top"));
                            element.Add(new XAttribute("Margin", left + "," + top + ",0,0"));
                            element.Add(new XAttribute("Width", width));
                            element.Add(new XAttribute("Height", height));
                        }
                    }
                }
            }

            // Inyección de eventos
            if (events.ContainsKey(name))
            {
                foreach (var evt in events[name])
                {
                    element.Add(new XAttribute(evt.Key, evt.Value));
                }
            }

            if (relations.ContainsKey(name))
            {
                bool isInputWithInlineButtons = (cleanType == "TextBox" || cleanType == "ComboBox") && relations[name].Count > 0;
                bool needsLayoutBridge = (cleanType == "TabItem" || cleanType == "GroupBox" || cleanType == "Grid") && relations[name].Count > 0;

                if (isInputWithInlineButtons)
                {
                    XElement structuralGrid = new XElement(nsWpf + "Grid");

                    if (properties.ContainsKey(name))
                    {
                        var parentProps = properties[name];
                        string pLocKey = parentProps.ContainsKey("Location") ? "Location" : null;
                        string pSizeKey = parentProps.ContainsKey("Size") ? "Size" : null;

                        if (pLocKey != null && pSizeKey != null)
                        {
                            var marginParts = ExtractCoordinates(parentProps[pLocKey]);
                            var sizeParts = ExtractCoordinates(parentProps[pSizeKey]);

                            if (marginParts.Length >= 2 && sizeParts.Length >= 2)
                            {
                                int.TryParse(marginParts[0], out int left);
                                int.TryParse(marginParts[1], out int top);
                                int.TryParse(sizeParts[0], out int width);
                                int.TryParse(sizeParts[1], out int height);

                                if (left < -2000 || left > 2000 || top < -2000 || top > 2000)
                                {
                                    structuralGrid.Add(new XAttribute("Visibility", "Collapsed"));
                                    left = Math.Abs(left) % 1000;
                                    top = Math.Abs(top) % 1000;
                                }

                                structuralGrid.Add(new XAttribute("Margin", left + "," + top + ",0,0"));
                                structuralGrid.Add(new XAttribute("Width", width));
                                structuralGrid.Add(new XAttribute("Height", height));
                            }
                        }
                    }

                    if (element.Attribute("Visibility") != null && structuralGrid.Attribute("Visibility") == null)
                    {
                        structuralGrid.Add(new XAttribute("Visibility", "Collapsed"));
                    }

                    structuralGrid.Add(new XAttribute("HorizontalAlignment", "Left"));
                    structuralGrid.Add(new XAttribute("VerticalAlignment", "Top"));

                    if (element.Attribute("Width") != null) element.Attribute("Width").Remove();
                    if (element.Attribute("Height") != null) element.Attribute("Height").Remove();
                    if (element.Attribute("Margin") != null) element.Attribute("Margin").Remove();

                    element.SetAttributeValue("Margin", "0");
                    structuralGrid.Add(element);

                    foreach (var childName in relations[name])
                    {
                        var childElement = BuildXamlElement(childName, types, properties, relations, events, nsWpf, nsFs);

                        if (properties.ContainsKey(childName) && properties[childName].ContainsKey("Location"))
                        {
                            var btnCoords = ExtractCoordinates(properties[childName]["Location"]);
                            if (btnCoords.Length >= 2)
                            {
                                int.TryParse(btnCoords[0], out int bLeft);
                                int.TryParse(btnCoords[1], out int bTop);
                                childElement.SetAttributeValue("Margin", bLeft + "," + bTop + ",2,0");
                            }
                        }
                        else
                        {
                            childElement.SetAttributeValue("Margin", "0,0,2,0");
                        }

                        childElement.SetAttributeValue("HorizontalAlignment", "Right");
                        structuralGrid.Add(childElement);
                    }

                    return structuralGrid;
                }
                else if (needsLayoutBridge)
                {
                    XElement bridgeGrid = new XElement(nsWpf + "Grid");
                    element.Add(bridgeGrid);

                    foreach (var childName in relations[name])
                    {
                        bridgeGrid.Add(BuildXamlElement(childName, types, properties, relations, events, nsWpf, nsFs));
                    }
                }
                else
                {
                    foreach (var childName in relations[name])
                    {
                        element.Add(BuildXamlElement(childName, types, properties, relations, events, nsWpf, nsFs));
                    }
                }
            }

            return element;
        }

        private static string ExtractControlNameFromLeft(string leftContent)
        {
            if (string.IsNullOrEmpty(leftContent)) return string.Empty;

            string clean = leftContent.Trim();
            if (clean.StartsWith("this.")) clean = clean.Substring(5);

            if (clean.StartsWith("(("))
            {
                int closingParenthesisIdx = clean.IndexOf(')');
                if (closingParenthesisIdx != -1 && closingParenthesisIdx + 1 < clean.Length)
                {
                    clean = clean.Substring(closingParenthesisIdx + 1).TrimStart(')');
                }
            }

            if (clean.Contains("."))
            {
                var parts = clean.Split('.');
                if (parts[0] != "Controls" && parts[0] != "Location" && parts[0] != "Size" && parts[0] != "Bounds" && parts[0] != "Margin" && parts[0] != "Visible")
                {
                    clean = parts[0];
                }
            }

            if (clean.Contains("[")) clean = clean.Split('[')[0];

            return clean.Trim();
        }

        private static string[] ExtractCoordinates(string input)
        {
            if (string.IsNullOrEmpty(input)) return Array.Empty<string>();

            int start = input.IndexOf('(');
            int end = input.LastIndexOf(')');

            if (start != -1 && end != -1 && end > start)
            {
                string raw = input.Substring(start + 1, end - (start + 1));
                raw = raw.Replace("(byte)", "").Replace("(int)", "").Replace("(short)", "");
                return raw.Split(',').Select(p => p.Trim()).ToArray();
            }
            return Array.Empty<string>();
        }

        private static void RegisterRelation(Dictionary<string, List<string>> relations, string parent, string child)
        {
            if (parent == "this" || string.IsNullOrEmpty(parent) || string.IsNullOrEmpty(child)) return;

            if (!relations.ContainsKey(parent)) relations[parent] = new List<string>();
            if (!relations[parent].Contains(child)) relations[parent].Add(child);
        }
    }
}

#endif