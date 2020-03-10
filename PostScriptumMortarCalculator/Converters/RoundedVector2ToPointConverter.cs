using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using PostScriptumMortarCalculator.Utils;

namespace PostScriptumMortarCalculator.Converters
{
    public class RoundedVector2ToPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is RoundedVector2 vec2 ? new Point(vec2.X,vec2.Y) : new Point(0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}