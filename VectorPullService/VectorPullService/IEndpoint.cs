using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPullService
{
    interface IEndpoint
    {
        void Write(BrokeredMessage message);

        void Connect();

        void Disconnect();

        string Name { get; }
    }
}
