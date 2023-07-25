using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class SobelKernel
    {
        #region SobelKernels
        public static double[,] Sobel3x3 = new double[,]
                                          { {  -1,  0,  1,  },
                                             {  -2,  0,  2,  },
                                             {  -1,  0,  1,  }, };

        public static double[,] Sobel5x5 = new double[,]
                                           { {   -5,  -4,  0,   4,  5,  },
                                             {   -8, -10,  0,  10,  8,  },
                                             {  -10, -20,  0,  20, 10,  },
                                             {   -8, -10,  0,  10,  8,  },
                                             {   -5,  -4,  0,   4,  5,  }, };

        public static double[,] Sobel7x7 = new double[,]
                                           {{ -130, -120, -78,  0, 78, 120, 130 },
                                           { -180, -195, -156, 0, 156, 195, 180 },
                                           { -234, -312, -390, 0, 390, 312, 234 },
                                           { -260, -390, -780, 0, 780, 390, 260 },
                                           { -234, -312, -390, 0, 390, 312, 234 },
                                           { -180, -195, -156, 0, 156, 195, 180 },
                                           { -130, -120, -78,  0, 78, 120, 130 }  };

        public static double[,] Sobel9x9 = new double[,]
                 {  { -16575, -15912, -13260, -7800, 0, 7800, 13260, 15912, 16575 },
                    { -21216, -22100, -20400, -13260, 0, 13260, 20400, 22100, 21216 },
                    {-26520, -30600, -33150, -26520, 0, 26520, 33150, 30600, 26520 },
                    { -31200, -39780, -53040, -66300, 0, 66300, 53040, 39780, 31200},
                    {-33150, -44200, -66300, -132600, 0, 132600, 66300, 44200, 33150 },
                    { -31200, -39780, -53040, -66300, 0, 66300, 53040, 39780, 31200},
                    {-26520, -30600, -33150, -26520, 0, 26520, 33150, 30600, 26520 },
                    { -21216, -22100, -20400, -13260, 0, 13260, 20400, 22100, 21216 },
                    { -16575, -15912, -13260, -7800, 0, 7800, 13260, 15912, 16575 } };
        #endregion

        #region SobelResult
        public List<double[,]> Sobel3x3x1 => MatrixOperation.RotateMatrix(Sobel3x3, 360);
        public List<double[,]> Sobel3x3x2 => MatrixOperation.TransponMatrix(Sobel3x3);
        public List<double[,]> Sobel3x3x4 => MatrixOperation.RotateMatrix(Sobel3x3, 90);
        public List<double[,]> Sobel3x3x8 => MatrixOperation.RotateMatrix(Sobel3x3, 45);

        public List<double[,]> Sobel5x5x1 => MatrixOperation.RotateMatrix(Sobel5x5, 360);
        public List<double[,]> Sobel5x5x2 => MatrixOperation.TransponMatrix(Sobel5x5);
        public List<double[,]> Sobel5x5x4 => MatrixOperation.RotateMatrix(Sobel5x5, 90);

        public List<double[,]> Sobel7x7x1 => MatrixOperation.RotateMatrix(Sobel7x7, 360);
        public List<double[,]> Sobel7x7x2 => MatrixOperation.TransponMatrix(Sobel7x7);
        public List<double[,]> Sobel7x7x4 => MatrixOperation.RotateMatrix(Sobel7x7, 90);

        public List<double[,]> Sobel9x9x1 => MatrixOperation.RotateMatrix(Sobel9x9, 360);
        public List<double[,]> Sobel9x9x2 => MatrixOperation.TransponMatrix(Sobel9x9);
        public List<double[,]> Sobel9x9x4 => MatrixOperation.RotateMatrix(Sobel9x9, 90);

        #endregion
    }
}
