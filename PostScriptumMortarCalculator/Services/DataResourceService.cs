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
        private const string c_MAP_DATA_FILENAME = "mapdata.json";
        private const string c_MORTAR_DATA_FILENAME = "mortardata.json";
        private const string c_HELP_DATA_FILENAME = "helpdata.json";
        private const string c_CREDITS_DATA_FILENAME = "creditsdata.json";

        private readonly ResourceSet m_resources;

        public DataResourceService()
        {
            m_resources = new ResourceManager(c_RESOURCE_MANAGER, Assembly.GetExecutingAssembly())
                .GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        }

        public List<MapDataModel> GetMapData() =>
            GetDataFromFileName<List<MapDataModel>>(c_MAP_DATA_FILENAME);

        public List<MortarDataModel> GetMortarData() =>
            GetDataFromFileName<List<MortarDataModel>>(c_MORTAR_DATA_FILENAME);

        public CreditsDataModel GetCreditsData() => 
            GetDataFromFileName<CreditsDataModel>(c_CREDITS_DATA_FILENAME);

        public HelpDataModel GetHelpData() =>
            GetDataFromFileName<HelpDataModel>(c_HELP_DATA_FILENAME);
        
        // TODO: Clean this up to something less awful.
        private T GetDataFromFileName<T>(string fileName)
        {
            foreach (var res in from object resource in m_resources select (DictionaryEntry) resource)
            {
                if (!((string)res.Key).EndsWith(fileName)) continue;
                using var streamReader = new StreamReader((UnmanagedMemoryStream) res.Value ?? throw new Exception());
                var jsonString = streamReader.ReadToEnd();
                var obj = JsonConvert.DeserializeObject<T>(jsonString);
                return obj;
            }
            return default;
        }
    }
}