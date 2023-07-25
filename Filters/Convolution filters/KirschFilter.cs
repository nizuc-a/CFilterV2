namespace ConvolutionFilter
{
    public class KirschFilter : BaseFilter, IFilter
    {
        public ParameterInfo GetParameters() => new ParameterInfo() { NumberDimension = 2, NumberDirection = 3 };

        public override string ToString() => "Фильтр Кирша";

        public override unsafe double ProcessFilter(byte*[] neigbor, double[,] filter) =>
            MatrixOperation.SumElements(neigbor, filter);
    }
}
