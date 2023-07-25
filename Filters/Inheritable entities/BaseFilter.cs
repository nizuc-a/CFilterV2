using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConvolutionFilter
{
    public abstract class BaseFilter
    {
        /// <summary>
        /// Возвращает изображение с наложенным матричным фильтром.
        /// </summary>
        /// <param name="originalBitmap">Оригинальное изображение</param>
        /// <param name="convolutionMatrix">Матрица свертки</param>
        /// <param name="answer">Какого рода ответ нужного получить после обработки мини матрицы, сумму элементов, среднее значение и т.д.</param>
        /// <param name="toGrayscale">Нужен ли перевод в ЧБ</param>
        /// <param name="factor"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        public Bitmap Process(Bitmap originalBitmap, List<double[,]> convolutionMatrix)
        {
            if (convolutionMatrix.Count == 0) return originalBitmap;

            if (originalBitmap.Height < convolutionMatrix[0].GetLength(1)
                || originalBitmap.Width < convolutionMatrix[0].GetLength(2))
                return originalBitmap;

            Bitmap returnMap = new Bitmap(originalBitmap.Width, originalBitmap.Height, PixelFormat.Format32bppArgb);

            BitmapData bitmapData1 = originalBitmap.LockBits(new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0, returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int heightImage = bitmapData1.Height;
            int widthImage = bitmapData1.Width;
            int stride = bitmapData1.Stride;
            int numberOfFilter = convolutionMatrix.Count;

            int matrixSize = convolutionMatrix[0].GetLength(1);

            int halfMatrixSize = (matrixSize - 1) / 2;

            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                byte* pointer = (byte*)imagePointer1;

                var neigbor = new byte*[matrixSize * matrixSize];

                for (int y = 0; y < heightImage; ++y)
                {
                    for (int x = 0; x < widthImage; ++x)
                    {
                        if (y >= halfMatrixSize && y < heightImage - halfMatrixSize
                                && x >= halfMatrixSize && x < heightImage - halfMatrixSize)
                        {
                            pointer = (byte*)imagePointer1;
                            pointer -= (widthImage * halfMatrixSize + halfMatrixSize) * 4;
                            double finalyAnswer = 0;

                            for (int matrix = 0; matrix < numberOfFilter; matrix++)
                            {
                                int k = 0;

                                for (int filterY = 0; filterY < matrixSize; filterY++)
                                {
                                    for (int filterX = 0; filterX < matrixSize; filterX++)
                                    {
                                        neigbor[k++] = pointer;
                                        pointer += 4;
                                    }

                                    pointer -= 4;
                                    pointer += (widthImage - (matrixSize - 1)) * 4;

                                }

                                pointer -= (widthImage - (matrixSize - 1)) * 4;
                                pointer -= (widthImage * (matrixSize - 1) + (matrixSize - 1)) * 4;

                                var tempAnswer = ProcessFilter(neigbor, convolutionMatrix[matrix]);

                                if (matrix == 0) 
                                    finalyAnswer = tempAnswer;

                                finalyAnswer = finalyAnswer > tempAnswer ? finalyAnswer : tempAnswer;
                            }

                            finalyAnswer = finalyAnswer > 255 ? 255 : finalyAnswer < 0 ? 0 : finalyAnswer;

                            imagePointer2[0] = (byte)finalyAnswer;
                            imagePointer2[1] = (byte)finalyAnswer;
                            imagePointer2[2] = (byte)finalyAnswer;
                            imagePointer2[3] = (byte)imagePointer1[3];
                        }

                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }

                    imagePointer1 += stride - (widthImage * 4);
                    imagePointer2 += stride - (widthImage * 4);
                }
            }

            returnMap.UnlockBits(bitmapData2);
            originalBitmap.UnlockBits(bitmapData1);
            return returnMap;
        }

        unsafe public abstract double ProcessFilter(byte*[] neigbor, double[,] filter);
    }
}
