using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DataBase
    {
        readonly static Dictionary<int, Lek> dataBaseOfLek;

        static DataBase()
        {
            dataBaseOfLek = new Dictionary<int, Lek>();
        }
        public static Dictionary<int, Lek> DataBaseOfLek { get { return dataBaseOfLek; } }
    }
}
