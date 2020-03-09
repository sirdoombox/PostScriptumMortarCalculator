using System;
using System.Windows.Input;
using PostScriptumMortarCalculator.Models;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class CreditsViewModel : Screen
    {
        public BindableCollection<ContributorContainer> PersonalContributors { get; set; } 
            = new BindableCollection<ContributorContainer>();
        
        public BindableCollection<ContributorContainer> LibraryContributors { get; set; } 
            = new BindableCollection<ContributorContainer>();
        
        public ICommand OpenUrlCommand { get; set; }
        
        public CreditsViewModel()
        {
            OpenUrlCommand = new SimpleOpenUrlCommand();
            DisplayName = "Credits";
            PersonalContributors.AddRange(new ContributorContainer[]
            {
                new ContributorContainer("Testing", "Solmyr", "https://steamcommunity.com/id/solmyr101/"),
                new ContributorContainer("Testing", "Mr.Skeltal", "https://steamcommunity.com/id/Starman-NL/"), 
            });
            LibraryContributors.AddRange(new ContributorContainer[]
            {
                new ContributorContainer("Map Image Extraction", "UEViewer", "https://www.gildor.org/en/projects/umodel"),
                new ContributorContainer("UI & Controls", "MahApps.Metro", "https://mahapps.com/"),
                new ContributorContainer("MVVM & IoC Provider", "Stylet", "https://github.com/canton7/Stylet"), 
                new ContributorContainer("Assembly Weaver", "Fody [Costura/PropertyChanged]", "https://github.com/Fody/Fody"), 
            });
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
        
        public class ContributorContainer
        {
            public string ContributionType { get; }
        
            public string Name { get; }
            public string UrlString { get; }

            public ContributorContainer(string contributionType, string name, string urlString)
            {
                ContributionType = contributionType;
                Name = name;
                UrlString = urlString;
            }
        }
    }
}