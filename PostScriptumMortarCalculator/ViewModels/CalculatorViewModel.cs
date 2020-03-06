using System.Linq;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using PostScriptumMortarCalculator.ViewModels.Base;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class CalculatorViewModel : ConductorViewModelBase<CalculatorModel, MapViewModel>
    {
        
        public CalculatorViewModel(MapViewModel mapViewModel, DataResourceService dataResourceService) : base(mapViewModel)
        {
            Model.MapModel = mapViewModel.Model;
            Model.Mortars.AddRange(dataResourceService.GetMortarData());
            Model.SelectedMortar = Model.Mortars.First();
        }

        public void OnMortarSelectionChanged()
        {
            Model.MapModel.SelectedMortar = Model.SelectedMortar;
        }
    }
}