using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VectorPullService
{
    class Program
    {
        static EndPointManager man = new EndPointManager();
        static void Main(string[] args)
        {
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            QueueClient Client = QueueClient.CreateFromConnectionString(connectionString, "vectormain", ReceiveMode.ReceiveAndDelete);
           
            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = true;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            Client.OnMessage(MessageHandler, options);

            Console.WriteLine("====Listening for messages. Press ENTER to quit.====");
            Console.ReadLine();
        }

        private static void MessageHandler(BrokeredMessage msg)
        {
            man.WriteMessageToEndpoints(msg);
        }
    }
}
