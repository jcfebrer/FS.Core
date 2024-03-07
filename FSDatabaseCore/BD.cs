// // <fileheader>
// // <copyright file="BD.cs" company="Febrer Software">
// //     Fecha: 03/07/2010
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2010 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Collections;

#endregion

namespace FSDatabaseCore
{
    public class Field
    {
        public string Campo;

        public bool Obligatorio;

        public int Tamano;

        public Utils.FieldTypeEnum Tipo;

        public string Valor;

        public Field()
        {
            Campo = "";
            Valor = "";
            Tipo = Utils.FieldTypeEnum.String;
            Obligatorio = false;
            Tamano = 50;
        }

        public Field(string campo, string valor)
        {
            Campo = campo;
            Valor = valor.ToString();
            Tipo = Utils.FieldTypeEnum.String;
        }

        public Field(string campo, int valor)
        {
            Campo = campo;
            Valor = valor.ToString();
            Tipo = Utils.FieldTypeEnum.Number;
        }

        public Field(string campo, DateTime valor)
        {
            Campo = campo;
            Valor = valor.ToString();
            Tipo = Utils.FieldTypeEnum.DateTime;
        }

        public Field(string campo, bool valor)
        {
            Campo = campo;
            Valor = valor.ToString();
            Tipo = Utils.FieldTypeEnum.Boolean;
        }

        public Field(string campo, string valor, Utils.FieldTypeEnum tipo)
        {
            Campo = campo;
            Valor = valor;
            Tipo = tipo;
        }

        public Field(string campo, string valor, Utils.FieldTypeEnum tipo, int tamano, bool obligatorio)
        {
            Campo = campo;
            Valor = valor;
            Tipo = tipo;
            Obligatorio = obligatorio;
            Tamano = tamano;
        }
    }


    public class Register : CollectionBase
    {
        public Field get_List(int index)
        {
            return (Field) List[index];
        }

        public void set_List(int index, Field value)
        {
            List[index] = value;
        }

        public Field Find(string item)
        {
            foreach (Field field in List)
                if (field.Campo.ToLower() == item.ToLower())
                    return field;
            return null;
        }


        public Field FindType(string type)
        {
            foreach (Field field in List)
                if (field.Tipo.ToString().ToLower() == type.ToLower())
                    return field;
            return null;
        }

        public int Add(string campo, int valor)
        {
            return List.Add(new Field(campo, valor));
        }

        public int Add(string campo, bool valor)
        {
            return List.Add(new Field(campo, valor));
        }

        public int Add(string campo, DateTime valor)
        {
            return List.Add(new Field(campo, valor));
        }

        public int Add(string campo, string valor)
        {
            return List.Add(new Field(campo, valor));
        }

        public int Add(string campo, string valor, Utils.FieldTypeEnum tipo)
        {
            return List.Add(new Field(campo, valor, tipo));
        }

        public int Add(Field itemValue)
        {
            return List.Add(itemValue);
        }

        public void Remove(Field itemvalue)
        {
            List.Remove(itemvalue);
        }
    }
}