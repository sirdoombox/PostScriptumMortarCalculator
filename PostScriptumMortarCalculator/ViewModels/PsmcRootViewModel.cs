using System.Linq;
using System.Windows;
using MahApps.Metro;
using MahApps.Metro.Controls;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using PostScriptumMortarCalculator.ViewModels.Base;

namespace PostScriptumMortarCalculator.ViewModels
{
    public sealed class PsmcRootViewModel : ConductorViewModelBase<PsmcRootModel, CalculatorViewModel>
    {
        private readonly UserConfigModel m_cfgModel;

        public UserConfigViewModel ConfigViewModel { get; set; }

        public PsmcRootViewModel(CalculatorViewModel calcViewModel,
            UserConfigModel cfg,
            UserConfigViewModel cfgViewModel) : base(calcViewModel)
        {
            ConfigViewModel = cfgViewModel;
            m_cfgModel = cfg;
            cfg.PropertyChanged += (_, __) => OnUserConfigChanged();
            m_cfgModel.Accents.AddRange(ThemeManager.ColorSchemes.Select(x => x.Name));
            m_cfgModel.Themes.AddRange(ThemeManager.Themes.GroupBy(x => x.BaseColorScheme).Select(x => x.First())
                .Select(x => x.BaseColorScheme));

            ThemeManager.ChangeThemeBaseColor(Application.Current, m_cfgModel.Theme);
            ThemeManager.ChangeThemeColorScheme(Application.Current, m_cfgModel.Accent);
        }

        private void OnUserConfigChanged()
        {
            ThemeManager.ChangeThemeBaseColor(Application.Current, m_cfgModel.Theme);
            ThemeManager.ChangeThemeColorScheme(Application.Current, m_cfgModel.Accent);
        }

        public void OpenSettingsFlyout()
        {
            var flyout = ((MetroWindow) Application.Current.MainWindow).Flyouts.Items[0];
            ((Flyout) flyout).IsOpen = !((Flyout) flyout).IsOpen;
        }
    }
}