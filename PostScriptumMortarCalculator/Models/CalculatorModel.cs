using PostScriptumMortarCalculator.Data;
using Stylet;

namespace PostScriptumMortarCalculator.Models
{
    public class CalculatorModel : PropertyChangedBase
    {
        public MapModel MapModel { get; set; }
        
        public BindableCollection<MortarData> Mortars { get; set; } = new BindableCollection<MortarData>();
        
        public MortarData SelectedMortar { get; set; }
    }
}