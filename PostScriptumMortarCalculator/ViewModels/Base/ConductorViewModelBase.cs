using Stylet;

namespace PostScriptumMortarCalculator.ViewModels.Base
{
    public class ConductorViewModelBase<TModel, TViewModel> : Conductor<TViewModel>
        where TViewModel : class, IScreen where TModel : PropertyChangedBase, new()
    {
        public TModel Model { get; set; } = new TModel();

        public ConductorViewModelBase(TViewModel viewModel)
        {
            ActiveItem = viewModel;
        }
    }
}