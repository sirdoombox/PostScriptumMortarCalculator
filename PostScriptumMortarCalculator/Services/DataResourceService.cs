using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using Newtonsoft.Json;
using PostScriptumMortarCalculator.Data;

namespace PostScriptumMortarCalculator.Services
{
    public class DataResourceService
    {
        private const string c_RESOURCE_PATH = "/PostScriptumMortarCalculator;component/Assets";
        private const string c_RESOURCE_MANAGER = "PostScriptumMortarCalculator.g";

        public IEnumerable<MapData> GetAvailableMapData()
        {
            var resourceManager = new ResourceManager(c_RESOURCE_MANAGER, Assembly.GetExecutingAssembly());
            var resources = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (var res in from object resource in resources select (DictionaryEntry) resource)
            {
                if (!((string)res.Key).EndsWith(".json")) continue;
                using var streamReader = new StreamReader((UnmanagedMemoryStream) res.Value ?? throw new Exception());
                var jsonString = streamReader.ReadToEnd();
                var maps = JsonConvert.DeserializeObject<List<MapData>>(jsonString);
                maps.ForEach(x => x.MapPath = c_RESOURCE_PATH + x.MapPath);
                return maps;
            }
            return null;
        }
    }
}