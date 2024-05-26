using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IApoteka> factory = new ChannelFactory<IApoteka>("NazivServisa");
            IApoteka proxy = factory.CreateChannel();

            Lek l1 = new Lek(1,"Ime1", 5, new List<string>(){ "s1","s2" });
            Lek l2 = new Lek(2,"Ime2", 15, new List<string>() { "s1", "s2","s3" });
            Lek l3 = new Lek(3,"Ime3", 25, new List<string>() { "s3","s2" });
            Lek l4 = new Lek(4,"Ime4", 1, new List<string>() { "s4", "s5" });
            Lek l5 = new Lek(5,"Ime5", 9, new List<string>() { "s1", "s6" });

            Console.WriteLine("Test Dodavanja");
            proxy.DodajLek(l1);
            proxy.DodajLek(l2);
            proxy.DodajLek(l3);
            proxy.DodajLek(l4);
            proxy.DodajLek(l5);

            List<Lek> proba = proxy.dobaviSveLekove();
            foreach(Lek lek in proba)
            {
                Console.WriteLine(lek);
            }
            Console.WriteLine("^^^Dodavanje uspesno");

            Console.WriteLine("################################");
            Console.WriteLine("Test Brisanja");
            try
            {
                proxy.ObrisiLek(2);
            }
            catch (FaultException<CustomException> e)
            {
                Console.WriteLine(e.Detail.Message);
            }
            proba = proxy.dobaviSveLekove();
            foreach (Lek lek in proba)
            {
                Console.WriteLine(lek);
            }
            Console.WriteLine("^^^Brisanje uspesno OBRISAN ID 2");

            try
            {
                proxy.ObrisiLek(25);
            }
            catch (FaultException<CustomException> e)
            {
                Console.WriteLine(e.Detail.Message);
            }
            Console.WriteLine("^^^Brisanje ne uspesno nema id 25");

            Console.WriteLine("################################");
            Console.WriteLine("Test promene kolicine");
            try
            {
                proxy.PromeniKolicinuLek(1, 5, true);
            }
            catch (FaultException<CustomException> e)
            {
                Console.WriteLine(e.Detail.Message);
            }
            proba = proxy.dobaviSveLekove();
            foreach (Lek lek in proba)
            {
                Console.WriteLine(lek);
            }
            Console.WriteLine("^^^Test uspeo dodano 5 na kolicinu za ID 1");

            try
            {
                proxy.PromeniKolicinuLek(1, 999, false);
            }
            catch (FaultException<CustomException> e)
            {
                Console.WriteLine(e.Detail.Message);
            }
            Console.WriteLine("^^^Test pao pokusam da oduzmem 999 od kolicine pa da kolicina ode u minus");

            try
            {
                proxy.PromeniKolicinuLek(11, 5, true);
            }
            catch (FaultException<CustomException> e)
            {
                Console.WriteLine(e.Detail.Message);
            }
            Console.WriteLine("^^^Test pao nema id 11");

            Console.WriteLine("################################");
            Console.WriteLine("Test Pronalaska sastojka");

            List<Lek> le = proxy.pronadjiLekNaOsnovuSastojka("s1");
            if(le.Count == 0)
            {
                Console.WriteLine("Prazna lista\n");
            }
            else
            {
                foreach (var l in le)
                {
                    Console.WriteLine(l);
                }
            }
            Console.WriteLine("^^^Test uspeo pronadjeni lekovi sa sastojkom s1");


            le = proxy.pronadjiLekNaOsnovuSastojka("s100");
            if (le.Count == 0)
            {
                Console.WriteLine("Prazna lista\n");
            }
            else
            {
                foreach (var l in le)
                {
                    Console.WriteLine(l);
                }
            }
            Console.WriteLine("^^^Test pao nema sastojka s100");

            Console.WriteLine("################################");
            Console.WriteLine("Test dodavanja sastojka");
            try
            {
                proxy.dodajSastojak(4, "s223");
            }catch(FaultException<CustomException> e)
            {
                Console.WriteLine(e.Detail.Message);
            }
            proba = proxy.dobaviSveLekove();
            foreach (Lek lek in proba)
            {
                Console.WriteLine(lek);
            }
            Console.WriteLine("^^^Test uspeo dodat sastojak s223 na ID 4");

            try
            {
                proxy.dodajSastojak(100, "s22");
            }
            catch (FaultException<CustomException> e)
            {
                Console.WriteLine(e.Detail.Message);
            }
            Console.WriteLine("^^^Test pao nema id 100");
            proba = proxy.dobaviSveLekove();
            foreach (Lek lek in proba)
            {
                Console.WriteLine(lek);
            }
            Console.WriteLine("################################");
            Console.WriteLine("Dosao do kraja");
        }
    }
}
