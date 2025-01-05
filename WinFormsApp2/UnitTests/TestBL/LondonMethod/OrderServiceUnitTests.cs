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
using Moq;

namespace UnitTests.UnitTests.TestBL.LondonMethod
{
    [AllureOwner("VHD")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("OrderServices Unit tests")]
    [AllureSubSuite("OrderService unit tests London Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class OrderServiceUnitTests
    {
        private ServiceFixture fixture = new ServiceFixture();
        private OrderObjectMother OrderOM = new OrderObjectMother();
        [AllureBefore]
       
        [Fact]
        public void TestGetOrderByIdSuccess()
        {
            var Orders = fixture.PrepareOrdersForTest();
            var Order = Orders[0];
            Mock<IOrderRepository> _OrderRepoMock = new Mock<IOrderRepository>();
            _OrderRepoMock.Setup(m => m.GetOrder(Order.Id)).Returns(Orders.FirstOrDefault(u => u.Id == Order.Id));
            var OrderService = new OrderService(_OrderRepoMock.Object);

            var actual = OrderService.GetOrderById(Order.Id);

            Assert.Equal(Order, actual);
            _OrderRepoMock.Verify(m => m.GetOrder(Order.Id), Times.Once());
        }

        [Fact]
        public void TestGetOrderByIdFailure()
        {
            var Orders = fixture.PrepareOrdersForTest();
            var Order = OrderOM.CreateOrder().WithId(11).BuildCoreModel();
            Mock<IOrderRepository> _OrderRepoMock = new Mock<IOrderRepository>();

            _OrderRepoMock.Setup(m => m.GetOrder(Order.Id)).Returns(Orders.FirstOrDefault(u => u.Id == Order.Id));
            var OrderService = new OrderService(_OrderRepoMock.Object);

            Assert.Throws<OrderNotFoundException>(() => OrderService.GetOrderById(Order.Id));
            _OrderRepoMock.Verify(m => m.GetOrder(Order.Id), Times.Once());
        }

        [Fact]
        public void TestAddOrderSuccess()
        {
            var Orders = fixture.PrepareOrdersForTest();
            var Order = OrderOM.CreateOrder().WithId(13).BuildCoreModel();
            Mock<IOrderRepository> _OrderRepoMock = new Mock<IOrderRepository>();

            _OrderRepoMock.Setup(m => m.AddOrder(It.IsAny<Order>())).Callback<Order>(u => Orders.Add(Order)).Verifiable();
            //_OrderRepoMock.Setup(m => m.GetOrder(Order.Id)).Returns(Order);
            _OrderRepoMock.Setup(m => m.GetOrder(Order.Id)).Returns(Orders.Find(u => u.Id == Order.Id));
            var OrderService = new OrderService(_OrderRepoMock.Object);

            OrderService.AddOrder(Order);
            Assert.Equivalent(Orders.Count(), 11);
            _OrderRepoMock.Verify(m => m.GetOrder(Order.Id), Times.Once());
        }
        [Fact]
        public void TestAddOrderFailure()
        {
            var Orders = fixture.PrepareOrdersForTest();
            var Order = Orders[0];
            Mock<IOrderRepository> _OrderRepoMock = new Mock<IOrderRepository>();

            _OrderRepoMock.Setup(m => m.AddOrder(It.IsAny<Order>())).Callback<Order>(u => Orders.Add(u)).Verifiable();
            _OrderRepoMock.Setup(m => m.GetOrder(Order.Id)).Returns(Order);

            var OrderService = new OrderService(_OrderRepoMock.Object);

            Assert.Throws<OrderExistException>(() => OrderService.AddOrder(Order));
            _OrderRepoMock.Verify(m => m.GetOrder(Order.Id), Times.Once());
        }

        [Fact]
        public void TestDeleteOrderSuccess()
        {
            var Orders = fixture.PrepareOrdersForTest();
            var Order = Orders[0];
            Mock<IOrderRepository> _OrderRepoMock = new Mock<IOrderRepository>();
            _OrderRepoMock.Setup(m => m.GetOrder(Order.Id)).Returns(Orders.FirstOrDefault(u => u.Id == Order.Id));
            _OrderRepoMock.Setup(m => m.DelOrder(Order)).Callback(() => Orders.Remove(Order));
            var OrderService = new OrderService(_OrderRepoMock.Object);

            OrderService.DelOrder(Order.Id);

            Assert.Equal(9, Orders.Count);
            _OrderRepoMock.Verify(m => m.DelOrder(Order), Times.Once());
        }
        [Fact]
        public void TestDeleteOrderFailure()
        {
            var Orders = fixture.PrepareOrdersForTest();
            var Order = OrderOM.CreateOrder().WithId(11).BuildCoreModel();
            Mock<IOrderRepository> _OrderRepoMock = new Mock<IOrderRepository>();
            _OrderRepoMock.Setup(m => m.GetOrder(Order.Id)).Returns(Orders.FirstOrDefault(u => u.Id == Order.Id));
            _OrderRepoMock.Setup(m => m.DelOrder(Order)).Callback(() => Orders.Remove(Order));
            var OrderService = new OrderService(_OrderRepoMock.Object);


            Assert.Throws<OrderNotFoundException>(() => OrderService.DelOrder(Order.Id));
            _OrderRepoMock.Verify(m => m.GetOrder(Order.Id), Times.Once());
            _OrderRepoMock.Verify(m => m.DelOrder(Order), Times.Never());
        }

        [Fact]
        public void TestUpdateOrderSuccess()
        {
            var Orders = fixture.PrepareOrdersForTest();
            var Order = Orders[0];
            Mock<IOrderRepository> _OrderRepoMock = new Mock<IOrderRepository>();
            Order.Status = Status.Delivering;
            _OrderRepoMock.Setup(m => m.UpdateOrder(It.IsAny<Order>()))
                    .Callback((Order Order) =>
                    {
                        Orders.Remove(item: Orders.Find(e => e.Id == Order.Id)!);
                        Orders.Add(Order);
                    }).Verifiable();

            _OrderRepoMock.Setup(m => m.GetOrder(Order.Id)).Returns(Order);
            var OrderService = new OrderService(_OrderRepoMock.Object);

            OrderService.UpdateOrder(Order);
            var actual = OrderService.GetOrderById(Order.Id);
            Assert.Equivalent(Order.Status, actual.Status);
        }
        [Fact]
        public void TestUpdateOrderFailure()
        {
            var Orders = fixture.PrepareOrdersForTest();
            var Order = OrderOM.CreateOrder().WithId(11).WithStatus(Status.Delivered).BuildCoreModel();
            Mock<IOrderRepository> _OrderRepoMock = new Mock<IOrderRepository>();
            Order.Status = Status.Delivering;
            _OrderRepoMock.Setup(m => m.UpdateOrder(It.IsAny<Order>()))
                    .Callback((Order Order) =>
                    {
                        Orders.Remove(item: Orders.Find(e => e.Id == Order.Id)!);
                        Orders.Add(Order);
                    }).Verifiable();

            _OrderRepoMock.Setup(m => m.GetOrder(Order.Id)).Returns(Orders.Find(p => p.Id == Order.Id));
            var OrderService = new OrderService(_OrderRepoMock.Object);

            Assert.Throws<OrderNotFoundException>(() => OrderService.UpdateOrder(Order));
        }

    }
}
