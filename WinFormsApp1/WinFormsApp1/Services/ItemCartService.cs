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
    public class ItemCartService
    {
        private readonly IItemCartRepository _itemCartRepository;

        public ItemCartService (IItemCartRepository itemCartRepository)
        {
            _itemCartRepository = itemCartRepository;   
        }
        public ItemCart GetItemCartById(int id)
        {
            ItemCart itemCart = _itemCartRepository.GetItemCart(id);
            if (itemCart == null)
            {
                throw new ItemCartNotFoundException();
            }
            return itemCart;
        }

        public void AddItemCart(ItemCart itemCart)
        {
            if (_itemCartRepository.IsExistItemCartWithProduct(itemCart) == true)
            {
                itemCart.Quantity += 1;
                _itemCartRepository.UpdateItemCart(itemCart);
            }
            else
                _itemCartRepository.AddItemCart(itemCart);
        }

        public void DelItemCart(int id)
        {
            if (_itemCartRepository.GetItemCart(id) == null)
            {
                throw new ItemCartNotFoundException();
            }
            else
                _itemCartRepository.DelItemCart(_itemCartRepository.GetItemCart(id));
        }

        public void UpdateItemCart(ItemCart itemCart)
        {
            if (_itemCartRepository.IsExistItemCart(itemCart) == false)
            {
                throw new ItemCartNotFoundException();
            }
            else
                _itemCartRepository.UpdateItemCart(itemCart);
        }

        public List<ItemCart> GetAllItemCartByIdCart(int id_cart)
        {
            return _itemCartRepository.GetAllItemCartByIdCart(id_cart);
        }
    }
}
