using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusQPullTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            QueueClient Client = QueueClient.CreateFromConnectionString(connectionString, "testvector", ReceiveMode.ReceiveAndDelete);
            var ns = NamespaceManager.CreateFromConnectionString(connectionString);

            var qLength = ns.GetQueue("testvector").MessageCount; //requires manager privilage... uch


            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = true;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            if (qLength > 0)
            {
                var message = Client.Receive();
                //if receivemode above is peek, must call message.complete to remove from queue
            }

        }
    }
}
