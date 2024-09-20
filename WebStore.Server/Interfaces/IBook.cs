using WebStore.Server.Models;

namespace WebStore.Server.Interfaces
{
    public interface IBook
    {
        Task<IEnumerable<Book>> GetAll();
        Task<IEnumerable<Book>> Search(string str);
        Task Insert(Book book);
        void Update(Book book);
        void Delete(Book book);
        void ResetAuthor(Book book);
        void ResetCategory(Book book);
        Task<Book> GetById(int? id);

        

    }
}
