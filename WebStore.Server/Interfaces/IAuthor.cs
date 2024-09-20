using WebStore.Server.Models;

namespace WebStore.Server.Interfaces
{
    public interface IAuthor
    {
        Task<IEnumerable<Author>> GetAll();
        Task Insert(Author author);
        void Update(Author author);
        void Delete(Author author);
        Task<Author> GetById(int? id);
    }
}
