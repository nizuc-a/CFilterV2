using System.Collections.Generic;

namespace ConvolutionFilter
{
    public class UniformKernel
    {
        #region Kernel
        public static double[,] Uniform3x3 = new double[,]
                                                {{0,1,0 },
                                                { 1,1,1},
                                                {0,1,0 } };

        public static double[,] Uniform5x5 = new double[,]
                                                {{0,1,1,1,0 },
                                                 {1,1,1,1,1 },
                                                 {1,1,1,1,1 },
                                                 {1,1,1,1,1 },
                                                 {0,1,1,1,0 }};
        #endregion

        #region Result
        public List<double[,]> Uniform3x3x1 => MatrixOperation.RotateMatrix(Uniform3x3, 360);

        public List<double[,]> Uniform5x5x1 => MatrixOperation.RotateMatrix(Uniform5x5, 360);

        #endregion
    }
}
