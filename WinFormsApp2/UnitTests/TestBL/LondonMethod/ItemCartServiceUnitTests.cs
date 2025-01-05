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
    [AllureSuite("ItemCartServices Unit tests")]
    [AllureSubSuite("ItemCartService unit tests London Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class ItemCartServiceUnitTests
    {
        private ServiceFixture fixture = new ServiceFixture();
        private ItemCartObjectMother ItemCartOM = new ItemCartObjectMother();
        [AllureBefore]
       
        [Fact]
        public void TestGetItemCartByIdSuccess()
        {
            var ItemCarts = fixture.PrepareItemCartsForTest();
            var ItemCart = ItemCarts[0];
            Mock<IItemCartRepository> _ItemCartRepoMock = new Mock<IItemCartRepository>();
            _ItemCartRepoMock.Setup(m => m.GetItemCart(ItemCart.Id)).Returns(ItemCarts.FirstOrDefault(u => u.Id == ItemCart.Id));
            var ItemCartService = new ItemCartService(_ItemCartRepoMock.Object);

            var actual = ItemCartService.GetItemCartById(ItemCart.Id);

            Assert.Equal(ItemCart, actual);
            _ItemCartRepoMock.Verify(m => m.GetItemCart(ItemCart.Id), Times.Once());
        }

        [Fact]
        public void TestGetItemCartByIdFailure()
        {
            var ItemCarts = fixture.PrepareItemCartsForTest();
            var ItemCart = ItemCartOM.CreateItemCart().WithId(11).BuildCoreModel();
            Mock<IItemCartRepository> _ItemCartRepoMock = new Mock<IItemCartRepository>();

            _ItemCartRepoMock.Setup(m => m.GetItemCart(ItemCart.Id)).Returns(ItemCarts.FirstOrDefault(u => u.Id == ItemCart.Id));
            var ItemCartService = new ItemCartService(_ItemCartRepoMock.Object);

            Assert.Throws<ItemCartNotFoundException>(() => ItemCartService.GetItemCartById(ItemCart.Id));
            _ItemCartRepoMock.Verify(m => m.GetItemCart(ItemCart.Id), Times.Once());
        }

        [Fact]
        public void TestAddItemCartSuccess()
        {
            var ItemCarts = fixture.PrepareItemCartsForTest();
            var ItemCart = ItemCartOM.CreateItemCart().WithId(11).BuildCoreModel();
            Mock<IItemCartRepository> _ItemCartRepoMock = new Mock<IItemCartRepository>();

            _ItemCartRepoMock.Setup(m => m.AddItemCart(It.IsAny<ItemCart>())).Callback<ItemCart>(u => ItemCarts.Add(u)).Verifiable();
            _ItemCartRepoMock.Setup(m => m.GetItemCart(ItemCart.Id)).Returns(ItemCart);
            _ItemCartRepoMock.Setup(m => m.IsExistItemCart(ItemCart)).Returns(ItemCarts.Contains(ItemCart));
            var ItemCartService = new ItemCartService(_ItemCartRepoMock.Object);

            ItemCartService.AddItemCart(ItemCart);
            var actual = ItemCartService.GetItemCartById(11);
            Assert.Equivalent(ItemCart, actual);
            _ItemCartRepoMock.Verify(m => m.GetItemCart(ItemCart.Id), Times.Once());
        }
        [Fact]
        public void TestAddItemCartFailure()
        {
            var ItemCarts = fixture.PrepareItemCartsForTest();
            var ItemCart = ItemCarts[0];
            Mock<IItemCartRepository> _ItemCartRepoMock = new Mock<IItemCartRepository>();

            _ItemCartRepoMock.Setup(m => m.AddItemCart(It.IsAny<ItemCart>())).Callback<ItemCart>(u => ItemCarts.Add(u)).Verifiable();
            _ItemCartRepoMock.Setup(m => m.GetItemCart(ItemCart.Id)).Returns(ItemCarts.FirstOrDefault(u => u.Id == ItemCart.Id));
            _ItemCartRepoMock.Setup(m => m.IsExistItemCart(ItemCart)).Returns(ItemCarts.Contains(ItemCart));
            var ItemCartService = new ItemCartService(_ItemCartRepoMock.Object);

            ItemCartService.AddItemCart(ItemCart);
            var actual = ItemCartService.GetItemCartById(1);
            Assert.Equivalent(ItemCart.Quantity, actual.Quantity);
            _ItemCartRepoMock.Verify(m => m.GetItemCart(ItemCart.Id), Times.Once());
        }

        [Fact]
        public void TestDeleteItemCartSuccess()
        {
            var ItemCarts = fixture.PrepareItemCartsForTest();
            var ItemCart = ItemCarts[0];
            Mock<IItemCartRepository> _ItemCartRepoMock = new Mock<IItemCartRepository>();
            _ItemCartRepoMock.Setup(m => m.GetItemCart(ItemCart.Id)).Returns(ItemCarts.FirstOrDefault(u => u.Id == ItemCart.Id));
            _ItemCartRepoMock.Setup(m => m.DelItemCart(ItemCart)).Callback(() => ItemCarts.Remove(ItemCart));
            var ItemCartService = new ItemCartService(_ItemCartRepoMock.Object);

            ItemCartService.DelItemCart(ItemCart.Id);

            Assert.Equal(9, ItemCarts.Count);
            _ItemCartRepoMock.Verify(m => m.DelItemCart(ItemCart), Times.Once());
        }
        [Fact]
        public void TestDeleteItemCartFailure()
        {
            var ItemCarts = fixture.PrepareItemCartsForTest();
            var ItemCart = ItemCartOM.CreateItemCart().WithId(11).BuildCoreModel();
            Mock<IItemCartRepository> _ItemCartRepoMock = new Mock<IItemCartRepository>();
            _ItemCartRepoMock.Setup(m => m.GetItemCart(ItemCart.Id)).Returns(ItemCarts.FirstOrDefault(u => u.Id == ItemCart.Id));
            _ItemCartRepoMock.Setup(m => m.DelItemCart(ItemCart)).Callback(() => ItemCarts.Remove(ItemCart));
            var ItemCartService = new ItemCartService(_ItemCartRepoMock.Object);


            Assert.Throws<ItemCartNotFoundException>(() => ItemCartService.DelItemCart(ItemCart.Id));
            _ItemCartRepoMock.Verify(m => m.GetItemCart(ItemCart.Id), Times.Once());
            _ItemCartRepoMock.Verify(m => m.DelItemCart(ItemCart), Times.Never());
        }
        [Fact]
        public void TestUpdateItemCartSuccess()
        {
            var ItemCarts = fixture.PrepareItemCartsForTest();
            var ItemCart = ItemCarts[0];
            Mock<IItemCartRepository> _ItemCartRepoMock = new Mock<IItemCartRepository>();
            ItemCart.Quantity = 1;
            _ItemCartRepoMock.Setup(m => m.UpdateItemCart(It.IsAny<ItemCart>()))
                    .Callback((ItemCart ItemCart) =>
                    {
                        ItemCarts.Remove(item: ItemCarts.Find(e => e.Id == ItemCart.Id)!);
                        ItemCarts.Add(ItemCart);
                    }).Verifiable();
            _ItemCartRepoMock.Setup(m => m.GetItemCart(ItemCart.Id)).Returns(ItemCarts.Find(u => u.Id == ItemCart.Id));
            _ItemCartRepoMock.Setup(m => m.IsExistItemCart(ItemCart)).Returns(ItemCarts.Contains(ItemCart));
            var ItemCartService = new ItemCartService(_ItemCartRepoMock.Object);

            ItemCartService.UpdateItemCart(ItemCart);
            var actual = ItemCartService.GetItemCartById(ItemCart.Id);
            Assert.Equivalent(ItemCart.Quantity, actual.Quantity);
        }
        [Fact]
        public void TestUpdateItemCartFailure()
        {
            var ItemCarts = fixture.PrepareItemCartsForTest();
            var ItemCart = ItemCartOM.CreateItemCart().WithId(11).BuildCoreModel();
            Mock<IItemCartRepository> _ItemCartRepoMock = new Mock<IItemCartRepository>();
            ItemCart.Quantity = 2;
            _ItemCartRepoMock.Setup(m => m.UpdateItemCart(It.IsAny<ItemCart>()))
                    .Callback((ItemCart ItemCart) =>
                    {
                        ItemCarts.Remove(item: ItemCarts.Find(e => e.Id == ItemCart.Id)!);
                        ItemCarts.Add(ItemCart);
                    }).Verifiable();

            _ItemCartRepoMock.Setup(m => m.GetItemCart(ItemCart.Id)).Returns(ItemCarts.Find(p => p.Id == ItemCart.Id));
            var ItemCartService = new ItemCartService(_ItemCartRepoMock.Object);

            Assert.Throws<ItemCartNotFoundException>(() => ItemCartService.UpdateItemCart(ItemCart));
        }
    }
}
