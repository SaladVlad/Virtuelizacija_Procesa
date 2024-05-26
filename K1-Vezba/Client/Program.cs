using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Client
{
    internal class Program
    {
        static void Main()
        {
            ChannelFactory<ILibrary> factory = new ChannelFactory<ILibrary>("LibraryService");
            ILibrary proxy = factory.CreateChannel();

            int selectedNumber;
            do
            {
                selectedNumber = PrintMenu();
                switch (selectedNumber)
                {
                    case 0:
                        Console.WriteLine("You need to select existing option");
                        break;
                    case 1:
                        SendFileOption(proxy);
                        break;
                    case 2:
                        ReceiveFilesOption(proxy);
                        break;
                    case 3:
                        GetAllBooks(proxy);
                        break;
                    case 4:
                        AddBook(proxy);
                        break;
                    case 5:
                        DeleteBook(proxy);
                        break;
                }
            }
            while (selectedNumber != 6);


        }
        static int PrintMenu()
        {
            Console.WriteLine("Select an option");
            Console.WriteLine("1. Send Files");
            Console.WriteLine("2. Receive files");
            Console.WriteLine("3. Get all books");
            Console.WriteLine("4. Add book");
            Console.WriteLine("5. Delete book");
            Console.WriteLine("6. Exit program");
            if (Int32.TryParse(Console.ReadLine(), out int number))
            {
                if (number >= 1 && number <= 6)
                {
                    return number;
                }
            }
            return 0;
        }

        static void GetAllBooks(ILibrary proxy)
        {
            foreach (Book b in proxy.GetAllBooks())
            {
                Console.WriteLine($"{b.Name}: {b.Author}; {b.Price}");
            }
        }

        static void AddBook(ILibrary proxy)
        {
            Console.Write("Enter book id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter book name: ");
            var name = Console.ReadLine();
            Console.Write("Enter author name: ");
            var author = Console.ReadLine();
            Console.Write("Enter book release date (DD-MM-YYYY): ");
            var date = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter book price: ");
            int price = int.Parse(Console.ReadLine());

            proxy.AddNewBook(new Book(id, name, author, date, price));
            
        }

        static void DeleteBook(ILibrary proxy)
        {
            Console.Write("Enter book id: ");
            int id = int.Parse(Console.ReadLine());

            proxy.DeleteBook(id);
        }

        

        static void SendFileOption(ILibrary proxy)
        {
            Console.WriteLine("Please input file that you want to sent");
            string fileName = Console.ReadLine();
            FileManipulationResults results =
                proxy.SendFile(new FileManipulationOptions(GetMemoryStream(fileName), fileName));

            switch (results.ResultType)
            {
                case ResultType.Success:
                    Console.WriteLine("File is sucessfuly sent.");
                    break;
                case ResultType.Warning:
                    Console.WriteLine($"[WARNING] Send File return message:{results.ResultMessage}");
                    break;
                case ResultType.Failed:
                    Console.WriteLine($"[ERROR] Send File return message:{results.ResultMessage}");
                    break;
            }
        }

        static void ReceiveFilesOption(ILibrary proxy)
        {
            Console.WriteLine("Please input key word for files");
            string keyWord = Console.ReadLine();
            FileManipulationResults results = proxy.GetFiles(new FileManipulationOptions(keyWord));

            switch (results.ResultType)
            {
                case ResultType.Success:
                    DownloadFiles(results);
                    break;
                case ResultType.Warning:
                    Console.WriteLine($"[WARNING] Receive Files return message:{results.ResultMessage}");
                    break;
                case ResultType.Failed:
                    Console.WriteLine($"[ERROR] Receive Files return message:{results.ResultMessage}");
                    break;
            }
        }

        public static MemoryStream GetMemoryStream(string fileName)
        {
            var uploadPath = ConfigurationManager.AppSettings["uploadPath"];
            if (!Directory.Exists(uploadPath))
            {
                Console.WriteLine($"Cannot process the file because directory not exists.");
                return null;
            }

            MemoryStream memoryStream = new MemoryStream();
            FileStream fileStream = null;
            string filePath = $"{uploadPath}/{fileName}";
            try
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                fileStream.CopyTo(memoryStream);
                fileStream.Close();

            }
            catch (IOException e)
            {
                Console.WriteLine($"Cannot process the file {filePath}. Message: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {filePath}.  Message: {e.Message}");
            }
            finally
            {
                fileStream?.Dispose();
            }
            return memoryStream;
        }

        public static void DownloadFiles(FileManipulationResults results)
        {
            var downloadPath = ConfigurationManager.AppSettings["downloadPath"];
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            foreach (KeyValuePair<string, MemoryStream> stream in results.MemoryStreamCollection)
            {
                string fileName = stream.Key;
                using (FileStream fileStream = new FileStream($"{downloadPath}\\{fileName}", FileMode.Create, FileAccess.Write))
                {
                    stream.Value.WriteTo(fileStream);
                    fileStream.Dispose();
                    stream.Value.Dispose();
                    Console.WriteLine($"Downloaded file {stream.Key}");
                }
            }

        }
    }
}
