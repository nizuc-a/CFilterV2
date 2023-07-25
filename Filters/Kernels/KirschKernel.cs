using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class KirschKernel
    {
        #region Kernel

        #endregion
        public static double[,] Kirsch3x3 = new double[,]
                                            { {  -3, -3,  5,  },
                                              {  -3,  0,  5,  },
                                              {  -3, -3,  5,  }, };

        public static double[,] Kirsch5x5 = new double[,]
                                            { {   9,  9,  9,  9,  9  },
                                               {   9,  5,  5,  5,  9  },
                                               {  -7, -3,  0, -3, -7  },
                                               {  -7, -3, -3, -3, -7  },
                                               {  -7, -7, -7, -7, -7  }, };
        #region Result

        public List<double[,]> Kirsch3x3x1 => MatrixOperation.RotateMatrix(Kirsch3x3, 360);
        public List<double[,]> Kirsch3x3x2 => MatrixOperation.TransponMatrix(Kirsch3x3);
        public List<double[,]> Kirsch3x3x4 => MatrixOperation.RotateMatrix(Kirsch3x3, 90);
        public List<double[,]> Kirsch3x3x8 => MatrixOperation.RotateMatrix(Kirsch3x3, 45);

        public List<double[,]> Kirsch5x5x1 => MatrixOperation.RotateMatrix(Kirsch5x5, 360);
        public List<double[,]> Kirsch5x5x2 => MatrixOperation.TransponMatrix(Kirsch5x5);
        public List<double[,]> Kirsch5x5x4 => MatrixOperation.RotateMatrix(Kirsch5x5, 90);

        #endregion
    }
}
