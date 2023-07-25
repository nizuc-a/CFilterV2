using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class GaussianKernel
    {
        #region Kernels
        public static double[,] Gaussian3x3 = new double[,]
                                                { { 1, 2, 1, },
                                                {   2, 4, 2, },
                                                {   1, 2, 1, } };

        public static double[,] Gaussian5x5Type1 = new double[,]
                                                   { { 2, 04, 05, 04, 2 },
                                                     { 4, 09, 12, 09, 4 },
                                                     { 5, 12, 15, 12, 5 },
                                                     { 4, 09, 12, 09, 4 },
                                                     { 2, 04, 05, 04, 2 }, };

        public static double[,] Gaussian5x5Type2 = new double[,]
                                                   { {  1,   4,  6,  4,  1 },
                                                     {  4,  16, 24, 16,  4 },
                                                     {  6,  24, 36, 24,  6 },
                                                     {  4,  16, 24, 16,  4 },
                                                     {  1,   4,  6,  4,  1 }, };
        #endregion

        #region Result

        public List<double[,]> Gaussian3x3x1 => MatrixOperation.RotateMatrix(Gaussian3x3, 360);
        public List<double[,]> Gaussian5x5Type1x1 => MatrixOperation.RotateMatrix(Gaussian5x5Type1, 360);
        public List<double[,]> Gaussian5x5Type2x1 => MatrixOperation.RotateMatrix(Gaussian5x5Type2, 360);

        #endregion
    }
}
