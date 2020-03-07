using System;
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

        public double MortarMinDistancePixels =>
            SelectedMortar.MinRange.Distance.ToPixelScale(MapPixelsPerMeter) * 2d;

        public double HalfMortarMinDistance =>
            -(MortarMinDistancePixels / 2d);

        public double MortarMaxDistancePixels =>
            SelectedMortar.MaxRange.Distance.ToPixelScale(MapPixelsPerMeter) * 2d;

        public double HalfMortarMaxDistance =>
            -(MortarMaxDistancePixels / 2d);

        public RoundedVector2 TargetInnateSplashPixels =>
            RoundedVector2.LerpBetween(SelectedMortar.DispersionRadiusAtMinRange, 
                SelectedMortar.DispersionRadiusAtMaxRange,
                SelectedMortar.PercentageBetweenMinAndMaxDistance(Distance)).ToPixelScale(MapPixelsPerMeter) * 2;

        public RoundedVector2 TargetInnateSplashOffset => 
            -(TargetInnateSplashPixels / 2d);

        public bool IsMortarPositionSet =>
            !MortarPosition.Equals(default);

        public bool IsTargetPositionSet =>
            !TargetPosition.Equals(default);
        
        public double Milliradians
        {
            get
            {
                for (var i = 0; i < SelectedMortar.RangeValues.Count-1; i++)
                {
                    var curr = SelectedMortar.RangeValues[i];
                    var next = SelectedMortar.RangeValues[i + 1];
                    if (Distance < curr.Distance || Distance > next.Distance) continue;
                    var perc = Distance.PercentageBetween(curr.Distance, next.Distance);
                    return Math.Round(perc.LerpBetween(curr.Milliradians, next.Milliradians), 2);
                }

                return 0;
            }
        }

        public bool IsHelpVisible { get; set; } = true;

        public double Opacity { get; set; } = .25d;

        public double MapPixelsPerMeter;

        public void Reset(bool resetMapPixels = false)
        {
            MortarPosition = default;
            TargetPosition = default;
            if (!resetMapPixels) return;
            MapPixelsPerMeter = default;
        }

        
        
        public void OnSelectedMortarChanged()
        {
            NotifyOfPropertyChange(() => MortarMinDistancePixels);
            NotifyOfPropertyChange(() => HalfMortarMinDistance);
            NotifyOfPropertyChange(() => MortarMaxDistancePixels);
            NotifyOfPropertyChange(() => HalfMortarMaxDistance);
        }
    }
}