using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VectorLib
{
    public class Vector
    {
        public string ConnectionString { get; private set; }
        private QueueClient QClient;

        private Queue<BrokeredMessage> Messages;

        private object LockObj = new Object();

        public string ConfigProfile { get; set; }

        private const string QName = "vectormain";

        public Vector(string configProfile)
        {
            Messages = new Queue<BrokeredMessage>();
            ConfigProfile = configProfile;
            ConnectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            QClient = QueueClient.CreateFromConnectionString(ConnectionString, QName, ReceiveMode.ReceiveAndDelete);

            var timer = new Timer();
            timer.Interval = 100;
            timer.AutoReset = true;
            timer.Elapsed += SendToBus;
            timer.Enabled = true;
        }

        public void WriteMessage(string message)
        {
            BrokeredMessage msg = new BrokeredMessage();
            msg.Properties["message-content"] = message;
            msg.Properties["profile"] = ConfigProfile;
            msg.Properties["timestamp"] = DateTime.UtcNow;

            lock (LockObj)
            {
                Messages.Enqueue(msg);
            }
        }

        private void SendToBus(object source, ElapsedEventArgs e)
        {
            lock (LockObj)
            {
                if (Messages.Any())
                {
                    QClient.SendBatch(Messages);
                    Messages = new Queue<BrokeredMessage>();
                }
            }
        }
    }
}
