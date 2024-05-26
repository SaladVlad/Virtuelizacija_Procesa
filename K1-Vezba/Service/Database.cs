using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Database
    {
        readonly static Dictionary<int, Book> collectionOfBooks;

        static Database()
        {
            collectionOfBooks = new Dictionary<int, Book>();
        }

        public static Dictionary<int, Book> CollectionOfBooks => collectionOfBooks;
    }
}
