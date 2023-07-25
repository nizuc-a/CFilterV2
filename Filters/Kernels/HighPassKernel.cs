using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class HighPassKernel
    {
        #region Kernel
        public static double[,] HighPass3x3 = new double[,]
                                                    { { -1, -2, -1, },
                                                      { -2, 12, -2, },
                                                      { -1, -2, -1 } };

        public static double[,] HighPass5x5Type1 = new double[,]
                                                    {{-1,-1,-1,-1,-1 },
                                                     {-1,-1,-1,-1,-1 },
                                                     {-1,-1,24,-1,-1 },
                                                     {-1,-1,-1,-1,-1 },
                                                     {-1,-1,-1,-1,-1 }};

        public static double[,] HighPass5x5Type2 = new double[,]
                                                     {{-2,-2,-2,-2,-2},
                                                     {-2,-3,-3,-3,-2 },
                                                     {-2,-3,57,-3,-2 },
                                                     {-2,-2,-2,-2,-2},
                                                     {-2,-3,-3,-3,-2 }};
        #endregion

        #region HighPass
        public List<double[,]> HighPass3x3x1 =>      MatrixOperation.RotateMatrix(HighPass3x3, 360);
        public List<double[,]> HighPass5x5Type1x1 => MatrixOperation.RotateMatrix(HighPass5x5Type1, 360);
        public List<double[,]> HighPass5x5Type2x1 => MatrixOperation.RotateMatrix(HighPass5x5Type2, 360);
        #endregion
    }
}
