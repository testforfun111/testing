using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace Interfaces
{
    public interface ICartRepository
    {
        ICollection<Cart> GetAllCarts();
        Cart GetCart(int id);

        void AddCart(Cart cart);
        void DelCart(Cart cart);
        void UpdateCart(Cart cart);

        bool IsExistCart(Cart cart);
    }
}
