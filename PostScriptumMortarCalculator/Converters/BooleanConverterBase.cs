using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace PostScriptumMortarCalculator.Converters
{
    public class BooleanConverterBase<T> : IValueConverter    
    {
        public BooleanConverterBase(T trueValue, T falseValue)
        {
            m_true = trueValue;
            m_false = falseValue;
        }

        private readonly T m_true;
        private readonly T m_false;

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool val && val ? m_true : m_false;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T val && EqualityComparer<T>.Default.Equals(val, m_true);
        }
    }
}