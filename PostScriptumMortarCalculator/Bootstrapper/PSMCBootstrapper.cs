using System.Collections.Generic;
using System.Linq;
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
            var configService = new ConfigService();
            builder.Bind<UserConfigModel>().ToInstance(configService.LoadOrDefault());
            builder.Bind<ConfigService>().ToInstance(configService);
            var dataResourceService = new DataResourceService();
            builder.Bind<IReadOnlyList<MapDataModel>>()
                .ToFactory(_ => dataResourceService.GetMapData())
                .InSingletonScope();
            builder.Bind<IReadOnlyList<MortarDataModel>>()
                .ToFactory(_ => dataResourceService.GetMortarData())
                .InSingletonScope();
            builder.Bind<HelpDataModel>()
                .ToFactory(_ => dataResourceService.GetHelpData())
                .InSingletonScope();
            builder.Bind<CreditsDataModel>()
                .ToFactory(_ => dataResourceService.GetCreditsData())
                .InSingletonScope();
            builder.Bind<MortarDataModel>()
                .ToFactory(container => container.Get<IReadOnlyList<MortarDataModel>>().First());
        }
    }
}