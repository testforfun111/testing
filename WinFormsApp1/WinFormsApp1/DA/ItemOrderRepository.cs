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
    public class ItemOrderRepository : IItemOrderRepository
    {
        private readonly DataContext _context;
        public ItemOrderRepository(DataContext context)
        {
            _context = context;
        }
        public ItemOrder GetItemOrder(int id)
        {
            return _context.itemorders.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<ItemOrder> GetItemOrderByIdOrder(int id)
        {
            return _context.itemorders.Where(o => o.Id_order == id).ToList();
        }

        public void AddItemOrder(ItemOrder itemOrder)
        {
            List<ItemOrder> items = _context.itemorders.Count() > 0 ? _context.itemorders.ToList() : null;
            int temp_id = 0;
            if (items != null)
            {
                foreach (ItemOrder tmp in items)
                    temp_id = (tmp.Id > temp_id ? tmp.Id : temp_id);
            } 
            itemOrder.Id = temp_id + 1;
            _context.itemorders.Add(itemOrder);
            _context.SaveChanges();
        }

        public void DelItemOrder(ItemOrder itemOrder)
        {
            _context.Remove(itemOrder);
            _context.SaveChanges();
        }
        public void UpdateItemOrder(ItemOrder itemOrder)
        {
            var itemorderToUpdate = _context.itemorders.FirstOrDefault(o => o.Id == itemOrder.Id);
            if (itemorderToUpdate != null)
            {
                itemorderToUpdate.Quantity = itemOrder.Quantity;
                itemorderToUpdate.Id_product = itemOrder.Id_product;
            }
            _context.SaveChanges();
        }

        public bool IsExistItemOrder(ItemOrder itemOrder)
        {
            return _context.itemorders.Any(u => u.Id == itemOrder.Id);
        }
    }
}
