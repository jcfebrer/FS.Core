using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FSDisk
{
    public class Folders : CollectionBase
    {
        /// <summary>
        ///     Carpeta
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Folder this[int index]
        {
            get { return (Folder)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        ///     Añadir Carpeta
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(Folder value)
        {
            return List.Add(value);
        }

        /// <summary>
        ///     Devuelve el indice
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(Folder value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        ///     Insertamos una carpeta
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, Folder value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///     Borramos una carpeta
        /// </summary>
        /// <param name="value"></param>
        public void Remove(Folder value)
        {
            List.Remove(value);
        }

        /// <summary>
        ///     Devuelve true/false si existe el fichero en la colección
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(Folder value)
        {
            return List.Contains(value);
        }

        public Folder Find(string nombre)
        {
            foreach (Folder fol in List)
            {
                if (fol.Nombre.ToLower() == nombre.ToLower()) return fol;
            }

            return null;
        }
    }
}
