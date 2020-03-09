using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class HelpViewModel : Screen
    {
        public BindableCollection<HotkeyContainer> Hotkeys { get; set; } = 
            new BindableCollection<HotkeyContainer>();
        
        public BindableCollection<string> Hints { get; set; } =
            new BindableCollection<string>();
        
        public HelpViewModel()
        {
            DisplayName = "Help";
            Hotkeys.AddRange(new[]
            {
                new HotkeyContainer("Place Mortar", "Right Click"),
                new HotkeyContainer("Place Target", "Left Click"),
                new HotkeyContainer("Pan Map", "Middle Mouse Hold"),
                new HotkeyContainer("Zoom Map", "Mouse Scroll"),
                new HotkeyContainer("Reset Map", "Keyboard 'R'"), 
            });
            Hints.AddRange(new[]
            {
                "Remember to update your mortar position when you move.",
                "Zoom in fully to place your mortar position as accurately as possible.",
                "The red area at your target location is the innate spread of the mortar.",
                "This tool is very accurate but not perfect.",
                "No terrain height data is incorporated in the calculation because that data is not available.",
                "Innate spread and distance beyond the maximum range on the in-game tables is a (quite accurate) approximation."
            });
        }

        public class HotkeyContainer
        {
            public string Action { get; }
            public string Key { get; }

            public HotkeyContainer(string action, string key)
            {
                Action = action;
                Key = key;
            }
        }
    }
}