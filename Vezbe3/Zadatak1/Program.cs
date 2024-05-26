using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "fajl.txt";
            
            while (true)
            {
                Klasa k = new Klasa(path);
                char c = ' ';
                bool flag = true;
                while (flag)
                {   

                    Console.WriteLine("----------------------\n***[" + path + "]***");
                    Console.Write("Unesi opciju koju zelis:\n1.Ispisi ceo fajl\n2.Dodaj na kraj fajla\n3.Trazi u tekstu\n4.Obrisi ceo sadrzaj\nx.Izlaz iz fajla\n>>");
                    c = char.Parse(Console.ReadLine());
                    switch (c)
                    {
                        case '1':
                            k.ReadAll();
                            break;
                        case '2':
                            Console.WriteLine("Unesi string:");

                            k.AddToEnd(Console.ReadLine());
                            break;
                        case '3':
                            Console.WriteLine("Unesite reci za pretragu (unesi x za kraj):");
                            List<string> lista = new List<string>();
                            string word;
                            while ((word = Console.ReadLine()) != "x")
                            {
                                lista.Add(word);
                            }
                            k.SearchTypes(lista);
                            break;
                        case '4':
                            k.DeleteAll();
                            break;

                        case 'x':
                            flag = false;
                            k.Dispose();
                            break;

                    }
                }
                string input;
                Console.WriteLine("Da li zelis da nastavis? Unesi \"x\" za kraj ili novu putanju fajla:");
                if ((input = Console.ReadLine()) == "x")
                {
                    break;
                }
                path = input.Trim();
            }
        }
    }
}
