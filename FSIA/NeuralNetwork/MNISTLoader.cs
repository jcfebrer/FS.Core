using System;
using System.Diagnostics;
using System.IO;

namespace FSIA
{
    public class MNISTLoader
    {
        public (byte[][] images, byte[] labels) Load(string imagesPath, string labelsPath)
        {
            byte[][] images = LoadImages(imagesPath);
            byte[] labels = LoadLabels(labelsPath);
            return (images, labels);
        }

        /// <summary>
        /// Normaliza las imágenes en formato plano (1D).
        /// </summary>
        /// <param name="rawImages">Imágenes crudas cargadas desde el archivo.</param>
        /// <returns>Imágenes normalizadas como arreglos de double.</returns>
        public double[][] NormalizeImages(byte[][] rawImages)
        {
            int numImages = rawImages.Length;
            int imageSize = 28 * 28; // Asumimos imágenes de 28x28 píxeles
            double[][] normalizedImages = new double[numImages][];

            for (int i = 0; i < numImages; i++)
            {
                normalizedImages[i] = new double[imageSize];
                for (int j = 0; j < imageSize; j++)
                {
                    normalizedImages[i][j] = rawImages[i][j] / 255.0;
                }
            }

            return normalizedImages;
        }

        /// <summary>
        /// Normaliza las imagenes en formato 3D.
        /// </summary>
        /// <param name="rawImages">Imágenes crudas cargadas desde el archivo.</param>
        /// <returns>Imágenes normalizadas como arreglos de double.</returns>
        public double[][,,] NormalizeImages3D(byte[][] rawImages)
        {
            int numImages = rawImages.Length;
            int imageSize = 28; // Asumimos imágenes de 28x28 píxeles
            double[][,,] normalizedImages = new double[numImages][,,];

            for (int i = 0; i < numImages; i++)
            {
                double[,,] image = new double[1, imageSize, imageSize]; // Canal único (grayscale)
                for (int row = 0; row < imageSize; row++)
                {
                    for (int col = 0; col < imageSize; col++)
                    {
                        image[0, row, col] = rawImages[i][row * imageSize + col] / 255.0;
                    }
                }
                normalizedImages[i] = image;
            }

            return normalizedImages;
        }

        public double[][] OneHotEncodeLabels(byte[] rawLabels, int numClasses)
        {
            double[][] oneHotLabels = new double[rawLabels.Length][];

            for (int i = 0; i < rawLabels.Length; i++)
            {
                oneHotLabels[i] = new double[numClasses];
                oneHotLabels[i][rawLabels[i]] = 1.0;
            }

            return oneHotLabels;
        }

        public void PrintImage3D(double[,,] image)
        {
            int rows = image.GetLength(1);
            int cols = image.GetLength(2);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Debug.Write(image[0, i, j] > 0.5 ? "#" : ".");
                }
                Debug.WriteLine("");
            }
        }

        public void PrintImage(double[] image)
        {
            int size = 28; // Dimensión de la imagen (28x28)
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Debug.Write(image[row * size + col] > 0.5 ? "#" : ".");
                }
                Debug.WriteLine("");
            }
        }

        private byte[][] LoadImages(string path)
        {
            using (var reader = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                int magicNumber = ReverseBytes((long)reader.ReadInt32());
                if (magicNumber != 2051)
                    throw new Exception("Número mágico inválido en el archivo de imágenes");

                int numImages = ReverseBytes((long)reader.ReadInt32());
                int numRows = ReverseBytes((long)reader.ReadInt32());
                int numCols = ReverseBytes((long)reader.ReadInt32());

                byte[][] images = new byte[numImages][];
                for (int i = 0; i < numImages; i++)
                {
                    images[i] = reader.ReadBytes(numRows * numCols);
                }

                return images;
            }
        }

        private byte[] LoadLabels(string path)
        {
            using (var reader = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                int magicNumber = ReverseBytes((long)reader.ReadInt32());
                if (magicNumber != 2049)
                    throw new Exception("Número mágico inválido en el archivo de etiquetas");

                int numLabels = ReverseBytes((long)reader.ReadInt32());
                return reader.ReadBytes(numLabels);
            }
        }

        private int ReverseBytes(long value)
        {
            return (int)(((value & 0x000000FF) << 24) |
                         ((value & 0x0000FF00) << 8) |
                         ((value & 0x00FF0000) >> 8) |
                         ((value & 0xFF000000) >> 24));
        }
    }
}
