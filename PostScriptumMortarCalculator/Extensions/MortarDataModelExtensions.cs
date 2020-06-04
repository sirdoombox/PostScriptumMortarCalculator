using System;
using PostScriptumMortarCalculator.Models;

namespace PostScriptumMortarCalculator.Extensions
{
    public static class MortarDataModelExtensions
    {
        public static double GetMilliradiansForDistance(this MortarDataModel mortarData, double distance)
        {
            for (var i = 0; i < mortarData.RangeValues.Count-1; i++)
            {
                var curr = mortarData.RangeValues[i];
                var next = mortarData.RangeValues[i + 1];
                if (distance < curr.Distance || distance > next.Distance) continue;
                var percentage = distance.PercentageBetween(curr.Distance, next.Distance);
                return Math.Round(percentage.LerpBetween(curr.Milliradians, next.Milliradians), 2);
            }
            return 0;
        }
    }
}