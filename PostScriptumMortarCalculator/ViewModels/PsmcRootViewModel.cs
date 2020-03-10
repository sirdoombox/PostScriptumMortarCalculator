using System.Windows;
using MahApps.Metro.Controls;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public sealed class PsmcRootViewModel : Conductor<IScreen>
    {
        public MenuFlyoutViewModel MenuFlyoutViewModel { get; set; }
        public MapViewModel MapViewModel { get; set; }
        public CalculatorViewModel CalculatorViewModel { get; set; }

        public PsmcRootViewModel(CalculatorViewModel calcViewModel,
            MapViewModel mapViewModel,
            MenuFlyoutViewModel menuFlyoutViewModel)
        {
            MapViewModel = mapViewModel;
            ActivateItem(MapViewModel);
            CalculatorViewModel = calcViewModel;
            ActivateItem(CalculatorViewModel);
            MenuFlyoutViewModel = menuFlyoutViewModel;
            ActivateItem(MenuFlyoutViewModel);
        }

        public void OpenMenuFlyout()
        {
            var flyout = ((MetroWindow) Application.Current.MainWindow).Flyouts.Items[0];
            ((Flyout) flyout).IsOpen = !((Flyout) flyout).IsOpen;
        }
    }
}