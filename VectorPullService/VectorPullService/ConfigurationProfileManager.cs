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
        FileSystemWatcher Watcher;
        string Path;

        public ConfigurationProfileManager(string path)
        {
            FileInfo info = new FileInfo(path);
            if (!info.Exists)
            {
                EndPointManager.Instance.WriteMessageToDebugLog(String.Format("Provided config file {0} does not exist.", info.FullName));
                throw new ArgumentException(String.Format("Provided config file {0} does not exist."), info.FullName);
            }

            LoadConfigs(path);
            Path = path;

            Watcher = new FileSystemWatcher(info.DirectoryName);
            Watcher.Filter = info.Name;
            Watcher.Changed += ConfigFileChanged;
            Watcher.EnableRaisingEvents = true;
        }

        private void ConfigFileChanged(object sender, FileSystemEventArgs e)
        {
            LoadConfigs(Path);
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
            EndPointManager.Instance.WriteMessageToDebugLog("Loading Configurations");
            Profiles = new Dictionary<string, ConfigurationProfile>();
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
