using System;
using System.ComponentModel;
using Newtonsoft.Json;
using PropertyChanged;
using Stylet;

namespace PostScriptumMortarCalculator.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserConfigModel : PropertyChangedBase
    {
        [JsonIgnore]
        public BindableCollection<string> Themes { get; set; }
            = new BindableCollection<string>();

        [JsonIgnore]
        public BindableCollection<string> Accents { get; set; }
            = new BindableCollection<string>();
        
        [JsonProperty] public string Theme { get; set; }
        [JsonProperty] public string Accent { get; set; }

        [DoNotNotify]
        public static UserConfigModel Default
        {
            get => new UserConfigModel
            {
                Theme = "Dark",
                Accent = "Lime"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnThemeChanged() => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Theme)));

        public void OnAccentChanged() => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Accent)));
    }
}