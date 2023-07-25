using System.Collections.Generic;

namespace ConvolutionFilter
{
    class LaplaceKernel
    {
        #region Kernels
        public static double[,] Laplace3x3 = new double[,]
                                             { {  -1, -1, -1  },
                                               {  -1,  8, -1  },
                                               {  -1, -1, -1  }, };

        public static double[,] Laplace5x5 = new double[,]
                                             { { -1, -1, -1, -1, -1 },
                                               { -1, -1, -1, -1, -1 },
                                               { -1, -1, 24, -1, -1 },
                                               { -1, -1, -1, -1, -1 },
                                               { -1, -1, -1, -1, -1 } };
        #endregion

        #region Result

        public List<double[,]> Laplace3x3x1 => MatrixOperation.RotateMatrix(Laplace3x3, 360);
        public List<double[,]> Laplace5x5x1 => MatrixOperation.RotateMatrix(Laplace5x5, 360);

        #endregion
    }
}
