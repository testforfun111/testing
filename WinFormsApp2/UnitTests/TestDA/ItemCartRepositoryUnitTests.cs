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
    [AllureSubSuite("ItemCartRepositoty Unit tests")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class ItemCartRepositoryUnitTests
    {
        private IItemCartRepository _ItemCartRepository;
        private DBFixture _dbFixture;
        private ItemCartObjectMother _ItemCartObjectMother;
        public ItemCartRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _ItemCartRepository = new ItemCartRepository(_dbFixture._dbContextFactory.get_db_context());
            _ItemCartObjectMother = new ItemCartObjectMother();
        }
        [Fact]
        public void TestGetItemCartById()
        {
            var ItemCarts = _dbFixture.AddItemCarts(10);
            var expected = ItemCarts[0];

            var actual = _ItemCartRepository.GetItemCart(expected.Id);

            Assert.Equivalent(expected, actual);
        }
        
        [Fact]
        public void TestDeleteItemCart()
        {
            var ItemCarts = _dbFixture.AddItemCarts(10);
            var ItemCart = ItemCarts.First();

            _ItemCartRepository.DelItemCart(ItemCart);

            var actual = _ItemCartRepository.GetItemCart(ItemCart.Id);
            Assert.Null(actual);
        }
        [Fact]
        public void TestUpdateItemCart()
        {
            var ItemCarts = _dbFixture.AddItemCarts(10);
            var ItemCart = _ItemCartObjectMother.CreateItemCart().WithId(1).BuildCoreModel();

            _ItemCartRepository.UpdateItemCart(ItemCart);

            var actual = _ItemCartRepository.GetItemCart(ItemCart.Id);
            Assert.Equivalent(ItemCart, actual);
        }
        [Fact]
        public void TestAddItemCart()
        {
            var ItemCarts = _dbFixture.AddItemCarts(10);
            var ItemCart = _ItemCartObjectMother.CreateItemCart().BuildCoreModel();

            _ItemCartRepository.AddItemCart(ItemCart);

            var actual = _ItemCartRepository.GetItemCart(11);
            Assert.Equivalent(ItemCart, actual);
        }
        [Fact]
        public void TestGetAllItemCartByIdCart()
        {
            var ItemCarts = _dbFixture.AddItemCarts(10);
            List<ItemCart> temp = new List<ItemCart>();

            foreach( var o in ItemCarts)
            {
                if (o.Id_cart == 1) 
                    temp.Add(o);
            }
            var actual = _ItemCartRepository.GetAllItemCartByIdCart(1);

            Assert.Equivalent(temp, actual);
        }


    }
}
