using System;

namespace PostScriptumMortarCalculator.Extensions
{
    public static class NumericExtensions
    {
        public static double ToScaledMeters(this double pixelValue, double scale)
        {
            return Math.Round(pixelValue / scale, 2);
        }

        public static double PixelBoundsToScaledMeters(this double pixelBounds, double mapBounds)
        {
            return Math.Round(pixelBounds / mapBounds, 2);
        }
    }
}