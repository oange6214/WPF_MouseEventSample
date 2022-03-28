using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFCanvas_Move_Drog.Converters
{
    public class HalfDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double visiable))
            {
                return visiable / 2;
            }

            return targetType;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.Parse(value.ToString()) * 2;
        }
    }
}
