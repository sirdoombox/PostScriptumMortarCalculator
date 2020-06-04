using System.Collections.Generic;
using System.Linq;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using PostScriptumMortarCalculator.ViewModels;
using Stylet;
using StyletIoC;
#if (!DEBUG)
using LogLevel = NLog.LogLevel;
using NLog.Config;
using NLog.Targets;
#endif

namespace PostScriptumMortarCalculator.Bootstrapper
{
    public class PsmcBootstrapper : Bootstrapper<PsmcRootViewModel>
    {
        #if (!DEBUG)
        // Release logging.
        private const string c_LOG_DUMP_FILE_NAME = "ErrorLog.log";
        private ErrorLogger m_logger;
        protected override void OnStart()
        {
            var config = new LoggingConfiguration();
            var logFile = new FileTarget("logfile")
            {
                CreateDirs = true, 
                FileName = c_LOG_DUMP_FILE_NAME, 
                DeleteOldFileOnStartup = true, 
                Layout = "${longdate} ${message} ${exception:format=tostring}"
            };
            config.AddRule(LogLevel.Trace, LogLevel.Trace, logFile);
            NLog.LogManager.Configuration = config;
            m_logger = new ErrorLogger();
        }
        #endif

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