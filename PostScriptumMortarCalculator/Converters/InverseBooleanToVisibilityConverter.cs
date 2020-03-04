using System.Windows;

namespace PostScriptumMortarCalculator.Converters
{
    public class InverseBooleanToVisibilityConverter : BooleanConverterBase<Visibility>
    {
        public InverseBooleanToVisibilityConverter() : base(Visibility.Collapsed, Visibility.Visible) { }
    }
}