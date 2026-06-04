#if NET47_OR_GREATER || NETCOREAPP

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FSConvert
{
    public class ConvertToWPF_Test
    {
        private static bool setPosition = true;
        private static bool setMargin = true;
        private static bool setSize = true;
        private static bool setAnchor = true;
        private static bool setDock = true;

        public static string Convert(string winFormsCode)
        {
            Console.WriteLine("====== [INICIO] CONVERSIÓN CON POSICIONAMIENTO PRECISO ======");
            var detectedControls = new Dictionary<string, string>();

            var instantiationPatterns = new Dictionary<string, string>()
            {
                { @"(?:Button|DBButtonEx|DBButton)\b", "Button" },
                { @"(?:Label|DBLabelEx|DBLabel)\b", "Label" },
                { @"(?:TextBox|DBTextBoxEx|DBTextBox)\b", "TextBox" },
                { @"(?:DatePicker|Date|DBDatePicker|DBDate)\b", "DatePicker" },
                { @"(?:CheckBox|DBCheckBoxEx|DBCheckBox)\b", "CheckBox" },
                { @"(?:RadioButton|DBRadioButtonEx|DBRadioButton)\b", "RadioButton" },
                { @"(?:ComboBox|ComboEx|DBComboBox|DBComboEx)\b", "ComboBox" },
                { @"(?:ListBox|DBListBox)\b", "ListBox" },
                { @"(?:DataGridView|GridView|Grid|DataGridViewEx|GridEx|DBGrid|DBGridEx|DBGridViewEx)\b", "DataGridView" },
                { @"(?:PictureBox|DBPictureBox)\b", "PictureBox" },
                { @"(?:Panel|PanelEx|DBPanel|DBPanelEx)\b", "Panel" },
                { @"(?:GroupBox|DBGroupBox)\b", "GroupBox" },
                { @"(?:TabControl|DBTabControl)\b", "TabControl" },
                { @"(?:TabPage|DBTabPage)\b", "TabPage" },
                { @"(?:FlowLayoutPanel|DBFlowLayoutPanel)\b", "FlowLayoutPanel" }
            };

            // Soporte total para Ñ, acentos y caracteres especiales de codificación
            string idPattern = @"[a-zA-Z0-9_ñÑáéíóúÁÉÍÓÚüÜ\uFFFDÃ±]+";

            // 1. Detección de Controles (Soporta constructores con parámetros)
            foreach (var kvp in instantiationPatterns)
            {
                string regexPattern = @"(?:this\.)?(?<name>" + idPattern + @")\s*=\s*new\s*(?:global::)?(?:[a-zA-Z0-9_:]+\.)*" + kvp.Key + @"\s*\((?<args>[^\)]*)\);";
                var matches = Regex.Matches(winFormsCode, regexPattern);
                foreach (Match match in matches)
                {
                    string name = match.Groups["name"].Value;
                    if (!detectedControls.ContainsKey(name))
                    {
                        detectedControls[name] = kvp.Value;
                    }
                }
            }

            var propTexts = new Dictionary<string, string>();
            var propLocations = new Dictionary<string, (string x, string y)>();
            var propSizes = new Dictionary<string, (string w, string h)>();
            var propAnchors = new Dictionary<string, string>();
            var propDocks = new Dictionary<string, string>();
            var propEvents = new Dictionary<string, string>();

            // 2. Extracción Ultra-Tolerante a Espacios y Saltos de Línea
            var textMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(" + idPattern + @")\.(?:Text|Content|HeaderText|Caption)\s*=\s*""(?<val>[^""]*)""\s*;", RegexOptions.Multiline);
            foreach (Match m in textMatches) propTexts[m.Groups[1].Value] = m.Groups["val"].Value;

            // Regex de Ubicación (Point) flexible
            var locMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(?<name>" + idPattern + @")\.Location\s*=\s*new\s*(?:global::)?(?:System\.Drawing\.)?Point\s*\(\s*(?<x>-?\d+)\s*,\s*(?<y>-?\d+)\s*\)\s*;", RegexOptions.Multiline);
            foreach (Match m in locMatches) propLocations[m.Groups["name"].Value] = (m.Groups["x"].Value.Trim(), m.Groups["y"].Value.Trim());

            // Regex de Tamaño (Size) flexible
            var sizeMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(?<name>" + idPattern + @")\.Size\s*=\s*new\s*(?:global::)?(?:System\.Drawing\.)?Size\s*\(\s*(?<w>\d+)\s*,\s*(?<h>\d+)\s*\)\s*;", RegexOptions.Multiline);
            foreach (Match m in sizeMatches) propSizes[m.Groups["name"].Value] = (m.Groups["w"].Value.Trim(), m.Groups["h"].Value.Trim());

            // Captura de propiedades individuales (.Left, .Top, .Width, .Height) por si el Designer las separó
            var leftMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(?<name>" + idPattern + @")\.Left\s*=\s*(?<val>-?\d+)\s*;", RegexOptions.Multiline);
            var topMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(?<name>" + idPattern + @")\.Top\s*=\s*(?<val>-?\d+)\s*;", RegexOptions.Multiline);
            var widthMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(?<name>" + idPattern + @")\.Width\s*=\s*(?<val>\d+)\s*;", RegexOptions.Multiline);
            var heightMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(?<name>" + idPattern + @")\.Height\s*=\s*(?<val>\d+)\s*;", RegexOptions.Multiline);

            foreach (Match m in leftMatches) {
                string n = m.Groups["name"].Value; string x = m.Groups["val"].Value;
                string y = propLocations.ContainsKey(n) ? propLocations[n].y : "0"; propLocations[n] = (x, y);
            }
            foreach (Match m in topMatches) {
                string n = m.Groups["name"].Value; string y = m.Groups["val"].Value;
                string x = propLocations.ContainsKey(n) ? propLocations[n].x : "0"; propLocations[n] = (x, y);
            }
            foreach (Match m in widthMatches) {
                string n = m.Groups["name"].Value; string w = m.Groups["val"].Value;
                string h = propSizes.ContainsKey(n) ? propSizes[n].h : "0"; propSizes[n] = (w, h);
            }
            foreach (Match m in heightMatches) {
                string n = m.Groups["name"].Value; string h = m.Groups["val"].Value;
                string w = propSizes.ContainsKey(n) ? propSizes[n].w : "0"; propSizes[n] = (w, h);
            }

            var anchorMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(" + idPattern + @")\.Anchor\s*=\s*(?:.*?\bAnchorStyles\.)([a-zA-Z0-9_|, ]+);", RegexOptions.Multiline);
            foreach (Match m in anchorMatches) propAnchors[m.Groups[1].Value] = m.Groups[2].Value;

            var dockMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(" + idPattern + @")\.Dock\s*=\s*(?:.*?\bDockStyle\.)(\w+);", RegexOptions.Multiline);
            foreach (Match m in dockMatches) propDocks[m.Groups[1].Value] = m.Groups[2].Value;

            var eventMatches = Regex.Matches(winFormsCode, @"(?:this\.)?(" + idPattern + @")\.(?:Click|CheckedChanged|Leave|Enter|ValueChanged)\s*\+=\s*new\s*(?:global::)?System\.EventHandler\((?:this\.)?(" + idPattern + @")\);", RegexOptions.Multiline);
            foreach (Match m in eventMatches) propEvents[m.Groups[1].Value] = m.Groups[2].Value;

            var controlXamlMapping = new Dictionary<string, (string Xaml, string ControlType)>();

            // 3. Construcción del Bloque de Atributos del Control
            foreach (var controlPair in detectedControls)
            {
                string name = controlPair.Key;
                string type = controlPair.Value;
                
                List<string> attrs = new List<string>();
                attrs.Add($"x:Name='{name}'");

                // Textos / Contenidos
                string textValue = propTexts.ContainsKey(name) ? propTexts[name] : null;
                if (!string.IsNullOrEmpty(textValue))
                {
                    if (type == "TextBox" || type == "DatePicker" || type == "ComboBox")
                        attrs.Add($"Text='{textValue}'");
                    else if (type == "GroupBox" || type == "TabPage")
                        attrs.Add($"Header='{textValue}'");
                    else
                        attrs.Add($"Content='{textValue}'");
                }

                if (type == "Label" || type == "DatePicker")
                {
                    attrs.Add("Padding='0'");
                }

                // Alineaciones por defecto
                string hAlign = "Left";
                string vAlign = "Top";

                if (setAnchor && propAnchors.ContainsKey(name))
                {
                    string anchor = propAnchors[name];
                    bool isLeft = anchor.Contains("Left");
                    bool isRight = anchor.Contains("Right");
                    bool isTop = anchor.Contains("Top");
                    bool isBottom = anchor.Contains("Bottom");

                    if (!isLeft && !isRight && !isTop && !isBottom) { isLeft = true; isTop = true; }
                    hAlign = (isLeft && isRight) ? "Stretch" : isRight ? "Right" : "Left";
                    vAlign = (isTop && isBottom) ? "Stretch" : isBottom ? "Bottom" : "Top";
                }

                bool isDockFill = propDocks.ContainsKey(name) && propDocks[name] == "Fill";

                if (setDock && propDocks.ContainsKey(name) && !isDockFill)
                {
                    string dock = propDocks[name];
                    if (dock != "None") attrs.Add($"DockPanel.Dock='{dock}'");
                }

                if (isDockFill)
                {
                    hAlign = "Stretch";
                    vAlign = "Stretch";
                }
                else
                {
                    // Aplicar Márgenes y Dimensiones Reales si no es un Dock=Fill
                    if (setPosition)
                    {
                        attrs.Add($"HorizontalAlignment='{hAlign}'");
                        attrs.Add($"VerticalAlignment='{vAlign}'");
                    }
                    if (setMargin && propLocations.ContainsKey(name))
                    {
                        var loc = propLocations[name];
                        attrs.Add($"Margin='{loc.x},{loc.y},0,0'");
                    }
                    if (setSize && propSizes.ContainsKey(name))
                    {
                        var sz = propSizes[name];
                        if (sz.w != "0" || sz.h != "0")
                        {
                            attrs.Add($"Width='{sz.w}' Height='{sz.h}'");
                        }
                    }
                }

                string attrString = string.Join(" ", attrs);
                string xaml = "";

                switch (type)
                {
                    case "Button":
                        string btnEvent = propEvents.ContainsKey(name) ? $" Click='{propEvents[name]}'" : "";
                        xaml = $"<fs:Button {attrString}{btnEvent} />";
                        break;
                    case "Label":
                        xaml = $"<fs:Label {attrString} />";
                        break;
                    case "TextBox":
                        xaml = $"<fs:TextBox {attrString} />";
                        break;
                    case "DatePicker":
                        xaml = $"<fs:DatePicker {attrString} />";
                        break;
                    case "CheckBox":
                        string chkEvent = propEvents.ContainsKey(name) ? $" Checked='{propEvents[name]}'" : "";
                        xaml = $"<fs:CheckBox {attrString}{chkEvent} />";
                        break;
                    case "RadioButton":
                        xaml = $"<fs:RadioButton {attrString} />";
                        break;
                    case "ComboBox":
                        xaml = $"<fs:ComboBox {attrString} />";
                        break;
                    case "ListBox":
                        xaml = $"<fs:ListBox {attrString} />";
                        break;
                    case "DataGridView":
                        xaml = $"<fs:DataGrid {attrString}>\n</fs:DataGrid>";
                        break;
                    case "PictureBox":
                        xaml = $"<fs:Image {attrString} />";
                        break;
                    case "Panel":
                        xaml = $"<Grid {attrString}>\nRESERVED_CHILDREN_TOKEN\n</Grid>";
                        break;
                    case "GroupBox":
                        xaml = $"<fs:GroupBox {attrString}>\n  <Grid>\nRESERVED_CHILDREN_TOKEN  </Grid>\n</fs:GroupBox>";
                        break;
                    case "TabControl":
                        xaml = $"<TabControl {attrString}>\nRESERVED_CHILDREN_TOKEN</TabControl>";
                        break;
                    case "TabPage":
                        xaml = $"<TabItem {attrString}>\n  <Grid>\nRESERVED_CHILDREN_TOKEN  </Grid>\n</TabItem>";
                        break;
                    case "FlowLayoutPanel":
                        xaml = $"<WrapPanel {attrString}>\nRESERVED_CHILDREN_TOKEN\n</WrapPanel>";
                        break;
                }
                
                controlXamlMapping[name] = (xaml, type);
            }

            // 4. Mapeo de la estructura jerárquica
            var containerMapping = new Dictionary<string, List<string>>();
            var childControls = new HashSet<string>();

            var singleAddRegex = new Regex(@"(?:this\.)?(?:(?<container>" + idPattern + @")\.)?(?:Controls|TabPages|Tabs)\.Add\(\s*(?:this\.)?(?<child>" + idPattern + @")\s*\);", RegexOptions.Multiline);
            foreach (Match m in singleAddRegex.Matches(winFormsCode))
            {
                string container = m.Groups["container"].Success && !string.IsNullOrEmpty(m.Groups["container"].Value) ? m.Groups["container"].Value : "root_window";
                string child = m.Groups["child"].Value;

                if (!containerMapping.ContainsKey(container)) containerMapping[container] = new List<string>();
                if (!containerMapping[container].Contains(child)) containerMapping[container].Add(child);
                childControls.Add(child);
            }

            var addRangeRegex = new Regex(@"(?:this\.)?(?:(?<container>" + idPattern + @")\.)?(?:Controls|TabPages|Tabs)\.AddRange\(new\s*(?:global::)?(?:[a-zA-Z0-9_\.]+)\s*\[\]\s*\{(?<children>[^\}]+)\}\);", RegexOptions.Multiline);
            foreach (Match m in addRangeRegex.Matches(winFormsCode))
            {
                string container = m.Groups["container"].Success && !string.IsNullOrEmpty(m.Groups["container"].Value) ? m.Groups["container"].Value : "root_window";
                string childrenRaw = m.Groups["children"].Value;

                string[] children = childrenRaw.Split(',');
                foreach (var childRaw in children)
                {
                    string child = Regex.Replace(childRaw.Trim(), @"^(?:this\.|global::)", "").Trim();
                    if (!string.IsNullOrEmpty(child))
                    {
                        if (!containerMapping.ContainsKey(container)) containerMapping[container] = new List<string>();
                        if (!containerMapping[container].Contains(child)) containerMapping[container].Add(child);
                        childControls.Add(child);
                    }
                }
            }

            // Sistema de rescate de huérfanos (Ghost Orphan Rescue)
            var brokenContainers = new List<string>();
            foreach (var parent in containerMapping.Keys)
            {
                if (parent != "root_window" && !controlXamlMapping.ContainsKey(parent))
                    brokenContainers.Add(parent);
            }

            if (!containerMapping.ContainsKey("root_window"))
                containerMapping["root_window"] = new List<string>();

            foreach (var deadParent in brokenContainers)
            {
                foreach (var orphanedChild in containerMapping[deadParent])
                {
                    if (!containerMapping["root_window"].Contains(orphanedChild))
                        containerMapping["root_window"].Add(orphanedChild);
                }
            }

            foreach (var controlName in controlXamlMapping.Keys)
            {
                if (!childControls.Contains(controlName) && controlName != "root_window")
                {
                    if (!containerMapping["root_window"].Contains(controlName))
                        containerMapping["root_window"].Add(controlName);
                }
            }

            // 5. Ensamblado Estructurado Final
            var titleMatch = Regex.Match(winFormsCode, @"this\.Text\s*=\s*""(?<title>[^""]+)"";");
            string windowTitle = titleMatch.Success ? titleMatch.Groups["title"].Value : "Mantenimiento de servicios";

            var sizeMatch = Regex.Match(winFormsCode, @"this\.ClientSize\s*=\s*new\s*(?:global::)?System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\);");
            string windowWidth = sizeMatch.Success ? sizeMatch.Groups["width"].Value : "800";
            string windowHeight = sizeMatch.Success ? sizeMatch.Groups["height"].Value : "450";

            var finalXamlBuilder = new StringBuilder();
            finalXamlBuilder.AppendLine($"<Window x:Class=\"FSAppLauncher.MVVM.View.SubView.Window1\"\n" +
                $"        xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\n" +
                $"        xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\n" +
                $"        xmlns:fs=\"clr-namespace:FSFormControls.UI;assembly=FSFormControls.UI\"\n" +
                $"        Title=\"{windowTitle}\" Height=\"{windowHeight}\" Width=\"{windowWidth}\">");
            finalXamlBuilder.AppendLine("    <Grid>");

            foreach (string topControl in containerMapping["root_window"])
            {
                string controlContent = BuildXamlForControl(controlXamlMapping, containerMapping, topControl);
                if (!string.IsNullOrWhiteSpace(controlContent))
                {
                    finalXamlBuilder.AppendLine("        " + controlContent);
                }
            }

            finalXamlBuilder.AppendLine("    </Grid>");
            finalXamlBuilder.AppendLine("</Window>");

            Console.WriteLine("====== [FIN] PROCESADO CON ÉXITO ======\n");
            return finalXamlBuilder.ToString();
        }

        static string BuildXamlForControl(Dictionary<string, (string Xaml, string ControlType)> controlXamlMapping, Dictionary<string, List<string>> containerMapping, string controlName)
        {
            if (!controlXamlMapping.ContainsKey(controlName))
                return "";

            var (xamlBase, controlType) = controlXamlMapping[controlName];
            string childrenXaml = "";
            
            if (containerMapping.ContainsKey(controlName))
            {
                foreach (string child in containerMapping[controlName])
                {
                    string parsedChild = BuildXamlForControl(controlXamlMapping, containerMapping, child);
                    if (!string.IsNullOrWhiteSpace(parsedChild))
                    {
                        childrenXaml += parsedChild + "\n        ";
                    }
                }
                childrenXaml = childrenXaml.TrimEnd('\n', ' ');
            }

            if (xamlBase.Contains("RESERVED_CHILDREN_TOKEN"))
            {
                return xamlBase.Replace("RESERVED_CHILDREN_TOKEN", "    " + childrenXaml).TrimEnd('\n');
            }
            else
            {
                return xamlBase;
            }
        }
    }
}

#endif