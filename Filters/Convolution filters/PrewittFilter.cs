namespace ConvolutionFilter
{
    public class PrewittFilter : BaseFilter, IFilter
    {
        public ParameterInfo GetParameters() => new ParameterInfo() { NumberDimension = 7, NumberDirection = 3 };

        public override string ToString() => "Фильтр Прюитта";

        public override unsafe double ProcessFilter(byte*[] neigbor, double[,] filter) =>
            MatrixOperation.SumElements(neigbor, filter);
    }
}
