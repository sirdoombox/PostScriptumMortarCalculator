using System.Collections.Generic;
using System.Linq;
using PostScriptumMortarCalculator.Utils;
using PropertyChanged;

namespace PostScriptumMortarCalculator.Models
{
    [DoNotNotify]
    public class MortarDataModel
    {
        public string Name { get; }
        public RoundedVector2 DispersionRadiusAtMinRange { get; }
        public RoundedVector2 DispersionRadiusAtMaxRange { get; }
        public IReadOnlyList<MortarRangeValue> RangeValues { get; }

        public MortarRangeValue MinRange => RangeValues.First();
        
        public MortarRangeValue MaxRange => RangeValues.Last();

        public MortarDataModel(string mortarName, RoundedVector2 dispersionRadiusAtMinRange,
            RoundedVector2 dispersionRadiusAtMaxRange, List<MortarRangeValue> rangeValues)
        {
            Name = mortarName;
            DispersionRadiusAtMinRange = dispersionRadiusAtMinRange;
            DispersionRadiusAtMaxRange = dispersionRadiusAtMaxRange;
            RangeValues = rangeValues.OrderBy(x => x.Distance).ToList();
        }
        
        
        public struct MortarRangeValue
        {
            public double Distance { get; }

            public double Milliradians { get; }

            public MortarRangeValue(double distance, double milliradians)
            {
                Distance = distance;
                Milliradians = milliradians;
            }
        }
    }
}