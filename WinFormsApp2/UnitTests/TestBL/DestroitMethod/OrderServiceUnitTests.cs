using Allure.Xunit.Attributes;
using Allure.Xunit.Attributes.Steps;
//using Castle.Core.Logging;
using Exceptions;
using Interfaces;
using Models;
using Services;
using Repository;
using Microsoft.Extensions.Logging.Abstractions;
using UnitTests.Fixture;
using UnitTests.ObjectMothers;
using Xunit;
using Microsoft.VisualBasic.ApplicationServices;

namespace UnitTests.UnitTests.TestBL.DestroitMethod
{
    [AllureOwner("VHD")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("OrderServices Unit tests")]
    [AllureSubSuite("OrderService unit tests Destroit Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class OrderServiceUnitTests
    {
        private DBFixture fixture = new DBFixture();
        private OrderObjectMother OrderOM = new OrderObjectMother();
        [AllureBefore]
        public OrderServiceUnitTests() { }
   
        [Fact]
        public void TestGetOrderByIdSuccessDestroitMethod()
        {
            var Orders = fixture.AddOrders(10);
            var Order = Orders.First();
            IOrderRepository _OrderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            var OrderService = new OrderService(_OrderRepo);

            var actual = OrderService.GetOrderById(Order.Id);

            Assert.Equivalent(Order, actual);
        }

        [Fact]
        public void TestGetOrderByIdFailureDestroitMethod()
        {
            var Orders = fixture.AddOrders(10);
            var Order = Orders.First();
            IOrderRepository _OrderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            var OrderService = new OrderService(_OrderRepo);

            Assert.Throws<OrderNotFoundException>(() => OrderService.GetOrderById(1000));
        }

        [Fact]
        public void TestAddOrderSuccessDestroitMethod()
        {
            var Orders = fixture.AddOrders(10);
            var Order = OrderOM.CreateOrder().WithId(11).BuildCoreModel();
            IOrderRepository _OrderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            var OrderService = new OrderService(_OrderRepo);

            OrderService.AddOrder(Order);
            var actual = OrderService.GetOrderById(Order.Id);
            Assert.Equivalent(Order, actual);
        }
        [Fact]
        public void TestAddOrderFailureDestroitMethod()
        {
            var Orders = fixture.AddOrders(10);
            var Order = Orders.First();
            IOrderRepository _OrderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            var OrderService = new OrderService(_OrderRepo);

            Assert.Throws<OrderExistException>(() => OrderService.AddOrder(Order));
        }

        [Fact]
        public void TestDeleteOrderSuccessDestroitMethod()
        {
            var Orders = fixture.AddOrders(10);
            var Order = Orders.First();
            IOrderRepository _OrderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            var OrderService = new OrderService(_OrderRepo);

            OrderService.DelOrder(Order.Id);
            Assert.Throws<OrderNotFoundException>(() => OrderService.GetOrderById(Order.Id));
        }
        [Fact]
        public void TestDeleteOrderFailureDestroitMethod()
        {
            var Orders = fixture.AddOrders(10);
            IOrderRepository _OrderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            var OrderService = new OrderService(_OrderRepo);
            
            Assert.Throws<OrderNotFoundException>(() => OrderService.DelOrder(100));
        }
        
        public void TestUpdateOrderSuccessDestroitMethod()
        {
            var Orders = fixture.AddOrders(10);
            var Order = Orders.First();
            Order.Status = Status.Delivered;
            IOrderRepository _OrderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            var OrderService = new OrderService(_OrderRepo);

            OrderService.UpdateOrder(Order);
            var actual = OrderService.GetOrderById(Order.Id);
            Assert.Equivalent(Order.Status, Status.Delivered);
        }
        [Fact]
        public void TestUpdateOrderFailureDestroitMethod()
        {
            var Orders = fixture.AddOrders(10);
            var Order = OrderOM.CreateOrder().WithId(100).BuildCoreModel();
            IOrderRepository _OrderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            var OrderService = new OrderService(_OrderRepo);
            
            Assert.Throws<OrderNotFoundException>(() => OrderService.UpdateOrder(Order));
        }

        [Fact]
        public void TestGetAllDestroitMethod()
        {
            var Orders = fixture.AddOrders(10);
            IOrderRepository _OrderRepo = new OrderRepository(fixture._dbContextFactory.get_db_context());
            var OrderService = new OrderService(_OrderRepo);

            var actual = OrderService.GetAllOrders();
            Assert.Equivalent(Orders, actual);

        }
    }
}
