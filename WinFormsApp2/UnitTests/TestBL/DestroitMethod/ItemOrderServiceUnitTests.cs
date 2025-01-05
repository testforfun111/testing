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
    [AllureSuite("ItemOrderServices Unit tests")]
    [AllureSubSuite("ItemOrderService unit tests Destroit Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class ItemOrderServiceUnitTests
    {
        private DBFixture fixture = new DBFixture();
        private ItemOrderObjectMother ItemOrderOM = new ItemOrderObjectMother();
        [AllureBefore]
        public ItemOrderServiceUnitTests() { }
   
        [Fact]
        public void TestGetItemOrderByIdSuccessDestroitMethod()
        {
            var ItemOrders = fixture.AddItemOrders(10);
            var ItemOrder = ItemOrders.First();
            IItemOrderRepository _ItemOrderRepo = new ItemOrderRepository(fixture._dbContextFactory.get_db_context());
            var ItemOrderService = new ItemOrderService(_ItemOrderRepo);

            var actual = ItemOrderService.GetItemOrderById(ItemOrder.Id);

            Assert.Equivalent(ItemOrder, actual);
        }

        [Fact]
        public void TestGetItemOrderByIdFailureDestroitMethod()
        {
            var ItemOrders = fixture.AddItemOrders(10);
            var ItemOrder = ItemOrders.First();
            IItemOrderRepository _ItemOrderRepo = new ItemOrderRepository(fixture._dbContextFactory.get_db_context());
            var ItemOrderService = new ItemOrderService(_ItemOrderRepo);

            Assert.Throws<ItemOrderNotFoundException>(() => ItemOrderService.GetItemOrderById(1000));
        }

        [Fact]
        public void TestAddItemOrderSuccessDestroitMethod()
        {
            var ItemOrders = fixture.AddItemOrders(10);
            var ItemOrder = ItemOrderOM.CreateItemOrder().WithId(11).BuildCoreModel();
            IItemOrderRepository _ItemOrderRepo = new ItemOrderRepository(fixture._dbContextFactory.get_db_context());
            var ItemOrderService = new ItemOrderService(_ItemOrderRepo);

            ItemOrderService.AddItemOrder(ItemOrder);
            var actual = ItemOrderService.GetItemOrderById(ItemOrder.Id);
            Assert.Equivalent(ItemOrder, actual);
        }
        [Fact]
        public void TestAddItemOrderFailureDestroitMethod()
        {
            var ItemOrders = fixture.AddItemOrders(10);
            var ItemOrder = ItemOrders.First();
            IItemOrderRepository _ItemOrderRepo = new ItemOrderRepository(fixture._dbContextFactory.get_db_context());
            var ItemOrderService = new ItemOrderService(_ItemOrderRepo);

            Assert.Throws<ItemOrderExistException>(() => ItemOrderService.AddItemOrder(ItemOrder));
        }

        [Fact]
        public void TestDeleteItemOrderSuccessDestroitMethod()
        {
            var ItemOrders = fixture.AddItemOrders(10);
            var ItemOrder = ItemOrders.First();
            IItemOrderRepository _ItemOrderRepo = new ItemOrderRepository(fixture._dbContextFactory.get_db_context());
            var ItemOrderService = new ItemOrderService(_ItemOrderRepo);

            ItemOrderService.DelItemOrder(ItemOrder.Id);
            Assert.Throws<ItemOrderNotFoundException>(() => ItemOrderService.GetItemOrderById(ItemOrder.Id));
        }
        [Fact]
        public void TestDeleteItemOrderFailureDestroitMethod()
        {
            var ItemOrders = fixture.AddItemOrders(10);
            IItemOrderRepository _ItemOrderRepo = new ItemOrderRepository(fixture._dbContextFactory.get_db_context());
            var ItemOrderService = new ItemOrderService(_ItemOrderRepo);
            
            Assert.Throws<ItemOrderNotFoundException>(() => ItemOrderService.DelItemOrder(100));
        }
        
        public void TestUpdateItemOrderSuccessDestroitMethod()
        {
            var ItemOrders = fixture.AddItemOrders(10);
            var ItemOrder = ItemOrders.First();
            ItemOrder.Quantity = 3;
            IItemOrderRepository _ItemOrderRepo = new ItemOrderRepository(fixture._dbContextFactory.get_db_context());
            var ItemOrderService = new ItemOrderService(_ItemOrderRepo);

            ItemOrderService.UpdateItemOrder(ItemOrder);
            var actual = ItemOrderService.GetItemOrderById(ItemOrder.Id);
            Assert.Equivalent(ItemOrder.Quantity, 3);
        }
        [Fact]
        public void TestUpdateItemOrderFailureDestroitMethod()
        {
            var ItemOrders = fixture.AddItemOrders(10);
            var ItemOrder = ItemOrderOM.CreateItemOrder().WithId(100).BuildCoreModel();
            IItemOrderRepository _ItemOrderRepo = new ItemOrderRepository(fixture._dbContextFactory.get_db_context());
            var ItemOrderService = new ItemOrderService(_ItemOrderRepo);
            
            Assert.Throws<ItemOrderNotFoundException>(() => ItemOrderService.UpdateItemOrder(ItemOrder));
        }
    }
}
