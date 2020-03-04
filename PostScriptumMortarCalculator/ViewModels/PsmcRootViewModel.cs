using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.ViewModels.Base;

namespace PostScriptumMortarCalculator.ViewModels
{
    public sealed class PsmcRootViewModel : ConductorViewModelBase<PsmcRootModel, CalculatorViewModel>
    {
        public PsmcRootViewModel(CalculatorViewModel calcViewModel) : base(calcViewModel)
        {
            
        }
    }
}