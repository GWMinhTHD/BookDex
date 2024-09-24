using StoreManagement.Models;

namespace StoreManagement.Interfaces
{
    public interface IAuthor
    {
        List<Author> GetAll();
        void Insert(Author author);
        void Update(Author author);
        void Delete(Author author);
        Author GetById(int? id);
    }
}
