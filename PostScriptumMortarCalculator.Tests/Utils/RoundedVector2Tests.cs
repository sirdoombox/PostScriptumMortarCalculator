using NUnit.Framework;
using PostScriptumMortarCalculator.Utils;

namespace PostScriptumMortarCalculator.Tests.Utils
{
    [TestFixture]
    public class RoundedVector2Tests
    {
        private const double c_ACCURACY = 0.01d;
        
        // TODO: Add additional test cases.

        [Test]
        public void Constructor_LongNumbers_RoundCorrectly()
        {
            var roundedVector2 = new RoundedVector2(0.91919424243434d, 0.1919192424163d);
            Assert.That(roundedVector2.X, Is.EqualTo(0.919d).Within(c_ACCURACY));
            Assert.That(roundedVector2.Y, Is.EqualTo(0.192d).Within(c_ACCURACY));
        }

        [TestCase(0, 0, 2, 2, 2.828)]
        [TestCase(2, 3, 5, 3, 3)]
        [TestCase(7, 9, -13, -13, 29.732)]
        [TestCase(16, 10, 6, 6, 10.770)]
        [TestCase(1.232413, 1.14256, 1.9248253, 1.24828503, 0.700)]
        public void Distance_BetweenTwoVectors_CalculatedCorrectly(double v1X, double v1Y, double v2X, double v2Y,
            double shouldBeDistance)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var v2 = new RoundedVector2(v2X, v2Y);
            var dist = RoundedVector2.Distance(v1, v2);
            Assert.That(dist, Is.EqualTo(shouldBeDistance).Within(c_ACCURACY));
        }

        [TestCase(0, 0, 0, -1, 0)]
        [TestCase(0, 0, 1, 0, 90)]
        [TestCase(0, 0, 0, 1, 180)]
        [TestCase(0, 0, -1, 0, 270)]
        public void Angle_BetweenTwoVectors_CalculatedCorrectly(double v1X, double v1Y, double v2X, double v2Y,
            double shouldBeAngle)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var v2 = new RoundedVector2(v2X, v2Y);
            var angle = RoundedVector2.Angle(v1, v2);
            Assert.That(angle, Is.EqualTo(shouldBeAngle).Within(c_ACCURACY));
        }

        [TestCase(0, 0, 1, 1, 0.5, 0.5, 0.5)]
        public void LepBetween_OfTwoVectors_CalculatedCorrectly(double v1X, double v1Y, double v2X, double v2Y,
            double percent, double shouldBeX, double shouldBeY)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var v2 = new RoundedVector2(v2X, v2Y);
            var shouldBe = new RoundedVector2(shouldBeX, shouldBeY);
            var lerpedValue = RoundedVector2.LerpBetween(v1, v2, percent);
            Assert.That(lerpedValue.X, Is.EqualTo(shouldBe.X).Within(c_ACCURACY));
            Assert.That(lerpedValue.Y, Is.EqualTo(shouldBe.Y).Within(c_ACCURACY));
        }

        [TestCase(1, 1, 2, 0.5, 0.5)]
        public void Division_OfVectorByDouble_CalculatedCorrectly(double v1X, double v1Y,
            double divideBy, double shouldBeX, double shouldBeY)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var shouldBe = new RoundedVector2(shouldBeX, shouldBeY);
            var dividedValue = v1 / divideBy;
            Assert.That(dividedValue.X, Is.EqualTo(shouldBe.X).Within(c_ACCURACY));
            Assert.That(dividedValue.Y, Is.EqualTo(shouldBe.Y).Within(c_ACCURACY));
        }

        [TestCase(5, 5, 2, 2, 2.5, 2.5)]
        public void Division_OfTwoVectors_CalculatedCorrectly(double v1X, double v1Y, double v2X, double v2Y,
            double shouldBeX, double shouldBeY)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var v2 = new RoundedVector2(v2X, v2Y);
            var shouldBe = new RoundedVector2(shouldBeX, shouldBeY);
            var dividedValue = v1 / v2;
            Assert.That(dividedValue.X, Is.EqualTo(shouldBe.X).Within(c_ACCURACY));
            Assert.That(dividedValue.Y, Is.EqualTo(shouldBe.Y).Within(c_ACCURACY));
        }
        
        [TestCase(10,12, -10,-12)]
        public void Negation_OfVector_CalculatedCorrect(double v1X, double v1Y, double shouldBeX, double shouldBeY)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var shouldBe = new RoundedVector2(shouldBeX, shouldBeY);
            var negatedValue = -v1;
            Assert.That(negatedValue.X, Is.EqualTo(shouldBe.X).Within(c_ACCURACY));
            Assert.That(negatedValue.Y, Is.EqualTo(shouldBe.Y).Within(c_ACCURACY));
        }
        
        [TestCase(1,1,2,2,2)]
        public void Multiplication_OfVectorByDouble_CalculatedCorrectly(double v1X, double v1Y,
            double multiplyBy, double shouldBeX, double shouldBeY)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var shouldBe = new RoundedVector2(shouldBeX, shouldBeY);
            var multipliedValue = v1*multiplyBy;
            Assert.That(multipliedValue.X, Is.EqualTo(shouldBe.X).Within(c_ACCURACY));
            Assert.That(multipliedValue.Y, Is.EqualTo(shouldBe.Y).Within(c_ACCURACY));
        }
        
        [TestCase(1,1,1,1)]
        public void Equality_OfTwoIdenticalVectors_AreEqual(double v1X, double v1Y, double v2X, double v2Y)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var v2 = new RoundedVector2(v2X, v2Y);
            Assert.That(v1 == v2);
            Assert.That(v2 == v1);
        }
        
        [TestCase(1,1,1,1)]
        public void Equality_OfVectorAndIdenticalVectorCastToObject_AreEqual(double v1X, double v1Y, double v2X,
            double v2Y)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            object v2 = new RoundedVector2(v2X, v2Y);
            Assert.That(v1 == v2);
            Assert.That(v2 == v1);
        }
        
        [TestCase(1,1,2,2)]
        public void Equality_OfTwoDifferentVectors_AreUnequal(double v1X, double v1Y, double v2X, double v2Y)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var v2 = new RoundedVector2(v2X, v2Y);
            Assert.That(v1 != v2);
            Assert.That(v2 != v1);
        }
        
        [TestCase(1,1,2,2)]
        public void Equality_OfVectorAndDifferentVectorCastToObject_AreUnequalEqual(double v1X, double v1Y, double v2X,
            double v2Y)
        {
            var v1 = new RoundedVector2(v1X, v1Y);
            var v2 = new RoundedVector2(v2X, v2Y);
            Assert.That(v1 != v2);
            Assert.That(v2 != v1);
        }
    }
}