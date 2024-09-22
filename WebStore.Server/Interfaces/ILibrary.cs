using WebStore.Server.Models;

namespace WebStore.Server.Interfaces
{
    public interface ILibrary
    {
        Task<List<Book>> GetUserBooks(ApplicationUser user);
        Task<Library> CheckOwnership(ApplicationUser user, int id);
        Task Insert(string cusId, int bookId);
        void Update(Library library);
    }
}
