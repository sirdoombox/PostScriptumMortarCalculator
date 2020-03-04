using PostScriptumMortarCalculator.Data;
using PostScriptumMortarCalculator.Extensions;
using PostScriptumMortarCalculator.Utils;
using Stylet;

namespace PostScriptumMortarCalculator.Models
{
    public class MapModel : PropertyChangedBase
    {
        public BindableCollection<MapData> AvailableMaps { get; set; }
        public MapData SelectedMap { get; set; }

        public double IconScale { get; set; }

        public Vector2 MortarPosition { get; set; }

        public Vector2 MortarPositionMeters => MortarPosition.ToMetersScale(MapPixelsPerMeter);
        
        public Vector2 MortarPositionIconOffset => new Vector2(MortarPosition.X - (IconScale/2d), MortarPosition.Y - IconScale);

        public Vector2 TargetPosition { get; set; }
        
        public Vector2 TargetPositionIconOffset => new Vector2(TargetPosition.X - (IconScale / 2d), TargetPosition.Y - (IconScale / 2d));

        public Vector2 TargetPositionMeters => TargetPosition.ToMetersScale(MapPixelsPerMeter);
        
        public bool IsMortarPositionSet => !MortarPosition.Equals(default);

        public bool IsTargetPositionSet => !TargetPosition.Equals(default);

        public double MapPixelsPerMeter;
        public double MapSizeBoundsPixels;
    }
}