/*Copyright 2018 Alaa Ben Fatma.
License: MIT
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FSLibrary
{
    /// <summary>
    /// Clase que permite la serialización/deserialización de una clase en texto y viceversa
    /// </summary>
    public class Serializer
    {
        const string OPEN_BLOCK = "<!";
        const string CLOSE_BLOCK = "!>";
        const string PARAM_SEPARATOR = "=";
        const string OPEN_PARAM = "[";
        const string CLOSE_PARAM = "]";
        const char IP_SEPARATOR = ':';

        //Rectangle
        const char COMMA_SEPARATOR = ',';
        const char EQUAL_SEPARATOR = '=';
        const string OPEN_BRACKET = "{";
        const string CLOSE_BRACKET = "}";

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(OPEN_BLOCK);

            List<PropertyInfo> props = new List<PropertyInfo>(obj.GetType().GetProperties());
            foreach (PropertyInfo prop in props)
            {
                ParameterInfo[] propParams = prop.GetIndexParameters();
                if (propParams.Length == 0)
                {
                    object propValue = prop.GetValue(obj, null);

                    if (prop.PropertyType == typeof(Byte[]) && propValue != null)
                    {
                        sb.Append(OPEN_PARAM + prop.Name + PARAM_SEPARATOR + System.Text.Encoding.ASCII.GetString((byte[])propValue) + CLOSE_PARAM);
                    }
                    else
                    {
                        sb.Append(OPEN_PARAM + prop.Name + PARAM_SEPARATOR + propValue + CLOSE_PARAM);
                    }
                }
                else
                {
                    int count = 0;
                    while (true)
                    {
                        try
                        {
                            prop.GetValue(obj, new object[] { count });
                            count++;
                        }
                        catch (TargetInvocationException) { break; }
                    }

                    for (int i = 0; i < count; i++)
                    {
                        sb.Append(OPEN_PARAM + prop.Name + PARAM_SEPARATOR + prop.GetValue(obj, new object[] { i }) + CLOSE_PARAM);
                    }
                }
            }
            sb.Append(CLOSE_BLOCK);
            return sb.ToString();
        }
        /// <summary>
        /// Returns a list of the serialized objects.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startString"></param>
        /// <param name="endString"></param>
        /// <param name="raw">Enabled: Will return the raw representation without any filtering.</param>
        /// <returns></returns>
        public static List<string> ExtractData(
            string text, string startString = OPEN_BLOCK, string endString = CLOSE_BLOCK, bool raw = false)
        {
            List<string> matched = new List<string>();
            bool exit = false;
            while (!exit)
            {
                int indexStart = text.IndexOf(startString, StringComparison.Ordinal);
                int indexEnd = text.IndexOf(endString, StringComparison.Ordinal);
                if (indexStart != -1 && indexEnd != -1)
                {
                    if (raw)
                        matched.Add(startString + text.Substring(indexStart + startString.Length, indexEnd - indexStart - startString.Length) + endString);
                    else
                        matched.Add(text.Substring(indexStart + startString.Length, indexEnd - indexStart - startString.Length));

                    text = text.Substring(indexEnd + endString.Length);
                }
                else
                {
                    exit = true;
                }
            }
            return matched;
        }
        /// <summary>
        /// Returns a list of all the serialized properties.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<Property> ExtractValuesFromData(string text)
        {
            List<Property> properties = new List<Property>();
            List<string> allData = ExtractData(text, OPEN_PARAM, CLOSE_PARAM);
            foreach (string data in allData)
            {
                string pName = data.Substring(0, data.IndexOf(PARAM_SEPARATOR, StringComparison.Ordinal));
                string pValue = data.Substring(data.IndexOf(PARAM_SEPARATOR, StringComparison.Ordinal) + 1);
                properties.Add(new Property { Name = pName, Value = pValue });
            }
            return properties;
        }
        /// <summary>
        /// Deserialize an object based on some serialized data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializeData"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(string serializeData, T target) where T : new()
        {
            List<string> deserializedObjects = ExtractData(serializeData);

            foreach (string obj in deserializedObjects)
            {
                List<Property> properties = ExtractValuesFromData(obj);
                foreach (Property property in properties)
                {
                    PropertyInfo propInfo = target.GetType().GetProperty(property.Name);

                    if (propInfo == null)
                        continue;

                    if (!propInfo.CanWrite)
                        continue;

                    if (propInfo.PropertyType == typeof(System.Net.IPEndPoint))
                    {
                        System.Net.IPEndPoint ipendpoint = null;
                        if (!String.IsNullOrEmpty(property.Value))
                        {
                            string address = property.Value.Split(IP_SEPARATOR)[0];
                            string port = property.Value.Split(IP_SEPARATOR)[1];
                            ipendpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(address), Convert.ToInt32(port));
                        }
                        propInfo?.SetValue(target, ipendpoint, null);
                    }
                    else if (propInfo.PropertyType == typeof(System.Drawing.Rectangle))
                    {
                        if (!String.IsNullOrEmpty(property.Value))
                        {
                            string _value = property.Value.Replace(OPEN_BRACKET, "").Replace(CLOSE_BRACKET, "");
                            string[] parameters = _value.Split(COMMA_SEPARATOR);
                            int x = Convert.ToInt32(parameters[0].Split(EQUAL_SEPARATOR)[1]);
                            int y = Convert.ToInt32(parameters[1].Split(EQUAL_SEPARATOR)[1]);
                            int w = Convert.ToInt32(parameters[2].Split(EQUAL_SEPARATOR)[1]);
                            int h = Convert.ToInt32(parameters[3].Split(EQUAL_SEPARATOR)[1]);
                            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(x, y, w, h);
                            propInfo?.SetValue(target, rectangle, null);
                        }
                    }
                    else if (propInfo.PropertyType == typeof(Byte[]))
                    {
                        object byteValue = (String.IsNullOrEmpty(property.Value)) ? null : Convert.ChangeType(Encoding.ASCII.GetBytes(property.Value), propInfo.PropertyType);
                        propInfo?.SetValue(target, byteValue, null);
                    }
                    else if (propInfo.PropertyType == typeof(System.Drawing.Point))
                    {
                        string _value = property.Value.Replace(OPEN_BRACKET, "").Replace(CLOSE_BRACKET, "");
                        string[] parameters = _value.Split(COMMA_SEPARATOR);
                        int x = Convert.ToInt32(parameters[0].Split(EQUAL_SEPARATOR)[1]);
                        int y = Convert.ToInt32(parameters[1].Split(EQUAL_SEPARATOR)[1]);
                        System.Drawing.Point point = new System.Drawing.Point(x, y);
                        propInfo?.SetValue(target, point, null);
                    }
                    else if (propInfo.PropertyType == typeof(System.Drawing.Size))
                    {
                        string _value = property.Value.Replace(OPEN_BRACKET, "").Replace(CLOSE_BRACKET, "");
                        string[] parameters = _value.Split(COMMA_SEPARATOR);
                        int width = Convert.ToInt32(parameters[0].Split(EQUAL_SEPARATOR)[1]);
                        int height = Convert.ToInt32(parameters[1].Split(EQUAL_SEPARATOR)[1]);
                        System.Drawing.Size size = new System.Drawing.Size(width, height);
                        propInfo?.SetValue(target, size, null);
                    }
                    else if (propInfo.PropertyType.BaseType == typeof(Enum))
                    {
                        Object enumValue = Enum.Parse(propInfo.PropertyType, property.Value, false);
                        propInfo?.SetValue(target, Enum.ToObject(propInfo.PropertyType, enumValue), null);
                    }
                    else
                    {
                        string _value = property.Value;
                        propInfo?.SetValue(target, Convert.ChangeType(_value, propInfo.PropertyType), null);
                    }                 
                }
            }
            return target;
        }

        /// <summary>
        /// Nombre y valor de la propiedad
        /// </summary>
        public struct Property
        {
            /// <summary>
            /// Nombre
            /// </summary>
            public string Name;
            /// <summary>
            /// Valor
            /// </summary>
            public string Value;
        }
    }
}