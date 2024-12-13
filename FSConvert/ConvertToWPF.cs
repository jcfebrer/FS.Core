using FSTrace;
using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace FSConvert
{
    class ConvertToWPF
    {
        public static bool Convert(string inputFile, string outputFile)
        {
            if (!File.Exists(inputFile))
            {
                throw new Exception($"Archivo no encontrado: {inputFile}");
            }

            string content = File.ReadAllText(inputFile);

            // 1. Eliminar Suspensión y Reanudación de Diseño
            content = Regex.Replace(content, @"this\.SuspendLayout\(\);[\s\S]*?this\.ResumeLayout\(false\);\s*this\.PerformLayout\(\);", "");

            // 2. Controles Comunes de Windows Forms
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.Button\(\);", "<Button Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.Label\(\);", "<TextBlock Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TextBox\(\);", "<TextBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.CheckBox\(\);", "<CheckBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.RadioButton\(\);", "<RadioButton Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.ComboBox\(\);", "<ComboBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.ListBox\(\);", "<ListBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.DataGridView\(\);", "<DataGrid Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.PictureBox\(\);", "<Image Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.Panel\(\);", "<Grid Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.GroupBox\(\);", "<GroupBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TabControl\(\);", "<TabControl Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TabPage\(\);", "<TabItem Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.FlowLayoutPanel\(\);", "<WrapPanel Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TableLayoutPanel\(\);", "<Grid Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TrackBar\(\);", "<Slider Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.ProgressBar\(\);", "<ProgressBar Name=\"$1\" />");

            // 3. Propiedades de los Controles
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.BackColor = System\.Drawing\.Color\.(\w+);", "Background=\"{x:Static SystemColors.$2Brush}\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.ForeColor = System\.Drawing\.Color\.(\w+);", "Foreground=\"{x:Static SystemColors.$2Brush}\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Size = new System\.Drawing\.Size\((\d+), (\d+)\);", "Width=\"$2\" Height=\"$3\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Location = new System\.Drawing\.Point\((\d+), (\d+)\);", "Margin=\"$2,$3,0,0\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Text = ""([^""]+)"";", "Content=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Enabled = false;", "IsEnabled=\"False\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Visible = false;", "Visibility=\"Collapsed\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Anchor = System\.Windows\.Forms\.AnchorStyles\.(\w+);", "HorizontalAlignment=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Dock = System\.Windows\.Forms\.DockStyle\.(\w+);", "VerticalAlignment=\"$2\"");

            // 4. Eventos de los Controles
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Click \+= new System\.EventHandler\(this\.([a-zA-Z0-9_]+)\);", "Click=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.CheckedChanged \+= new System\.EventHandler\(this\.([a-zA-Z0-9_]+)\);", "Checked=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.TextChanged \+= new System\.EventHandler\(this\.([a-zA-Z0-9_]+)\);", "TextChanged=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.SelectedIndexChanged \+= new System\.EventHandler\(this\.([a-zA-Z0-9_]+)\);", "SelectionChanged=\"$2\"");

            // 5. Declaración de Controles
            content = Regex.Replace(content, @"private System\.Windows\.Forms\.([a-zA-Z0-9_]+) ([a-zA-Z0-9_]+);", "");

            // Guardar el resultado
            File.WriteAllText(outputFile, content);

            Log.Trace($"Reemplazos completados. Archivo generado: {outputFile}");

            return true;
        }
    }
}
