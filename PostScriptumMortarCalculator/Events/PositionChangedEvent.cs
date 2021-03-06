using PostScriptumMortarCalculator.Utils;

namespace PostScriptumMortarCalculator.Events
{
    public class PositionChangedEvent
    {
        public RoundedVector2 MortarPositionMeters { get; }
        public RoundedVector2 TargetPositionMeters { get; }
        public double Angle { get; }

        public double MapBounds { get; }

        public PositionChangedEvent(RoundedVector2 mortarPositionMeters,
            RoundedVector2 targetPositionMeters,
            double angle,
            double mapBounds)
        {
            MortarPositionMeters = mortarPositionMeters;
            TargetPositionMeters = targetPositionMeters;
            Angle = angle;
            MapBounds = mapBounds;
        }
    }
}