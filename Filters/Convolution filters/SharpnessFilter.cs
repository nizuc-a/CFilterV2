namespace ConvolutionFilter
{
    class SharpnessFilter : BaseFilter, IFilter
    {
        public ParameterInfo GetParameters() => new ParameterInfo() { NumberDimension = 1, NumberDirection = 1 };

        public override string ToString() => "Фильр Резкости";

        public override unsafe double ProcessFilter(byte*[] neigbor, double[,] filter) =>
           MatrixOperation.SumElements(neigbor, filter);
    }
}
