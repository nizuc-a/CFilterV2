﻿namespace ConvolutionFilter
{
    public class GaussFilter : BaseFilter, IFilter
    {
        public ParameterInfo GetParameters() => new ParameterInfo() { NumberDimension = 2, NumberDirection = 1 };

        public override string ToString() => "Фильтр Гаусса";

        public override unsafe double ProcessFilter(byte*[] neigbor, double[,] filter) =>
            MatrixOperation.SumElements(neigbor, filter);
    }
}
