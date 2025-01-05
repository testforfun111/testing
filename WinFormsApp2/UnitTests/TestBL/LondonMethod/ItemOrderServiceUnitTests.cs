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
    [AllureSuite("ItemOrderServices Unit tests")]
    [AllureSubSuite("ItemOrderService unit tests London Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class ItemOrderServiceUnitTests
    {
        private ServiceFixture fixture = new ServiceFixture();
        private ItemOrderObjectMother ItemOrderOM = new ItemOrderObjectMother();
        [AllureBefore]
       
        [Fact]
        public void TestGetItemOrderByIdSuccess()
        {
            var ItemOrders = fixture.PrepareItemOrdersForTest();
            var ItemOrder = ItemOrders[0];
            Mock<IItemOrderRepository> _ItemOrderRepoMock = new Mock<IItemOrderRepository>();
            _ItemOrderRepoMock.Setup(m => m.GetItemOrder(ItemOrder.Id)).Returns(ItemOrders.FirstOrDefault(u => u.Id == ItemOrder.Id));
            var ItemOrderService = new ItemOrderService(_ItemOrderRepoMock.Object);

            var actual = ItemOrderService.GetItemOrderById(ItemOrder.Id);

            Assert.Equal(ItemOrder, actual);
            _ItemOrderRepoMock.Verify(m => m.GetItemOrder(ItemOrder.Id), Times.Once());
        }

        [Fact]
        public void TestGetItemOrderByIdFailure()
        {
            var ItemOrders = fixture.PrepareItemOrdersForTest();
            var ItemOrder = ItemOrderOM.CreateItemOrder().WithId(11).BuildCoreModel();
            Mock<IItemOrderRepository> _ItemOrderRepoMock = new Mock<IItemOrderRepository>();

            _ItemOrderRepoMock.Setup(m => m.GetItemOrder(ItemOrder.Id)).Returns(ItemOrders.FirstOrDefault(u => u.Id == ItemOrder.Id));
            var ItemOrderService = new ItemOrderService(_ItemOrderRepoMock.Object);

            Assert.Throws<ItemOrderNotFoundException>(() => ItemOrderService.GetItemOrderById(ItemOrder.Id));
            _ItemOrderRepoMock.Verify(m => m.GetItemOrder(ItemOrder.Id), Times.Once());
        }

        [Fact]
        public void TestAddItemOrderSuccess()
        {
            var ItemOrders = fixture.PrepareItemOrdersForTest();
            var ItemOrder = ItemOrderOM.CreateItemOrder().WithId(11).BuildCoreModel();
            Mock<IItemOrderRepository> _ItemOrderRepoMock = new Mock<IItemOrderRepository>();

            _ItemOrderRepoMock.Setup(m => m.AddItemOrder(It.IsAny<ItemOrder>())).Callback<ItemOrder>(u => ItemOrders.Add(u)).Verifiable();
            _ItemOrderRepoMock.Setup(m => m.GetItemOrder(ItemOrder.Id)).Returns(ItemOrders.FirstOrDefault(u => u.Id == ItemOrder.Id));
            //_ItemOrderRepoMock.Setup(m => m.GetItemOrder(ItemOrder.Id)).Returns(ItemOrder);
            _ItemOrderRepoMock.Setup(m => m.IsExistItemOrder(ItemOrder)).Returns(ItemOrders.Contains(ItemOrder));
            var ItemOrderService = new ItemOrderService(_ItemOrderRepoMock.Object);

            ItemOrderService.AddItemOrder(ItemOrder);
            Assert.Equivalent(ItemOrders.Count(), 11);
            _ItemOrderRepoMock.Verify(m => m.GetItemOrder(ItemOrder.Id), Times.Once());
        }
        [Fact]
        public void TestAddItemOrderFailure()
        {
            var ItemOrders = fixture.PrepareItemOrdersForTest();
            var ItemOrder = ItemOrders[0];
            Mock<IItemOrderRepository> _ItemOrderRepoMock = new Mock<IItemOrderRepository>();
            _ItemOrderRepoMock.Setup(m => m.IsExistItemOrder(ItemOrder)).Returns(ItemOrders.Contains(ItemOrder));
            _ItemOrderRepoMock.Setup(m => m.GetItemOrder(ItemOrder.Id)).Returns(ItemOrders.FirstOrDefault(u => u.Id == ItemOrder.Id));
            var ItemOrderService = new ItemOrderService(_ItemOrderRepoMock.Object);

            Assert.Throws<ItemOrderExistException>(() => ItemOrderService.AddItemOrder(ItemOrder));
            _ItemOrderRepoMock.Verify(m => m.GetItemOrder(ItemOrder.Id), Times.Once());
        }

        [Fact]
        public void TestDeleteItemOrderSuccess()
        {
            var ItemOrders = fixture.PrepareItemOrdersForTest();
            var ItemOrder = ItemOrders[0];
            Mock<IItemOrderRepository> _ItemOrderRepoMock = new Mock<IItemOrderRepository>();
            _ItemOrderRepoMock.Setup(m => m.GetItemOrder(ItemOrder.Id)).Returns(ItemOrders.FirstOrDefault(u => u.Id == ItemOrder.Id));
            _ItemOrderRepoMock.Setup(m => m.DelItemOrder(ItemOrder)).Callback(() => ItemOrders.Remove(ItemOrder));
            var ItemOrderService = new ItemOrderService(_ItemOrderRepoMock.Object);

            ItemOrderService.DelItemOrder(ItemOrder.Id);

            Assert.Equal(9, ItemOrders.Count);
            _ItemOrderRepoMock.Verify(m => m.DelItemOrder(ItemOrder), Times.Once());
        }
        [Fact]
        public void TestDeleteItemOrderFailure()
        {
            var ItemOrders = fixture.PrepareItemOrdersForTest();
            var ItemOrder = ItemOrderOM.CreateItemOrder().WithId(11).BuildCoreModel();
            Mock<IItemOrderRepository> _ItemOrderRepoMock = new Mock<IItemOrderRepository>();
            _ItemOrderRepoMock.Setup(m => m.GetItemOrder(ItemOrder.Id)).Returns(ItemOrders.FirstOrDefault(u => u.Id == ItemOrder.Id));
            _ItemOrderRepoMock.Setup(m => m.DelItemOrder(ItemOrder)).Callback(() => ItemOrders.Remove(ItemOrder));
            var ItemOrderService = new ItemOrderService(_ItemOrderRepoMock.Object);


            Assert.Throws<ItemOrderNotFoundException>(() => ItemOrderService.DelItemOrder(ItemOrder.Id));
            _ItemOrderRepoMock.Verify(m => m.GetItemOrder(ItemOrder.Id), Times.Once());
            _ItemOrderRepoMock.Verify(m => m.DelItemOrder(ItemOrder), Times.Never());
        }
        [Fact]
        public void TestUpdateItemOrderSuccess()
        {
            var ItemOrders = fixture.PrepareItemOrdersForTest();
            var ItemOrder = ItemOrders[0];
            Mock<IItemOrderRepository> _ItemOrderRepoMock = new Mock<IItemOrderRepository>();
            ItemOrder.Quantity = 1;
            _ItemOrderRepoMock.Setup(m => m.UpdateItemOrder(It.IsAny<ItemOrder>()))
                    .Callback((ItemOrder ItemOrder) =>
                    {
                        ItemOrders.Remove(item: ItemOrders.Find(e => e.Id == ItemOrder.Id)!);
                        ItemOrders.Add(ItemOrder);
                    }).Verifiable();
            _ItemOrderRepoMock.Setup(m => m.GetItemOrder(ItemOrder.Id)).Returns(ItemOrders.Find(u => u.Id == ItemOrder.Id));
            _ItemOrderRepoMock.Setup(m => m.IsExistItemOrder(ItemOrder)).Returns(ItemOrders.Contains(ItemOrder));
            var ItemOrderService = new ItemOrderService(_ItemOrderRepoMock.Object);

            ItemOrderService.UpdateItemOrder(ItemOrder);
            var actual = ItemOrderService.GetItemOrderById(ItemOrder.Id);
            Assert.Equivalent(ItemOrder.Quantity, actual.Quantity);
        }
        [Fact]
        public void TestUpdateItemOrderFailure()
        {
            var ItemOrders = fixture.PrepareItemOrdersForTest();
            var ItemOrder = ItemOrderOM.CreateItemOrder().WithId(11).BuildCoreModel();
            Mock<IItemOrderRepository> _ItemOrderRepoMock = new Mock<IItemOrderRepository>();
            ItemOrder.Quantity = 2;
            _ItemOrderRepoMock.Setup(m => m.UpdateItemOrder(It.IsAny<ItemOrder>()))
                    .Callback((ItemOrder ItemOrder) =>
                    {
                        ItemOrders.Remove(item: ItemOrders.Find(e => e.Id == ItemOrder.Id)!);
                        ItemOrders.Add(ItemOrder);
                    }).Verifiable();

            _ItemOrderRepoMock.Setup(m => m.GetItemOrder(ItemOrder.Id)).Returns(ItemOrders.Find(p => p.Id == ItemOrder.Id));
            var ItemOrderService = new ItemOrderService(_ItemOrderRepoMock.Object);

            Assert.Throws<ItemOrderNotFoundException>(() => ItemOrderService.UpdateItemOrder(ItemOrder));
        }
    }
}
