using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class ItemCartBuilder
    {
        private int id;
        private int id_product;
        private int id_cart;
        private int quantity;
        public ItemCartBuilder()
        {
            id = 1;
            id_product = 1;
            id_cart = 1;
            quantity = 1;
        }
        public ItemCartBuilder WithId(int Id)
        {
            id = Id;
            return this;
        }
        public ItemCartBuilder WithIdProduct(int Id_product)
        {
            id_product = Id_product;
            return this;
        }
        public ItemCartBuilder WithIdCart(int Id_cart)
        {
            id_cart = Id_cart;
            return this;
        }
        public ItemCartBuilder WithQuantity(int Quantity)
        {
            quantity = Quantity;
            return this;
        }
        public ItemCart BuildCoreModel()
        {
            return new ItemCart(id, id_product, id_cart, quantity);
        }
    }
}
