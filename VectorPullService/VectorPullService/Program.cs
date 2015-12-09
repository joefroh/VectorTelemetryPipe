using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace VectorPullService
{
    class Program
    {
        static EndPointManager man = new EndPointManager();

        static void Main(string[] args)
        {
            var time = new System.Timers.Timer();
            time.AutoReset = true;
            time.Interval = 100;
            time.Elapsed += ReadFromBus;
            time.Enabled = true;

            Console.WriteLine("====Listening for messages. Press ENTER to quit.====");
            Console.ReadLine();
        }

        private static void ReadFromBus(object source, ElapsedEventArgs e)
        {
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            QueueClient Client = QueueClient.CreateFromConnectionString(connectionString, "vectormain", ReceiveMode.ReceiveAndDelete);

            var messages = Client.ReceiveBatch(100000000);

            foreach (var message in messages)
            {
                man.WriteMessageToEndpoints(message);
            }
        }
    }
}
