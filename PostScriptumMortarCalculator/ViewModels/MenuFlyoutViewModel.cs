using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class MenuFlyoutViewModel : Conductor<Screen>.Collection.OneActive
    {
        public MenuFlyoutViewModel(UserConfigViewModel configViewModel,
            HelpViewModel helpViewModel,
            CreditsViewModel creditsViewModel)
        {
            Items.Add(configViewModel);
            Items.Add(helpViewModel);
            Items.Add(creditsViewModel);
            ActiveItem = configViewModel;
        }
    }
}