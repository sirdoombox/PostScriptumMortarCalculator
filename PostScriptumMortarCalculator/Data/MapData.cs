using Newtonsoft.Json;
using Stylet;

namespace PostScriptumMortarCalculator.Data
{
    public class MapData : PropertyChangedBase
    {
        [JsonProperty("path")]
        public string MapPath { get; set; }
        
        [JsonProperty("name")]
        public string DisplayName { get; set; }
        
        [JsonProperty("bounds")]
        public double Bounds { get; set; }
    }
}