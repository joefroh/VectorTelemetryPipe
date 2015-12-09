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

        private string FileNameOverride;

        private const string LogFormat = "{0}: {1}"; //DateTime: message

        StreamWriter writer;

        public virtual string Name
        {
            get
            {
                return "FlatFile";
            }
        }

        public FlatFileEndpoint()
        {

        }

        public FlatFileEndpoint(string fileName)
        {
            FileNameOverride = fileName;
        }

        public void Connect()
        {
            Directory.CreateDirectory(Path);
            if (String.IsNullOrEmpty(FileNameOverride))
            {
                writer = new StreamWriter(String.Format("{0}/{1}.{2}", Path, FileName, Extension), true/*append*/);
            }
            else
            {
                writer = new StreamWriter(String.Format("{0}/{1}.{2}", Path, FileNameOverride, Extension), true/*append*/);
            }
        }

        public void Disconnect()
        {
            writer.Close();
        }

        public void Write(BrokeredMessage message)
        {
            writer.WriteLine(String.Format(LogFormat, message.Properties["timestamp"], message.Properties["message-content"]));
            writer.Flush();
        }
    }
}
