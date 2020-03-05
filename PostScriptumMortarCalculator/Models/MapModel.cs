using PostScriptumMortarCalculator.Data;
using PostScriptumMortarCalculator.Extensions;
using PostScriptumMortarCalculator.Utils;
using Stylet;

namespace PostScriptumMortarCalculator.Models
{
    public class MapModel : PropertyChangedBase
    {
        public BindableCollection<MapData> AvailableMaps { get; set; } = new BindableCollection<MapData>();
        public MapData SelectedMap { get; set; }

        public double IconScale { get; set; }

        public RoundedVector2 MortarPosition { get; set; }

        public RoundedVector2 MortarPositionMeters => 
            MortarPosition.ToMetersScale(MapPixelsPerMeter);

        public RoundedVector2 MortarPositionIconOffset => 
            new RoundedVector2(MortarPosition.X - (IconScale/2d), MortarPosition.Y - IconScale);

        public RoundedVector2 TargetPosition { get; set; }
        
        public RoundedVector2 TargetPositionIconOffset => 
            new RoundedVector2(TargetPosition.X - (IconScale / 2d), TargetPosition.Y - (IconScale / 2d));

        public RoundedVector2 TargetPositionMeters => 
            TargetPosition.ToMetersScale(MapPixelsPerMeter);
        
        public double Distance => 
            RoundedVector2.Distance(MortarPositionMeters, TargetPositionMeters);

        public double Angle =>
            RoundedVector2.Angle(MortarPositionMeters, TargetPositionMeters);
        
        public bool IsMortarPositionSet => 
            !MortarPosition.Equals(default);

        public bool IsTargetPositionSet => 
            !TargetPosition.Equals(default);

        public bool IsHelpVisible { get; set; } = true;

        public double MapPixelsPerMeter;

        public void Reset()
        {
            IconScale = default;
            MortarPosition = default;
            TargetPosition = default;
            MapPixelsPerMeter = default;
        }
    }
}