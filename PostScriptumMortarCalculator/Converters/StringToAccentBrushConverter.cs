using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using MahApps.Metro;

namespace PostScriptumMortarCalculator.Converters
{
    public class StringToAccentBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string accentName ? ThemeManager.Themes.First(x => x.ColorScheme == accentName).ShowcaseBrush : Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}