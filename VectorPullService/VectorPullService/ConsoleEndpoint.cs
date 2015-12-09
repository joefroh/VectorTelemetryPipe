using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace VectorPullService
{
   public class ConsoleEndpoint : IEndpoint
    {
        public string Name
        {
            get
            {
                return "Console";
            }
        }

        public void Connect()
        {
            
        }

        public void Disconnect()
        {
            
        }

        public void Write(BrokeredMessage message)
        {
            Console.WriteLine(message.Properties["message-content"]);
        }
    }
}
