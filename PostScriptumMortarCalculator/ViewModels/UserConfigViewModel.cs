using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.ViewModels.Base;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class UserConfigViewModel : ViewModelBase<UserConfigModel>
    {
        public UserConfigViewModel(UserConfigModel cfg)
        {
            Model = cfg;
        }
    }
}