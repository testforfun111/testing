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
    [AllureSubSuite("ItemOrderRepositoty Unit tests")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class ItemOrderRepositoryUnitTests
    {
        private IItemOrderRepository _ItemOrderRepository;
        private DBFixture _dbFixture;
        private ItemOrderObjectMother _ItemOrderObjectMother;
        public ItemOrderRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _ItemOrderRepository = new ItemOrderRepository(_dbFixture._dbContextFactory.get_db_context());
            _ItemOrderObjectMother = new ItemOrderObjectMother();
        }
        [Fact]
        public void TestGetItemOrderById()
        {
            var ItemOrders = _dbFixture.AddItemOrders(10);
            var expected = ItemOrders[0];

            var actual = _ItemOrderRepository.GetItemOrder(expected.Id);

            Assert.Equivalent(expected, actual);
        }
        
        [Fact]
        public void TestDeleteItemOrder()
        {
            var ItemOrders = _dbFixture.AddItemOrders(10);
            var ItemOrder = ItemOrders.First();

            _ItemOrderRepository.DelItemOrder(ItemOrder);

            var actual = _ItemOrderRepository.GetItemOrder(ItemOrder.Id);
            Assert.Null(actual);
        }
        [Fact]
        public void TestUpdateItemOrder()
        {
            var ItemOrders = _dbFixture.AddItemOrders(10);
            var ItemOrder = _ItemOrderObjectMother.CreateItemOrder().WithId(1).BuildCoreModel();

            _ItemOrderRepository.UpdateItemOrder(ItemOrder);

            var actual = _ItemOrderRepository.GetItemOrder(ItemOrder.Id);
            Assert.Equivalent(ItemOrder, actual);
        }
        [Fact]
        public void TestAddItemOrder()
        {
            var ItemOrders = _dbFixture.AddItemOrders(10);
            var ItemOrder = _ItemOrderObjectMother.CreateItemOrder().BuildCoreModel();

            _ItemOrderRepository.AddItemOrder(ItemOrder);

            var actual = _ItemOrderRepository.GetItemOrder(11);
            Assert.Equivalent(ItemOrder, actual);
        }
        [Fact]
        public void TestIsExistItemOrder()
        {
            var ItemOrders = _dbFixture.AddItemOrders(10);
            
            var actual = _ItemOrderRepository.IsExistItemOrder(ItemOrders[0]);

            Assert.Equivalent(true, actual);
        }
    }
}
