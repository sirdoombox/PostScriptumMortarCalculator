using PostScriptumMortarCalculator.Models;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class HelpViewModel : Screen
    {
        public BindableCollection<HelpDataModel.Hotkey> Hotkeys { get; set; } = 
            new BindableCollection<HelpDataModel.Hotkey>();
        
        public BindableCollection<string> Hints { get; set; } =
            new BindableCollection<string>();
        
        public HelpViewModel(HelpDataModel helpDataModel)
        {
            DisplayName = "Help";
            Hotkeys.AddRange(helpDataModel.Hotkeys);
            Hints.AddRange(helpDataModel.Hints);
        }
    }
}