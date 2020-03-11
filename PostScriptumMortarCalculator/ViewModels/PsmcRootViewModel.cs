using System.Windows;
using MahApps.Metro.Controls;
using PostScriptumMortarCalculator.Services;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public sealed class PsmcRootViewModel : Conductor<IScreen>
    {
        public MenuFlyoutViewModel MenuFlyoutViewModel { get; set; }
        public MapViewModel MapViewModel { get; set; }
        public CalculatorViewModel CalculatorViewModel { get; set; }
        
        public bool IsUpdateAvailable { get; private set; }

        public string UpdateTooltip { get; private set; }

        private readonly UpdateService m_updateService;
        
        private string m_updatePath;

        public PsmcRootViewModel(CalculatorViewModel calcViewModel,
            UpdateService updateService,
            MapViewModel mapViewModel,
            MenuFlyoutViewModel menuFlyoutViewModel)
        {
            MapViewModel = mapViewModel;
            ActivateItem(MapViewModel);
            CalculatorViewModel = calcViewModel;
            m_updateService = updateService;
            ActivateItem(CalculatorViewModel);
            MenuFlyoutViewModel = menuFlyoutViewModel;
            ActivateItem(MenuFlyoutViewModel);
        }

        protected override async void OnViewLoaded()
        {
            var result = await m_updateService.CheckForUpdate();
            IsUpdateAvailable = result.IsUpdateAvailable;
            m_updatePath = result.ReleasePath;
            UpdateTooltip = $"Current: {result.CurrentVersion} | New: {result.NewVersion}";
        }

        public void UpdateAvailableClicked()
        {
            System.Diagnostics.Process.Start(m_updatePath);
        }

        public void OpenMenuFlyout()
        {
            var flyout = ((MetroWindow) Application.Current.MainWindow).Flyouts.Items[0];
            ((Flyout) flyout).IsOpen = !((Flyout) flyout).IsOpen;
        }
    }
}