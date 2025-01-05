using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;
using Exceptions;

namespace Services
{
    public class ItemOrderService
    {
        private readonly IItemOrderRepository _itemOrderRepository;

        public ItemOrderService(IItemOrderRepository itemOrderRepository)
        {
            _itemOrderRepository = itemOrderRepository;
        }

        public ItemOrder GetItemOrderById(int id)
        {
            ItemOrder itemOrder = _itemOrderRepository.GetItemOrder(id);
            if (itemOrder == null)
            {
                throw new ItemOrderNotFoundException();
            }
            return itemOrder;
        }


        public ICollection<ItemOrder> GetItemOrderByIdOrder(int id)
        {
            return _itemOrderRepository.GetItemOrderByIdOrder(id);
        }

        public void AddItemOrder(ItemOrder itemOrder)
        {
            if (_itemOrderRepository.GetItemOrder(itemOrder.Id) != null)
            {
                throw new ItemOrderExistException();
            }
            else 
                _itemOrderRepository.AddItemOrder(itemOrder);
        }

        public void DelItemOrder(int id)
        {
            if (_itemOrderRepository.GetItemOrder(id) == null)
            {
                throw new ItemOrderNotFoundException();
            }
            else
                _itemOrderRepository.DelItemOrder(_itemOrderRepository.GetItemOrder(id));
        }

        public void UpdateItemOrder(ItemOrder itemOrder)
        {
            if (_itemOrderRepository.IsExistItemOrder(itemOrder) == false)
            {
                throw new ItemOrderNotFoundException();
            }
            else
                _itemOrderRepository.UpdateItemOrder(itemOrder);
        }
    }
}
