using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using VectorPullService;

namespace ServiceHost
{
    public partial class Service1 : ServiceBase
    {
        QueueClient Client;
        public Service1()
        {
            this.CanStop = true;
            this.CanPauseAndContinue = false;
            InitializeComponent();
            //this.RequestAdditionalTime(60000);
        }

        protected override void OnStart(string[] args)
        {
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            Client = QueueClient.CreateFromConnectionString(connectionString, "vectormain", ReceiveMode.ReceiveAndDelete);
            var time = new System.Timers.Timer();
            time.AutoReset = true;
            time.Interval = 100;
            time.Elapsed += ReadFromBus;
            time.Enabled = true;
            EndPointManager.Instance.WriteMessageToDebugLog("Finished Service Start");
        }

        protected override void OnStop()
        {
        }

        private void ReadFromBus(object source, ElapsedEventArgs e)
        {
            var messages = Client.ReceiveBatch(100000000);

            foreach (var message in messages)
            {
                EndPointManager.Instance.WriteMessageToEndpoints(message);
            }
        }
    }
}
