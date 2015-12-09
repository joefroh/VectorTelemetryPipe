using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPullService
{
    class EndPointManager
    {
        public static EndPointManager Instance = new EndPointManager();
        private Dictionary<string, IEndpoint> endpoints;
        private ConfigurationProfileManager ConfigManager;

        private EndPointManager()
        {
            endpoints = new Dictionary<string, IEndpoint>();
            ConfigManager = new ConfigurationProfileManager("Vector.vcf");

            ReflectionLoadEndpoints();
        }

        public void WriteMessageToEndpoints(BrokeredMessage message)
        {
            if (!message.Properties.ContainsKey("profile"))
            {
                return; //if you are losing messages, its probably here.
            }
            var messageProfile = FetchProfile((string)message.Properties["profile"]);
            foreach (var endpoint in messageProfile.EndpointNames)
            {
                if (endpoints.ContainsKey(endpoint))
                {
                    endpoints[endpoint].Write(message);
                }
                
            }
        }

        private ConfigurationProfile FetchProfile(string profile)
        {
            return ConfigManager.GetConfigurationProfile(profile);
        }

        private void ReflectionLoadEndpoints()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var types = assembly.GetExportedTypes();

            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(typeof(IEndpoint)))
                {
                    var endpoint = (IEndpoint)Activator.CreateInstance(type);
                    endpoint.Connect();
                    endpoints.Add(endpoint.Name, endpoint);
                }
            }

            var msg = new BrokeredMessage();
            msg.Properties["message-content"] = String.Format("{0} Endpoints Loaded", endpoints.Count);
            msg.Properties["timestamp"] = DateTime.UtcNow;
            
            endpoints["VectorDebugLog"].Write(msg);
        }
    }
}
