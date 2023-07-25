using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConvolutionFilter
{
    public static class Filter
    {
        private static double[,,] EditByWeight(double[,,] convolutionMatrix, double weight)
        {
            for (int i = 0; i < convolutionMatrix.GetLength(0); i++)
                for (int j = 0; j < convolutionMatrix.GetLength(1); j++)
                    for (int k = 0; k < convolutionMatrix.GetLength(2); k++)
                        convolutionMatrix[i, j, k] *= weight;
            return convolutionMatrix;
        }

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
        public static Bitmap ConvolutionFilter(Bitmap originalBitmap, double[,,] convolutionMatrix,
            Func<long[], int> answer, bool toGrayscale = false,
            double weight = 1, int bias = 0)
        {
            if (originalBitmap.Height < convolutionMatrix.GetLength(1)
                || originalBitmap.Width < convolutionMatrix.GetLength(2)) return originalBitmap;


            if (convolutionMatrix.GetLength(0) == 0
                || convolutionMatrix.GetLength(1) == 0
                || convolutionMatrix.GetLength(2) == 0) return originalBitmap;

            if((int)weight!=1)
            convolutionMatrix = EditByWeight(convolutionMatrix, weight);


            Bitmap sourceBitmap = toGrayscale ? ToGrayscale(originalBitmap) : originalBitmap;


            Bitmap returnMap = new Bitmap(originalBitmap.Width, originalBitmap.Height, PixelFormat.Format32bppArgb);


            BitmapData bitmapData1 = originalBitmap.LockBits(new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0, returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int heightImage = bitmapData1.Height;
            int widthImage = bitmapData1.Width;
            int stride = bitmapData1.Stride;
            int numberOfFilter = convolutionMatrix.GetLength(0);

            int matrixSize = convolutionMatrix.GetLength(1);
            var neigborArray = new long[matrixSize * matrixSize];
            int halfMatrixSize = (matrixSize - 1) / 2;

            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                byte* pointer = (byte*)imagePointer1;

                for (int y = 0; y < heightImage; ++y)
                {
                    for (int x = 0; x < widthImage; ++x)
                    {

                        if (y >= halfMatrixSize && y < heightImage - halfMatrixSize
                                && x >= halfMatrixSize && x < heightImage - halfMatrixSize)
                        {
                            pointer = (byte*)imagePointer1;
                            pointer -= (widthImage * halfMatrixSize + halfMatrixSize) * 4;
                            int finalyAnswer = 0;

                            for (int matrix = 0; matrix < numberOfFilter; matrix++)
                            {
                                int k = 0;

                                for (int filterY = 0; filterY < matrixSize; filterY++)
                                {
                                    for (int filterX = 0; filterX < matrixSize; filterX++)
                                    {
                                        neigborArray[k++] = (long)(pointer[0] * convolutionMatrix[matrix, filterY, filterX]);
                                        pointer += 4;
                                    }

                                    pointer -= 4;

                                    pointer += (widthImage - (matrixSize - 1)) * 4;

                                }

                                pointer -= (widthImage - (matrixSize - 1)) * 4;

                                pointer -= (widthImage * (matrixSize - 1) + (matrixSize - 1)) * 4;

                                var tempAnswer = answer(neigborArray);
                                if (matrix == 0) finalyAnswer = tempAnswer;
                                finalyAnswer = finalyAnswer > tempAnswer ? finalyAnswer : tempAnswer;
                            }

                            finalyAnswer += bias;

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
            sourceBitmap.UnlockBits(bitmapData1);
            return returnMap;
        }



        public static Bitmap ToGrayscale(Bitmap originalImage)
        {
            Bitmap returnMap = new Bitmap(originalImage.Width, originalImage.Height, PixelFormat.Format32bppArgb);

            BitmapData bitmapData1 = originalImage.LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0, returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte averageValueBit = 0;
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;

                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        averageValueBit = (byte)(imagePointer1[0]*0.11 + imagePointer1[1]*0.59 +
                             imagePointer1[2]*0.3);

                        imagePointer2[0] = averageValueBit;
                        imagePointer2[1] = averageValueBit;
                        imagePointer2[2] = averageValueBit;
                        imagePointer2[3] = imagePointer1[3];

                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }

                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }

            returnMap.UnlockBits(bitmapData2);
            originalImage.UnlockBits(bitmapData1);
            return returnMap;
        }

        public static Bitmap ToInversion(Bitmap originalImage, bool toGrayscale = false)
        {
            originalImage = toGrayscale ? ToGrayscale(originalImage) : originalImage;

            Bitmap returnMap = new Bitmap(originalImage.Width, originalImage.Height, PixelFormat.Format32bppArgb);

            BitmapData bitmapData1 = originalImage.LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0, returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;

                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        imagePointer2[0] = (byte)(255 - imagePointer1[0]);
                        imagePointer2[1] = (byte)(255 - imagePointer1[1]);
                        imagePointer2[2] = (byte)(255 - imagePointer1[2]);
                        imagePointer2[3] = imagePointer1[3];

                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }

                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }

            returnMap.UnlockBits(bitmapData2);
            originalImage.UnlockBits(bitmapData1);
            return returnMap;
        }

        public static Bitmap ToSobelFilter(Bitmap originalBitmap, FilterParameters.SobelEnum filter, bool toGrayscale = false)
        {
            Bitmap resultBitmap = null;
            switch (filter)
            {
                #region Case3x3
                case FilterParameters.SobelEnum.Sobel3x3x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel3x3x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.SobelEnum.Sobel3x3x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel3x3x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.SobelEnum.Sobel3x3x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel3x3x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.SobelEnum.Sobel3x3x8:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel3x3x8, DelegateMethods.SumElements, toGrayscale);
                    break;
                #endregion

                #region Case5x5
                case FilterParameters.SobelEnum.Sobel5x5x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel5x5x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.SobelEnum.Sobel5x5x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel5x5x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.SobelEnum.Sobel5x5x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel5x5x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                #endregion

                #region Case7x7
                case FilterParameters.SobelEnum.Sobel7x7x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel7x7x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.SobelEnum.Sobel7x7x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel7x7x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.SobelEnum.Sobel7x7x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel7x7x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                #endregion

                #region Case9x9
                case FilterParameters.SobelEnum.Sobel9x9x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel9x9x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.SobelEnum.Sobel9x9x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel9x9x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.SobelEnum.Sobel9x9x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Sobel9x9x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                    #endregion
            }
            return resultBitmap;
        }

        public static Bitmap ToMedianFilter(Bitmap originalBitmap, int matrixSize = 3, bool toGrayscale = false)
        {
            return ConvolutionFilter(originalBitmap, Kernel.MedianKernel(matrixSize), DelegateMethods.MedianElement, toGrayscale);
        }

        public static Bitmap ToPrewittFilter(Bitmap originalBitmap, FilterParameters.PrewittEnum filter, bool toGrayscale = false)
        {
            Bitmap resultBitmap = null;

            switch (filter)
            {
                #region Case3x3
                case FilterParameters.PrewittEnum.Prewitt3x3x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt3x3x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.PrewittEnum.Prewitt3x3x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt3x3x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.PrewittEnum.Prewitt3x3x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt3x3x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.PrewittEnum.Prewitt3x3x8:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt3x3x8, DelegateMethods.SumElements, toGrayscale);
                    break;
                #endregion

                #region Case5x5
                case FilterParameters.PrewittEnum.Prewitt5x5x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt5x5x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.PrewittEnum.Prewitt5x5x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt5x5x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.PrewittEnum.Prewitt5x5x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt5x5x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                #endregion

                #region Case7x7
                case FilterParameters.PrewittEnum.Prewitt7x7x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt7x7x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.PrewittEnum.Prewitt7x7x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt7x7x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.PrewittEnum.Prewitt7x7x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Prewitt7x7x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                    #endregion
            }

            return resultBitmap;

        }

        public static Bitmap ToKirschFilter(Bitmap originalBitmap, FilterParameters.KirschEnum filter, bool toGrayscale = false)
        {
            Bitmap resultBitmap = null;

            switch (filter)
            {
                #region Case3x3
                case FilterParameters.KirschEnum.Kirsch3x3x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Kirsch3x3x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.KirschEnum.Kirsch3x3x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Kirsch3x3x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.KirschEnum.Kirsch3x3x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Kirsch3x3x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.KirschEnum.Kirsch3x3x8:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Kirsch3x3x8, DelegateMethods.SumElements, toGrayscale);
                    break;
                #endregion

                #region Case5x5
                case FilterParameters.KirschEnum.Kirsch5x5x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Kirsch5x5x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.KirschEnum.Kirsch5x5x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Kirsch5x5x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.KirschEnum.Kirsch5x5x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Kirsch5x5x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                    #endregion

            }

            return resultBitmap;
        }

        public static Bitmap ToScharrFilter(Bitmap originalBitmap, FilterParameters.ScharrEnum filter, bool toGrayscale = false)
        {
            Bitmap resultBitmap = null;

            switch (filter)
            {
                #region Case3x3
                case FilterParameters.ScharrEnum.Scharr3x3x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Scharr3x3x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.ScharrEnum.Scharr3x3x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Scharr3x3x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.ScharrEnum.Scharr3x3x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Scharr3x3x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.ScharrEnum.Scharr3x3x8:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Scharr3x3x8, DelegateMethods.SumElements, toGrayscale);
                    break;
                #endregion

                #region Case5x5
                case FilterParameters.ScharrEnum.Scharr5x5x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Scharr5x5x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.ScharrEnum.Scharr5x5x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Scharr5x5x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.ScharrEnum.Scharr5x5x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Scharr5x5x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                    #endregion
            }

            return resultBitmap;
        }

        public static Bitmap ToIsotropicFilter(Bitmap originalBitmap, FilterParameters.IsotropicEnum filter, bool toGrayscale = false)
        {
            Bitmap resultBitmap = null;

            switch (filter)
            {
                #region Case3x3
                case FilterParameters.IsotropicEnum.Isotropic3x3x1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Isotropic3x3x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.IsotropicEnum.Isotropic3x3x2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Isotripic3x3x2, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.IsotropicEnum.Isotropic3x3x4:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Isotropic3x3x4, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.IsotropicEnum.Isotropic3x3x8:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Isotropic3x3x8, DelegateMethods.SumElements, toGrayscale);
                    break;
                    #endregion
            }

            return resultBitmap;

        }

        public static Bitmap ToLaplaceFilter(Bitmap originalBitmap, FilterParameters.LaplaceEnum filter, bool toGrayscale = false)
        {
            Bitmap resultBitmap = null;

            switch (filter)
            {
                case FilterParameters.LaplaceEnum.Laplace3x3:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Laplace3x3x1, DelegateMethods.SumElements, toGrayscale);
                    break;

                case FilterParameters.LaplaceEnum.Laplace5x5:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Laplace5x5x1, DelegateMethods.SumElements, toGrayscale);
                    break;
            }

            return resultBitmap;

        }

        public static Bitmap ToGaussianFilter(Bitmap originalBitmap, FilterParameters.GaussianEnum filter, bool toGrayscale = false)
        {
            Bitmap resultBitmap = null;

            switch (filter)
            {
                case FilterParameters.GaussianEnum.Gaussian3x3:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Gaussian3x3x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.GaussianEnum.Gaussian5x5Type1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Gaussian5x5Type1x1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.GaussianEnum.Gaussian5x5Type2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Gaussian5x5Type2x1, DelegateMethods.SumElements, toGrayscale);
                    break;
            }

            return resultBitmap;

        }

        public static Bitmap ToHighPassFilter(Bitmap originalBitmap, FilterParameters.HighPassEnum filter, bool toGrayscale = false)
        {
            Bitmap resultBitmap = null;

            switch (filter)
            {
                case FilterParameters.HighPassEnum.HighPass3x3:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.HighPass3x3, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.HighPassEnum.HighPass5x5Type1:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.HighPass5x5Type1, DelegateMethods.SumElements, toGrayscale);
                    break;
                case FilterParameters.HighPassEnum.HighPass5x5Type2:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.HighPass5x5Type2, DelegateMethods.SumElements, toGrayscale);
                    break;

            }

            return resultBitmap;
        }

        public static Bitmap ToUniformFilter(Bitmap originalBitmap, FilterParameters.UniformEnum filter, bool toGrayscale = false)
        {
            Bitmap resultBitmap = null;

            switch (filter)
            {
                case FilterParameters.UniformEnum.Uniform3x3:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Uniform3x3, DelegateMethods.AverageElements, toGrayscale);
                    break;
                case FilterParameters.UniformEnum.Uniform5x5:
                    resultBitmap = ConvolutionFilter(originalBitmap, Kernel.Uniform5x5, DelegateMethods.AverageElements, toGrayscale);
                    break;
            }

            return resultBitmap;
        }
    }
}
