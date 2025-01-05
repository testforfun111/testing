using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Models;
using Microsoft.Extensions.Logging.Abstractions;
using Data;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        public Order GetOrder(int id)
        {
            return _context.orders.Where(u => u.Id == id).FirstOrDefault();
        }

        public ICollection<Order> GetAllOrders()
        {
            return _context.orders.ToList();
        }
        
        public ICollection<Order> GetAllOrdersByIdUser(int id_user)
        {
            return _context.orders.Where(o => o.Id_user == id_user).ToList();
        }
        public void AddOrder(Order order)
        {
            List<Order> orders = _context.orders.Count() > 0 ? _context.orders.ToList() : null;
            int temp_id = 0;
            if (orders != null)
            {
                foreach (Order o in orders)
                    temp_id = (o.Id > temp_id ? o.Id : temp_id);
            }
            order.Id = temp_id + 1;
            _context.orders.Add(order); 
            _context.SaveChanges();
        }
        
        public void UpdateOrder(Order order)
        {
            var orderToUpdate = _context.orders.FirstOrDefault(u => u.Id == order.Id);
            if (orderToUpdate != null)
            {
                orderToUpdate.Status = order.Status;
                orderToUpdate.Data_created = order.Data_created;
                orderToUpdate.Id_user = order.Id_user;

                _context.SaveChanges();
            }
        }
        public void DelOrder(Order order)
        {
            Order orderToRemove = _context.orders.FirstOrDefault(o => o.Id == order.Id);
            if (orderToRemove != null)
                _context.Remove(orderToRemove);
            _context.SaveChanges();
        }

        public bool IsExistOrder(Order order)
        {
            return _context.orders.Any(o => o.Id == order.Id);
        }
    }
}
