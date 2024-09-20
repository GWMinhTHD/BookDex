using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using WebStore.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Server.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Delete(Category category)
        {
            _context.Category.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetById(int? id)
        {
            return await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(Category category)
        {
            await _context.Category.AddAsync(category);
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }
    }
}
