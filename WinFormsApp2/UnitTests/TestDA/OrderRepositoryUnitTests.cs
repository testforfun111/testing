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

namespace UnitTests.UnitTests.TestDA
{
    [AllureOwner("VHD")]
    [AllureSuite("DA Unit Tests")]
    [AllureSubSuite("OrderRepositoty Unit tests")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class OrderRepositoryUnitTests
    {
        private IOrderRepository _OrderRepository;
        private DBFixture _dbFixture;
        private OrderObjectMother _OrderObjectMother;
        public OrderRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _OrderRepository = new OrderRepository(_dbFixture._dbContextFactory.get_db_context());
            _OrderObjectMother = new OrderObjectMother();
        }
        [Fact]
        public void TestGetOrderById()
        {
            var Orders = _dbFixture.AddOrders(10);
            var expected = Orders[0];

            var actual = _OrderRepository.GetOrder(expected.Id);

            Assert.Equivalent(expected, actual);
        }
        [Fact]
        public void TestGetOrderByIdUser()
        {
            var Orders = _dbFixture.AddOrders(10);
            ICollection<Order> temp = new List<Order>();
            foreach(var o in Orders)
            {
                if (o.Id_user == 1)
                    temp.Add(o);
            }

            var actual = _OrderRepository.GetAllOrdersByIdUser(1);

            Assert.Equivalent(temp, actual);
        }
        
        [Fact]
        public void TestDeleteOrder()
        {
            var Orders = _dbFixture.AddOrders(10);
            var Order = Orders.First();

            _OrderRepository.DelOrder(Order);

            var actual = _OrderRepository.GetOrder(Order.Id);
            Assert.Null(actual);
        }
        [Fact]
        public void TestUpdateOrder()
        {
            var Orders = _dbFixture.AddOrders(10);
            var Order = _OrderObjectMother.CreateOrder().WithId(1).WithStatus(Status.Delivered).BuildCoreModel();

            _OrderRepository.UpdateOrder(Order);

            var actual = _OrderRepository.GetOrder(Order.Id);
            Assert.Equivalent(Order, actual);
        }
        [Fact]
        public void TestAddOrder()
        {
            var Orders = _dbFixture.AddOrders(10);
            var Order = _OrderObjectMother.CreateOrder().BuildCoreModel();

            _OrderRepository.AddOrder(Order);

            var actual = _OrderRepository.GetOrder(11);
            Assert.Equivalent(Order, actual);
        }
        [Fact]
        public void TestGetAll()
        {
            var Orders = _dbFixture.AddOrders(10);

            var actual = _OrderRepository.GetAllOrders();

            Assert.Equivalent(Orders, actual);
        }
    }
}
