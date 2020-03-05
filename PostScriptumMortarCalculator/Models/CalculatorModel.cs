using System.Windows;
using PostScriptumMortarCalculator.Data;
using PostScriptumMortarCalculator.Utils;
using PostScriptumMortarCalculator.ViewModels;
using Stylet;

namespace PostScriptumMortarCalculator.Models
{
    public class CalculatorModel : PropertyChangedBase
    {
        public MapModel MapModel { get; set; }
    }
}