using System;
using System.Linq;
using PostScriptumMortarCalculator.Data;
using PostScriptumMortarCalculator.Events;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class CalculatorViewModel : ViewModelBase<CalculatorModel>
    {
        public Action<MapData> MapSelectionChanged { get; set; }
        
        public CalculatorViewModel(DataResourceService dataResourceService)
        {
            Model.AvailableMaps = new BindableCollection<MapData>();
            Model.AvailableMaps.AddRange(dataResourceService.GetAvailableMapData());
            Model.SelectedMap = Model.AvailableMaps.First();
        }

        public void OnMapSelectionChanged()
        {
            MapSelectionChanged?.Invoke(Model.SelectedMap);
        }
    }
}