using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;
using Models;
using Data;

namespace ItegrationalTests.Fixture
{
    public class IntegrationFixture
    {
        public dbContextFactory _dbContextFactory = new pgSqlDbContextFactory("Server=localhost;Username=postgres;Password=anhyeuem;Database=test;");

        //public dbContextFactory _dbContextFactory = new InMemoryDbContextFactory();
        public static bool SkipTest = Environment.GetEnvironmentVariable("skip") == "true";
        //public static string TestSkipTest = Environment.GetEnvironmentVariable("skip");
        protected UserObjectMother userOM = new UserObjectMother();
        protected ProductObjectMother productOM = new ProductObjectMother();
        protected OrderObjectMother orderOM = new OrderObjectMother();
        protected CartObjectMother cartOM = new CartObjectMother();
        protected ItemOrderObjectMother itemOrderOM = new ItemOrderObjectMother();
        protected ItemCartObjectMother itemCartOM = new ItemCartObjectMother();
        public IntegrationFixture()
        {
           
        }
        //~IntegrationFixture()
        //{
        //    _dbContextFactory.get_db_context().Database.EnsureDeleted();

        //}
        public List<User> AddUsers(int cnt = 10)
        {
            var users = new List<User>();
            for (int i = 0; i < cnt; i++)
            {
                users.Add(userOM.CreateUser(i + 1, $"nametest {i + 1}")
                                    .WithLogin($"logintest {i + 1}")
                                    .WithPassword($"passwordtest {i + 1}")
                                    .BuildCoreModel());

            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.users.AddRange(users);
            db_context.SaveChanges();
            return users;
        }

        public List<Product> AddProducts(int cnt = 10)
        {
            var products = new List<Product>();
            for (int i = 0; i < cnt; i++)
            {
                products.Add(productOM.CreateProduct().WithId(i + 1).WithName($"nametest {i + 1}").WithPrice(i * 10).WithQuantity(i + 2).WithDescription($"Descriptiontest {i + 1}").BuildCoreModel());

            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.products.AddRange(products);
            db_context.SaveChanges();

            return products;
        }
        public List<Order> AddOrders(int cnt = 10)
        {
            var orders = new List<Order>();
            for (int i = 0; i < cnt; i++)
            {
                orders.Add(orderOM.CreateOrder().WithId(i + 1).WithIdUser(i + 1).BuildCoreModel());
            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.orders.AddRange(orders);
            db_context.SaveChanges();

            return orders;
        }
        public List<Cart> AddCarts(int cnt = 10)
        {
            var carts = new List<Cart>();
            for (int i = 0; i < cnt; i++)
            {
                carts.Add(cartOM.CreateCart()
                                    .WithId(i + 1)
                                    .WithIdUser(i + 1)
                                    .BuildCoreModel());
            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.carts.AddRange(carts);
            db_context.SaveChanges();

            return carts;
        }

        public List<ItemOrder> AddItemOrders(int cnt = 10)
        {
            var itemOrders = new List<ItemOrder>();
            for (int i = 0; i < cnt; i++)
            {
                itemOrders.Add(itemOrderOM.CreateItemOrder().WithId(i + 1).WithIdProduct(i + 1).WithIdOrder(i + 1).BuildCoreModel());
            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.itemorders.AddRange(itemOrders);
            db_context.SaveChanges();

            return itemOrders;
        }
        public List<ItemCart> AddItemCarts(int cnt = 10)
        {
            var itemCarts = new List<ItemCart>();
            for (int i = 0; i < cnt; i++)
            {
                itemCarts.Add(itemCartOM.CreateItemCart().WithId(i + 1).WithIdProduct(i + 1).WithIdCart(i + 1).WithQuantity(1).BuildCoreModel());
            }
            using var db_context = _dbContextFactory.get_db_context();
            db_context.itemcarts.AddRange(itemCarts);
            db_context.SaveChanges();

            return itemCarts;
        }

        //public void Dispose()
        //{
        //    _dbContextFactory.get_db_context().Database.EnsureDeleted();
        //}
    }
}
