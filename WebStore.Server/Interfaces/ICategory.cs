using WebStore.Server.Models;

namespace WebStore.Server.Interfaces
{
    public interface ICategory
    {
        Task<IEnumerable<Category>> GetAll();
        Task Insert(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task<Category> GetById(int? id);
    }
}
