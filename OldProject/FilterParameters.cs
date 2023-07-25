using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvolutionFilter
{
    public class FilterParameters
    {
        public enum SobelEnum
        {
            Sobel3x3x1,
            Sobel3x3x2,
            Sobel3x3x4,
            Sobel3x3x8,
            Sobel5x5x1,
            Sobel5x5x2,
            Sobel5x5x4,
            Sobel5x5x8,
            Sobel7x7x1,
            Sobel7x7x2,
            Sobel7x7x4,
            Sobel7x7x8,
            Sobel9x9x1,
            Sobel9x9x2,
            Sobel9x9x4,
            Sobel9x9x8

        }

        public enum PrewittEnum
        {
            Prewitt3x3x1,
            Prewitt3x3x2,
            Prewitt3x3x4,
            Prewitt3x3x8,
            Prewitt5x5x1,
            Prewitt5x5x2,
            Prewitt5x5x4,
            Prewitt5x5x8,
            Prewitt7x7x1,
            Prewitt7x7x2,
            Prewitt7x7x4,
            Prewitt7x7x8
        }

        public enum KirschEnum
        {
            Kirsch3x3x1,
            Kirsch3x3x2,
            Kirsch3x3x4,
            Kirsch3x3x8,
            Kirsch5x5x1,
            Kirsch5x5x2,
            Kirsch5x5x4,
            Kirsch5x5x8
        }

        public enum ScharrEnum
        {
            Scharr3x3x1,
            Scharr3x3x2,
            Scharr3x3x4,
            Scharr3x3x8,
            Scharr5x5x1,
            Scharr5x5x2,
            Scharr5x5x4,
            Scharr5x5x8
        }

        public enum IsotropicEnum
        {
            Isotropic3x3x1,
            Isotropic3x3x2,
            Isotropic3x3x4,
            Isotropic3x3x8
        }

        public enum LaplaceEnum
        {
            Laplace3x3,
            Laplace5x5
        }

        public enum GaussianEnum
        {
            Gaussian3x3,
            Gaussian5x5Type1,
            Gaussian5x5Type2

        }

        public enum HighPassEnum
        {
            HighPass3x3,
            HighPass5x5Type1,
            HighPass5x5Type2
        }

        public enum UniformEnum
        {
            Uniform3x3,
            Uniform5x5
        }
    }
}
