using System;
using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class IsotripicKernel
    {
        #region Kernel
        public static double[,] Isotripic3x3 = new double[,]
                                              { {            -1,  0,             1,  },
                                                 {  -Math.Sqrt(2), 0,  Math.Sqrt(2),  },
                                                 {            -1,  0,             1,  },  };

        #endregion

        #region Result

        public List<double[,]> Isotropic3x3x1 => MatrixOperation.RotateMatrix(Isotripic3x3, 360);
        public List<double[,]> Isotripic3x3x2 => MatrixOperation.TransponMatrix(Isotripic3x3);
        public List<double[,]> Isotropic3x3x4 => MatrixOperation.RotateMatrix(Isotripic3x3, 90);
        public List<double[,]> Isotropic3x3x8 => MatrixOperation.RotateMatrix(Isotripic3x3, 45);

        #endregion
    }
}
