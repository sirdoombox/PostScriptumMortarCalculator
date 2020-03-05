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
        
        public CalculatorViewModel(MapViewModel mapViewModel) : base(mapViewModel)
        {
            Model.MapModel = mapViewModel.Model;
        }
    }
}