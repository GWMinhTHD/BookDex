﻿using StoreManagement.Interfaces;
using StoreManagement.Models;
using StoreManagement.Data;

namespace StoreManagement.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly AppDBContext _context;
        public CategoryRepository(AppDBContext context)
        {
            _context = context;
        }

        public void Delete(Category category)
        {
            _context.Category.Remove(category);
        }

        public List<Category> GetAll()
        {
            return _context.Category.ToList();
        }

        public Category GetById(int? id)
        {
            return _context.Category.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Category category)
        {
            _context.Category.Add(category);
        }

        public void Update(Category category)
        {
            _context.Category.Update(category);
        }
    }
}
