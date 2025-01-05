using Allure.Xunit.Attributes;
using ItegrationalTests.Fixture;
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

namespace ItegrationalTests.IntegrationalTests
{
    [AllureOwner("VHD")]
    [AllureSuite("Integrational Tests")]
    [AllureSubSuite("OrderIntegrational Tests")]
    public class OrderIntegrationTests
    {
        private IntegrationFixture fixture;
        private OrderService OrderService;
        private OrderObjectMother OrderOM = new OrderObjectMother();
        public OrderIntegrationTests()
        {
            fixture = new IntegrationFixture();
            var orderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            OrderService = new OrderService(orderRepo);
        }
        [SkippableFact]
        public void TestGetOrderById()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var Orders = fixture.AddOrders(10);
            var Order = Orders.First();

            var actual = OrderService.GetOrderById(Order.Id);

            Assert.Equivalent(Order.Id_user, actual.Id_user);
            Assert.Equivalent(Order.Status, actual.Status);
        }
        [SkippableFact]
        public void TestAddOrder()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var Orders = fixture.AddOrders(10);
            var Order = OrderOM.CreateOrder().WithId(11).BuildCoreModel();


            OrderService.AddOrder(Order);
            var actual = OrderService.GetOrderById(Order.Id);
            Assert.Equivalent(Order.Id_user, actual.Id_user);
            Assert.Equivalent(Order.Status, actual.Status);
        }
        [SkippableFact]
        public void TestDeleteOrder()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var Orders = fixture.AddOrders(10);
            var Order = Orders.First();
           
            OrderService.DelOrder(Order.Id);
            Assert.Throws<OrderNotFoundException>(() => OrderService.GetOrderById(Order.Id));
        }

        [SkippableFact]
        public void TestUpdateOrder()
        {
            var Orders = fixture.AddOrders(10);
            var Order = Orders.First();
            Order.Status = Status.Delivered;
            
            OrderService.UpdateOrder(Order);
            var actual = OrderService.GetOrderById(Order.Id);
            Assert.Equivalent(Order.Status, Status.Delivered);
        }

        [SkippableFact]
        public void TestGetAll()
        {
            var Orders = fixture.AddOrders(10);
           
            var actual = OrderService.GetAllOrders();
            Assert.Equivalent(Orders.Count, actual.Count());

        }
    }
}

