using System;
using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class MatrixOperation
    {
        /// <summary>
        /// Возвращает массив всех матриц с поворотом на угол degrees
        /// </summary>
        /// <param name="baseKernel">Базовая матрица</param>
        /// <param name="degrees">Угол поворота</param>
        /// <returns></returns>
        public static List<double[,]> RotateMatrix(double[,] baseKernel, double degrees)
        {
            var kernels = new List<double[,]>();
            var numberKernel = (int)(360 / degrees);

            int xOffset = baseKernel.GetLength(1) / 2;
            int yOffset = baseKernel.GetLength(0) / 2;

            for (int y = 0; y < baseKernel.GetLength(0); y++)
            {
                var kernel = new double[baseKernel.GetLength(0), baseKernel.GetLength(1)];

                for (int x = 0; x < baseKernel.GetLength(1); x++)
                {
                    for (int compass = 0; compass < numberKernel; compass++)
                    {
                        double radians = compass * degrees * 3.14 / 180.0;

                        int resultX = (int)(Math.Round(((x - xOffset) * Math.Cos(radians)) - ((y - yOffset) * Math.Sin(radians))) + xOffset);

                        int resultY = (int)(Math.Round(((x - xOffset) * Math.Sin(radians)) + ((y - yOffset) * Math.Cos(radians))) + yOffset);

                        kernel[resultY, resultX] = baseKernel[y, x];
                    }
                }
                kernels.Add(kernel);
            }

            return kernels;
        }

        /// <summary>
        /// Возвращает массив транспонированных матриц.
        /// </summary>
        /// <param name="baseKernel">Базовая матрица</param>
        /// <returns></returns>
        public static List<double[,]> TransponMatrix(double[,] baseKernel)
        {
            var kernels = new List<double[,]>();
            var kernelX = new double[baseKernel.GetLength(0), baseKernel.GetLength(1)];
            var kernelY = new double[baseKernel.GetLength(0), baseKernel.GetLength(1)];

            for (int i = 0; i < baseKernel.GetLength(0); i++)
                for (int j = 0; j < baseKernel.GetLength(1); j++)
                {
                    kernelX[i, j] = baseKernel[i, j];
                    kernelY[i, j] = baseKernel[j, i];
                }
            kernels.Add(kernelX);
            kernels.Add(kernelY);

            return kernels;
        }

        unsafe public static double SumElements(byte*[] neigbor, double[,] filter)
        {
            double sum = 0;
            int pointer = 0;
            for (int i = 0; i < filter.GetLength(0); i++)
                for (int j = 0; j < filter.GetLength(0); j++)
                    sum += neigbor[pointer++][0] * filter[i, j];

            return sum;
        }
    }
}
