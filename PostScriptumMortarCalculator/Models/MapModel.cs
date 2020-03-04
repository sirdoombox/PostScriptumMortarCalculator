using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using PostScriptumMortarCalculator.Data;
using PostScriptumMortarCalculator.Extensions;
using Stylet;

namespace PostScriptumMortarCalculator.Models
{
    public class MapModel : PropertyChangedBase
    {
        public MapData Data { get; set; }

        public double IconScale { get; set; }

        public double MortarPositionTop { get; set; }

        public double MortarPositionLeft { get; set; }

        public double MortarPositionTopWithOffset => MortarPositionTop - IconScale;

        public double MortarPositionLeftWithOffset => MortarPositionLeft - (IconScale / 2d);


        public double TargetPositionTop { get; set; }

        public double TargetPositionLeft { get; set; }

        public double TargetPositionTopWithOffset => TargetPositionTop - (IconScale / 2d);

        public double TargetPositionLeftWithOffset => TargetPositionLeft - (IconScale / 2d);

        public bool IsMortarPositionSet => !MortarPositionTop.Equals(default) || !MortarPositionLeft.Equals(default);

        public bool IsTargetPositionSet => !TargetPositionTop.Equals(default) || !TargetPositionLeft.Equals(default);

        public double MapPixelsPerMeter;
        public double MapSizeBoundsPixels;

        public Vector2 MortarPositionMeters =>
            new Vector2((float) MortarPositionLeft.ToScaledMeters(MapPixelsPerMeter),
                (float) MortarPositionTop.ToScaledMeters(MapPixelsPerMeter));
        
        public Vector2 TargetPositionMeters =>
            new Vector2((float) TargetPositionLeft.ToScaledMeters(MapPixelsPerMeter),
                (float) TargetPositionTop.ToScaledMeters(MapPixelsPerMeter));
    }
}