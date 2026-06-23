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
        // Diccionario unificado y ampliado según tu script: Infragistics -> FSFormControls
        private static readonly Dictionary<string, string> TypeMapping = new Dictionary<string, string>()
        {
            { "UltraNumericEditor", "DBTextBoxEx" },
            { "UltraDateTimeEditor", "DBDate" },
            { "UltraMaskedEdit", "DBTextBoxEx" },
            { "UltraTextEditor", "DBTextBoxEx" },
            { "UltraCheckEditor", "DBCheckBox" },
            { "UltraComboEditor", "DBComboEx" },
            { "UltraCombo", "DBComboEx" },
            { "UltraDataSource", "DBDataTable" },
            { "UltraWinDataSource", "DBDataTable" },
            { "UltraGrid", "DBGridView" },
            { "UltraGridRow", "DBGridViewRow" },
            { "UltraGridCell", "DBGridViewCell" },
            { "UltraGridColumn", "DBColumn" },
            { "InitializeRowEventArgs", "DataGridViewRowEventArgs" },
            { "UltraListView", "DBListView" },
            { "UltraTree", "DBTreeView" },
            { "UltraWinTree", "DBTreeView" },
            { "UltraTabControl", "DBTabControl" },
            { "UltraTab", "DBTabPage" },
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
            { "UIElementBorderStyle", "BorderStyle" },
            { "SummarySettings", "DBSummarie" }
        };

        public static string Convert(string sourceCode)
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
            WinFormsStandardRewriter rewriter = new WinFormsStandardRewriter(TypeMapping, false);
            CompilationUnitSyntax processedRoot = (CompilationUnitSyntax)rewriter.Visit(root);

            // 4. Inyectar FSFormControls condicionalmente si hubo cambios
            if (rewriter.HasReplacements)
            {
                if (!cleanUsings.Any(u => u.Name.ToString() == "FSFormControls"))
                {
                    cleanUsings.Add(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("FSFormControls")));
                }
            }

            processedRoot = processedRoot.WithUsings(SyntaxFactory.List(cleanUsings));

            // 5. Post-procesamiento de reemplazos de texto de bajo nivel y expresiones regulares heredadas del .scp
            string finalCode = processedRoot.NormalizeWhitespace().ToFullString();
            return ApplyScriptReplacements(finalCode);
        }

        private static string ApplyScriptReplacements(string code)
        {
            // Mapeos rápidos de propiedades específicas del archivo .scp
            code = code.Replace("Infragistics.Win.", "")
                .Replace(".DisplayLayout;", ";")
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
                .Replace("Nodes.Exists", "Nodes.ContainsKey")
                .Replace("EditorWithCombo", "DBComboEx")
                .Replace("UltraWinProgressBar.UltraProgressBar", "DBProgressBar")
                .Replace("UltraWinChart.UltraChart", "DBChart")
                .Replace("Infragistics.UltraChart", "DBChart")
                .Replace("UltraWinGrid.FilterCondition", "DBGridViewFilter")
                .Replace("UltraWinEditors.EditorButtonEventArgs", "DBEditorButtonEventArgs")
                .Replace("UltraWinGrid.UltraGridRow", "DBGridViewRow")
                .Replace("UltraWinDataSource.UltraDataSource", "DataTable")
                .Replace("UltraWinDataSource", "DataTable")
                .Replace("UltraWinStatusBar.UltraStatusBar", "DBStatusBar")
                .Replace("UltraWinStatusBar.UltraStatusPanel", "DBStatusBarPanel")
                .Replace("UltraWinGrid.UltraGridColumn", "DBColumn")
                .Replace("UltraWinGrid.UltraGridCell", "DBGridViewCell")
                .Replace("UltraWinGrid.UltraGrid", "DBGridView")
                .Replace("UltraWinDataSource.UltraDataRow", "DataRow")
                .Replace("Misc.UltraGroupBox", "DBGroupBox")
                .Replace("UltraWinEditors.UltraButton", "DBButton")
                .Replace("Misc.UltraButton", "DBButton")
                .Replace("UltraWinEditors.EditorButton", "DBButton")
                .Replace("UltraWinEditors.UltraDateTimeEditor", "DBDate")
                .Replace("UltraWinEditors.UltraNumericEditor", "DBTextBoxEx")
                .Replace("Misc.UltraLabel", "DBLabel")
                .Replace("UltraWinMaskedEdit.UltraMaskedEdit", "DBTextBoxEx")
                .Replace("UltraWinEditors.UltraTextEditor", "DBTextBoxEx")
                .Replace("UltraWinToolTip.UltraToolTipInfo", "DBTooltip")
                .Replace("UltraWinToolTip.UltraToolTipManager", "DBTooltipManager")
                .Replace("UltraWinToolbars.ButtonTool", "DBToolBarButton")
                .Replace("UltraWinToolbars.UltraToolbarsManager", "DBToolBarManager")
                .Replace("UltraWinTabControl.UltraTabControl", "DBTabControl")
                .Replace("UltraWinTabControl.UltraTabSharedControlsPage", "DBTabPageShared")
                .Replace("UltraWinTabControl.UltraTabPageControl", "DBTabPage")
                .Replace("UltraWinTabControl.UltraTab", "DBTabPage")
                .Replace("UltraWinTabControl", "DBTabControl")
                .Replace("UltraWinEditors.UltraComboEditor", "DBComboEx")
                .Replace("UltraWinEditors.UltraCheckEditor", "DBCheckBox")
                .Replace("UltraWinEditors.NumericType", "DBTextBoxEx.NumericTypeEnum")
                .Replace("UltraWinMaskedEdit.MaskedEditTabNavigation", "DBTextBoxEx.TabNavigationEnum")
                .Replace("UIElementBorderStyle", "DBGridViewDisplayLayout.DBElementBorderStyle")
                .Replace("UltraWinMaskedEdit.MaskChangedEventArgs", "EventArgs")
                .Replace("UltraWinGrid.ViewStyleBand", "DBGridViewDisplayLayout.DBViewStyleBand")
                .Replace("UltraWinGrid.ScrollStyle", "DBGridViewDisplayLayout.DBScrollStyle")
                .Replace("UltraWinGrid.ScrollBounds", "DBGridViewDisplayLayout.DBScrollBounds")
                .Replace("UltraWinGrid.RowSizing", "DBGridViewDisplayLayout.DBRowSizing")
                .Replace("UltraWinGrid.CellClickAction", "DBGridViewDisplayLayout.DBCellClickAction")
                .Replace("= HeaderStyle", "= DBGridViewDisplayLayout.DBHeaderStyle")
                .Replace("UltraWinGrid.HeaderClickAction", "DBGridViewDisplayLayout.DBHeaderClickAction")
                .Replace("UIElementEventArgs", "DBEditorButtonEventArgs")
                .Replace("DefaultableBoolean.False", "false")
                .Replace("DefaultableBoolean.True", "true")
                .Replace("DefaultableBoolean.Default", "true")
                .Replace("UltraWinGrid.CellClickAction", "DBGridViewDisplayLayout.DBCellClickAction")
                .Replace("UltraWinToolbars.UltraToolbarsDockArea", "DBToolBarContainer")
                .Replace("UltraWinTree.UltraTreeNode", "TreeNode")
                .Replace("UltraWinTree.UltraTree", "DBTreeView")
                .Replace("UltraWinTree", "DBTreeView")
                .Replace("UltraWinGrid.ErrorEventArgs", "EventArgs")
                .Replace("UltraWinEditors.UltraOptionSet", "DBOptionSet")
                .Replace("ValueListItem", "DBRadioButton")
                .Replace("DefaultableBoolean.False", "false")
                .Replace("DefaultableBoolean.True", "true")
                .Replace("DefaultableBoolean.Default", "true")
                .Replace(".DisplayLayout;", ";")
                .Replace(".DisplayLayout.Bands[0]", "")
                .Replace(".DisplayLayout.Override", "")
                .Replace(".BackColorAlpha = ", ".BackColorAlpha = DBAppearance.")
                .Replace(".DBButtonClick +=", ".Click +=")
                .Replace(".DBButtonClick -=", ".Click -=")
                .Replace(".Scrollbars = System.Windows.Forms.ScrollBars", ".ScrollBars = System.Windows.Forms.ScrollBars")
                .Replace(".Scrollbars = System.Windows.Forms.ScrollBars", ".ScrollBars = System.Windows.Forms.ScrollBars")
                .Replace(".GroupByBox.", ".")
                .Replace(".Items.ValueList.FindByDataValue", ".FindByValue")
                .Replace(".ValorOriginal).DisplayText;", ".ValorOriginal.ToString());")
                .Replace(".Valor).DisplayText;", ".Valor.ToString());")
                .Replace(".SelectedTab.Index", ".SelectedIndex")
                .Replace(".Band.Columns", ".Columns")
                .Replace(".Band.Index", ".Index")
                .Replace(".HeaderStyle = HeaderStyle.", ".HeaderStyle = DBHeaderStyle.")
                .Replace(".RowSelectorStyle = HeaderStyle.", ".RowSelectorStyle = DBHeaderStyle.")
                .Replace(".RowSelectorNumberStyle = RowSelectorNumberStyle.", ".RowSelectorNumberStyle = DBRowSelectorNumberStyle.")
                .Replace(".RowSelectorHeaderStyle = RowSelectorHeaderStyle.", ".RowSelectorHeaderStyle = DBRowSelectorHeaderStyle.")
                .Replace(".CellClickAction = CellClickAction.", ".CellClickAction = DBCellClickAction.")
                .Replace(".HeaderClickAction = HeaderClickAction.", ".HeaderClickAction = DBHeaderClickAction.")
                .Replace(".AllowColMoving = UltraWinGrid.AllowColMoving.", ".AllowColMoving = DBGridViewDisplayLayout.DBAllowColMoving.")
                .Replace(".AllowColSwapping = UltraWinGrid.AllowColSwapping.", ".AllowColSwapping = DBGridViewDisplayLayout.DBAllowColSwapping.")
                .Replace(".SelectTypeRow = UltraWinGrid.SelectType.", ".SelectTypeRow = DBGridViewDisplayLayout.SelectType.")
                .Replace(".TabNavigation = UltraWinGrid.TabNavigation.", ".TabNavigation = DBGridViewDisplayLayout.DBTabNavigation.")
                .Replace(".RuntimeCustomizationOptions = UltraWinToolbars.RuntimeCustomizationOptions.", ".RuntimeCustomizationOptions = DBToolBarManager.DBRuntimeCustomizationOptions.")
                .Replace(".RowSizing = RowSizing.", ".RowSizing = DBRowSizing.")
                .Replace(".Scrollbars = ScrollBars.", ".ScrollBars = ScrollBars.")
                .Replace(".DockedPosition = UltraWinToolbars.DockedPosition.", ".Dock = DockStyle.")
                .Replace("_ClickCellButton(object sender, CellEventArgs e)", "_ClickCellButton(object sender, DataGridViewCellEventArgs e)")
                .Replace(".ClickCellButton += new UltraWinGrid.CellEventHandler", ".CellClick += new DBGridView.CellClickEventHandler")
                .Replace(".Error += new UltraWinGrid.ErrorEventHandler", ".Error += new DBGridView.ErrorEventHandler")
                .Replace(".InitializeRow += new UltraWinGrid.InitializeRowEventHandler", ".InitializeRow += new DataGridViewRowEventHandler")
                .Replace("UltraWinGrid.InitializeRowEventArgs", "DataGridViewRowEventArgs")
                .Replace("InitializeRowEventArgs", "DataGridViewRowEventArgs")
                .Replace(".Tools.AddRange(new UltraWinToolbars.ToolBase[]", ".Items.AddRange(new DBToolBarButton[]")
                .Replace(".SharedPropsInternal.", ".")
                .Replace(".SharedProps.", ".")
                .Replace(".ClickCellButton", ".CellClick")
                .Replace(".SizingMode = UltraWinStatusBar.PanelSizingMode.", ".SizingMode = DBStatusBarPanel.SizingModeEnum.")
                .Replace(".ViewStyle = UltraWinStatusBar.ViewStyle.", ".ViewStyle = DBStatusBar.ViewStyleEnum.")
                .Replace(".ToolClick += new UltraWinToolbars.ToolClickEventHandler", ".ItemClick += new DBToolBarManager.ToolStripItemClickEventHandler")
                .Replace("UltraWinToolbars.ToolClickEventArgs", "ToolStripItemClickedEventArgs")
                .Replace(".Tool.Key", ".Button.Name")
                .Replace(".InsetSoft;", ".Raised;")
                .Replace(".ToolClick", ".ItemClick")
                .Replace(".CellChange", ".CellValueChanged")
                .Replace(".AfterCellUpdate", ".CellEndEdit")
                .Replace(".Band.DataSource", ".DataSource")
                .Replace(@".Key == """, @".Name == """)
                .Replace(".Tools", ".Items")
                .Replace(".Style = ColumnStyle.", ".ColumnType = DBColumn.ColumnTypes.")
                .Replace(".TextHAlign = HAlign.", ".Alignment = HorizontalAlignment.")
                .Replace(".TextHAlign = DBAppearance.HAlign.", ".Alignment = HorizontalAlignment.")
                .Replace("= HAlign.", "= DBAppearance.HAlign.")
                .Replace("= VAlign.", "= DBAppearance.VAlign.")
                .Replace(", SummaryType.", ", DBSummarie.SummarieType.")
                .Replace("UltraWinMaskedEdit.EditAsType.", "DBTextBoxEx.EditAsType.")
                .Replace("UltraToolbarsDockArea", "DBToolBarContainer")
                .Replace("UltraButton", "DBButton")
                .Replace("UltraToolbarsManager", "DBToolBarManager")
                .Replace("UltraWinDataSource", "DBDataTable")
                .Replace("UltraDataSource", "DBDataTable")
                .Replace("UltraStatusBar", "DBStatusBar")
                .Replace("UltraStatusPanel", "DBStatusBarPanel")
                .Replace("ButtonTool", "DBToolBarButton")
                .Replace("UltraGridRow", "DBGridViewRow")
                .Replace("UltraGridCell", "DBGridViewCell")
                .Replace("UltraGridColumn", "DBColumn")
                .Replace("UltraGrid", "DBGridView")
                .Replace("UltraDataRowsCollection", "DataRowCollection")
                .Replace("UltraDataRow", "DataRow")
                .Replace("UltraGroupBox", "DBGroupBox")
                .Replace("UltraDateTimeEditor", "DBDate")
                .Replace("UltraNumericEditor", "DBTextBoxEx")
                .Replace("UltraLabel", "DBLabel")
                .Replace("UltraListView", "DBListView")
                .Replace("UltraMaskedEdit", "DBTextBoxEx")
                .Replace("UltraTextEditor", "DBTextBoxEx")
                .Replace("UltraToolTipInfo", "DBTooltip")
                .Replace("UltraTabPageControl", "DBTabPage")
                .Replace("UltraTabControl", "DBTabControl")
                .Replace("UltraTabSharedControlsPage", "DBTabPageShared")
                .Replace("UltraWinTabControl", "DBTabControl")
                .Replace("UltraTab", "DBTabPage")
                .Replace("UltraComboEditor", "DBComboEx")
                .Replace("UltraCheckEditor", "DBCheckBox")
                .Replace("UIElementEventArgs", "DBEditorButtonEventArgs")
                .Replace("UltraToolbar", "DBToolBar")
                .Replace("UltraWinTree", "DBTreeView")
                .Replace("UltraTree", "DBTreeView")
                .Replace("UltraToolTipManager", "DBTooltipManager")
                .Replace("UltraWinGrid", "DBGridView")
                .Replace("UltraOptionSet", "DBOptionSet")
                .Replace("FilterCondition", "DBGridViewFilter")
                .Replace("= DBTabControlStyle.", "= DBTabControl.DBTabControlStyle.")
                .Replace("= DBGridViewDisplayLayout.DBElementBorderStyle.", "= StatusBarPanelBorderStyle.")
                .Replace("= TextTrimming.", "= DBAppearance.DBTextTrimming.")
                .Replace("= GradientAlignment", "= DBAppearance.GradientAlignment")
                .Replace("= GradientStyle", "= DBAppearance.GradientStyle")
                .Replace("DBGridView.CellEventArgs", "System.Windows.Forms.DataGridViewCellEventArgs")
                .Replace("DBGridView.CellClickEventArgs", "System.Windows.Forms.DataGridViewCellEventArgs")
                .Replace("DBDataTable.DataRow", "DataRow")
                .Replace("DataTable.DataRow", "DataRow")
                .Replace("AutoCompleteMode.SuggestAppend;", "AutoCompleteMode.SuggestAppend;")
                .Replace("UltraWinMaskedEdit.MaskSelectAllBehavior.", "DBTextBoxEx.SelectAllBehaviorEnum.")
                .Replace(".Style = DBGridView.ColumnStyle.", ".ColumnType = DBColumn.ColumnTypes.")
                .Replace(".ListIndex", ".Index")
                .Replace(".CellMultiline", ".Multiline")
                .Replace(".AfterActivate", ".AfterSelect")
                .Replace("DBTreeView.AfterNodeChangedEventHandler", "DBTreeView.AfterSelectEventHandler")
                .Replace("DoubleClickRowEventArgs", "DataGridViewCellEventArgs")
                .Replace("SummarySettings", "DBSummarie")
                .Replace("DropDownStyle.", "ComboBoxStyle.")
                .Replace("barraEstado.Items[1]", @"barraEstado.Items[""progreso""]")
                .Replace(@"barraEstado.Items[""progreso""]", @"((DBStatusBarProgressPanel)barraEstado.Items[""progreso""])")
                .Replace(".ProgressBarInfo", "")
                .Replace(".BorderStyle = StatusBarPanelBorderStyle.", ".BorderStyle = BorderStyle.")
                .Replace(".Nullable", ".AllowNull")
                .Replace(".Alignment = HorizontalAlignment.", ".Alignment = System.Windows.Forms.HorizontalAlignment.")
                .Replace(".Header.Caption", ".HeaderCaption")
                .Replace("Nodes.Exists", "Nodes.ContainsKey")
                .Replace("== SortIndicator.", "== DBColumn.SortIndicatorEnum.")
                .Replace("ValueListSortStyle.", "DBComboEx.SortStyleEnum.")
                .Replace("DBGridView.SortIndicator.", "DBColumn.SortIndicatorEnum.")
                .Replace("StateDBToolBarButton", "DBToolBarButton")
                .Replace("DataTable.UltraDataColumnsCollection", "DataColumnCollection")
                .Replace("DBGridView.ExcelExport.DBGridViewExcelExporter", "Excel")
                .Replace(".RowAdding", ".TableNewRow")
                .Replace("e.Button", "e.ClickedItem")
                .Replace("Header.Appearance", "HeaderAppearance")
                .Replace("Border3DStyle.Solid", "BorderStyle.FixedSingle")
                .Replace("StatusBarPanelBorderStyle.Dotted", "BorderStyle.FixedSingle")
                .Replace("ultraStatusPanel1.BorderStyle = BorderStyle.FixedSingle", "ultraStatusPanel1.BorderStyle = Border3DStyle.Raised")
                .Replace("DBStatusBarPanel1.BorderStyle = BorderStyle.FixedSingle", "DBStatusBarPanel1.BorderStyle = Border3DStyle.Raised")
                .Replace("BorderStyle.Solid", "BorderStyle.FixedSingle")
                .Replace("BorderStyle.Dotted", "BorderStyle.FixedSingle")
                .Replace("BorderStyle.Raised", "Border3DStyle.Raised")
                .Replace("DBGridView.Filter", "DBGridViewFilter.Filter")
                .Replace(".Tabs.AddRange", ".TabPages.AddRange")
                .Replace(".Tabs[", ".TabPages[")
                .Replace("DBGridView.CancelablePrintEventArgs", "EventArgs")
                .Replace("DBGridView.DoubleClickRowEventArgs", "DataGridViewCellEventArgs")
                .Replace("DBGridView.BeforeSortChangeEventArgs", "EventArgs")
                .Replace("DBGridView.BandEventArgs", "EventArgs")
                .Replace("DBGridView.CancelablePrintEventArgs", "EventArgs")
                .Replace("DBGridView.CancelablePrintPreviewEventArgs", "EventArgs")
                .Replace("DBTreeView.NodeEventArgs", "EventArgs")
                .Replace("DBGridView.CancelableCellEventArgs", "EventArgs")
                .Replace("DBGridView.BeforeCellUpdateEventArgs", "EventArgs")
                .Replace("DBGridView.RowEventArgs", "EventArgs")
                .Replace("DBGridView.EventArgs", "EventArgs")
                .Replace("DBGridView.DataGridViewCellEventArgs", "DataGridViewCellEventArgs")
                .Replace("DBDataTable.CellDataUpdatingEventArgs", "EventArgs")
                .Replace("DBDataTable.CellDataRequestedEventArgs", "EventArgs")
                .Replace("DBDataTable.RowAddingEventArgs", "EventArgs")
                .Replace("DBDataTable.RowDeletingEventArgs", "EventArgs")
                .Replace("DBDataTable.DataTableNewRowEventArgs", "DataTableNewRowEventArgs")
                .Replace("DBColumn.ColumnTypes.DateTime", "DBColumn.ColumnTypes.TimeColumn")
                .Replace("DBColumn.ColumnTypes.Date", "DBColumn.ColumnTypes.DateColumn")
                .Replace("DBColumn.ColumnTypes.Integer", "DBColumn.ColumnTypes.NumberColumn")
                .Replace("DBColumn.ColumnTypes.Default", "DBColumn.ColumnTypes.TextColumn")
                .Replace("DBColumn.ColumnTypes.CheckBox", "DBColumn.ColumnTypes.CheckColumn")
                .Replace("DBColumn.ColumnTypes.Double", "DBColumn.ColumnTypes.MoneyColumn")
                .Replace("DBColumn.ColumnTypes.Button", "DBColumn.ColumnTypes.ButtonColumn")
                .Replace("DBColumn.ColumnTypes.Edit", "DBColumn.ColumnTypes.TextColumn")
                .Replace("DBColumn.ColumnTypes.DropDownValidate", "DBColumn.ColumnTypes.ComboColumn")
                .Replace(".DataSource.Rows.", ".Rows.")
                .Replace(".Panels.", ".Items.")
                .Replace(".Panels[", ".Items[")
                .Replace("(var row ", "(DBGridViewRow row ")
                .Replace(".CellMultiLine =", ".Multiline =")
                .Replace("ToolBase", "ToolStripItem")
                .Replace(".AddTool", ".Add")
                .Replace(".EditorButtonClick += new DBButtonEventHandler", ".EditorButtonClick += new DBEditorButtonEventHandler")
                .Replace("withBlock.Addbar", "new DBToolBar");

            // Reemplazos Regex del script .scp
            code = TextUtil.ReplaceREG(code, @"\.Cells\[(.*?)\]\.Text", ".Cells[$1].Value");
            code = TextUtil.ReplaceREG(code, @"new DBToolBar\(.*\)\;", "new DBToolBar();");
            code = TextUtil.ReplaceREG(code, @"new DBToolBarManager\(.*\)\;", "new DBToolBarManager();");

            // Regla del script: cambiar exportación de objeto Excel a método de extensión del control
            code = TextUtil.ReplaceREG(code, @"excel\.Export\((.*?), (.*?)\)\;", "$1.ExportToExcel($2);");

            string result = code;
            
            result = TextUtil.ReplaceREG(result, @"(.*cbo.*)\.MouseEnterElement \+\= new UIElementEventHandler", "$1.MouseEnterElement += new DBComboEx.MouseEnterElementEventHandler");
            result = TextUtil.ReplaceREG(result, @"(.*txt.*)\.MouseEnterElement \+\= new UIElementEventHandler", "$1.MouseEnterElement += new DBTextBoxEx.MouseEnterElementEventHandler");

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
            result = TextUtil.ReplaceREG(result, @"^(?!\s*//).*?\.ActiveRowScrollRegion.*", "// *** BORRAR $&");
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
                HasReplacements = true;
                var standardType = SyntaxFactory.ParseTypeName(_typeMapping[pureTypeStr]);
                return node.WithType(standardType).WithExpression((ExpressionSyntax)Visit(node.Expression));
            }
            return base.VisitCastExpression(node);
        }

        public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            return base.VisitExpressionStatement(node);
        }
    }
}
#endif