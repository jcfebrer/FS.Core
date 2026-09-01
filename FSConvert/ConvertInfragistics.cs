#if NET47_OR_GREATER || NETCOREAPP

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FSConvert
{
    /// <summary>
    // **********************************************************************************
    // ***** Script para la automatización de la conversión de Infragistics a FSFormControls
    // **********************************************************************************

    // PROCESO DE CONVERSIÓN DE INFRAGISTICS A FSFORMCONTROLS
    // 1.- Añadir referencias a FSExcel y FSFormControls
    // 2.- Comentar todo lo relacionado con LAYOUT, PRINT, PRINTPREVIEW, FILTER Y SUMMARY en todo el proyecto.
    // 3.- Cambiar excel.Export(gridFacturas, nombreFichero); por excel.Export(gridFacturas.DataControl.DataTable, nombreFichero);
    // 4.- Utilizar AtpInterface modificado por FS. Mejor cambiar solo: EitorGrid.cs, BDListado.cs, ListadoMaestro.cs, ListadoMaestro.Designer.cs, ComboBoxDatos.cs
    // REVISAR:
    // 5.- Si hay referencias a "Key", cambiar por "Name" o "FieldDB".
    // 6.- Errores de conversión, utilizar Convert.ToDecimal o cast.
    // 7.- Comentamos "ConstuirCoumnaGrid". Y Comentamos InitializeLayout, BeforeRowPrinted en Designer.cs
    // 8.- Convertir a ToolStripProgressBar las columnas para asignar Maximun y Minimun y Value.
    // 9.- Comenar todas las linesas que contengan "e.Row.Appearance"
    // 10.- Todo lo relacionado con DBChart, comentar.
    // 11.- Todo lo relacionado con el Botón MAPA, comentar.
    // NOTA: Todo lo relacionado con apariencia, se puede utilizar "DBAppearance", "Appearance" o "Style".

    //MODIFICACIONES UNA VEZ CREADO EL PROYECTO
    // Cambiar My.Resources.Resources.Validar por Image en BotonesBarraHerramientas.cs
    // Eliminar página Shared y poner título a cada TabPage. Y poner la propiedad "Text" a cada TabPage. Y eliminar Controls.AddRange(. Ejemplo:
    //          //Comentamos paged Shared 3:
    //			//this.tabTarifaConcertada.Controls.Add(this.DBTabPageShared3);
    //          this.tabTarifaConcertada.Controls.Add(this.tabDatosGenerales);
    //          this.tabTarifaConcertada.Controls.Add(this.tabPreciosConcertados);
    //          this.tabTarifaConcertada.Location = new System.Drawing.Point(8, 8);
    //          this.tabTarifaConcertada.Name = "tabTarifaConcertada";
    //          this.tabTarifaConcertada.SharedControlsPage = this.DBTabPageShared3;
    //          this.tabTarifaConcertada.Size = new System.Drawing.Size(1019, 564);
    //          this.tabTarifaConcertada.TabIndex = 1;
    //          DBTabControl1.Key = "tabDatosGenerales";
    //          DBTabControl1.TabPage = this.tabDatosGenerales;
    //          tabDatosGenerales.Text = "Datos generales";
    //          DBTabControl2.Key = "tabPreciosConcertados";
    //          DBTabControl2.TabPage = this.tabPreciosConcertados;
    //          tabPreciosConcertados.Text = "Precios concertados";
    //		    //Comentamos estas líneas:
    //          //this.tabTarifaConcertada.Controls.AddRange(new DBTabControl[] {
    //          //DBTabControl1,
    //          //DBTabControl2});
    //
    /// </summary>
    public class ConvertInfragistics
    {
        public static Dictionary<string, int> ReplacementStats { get; private set; } = new Dictionary<string, int>(StringComparer.Ordinal);

        private static readonly Dictionary<string, string> TypeMapping = new Dictionary<string, string>() {
            { "Appearance", "DBAppearance" },
            { "ButtonTool", "DBToolBarButton" },
            { "EditorButton", "DBButton" },
            { "EditorWithCombo", "DBComboEx"},
            { "FilterCondition", "DBGridViewFilter" },
            { "HAlign", "DBAppearance.HAlign" },
            { "InitializeRowEventArgs", "DataGridViewRowEventArgs" },
            { "StateButtonTool", "DBToolBarButton"},
            { "SummarySettings", "DBSummarie" },
            { "UIElementBorderStyle", "BorderStyle" },
            { "UltraButton", "DBButton" },
            { "UltraChart", "DBChart" },
            { "UltraCheckEditor", "DBCheckBox" },
            { "UltraCombo", "DBComboEx" },
            { "UltraComboEditor", "DBComboEx" },
            { "UltraDataRow", "DataRow" },
            { "UltraDataRowsCollection", "DataRowCollection"},
            { "UltraDataSource", "DataTable" },
            { "UltraDateTimeEditor", "DBDate" },
            { "UltraExpandableGroupBox", "DBGroupBox" },
            { "UltraGrid", "DBGridView" },
            { "UltraGridCell", "DBGridViewCell" },
            { "UltraGridColumn", "DBColumn" },
            { "UltraGridRow", "DBGridViewRow" },
            { "UltraGroupBox", "DBGroupBox" },
            { "UltraLabel", "DBLabel" },
            { "UltraListView", "DBListView" },
            { "UltraMaskedEdit", "DBTextBoxEx" },
            { "UltraNumericEditor", "DBTextBoxEx" },
            { "UltraOptionSet", "DBOptionSet" },
            { "UltraPanel", "DBPanel" },
            { "UltraProgressBar", "DBProgressBar" },
            { "UltraStatusBar", "DBStatusBar" },
            { "UltraStatusPanel", "DBStatusBarPanel" },
            { "UltraTab", "DBTabPage" },
            { "UltraTabControl", "DBTabControl" },
            { "UltraTabPageControl", "DBTabPage" },
            { "UltraTabSharedControlsPage", "DBTabPageShared" },
            { "UltraTextEditor", "DBTextBoxEx" },
            { "UltraToolbar", "DBToolBar" },
            { "UltraToolbarsDockArea", "DBToolBarContainer"},
            { "UltraToolbarsManager", "DBToolBarManager" },
            { "UltraToolTipInfo", "DBTooltip" },
            { "UltraToolTipManager", "DBTooltipManager" },
            { "UltraTree", "DBTreeView" },
            { "UltraTreeNode", "TreeNode" },
            { "UltraWinDataSource", "DataTable" },
            { "UltraWinGrid.ExcelExport.Excel", "Excel" },
            { "UltraWinTabControl", "DBTabControl" },
            { "UltraWinTree", "DBTreeView" },
            { "VAlign", "DBAppearance.VAlign" },
            { "ValueListItem", "DBRadioButton" }
        };

        private static readonly HashSet<string> MethodsToRemove = new HashSet<string>(StringComparer.Ordinal) { "RowFilter" };

        private static readonly Dictionary<string, string> FastLiteralMap = new Dictionary<string, string>(StringComparer.Ordinal) {
                {"Infragistics.Win.", ""},
                {".DisplayLayout;", ";"},
                {".DisplayLayout.Bands[0]", ""},
                {".DisplayLayout.Override", ""},
                {".SharedPropsInternal.", "."},
                {".SharedProps.", "."},
                {".GroupByBox.", "."},
                {".Band.Columns", ".Columns"},
                {".Band.Index", ".Index"},
                {".Band.DataSource", ".DataSource"},
                {".Panels.", ".Items."},
                {".Panels[", ".Items["},
                {".Items.ValueList.FindByDataValue", ".FindByValue"},
                {".SelectedTab.Index", ".SelectedIndex"},
                {".ClickCellButton", ".CellClick"},
                {".CellChange", ".CellValueChanged"},
                {".AfterCellUpdate", ".CellEndEdit"},
                {".Tool.Key", ".ClickedItem.Name"},
                {".Tools", ".Items"},
                {"ToolBase", "ToolStripItem"},
                {".AddTool", ".Add"},
                {".Tabs.AddRange", ".TabPages.AddRange"},
                {".Tabs[", ".TabPages["},
                {"DefaultableBoolean.False", "false"},
                {"DefaultableBoolean.True", "true"},
                {"DefaultableBoolean.Default", "true"},
                {".Nullable", ".AllowNull"},
                {".DBAppearance", ".Appearance"},
                {"Nodes.Exists", "Nodes.ContainsKey"},
                {"UIElementBorderStyle", "DBGridViewDisplayLayout.DBElementBorderStyle"},
                {"ValueListItem", "DBRadioButton"},
                {".BackColorAlpha = ", ".BackColorAlpha = DBAppearance."},
                {".DBButtonClick +=", ".Click +="},
                {".DBButtonClick -=", ".Click -="},
                {".Scrollbars = System.Windows.Forms.ScrollBars", ".ScrollBars = System.Windows.Forms.ScrollBars"},
                {".ValorOriginal).DisplayText;", ".ValorOriginal.ToString());"},
                {".Valor).DisplayText;", ".Valor.ToString());"},
                {".RowSelectorStyle = HeaderStyle.", ".RowSelectorStyle = DBHeaderStyle."},
                {".RowSelectorNumberStyle = RowSelectorNumberStyle.", ".RowSelectorNumberStyle = DBRowSelectorNumberStyle."},
                {".RowSelectorHeaderStyle = RowSelectorHeaderStyle.", ".RowSelectorHeaderStyle = DBRowSelectorHeaderStyle."},
                {".CellClickAction = CellClickAction.", ".CellClickAction = DBCellClickAction."},
                {".HeaderClickAction = HeaderClickAction.", ".HeaderClickAction = DBHeaderClickAction."},
                {".Scrollbars = ScrollBars.", ".ScrollBars = ScrollBars."},
                {"_ClickCellButton(object sender, CellEventArgs e)", "_ClickCellButton(object sender, DataGridViewCellEventArgs e)"},
                {".ClickCellButton += new UltraWinGrid.CellEventHandler", ".CellClick += new DataGridViewCellClickEventHandler"},
                {".Error += new UltraWinGrid.ErrorEventHandler", ".Error += new DBGridView.ErrorEventHandler"},
                {"UltraWinGrid.InitializeRowEventArgs", "DataGridViewRowEventArgs"},
                {"InitializeRowEventArgs", "DataGridViewRowEventArgs"},
                {".InsetSoft;", ".Raised;"},
                {".ToolClick", ".ItemClick"},
                {@".Key == """, @".Name == """},
                {".Style = ColumnStyle.", ".ColumnType = DBColumn.ColumnTypes."},
                {".TextHAlign = HAlign.", ".Alignment = HorizontalAlignment."},
                {".TextHAlign = DBAppearance.HAlign.", ".Alignment = HorizontalAlignment."},
                {", SummaryType.", ", DBSummarie.SummarieType."},
                {"UltraWinGrid.FilterCondition", "DBGridViewFilter"},
                {"UltraWinEditors.EditorButtonEventArgs", "DBEditorButtonEventArgs"},
                {"UltraWinMaskedEdit.MaskChangedEventArgs", "EventArgs"},
                {"UltraWinGrid.ViewStyleBand", "DBGridViewDisplayLayout.DBViewStyleBand"},
                {"UltraWinGrid.ScrollStyle", "DBGridViewDisplayLayout.DBScrollStyle"},
                {"UltraWinGrid.ScrollBounds", "DBGridViewDisplayLayout.DBScrollBounds"},
                {"UltraWinGrid.RowSizing", "DBGridViewDisplayLayout.DBRowSizing"},
                {"UltraWinGrid.CellClickAction", "DBGridViewDisplayLayout.DBCellClickAction"},
                {"UltraWinGrid.HeaderClickAction", "DBGridViewDisplayLayout.DBHeaderClickAction"},
                {"UIElementEventArgs", "DBEditorButtonEventArgs"},
                {"UltraWinToolbars.UltraToolbarsDockArea", "DBToolBarContainer"},
                {"UltraWinToolbars.ToolBase", "DBToolBarButton"},
                {"UltraWinGrid.ErrorEventArgs", "EventArgs"},
                {"UltraWinEditors.NumericType", "DBTextBoxEx.NumericTypeEnum"},
                {"UltraWinMaskedEdit.MaskedEditTabNavigation", "DBTextBoxEx.TabNavigationEnum"},
                {"UltraWinMaskedEdit.EditAsType.", "DBTextBoxEx.EditAsType."},
                {"DBGridView.CellEventArgs", "System.Windows.Forms.DataGridViewCellEventArgs"},
                {"DBGridView.CellClickEventArgs", "System.Windows.Forms.DataGridViewCellEventArgs"},
                {"AutoCompleteMode.SuggestAppend;", "AutoCompleteMode.SuggestAppend;"},
                {"UltraWinMaskedEdit.MaskSelectAllBehavior.", "DBTextBoxEx.SelectAllBehaviorEnum."},
                {".Style = DBGridView.ColumnStyle.", ".ColumnType = DBColumn.ColumnTypes."},
                {".ListIndex", ".Index"},
                {".CellMultiline", ".Multiline"},
                {".AfterActivate", ".AfterSelect"},
                {"DBTreeView.AfterNodeChangedEventHandler", "DBTreeView.AfterSelectEventHandler"},
                {"DoubleClickRowEventArgs", "DataGridViewCellEventArgs"},
                {"SummarySettings", "DBSummarie"},
                {"DropDownStyle.", "ComboBoxStyle."},
                {"barraEstado.Items[1]", @"barraEstado.Items[""progreso""]"},
                {@"barraEstado.Items[""progreso""]", @"((DBStatusBarProgressPanel)barraEstado.Items[""progreso""])"},
                {".ProgressBarInfo", ""},
                {".BorderStyle = StatusBarPanelBorderStyle.", ".BorderStyle = BorderStyle."},
                {".Alignment = HorizontalAlignment.", ".Alignment = System.Windows.Forms.HorizontalAlignment."},
                {".Header.Caption", ".HeaderCaption"},
                {"ValueListSortStyle.", "DBComboEx.SortStyleEnum."},
                {"DBGridView.SortIndicator.", "DBColumn.SortIndicatorEnum."},
                {"StateDBToolBarButton", "DBToolBarButton"},
                {"DataTable.UltraDataColumnsCollection", "DataColumnCollection"},
                {".RowAdding", ".TableNewRow"},
                {"e.Button", "e.ClickedItem"},
                {"Header.Appearance", "HeaderAppearance"},
                {"Border3DStyle.Solid", "BorderStyle.FixedSingle"},
                {"StatusBarPanelBorderStyle.Dotted", "BorderStyle.FixedSingle"},
                {"ultraStatusPanel1.BorderStyle = BorderStyle.FixedSingle", "ultraStatusPanel1.BorderStyle = Border3DStyle.Raised"},
                {"DBStatusBarPanel1.BorderStyle = BorderStyle.FixedSingle", "DBStatusBarPanel1.BorderStyle = Border3DStyle.Raised"},
                {"DBDataTable.CellDataUpdatingEventArgs", "EventArgs"},
                {"DBDataTable.CellDataRequestedEventArgs", "EventArgs"},
                {"DBDataTable.RowAddingEventArgs", "EventArgs"},
                {"DBDataTable.RowDeletingEventArgs", "EventArgs"},
                {"DBDataTable.DataTableNewRowEventArgs", "DataTableNewRowEventArgs"},
                {"DBColumn.ColumnTypes.DateTime", "DBColumn.ColumnTypes.TimeColumn"},
                {"DBColumn.ColumnTypes.Date", "DBColumn.ColumnTypes.DateColumn"},
                {"DBColumn.ColumnTypes.Integer", "DBColumn.ColumnTypes.NumberColumn"},
                {"DBColumn.ColumnTypes.Default", "DBColumn.ColumnTypes.TextColumn"},
                {"DBColumn.ColumnTypes.CheckBox", "DBColumn.ColumnTypes.CheckColumn"},
                {"DBColumn.ColumnTypes.Double", "DBColumn.ColumnTypes.MoneyColumn"},
                {"DBColumn.ColumnTypes.Button", "DBColumn.ColumnTypes.ButtonColumn"},
                {"DBColumn.ColumnTypes.Edit", "DBColumn.ColumnTypes.TextColumn"},
                {"DBColumn.ColumnTypes.DropDownValidate", "DBColumn.ColumnTypes.ComboColumn"},
                {".DataSource.Rows.", ".Rows."},
                {"(var row ", "(DBGridViewRow row "},
                {".CellMultiLine =", ".Multiline ="},
                {"withBlock.Addbar", "new DBToolBar"}
};

        // REGEX MAESTRO COMBINADO Y PRECOMPILADO EN MEMORIA (Para máxima velocidad)
        private static readonly Regex CombinedLiteralRegex;
        private static readonly List<Tuple<Regex, string>> CompiledRegexPatterns = new List<Tuple<Regex, string>>();

        static ConvertInfragistics()
        {
            // Compilar el super-patrón alternado ordenando las claves de mayor a menor longitud
            // Esto evita falsos positivos (ej: reemplazar ".Tabs[" antes que ".Tabs.AddRange")
            var escapedKeys = FastLiteralMap.Keys
                .OrderByDescending(k => k.Length)
                .Select(Regex.Escape);

            string combinedPattern = "(" + string.Join("|", escapedKeys) + ")";
            CombinedLiteralRegex = new Regex(combinedPattern, RegexOptions.Compiled);

            // --------------------------------------------------------------------------------------------
            // NUEVO BLOQUE: Reglas que empiezan por "=" pasadas a Regex elásticas.
            // Al usar \s*, hacemos que ignoren cualquier espacio/salto de línea que introduzca Roslyn.
            // --------------------------------------------------------------------------------------------
            AddCompiledRegex(@"=\s*HeaderStyle\b", "= DBGridViewDisplayLayout.DBHeaderStyle");
            AddCompiledRegex(@"=\s*DBTabControlStyle\.", "= DBTabControl.DBTabControlStyle.");
            AddCompiledRegex(@"=\s*DBGridViewDisplayLayout\.DBElementBorderStyle\.", "= StatusBarPanelBorderStyle.");
            AddCompiledRegex(@"=\s*TextTrimming\.", "= DBAppearance.DBTextTrimming.");
            AddCompiledRegex(@"=\s*GradientAlignment\b", "= DBAppearance.GradientAlignment");
            AddCompiledRegex(@"=\s*GradientStyle\b", "= DBAppearance.GradientStyle");
            AddCompiledRegex(@"=\s*HAlign\.", "= DBAppearance.HAlign.");
            AddCompiledRegex(@"=\s*VAlign\.", "= DBAppearance.VAlign.");
            AddCompiledRegex(@"==\s*SortIndicator\.", "== DBColumn.SortIndicatorEnum.");
            AddCompiledRegex(@"\.EditorButtonClick\s*([\+\-])=\s*new\s+(?:DBButtonEventHandler|EditorButtonEventHandler|UltraWinEditors\.EditorButtonEventHandler)", ".EditorButtonClick $1= new DBEditorButtonEventHandler");
            AddCompiledRegex(@"\b(?:UltraWinGrid|DBGridView)\.AllowColMoving\.", "DBGridViewDisplayLayout.DBAllowColMoving.");
            AddCompiledRegex(@"\b(?:UltraWinGrid|DBGridView)\.AllowColSwapping\.", "DBGridViewDisplayLayout.DBAllowColSwapping.");
            AddCompiledRegex(@"\b(?:UltraWinGrid|DBGridView)\.SelectType\.", "DBGridViewDisplayLayout.SelectType.");
            AddCompiledRegex(@"\b(?:UltraWinGrid|DBGridView)\.TabNavigation\.", "DBGridViewDisplayLayout.DBTabNavigation.");
            AddCompiledRegex(@"\b(?:UltraWinToolbars|DBToolBarManager)\.RuntimeCustomizationOptions\.", "DBToolBarManager.DBRuntimeCustomizationOptions.");
            AddCompiledRegex(@"\b(?:UltraWinGrid|DBGridView)\.RowSizing\.", "DBRowSizing.");
            AddCompiledRegex(@"\b(?:Infragistics\.Win\.UltraWinGrid\.|UltraWinGrid\.|UltraGrid\.|DBGridView\.)Filter\b", "DBGridViewFilter.Filter");
            AddCompiledRegex(@"\b(?:Infragistics\.Win\.UltraWinGrid\.|UltraWinGrid\.|UltraGrid\.|DBGridView\.)(?:CancelablePrintEventArgs|BeforeSortChangeEventArgs|BandEventArgs|CancelablePrintPreviewEventArgs|CancelableCellEventArgs|BeforeCellUpdateEventArgs|RowEventArgs|EventArgs)\b", "EventArgs");
            AddCompiledRegex(@"\b(?:Infragistics\.Win\.UltraWinGrid\.|UltraWinGrid\.|UltraGrid\.|DBGridView\.)(?:DoubleClickRowEventArgs|DataGridViewCellEventArgs)\b", "DataGridViewCellEventArgs");
            AddCompiledRegex(@"\b(?:Infragistics\.Win\.UltraWinTree\.|UltraWinTree\.|UltraTree\.|DBTreeView\.)NodeEventArgs\b", "EventArgs");
            AddCompiledRegex(@"\.SizingMode\s*=\s*(?:UltraWinStatusBar|DBStatusBar)\.PanelSizingMode\.", ".SizingMode = DBStatusBarPanel.SizingModeEnum.");
            AddCompiledRegex(@"\.DockedPosition\s*=\s*(?:UltraWinToolbars|DBToolBarManager)\.DockedPosition\.(Left|Right|Top|Bottom|Fill)", ".Dock = System.Windows.Forms.DockStyle.$1");
            AddCompiledRegex(@"\.ItemClick\s*\+=\s*new\s+(?:UltraWinToolbars|DBToolBarManager)\.ItemClickEventHandler", ".ItemClick += new DBToolBarManager.ToolStripItemClickEventHandler");
            AddCompiledRegex(@"\b(?:UltraWinToolbars\.)?ItemClickEventArgs\b", "ToolStripItemClickedEventArgs");
            AddCompiledRegex(@"\.Cells\[(.*?)\]\.Text", ".Cells[$1].Value");
            AddCompiledRegex(@"new DBToolBar\(.*\)\;", "new DBToolBar();");
            AddCompiledRegex(@"new DBToolBarManager\(.*\)\;", "new DBToolBarManager();");
            AddCompiledRegex(@"excel\.Export\((.*?), (.*?)\)\;", "$1.ExportToExcel($2);");
            AddCompiledRegex(@"(.*cbo.*)\.MouseEnterElement \+\= new UIElementEventHandler", "$1.MouseEnterElement += new DBComboEx.MouseEnterElementEventHandler");
            AddCompiledRegex(@"(.*txt.*)\.MouseEnterElement \+\= new UIElementEventHandler", "$1.MouseEnterElement += new DBTextBoxEx.MouseEnterElementEventHandler");
            AddCompiledRegex(@"\.InitializeRow\s*([\+\-])=\s*new\s+(?:UltraWinGrid|DBGridView)\.InitializeRowEventHandler", ".InitializeRow $1= new EventHandler<DataGridViewRowEventArgs>");
            AddCompiledRegex(@"\.BeforeSortChange\s*([\+\-])=\s*new\s+(?:UltraWinGrid|DBGridView)\.BeforeSortChangeEventHandler", ".BeforeSortChange $1= new EventHandler");
            AddCompiledRegex(@"\.AfterSortChange\s*([\+\-])=\s*new\s+(?:UltraWinGrid|DBGridView)\.BandEventHandler", ".AfterSortChange $1= new EventHandler");
            AddCompiledRegex(@"\.Campo\(""Fecha(.*?)""\)\.Valor\;", ".Campo(\"Fecha$1\").ValorDateTime;");
            AddCompiledRegex(@"\.Cells\[(.*?)\]\.Text", ".Cells[$1].Value");
            AddCompiledRegex(@"new DBToolBar\(.*\)\;", "new DBToolBar();");
            AddCompiledRegex(@"new DBToolBarManager\(.*\)\;", "new DBToolBarManager();");
            AddCompiledRegex(@"txtFecha(.*)\.Value \= (.*).Valor;", "txtFecha$1.Value = $2.ValorDateTime;");
            AddCompiledRegex(@"(new DBTooltip\([^;]*?)\s*,\s*(?:Infragistics\.Win\.)?ToolTipImage[^;]*?\);", "$1);");
            AddCompiledRegex(@"\bViewStyle\s*=\s*(?:UltraWinStatusBar|DBStatusBar)\.ViewStyle\.", "ViewStyle = DBStatusBar.ViewStyleEnum.");

            AddCompiledRegex(@"\bBorderStyle(?:Cell|Row)?\s*=\s*BorderStyle\.Raised\b", "BorderStyle = Border3DStyle.Raised");
            AddCompiledRegex(@"\bBorderStyle(?:Cell|Row)?\s*=\s*BorderStyle\.Solid\b", "BorderStyle = BorderStyle.FixedSingle");
            AddCompiledRegex(@"\bBorderStyle(?:Cell|Row)?\s*=\s*BorderStyle\.Dotted\b", "BorderStyle = BorderStyle.FixedSingle");

            AddCompiledRegex(@"^(?!\s*//).*?\.BorderStyleInner.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.BorderStyleOuter.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.FillAppearance.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.TabPageMargins.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.TabButtonStyle.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.DockWithinContainerBaseType.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.Style = Infragistics.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.PerformAction.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.ActiveColScrollRegion.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.ActiveRowScrollRegion.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?e.ProcessMode.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?e.AllowRowFiltering.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.DatosGrid.ReadOnly.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.BeforeRowFilterChanged\;.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.BeforeRowFilterDropDownPopulate\;.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.ForceSerialization.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.InitializePrintPreview.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.InitializePrint.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.BeforePrint.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.InstanceProps.*", "// *** BORRAR $&");
            AddCompiledRegex(@"^(?!\s*//).*?\.BoldAsString.*", "// *** BORRAR $&");
            AddCompiledRegex(@"(.*)new Excel(.*)", "// *** BORRAR $&");
            AddCompiledRegex(@"excel.Export\((.*), (.*)\)\;", "$1.ExportToExcel($2);");
            AddCompiledRegex(@"\.(?:Appearance|DBAppearance)\.ResetForeColor\s*\(\s*\)\s*;", ".ForeColor = Color.Empty;");
            AddCompiledRegex(@"([""'\]])\.(?:Appearance|DBAppearance)\.", "$1.");
            AddCompiledRegex(@"\b(?:Infragistics\.Win\.UltraWinGrid\.ExcelExport\.)?(?:UltraGridExcelExporter|DBGridViewExcelExporter)\b", "Excel");

            ResetGlobalStats();
        }

        private static void AddCompiledRegex(string pattern, string replacement)
        {
            CompiledRegexPatterns.Add(Tuple.Create(new Regex(pattern, RegexOptions.Compiled | RegexOptions.Multiline), replacement));
        }

        public static void ResetGlobalStats()
        {
            ReplacementStats.Clear();

            // Pre-cargamos todas las reglas con 0 ocurrencias
            if (TypeMapping != null)
            {
                foreach (var kvp in TypeMapping)
                {
                    ReplacementStats[$"Tipo Mapeado (AST): {kvp.Key} -> {kvp.Value}"] = 0;
                    ReplacementStats[$"Identificador Mapeado (AST): {kvp.Key} -> {kvp.Value}"] = 0;
                    ReplacementStats[$"Cast Traducido (AST): ({kvp.Key})"] = 0;
                }
            }

            if (FastLiteralMap != null)
            {
                foreach (var kvp in FastLiteralMap)
                {
                    ReplacementStats[$"Texto: [{kvp.Key}] -> [{kvp.Value}]"] = 0;
                }
            }

            if (CompiledRegexPatterns != null)
            {
                foreach (var pattern in CompiledRegexPatterns)
                {
                    ReplacementStats[$"Regex: [{pattern.Item1}] -> [{pattern.Item2}]"] = 0;
                }
            }

            if (MethodsToRemove != null)
            {
                foreach (var m in MethodsToRemove)
                {
                    ReplacementStats[$"Método Eliminado (AST): {m}"] = 0;
                }
            }
        }

        public static string GetGlobalSummaryReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("====================================================");
            sb.AppendLine("   RESUMEN GLOBAL DE REEMPLAZOS ACUMULADOS          ");
            sb.AppendLine("====================================================");

            // Filtramos y ordenamos: Aplicados (> 0) y No aplicados (== 0)
            var aplicados = ReplacementStats.Where(x => x.Value > 0).OrderByDescending(x => x.Value).ToList();
            var ceroVeces = ReplacementStats.Where(x => x.Value == 0).OrderBy(x => x.Key).ToList();

            sb.AppendLine();
            sb.AppendLine(">>> REEMPLAZOS EJECUTADOS CON ÉXITO:");
            if (aplicados.Count == 0) sb.AppendLine("    Ninguno.");
            else
            {
                foreach (var stat in aplicados)
                    sb.AppendLine($"[ {stat.Value} veces ] {stat.Key}");
            }

            sb.AppendLine();
            sb.AppendLine(">>> REEMPLAZOS NO ENCONTRADOS (0 veces):");
            if (ceroVeces.Count == 0) sb.AppendLine("    Ninguno.");
            else
            {
                foreach (var stat in ceroVeces)
                    sb.AppendLine($"[ 0 veces ] {stat.Key}");
            }

            sb.AppendLine("====================================================");
            return sb.ToString();
        }

        public static string Convert(string sourceCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            var cleanUsings = new List<UsingDirectiveSyntax>(root.Usings.Count + 3);
            bool hasData = false, hasForms = false, hasFS = false;

            foreach (var u in root.Usings)
            {
                string uName = u.Name.ToString();
                if (uName.StartsWith("Infragistics")) continue;
                if (uName == "System.Data") hasData = true;
                if (uName == "System.Windows.Forms") hasForms = true;
                if (uName == "FSFormControls") hasFS = true;
                cleanUsings.Add(u);
            }

            if (!hasForms) cleanUsings.Insert(0, SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Windows.Forms")));
            if (!hasData) cleanUsings.Insert(0, SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Data")));

            var rewriter = new WinFormsStandardRewriter(TypeMapping, MethodsToRemove);
            var processedRoot = (CompilationUnitSyntax)rewriter.Visit(root);

            if (rewriter.HasReplacements && !hasFS)
                cleanUsings.Add(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("FSFormControls")));

            processedRoot = processedRoot.WithUsings(SyntaxFactory.List(cleanUsings));
            return ExecuteUltraFastReplacements(processedRoot.NormalizeWhitespace().ToFullString());
        }

        // MOTOR CENTRALIZADO ULTRA-RÁPIDO EN UNA SOLA PASADA DE MEMORIA
        private static string ExecuteUltraFastReplacements(string code)
        {
            // 1. Resolver todas las cadenas fijas simultáneamente mapeando métricas inline
            code = CombinedLiteralRegex.Replace(code, match =>
            {
                string token = match.Value;
                if (FastLiteralMap.TryGetValue(token, out string value))
                {
                    string key = $"Texto: [{token}] -> [{value}]";
                    if (ReplacementStats.ContainsKey(key)) ReplacementStats[key]++;
                    else ReplacementStats[key] = 1;
                    return value;
                }
                return token;
            });

            // 2. Procesar los patrones Regex ya compilados nativos del procesador
            foreach (var patternTuple in CompiledRegexPatterns)
            {
                Regex regex = patternTuple.Item1;
                string replacement = patternTuple.Item2;

                int matchesCount = regex.Matches(code).Count;
                if (matchesCount > 0)
                {
                    string key = $"Regex: [{regex}] -> [{replacement}]";
                    if (ReplacementStats.ContainsKey(key)) ReplacementStats[key] += matchesCount;
                    else ReplacementStats[key] = matchesCount;

                    code = regex.Replace(code, replacement);
                }
            }

            return code;
        }
    }

    internal class WinFormsStandardRewriter : CSharpSyntaxRewriter
    {
        private readonly Dictionary<string, string> _typeMapping;
        private readonly HashSet<string> _methodsToRemove;
        public bool HasReplacements { get; private set; } = false;

        public WinFormsStandardRewriter(Dictionary<string, string> typeMapping, HashSet<string> methodsToRemove)
        {
            _typeMapping = typeMapping;
            _methodsToRemove = methodsToRemove;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            string methodName = node.Identifier.Text;

            if (_methodsToRemove != null)
            {
                bool shoulderRemove = _methodsToRemove.Any(m =>
                    !string.IsNullOrEmpty(m) && methodName.IndexOf(m, StringComparison.OrdinalIgnoreCase) >= 0
                );

                if (shoulderRemove)
                {
                    string key = $"Método Eliminado (AST): {methodName}";
                    if (ConvertInfragistics.ReplacementStats.ContainsKey(key)) ConvertInfragistics.ReplacementStats[key]++;
                    else ConvertInfragistics.ReplacementStats[key] = 1;

                    return null;
                }
            }
            return base.VisitMethodDeclaration(node);
        }

        public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
        {
            string fullNamespace = node.ToString();
            if (fullNamespace.StartsWith("Infragistics.Win"))
            {
                string leafType = fullNamespace.Split('.').Last();
                if (_typeMapping.TryGetValue(leafType, out string targetType))
                {
                    HasReplacements = true;
                    string key = $"Tipo Mapeado (AST): {leafType} -> {targetType}";
                    if (ConvertInfragistics.ReplacementStats.ContainsKey(key)) ConvertInfragistics.ReplacementStats[key]++;
                    else ConvertInfragistics.ReplacementStats[key] = 1;
                    return SyntaxFactory.ParseTypeName(targetType);
                }
            }
            return base.VisitQualifiedName(node);
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            string typeName = node.Identifier.Text;
            if (_typeMapping.TryGetValue(typeName, out string targetType))
            {
                HasReplacements = true;
                string key = $"Identificador Mapeado (AST): {typeName} -> {targetType}";
                if (ConvertInfragistics.ReplacementStats.ContainsKey(key)) ConvertInfragistics.ReplacementStats[key]++;
                else ConvertInfragistics.ReplacementStats[key] = 1;
                return SyntaxFactory.IdentifierName(targetType);
            }
            return base.VisitIdentifierName(node);
        }

        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            string pureTypeStr = node.Type.ToString().Split('.').Last();
            if (_typeMapping.TryGetValue(pureTypeStr, out string targetType))
            {
                HasReplacements = true;
                string key = $"Cast Traducido (AST): ({pureTypeStr})";
                if (ConvertInfragistics.ReplacementStats.ContainsKey(key)) ConvertInfragistics.ReplacementStats[key]++;
                else ConvertInfragistics.ReplacementStats[key] = 1;
                return node.WithType(SyntaxFactory.ParseTypeName(targetType)).WithExpression((ExpressionSyntax)Visit(node.Expression));
            }
            return base.VisitCastExpression(node);
        }
    }
}
#endif