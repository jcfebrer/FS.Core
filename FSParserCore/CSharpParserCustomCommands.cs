using FSLibraryCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSParserCore
{
    public class CSharpParserCustomCommands
    {
        public static CSharpParser Commands(CSharpParser parser)
        {
            parser.CustomCommands["Print"] = args =>
            {
                Console.WriteLine(string.Join(" ", args));
                return null;
            };

            parser.CustomCommands["Concat"] = args =>
            {
                return string.Join(" ", args);
            };

            parser.CustomCommands["Contains"] = args =>
            {
                if (args.Count != 2)
                    throw new Exception("Parámetros incorrectos.");
                return args[0].ToString().Contains(args[1].ToString());
            };

            parser.CustomCommands["Replace"] = args =>
            {
                if (args.Count != 3)
                    throw new Exception("Parámetros incorrectos.");
                return args[0].ToString().Replace(args[1].ToString(), args[2].ToString());
            };

            parser.CustomCommands["ReplaceReg"] = args =>
            {
                if (args.Count != 3)
                    throw new Exception("Parámetros incorrectos.");
                return TextUtil.ReplaceREG(args[0].ToString(), args[1].ToString(), args[2].ToString());
            };

            parser.CustomCommands["Help"] = args =>
            {
                return string.Join(" ", parser.CustomCommands.Keys.ToArray());
            };

            return parser;
        }
    }
}
