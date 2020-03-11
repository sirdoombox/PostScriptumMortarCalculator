using System.Collections.Generic;
using NUnit.Framework;
using PostScriptumMortarCalculator.Extensions;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Utils;

namespace PostScriptumMortarCalculator.Tests.Extensions
{
    [TestFixture]
    public class NumericExtensionsTests
    {
        // TODO: More test cases.
        private const double c_PRECISION = 0.01d;

        [TestCase(10, 10, 2, 5, 5)]
        [TestCase(100, 50, 5, 20, 10)]
        public void RoundedVector2ToMetersScale_PassedValues_ReturnsCorrectResult(double vX, double vY,
            double pixelsPerMeter, double vShouldBeX, double vShouldBeY)
        {
            var v = new RoundedVector2(vX, vY);
            var vShouldBe = new RoundedVector2(vShouldBeX, vShouldBeY);
            var scaledV = v.ToMetersScale(pixelsPerMeter);
            Assert.That(scaledV.X, Is.EqualTo(vShouldBe.X).Within(c_PRECISION));
            Assert.That(scaledV.Y, Is.EqualTo(vShouldBe.Y).Within(c_PRECISION));
        }

        [TestCase(10, 10, 2, 20, 20)]
        [TestCase(100, 50, 5, 500, 250)]
        public void RoundedVector2ToPixelScale_PassedValues_ReturnsCorrectResult(double vX, double vY,
            double pixelsPerMeter, double vShouldBeX, double vShouldBeY)
        {
            var v = new RoundedVector2(vX, vY);
            var vShouldBe = new RoundedVector2(vShouldBeX, vShouldBeY);
            var scaledV = v.ToPixelScale(pixelsPerMeter);
            Assert.That(scaledV.X, Is.EqualTo(vShouldBe.X).Within(c_PRECISION));
            Assert.That(scaledV.Y, Is.EqualTo(vShouldBe.Y).Within(c_PRECISION));
        }

        [TestCase(0, 1000, 500, 0.5)]
        public void PercentageBetweenMinAndMaxDistance_PassedValues_ReturnsCorrectResult(double minRange,
            double maxRange, double distanceBetween, double shouldBePercentage)
        {
            var mortarData = new MortarDataModel(string.Empty, RoundedVector2.Zero, RoundedVector2.Zero,
                new List<MortarDataModel.MortarRangeValue>
                {
                    new MortarDataModel.MortarRangeValue(minRange, minRange),
                    new MortarDataModel.MortarRangeValue(maxRange, maxRange)
                });
            var percentageBetween = mortarData.PercentageBetweenMinAndMaxDistance(distanceBetween);
            Assert.That(percentageBetween, Is.EqualTo(shouldBePercentage).Within(c_PRECISION));
        }

        [TestCase(0, 1000, 1001)]
        public void PercentageBetweenMinAndMaxDistance_PassedIncorrectValues_ReturnsLessThanZero(double minRange,
            double maxRange, double distanceBetween)
        {
            var mortarData = new MortarDataModel(string.Empty, RoundedVector2.Zero, RoundedVector2.Zero,
                new List<MortarDataModel.MortarRangeValue>
                {
                    new MortarDataModel.MortarRangeValue(minRange, minRange),
                    new MortarDataModel.MortarRangeValue(maxRange, maxRange)
                });
            var percentageBetween = mortarData.PercentageBetweenMinAndMaxDistance(distanceBetween);
            Assert.That(percentageBetween, Is.LessThan(0));
        }
        
        [TestCase(0,100,50,0.5)]
        public void PercentageBetween_PassedValues_ReturnsCorrectResult(double min, double max, double value,
            double shouldBePercentage)
        {
            Assert.That(value.PercentageBetween(min,max), Is.EqualTo(shouldBePercentage).Within(c_PRECISION));
        }
        
        [TestCase(0,100,0.5,50)]
        public void LerpBetween_PassedValues_ReturnsCorrectResult(double min, double max, double percentage,
            double shouldBeValue)
        {
            Assert.That(percentage.LerpBetween(min,max), Is.EqualTo(shouldBeValue).Within(c_PRECISION));
        }
    }
}