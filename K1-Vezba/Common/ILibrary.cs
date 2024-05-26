using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ILibrary
    {
        [OperationContract]
        bool AddNewBook(Book book);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void DeleteBook(int id);

        [OperationContract]
        List<Book> GetAllBooks();

        [OperationContract]
        List<Book> GetBooksByAuthor(string author);

        [OperationContract]
        FileManipulationResults SendFile(FileManipulationOptions options);

        [OperationContract]
        FileManipulationResults GetFiles(FileManipulationOptions options);

    }
}
