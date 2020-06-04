using System;
using System.Reflection;
using System.Threading.Tasks;
using NLog;
using System.Windows;

namespace PostScriptumMortarCalculator.Bootstrapper
{
    public class ErrorLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public ErrorLogger()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            Application.Current.DispatcherUnhandledException += (s, e) =>
            {
                LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };
        }
        
        private void LogUnhandledException(Exception exception, string source)
        {
            var message = $"Unhandled exception ({source})";
            try
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName();
                message = $"Unhandled exception in {assemblyName.Name} v{assemblyName.Version}";
            }
            catch (Exception ex)
            {
                Logger.Trace(ex, "Exception in LogUnhandledException");
                LogManager.Shutdown();
                Application.Current.Shutdown();
            }
            finally
            {
                Logger.Trace(exception, message);
                LogManager.Shutdown();
                Application.Current.Shutdown();
            }
        }
    }
}