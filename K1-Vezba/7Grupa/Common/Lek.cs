using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Lek
    {
        [DataMember]
        public int Id {  get; set; }
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public int Kolicina { get; set; }
        [DataMember]
        public List<string> Sastojci { get; set; }

        
        public Lek()
        {
            Id = 0;
            Ime = "ime";
            Kolicina = 0;
            Sastojci = new List<string>();
        }

        public Lek(int id,string ime, int kolicina, List<string> sastojci)
        {
            Id = id;
            Ime = ime;
            Kolicina = kolicina;
            Sastojci = sastojci;
        }

        public override string ToString()
        {
            string ret = $"ID: {Id}, IME: {Ime}, KOLICINA: {Kolicina}, Sastojak: ";
            foreach(string s in Sastojci)
            {
                ret += s ;
                ret += " ";
            }
            ret += "\n";
            return ret;
        }
    }
}
