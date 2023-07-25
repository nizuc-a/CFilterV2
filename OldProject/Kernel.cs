using System;

namespace ConvolutionFilter
{
    public static class Kernel
    {
        /// <summary>
        /// Возвращает массив всех матриц с поворотом на угол degrees
        /// </summary>
        /// <param name="baseKernel">Базовая матрица</param>
        /// <param name="degrees">Угол поворота</param>
        /// <returns></returns>
        public static double[,,] RotateMatrix(double[,] baseKernel, double degrees)
        {
            double[,,] kernel = new double[(int)(360 / degrees),
                baseKernel.GetLength(0), baseKernel.GetLength(1)];

            int xOffset = baseKernel.GetLength(1) / 2;
            int yOffset = baseKernel.GetLength(0) / 2;

            for (int y = 0; y < baseKernel.GetLength(0); y++)
            {
                for (int x = 0; x < baseKernel.GetLength(1); x++)
                {
                    for (int compass = 0; compass < kernel.GetLength(0); compass++)
                    {
                        double radians = compass * degrees * 3.14 / 180.0;

                        int resultX = (int)(Math.Round( ((x - xOffset) * Math.Cos(radians)) - ((y - yOffset) * Math.Sin(radians)) ) + xOffset);

                        int resultY = (int)(Math.Round( ((x - xOffset) * Math.Sin(radians)) + ((y - yOffset) * Math.Cos(radians)) ) + yOffset);

                        kernel[compass, resultY, resultX] = baseKernel[y, x];
                    }
                }
            }
            return kernel;
        }

        /// <summary>
        /// Возвращает массив транспонированных матриц.
        /// </summary>
        /// <param name="baseKernel">Базовая матрица</param>
        /// <returns></returns>
        public static double[,,] TransponMatrix(double[,] baseKernel)
        {
            var kernel = new double[2, baseKernel.GetLength(0), baseKernel.GetLength(1)];

            for (int i = 0; i < baseKernel.GetLength(0); i++)
                for (int j = 0; j < baseKernel.GetLength(1); j++)
                {
                    kernel[0, i, j] = baseKernel[i, j];
                    kernel[1, i, j] = baseKernel[j, i];
                }

            return kernel;
        }

        /// <summary>
        /// Возвращает гауссиан заданного размера.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static double[,] GaussianCalculator(int length, double weight)
        {
            double[,] kernel = new double[length, length];
            double sumTotal = 0;

            int kernelRadius = length / 2;
            double distance;

            double calculatedEuler = 1.0 / (2.0 * Math.PI * Math.Pow(weight, 2));

            for (int filterY = -kernelRadius; filterY <= kernelRadius; filterY++)
            {
                for (int filterX = -kernelRadius; filterX <= kernelRadius; filterX++)
                {
                    distance = ((filterX * filterX) + (filterY * filterY)) / (2 * (weight * weight));

                    kernel[filterY + kernelRadius, filterX + kernelRadius] = calculatedEuler * Math.Exp(-distance);

                    sumTotal += kernel[filterY + kernelRadius, filterX + kernelRadius];
                }
            }

            for (int y = 0; y < length; y++)
                for (int x = 0; x < length; x++)
                    kernel[y, x] = kernel[y, x] * (1.0 / sumTotal);


            return kernel;
        }

        /// <summary>
        /// Возвращает матрицу заполненную единицами для медианного фильтра.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static double[,,] MedianKernel(int length)
        {
            var kernel = new double[length, length];
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    kernel[i, j] = 1;

            return RotateMatrix(kernel, 360);
        }

     

        

        

       

        

        

        

        

        

        



    }
}
