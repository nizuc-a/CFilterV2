namespace ConvolutionFilter
{
    public class HighPassFilter : BaseFilter, IFilter
    {
        public ParameterInfo GetParameters() => new ParameterInfo() { NumberDimension = 2, NumberDirection = 2 };

        public override string ToString() => "HighPass фильтр";

        public override unsafe double ProcessFilter(byte*[] neigbor, double[,] filter) =>
            MatrixOperation.SumElements(neigbor, filter);
    }
}
