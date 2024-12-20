﻿using StoreManagement.Models;

namespace StoreManagement.Interfaces
{
    public interface IOrder
    {
        void CreateOrder (Order order);
        void Update(Order order);
        List<Order> GetOfUser(string currentUserID);
        List<Order> GetAll();
        Order GetById(int? id);


    }
}
