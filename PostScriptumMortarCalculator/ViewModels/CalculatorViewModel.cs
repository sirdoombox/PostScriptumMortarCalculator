using System.Linq;
using System.Windows;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using PostScriptumMortarCalculator.ViewModels.Base;
using PropertyChanged;

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
        
        [SuppressPropertyChangedWarnings]
        public void OnMortarSelectionChanged()
        {
            Model.MapModel.SelectedMortar = Model.SelectedMortar;
        }
        
        [SuppressPropertyChangedWarnings]
        public void CopyToClipboardClicked()
        {
            Clipboard.SetData(DataFormats.Text, $"Angle: {Model.MapModel.Angle} | Mills: {Model.MapModel.Milliradians}");
        }
    }
}