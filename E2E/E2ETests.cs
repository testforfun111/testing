using Allure.Xunit.Attributes;
using Models;
using Services;
using Repository;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;
using Xunit;
using Interfaces;
using UnitTests.Fixture;
using Exceptions;
using Data;

namespace E2ETests
{
    [AllureOwner("VHD")]
    [AllureSuite("E2E")]
    public class E2ETests
    {
        //private IConfiguration config;
        //private 
        public dbContextFactory _dbContextFactory = new pgSqlDbContextFactory("Server=localhost;Username=postgres;Password=anhyeuem;Database=test;");
        public bool SkipTest = Environment.GetEnvironmentVariable("skip") == "true";

        private UserRepository _userRepo;
        private ProductRepository _productRepo;
        private OrderRepository _orderRepo;
        private CartRepository _cartRepo;
        private ItemCartRepository _itemCartRepo;
        private ItemOrderRepository _itemOrderRepo;


        private UserService _userService;
        private ProductService _productService;
        private OrderService _orderService;
        private CartService _cartService;
        private ItemOrderService _itemOrderService;
        private ItemCartService _itemCartService;
        public E2ETests() 
        {
            _userRepo = new UserRepository(_dbContextFactory.get_db_context());
            _productRepo = new ProductRepository(_dbContextFactory.get_db_context());
            _orderRepo = new OrderRepository(_dbContextFactory.get_db_context());
            _cartRepo = new CartRepository(_dbContextFactory.get_db_context());
            _itemOrderRepo = new ItemOrderRepository(_dbContextFactory.get_db_context());
            _itemCartRepo = new ItemCartRepository(_dbContextFactory.get_db_context());

            _userService = new UserService(_userRepo);
            _productService = new ProductService(_productRepo);
            _orderService = new OrderService(_orderRepo);
            _cartService = new CartService(_cartRepo);
            _itemCartService = new ItemCartService(_itemCartRepo);
            _itemOrderService = new ItemOrderService(_itemOrderRepo);
        }

        [SkippableFact]
        public void TestOrderProduct()
        {
            Skip.If(SkipTest);
            var userOM = new UserObjectMother();
            var productOM = new ProductObjectMother();
            var orderOM = new OrderObjectMother();
            var cartOM = new CartObjectMother();
            var itemCartOM = new ItemCartObjectMother();
            var itemOrderOM = new ItemOrderObjectMother();
            var user = userOM.CreateClient().WithLogin("teste2e").WithPassword("teste2e").WithPassword("teste2e").BuildCoreModel();
            int cnt = _userRepo.GetAll().Count;

            _userService.Register(user.Name, user.Phone, user.Address, user.Email, user.Login, user.Password, user.Role);

            Assert.Equal(cnt + 1, _userRepo.GetAll().Count);

            var actual = _userService.LogIn(user.Login, user.Password);

            Assert.Equal(user.Login, actual.Login);
            Assert.Equal(user.Password, actual.Password);
            Assert.Equal(user.Role, actual.Role);
            Assert.Equal(user.Name, actual.Name);

            var product = productOM.CreateProduct().WithName("teste2eleague").BuildCoreModel();
            int cntProduct = _productRepo.GetAllProducts().Count;

            _productService.AddProduct(product);

            Assert.Equal(cntProduct + 1, _productRepo.GetAllProducts().Count);

            var order = orderOM.CreateOrder().BuildCoreModel();
            _orderService.AddOrder(order);
            _orderService.UpdateOrder(order);

            var cart = cartOM.CreateCart().WithId(2).BuildCoreModel();
            _cartService.AddCart(cart);

            var itemCart = itemCartOM.CreateItemCart().BuildCoreModel();
            int cntItemCart = _itemCartService.GetAllItemCartByIdCart(2).Count();
            _itemCartService.AddItemCart(itemCart);

            Assert.Equal(cntItemCart, _itemCartService.GetAllItemCartByIdCart(2).Count());
        }
    }
}