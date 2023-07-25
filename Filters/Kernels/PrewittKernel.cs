using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class PrewittKernel
    {
        #region PrewittKernels
        public static double[,] Prewitt3x3 = new double[,]
                                             { {  -1,  0,  1,  },
                                               {  -1,  0,  1,  },
                                               {  -1,  0,  1,  }, };

        public static double[,] Prewitt5x5 = new double[,]
                                             { {  -2, -1,  0,  1, 2,  },
                                               {  -2, -1,  0,  1, 2,  },
                                               {  -2, -1,  0,  1, 2,  },
                                               {  -2, -1,  0,  1, 2,  },
                                               {  -2, -1,  0,  1, 2,  }, };

        public static double[,] Prewitt7x7 = new double[,]
                                             { { -3, -2, -1,  0,  1, 2, 3 },
                                               {  -3,-2, -1,  0,  1, 2,  3},
                                               {  -3,-2, -1,  0,  1, 2,  3},
                                               {  -3,-2, -1,  0,  1, 2,  3},
                                               {  -3,-2, -1,  0,  1, 2,  3},
                                               {  -3,-2, -1,  0,  1, 2,  3},
                                               {  -3,-2, -1,  0,  1, 2,  3}, };
        #endregion

        #region PrewittResult

        public static List<double[,]>Prewitt3x3x1 => MatrixOperation.RotateMatrix(Prewitt3x3, 360);
        public static List<double[,]>Prewitt3x3x2 => MatrixOperation.TransponMatrix(Prewitt3x3);
        public static List<double[,]>Prewitt3x3x4 => MatrixOperation.RotateMatrix(Prewitt3x3, 90);
        public static List<double[,]>Prewitt3x3x8 => MatrixOperation.RotateMatrix(Prewitt3x3, 45);

        public static List<double[,]>Prewitt5x5x1 => MatrixOperation.RotateMatrix(Prewitt5x5, 360);
        public static List<double[,]>Prewitt5x5x2 => MatrixOperation.TransponMatrix(Prewitt5x5);
        public static List<double[,]>Prewitt5x5x4 => MatrixOperation.RotateMatrix(Prewitt5x5, 90);

        public static List<double[,]>Prewitt7x7x1 => MatrixOperation.RotateMatrix(Prewitt7x7, 360);
        public static List<double[,]>Prewitt7x7x2 => MatrixOperation.TransponMatrix(Prewitt7x7);
        public static List<double[,]> Prewitt7x7x4 => MatrixOperation.RotateMatrix(Prewitt7x7, 90);

        #endregion
    }
}
