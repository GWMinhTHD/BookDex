using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using WebStore.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Server.Repositories
{
    public class BookRepository : IBook
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public void  Delete(Book book)
        {
            _context.Book.Remove(book);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Book.Include("BookCategories.Category").Include("BookAuthors.Author").ToListAsync();
        }

        public async Task<IEnumerable<Book>> Search(string str)
        {
            return await _context.Book.Include("BookCategories.Category").Where(s => s.Name!.Contains(str)).ToListAsync();
        }

        public async Task<Book> GetById(int? id)
        {
            return await _context.Book.Include("BookCategories.Category").Include("BookAuthors.Author").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(Book book)
        {
           await _context.Book.AddAsync(book);
        }

        public void Update(Book book)
        {
            _context.Book.Update(book);
        }
        public void ResetAuthor(Book book)
        {
            _context.BookAuthor.RemoveRange(_context.BookAuthor.Where(c => c.BookId == book.Id));
        }
        public void ResetCategory(Book book)
        {
            _context.BookCategory.RemoveRange(_context.BookCategory.Where(c => c.BookId == book.Id));
        }

        
    }
}
