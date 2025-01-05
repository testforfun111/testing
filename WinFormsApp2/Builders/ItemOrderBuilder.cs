using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class ItemOrderBuilder
    {
        private int id;
        private int id_product;
        private int id_order;
        private int quantity;
        public ItemOrderBuilder()
        {
            id = 1;
            id_product = 1;
            id_order = 1;
            quantity = 1;
        }
        public ItemOrderBuilder WithId(int Id)
        {
            id = Id;
            return this;
        }
        public ItemOrderBuilder WithIdProduct(int Id_product)
        {
            id_product = Id_product;
            return this;
        }
        public ItemOrderBuilder WithIdOrder(int Id_order)
        {
            id_order = Id_order;
            return this;
        }
        public ItemOrder BuildCoreModel()
        {
            return new ItemOrder(id, id_product, id_order, quantity);
        }
    }
}
