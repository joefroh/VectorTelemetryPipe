using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorLib
{
    public class Vector
    {
        public string ConnectionString { get; private set; }
        private QueueClient QClient;

        private const string QName = "vectormain";

        public Vector()
        {
            ConnectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            QClient = QueueClient.CreateFromConnectionString(ConnectionString, QName, ReceiveMode.ReceiveAndDelete);
        }

        public void WriteMessage(string message)
        {
            BrokeredMessage msg = new BrokeredMessage();
            msg.Properties["message-content"] = message;

            QClient.Send(msg);
        }
    }
}
