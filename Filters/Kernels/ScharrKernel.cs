using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class ScharrKernel
    {
        #region Kernel
        public static double[,] Scharr3x3 = new double[,]
                                            { {  -1,  0,  1,  },
                                              {  -3,  0,  3,  },
                                              {  -1,  0,  1,  }, };

        public static double[,] Scharr5x5 = new double[,]
                                             { {   -1,  -1,  0,   1,  1,  },
                                               {   -2,  -2,  0,   2,  2,  },
                                               {   -3,  -6,  0,   6,  3,  },
                                               {   -2,  -2,  0,   2,  2,  },
                                               {   -1,  -1,  0,   1,  1,  }, };
        #endregion

        #region Result

        public List<double[,]> Scharr3x3x1 => MatrixOperation.RotateMatrix(Scharr3x3, 360);
        public List<double[,]> Scharr3x3x2 => MatrixOperation.TransponMatrix(Scharr3x3);
        public List<double[,]> Scharr3x3x4 => MatrixOperation.RotateMatrix(Scharr3x3, 90);
        public List<double[,]> Scharr3x3x8 => MatrixOperation.RotateMatrix(Scharr3x3, 45);
        public List<double[,]> Scharr5x5x1 => MatrixOperation.RotateMatrix(Scharr5x5, 360);
        public List<double[,]> Scharr5x5x2 => MatrixOperation.TransponMatrix(Scharr5x5);
        public List<double[,]> Scharr5x5x4 => MatrixOperation.RotateMatrix(Scharr5x5, 90);

        #endregion
    }
}
