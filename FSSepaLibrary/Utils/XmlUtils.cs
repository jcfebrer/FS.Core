﻿using FSException;
using System.Xml;

namespace FSSepaLibrary.Utils
{
    /// <summary>
    ///     Some Utilities to manage XML
    /// </summary>
    public static class XmlUtils
    {
        /// <summary>
        ///     Find First element in the Xml document with provided name
        /// </summary>
        /// <param name="document">The Xml Document</param>
        /// <param name="nodeName">The name of the node</param>
        /// <returns></returns>
        public static XmlElement SelectSingleNode(XmlNode document, string nodeName)
        {
            if (document == null)
                throw new ExceptionUtil("Document is null");
            return document.SelectSingleNode(nodeName) as XmlElement;
        }

        /// <summary>
        ///     Create a BIC
        /// </summary>
        /// <param name="element">The Xml element</param>
        /// <param name="iban">The iban</param>
        /// <returns></returns>
        public static void CreateBic(XmlElement element, SepaIbanData iban)
        {
            if (iban.UnknownBic)
            {
                element.NewElement("FinInstnId").NewElement("Othr").NewElement("Id", "NOTPROVIDED");
            }
            else
            {
                element.NewElement("FinInstnId").NewElement("BIC", iban.Bic);
            }
        }
    }
}
