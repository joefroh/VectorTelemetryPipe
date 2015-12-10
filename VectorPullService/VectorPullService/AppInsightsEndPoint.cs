using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace VectorPullService
{
    public class AppInsightsEndPoint : IEndpoint
    {
        TelemetryClient Client;
        public string Name
        {
            get
            {
                return "AppInsights";
            }
        }

        public void Connect()
        {
            Client = new TelemetryClient();
        }

        public void Disconnect()
        {
            
        }

        public void Write(BrokeredMessage message)
        {
            TraceTelemetry tele = new TraceTelemetry();
            tele.Timestamp = (DateTime)message.Properties["timestamp"];
            tele.SeverityLevel = SeverityLevel.Information;
            tele.Message = (string)message.Properties["message-content"];

            Client.TrackTrace(tele);
        }
    }
}
