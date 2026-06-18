#if NET40_OR_GREATER || NETCOREAPP

using FSLibrary;
using FSTrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
#if NET47_OR_GREATER || NETCOREAPP
                return TextUtil.ProtectText(FSConvert.ConvertToWPF.Convert(TextUtil.UnProtectText(_Q(args[0]))));
#else
                throw new Exception("ConvertToWpf no esta disponible.");
#endif
            };

            parser.CustomCommands["removeinfragistics"] = args =>
            {
#if NET47_OR_GREATER || NETCOREAPP
                return TextUtil.ProtectText(FSConvert.ConvertInfragistics.Convert(TextUtil.UnProtectText(_Q(args[0])), true));
#else
                throw new Exception("RemoveInfragistics no esta disponible.");
#endif
            };

            parser.CustomCommands["convertwinformscodetowpf"] = args =>
            {
#if NET47_OR_GREATER || NETCOREAPP
                return TextUtil.ProtectText(FSConvert.ConvertWinFormsCodeToWpf.Convert(TextUtil.UnProtectText(_Q(args[0]))));
#else
                throw new Exception("ConvertWinFormsCodeToWpf no esta disponible.");
#endif
            };

            parser.CustomCommands["convertrpttowpf"] = args =>
            {
#if NET40_OR_GREATER
                return TextUtil.ProtectText(FSConvert.ConvertRptToWpf.Convert(TextUtil.UnProtectText(_Q(args[0]))));
#else
                throw new Exception("ConvertRptToWpf no esta disponible.");
#endif
            };

            parser.CustomCommands["convertcsprojtowpf"] = args =>
            {
#if NET47_OR_GREATER || NETCOREAPP
                return TextUtil.ProtectText(FSConvert.ConvertCsprojToWpf.Convert(TextUtil.UnProtectText(_Q(args[0])), "net48"));
#else
                throw new Exception("ConvertCsprojToWpf no esta disponible.");
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

                int count = 0;
                if (parser.CountReplaces)
                {
                    // Contamos el número de veces que aparece el texto a sustituir
                    count = (_Q(args[0]).Length - _Q(args[0]).Replace(_Q(args[1]), "").Length) / _Q(args[1]).Length;
                }
                else
                    count = 1;

                // Guardamos el resultado en memoria temporal para mostrarlo al final del proceso
                if (parser.Variables.ContainsKey(_Q(args[1])))
                    parser.Variables[_Q(args[1])] = (int)parser.Variables[_Q(args[1])] + count;
                else
                    parser.Variables[_Q(args[1])] = count;

                return _Q(args[0]).Replace(_Q(args[1]), _Q(args[2]));
            };

            parser.CustomCommands["replacereg"] = args =>
            {
                if (args.Count != 3)
                    throw new Exception("Parámetros incorrectos. En función: replacereg");

                int count = 0;
                if (parser.CountReplaces)
                {
                    // Contamos el número de veces que aparece el texto a sustituir
                    count = Regex.Matches(_Q(args[0]), _Q(args[1])).Count;
                }
                else
                    count = 1;

                // Guardamos el resultado en memoria temporal para mostrarlo al final del proceso
                if (parser.Variables.ContainsKey(_Q(args[1])))
                    parser.Variables[_Q(args[1])] = (int)parser.Variables[_Q(args[1])] + count;
                else
                    parser.Variables[_Q(args[1])] = count;

                return TextUtil.ReplaceREG(_Q(args[0]), _Q(args[1]), _Q(args[2]));
            };

            return parser;
        }
    }
}

#endif