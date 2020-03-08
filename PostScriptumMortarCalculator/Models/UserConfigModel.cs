using Newtonsoft.Json;

namespace PostScriptumMortarCalculator.Models
{
    public class UserConfigModel
    {
        [JsonProperty] public string Theme { get; set; }
        [JsonProperty] public string Accent { get; set; }

        public static UserConfigModel Default => new UserConfigModel
        {
            Theme = "Dark",
            Accent = "Lime"
        };
    }
}