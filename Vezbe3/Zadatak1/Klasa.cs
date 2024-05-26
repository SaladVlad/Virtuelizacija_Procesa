using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zadatak1
{
    internal class Klasa : IDisposable
    {
        string path;

        public Klasa(string path)
        {
            this.path = path;
        }

        public void AddToEnd(string text)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path,true))
                {
                    sw.WriteLine(text);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
            

        public void DeleteAll()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path,false))
                {
                    writer.WriteLine(string.Empty);
                }
            }
            catch( Exception ex ) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public void ReadAll()
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    Console.WriteLine("######################################");
                    while (!reader.EndOfStream)
                    {
                        Console.WriteLine(reader.ReadLine());
                    }
                    Console.WriteLine("######################################");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public void SearchTypes(List<string> words)
        {
            string line;
            foreach(string word in words)
            {
                Console.WriteLine(word);
                int count = 0;
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (!sr.EndOfStream)
                        {
                            line = sr.ReadLine();
                            if (line.Contains(word))
                            {
                                count = Regex.Matches(line, word).Count;
                            }
                        }
                    }
                    Console.WriteLine("Count for [" + word + "]: " + count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
        
        
        private bool disposed = false;

        public string Path { get => path; }

        ~Klasa() {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }
                disposed = true;
            }
        }

    }
}
