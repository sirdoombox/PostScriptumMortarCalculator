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
        
        private readonly string m_appDataPath;
        private readonly string m_configDir;
        private readonly string m_configFilePath;
        private UserConfigModel m_activeConfig;
        
        public ConfigService()
        {
            m_appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            m_configDir = Path.Combine(m_appDataPath, c_DATA_DIR);
            m_configFilePath = Path.Combine(m_configDir, c_CONFIG_FILENAME);
        }

        public UserConfigModel LoadOrDefault()
        {
            if (!Directory.Exists(m_configDir) || !File.Exists(m_configFilePath))
                m_activeConfig = UserConfigModel.Default;
            else
                m_activeConfig = JsonConvert.DeserializeObject<UserConfigModel>(File.ReadAllText(m_configFilePath));
            m_activeConfig.PropertyChanged += (_,__) => SerialiseUserConfig();
            return m_activeConfig;
        }

        public void SerialiseUserConfig()
        {
            var cfgJson = JsonConvert.SerializeObject(m_activeConfig);
            if (!Directory.Exists(m_configDir)) Directory.CreateDirectory(m_configDir);
            File.WriteAllText(m_configFilePath, cfgJson);
        }
    }
}