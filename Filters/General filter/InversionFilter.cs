namespace ConvolutionFilter
{
    class InversionFilter : BaseFilter, IFilter
    {
        public ParameterInfo GetParameters() => new ParameterInfo() { NumberDimension = 1 };

        public override string ToString() => "Инверсия";

        public override unsafe double ProcessFilter(byte*[] neigbor, double[,] filter) =>
            255 - neigbor[0][0];
    }
}
