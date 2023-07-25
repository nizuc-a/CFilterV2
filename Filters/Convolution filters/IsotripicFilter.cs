namespace ConvolutionFilter
{
    public class IsotripicFilter : BaseFilter, IFilter
    {
        public ParameterInfo GetParameters() => new ParameterInfo() { NumberDimension = 1, NumberDirection = 4 };

        public override string ToString() => "Изотропный фильтр";

        public override unsafe double ProcessFilter(byte*[] neigbor, double[,] filter) =>
            MatrixOperation.SumElements(neigbor, filter);
    }
}
