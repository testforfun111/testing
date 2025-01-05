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
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new Exception("Параметр пустой!");
        }

        public ICollection<Cart> GetAllCarts()
        {
            return _cartRepository.GetAllCarts();
        }
        public Cart GetCartById(int id)
        {
            Cart cart = _cartRepository.GetCart(id);
            if (cart == null)
            {
                throw new CartNotFoundException();
            }
            return cart;
        }

        public void AddCart(Cart cart)
        {
            if (_cartRepository.IsExistCart(cart) == true)
            {
                throw new CartExistException();
            }
            else
            {
                _cartRepository.AddCart(cart);
            }
        }

        public void DelCart(int id)
        {
            if (_cartRepository.GetCart(id) == null)
            {
                throw new CartNotFoundException();
            }
            else
            {
                _cartRepository.DelCart(_cartRepository.GetCart(id));
            }
        }

        public void UpdateCart(Cart cart) 
        {
            if (_cartRepository.IsExistCart(cart) == false)
            {
                throw new CartNotFoundException();
            }
            else
            {
                _cartRepository.UpdateCart(cart);
            }
        }
    }
}
