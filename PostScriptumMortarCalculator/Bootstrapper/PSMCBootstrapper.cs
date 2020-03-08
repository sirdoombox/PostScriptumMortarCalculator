using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using PostScriptumMortarCalculator.ViewModels;
using Stylet;
using StyletIoC;

namespace PostScriptumMortarCalculator.Bootstrapper
{
    public class PsmcBootstrapper : Bootstrapper<PsmcRootViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.Bind<UserConfigModel>()
                .ToFactory(container => new ConfigService().LoadOrDefault())
                .InSingletonScope();
        }
    }
}