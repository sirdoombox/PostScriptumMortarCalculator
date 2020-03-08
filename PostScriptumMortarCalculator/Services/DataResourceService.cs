using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using Newtonsoft.Json;
using PostScriptumMortarCalculator.Models;

namespace PostScriptumMortarCalculator.Services
{
    public class DataResourceService
    {
        private const string c_RESOURCE_MANAGER = "PostScriptumMortarCalculator.g";

        public List<MapDataModel> GetMapData()
        {
            var resourceManager = new ResourceManager(c_RESOURCE_MANAGER, Assembly.GetExecutingAssembly());
            var resources = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (var res in from object resource in resources select (DictionaryEntry) resource)
            {
                if (!((string)res.Key).EndsWith("mapdata.json")) continue;
                using var streamReader = new StreamReader((UnmanagedMemoryStream) res.Value ?? throw new Exception());
                var jsonString = streamReader.ReadToEnd();
                var maps = JsonConvert.DeserializeObject<List<MapDataModel>>(jsonString);
                return maps;
            }
            return null;
        }
        
        public List<MortarDataModel> GetMortarData()
        {
            var resourceManager = new ResourceManager(c_RESOURCE_MANAGER, Assembly.GetExecutingAssembly());
            var resources = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (var res in from object resource in resources select (DictionaryEntry) resource)
            {
                if (!((string)res.Key).EndsWith("mortardata.json")) continue;
                using var streamReader = new StreamReader((UnmanagedMemoryStream) res.Value ?? throw new Exception());
                var jsonString = streamReader.ReadToEnd();
                var obj = JsonConvert.DeserializeObject<List<MortarDataModel>>(jsonString);
                return obj;
            }
            return null;
        }
    }
}