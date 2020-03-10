using System.Collections.Generic;
using PropertyChanged;

namespace PostScriptumMortarCalculator.Models
{
    [DoNotNotify]
    public class CreditsDataModel
    {
        public IReadOnlyList<Contribution> Contributors { get; }
        
        public IReadOnlyList<Contribution> ExternalTools { get; }

        public CreditsDataModel(List<Contribution> contributors, List<Contribution> externalTools)
        {
            Contributors = contributors;
            ExternalTools = externalTools;
        }
        
        [DoNotNotify]
        public class Contribution
        {
            public string Name { get; }
            public string ContributionType { get; }
            public string ContributionDescription { get; }
            public string URL { get; }

            public Contribution(string name, string contributionType, string contributionDescription, string url)
            {
                Name = name;
                ContributionType = contributionType;
                ContributionDescription = contributionDescription;
                URL = url;
            }
        }
    }
}