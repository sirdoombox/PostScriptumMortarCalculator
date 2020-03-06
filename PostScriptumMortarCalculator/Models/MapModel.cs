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

        public MortarData SelectedMortar { get; set; }

        public RoundedVector2 MortarPosition { get; set; }

        public RoundedVector2 MortarPositionMeters =>
            MortarPosition.ToMetersScale(MapPixelsPerMeter);

        public RoundedVector2 TargetPosition { get; set; }


        public RoundedVector2 TargetPositionMeters =>
            TargetPosition.ToMetersScale(MapPixelsPerMeter);

        public double Distance =>
            RoundedVector2.Distance(MortarPositionMeters, TargetPositionMeters);

        public double Angle =>
            RoundedVector2.Angle(MortarPositionMeters, TargetPositionMeters);

        public double MortarMinDistancePixels => SelectedMortar.MinRange.Distance.ToPixelScale(MapPixelsPerMeter);

        public double HalfMortarMinDistance => -(MortarMinDistancePixels / 2d);

        public double MortarMaxDistancePixels => SelectedMortar.MaxRange.Distance.ToPixelScale(MapPixelsPerMeter);

        public double HalfMortarMaxDistance => -(MortarMaxDistancePixels / 2d);

        public bool IsMortarPositionSet =>
            !MortarPosition.Equals(default);

        public bool IsTargetPositionSet =>
            !TargetPosition.Equals(default);

        public bool IsHelpVisible { get; set; } = true;

        public double MapPixelsPerMeter;

        public void Reset()
        {
            MortarPosition = default;
            TargetPosition = default;
            MapPixelsPerMeter = default;
        }
    }
}