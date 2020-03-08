using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MahApps.Metro;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class UserConfigViewModel : Screen
    {
        public BindableCollection<string> Themes { get; set; }
            = new BindableCollection<string>();
        
        public BindableCollection<string> Accents { get; set; }
            = new BindableCollection<string>();
        
        public string SelectedTheme { get; set; }
        public string SelectedAccent { get; set; }

        private readonly ConfigService m_configService;
        private readonly UserConfigModel m_configModel;
        
        public UserConfigViewModel(ConfigService configService)
        {
            m_configService = configService;
            m_configModel = m_configService.ActiveConfig;
            Accents.AddRange(ThemeManager.ColorSchemes.Select(x => x.Name));
            Themes.AddRange(ThemeManager.Themes.GroupBy(x => x.BaseColorScheme).Select(x => x.First())
                .Select(x => x.BaseColorScheme));
            SelectedTheme = m_configModel.Theme;
            SelectedAccent = m_configModel.Accent;
            ThemeManager.ChangeThemeBaseColor(Application.Current, SelectedTheme);
            ThemeManager.ChangeThemeColorScheme(Application.Current, SelectedAccent);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            if (m_configService is null) return;
            m_configModel.Accent = SelectedAccent;
            m_configModel.Theme = SelectedTheme;
            m_configService.SerialiseUserConfig();
            ThemeManager.ChangeThemeBaseColor(Application.Current, SelectedTheme);
            ThemeManager.ChangeThemeColorScheme(Application.Current, SelectedAccent);
        }
    }
}