using System;
using PostScriptumMortarCalculator.Models;
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

        public static double PercentageBetweenMinAndMaxDistance(this MortarDataModel mortarData, double distance)
        {
            var minDist = mortarData.MinRange.Distance;
            var maxDist = mortarData.MaxRange.Distance;
            if (distance > maxDist || distance < minDist) return -1;
            return PercentageBetween(distance, minDist, maxDist);
        }

        public static double PercentageBetween(this double value, double min, double max)
        {
            return (value - min) / (max - min);
        }

        public static double LerpBetween(this double perc, double min, double max)
        {
            return min + (max - min) * perc;
        }
        
        public static string ConvertMetersPositionToMapCoordsString(this RoundedVector2 metersPosition)
        {
            const int startLetter = 65;
            const int startNumber = 1;
            var rsltString = string.Empty;
            rsltString += (char) (startLetter + Math.Floor(metersPosition.X / 300));
            rsltString += startNumber + Math.Floor(metersPosition.Y / 300) + "-";
            var localised = metersPosition % 300;
            var yPart = 7 - (Math.Floor(localised.Y / 100) * 3);
            var zPart = Math.Floor(localised.X / 100);
            rsltString += yPart + zPart + "-";
            var furtherLocalised = localised % 100;
            yPart = 7 - (Math.Floor(furtherLocalised.Y / 33) * 3);
            zPart = Math.Floor(furtherLocalised.X / 33);
            rsltString += yPart + zPart;
            return rsltString;
        }
    }
}