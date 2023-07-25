namespace ConvolutionFilter
{
    class GrayscaleFilter : BaseFilter, IFilter
    {
        public ParameterInfo GetParameters() => new ParameterInfo() { NumberDimension = 1 };

        public override string ToString() => "Оттенки серого";

        public override unsafe double ProcessFilter(byte*[] neigbor, double[,] filter) =>
            0.299 * neigbor[0][0] + 0.587 * neigbor[0][1] + 0.114 * neigbor[0][2];
    }
}
