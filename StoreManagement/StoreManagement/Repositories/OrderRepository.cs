﻿using StoreManagement.Data;
using StoreManagement.Interfaces;
using StoreManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreManagement.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly AppDBContext _context;
        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }
        public void CreateOrder(Order order)
        {
            _context.Order.Add(order);
        }

        public List<Order> GetAll()
        {
            return _context.Order.ToList();
        }

        public Order GetById(int? id)
        {
            return _context.Order.Include("OrderBooks").Include("User").FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetOfUser(string currentUserID)
        {
            return _context.Order.Where(o => o.UserId == currentUserID).ToList();
        }

        public void Update(Order order)
        {
            _context.Order.Update(order);
        }
    }
}
