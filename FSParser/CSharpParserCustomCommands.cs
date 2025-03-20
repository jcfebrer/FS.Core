#if !NET35

using FSLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                return string.Join(" ", args.ToArray());
            };

            parser.CustomCommands["contains"] = args =>
            {
                if (args.Count != 2)
                    throw new Exception("Parámetros incorrectos. En función: contains");
                return _Q(args[0]).Contains(_Q(args[1]));
            };

            parser.CustomCommands["convertwpf"] = args =>
            {
#if NET5_0_OR_GREATER || NETCOREAPP
                return FSConvert.ConvertToWPF.Convert(_Q(args[0]));
#else
                throw new Exception("ConvertToWpf solo disponible en NETCORE.");
#endif
            };

            parser.CustomCommands["help"] = args =>
            {
                return string.Join(" | ", parser.CustomCommands.Keys.ToArray());
            };

            parser.CustomCommands["print"] = args =>
            {
                Console.WriteLine(string.Join(" ", args.ToArray()));
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

#endif