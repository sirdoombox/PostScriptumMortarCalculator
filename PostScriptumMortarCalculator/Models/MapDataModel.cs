using PropertyChanged;

namespace PostScriptumMortarCalculator.Models
{
    [DoNotNotify]
    public class MapDataModel
    {
        public string MapImagePath { get; }
        
        public string Name { get; }
        
        public double Bounds { get; }

        public MapDataModel(string path, string name, double bounds)
        {
            MapImagePath = path;
            Name = name;
            Bounds = bounds;
        }
    }
}