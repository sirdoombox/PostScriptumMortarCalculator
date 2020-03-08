using System.Collections.Generic;
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
            var config = configService.LoadOrDefault();
            builder.Bind<UserConfigModel>().ToInstance(config);
            builder.Bind<ConfigService>().ToInstance(configService);
            var dataResourceService = new DataResourceService();
            builder.Bind<IReadOnlyList<MapDataModel>>()
                .ToFactory(container => dataResourceService.GetMapData())
                .InSingletonScope();
            builder.Bind<IReadOnlyList<MortarDataModel>>()
                .ToFactory(container => dataResourceService.GetMortarData())
                .InSingletonScope();
        }
    }
}