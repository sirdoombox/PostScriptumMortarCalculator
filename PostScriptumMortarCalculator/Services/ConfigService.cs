using System;
using System.IO;
using Newtonsoft.Json;
using PostScriptumMortarCalculator.Models;

namespace PostScriptumMortarCalculator.Services
{
    public class ConfigService
    {
        private const string c_CONFIG_FILENAME = "UserConfig.cfg";
        private const string c_DATA_DIR = "PostScriptumMortarCalculator";

        private readonly string m_configDir;
        private readonly string m_configFilePath;

        public UserConfigModel ActiveConfig { get; private set; }

        public ConfigService()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            m_configDir = Path.Combine(appDataPath, c_DATA_DIR);
            m_configFilePath = Path.Combine(m_configDir, c_CONFIG_FILENAME);
        }

        public ConfigService LoadOrDefault()
        {
            if (!Directory.Exists(m_configDir) || !File.Exists(m_configFilePath))
                ActiveConfig = UserConfigModel.Default;
            else
                ActiveConfig = JsonConvert.DeserializeObject<UserConfigModel>(File.ReadAllText(m_configFilePath));
            return this;
        }

        public void SerialiseUserConfig()
        {
            var cfgJson = JsonConvert.SerializeObject(ActiveConfig);
            if (!Directory.Exists(m_configDir)) Directory.CreateDirectory(m_configDir);
            File.WriteAllText(m_configFilePath, cfgJson);
        }
    }
}