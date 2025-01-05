using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;
using Exceptions;

namespace Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository; 
        public OrderService (IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order GetOrderById(int id)
        {
            Order order = _orderRepository.GetOrder(id);
            if (order == null)
            {
                throw new OrderNotFoundException();
            } 
            return order;
                
        }

        public void AddOrder(Order order)
        {
            if (_orderRepository.GetOrder(order.Id) == null)
            {
                _orderRepository.AddOrder(order);
            }
            else
                throw new OrderExistException();
        }
        public void DelOrder(int id)
        {   
            if (_orderRepository.GetOrder(id) == null)
            {
                throw new OrderNotFoundException();
            }
            else
                _orderRepository.DelOrder(GetOrderById(id));
        }
        public ICollection<Order> GetOrdersByIdUser(int id_user)
        {
            return _orderRepository.GetAllOrdersByIdUser(id_user);
        }

        public void UpdateOrder(Order order)
        {
            if (_orderRepository.GetOrder(order.Id) == null)
            {
                throw new OrderNotFoundException();
            }
            _orderRepository.UpdateOrder(order);
        }

        public ICollection<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
    }
}
