using PostScriptumMortarCalculator.Data;
using Stylet;

namespace PostScriptumMortarCalculator.Models
{
    public class CalculatorModel : PropertyChangedBase
    {
        public BindableCollection<MapData> AvailableMaps { get; set; }

        public MapData SelectedMap { get; set; }
    }
}