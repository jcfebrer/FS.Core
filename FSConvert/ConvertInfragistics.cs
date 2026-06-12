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
        // LISTA AMPLIADA: Diccionario completo de equivalencias Infragistics -> .NET Estándar
        private static readonly Dictionary<string, string> TypeMapping = new Dictionary<string, string>()
        {
            // Editores de datos e inputs básicos
            { "UltraNumericEditor", "NumericUpDown" },
            { "UltraDateTimeEditor", "DateTimePicker" },
            { "UltraMaskedEdit", "MaskedTextBox" },
            { "UltraTextEditor", "TextBox" },
            { "UltraCheckEditor", "CheckBox" },
            { "UltraComboEditor", "ComboBox" },
            { "UltraCombo", "ComboBox" },
            { "UltraDataSource", "DataTable" },
            { "UltraWinDataSource", "DataTable" },

            // Grids, Listas y elementos jerárquicos de datos
            { "UltraGrid", "DataGridView" },
            { "UltraGridRow", "DataGridViewRow" },
            { "UltraGridCell", "DataGridViewCell" },
            { "InitializeRowEventArgs", "DataGridViewRowEventArgs" },
            { "UltraListView", "ListView" },
            { "UltraTree", "TreeView" },

            // Contenedores complejos, Navegación y Layout
            { "UltraTabControl", "TabControl" },
            { "UltraTab", "TabPage" },
            { "UltraTabPageControl", "TabPage" },
            { "UltraTabSharedControlsPage", "TabPage" },
            { "UltraWinTabControl", "TabControl" },
            { "SelectedTabChangedEventArgs", "TabControlEventArgs" },
            { "UltraGroupBox", "GroupBox" },
            { "UltraPanel", "Panel" },
            { "UltraExpandableGroupBox", "GroupBox" },

            // Componentes de interfaz, Barras y ToolTips
            { "UltraStatusBar", "StatusStrip" },
            { "UltraLabel", "Label" },
            { "UltraButton", "Button" },
            { "UltraToolTipManager", "ToolTip" },
            { "UltraToolTipInfo", "ToolTip" },
            { "UltraToolbarsManager", "ToolStrip" }
        };

        // LISTA AMPLIADA: Diccionario completo de equivalencias Infragistics -> FSFormControls
        private static readonly Dictionary<string, string> TypeMappingFS = new Dictionary<string, string>()
        {
            // Editores de datos e inputs básicos
            { "UltraNumericEditor", "DBTextBoxEx" },
            { "UltraDateTimeEditor", "DBDate" },
            { "UltraMaskedEdit", "DBTextBoxEx" },
            { "UltraTextEditor", "DBTextBoxEx" },
            { "UltraCheckEditor", "DBCheckBoxEx" },
            { "UltraComboEditor", "DBComboEx" },
            { "UltraCombo", "DBComboEx" },
            { "UltraDataSource", "DBDataTable" },
            { "UltraWinDataSource", "DBDataTable" },

            // Grids, Listas y elementos jerárquicos de datos
            { "UltraGrid", "DBGridView" },
            { "UltraGridRow", "DBGridViewRow" },
            { "UltraGridCell", "DBGridViewCell" },
            { "InitializeRowEventArgs", "DataGridViewRowEventArgs" },
            { "UltraListView", "DBListView" },
            { "UltraTree", "DBTreeView" },

            // Contenedores complejos, Navegación y Layout
            { "UltraTabControl", "DBTabControl" },
            { "UltraTab", "DBTabPage" },
            { "UltraTabPageControl", "DBTabPage" },
            { "UltraTabSharedControlsPage", "DBTabPageShared" },
            { "UltraWinTabControl", "DBTabControl" },
            { "SelectedTabChangedEventArgs", "TabControlEventArgs" },
            { "UltraGroupBox", "DBGroupBox" },
            { "UltraPanel", "DBPanel" },
            { "UltraExpandableGroupBox", "DBGroupBox" },

            // Componentes de interfaz, Barras y ToolTips
            { "UltraStatusBar", "DBStatusStrip" },
            { "UltraLabel", "DBLabel" },
            { "UltraButton", "DBButton" },
            { "UltraToolTipManager", "DBToolTip" },
            { "UltraToolTipInfo", "DBToolTip" },
            { "UltraToolbarsManager", "DBToolStrip" }
        };

        public static string Convert(string sourceCode, bool useFSMapping)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            // 1. Limpiar las directivas using de Infragistics
            var cleanUsings = root.Usings.Where(u => !u.Name.ToString().StartsWith("Infragistics")).ToList();

            // 2. Ejecutar el reescritor inteligente de tipos, castings y propiedades
            var rewriter = (useFSMapping) ? new WinFormsStandardRewriter(TypeMappingFS) : new WinFormsStandardRewriter(TypeMapping);
            var processedRoot = (CompilationUnitSyntax)rewriter.Visit(root);

            // 3. Reasignar los usings limpios
            processedRoot = processedRoot.WithUsings(SyntaxFactory.List(cleanUsings));

            return processedRoot.NormalizeWhitespace().ToFullString();
        }
    }

    internal class WinFormsStandardRewriter : CSharpSyntaxRewriter
    {
        private readonly Dictionary<string, string> _typeMapping;

        public WinFormsStandardRewriter(Dictionary<string, string> typeMapping)
        {
            _typeMapping = typeMapping;
        }

        // 1. Reescribir namespaces calificados compuestos (ej: Infragistics.Win.UltraWinGrid.UltraGrid -> DataGridView)
        public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
        {
            string fullNamespace = node.ToString();

            if (fullNamespace.StartsWith("Infragistics.Win"))
            {
                string leafType = fullNamespace.Split('.').Last();
                if (_typeMapping.ContainsKey(leafType))
                {
                    return SyntaxFactory.ParseTypeName(_typeMapping[leafType]);
                }
            }
            return base.VisitQualifiedName(node);
        }

        // 2. Reescribir identificadores de tipos simples (ej: UltraGrid -> DataGridView)
        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            string typeName = node.Identifier.Text;
            if (_typeMapping.ContainsKey(typeName))
            {
                return SyntaxFactory.IdentifierName(_typeMapping[typeName]);
            }
            return base.VisitIdentifierName(node);
        }

        // 3. Transformar castings explícitos (ej: (UltraComboEditor)objeto -> (ComboBox)objeto)
        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            string pureTypeStr = node.Type.ToString().Split('.').Last();

            if (_typeMapping.ContainsKey(pureTypeStr))
            {
                var standardType = SyntaxFactory.ParseTypeName(_typeMapping[pureTypeStr]);
                return node.WithType(standardType).WithExpression((ExpressionSyntax)Visit(node.Expression));
            }
            return base.VisitCastExpression(node);
        }

        // 4. TRADUCTOR INTELIGENTE DE PROPIEDADES INCOMPATIBLES
        // Captura asignaciones específicas presentes en 'MantenimientoServicios.cs' y las normaliza a .NET estándar
        public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            //string lineText = node.ToString();

            //// CASO 4.1: Purgar por completo infraestructuras propietarias de Infragistics que no compilarían en .NET estándar
            //if (lineText.Contains(".DisplayLayout") ||
            //    lineText.Contains(".ButtonsRight") ||
            //    lineText.Contains(".ButtonsLeft") ||
            //    lineText.Contains(".Tabs") ||
            //    lineText.Contains(".Toolbars") ||
            //    lineText.Contains("IsItemInList()") ||
            //    lineText.Contains("editorGrid") ||
            //    lineText.Contains(".Appearance"))
            //{
            //    // Devolvemos un statement vacío para eliminar de forma segura estas configuraciones estéticas o layouts especiales
            //    return SyntaxFactory.EmptyStatement();
            //}

            //// CASO 4.2: Traducir accesos a datos específicos de Infragistics (.ValorBool o .ValorDecimal) a nativos de .NET
            //// Tu código usa mucho ".Campo(...).ValorBool" o ".Value" directo para CheckBox
            //if (node.Expression is AssignmentExpressionSyntax assignment)
            //{
            //    string leftSide = assignment.Left.ToString();
            //    string rightSide = assignment.Right.ToString();

            //    // Traducir asignaciones de propiedades de CheckBox/DateTimePicker habituales en tu código
            //    if (leftSide.Contains(".Checked") && rightSide.EndsWith(".ValorBool"))
            //    {
            //        // Simplificar asignaciones complejas de tu modelo hacia booleanos nativos de .NET
            //        string cleanValue = rightSide.Replace(".ValorBool", ".Valor != null");
            //        return SyntaxFactory.ParseStatement(leftSide + " = " + cleanValue + ";");
            //    }

            //    if (leftSide.Contains(".Value") && (rightSide.EndsWith(".Valor") || rightSide.EndsWith(".ValorDecimal")))
            //    {
            //        // Convertir asignaciones de UltraNumericEditor a controles estándar
            //        string cleanValue = rightSide.Replace(".ValorDecimal", "").Replace(".Valor", "");
            //        return SyntaxFactory.ParseStatement(leftSide + " = Convert.ToDecimal(" + cleanValue + ");");
            //    }
            //}

            return base.VisitExpressionStatement(node);
        }
    }
}

#endif