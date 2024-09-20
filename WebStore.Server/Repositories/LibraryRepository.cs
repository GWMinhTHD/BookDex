using Microsoft.EntityFrameworkCore;
using System.Net;
using WebStore.Server.Data;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;

namespace WebStore.Server.Repositories
{
    public class LibraryRepository : ILibrary
    {
        private readonly AppDbContext _context;
        public LibraryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Library> CheckOwnership(ApplicationUser user, int id)
        {
            return await _context.Library.FirstOrDefaultAsync(c => c.CusId == user.Id && c.BookId == id);
        }

        public async Task<List<Book>> GetUserBooks(ApplicationUser user)
        {
            return await _context.Library.Include("Book.BookCategories.Category").Include("Book.BookAuthors.Author")
                                   .Where(u => u.CusId == user.Id)
                                   .Select(item => new Book
                                   {
                                    Id = item.BookId,
                                    Name = item.Book.Name,
                                    Cover = item.Book.Cover,
                                    BookAuthors = item.Book.BookAuthors,
                                    BookCategories = item.Book.BookCategories
                                   }).ToListAsync();
        }

        public async Task Insert(string cusId, int bookId)
        {
            var lib = new Library();
            lib.CusId = cusId;
            lib.BookId = bookId;
            await _context.Library.AddAsync(lib);
        }
    }
}
