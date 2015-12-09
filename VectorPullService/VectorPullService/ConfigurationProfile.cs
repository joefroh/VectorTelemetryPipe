using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPullService
{
    class ConfigurationProfile
    {
        public string Name { get; set; }

        public List<string> EndpointNames;

        public ConfigurationProfile()
        {
            EndpointNames = new List<string>();
        }
    }
}
