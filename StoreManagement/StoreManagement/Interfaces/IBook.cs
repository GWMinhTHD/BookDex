using StoreManagement.Models;

namespace StoreManagement.Interfaces
{
    public interface IBook
    {
        List<Book> GetAll();
        List<Book> Search(string str);
        void Insert(Book book);
        void Update(Book book);
        void Delete(Book book);
        void ResetAuthor(Book book);
        void ResetCategory(Book book);
        Book GetById(int? id);

        

    }
}
