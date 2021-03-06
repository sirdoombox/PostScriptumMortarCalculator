using System.Windows.Media;
using Newtonsoft.Json;

namespace PostScriptumMortarCalculator.Models
{
    public class UserConfigModel
    {
        [JsonProperty] public string Theme { get; set; } = "Dark";
        [JsonProperty] public string Accent { get; set; } = "Lime";
        [JsonProperty] public double IndicatorOpacity { get; set; } = 0.25d;
        [JsonProperty] public double GridOpacity { get; set; } = 0.25d;
        [JsonProperty] public string LastMapName { get; set; }
        [JsonProperty] public string LastMortarName { get; set; }
        [JsonProperty] public Color MortarIndicatorColour { get; set; } = Colors.Blue;
        [JsonProperty] public Color TargetIndicatorColour { get; set; } = Colors.Red;

    }
}