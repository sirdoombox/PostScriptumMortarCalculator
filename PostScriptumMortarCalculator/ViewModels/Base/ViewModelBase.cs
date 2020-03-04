using Stylet;

namespace PostScriptumMortarCalculator.ViewModels.Base
{
    public class ViewModelBase<TModel> : Screen where TModel : PropertyChangedBase, new()
    {
        public TModel Model { get; set; } = new TModel();
    }
}