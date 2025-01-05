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
    [AllureSuite("ItemCartServices Unit tests")]
    [AllureSubSuite("ItemCartService unit tests Destroit Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class ItemCartServiceUnitTests
    {
        private DBFixture fixture = new DBFixture();
        private ItemCartObjectMother ItemCartOM = new ItemCartObjectMother();
        [AllureBefore]
        public ItemCartServiceUnitTests() { }
   
        [Fact]
        public void TestGetItemCartByIdSuccessDestroitMethod()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCarts.First();
            IItemCartRepository _ItemCartRepo = new ItemCartRepository(fixture._dbContextFactory.get_db_context());
            var ItemCartService = new ItemCartService(_ItemCartRepo);

            var actual = ItemCartService.GetItemCartById(ItemCart.Id);

            Assert.Equivalent(ItemCart, actual);
        }

        [Fact]
        public void TestGetItemCartByIdFailureDestroitMethod()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCarts.First();
            IItemCartRepository _ItemCartRepo = new ItemCartRepository(fixture._dbContextFactory.get_db_context());
            var ItemCartService = new ItemCartService(_ItemCartRepo);

            Assert.Throws<ItemCartNotFoundException>(() => ItemCartService.GetItemCartById(1000));
        }

        [Fact]
        public void TestAddItemCartSuccessDestroitMethod()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCartOM.CreateItemCart().WithId(11).WithIdCart(13).WithIdProduct(14).WithQuantity(5).BuildCoreModel();
            IItemCartRepository _ItemCartRepo = new ItemCartRepository(fixture._dbContextFactory.get_db_context());
            var ItemCartService = new ItemCartService(_ItemCartRepo);

            ItemCartService.AddItemCart(ItemCart);
            var actual = ItemCartService.GetItemCartById(ItemCart.Id);
            Assert.Equivalent(ItemCart, actual);
        }
        [Fact]
        public void TestAddItemCartFailureDestroitMethod()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCarts.First();
            IItemCartRepository _ItemCartRepo = new ItemCartRepository(fixture._dbContextFactory.get_db_context());
            var ItemCartService = new ItemCartService(_ItemCartRepo);
            ItemCartService.AddItemCart(ItemCart);
            var actual = ItemCartService.GetItemCartById(ItemCart.Id);
            Assert.Equivalent(ItemCart.Quantity, actual.Quantity);
        }

        [Fact]
        public void TestDeleteItemCartSuccessDestroitMethod()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCarts.First();
            IItemCartRepository _ItemCartRepo = new ItemCartRepository(fixture._dbContextFactory.get_db_context());
            var ItemCartService = new ItemCartService(_ItemCartRepo);

            ItemCartService.DelItemCart(ItemCart.Id);
            Assert.Throws<ItemCartNotFoundException>(() => ItemCartService.GetItemCartById(ItemCart.Id));
        }
        [Fact]
        public void TestDeleteItemCartFailureDestroitMethod()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            IItemCartRepository _ItemCartRepo = new ItemCartRepository(fixture._dbContextFactory.get_db_context());
            var ItemCartService = new ItemCartService(_ItemCartRepo);
            
            Assert.Throws<ItemCartNotFoundException>(() => ItemCartService.DelItemCart(100));
        }
        
        public void TestUpdateItemCartSuccessDestroitMethod()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCarts.First();
            ItemCart.Quantity = 3;
            IItemCartRepository _ItemCartRepo = new ItemCartRepository(fixture._dbContextFactory.get_db_context());
            var ItemCartService = new ItemCartService(_ItemCartRepo);

            ItemCartService.UpdateItemCart(ItemCart);
            var actual = ItemCartService.GetItemCartById(ItemCart.Id);
            Assert.Equivalent(ItemCart.Quantity, 3);
        }
        [Fact]
        public void TestUpdateItemCartFailureDestroitMethod()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCartOM.CreateItemCart().WithId(100).BuildCoreModel();
            IItemCartRepository _ItemCartRepo = new ItemCartRepository(fixture._dbContextFactory.get_db_context());
            var ItemCartService = new ItemCartService(_ItemCartRepo);
            
            Assert.Throws<ItemCartNotFoundException>(() => ItemCartService.UpdateItemCart(ItemCart));
        }
    }
}
