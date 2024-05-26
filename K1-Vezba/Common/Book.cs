using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Book
    {
        string name, author;
        DateTime releaseDate;
        int price;
        int id;

        public Book(int id, string name, string author, DateTime releaseDate, int price)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.releaseDate = releaseDate;
            this.price = price;
        }

        public Book()
        {

        }

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string Name { get => name; set => name = value; }
        [DataMember]
        public string Author { get => author; set => author = value; }
        [DataMember]
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        [DataMember]
        public int Price { get => price; set => price = value; }
        
    }
}
