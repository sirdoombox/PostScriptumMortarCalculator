namespace PostScriptumMortarCalculator.Models
{
    public readonly struct MapCoordinate
    {
        public string GridLetter { get; }
        public int GridNumber { get; }
        public int KeypadMajor { get; }
        public int KeypadMinor { get; }
        
        public MapCoordinate(string gridLetter, int gridNumber, int keypadMajor, int keypadMinor)
        {
            GridLetter = gridLetter;
            GridNumber = gridNumber;
            KeypadMajor = keypadMajor;
            KeypadMinor = keypadMinor;
        }

        public override string ToString()
        {
            return $"{GridLetter}{GridNumber}-{KeypadMajor}-{KeypadMinor}";
        }
    }
}