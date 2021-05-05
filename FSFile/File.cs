
using System;
using System.Drawing;

namespace FSFile
{
	/// <summary>
	/// Description of File.
	/// </summary>
	public class File
	{
        string _dir;
        string _Nombre;
        string _FullName;
        string _NombreNormalizado;
        decimal _Tamaño;
        System.DateTime _FechaArchivo;
        string _Label;
        Color _ColorFondo;
        string _Extension;
        string _SoundEx;
        string _crc32;
        int _veces;

        public File()
        {
        }


        public File(string nombre)
        {
            _Nombre = nombre;
        }

        public File(string dir, string nombre, string extension, string nombreNormalizado, decimal tamaño, System.DateTime fechaArchivo, string label, Color colorFondo)
        {
            _dir = dir;
            _Nombre = nombre;
            _NombreNormalizado = nombreNormalizado;
            _Extension = extension;
            _Tamaño = tamaño;
            _FechaArchivo = fechaArchivo;
            _Label = label;
            _ColorFondo = colorFondo;
        }

        public string Dir
        {
            get { return _dir; }
            set { _dir = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string NombreNormalizado
        {
            get { return _NombreNormalizado; }
            set { _NombreNormalizado = value; }
        }

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public decimal Tamaño
        {
            get { return _Tamaño; }
            set { _Tamaño = value; }
        }

        public string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }

        public System.DateTime FechaArchivo
        {
            get { return _FechaArchivo; }
            set { _FechaArchivo = value; }
        }

        public string Label
        {
            get { return _Label; }
            set { _Label = value; }
        }

        public Color ColorFondo
        {
            get { return _ColorFondo; }
            set { _ColorFondo = value; }
        }

        public string SoundEx
        {
            get { return _SoundEx; }
            set { _SoundEx = value; }
        }

        public string Crc32
        {
            get { return _crc32; }
            set { _crc32 = value; }
        }

        public int Veces
        {
            get { return _veces; }
            set { _veces = value; }
        }
    }
}
