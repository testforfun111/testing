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
    public class ItemCartRepository : IItemCartRepository
    {
        private readonly DataContext _context;
        public ItemCartRepository(DataContext context)
        {
            _context = context;
        }
        public ItemCart GetItemCart(int id)
        {
            return _context.itemcarts.Where(o => o.Id == id).FirstOrDefault();
        }
        
        public void AddItemCart(ItemCart itemCart)
        {
            List<ItemCart> items = _context.itemcarts.Count() > 0 ? _context.itemcarts.ToList() : null;
            int temp_id = 0;
            if (items != null)
            {
                foreach (ItemCart tmp in items)
                    temp_id = (tmp.Id > temp_id ? tmp.Id : temp_id);
            }
            itemCart.Id = temp_id + 1;
            _context.itemcarts.Add(itemCart);
            _context.SaveChanges();
        }
        public void DelItemCart(ItemCart itemCart)
        {
            _context.Remove(itemCart);
            _context.SaveChanges();
        }
        public void UpdateItemCart(ItemCart itemCart)
        {
            var itemorderToUpdate = _context.itemcarts.FirstOrDefault(o => o.Id == itemCart.Id);
            if (itemorderToUpdate != null)
            {
                itemorderToUpdate.Quantity = itemCart.Quantity;
                itemorderToUpdate.Id_product = itemCart.Id_product;
            }
            _context.SaveChanges();
        }

        public bool IsExistItemCart(ItemCart itemCart)
        {
            return _context.itemcarts.Any(u => u.Id == itemCart.Id);
        }

        public bool IsExistItemCartWithProduct(ItemCart itemCart)
        {
            return _context.itemcarts.Any(u => u.Id_product == itemCart.Id_product);
        }

        public List<ItemCart> GetAllItemCartByIdCart(int id_cart)
        {
            return _context.itemcarts.Where(o => o.Id_cart == id_cart).ToList();
        }
    }
}
