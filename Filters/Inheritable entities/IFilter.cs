using System.Collections.Generic;
using System.Drawing;

namespace ConvolutionFilter
{
    public interface IFilter
    {
        /// <summary>
        /// Этот метод должен возвращать описание параметров, которые появляются в NumericUpDown-контроле
        /// снизу от контрола выбора фильтра.
        /// </summary>
        /// <returns></returns>
  	    ParameterInfo GetParameters();

        /// <summary>
        /// Этот метод принимает фотографию, которую надо обрабатывать, и матрицу для обработки.
        /// Длина массива parameters в точности равна длине массива, возвращаемого методом GetParameters.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
		Bitmap Process(Bitmap original, List<double[,]> convolutionMatrix);
    }
}
