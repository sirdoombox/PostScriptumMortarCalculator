using PostScriptumMortarCalculator.Data;

namespace PostScriptumMortarCalculator.Events
{
    public class MapSelectionChangedEvent
    {
        public MapData NewMap { get; }

        public MapSelectionChangedEvent(MapData newMap)
        {
            NewMap = newMap;
        }
    }
}