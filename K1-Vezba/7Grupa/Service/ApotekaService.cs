using Common;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ApotekaService : IApoteka
    {
        public string filePath = "log.txt";

        public List<Lek> dobaviSveLekove()
        {
            List<Lek> ret = new List<Lek>();    
            foreach(Lek lek in DataBase.DataBaseOfLek.Values)
            {
                ret.Add(lek);
            }
            return ret;
        }

        public void DodajLek(Lek lek)
        {
            bool canBeAdded = !DataBase.DataBaseOfLek.ContainsKey(lek.Id);
            if (canBeAdded)
            {
                DataBase.DataBaseOfLek.Add(lek.Id, lek);
            }
            string existingText = File.ReadAllText(filePath, Encoding.UTF8);
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine($"Zavrsena operacija dodaj lek {lek.Ime} {DateTime.Now}\n");
                sw.Write(existingText);
            }
        }

        public void dodajSastojak(int id, string sastojak)
        {
            if (!(DataBase.DataBaseOfLek.ContainsKey(id)))
            {
                throw new FaultException<CustomException>(new CustomException("Ne postoji lek sa tim id"));
            }
            else
            {
                foreach(var l in DataBase.DataBaseOfLek.Values)
                {
                    if(l.Id == id)
                    {
                        l.Sastojci.Add(sastojak);
                        Console.WriteLine($"Uspesno dodat sastojak: {sastojak} u lek: {l.Ime}");
                    }
                }
            }
            string existingText = File.ReadAllText(filePath, Encoding.UTF8);
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine($"Zavrsena operacija dodaj sastojak ID:{id} SASTOJAK:{sastojak} {DateTime.Now}\n");
                sw.Write(existingText);
            }
        }

        public void ObrisiLek(int id)
        {
            if(!(DataBase.DataBaseOfLek.Remove(id)))
            {
                throw new FaultException<CustomException>(new CustomException("Ne postoji lek sa tim id"));
            }
            string existingText = File.ReadAllText(filePath, Encoding.UTF8);
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine($"Zavrsena operacija obrisi lek ID:{id} {DateTime.Now}\n");
                sw.Write(existingText);
            }
        }

        public void PromeniKolicinuLek(int id, int kolicina, bool povecaIliSmanji)
        {
            if(kolicina<0)
            {
                throw new FaultException<CustomException>(new CustomException("Ne moze kolicina da bude manja od 0"));
            }
            else if (!(DataBase.DataBaseOfLek.ContainsKey(id)))
            {
                throw new FaultException<CustomException>(new CustomException("Ne postoji lek sa tim ID"));
            }
            else
            {
                foreach(var l in DataBase.DataBaseOfLek.Values)
                {
                    if(l.Id == id)
                    {
                        if(povecaIliSmanji == true)
                        {
                            l.Kolicina += kolicina;
                        }
                        else
                        {
                            if(l.Kolicina - kolicina< 0)
                            {
                                throw new FaultException<CustomException>(new CustomException("Ne moze kolicina u minus"));
                            }
                            else
                            {
                                l.Kolicina -= kolicina;
                            }
                        }
                    }
                }
            }
            string existingText = File.ReadAllText(filePath, Encoding.UTF8);
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine($"Zavrsena operacija promeni kolicinu leka ID:{id} KOLICINA:{kolicina} {DateTime.Now}\n");
                sw.Write(existingText);
            }
        }

        public List<Lek> pronadjiLekNaOsnovuSastojka(string sastojak)
        {
            List<Lek> ret = new List<Lek>();
            foreach(Lek l in DataBase.DataBaseOfLek.Values)
            {
                if (l.Sastojci.Contains(sastojak))
                {
                    ret.Add(l);
                }
            }

            string existingText = File.ReadAllText(filePath, Encoding.UTF8);
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine($"Zavrsena operacija pronadji lek na osnovu sastojka SASTOJAK:{sastojak} {DateTime.Now}\n");
                sw.Write(existingText);
            }

            return ret;
        }
    }
}
