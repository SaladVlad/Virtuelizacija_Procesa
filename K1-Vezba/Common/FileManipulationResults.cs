using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public enum ResultType { [EnumMember] Success, [EnumMember] Warning, [EnumMember] Failed }

    [DataContract]
    public class FileManipulationResults : IDisposable
    {
        ResultType resultType;
        string resultMessage;
        Dictionary<string, MemoryStream> memoryStreamCollection;

        public FileManipulationResults(ResultType resultType, string resultMessage)
        {
            this.ResultType = resultType;
            this.ResultMessage = resultMessage;
            MemoryStreamCollection = new Dictionary<string, MemoryStream>();
        }

        [DataMember]
        public ResultType ResultType { get => resultType; set => resultType = value; }

        [DataMember]
        public string ResultMessage { get => resultMessage; set => resultMessage = value; }

        [DataMember]
        public Dictionary<string, MemoryStream> MemoryStreamCollection { get => memoryStreamCollection; set => memoryStreamCollection = value; }

        public void Dispose()
        {
            if (memoryStreamCollection == null)
                return;

            foreach (KeyValuePair<string, MemoryStream> kvp in MemoryStreamCollection)
            {
                kvp.Value.Dispose();
            }
            MemoryStreamCollection.Clear();
            MemoryStreamCollection = null;
        }
    }
}
