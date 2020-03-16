using System.Windows.Media;

namespace PostScriptumMortarCalculator.Events
{
    public class ColorChangedEvent
    {
        public enum Type { Mortar, Target }
        public Type TypeChanged { get; }
        public Color NewColor { get; }

        public ColorChangedEvent(Type typeChanged, Color newColor)
        {
            TypeChanged = typeChanged;
            NewColor = newColor;
        }
    }
}