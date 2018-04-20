using System;
using System.Globalization;
using System.Windows.Data;

namespace MediaShop.Converters
{
    /// <summary>
    /// Used in the Shopping cart
    /// </summary>
    class PriceLabelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //return new Tuple<int, decimal>((int)values[0], (decimal)values[1]); ;
            return values[0] + "*" + values[1];
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
