using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    
    public class LibraryService : ILibrary
    {
        public bool AddNewBook(Book book)
        {
            if (!Database.CollectionOfBooks.ContainsKey(book.Id))
            {
                Database.CollectionOfBooks.Add(book.Id,book);
                return true;
            }
            return false;
        }

        public void DeleteBook(int id)
        {
            if (!Database.CollectionOfBooks.Remove(id))
            {
                throw new FaultException<CustomException>(new CustomException("Book doesn't exists"));
            }
        }

        public List<Book> GetAllBooks()
        {
            return new List<Book>(Database.CollectionOfBooks.Values);
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            List<Book> resultBooks = new List<Book>();
            foreach (Book book in Database.CollectionOfBooks.Values)
            {
                if (book.Author.Equals(author))
                {
                    resultBooks.Add(book);
                }
            }
            return resultBooks;
        }

        [OperationBehavior(AutoDisposeParameters = true)]
        public FileManipulationResults GetFiles(FileManipulationOptions options)
        {
            FileManipulationResults results = new FileManipulationResults(ResultType.Success,"");

            try
            {
                string fileDirectoryPath = ConfigurationManager.AppSettings["path"];
                if (!Directory.Exists(fileDirectoryPath))
                {
                    results.ResultType = ResultType.Warning;
                    results.ResultMessage = "There are no directories on the service.";
                    return results;
                }

                string[] files = Directory.GetFiles(fileDirectoryPath);
                foreach(string filepath in files)
                {
                    string fileName = Path.GetFileName(filepath);
                    if (fileName.StartsWith(options.KeyWord,StringComparison.CurrentCultureIgnoreCase))
                    {
                        using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        {
                            MemoryStream ms = new MemoryStream();
                            fs.CopyTo(ms);
                            results.MemoryStreamCollection.Add(fileName,ms);
                        }
                    }
                }

                results.ResultType = ResultType.Success;
            }
            catch(Exception ex)
            {
                results.ResultMessage = ex.Message;
                results.ResultType = ResultType.Failed;
            }

            return results;
        }

        [OperationBehavior(AutoDisposeParameters = true)]
        public FileManipulationResults SendFile(FileManipulationOptions options)
        {
            FileManipulationResults results = new FileManipulationResults(ResultType.Warning,"");

            try
            {
                string fileDirectoryPath = ConfigurationManager.AppSettings["path"];
                if (!Directory.Exists(fileDirectoryPath))
                {
                    results.ResultType = ResultType.Warning;
                    results.ResultMessage = "There is no directory on this service.";
                    return results;
                }

                if(options.MemoryStream == null || options.MemoryStream.Length == 0)
                {
                    results.ResultType = ResultType.Warning;
                    results.ResultMessage = "Memory stream does not contain data!";
                    return results;
                }

                using (FileStream fs = new FileStream(fileDirectoryPath + options.KeyWord,FileMode.Create,FileAccess.Write))
                {
                    options.MemoryStream.WriteTo(fs);
                    fs.Dispose();
                    options.MemoryStream.Dispose();
                }

            }
            catch(Exception ex)
            {
                results.ResultMessage = ex.Message;
                results.ResultType = ResultType.Failed;
            }

            return results;
        }
    }
}
