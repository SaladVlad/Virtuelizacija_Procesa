using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class CustomException
    {
        [DataMember]
        public string Message { get; set; }

        public CustomException(string message) {
            this.Message = message;
        }
    }
}