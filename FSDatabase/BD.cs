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

namespace FSDatabase
{
    public class Field
    {
        public string Campo;

        public bool Obligatorio;

        public int Tamano;

        public Type Tipo;

        public string Tipo2;

        public string Valor;

        public Field()
        {
            Campo = "";
            Valor = "";
            Tipo = typeof(string);
            Obligatorio = false;
            Tamano = 50;
            Tipo2 = "";
        }

        public Field(string campo, object valor)
        {
            Campo = campo;
            Valor = valor.ToString();
            Tipo = valor.GetType();
        }

        public Field(string campo, string valor, Type tipo)
        {
            Campo = campo;
            Valor = valor;
            Tipo = tipo;
        }


        public Field(string campo, string valor, Type tipo, string tipo2)
        {
            Campo = campo;
            Valor = valor;
            Tipo = tipo;
            Tipo2 = tipo2;
        }

        public Field(string campo, string valor, Type tipo, int tamano, bool obligatorio)
        {
            Campo = campo;
            Valor = valor;
            Tipo = tipo;
            Obligatorio = obligatorio;
            Tamano = tamano;
        }

        public Field(string campo, string valor, Type tipo, string tipo2, int tamano, bool obligatorio)
        {
            Campo = campo;
            Valor = valor;
            Tipo = tipo;
            Obligatorio = obligatorio;
            Tamano = tamano;
            Tipo2 = tipo2;
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

        public int Add(string campo, object valor)
        {
            return List.Add(new Field(campo, valor));
        }

        public int Add(string campo, string valor, Type tipo)
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