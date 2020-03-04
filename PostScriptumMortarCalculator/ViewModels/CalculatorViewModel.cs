using System.Linq;
using PostScriptumMortarCalculator.Data;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using PostScriptumMortarCalculator.ViewModels.Base;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class CalculatorViewModel : ConductorViewModelBase<CalculatorModel, MapViewModel>
    {
        
        public CalculatorViewModel(MapViewModel mapViewModel, DataResourceService dataResourceService) : base(mapViewModel)
        {
            Model.AvailableMaps = new BindableCollection<MapData>();
            Model.AvailableMaps.AddRange(dataResourceService.GetAvailableMapData());
            Model.SelectedMap = Model.AvailableMaps.First();
        }

        public void OnMapSelectionChanged()
        {
            
        }
    }
}