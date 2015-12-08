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
        private Dictionary<Type, IEndpoint> endpoints;

        public EndPointManager()
        {
            endpoints = new Dictionary<Type, IEndpoint>();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var types = assembly.GetExportedTypes();

            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(typeof(IEndpoint)))
                {
                    var endpoint = (IEndpoint)Activator.CreateInstance(type);
                    endpoint.Connect();
                    endpoints.Add(type, endpoint);
                }
            }
        }

        public void WriteMessageToEndpoints(BrokeredMessage message)
        {
            foreach (var endpoint in endpoints)
            {
                endpoint.Value.Write(message);
            }
        }
    }
}
