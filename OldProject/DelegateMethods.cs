using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvolutionFilter
{
    public static class DelegateMethods
    {
        public static int SumElements(long[] neigborByte) => (int)neigborByte.Sum();

        public static int MedianElement(long[] neigborByte) => (int)neigborByte
                                                                .OrderBy(x => x)
                                                                .ToArray()[(neigborByte.Length + 1) / 2];

        public static int AverageElements(long[] neigborByte) => (int)neigborByte.Average();
    }
}
