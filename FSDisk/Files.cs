using System;
using System.Collections;
using System.Collections.Generic;

#if NET35_OR_GREATER || NETCOREAPP
    using System.Linq;
#endif

using System.Text;

namespace FSDisk
{
    public class Files : CollectionBase
    {
        /// <summary>
        ///     Fichero
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public File this[int index]
        {
            get { return (File)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        ///     Añadir fichero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(File value)
        {
            return List.Add(value);
        }

        /// <summary>
        ///     Devuelve el indice
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(File value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        ///     Insertamos un fichero
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, File value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///     Borramos un fichero
        /// </summary>
        /// <param name="value"></param>
        public void Remove(File value)
        {
            List.Remove(value);
        }

        /// <summary>
        ///     Devuelve true/false si existe el fichero en la colección
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(File value)
        {
            return List.Contains(value);
        }

        public File Find(string nombre)
        {
            foreach (File fic in List)
            {
                if (fic.Nombre.ToLower() == nombre.ToLower()) return fic;
            }

            return null;
        }

        public Files FindExtension(string extension)
        {
            Files returnFiles = new Files();
            foreach (File fic in List)
            {
                if (fic.Extension.ToLower() == extension.ToLower())
                    returnFiles.Add(fic);
            }

            return returnFiles;
        }

        public Files FindSoundEx(string fileName, int length = 4)
        {
            Files returnFiles = new Files();
            string soundEx = FSFuzzyStrings.SoundExEsp.Do(fileName, length);

            foreach (File fic in List)
            {
                if (fic.SoundEx == soundEx)
                    returnFiles.Add(fic);
            }

            return returnFiles;
        }

#if NET40_OR_GREATER || NETCOREAPP
        public Files FindFuzzy(string fileName, double probability = 0.75)
        {
            Files returnFiles = new Files();
            foreach (File fic in List)
            {
                if (FSFuzzyStrings.StringExtensions.FuzzyEquals(fic.Nombre, fileName, probability))
                    returnFiles.Add(fic);
            }

            return returnFiles;
        }
#endif

        public Files FindLevenshtein(string fileName, int distance = 4)
        {
            Files returnFiles = new Files();
            foreach (File fic in List)
            {
                if (FSFuzzyStrings.LevenshteinDistanceExtensions.LevenshteinDistance(fic.Nombre, fileName) < distance)
                    returnFiles.Add(fic);
            }

            return returnFiles;
        }
    }
}
