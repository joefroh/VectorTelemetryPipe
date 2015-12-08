using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace VectorPullService
{
    public class FlatFileEndpoint : IEndpoint
    {
        private const string Path = "C:/Temp/Logs";
        private const string FileName = "TestLog";
        private const string Extension = "log";

        private const string LogFormat = "{0}: {1}"; //DateTime: message

        StreamWriter writer;

        public FlatFileEndpoint()
        {

        }

        public void Connect()
        {
            Directory.CreateDirectory(Path);
            writer = new StreamWriter(String.Format("{0}/{1}.{2}", Path, FileName, Extension), true/*append*/);
        }

        public void Disconnect()
        {
            writer.Close();
        }

        public void Write(BrokeredMessage message)
        {
            writer.WriteLine(String.Format(LogFormat, message.EnqueuedTimeUtc, message.Properties["message-content"]));
            writer.Flush();
        }
    }
}
