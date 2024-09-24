using StoreManagement.Interfaces;
using StoreManagement.Models;
using StoreManagement.Data;

namespace StoreManagement.Repositories
{
    public class AuthorRepository : IAuthor
    {
        private readonly AppDBContext _context;
        public AuthorRepository(AppDBContext context)
        {
            _context = context;
        }

        public void Delete(Author author)
        {
            _context.Author.Remove(author);
        }

        public List<Author> GetAll()
        {
            return _context.Author.ToList();
        }

        public Author GetById(int? id)
        {
            return _context.Author.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Author author)
        {
            _context.Author.Add(author);
        }

        public void Update(Author author)
        {
            _context.Author.Update(author);
        }
    }
}