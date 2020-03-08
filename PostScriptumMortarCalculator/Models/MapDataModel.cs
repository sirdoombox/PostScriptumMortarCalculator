using PropertyChanged;

namespace PostScriptumMortarCalculator.Models
{
    [DoNotNotify]
    public class MapDataModel
    {
        public string MapPath { get; }
        
        public string DisplayName { get; }
        
        public double Bounds { get; }

        public MapDataModel(string path, string name, double bounds)
        {
            MapPath = path;
            DisplayName = name;
            Bounds = bounds;
        }
    }
}