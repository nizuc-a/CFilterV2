using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class SharpnessKernel
    {
        #region Kernel
        public static double[,] Sharpness3x3 = new double[,]
                                               { { -1, -1, -1 },
                                                  { -1, 9, -1 },
                                                  { -1, -1, -1 } };
        #endregion

        #region Sharpness
        public List<double[,]> Sharpness => MatrixOperation.RotateMatrix(Sharpness3x3, 360);

        #endregion
    }
}
