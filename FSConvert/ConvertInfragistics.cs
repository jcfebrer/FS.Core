#if NET47_OR_GREATER || NETCOREAPP

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FSLibrary;
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


            string result = code;
            // Eliminamos los usings de Infragistics.
            result = TextUtil.ReplaceREG(result, "(.*)using Infragistics.Win.UltraWinDataSource;((\n|\r)*)", "");
            result = TextUtil.ReplaceREG(result, "(.*)using Infragistics.Win.UltraWinGrid;((\n|\r)*)", "");
            result = TextUtil.ReplaceREG(result, "(.*)using Infragistics.Win;((\n|\r)*)", "");
            result = TextUtil.ReplaceREG(result, "(.*)using Infragistics.Win.UltraWinToolbars;((\n|\r)*)", "");
            result = TextUtil.ReplaceREG(result, "(.*)using Infragistics.Win.UltraWinEditors;((\n|\r)*)", "");

            result = result.Replace(".EditorButtonClick += new Infragistics.Win.UltraWinEditors.EditorButtonEventHandler", ".EditorButtonClick += new DBEditorButtonEventHandler");

            result = TextUtil.ReplaceREG(result, @"(.*cbo.*)\.MouseEnterElement \+\= new Infragistics.Win.UIElementEventHandler", "$1.MouseEnterElement += new DBComboEx.MouseEnterElementEventHandler");
            result = TextUtil.ReplaceREG(result, @"(.*txt.*)\.MouseEnterElement \+\= new Infragistics.Win.UIElementEventHandler", "$1.MouseEnterElement += new DBTextBoxEx.MouseEnterElementEventHandler");

            result = result.Replace("Infragistics.Win.EditorWithCombo", "DBComboEx");
            result = result.Replace("Infragistics.Win.UltraWinProgressBar.UltraProgressBar", "DBProgressBar");
            result = result.Replace("Infragistics.Win.UltraWinChart.UltraChart", "DBChart");
            result = result.Replace("Infragistics.UltraChart", "DBChart");

            result = result.Replace("Infragistics.Win.UltraWinGrid.FilterCondition", "DBGridViewFilter");
            result = result.Replace("Infragistics.Win.UltraWinEditors.EditorButtonEventArgs", "DBEditorButtonEventArgs");
            result = result.Replace("Infragistics.Win.UltraWinGrid.UltraGridRow", "DBGridViewRow");
            result = result.Replace("Infragistics.Win.UltraWinDataSource.UltraDataSource", "DataTable");
            result = result.Replace("Infragistics.Win.UltraWinDataSource", "DataTable");
            result = result.Replace("Infragistics.Win.UltraWinStatusBar.UltraStatusBar", "DBStatusBar");
            result = result.Replace("Infragistics.Win.UltraWinStatusBar.UltraStatusPanel", "DBStatusBarPanel");
            result = result.Replace("Infragistics.Win.UltraWinGrid.UltraGridColumn", "DBColumn");
            result = result.Replace("Infragistics.Win.UltraWinGrid.UltraGridCell", "DBGridViewCell");
            result = result.Replace("Infragistics.Win.UltraWinGrid.UltraGrid", "DBGridView");
            result = result.Replace("Infragistics.Win.UltraWinDataSource.UltraDataRow", "DataRow");
            result = result.Replace("Infragistics.Win.Misc.UltraGroupBox", "DBGroupBox");
            result = result.Replace("Infragistics.Win.UltraWinEditors.UltraButton", "DBButton");
            result = result.Replace("Infragistics.Win.Misc.UltraButton", "DBButton");
            result = result.Replace("Infragistics.Win.UltraWinEditors.EditorButton", "DBButton");
            result = result.Replace("Infragistics.Win.UltraWinEditors.UltraDateTimeEditor", "DBDate");
            result = result.Replace("Infragistics.Win.UltraWinEditors.UltraNumericEditor", "DBTextBoxEx");
            result = result.Replace("Infragistics.Win.Misc.UltraLabel", "DBLabel");
            result = result.Replace("Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit", "DBTextBoxEx");
            result = result.Replace("Infragistics.Win.UltraWinEditors.UltraTextEditor", "DBTextBoxEx");
            result = result.Replace("Infragistics.Win.UltraWinToolTip.UltraToolTipInfo", "DBTooltip");
            result = result.Replace("Infragistics.Win.UltraWinToolTip.UltraToolTipManager", "DBTooltipManager");
            result = result.Replace("Infragistics.Win.UltraWinToolbars.ButtonTool", "DBToolBarButton");
            result = result.Replace("Infragistics.Win.UltraWinToolbars.UltraToolbarsManager", "DBToolBarManager");
            result = result.Replace("Infragistics.Win.UltraWinTabControl.UltraTabControl", "DBTabControl");
            result = result.Replace("Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage", "DBTabPageShared");
            result = result.Replace("Infragistics.Win.UltraWinTabControl.UltraTabPageControl", "DBTabPage");
            result = result.Replace("Infragistics.Win.UltraWinTabControl.UltraTab", "DBTabControl");
            result = result.Replace("Infragistics.Win.UltraWinTabControl", "DBTabControl");
            result = result.Replace("Infragistics.Win.UltraWinEditors.UltraComboEditor", "DBComboEx");
            result = result.Replace("Infragistics.Win.HAlign", "DBAppearance.HAlign");
            result = result.Replace("Infragistics.Win.VAlign", "DBAppearance.VAlign");
            result = result.Replace("Infragistics.Win.Appearance", "DBAppearance");
            result = result.Replace("Infragistics.Win.UltraWinEditors.UltraCheckEditor", "DBCheckBox");
            result = result.Replace("Infragistics.Win.UltraWinEditors.NumericType", "DBTextBoxEx.NumericTypeEnum");
            result = result.Replace("Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation", "DBTextBoxEx.TabNavigationEnum");
            result = result.Replace("Infragistics.Win.UIElementBorderStyle", "DBGridViewDisplayLayout.DBElementBorderStyle");
            result = result.Replace("Infragistics.Win.DropDownStyle", "ComboBoxStyle");
            result = result.Replace("Infragistics.Win.UltraWinMaskedEdit.MaskChangedEventArgs", "EventArgs");
            result = result.Replace("Infragistics.Win.UltraWinGrid.ViewStyleBand", "DBGridViewDisplayLayout.DBViewStyleBand");
            result = result.Replace("Infragistics.Win.UltraWinGrid.ScrollStyle", "DBGridViewDisplayLayout.DBScrollStyle");
            result = result.Replace("Infragistics.Win.UltraWinGrid.ScrollBounds", "DBGridViewDisplayLayout.DBScrollBounds");
            result = result.Replace("Infragistics.Win.UltraWinGrid.RowSizing", "DBGridViewDisplayLayout.DBRowSizing");
            result = result.Replace("Infragistics.Win.UltraWinGrid.CellClickAction", "DBGridViewDisplayLayout.DBCellClickAction");
            result = result.Replace("Infragistics.Win.HeaderStyle", "DBGridViewDisplayLayout.DBHeaderStyle");
            result = result.Replace("Infragistics.Win.UltraWinGrid.HeaderClickAction", "DBGridViewDisplayLayout.DBHeaderClickAction");
            result = result.Replace("Infragistics.Win.UIElementEventArgs", "DBEditorButtonEventArgs");
            result = result.Replace("Infragistics.Win.TextTrimming", "DBAppearance.DBTextTrimming");
            result = result.Replace("Infragistics.Win.GradientAlignment", "DBAppearance.GradientAlignment");
            result = result.Replace("Infragistics.Win.GradientStyle", "DBAppearance.GradientStyle");
            result = result.Replace("Infragistics.Win.DefaultableBoolean.False", "false");
            result = result.Replace("Infragistics.Win.DefaultableBoolean.True", "true");
            result = result.Replace("Infragistics.Win.DefaultableBoolean.Default", "true");
            result = result.Replace("Infragistics.Win.UltraWinGrid.CellClickAction", "DBGridViewDisplayLayout.DBCellClickAction");
            result = result.Replace("Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea", "DBToolBarContainer");
            result = result.Replace("Infragistics.Win.UltraWinTree.UltraTreeNode", "TreeNode");
            result = result.Replace("Infragistics.Win.UltraWinTree.UltraTree", "DBTreeView");
            result = result.Replace("Infragistics.Win.UltraWinTree", "DBTreeView");
            result = result.Replace("Infragistics.Win.UltraWinGrid.ErrorEventArgs", "EventArgs");
            result = result.Replace("Infragistics.Win.UltraWinEditors.UltraOptionSet", "DBOptionSet");
            result = result.Replace("Infragistics.Win.ValueListItem", "DBRadioButton");



            result = result.Replace("DefaultableBoolean.False", "false");
            result = result.Replace("DefaultableBoolean.True", "true");
            result = result.Replace("DefaultableBoolean.Default", "true");

            // Propiedades
            result = result.Replace(".DisplayLayout;", ";");
            result = result.Replace(".DisplayLayout.Bands[0]", "");
            result = result.Replace(".DisplayLayout.Override", "");
            result = result.Replace(".BackColorAlpha = Infragistics.Win.", ".BackColorAlpha = DBAppearance.");
            result = result.Replace(".DBButtonClick +=", ".Click +=");
            result = result.Replace(".DBButtonClick -=", ".Click -=");
            result = result.Replace(".Scrollbars = System.Windows.Forms.ScrollBars", ".ScrollBars = System.Windows.Forms.ScrollBars");
            result = result.Replace(".Scrollbars = System.Windows.Forms.ScrollBars", ".ScrollBars = System.Windows.Forms.ScrollBars");
            result = result.Replace(".GroupByBox.", ".");



            result = result.Replace(".Items.ValueList.FindByDataValue", ".FindByValue");
            result = result.Replace(".ValorOriginal).DisplayText;", ".ValorOriginal.ToString()).Text;");
            result = result.Replace(".Valor).DisplayText;", ".Valor.ToString()).Text;");
            result = result.Replace(".SelectedTab.Index", ".SelectedIndex");
            result = result.Replace(".Band.Columns", ".Columns");
            result = result.Replace(".Band.Index", ".Index");
            result = result.Replace(".HeaderStyle = HeaderStyle.", ".HeaderStyle = DBHeaderStyle.");
            result = result.Replace(".RowSelectorStyle = HeaderStyle.", ".RowSelectorStyle = DBHeaderStyle.");
            result = result.Replace(".RowSelectorNumberStyle = RowSelectorNumberStyle.", ".RowSelectorNumberStyle = DBRowSelectorNumberStyle.");
            result = result.Replace(".RowSelectorHeaderStyle = RowSelectorHeaderStyle.", ".RowSelectorHeaderStyle = DBRowSelectorHeaderStyle.");
            result = result.Replace(".CellClickAction = CellClickAction.", ".CellClickAction = DBCellClickAction.");
            result = result.Replace(".HeaderClickAction = HeaderClickAction.", ".HeaderClickAction = DBHeaderClickAction.");
            result = result.Replace(".AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.", ".AllowColMoving = DBGridViewDisplayLayout.DBAllowColMoving.");
            result = result.Replace(".AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.", ".AllowColSwapping = DBGridViewDisplayLayout.DBAllowColSwapping.");
            result = result.Replace(".SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.", ".SelectTypeRow = DBGridViewDisplayLayout.SelectType.");
            result = result.Replace(".TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.", ".TabNavigation = DBGridViewDisplayLayout.DBTabNavigation.");
            result = result.Replace(".RuntimeCustomizationOptions = Infragistics.Win.UltraWinToolbars.RuntimeCustomizationOptions.", ".RuntimeCustomizationOptions = DBToolBarManager.DBRuntimeCustomizationOptions.");
            result = result.Replace(".RowSizing = RowSizing.", ".RowSizing = DBRowSizing.");
            result = result.Replace(".Scrollbars = ScrollBars.", ".ScrollBars = ScrollBars.");
            result = result.Replace(".DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.", ".Dock = DockStyle.");
            result = result.Replace("_ClickCellButton(object sender, CellEventArgs e)", "_ClickCellButton(object sender, DataGridViewCellEventArgs e)");
            result = result.Replace(".ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler", ".CellClick += new DBGridView.CellClickEventHandler");
            result = result.Replace(".Error += new Infragistics.Win.UltraWinGrid.ErrorEventHandler", ".Error += new DBGridView.ErrorEventHandler");
            result = result.Replace(".InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler", ".InitializeRow += new DataGridViewRowEventHandler");

            result = result.Replace("Infragistics.Win.UltraWinGrid.InitializeRowEventArgs", "DataGridViewRowEventArgs");
            result = result.Replace("InitializeRowEventArgs", "DataGridViewRowEventArgs");
            result = result.Replace(".Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[]", ".Items.AddRange(new DBToolBarButton[]");
            result = result.Replace(".SharedPropsInternal.", ".");
            result = result.Replace(".SharedProps.", ".");
            result = result.Replace(".ClickCellButton", ".CellClick");
            result = result.Replace(".SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.", ".SizingMode = DBStatusBarPanel.SizingModeEnum.");
            result = result.Replace(".ViewStyle = Infragistics.Win.UltraWinStatusBar.ViewStyle.", ".ViewStyle = DBStatusBar.ViewStyleEnum.");
            result = result.Replace(".ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler", ".ItemClick += new DBToolBarManager.ToolStripItemClickEventHandler");
            result = result.Replace("Infragistics.Win.UltraWinToolbars.ToolClickEventArgs", "ToolStripItemClickedEventArgs");
            result = result.Replace(".Tool.Key", ".Button.Name");
            result = result.Replace(".InsetSoft;", ".Raised;");
            result = result.Replace(".ToolClick", ".ItemClick");
            result = result.Replace(".CellChange", ".CellValueChanged");
            result = result.Replace(".AfterCellUpdate", ".CellEndEdit");
            result = result.Replace(".Band.DataSource", ".DataSource");
            result = result.Replace(@".Key == """, @".Name == """);
            result = result.Replace(".Tools", ".Items");
            result = result.Replace(".Style = ColumnStyle.", ".ColumnType = DBColumn.ColumnTypes.");
            result = result.Replace(".TextHAlign = HAlign.", ".Alignment = HorizontalAlignment.");
            result = result.Replace(".TextHAlign = DBAppearance.HAlign.", ".Alignment = HorizontalAlignment.");
            result = result.Replace("= HAlign.", "= DBAppearance.HAlign.");
            result = result.Replace("= VAlign.", "= DBAppearance.VAlign.");

            result = result.Replace(", SummaryType.", ", DBSummarie.SummarieType.");
            result = result.Replace("Infragistics.Win.UltraWinMaskedEdit.EditAsType.", "DBTextBoxEx.EditAsType.");

            // Palabras clave
            result = result.Replace("UltraToolbarsDockArea", "DBToolBarContainer");
            result = result.Replace("UltraButton", "DBButton");
            result = result.Replace("UltraToolbarsManager", "DBToolBarManager");
            result = result.Replace("UltraWinDataSource", "DBDataTable");
            result = result.Replace("UltraDataSource", "DBDataTable");
            result = result.Replace("UltraStatusBar", "DBStatusBar");
            result = result.Replace("UltraStatusPanel", "DBStatusBarPanel");
            result = result.Replace("ButtonTool", "DBToolBarButton");
            result = result.Replace("UltraGridRow", "DBGridViewRow");
            result = result.Replace("UltraGridCell", "DBGridViewCell");
            result = result.Replace("UltraGridColumn", "DBColumn");
            result = result.Replace("UltraGrid", "DBGridView");
            result = result.Replace("UltraDataRowsCollection", "DataRowCollection");
            result = result.Replace("UltraDataRow", "DataRow");
            result = result.Replace("UltraGroupBox", "DBGroupBox");
            result = result.Replace("UltraDateTimeEditor", "DBDate");
            result = result.Replace("UltraNumericEditor", "DBTextBoxEx");
            result = result.Replace("UltraLabel", "DBLabel");
            result = result.Replace("UltraListView", "DBListView");
            result = result.Replace("UltraMaskedEdit", "DBTextBoxEx");
            result = result.Replace("UltraTextEditor", "DBTextBoxEx");
            result = result.Replace("UltraToolTipInfo", "DBTooltip");
            result = result.Replace("UltraTabPageControl", "DBTabPage");

            result = result.Replace("UltraTabControl", "DBTabControl");
            result = result.Replace("UltraTabSharedControlsPage", "DBTabPageShared");
            result = result.Replace("UltraWinTabControl", "DBTabControl");
            result = result.Replace("UltraTab", "DBTabControl");
            result = result.Replace("UltraComboEditor", "DBComboEx");
            result = result.Replace("UltraCheckEditor", "DBCheckBox");
            result = result.Replace("UIElementEventArgs", "DBEditorButtonEventArgs");
            result = result.Replace("UltraToolbar", "DBToolBar");
            result = result.Replace("UltraWinTree", "DBTreeView");
            result = result.Replace("UltraTree", "DBTreeView");
            result = result.Replace("UltraToolTipManager", "DBTooltipManager");
            result = result.Replace("Infragistics.Win.UltraWinGrid", "DBGridView");
            result = result.Replace("UltraOptionSet", "DBOptionSet");
            result = result.Replace("FilterCondition", "DBGridViewFilter");



            // Expresiones regulares
            result = TextUtil.ReplaceREG(result, @"\.Campo\(""Fecha(.*?)""\)\.Valor\;", ".Campo(\"Fecha$1\").ValorDateTime;");
            result = TextUtil.ReplaceREG(result, @"\.Cells\[(.*?)\]\.Text", ".Cells[$1].Value");
            result = TextUtil.ReplaceREG(result, @"new DBToolBar\(.*\)\;", "new DBToolBar();");
            result = TextUtil.ReplaceREG(result, @"new DBToolBarManager\(.*\)\;", "new DBToolBarManager();");
            result = TextUtil.ReplaceREG(result, @"txtFecha(.*)\.Value \= (.*).Valor;", "txtFecha$1.Value = $2.ValorDateTime;");
            // Por algún motivo, esta línea genera un salto de línea LF en vez de CRLF
            result = TextUtil.ReplaceREG(result, @"(?<=new DBTooltip\([^,]+?),.*", ");");

            // Comentarios
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.BorderStyleInner.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.BorderStyleOuter.*", "// *** BORRAR $&");
            //result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.DisplayLayout.*", "// *** BORRAR $&");
            //result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.Appearance.*", "// *** BORRAR $&");
            //result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.SetCount.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.FillAppearance.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.TabPageMargins.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.TabButtonStyle.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.DockWithinContainerBaseType.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.Style = Infragistics.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.PerformAction.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.ActiveColScrollRegion.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?e.ProcessMode.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.DatosGrid.ReadOnly.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.BeforeRowFilterChanged\;.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.BeforeRowFilterDropDownPopulate\;.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.ForceSerialization.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.InitializePrintPreview.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.InitializePrint.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.BeforePrint.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.InstanceProps.*", "// *** BORRAR $&");
            //result = TextUtil.ReplaceREG(result, "^(?!\s*//).*?\.InitializeLayout.*", "// *** BORRAR $&");
            //result = TextUtil.ReplaceREG(result, "^(?!\s*//).*?\.BeforeRowFilterDropDownPopulate.*", "// *** BORRAR $&");
            //result = TextUtil.ReplaceREG(result, "^(?!\s*//).*?\.BeforeRowFilterChanged.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.BoldAsString.*", "// *** BORRAR $&");
            result = TextUtil.ReplaceREG(result, @"(.*)new Excel(.*)", "// *** BORRAR $&");

            // Ajustes
            result = result.Replace("= DBTabControlStyle.", "= DBTabControl.DBTabControlStyle.");
            result = result.Replace("= DBGridViewDisplayLayout.DBElementBorderStyle.", "= StatusBarPanelBorderStyle.");
            result = result.Replace("DBGridView.CellEventArgs", "System.Windows.Forms.DataGridViewCellEventArgs");
            result = result.Replace("DBGridView.CellClickEventArgs", "System.Windows.Forms.DataGridViewCellEventArgs");
            result = result.Replace("DataTable.DataRow", "DataRow");
            result = result.Replace("Infragistics.Win.AutoCompleteMode.SuggestAppend;", "AutoCompleteMode.SuggestAppend;");
            result = result.Replace("Infragistics.Win.UltraWinMaskedEdit.MaskSelectAllBehavior.", "DBTextBoxEx.SelectAllBehaviorEnum.");
            result = result.Replace(".Style = DBGridView.ColumnStyle.", ".ColumnType = DBColumn.ColumnTypes.");
            result = result.Replace(".ListIndex", ".Index");
            result = result.Replace(".CellMultiline", ".Multiline");
            result = result.Replace(".AfterActivate", ".AfterSelect");
            result = result.Replace("DBTreeView.AfterNodeChangedEventHandler", "DBTreeView.AfterSelectEventHandler");
            result = result.Replace("DoubleClickRowEventArgs", "DataGridViewCellEventArgs");
            result = result.Replace("SummarySettings", "DBSummarie");
            result = result.Replace("DropDownStyle.", "ComboBoxStyle.");
            // GT
            result = result.Replace("barraEstado.Items[1]", @"barraEstado.Items[""progreso""]");
            result = result.Replace(@"barraEstado.Items[""progreso""]", @"((DBStatusBarProgressPanel)barraEstado.Items[""progreso""])");
            result = result.Replace(".ProgressBarInfo", "");
            result = result.Replace(".BorderStyle = StatusBarPanelBorderStyle.", ".BorderStyle = BorderStyle.");

            result = result.Replace(".Nullable", ".AllowNull");
            result = result.Replace(".Alignment = HorizontalAlignment.", ".Alignment = System.Windows.Forms.HorizontalAlignment.");
            result = result.Replace(".Header.Caption", ".HeaderCaption");
            result = result.Replace("Nodes.Exists", "Nodes.ContainsKey");
            result = result.Replace("== SortIndicator.", "== DBColumn.SortIndicatorEnum.");
            result = result.Replace("Infragistics.Win.ValueListSortStyle.", "DBComboEx.SortStyleEnum.");
            result = result.Replace("DBGridView.SortIndicator.", "DBColumn.SortIndicatorEnum.");
            result = result.Replace("StateDBToolBarButton", "DBToolBarButton");
            result = result.Replace("DataTable.UltraDataColumnsCollection", "DataColumnCollection");
            result = result.Replace("DBGridView.ExcelExport.DBGridViewExcelExporter", "Excel");
            result = result.Replace(".RowAdding", ".TableNewRow");
            result = result.Replace("e.Button", "e.ClickedItem");
            result = result.Replace("Header.Appearance", "HeaderAppearance");
            result = result.Replace("Border3DStyle.Solid", "BorderStyle.FixedSingle");
            result = result.Replace("StatusBarPanelBorderStyle.Dotted", "BorderStyle.FixedSingle");
            result = result.Replace("ultraStatusPanel1.BorderStyle = BorderStyle.FixedSingle", "ultraStatusPanel1.BorderStyle = Border3DStyle.Raised");
            result = result.Replace("DBStatusBarPanel1.BorderStyle = BorderStyle.FixedSingle", "DBStatusBarPanel1.BorderStyle = Border3DStyle.Raised");
            result = result.Replace("BorderStyle.Solid", "BorderStyle.FixedSingle");
            result = result.Replace("BorderStyle.Raised", "Border3DStyle.Raised");
            result = result.Replace("DBGridView.Filter", "DBGridViewFilter.Filter");
            result = result.Replace(".Tabs.AddRange", ".TabPages.AddRange");
            result = result.Replace(".Tabs[", ".TabPages[");



            result = result.Replace("DBGridView.CancelablePrintEventArgs", "EventArgs");
            result = result.Replace("DBGridView.DoubleClickRowEventArgs", "DataGridViewCellEventArgs");
            result = result.Replace("DBGridView.BeforeSortChangeEventArgs", "EventArgs");
            result = result.Replace("DBGridView.BandEventArgs", "EventArgs");
            result = result.Replace("DBGridView.CancelablePrintEventArgs", "EventArgs");
            result = result.Replace("DBGridView.CancelablePrintPreviewEventArgs", "EventArgs");
            result = result.Replace("DBTreeView.NodeEventArgs", "EventArgs");
            result = result.Replace("DBGridView.CancelableCellEventArgs", "EventArgs");
            result = result.Replace("DBGridView.BeforeCellUpdateEventArgs", "EventArgs");
            result = result.Replace("DBGridView.RowEventArgs", "EventArgs");
            result = result.Replace("DBGridView.EventArgs", "EventArgs");
            result = result.Replace("DBGridView.DataGridViewCellEventArgs", "DataGridViewCellEventArgs");

            result = result.Replace("DBDataTable.CellDataUpdatingEventArgs", "EventArgs");
            result = result.Replace("DBDataTable.CellDataRequestedEventArgs", "EventArgs");
            result = result.Replace("DBDataTable.RowAddingEventArgs", "EventArgs");
            result = result.Replace("DBDataTable.RowDeletingEventArgs", "EventArgs");
            result = result.Replace("DBDataTable.DataTableNewRowEventArgs", "DataTableNewRowEventArgs");


            result = result.Replace("DBColumn.ColumnTypes.DateTime", "DBColumn.ColumnTypes.TimeColumn");
            result = result.Replace("DBColumn.ColumnTypes.Date", "DBColumn.ColumnTypes.DateColumn");
            result = result.Replace("DBColumn.ColumnTypes.Integer", "DBColumn.ColumnTypes.NumberColumn");
            result = result.Replace("DBColumn.ColumnTypes.Default", "DBColumn.ColumnTypes.TextColumn");
            result = result.Replace("DBColumn.ColumnTypes.CheckBox", "DBColumn.ColumnTypes.CheckColumn");
            result = result.Replace("DBColumn.ColumnTypes.Double", "DBColumn.ColumnTypes.MoneyColumn");
            result = result.Replace("DBColumn.ColumnTypes.Button", "DBColumn.ColumnTypes.ButtonColumn");
            result = result.Replace("DBColumn.ColumnTypes.Edit", "DBColumn.ColumnTypes.TextColumn");
            result = result.Replace("DBColumn.ColumnTypes.DropDownValidate", "DBColumn.ColumnTypes.ComboColumn");

            result = result.Replace(".DataSource.Rows.", ".Rows.");
            result = result.Replace(".Panels.", ".Items.");
            result = result.Replace(".Panels[", ".Items[");
            result = result.Replace("(var row ", "(DBGridViewRow row ");
            result = result.Replace(".CellMultiLine =", ".Multiline =");
            result = result.Replace("ToolBase", "ToolStripItem");
            result = result.Replace(".AddTool", ".Add");
            result = result.Replace("withBlock.Addbar", "new DBToolBar");

            // Cambio de funciones obsoletas (de momento no lo aplico).
            //result = ReplaceReg(result, "FuncionesInterface.IsDifferent\((.*),(.*)\)", "$1 != $2");
            //result = ReplaceReg(result, "FuncionesInterface.IsEqual\((.*),(.*)\)", "$1 == $2");
            //result = ReplaceReg(result, "FuncionesInterface.Concatenate\((.*),(.*)\)", "$1 + $2");
            //result = ReplaceReg(result, "FuncionesInterface.Multiply\((.*),(.*)\)", "$1 * $2");
            //result = ReplaceReg(result, "FuncionesInterface.Divide\((.*),(.*)\)", "$1 / $2");

            //Quitamos referencias a Excel
            //result = Replace(result, "FSExcel.Excel excel = new FSExcel.Excel();", "");
            result = TextUtil.ReplaceREG(result, @"excel.Export\((.*), (.*)\)\;", "$1.ExportToExcel($2);");
            code = result;

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