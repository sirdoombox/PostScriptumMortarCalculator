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
            builder.Bind<ConfigService>()
                .ToFactory(_ => new ConfigService().LoadOrDefault())
                .InSingletonScope();
            
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
                .ToFactory(container =>
                {
                    var config = container.Get<ConfigService>().ActiveConfig;
                    var lastMortarSet = !string.IsNullOrWhiteSpace(config.LastMortarName);
                    var mortars = container.Get<IReadOnlyList<MortarDataModel>>();
                    return lastMortarSet ? mortars.First(x => x.Name == config.LastMortarName) : mortars.First();
                });
        }
    }
}