using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces
{
    public interface IItemOrderRepository
    {
        ItemOrder GetItemOrder(int id);

        void AddItemOrder(ItemOrder itemOrder);

        void DelItemOrder(ItemOrder itemOrder);

        void UpdateItemOrder(ItemOrder itemOrder);

        bool IsExistItemOrder(ItemOrder itemOrder);

        ICollection<ItemOrder> GetItemOrderByIdOrder(int id);
    }
}
