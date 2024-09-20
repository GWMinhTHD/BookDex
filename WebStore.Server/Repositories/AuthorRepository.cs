using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using WebStore.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Server.Repositories
{
    public class AuthorRepository : IAuthor
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _context.Author.ToListAsync();
        }

        public void Delete(Author author)
        {
            _context.Author.Remove(author);
        }

        public async Task<Author> GetById(int? id)
        {
            return await _context.Author.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(Author author)
        {
            await _context.Author.AddAsync(author);
        }

        public void Update(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
        }
    }
}