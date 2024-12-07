using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSIACore
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public class MNISTLoader
    {
        public List<byte[]> Images { get; private set; }
        public List<byte> Labels { get; private set; }
        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }

        public MNISTLoader(string imagesPath, string labelsPath)
        {
            Images = new List<byte[]>();
            Labels = new List<byte>();
            LoadImages(imagesPath);
            LoadLabels(labelsPath);
        }

        private void LoadImages(string filePath)
        {
            using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int magicNumber = ReverseBytes(reader.ReadInt32());
                if (magicNumber != 2051)
                    throw new Exception("El archivo de imágenes no es válido.");

                int numberOfImages = ReverseBytes(reader.ReadInt32());
                ImageHeight = ReverseBytes(reader.ReadInt32());
                ImageWidth = ReverseBytes(reader.ReadInt32());
                int imageSize = ImageHeight * ImageWidth;

                for (int i = 0; i < numberOfImages; i++)
                {
                    byte[] image = reader.ReadBytes(imageSize);
                    Images.Add(image);
                }
            }
        }

        private void LoadLabels(string filePath)
        {
            using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int magicNumber = ReverseBytes(reader.ReadInt32());
                if (magicNumber != 2049)
                    throw new Exception("El archivo de etiquetas no es válido.");

                int numberOfLabels = ReverseBytes(reader.ReadInt32());

                for (int i = 0; i < numberOfLabels; i++)
                {
                    byte label = reader.ReadByte();
                    Labels.Add(label);
                }
            }
        }

        private int ReverseBytes(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public double[] GetNormalizedImage(int index)
        {
            if (index < 0 || index >= Images.Count)
                throw new IndexOutOfRangeException("Índice fuera del rango.");

            byte[] image = Images[index];
            return Array.ConvertAll(image, pixel => pixel / 255.0);
        }

        public void PrintImage(int index)
        {
            if (index < 0 || index >= Images.Count)
                throw new IndexOutOfRangeException("Índice fuera del rango.");

            byte[] image = Images[index];
            for (int y = 0; y < ImageHeight; y++)
            {
                for (int x = 0; x < ImageWidth; x++)
                {
                    byte pixel = image[y * ImageWidth + x];
                    Debug.Write(pixel > 127 ? "#" : ".");
                }
                Debug.WriteLine("");
            }

            Debug.WriteLine($"Etiqueta: {Labels[index]}");
        }
    }

}
