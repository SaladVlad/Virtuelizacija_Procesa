using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class FileManipulationOptions : IDisposable
    {
        MemoryStream memoryStream;
        string keyWord;


        [DataMember]
        public MemoryStream MemoryStream { get => memoryStream; set => memoryStream = value; }

        [DataMember]
        public string KeyWord { get => keyWord; set => keyWord = value; }

        public FileManipulationOptions(MemoryStream memoryStream, string keyWord)
        {
            this.MemoryStream = memoryStream;
            this.KeyWord = keyWord;
        }

        public FileManipulationOptions()
        {
            this.MemoryStream = new MemoryStream();
        }

        public FileManipulationOptions(string keyWord)
        {
            this.keyWord = keyWord;
        }

        public void Dispose()
        {
            if (MemoryStream == null)
                return;
            try
            {
                MemoryStream.Dispose();
                MemoryStream.Close();
                MemoryStream = null;
            }
            catch (Exception)
            {
                Console.WriteLine("Unsuccesful disposing!");
            }
        }

    }
}
