using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPullService
{
    class ConfigurationProfileManager
    {
        Dictionary<string, ConfigurationProfile> Profiles;

        public ConfigurationProfileManager(string path)
        {
            FileInfo info = new FileInfo(path);
            if (!info.Exists)
            {
                throw new ArgumentException(String.Format("Provided config file {0} does not exist."), info.FullName);
            }

            Profiles = new Dictionary<string, ConfigurationProfile>();
            LoadConfigs(path);
        }

        public ConfigurationProfile GetConfigurationProfile(string name)
        {
            if (Profiles.ContainsKey(name))
            {
                return Profiles[name];
            }
            else return null;
        }

        private void LoadConfigs(string path)
        {
            StreamReader reader = new StreamReader(path);
            var currentConfig = new ConfigurationProfile();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line[0] == ':')
                {
                    currentConfig = new ConfigurationProfile();
                    currentConfig.Name = line.Substring(1).Trim();
                    Profiles.Add(currentConfig.Name, currentConfig);
                }
                else
                {
                    currentConfig.EndpointNames.Add(line.Trim());
                }
            }
        }
    }
}
