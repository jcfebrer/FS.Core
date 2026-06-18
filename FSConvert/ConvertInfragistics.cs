#if NET47_OR_GREATER || NETCOREAPP

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FSConvert
{
    public class ConvertInfragistics
    {
        // Diccionario de equivalencias Infragistics -> .NET Estándar
        private static readonly Dictionary<string, string> TypeMapping = new Dictionary<string, string>()
        {
            { "UltraNumericEditor", "NumericUpDown" },
            { "UltraDateTimeEditor", "DateTimePicker" },
            { "UltraMaskedEdit", "MaskedTextBox" },
            { "UltraTextEditor", "TextBox" },
            { "UltraCheckEditor", "CheckBox" },
            { "UltraComboEditor", "ComboBox" },
            { "UltraCombo", "ComboBox" },
            { "UltraDataSource", "DataTable" },
            { "UltraWinDataSource", "DataTable" },
            { "UltraGrid", "DataGridView" },
            { "UltraGridRow", "DataGridViewRow" },
            { "UltraGridCell", "DataGridViewCell" },
            { "UltraGridColumn", "DataGridViewColumn" },
            { "InitializeRowEventArgs", "DataGridViewRowEventArgs" },
            { "UltraListView", "ListView" },
            { "UltraTree", "TreeView" },
            { "UltraTabControl", "TabControl" },
            { "UltraTab", "TabPage" },
            { "UltraTabPageControl", "TabPage" },
            { "UltraTabSharedControlsPage", "TabPage" },
            { "UltraWinTabControl", "TabControl" },
            { "SelectedTabChangedEventArgs", "TabControlEventArgs" },
            { "UltraGroupBox", "GroupBox" },
            { "UltraPanel", "Panel" },
            { "UltraExpandableGroupBox", "GroupBox" },
            { "UltraStatusBar", "StatusStrip" },
            { "UltraLabel", "Label" },
            { "UltraButton", "Button" },
            { "UltraToolTipManager", "ToolTip" },
            { "UltraToolTipInfo", "ToolTip" },
            { "UltraToolbarsManager", "ToolStrip" },
            { "Appearance", "InfragisticsAppearanceMarkerToRemove" }
        };

        // Diccionario unificado y ampliado según tu script: Infragistics -> FSFormControls
        private static readonly Dictionary<string, string> TypeMappingFS = new Dictionary<string, string>()
        {
            { "UltraNumericEditor", "DBTextBoxEx" },
            { "UltraDateTimeEditor", "DBDate" },
            { "UltraMaskedEdit", "DBTextBoxEx" },
            { "UltraTextEditor", "DBTextBoxEx" },
            { "UltraCheckEditor", "DBCheckBox" }, // Actualizado de DBCheckBoxEx a DBCheckBox según .scp
            { "UltraComboEditor", "DBComboEx" },
            { "UltraCombo", "DBComboEx" },
            { "UltraDataSource", "DBDataTable" },
            { "UltraWinDataSource", "DBDataTable" },
            { "UltraGrid", "DBGridView" },
            { "UltraGridRow", "DBGridViewRow" },
            { "UltraGridCell", "DBGridViewCell" },
            { "UltraGridColumn", "DBColumn" }, // Actualizado según .scp
            { "InitializeRowEventArgs", "DataGridViewRowEventArgs" },
            { "UltraListView", "DBListView" },
            { "UltraTree", "DBTreeView" },
            { "UltraWinTree", "DBTreeView" },
            { "UltraTabControl", "DBTabControl" },
            { "UltraTab", "DBTabControl" }, // Mapeado según .scp
            { "UltraTabPageControl", "DBTabPage" },
            { "UltraTabSharedControlsPage", "DBTabPageShared" },
            { "UltraWinTabControl", "DBTabControl" },
            { "SelectedTabChangedEventArgs", "TabControlEventArgs" },
            { "UltraGroupBox", "DBGroupBox" },
            { "UltraPanel", "DBPanel" },
            { "UltraExpandableGroupBox", "DBGroupBox" },
            { "UltraStatusBar", "DBStatusBar" },
            { "UltraStatusPanel", "DBStatusBarPanel" },
            { "UltraLabel", "DBLabel" },
            { "UltraButton", "DBButton" },
            { "EditorButton", "DBButton" },
            { "UltraToolTipManager", "DBTooltipManager" },
            { "UltraToolTipInfo", "DBTooltip" },
            { "UltraToolbarsManager", "DBToolBarManager" },
            { "ButtonTool", "DBToolBarButton" },
            { "UltraToolbar", "DBToolBar" },
            { "UltraProgressBar", "DBProgressBar" },
            { "UltraChart", "DBChart" },
            { "ValueListItem", "DBRadioButton" },
            { "UltraOptionSet", "DBOptionSet" },
            { "FilterCondition", "DBGridViewFilter" },
            { "Appearance", "DBAppearance" },
            { "HAlign", "DBAppearance.HAlign" },
            { "VAlign", "DBAppearance.VAlign" },
            { "TextTrimming", "DBAppearance.DBTextTrimming" },
            { "GradientAlignment", "DBAppearance.GradientAlignment" },
            { "GradientStyle", "DBAppearance.GradientStyle" },
            { "UIElementBorderStyle", "DBGridViewDisplayLayout.DBElementBorderStyle" },
            { "ViewStyleBand", "DBGridViewDisplayLayout.DBViewStyleBand" },
            { "ScrollStyle", "DBGridViewDisplayLayout.DBScrollStyle" },
            { "ScrollBounds", "DBGridViewDisplayLayout.DBScrollBounds" },
            { "RowSizing", "DBGridViewDisplayLayout.DBRowSizing" },
            { "CellClickAction", "DBGridViewDisplayLayout.DBCellClickAction" },
            { "HeaderStyle", "DBGridViewDisplayLayout.DBHeaderStyle" },
            { "HeaderClickAction", "DBGridViewDisplayLayout.DBHeaderClickAction" },
            { "UIElementEventArgs", "DBEditorButtonEventArgs" },
            { "EditorButtonEventArgs", "DBEditorButtonEventArgs" },
            { "UltraToolbarsDockArea", "DBToolBarContainer" },
            { "SummarySettings", "DBSummarie" },
            { "DropDownStyle", "ComboBoxStyle" }
        };

        public static string Convert(string sourceCode, bool useFSMapping)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            // 1. Limpiar directivas usando filtro por Namespace de Infragistics
            var cleanUsings = root.Usings.Where(u => !u.Name.ToString().StartsWith("Infragistics")).ToList();

            // 2. Inyectar System.Data y System.Windows.Forms si no existen (Requisito del .scp)
            if (!cleanUsings.Any(u => u.Name.ToString() == "System.Data"))
                cleanUsings.Insert(0, SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Data")));

            if (!cleanUsings.Any(u => u.Name.ToString() == "System.Windows.Forms"))
                cleanUsings.Insert(0, SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Windows.Forms")));

            // 3. Ejecutar el Reescritor Inteligente de Árboles de Sintaxis
            var rewriter = useFSMapping ? new WinFormsStandardRewriter(TypeMappingFS, true) : new WinFormsStandardRewriter(TypeMapping, false);
            var processedRoot = (CompilationUnitSyntax)rewriter.Visit(root);

            // 4. Inyectar FSFormControls condicionalmente si hubo cambios
            if (useFSMapping && rewriter.HasReplacements)
            {
                if (!cleanUsings.Any(u => u.Name.ToString() == "FSFormControls"))
                {
                    cleanUsings.Add(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("FSFormControls")));
                }
            }

            processedRoot = processedRoot.WithUsings(SyntaxFactory.List(cleanUsings));

            // 5. Post-procesamiento de reemplazos de texto de bajo nivel y expresiones regulares heredadas del .scp
            string finalCode = processedRoot.NormalizeWhitespace().ToFullString();
            return ApplyScriptReplacements(finalCode, useFSMapping);
        }

        private static string ApplyScriptReplacements(string code, bool useFSMapping)
        {
            if (!useFSMapping) return code;

            // Mapeos rápidos de propiedades específicas del archivo .scp
            code = code.Replace(".DisplayLayout;", ";")
                       .Replace(".DisplayLayout.Bands[0]", "")
                       .Replace(".DisplayLayout.Override", "")
                       .Replace(".SharedPropsInternal.", ".")
                       .Replace(".SharedProps.", ".")
                       .Replace(".GroupByBox.", ".")
                       .Replace(".Band.Columns", ".Columns")
                       .Replace(".Band.Index", ".Index")
                       .Replace(".Band.DataSource", ".DataSource")
                       .Replace(".Panels.", ".Items.")
                       .Replace(".Panels[", ".Items[")
                       .Replace(".Items.ValueList.FindByDataValue", ".FindByValue")
                       .Replace(".SelectedTab.Index", ".SelectedIndex")
                       .Replace(".ClickCellButton", ".CellClick")
                       .Replace(".CellChange", ".CellValueChanged")
                       .Replace(".AfterCellUpdate", ".CellEndEdit")
                       .Replace(".Tool.Key", ".Button.Name")
                       .Replace(".Tools", ".Items")
                       .Replace("ToolBase", "ToolStripItem")
                       .Replace(".AddTool", ".Add")
                       .Replace(".Tabs.AddRange", ".TabPages.AddRange")
                       .Replace(".Tabs[", ".TabPages[")
                       .Replace("DefaultableBoolean.False", "false")
                       .Replace("DefaultableBoolean.True", "true")
                       .Replace("DefaultableBoolean.Default", "true")
                       .Replace(".Nullable", ".AllowNull")
                       .Replace(".DBAppearance", ".Appearance")
                       .Replace("Nodes.Exists", "Nodes.ContainsKey");

            // Reemplazos Regex del script .scp
            code = System.Text.RegularExpressions.Regex.Replace(code, @"\.Cells\[(.*?)\]\.Text", ".Cells[$1].Value");
            code = System.Text.RegularExpressions.Regex.Replace(code, @"new DBToolBar\(.*\)\;", "new DBToolBar();");
            code = System.Text.RegularExpressions.Regex.Replace(code, @"new DBToolBarManager\(.*\)\;", "new DBToolBarManager();");

            // Regla del script: cambiar exportación de objeto Excel a método de extensión del control
            code = System.Text.RegularExpressions.Regex.Replace(code, @"excel\.Export\((.*?), (.*?)\)\;", "$1.ExportToExcel($2);");

            return code;
        }
    }

    internal class WinFormsStandardRewriter : CSharpSyntaxRewriter
    {
        private readonly Dictionary<string, string> _typeMapping;
        private readonly bool _useFSMapping;
        public bool HasReplacements { get; private set; } = false;

        public WinFormsStandardRewriter(Dictionary<string, string> typeMapping, bool useFSMapping)
        {
            _typeMapping = typeMapping;
            _useFSMapping = useFSMapping;
        }

        public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
        {
            string fullNamespace = node.ToString();
            if (fullNamespace.StartsWith("Infragistics.Win"))
            {
                string leafType = fullNamespace.Split('.').Last();
                if (_typeMapping.ContainsKey(leafType))
                {
                    if (leafType == "Appearance" && !_useFSMapping)
                    {
                        return SyntaxFactory.ParseTypeName(string.Empty);
                    }
                    HasReplacements = true;
                    return SyntaxFactory.ParseTypeName(_typeMapping[leafType]);
                }
            }
            return base.VisitQualifiedName(node);
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            string typeName = node.Identifier.Text;
            if (_typeMapping.ContainsKey(typeName))
            {
                if (typeName == "Appearance" && !_useFSMapping)
                {
                    return base.VisitIdentifierName(node);
                }
                HasReplacements = true;
                return SyntaxFactory.IdentifierName(_typeMapping[typeName]);
            }
            return base.VisitIdentifierName(node);
        }

        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            string pureTypeStr = node.Type.ToString().Split('.').Last();
            if (_typeMapping.ContainsKey(pureTypeStr))
            {
                if (pureTypeStr == "Appearance" && !_useFSMapping)
                {
                    return (ExpressionSyntax)Visit(node.Expression);
                }
                HasReplacements = true;
                var standardType = SyntaxFactory.ParseTypeName(_typeMapping[pureTypeStr]);
                return node.WithType(standardType).WithExpression((ExpressionSyntax)Visit(node.Expression));
            }
            return base.VisitCastExpression(node);
        }

        public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            string lineText = node.ToString();

            // Regla del script: Comentar o eliminar líneas conflictivas si no es conversión FS
            if (!_useFSMapping)
            {
                if (lineText.Contains(".Appearance") ||
                    lineText.Contains(".DisplayLayout") ||
                    lineText.Contains(".ButtonsRight") ||
                    lineText.Contains(".ButtonsLeft") ||
                    lineText.Contains(".Tabs") ||
                    lineText.Contains(".Toolbars"))
                {
                    return SyntaxFactory.EmptyStatement();
                }
            }
            else
            {
                // Regla 9 y 10: Comentar líneas estéticas críticas en eventos o elementos específicos
                if (lineText.Contains("e.Row.Appearance") || lineText.Contains("DBChart") || lineText.Contains(".FillAppearance"))
                {
                    // Añadimos comentario de cancelación antes de omitir la línea o mutarla
                    var leadingTrivia = node.GetLeadingTrivia().Add(SyntaxFactory.Comment("// *** BORRAR DEBIDO A COMPATIBILIDAD FS: " + lineText));
                    return SyntaxFactory.EmptyStatement().WithLeadingTrivia(leadingTrivia);
                }
            }

            return base.VisitExpressionStatement(node);
        }
    }
}
#endif