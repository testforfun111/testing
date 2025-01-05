using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);
        ICollection<Order> GetAllOrders();
        ICollection<Order> GetAllOrdersByIdUser(int id_user);
        void AddOrder(Order order);
        void DelOrder(Order order);

        void UpdateOrder(Order order);

        bool IsExistOrder(Order order);
    }
}
