using Newtonsoft.Json;

namespace PostScriptumMortarCalculator.Models
{
    public class UserConfigModel
    {
        [JsonProperty] public string Theme { get; set; }
        [JsonProperty] public string Accent { get; set; }
        
        [JsonProperty] public double Opacity { get; set; }
        
        [JsonProperty] public string LastMapName { get; set; }
        
        [JsonProperty] public string LastMortarName { get; set; }

        public static UserConfigModel Default => new UserConfigModel
        {
            Theme = "Dark",
            Accent = "Lime",
            Opacity = 0.25d,
        };
    }
}