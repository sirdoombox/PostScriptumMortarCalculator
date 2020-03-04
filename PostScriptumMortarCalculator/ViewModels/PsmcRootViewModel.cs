using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public sealed class PsmcRootViewModel : Conductor<IScreen>.Collection.AllActive
    {
        public MapViewModel MapViewModel { get; set; }
        public CalculatorViewModel CalculatorViewModel { get; set; }

        public PsmcRootViewModel(CalculatorViewModel calculatorViewModel, MapViewModel mapViewModel)
        {
            mapViewModel.LinkToCalculator(calculatorViewModel);
            ActivateItem(calculatorViewModel);
            ActivateItem(mapViewModel);
            CalculatorViewModel = calculatorViewModel;
            MapViewModel = mapViewModel;
        }
    }
}