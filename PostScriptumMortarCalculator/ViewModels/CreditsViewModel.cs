using System;
using System.Windows.Input;
using PostScriptumMortarCalculator.Models;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class CreditsViewModel : Screen
    {
        public BindableCollection<CreditsDataModel.Contribution> Contributors { get; set; } 
            = new BindableCollection<CreditsDataModel.Contribution>();
        
        public BindableCollection<CreditsDataModel.Contribution> ExternalTools { get; set; } 
            = new BindableCollection<CreditsDataModel.Contribution>();
        
        public ICommand OpenUrlCommand { get; set; }
        
        public CreditsViewModel(CreditsDataModel creditsDataModel)
        {
            OpenUrlCommand = new SimpleOpenUrlCommand();
            DisplayName = "Credits";
            Contributors.AddRange(creditsDataModel.Contributors);
            ExternalTools.AddRange(creditsDataModel.ExternalTools);
        }

        public class SimpleOpenUrlCommand : ICommand
        {
            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter)
            {
                if (parameter is string url)
                {
                    System.Diagnostics.Process.Start(url);
                }
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}