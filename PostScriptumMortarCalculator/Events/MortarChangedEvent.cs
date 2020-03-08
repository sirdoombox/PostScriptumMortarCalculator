using PostScriptumMortarCalculator.Models;

namespace PostScriptumMortarCalculator.Events
{
    public class MortarChangedEvent
    {
        public MortarDataModel ActiveMortar { get; }

        public MortarChangedEvent(MortarDataModel activeMortar)
        {
            ActiveMortar = activeMortar;
        }
    }
}