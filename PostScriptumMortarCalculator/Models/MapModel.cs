using System;
using PostScriptumMortarCalculator.Controls;
using Stylet;

namespace PostScriptumMortarCalculator.Models
{
    public class MapModel : PropertyChangedBase
    {
        public string MapFile { get; set; }

        public string EventTest { get; set; }
    }
}