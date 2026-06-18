#if NET47_OR_GREATER || NETCOREAPP

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FSConvert
{
    public class ConvertWinFormsCodeToWpf
    {
        private static readonly Dictionary<string, string> PropertyMapping = new Dictionary<string, string>()
        {
            // Estados y Habilitación
            { "ReadOnly", "IsReadOnly" },
            { "Enabled", "IsEnabled" },
            { "Checked", "IsChecked" },
            { "Visible", "Visibility" },

            // Focos y Selección
            { "Focused", "IsFocused" },
            { "ContainsFocus", "IsKeyboardFocusWithin" },
            { "SelectedIndex", "SelectedIndex" },
            { "SelectedValue", "SelectedValue" },
            { "SelectedItem", "SelectedItem" },

            // Dimensiones y Layout
            { "Width", "Width" },
            { "Height", "Height" },
            { "Top", "Canvas.Top" },
            { "Left", "Canvas.Left" },
            { "Tag", "Tag" },
            { "Capture", "IsMouseCaptured" },
            
            // Textos y Contenidos genéricos
            { "SelectedText", "SelectedText" },
            { "SelectionLength", "SelectionLength" },
            { "SelectionStart", "SelectionStart" }
        };

        public static string Convert(string sourceCode, Compilation compilation = null)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);

            if (compilation == null)
            {
                compilation = CSharpCompilation.Create("TemporaryAssembly")
                    .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                    .AddSyntaxTrees(tree);
            }

            SemanticModel semanticModel = compilation.GetSemanticModel(tree);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            var cleanUsings = root.Usings.Where(u =>
                !u.Name.ToString().StartsWith("System.Windows.Forms") &&
                !u.Name.ToString().StartsWith("System.Drawing")
            ).ToList();

            cleanUsings.Add(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Windows")));
            cleanUsings.Add(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Windows.Controls")));

            // Pasamos la raíz original para el mapeo semántico por posiciones
            var rewriter = new WpfLogicRewriter(PropertyMapping, semanticModel, root);
            var processedRoot = (CompilationUnitSyntax)rewriter.Visit(root);

            processedRoot = processedRoot.WithUsings(SyntaxFactory.List(cleanUsings));

            return processedRoot.NormalizeWhitespace().ToFullString();
        }
    }

    internal class WpfLogicRewriter : CSharpSyntaxRewriter
    {
        private readonly Dictionary<string, string> _propertyMapping;
        private readonly SemanticModel _semanticModel;
        private readonly CompilationUnitSyntax _originalRoot;

        public WpfLogicRewriter(Dictionary<string, string> propertyMapping, SemanticModel semanticModel, CompilationUnitSyntax originalRoot)
        {
            _propertyMapping = propertyMapping;
            _semanticModel = semanticModel;
            _originalRoot = originalRoot;
        }

        // ==================================================================
        // 1. REESTRUCTURAR CLASE BASE CONDICIONAL (Solo si era un Form)
        // ==================================================================
        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            string className = node.Identifier.Text;
            var updatedNode = node.WithIdentifier(SyntaxFactory.Identifier(className));

            // Evaluamos si tiene herencias declaradas
            if (updatedNode.BaseList != null)
            {
                // Comprobamos si hereda explícitamente de componentes visuales de WinForms
                bool inheritsFromWinForms = updatedNode.BaseList.Types.Any(b =>
                    b.ToString() == "Form" || b.ToString() == "FormBase"
                );

                if (inheritsFromWinForms)
                {
                    // Filtramos y removemos "Form" o "FormBase"
                    var cleanBases = updatedNode.BaseList.Types.Where(b =>
                        b.ToString() != "Form" && b.ToString() != "FormBase"
                    ).ToList();

                    // Insertamos al principio la ventana nativa de WPF
                    var windowType = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("Window"));
                    cleanBases.Insert(0, windowType);

                    updatedNode = updatedNode.WithBaseList(SyntaxFactory.BaseList(SyntaxFactory.SeparatedList(cleanBases)));
                }
                // Si tiene otra herencia (ej: ICloneable, MyBusinessBaseClass), NO la tocamos.
            }
            // Si la clase no hereda de absolutamente nada (BaseList == null), pasa de largo sin añadir "Window"

            return base.VisitClassDeclaration(updatedNode);
        }

        // ==================================================================
        // 2. SANITIZAR EL CONSTRUCTOR
        // ==================================================================
        public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            string parentClassName = ((ClassDeclarationSyntax)node.Parent).Identifier.Text;
            var updatedNode = node.WithIdentifier(SyntaxFactory.Identifier(parentClassName));

            if (updatedNode.Body != null)
            {
                var cleanStatements = updatedNode.Body.Statements.Where(st =>
                    st is ExpressionStatementSyntax exprSt &&
                    exprSt.Expression is InvocationExpressionSyntax invoke &&
                    invoke.Expression.ToString() == "InitializeComponent"
                ).ToList();

                // Si el constructor de esta clase no usaba InitializeComponent, mantenemos sus statements originales
                if (cleanStatements.Count > 0)
                {
                    updatedNode = updatedNode.WithBody(SyntaxFactory.Block(cleanStatements));
                }
            }

            return base.VisitConstructorDeclaration(updatedNode);
        }

        // ==================================================================
        // 3. TRADUCIR PROPIEDADES LÓGICAS (ej: .ReadOnly -> .IsReadOnly)
        // ==================================================================
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var processedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);
            string propertyName = processedNode.Name.Identifier.Text;

            if (_propertyMapping.ContainsKey(propertyName))
            {
                var newName = SyntaxFactory.IdentifierName(_propertyMapping[propertyName]);
                processedNode = processedNode.WithName(newName);
            }

            string expressionStr = processedNode.Expression.ToString();
            if (expressionStr.StartsWith("_") && expressionStr.Length > 1)
            {
                var cleanExpression = SyntaxFactory.ParseExpression(expressionStr.Substring(1));
                processedNode = processedNode.WithExpression(cleanExpression);
            }

            return processedNode;
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            string text = node.Identifier.Text;
            if (text.StartsWith("_") && text.Length > 1)
            {
                return SyntaxFactory.IdentifierName(text.Substring(1));
            }
            return base.VisitIdentifierName(node);
        }

        // ==================================================================
        // 4. TRADUCCIÓN SEMÁNTICA INMUNE A MUTACIONES PREVIAS
        // ==================================================================
        public override SyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            string leftStr = node.Left.ToString();

            // CASO A: Conversión de visibilidades lógicas (.Visible -> .Visibility)
            if (leftStr.EndsWith(".Visibility") || leftStr.EndsWith(".Visible"))
            {
                string rightStr = node.Right.ToString();
                var processedAssignment = (AssignmentExpressionSyntax)base.VisitAssignmentExpression(node);

                if (rightStr == "true" || rightStr == "True")
                {
                    return processedAssignment.WithRight(SyntaxFactory.ParseExpression("Visibility.Visible"));
                }
                else if (rightStr == "false" || rightStr == "False")
                {
                    return processedAssignment.WithRight(SyntaxFactory.ParseExpression("Visibility.Collapsed"));
                }
                return processedAssignment;
            }

            // CASO B: Traducción semántica robusta de .Text a .Content
            if (leftStr.EndsWith(".Text"))
            {
                ITypeSymbol typeSymbol = null;

                try
                {
                    var originalNode = _originalRoot.FindNode(node.Span) as AssignmentExpressionSyntax;

                    if (originalNode != null && originalNode.Left is MemberAccessExpressionSyntax originalMemberAccess)
                    {
                        var typeInfo = _semanticModel.GetTypeInfo(originalMemberAccess.Expression);
                        typeSymbol = typeInfo.Type;
                    }
                }
                catch
                {
                    // Contingencia por si falla el mapeo por posición
                }

                var processedAssignment = (AssignmentExpressionSyntax)base.VisitAssignmentExpression(node);

                if (typeSymbol != null)
                {
                    string fullTypeStr = typeSymbol.ToString();

                    if (RequiresContentPropertyInsteadOfText(fullTypeStr))
                    {
                        string currentLeftStr = processedAssignment.Left.ToString();
                        string correctedLeft = currentLeftStr.Substring(0, currentLeftStr.Length - 5) + ".Content";

                        var newLeft = SyntaxFactory.ParseExpression(correctedLeft);
                        return processedAssignment.WithLeft(newLeft);
                    }
                }

                return processedAssignment;
            }

            return base.VisitAssignmentExpression(node);
        }

        private bool RequiresContentPropertyInsteadOfText(string typeFullName)
        {
            return typeFullName.Contains("Label") ||
                   typeFullName.Contains("Button") ||
                   typeFullName.Contains("CheckBox") ||
                   typeFullName.Contains("RadioButton") ||
                   typeFullName.Contains("GroupBox");
        }
    }
}
#endif