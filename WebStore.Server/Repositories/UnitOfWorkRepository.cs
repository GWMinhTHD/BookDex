using WebStore.Server.Interfaces;
using WebStore.Server.Data;
using WebStore.Server.Repositories;

namespace BookShop1Asm.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IBook _book;
        private ICategory _category;
        private IAuthor _author;
        private ILibrary _library;
        private ICart _cart;
        private IOrder _order;
        public UnitOfWorkRepository(AppDbContext context)
        {
            _context = context;
        }

        public IBook Book
        {
            get
            {
                return _book = _book ?? new BookRepository(_context);
            }
        }

        public ICategory Category
        {
            get
            {
                return _category = _category ?? new CategoryRepository(_context);
            }
        }

        public IAuthor Author
        {
            get
            {
                return _author = _author ?? new AuthorRepository(_context);
            }
        }

        public ILibrary Library
        {
            get
            {
                return _library = _library ?? new LibraryRepository(_context);
            }
        }

        public ICart Cart
        {
            get
            {
                return _cart = _cart ?? new CartRepository(_context);
            }
        }

        public IOrder Order
        {
            get
            {
                return _order = _order ?? new OrderRepository(_context);
            }
        }

        public async Task AddRange(IEnumerable<object> objects)
        {
            await _context.AddRangeAsync(objects);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
