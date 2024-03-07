using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSDiskCore
{
    public class Folder
    {
        string _Nombre;
        string _Path;
        decimal _Tamaño;
        System.DateTime _FechaArchivo;
        bool _HasSubfolder = false;

        public Folder()
        {
        }


        public Folder(string nombre)
        {
            _Nombre = nombre;
        }

        public Folder(string nombre, decimal tamaño, System.DateTime fechaArchivo)
        {
            _Nombre = nombre;
            _Tamaño = tamaño;
            _FechaArchivo = fechaArchivo;
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        public decimal Tamaño
        {
            get { return _Tamaño; }
            set { _Tamaño = value; }
        }

        public System.DateTime FechaArchivo
        {
            get { return _FechaArchivo; }
            set { _FechaArchivo = value; }
        }

        public bool HasSubfolder
        {
            get { return _HasSubfolder; }
            set { _HasSubfolder = value; }
        }
    }
}
