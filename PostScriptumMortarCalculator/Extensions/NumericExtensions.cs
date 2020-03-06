using System;
using System.Numerics;
using PostScriptumMortarCalculator.Data;
using PostScriptumMortarCalculator.Utils;

namespace PostScriptumMortarCalculator.Extensions
{
    public static class NumericExtensions
    {
        public static double ToScaledMeters(this double pixelValue, double pixelsPerMeter)
        {
            return Math.Round(pixelValue / pixelsPerMeter, 2);
        }

        public static double ToPixelScale(this double meterValue, double pixelsPerMeter)
        {
            return Math.Round(meterValue * pixelsPerMeter, 2);
        }

        public static double PixelBoundsToPixelsPerMeter(this double pixelBounds, double mapBounds)
        {
            return Math.Round(pixelBounds / mapBounds, 2);
        }
        
        public static RoundedVector2 ToMetersScale(this RoundedVector2 vector, double pixelsPerMeter)
        {
            return new RoundedVector2(vector.X.ToScaledMeters(pixelsPerMeter), vector.Y.ToScaledMeters(pixelsPerMeter));
        }

        public static RoundedVector2 ToPixelScale(this RoundedVector2 vector, double pixelsPerMeter)
        {
            return new RoundedVector2(vector.X.ToPixelScale(pixelsPerMeter), vector.Y.ToPixelScale(pixelsPerMeter));
        }

        public static double PercentageBetweenMinAndMaxDistance(this MortarData mortarData, double distance)
        {
            var minDist = mortarData.MinRange.Distance;
            var maxDist = mortarData.MaxRange.Distance;
            if(distance > maxDist || distance < minDist) throw new ArgumentOutOfRangeException(nameof(distance));
            return (distance - minDist) / (maxDist - minDist);
        }
    }
}