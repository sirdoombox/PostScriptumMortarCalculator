using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public sealed class PsmcRootViewModel : Conductor<IScreen>.Collection.AllActive
    {
        public MapViewModel MapViewModel { get; set; }

        public PsmcRootViewModel(MapViewModel mapViewModel)
        {
            ActivateItem(mapViewModel);
            MapViewModel = mapViewModel;
        }
    }
}