using FSLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSParser
{
    public class CSharpParserCustomCommands
    {
        private static string _Q(string text)
        {
            //Eliminamos las comillas dobles
            return TextUtil.RemoveQuotes(text);
        }

        public static CSharpParser Commands(CSharpParser parser)
        {
            parser.CustomCommands["ayuda"] = args =>
            {
                return string.Join(" | ", parser.CustomCommands.Keys.ToArray());
            };

            parser.CustomCommands["concat"] = args =>
            {
                return string.Join(" ", args);
            };

            parser.CustomCommands["contains"] = args =>
            {
                if (args.Count != 2)
                    throw new Exception("Parámetros incorrectos. En función: contains");
                return _Q(args[0]).Contains(_Q(args[1]));
            };

            parser.CustomCommands["convertwpf"] = args =>
            {
                return FSConvert.ConvertToWPF.Convert(_Q(args[0]));
            };

            parser.CustomCommands["help"] = args =>
            {
                return string.Join(" | ", parser.CustomCommands.Keys.ToArray());
            };

            parser.CustomCommands["print"] = args =>
            {
                Console.WriteLine(string.Join(" ", args));
                return null;
            };

            parser.CustomCommands["replace"] = args =>
            {
                if (args.Count != 3)
                    throw new Exception("Parámetros incorrectos. En función: replace");
                return _Q(args[0]).Replace(_Q(args[1]), _Q(args[2]));
            };

            parser.CustomCommands["replacereg"] = args =>
            {
                if (args.Count != 3)
                    throw new Exception("Parámetros incorrectos. En función: replacereg");
                return TextUtil.ReplaceREG(_Q(args[0]), _Q(args[1]), _Q(args[2]));
            };

            return parser;
        }
    }
}
