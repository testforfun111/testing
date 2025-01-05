using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class ProductBuilder
    {
        private int id;
        private string name;
        private int price;
        private int quantity;
        private string description;
        public ProductBuilder()
        {
            id = 1;
            name = "PC";
            price = 1;
            quantity = 1;
            description = "test";
        }
        public ProductBuilder WithId(int Id)
        {
            id = Id;
            return this;
        }
        public ProductBuilder WithName(string Name)
        {
            name = Name;
            return this;
        }
        public ProductBuilder WithPrice(int Price)
        {
            price = Price;
            return this;
        }
        public ProductBuilder WithQuantity(int Quantity)
        {
            quantity = Quantity;
            return this;
        }
        public ProductBuilder WithDescription(string Description)
        {
            description = Description;
            return this;
        }
        public Product BuildCoreModel()
        {
            return new Product(id, name, price, quantity, description);
        }
    }
}
