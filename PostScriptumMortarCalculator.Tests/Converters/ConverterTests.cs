using System.Windows;
using NUnit.Framework;
using PostScriptumMortarCalculator.Converters;

namespace PostScriptumMortarCalculator.Tests.Converters
{
    [TestFixture]
    public class ConverterTests
    {
        [Test]
        public void BooleanToVisibilityConverter_PassedBooleans_ConvertsCorrectly()
        {
            var converter = new BooleanToVisibilityConverter();
            Assert.That(converter.Convert(true, null, null, null), Is.EqualTo(Visibility.Visible));
            Assert.That(converter.Convert(false, null, null, null), Is.EqualTo(Visibility.Collapsed));
        }

        [Test]
        public void InverseBooleanConverter_PassedBooleans_ConvertsCorrectly()
        {
            var converter = new InverseBooleanConverter();
            Assert.That(converter.Convert(true, null, null, null), Is.EqualTo(false));
            Assert.That(converter.Convert(false, null, null, null), Is.EqualTo(true));
        }

        [Test]
        public void InverseBooleanToVisibilityConverter_PassedBooleans_ConvertsCorrectly()
        {
            var converter = new InverseBooleanToVisibilityConverter();
            Assert.That(converter.Convert(false, null, null, null), Is.EqualTo(Visibility.Visible));
            Assert.That(converter.Convert(true, null, null, null), Is.EqualTo(Visibility.Collapsed));
        }

        [TestCase(1, 0.5)]
        [TestCase(100.5, 50.25)]
        public void HalfDoubleConverter_PassedDoubles_HalvesCorrectly(double value, double expectedHalf)
        {
            var converter = new HalfDoubleConverter();
            var converted = converter.Convert(value, null, null, null);
            Assert.That(converted, Is.EqualTo(expectedHalf).Within(0.001d));
        }
        
        // Currently not testable outside of the scope of a WPF app
        // Might be a workaround but it will have to go without it for now.
        // [TestCase("Orange")]
        // [TestCase("lime")]
        // [TestCase("MAUVE")]
        // [TestCase("YeLlOw")]
        // public void StringToBrushConverter_PassedValidStrings_ReturnsBrush(string brushName)
        // {
        //     var converter = new StringToAccentBrushConverter();
        //     var returnedBrush = converter.Convert(brushName, null, null, null);
        //     Assert.That(returnedBrush, Is.Not.Null.And.Not.EqualTo(Brushes.White));
        // }
    }
}