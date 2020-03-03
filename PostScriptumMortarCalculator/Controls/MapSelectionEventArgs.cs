using System.Windows;

namespace PostScriptumMortarCalculator.Controls
{
    public class MapSelectionEventArgs : RoutedEventArgs
    {
        public MapSelectionEventType Type { get; }

        public MapSelectionEventArgs(MapSelectionEventType type)
        {
            Type = type;
        }
    }

    public enum MapSelectionEventType
    {
        Mortar,
        Target
    }
}