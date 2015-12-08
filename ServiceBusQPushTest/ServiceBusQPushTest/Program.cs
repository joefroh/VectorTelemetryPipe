using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusQPushTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            QueueClient Client = QueueClient.CreateFromConnectionString(connectionString, "testvector");

            Client.Send(new BrokeredMessage() { });
        }
    }
}
