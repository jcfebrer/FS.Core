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
                return args[0].ToString().Contains(args[1].ToString());
            };

            parser.CustomCommands["convertwpf"] = args =>
            {
                return FSConvert.ConvertToWPF.Convert(args[0]);
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
                return args[0].ToString().Replace(args[1].ToString(), args[2].ToString());
            };

            parser.CustomCommands["replacereg"] = args =>
            {
                if (args.Count != 3)
                    throw new Exception("Parámetros incorrectos. En función: replacereg");
                return TextUtil.ReplaceREG(args[0].ToString(), args[1].ToString(), args[2].ToString());
            };

            return parser;
        }
    }
}
