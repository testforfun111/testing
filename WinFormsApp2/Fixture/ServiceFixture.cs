using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;

namespace UnitTests.Fixture
{
    public class ServiceFixture
    {
        public ServiceFixture() { }
        public List<User> PrepareUsersForTest()
        {
            var userOM = new UserObjectMother();
            var users = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                users.Add(userOM.CreateClient().BuildCoreModel());
            }
            return users;
        }

        public List<Product> PrepareProductsForTest()
        {
            var productOM = new ProductObjectMother();
            var products = new List<Product>();
            for (int i = 0;i < 10;i++)
            {
                products.Add(productOM.CreateProduct().BuildCoreModel());
            }
            return products;
        }

        public List<Order> PrepareOrdersForTest()
        {
            var orderOM = new OrderObjectMother();
            var orders = new List<Order>();
            for (int i = 0; i < 10 ; i++)
            {
                orders.Add(orderOM.CreateOrder().BuildCoreModel());
            }
            return orders;
        }
        public List<Cart> PrepareCartsForTest()
        {
            var CartOM = new CartObjectMother();
            var carts = new List<Cart>();
            for (int i = 0; i < 10; i++)
            {
                carts.Add(CartOM.CreateCart().BuildCoreModel());
            }
            return carts;
        }
        public List<ItemOrder> PrepareItemOrdersForTest()
        {
            var orderOM = new ItemOrderObjectMother();
            var orders = new List<ItemOrder>();
            for (int i = 0; i < 10; i++)
            {
                orders.Add(orderOM.CreateItemOrder().BuildCoreModel());
            }
            return orders;
        }
        public List<ItemCart> PrepareItemCartsForTest()
        {
            var CartOM = new ItemCartObjectMother();
            var carts = new List<ItemCart>();
            for (int i = 0; i < 10; i++)
            {
                carts.Add(CartOM.CreateItemCart().BuildCoreModel());
            }
            return carts;
        }
    }
}
