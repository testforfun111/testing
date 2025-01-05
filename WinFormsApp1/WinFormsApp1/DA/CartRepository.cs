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
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;
        public CartRepository(DataContext context)
        {
            _context = context;
        }
        public Cart GetCart(int id)
        {
            return _context.carts.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Cart> GetAllCarts()
        {
            return _context.carts.ToList();
        }


        public void AddCart(Cart cart)
        {
            List<Cart> carts = _context.carts.Count() > 0 ? _context.carts.ToList() : null;
            int temp_id = 0;
            if (carts != null)
            {
                foreach (Cart o in carts)
                    temp_id = (o.Id > temp_id ? o.Id : temp_id);
            }
            cart.Id = temp_id + 1;
            _context.carts.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateCart(Cart cart)
        {
            var cartToUpdate = _context.carts.FirstOrDefault(u => u.Id == cart.Id);
            if (cartToUpdate != null)
            {
                cartToUpdate.Data_created = cart.Data_created;
                cartToUpdate.Id_user = cart.Id_user;

                _context.SaveChanges();
            }
        }
        public void DelCart(Cart cart)
        {
            Cart cartToRemove = _context.carts.FirstOrDefault(o => o.Id == cart.Id);
            if (cartToRemove != null)
                _context.Remove(cartToRemove);
            _context.SaveChanges();
        }

        public bool IsExistCart(Cart cart)
        {
            return _context.carts.Any(o => o.Id == cart.Id);
        }
    }
}
