using System;
using PostScriptumMortarCalculator.Utils;

namespace PostScriptumMortarCalculator.Extensions
{
    public static class NumericExtensions
    {
        public static double ToScaledMeters(this double pixelValue, double pixelsPerMeter)
        {
            return Math.Round(pixelValue / pixelsPerMeter, 2);
        }

        public static double PixelBoundsToPixelsPerMeter(this double pixelBounds, double mapBounds)
        {
            return Math.Round(pixelBounds / mapBounds, 2);
        }

        public static RoundedVector2 ToMetersScale(this RoundedVector2 vector, double pixelsPerMeter)
        {
            return new RoundedVector2(vector.X.ToScaledMeters(pixelsPerMeter), vector.Y.ToScaledMeters(pixelsPerMeter));
        }
    }
}