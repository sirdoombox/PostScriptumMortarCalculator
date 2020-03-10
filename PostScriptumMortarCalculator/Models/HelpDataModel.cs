using System.Collections.Generic;
using PropertyChanged;

namespace PostScriptumMortarCalculator.Models
{
    [DoNotNotify]
    public class HelpDataModel
    {
        public IReadOnlyList<Hotkey> Hotkeys { get; } 
        
        public IReadOnlyList<string> Hints { get; }

        public HelpDataModel(List<Hotkey> hotkeys, List<string> hints)
        {
            Hotkeys = hotkeys;
            Hints = hints;
        }
        
        [DoNotNotify]
        public class Hotkey
        {
            public string Action { get; }
            public string Key { get; }

            public Hotkey(string action, string key)
            {
                Action = action;
                Key = key;
            }
        }
    }
}