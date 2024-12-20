﻿using StoreManagement.Interfaces;
using StoreManagement.Data;

namespace StoreManagement.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly AppDBContext _context;
        private IBook _book;
        private ICategory _category;
        private IAuthor _author;
        private ICart _cart;
        private IOrder _order;
        public UnitOfWorkRepository(AppDBContext context)
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

        public void AddRange(IEnumerable<object> objects)
        {
            _context.AddRange(objects);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
