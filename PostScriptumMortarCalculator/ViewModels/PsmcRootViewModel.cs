using System.Linq;
using System.Windows;
using MahApps.Metro;
using MahApps.Metro.Controls;
using PostScriptumMortarCalculator.Models;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public sealed class PsmcRootViewModel : Conductor<IScreen>
    {
        public UserConfigViewModel ConfigViewModel { get; set; }
        public MapViewModel MapViewModel { get; set; }
        public CalculatorViewModel CalculatorViewModel { get; set; }

        public PsmcRootViewModel(CalculatorViewModel calcViewModel,
            MapViewModel mapViewModel,
            UserConfigViewModel cfgViewModel)
        {
            MapViewModel = mapViewModel;
            ActivateItem(MapViewModel);
            CalculatorViewModel = calcViewModel;
            ActivateItem(CalculatorViewModel);
            ConfigViewModel = cfgViewModel;
            ActivateItem(ConfigViewModel);
        }

        public void OpenSettingsFlyout()
        {
            var flyout = ((MetroWindow) Application.Current.MainWindow).Flyouts.Items[0];
            ((Flyout) flyout).IsOpen = !((Flyout) flyout).IsOpen;
        }
    }
}