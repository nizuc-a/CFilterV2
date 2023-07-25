namespace ConvolutionFilter
{
    public class SobelFilter : BaseFilter, IFilter
    {
        public ParameterInfo GetParameters() => 
            new ParameterInfo() { NumberDimension = 9, NumberDirection = 3 };

        public override string ToString() => "Фильтр Собеля";

        public override unsafe double ProcessFilter(byte*[] neigbor, double[,] filter) =>
           MatrixOperation.SumElements(neigbor, filter);
    }
}
