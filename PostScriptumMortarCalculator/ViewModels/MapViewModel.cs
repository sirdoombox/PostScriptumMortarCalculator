using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using PostScriptumMortarCalculator.Controls;
using PostScriptumMortarCalculator.Models;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class MapViewModel : ViewModelBase<MapModel>
    {
        private readonly List<string> maps = new List<string>();
        
        public MapViewModel()
        {
            var resourceManager =
                new ResourceManager("PostScriptumMortarCalculator.g", Assembly.GetExecutingAssembly());
            var resources = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (var resLoc in from object resource in resources select (string) ((DictionaryEntry) resource).Key)
            {
                if (!resLoc.EndsWith(".jpg")) continue;
                maps.Add($"/PostScriptumMortarCalculator;component/{resLoc}");
            }
            Model.MapFile = maps[new Random().Next(0, maps.Count)];
        }
        
        public void HandleMapSelectionEvent(MapSelectionEventArgs args)
        {
            Model.EventTest = args.Type.ToString();
        }
    }
}