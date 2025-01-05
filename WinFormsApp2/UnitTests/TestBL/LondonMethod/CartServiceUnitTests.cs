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
    [AllureSuite("CartServices Unit tests")]
    [AllureSubSuite("CartService unit tests London Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class CartServiceUnitTests
    {
        private ServiceFixture fixture = new ServiceFixture();
        private CartObjectMother CartOM = new CartObjectMother();
        [AllureBefore]
       
        [Fact]
        public void TestGetCartByIdSuccess()
        {
            var Carts = fixture.PrepareCartsForTest();
            var Cart = Carts[0];
            Mock<ICartRepository> _CartRepoMock = new Mock<ICartRepository>();
            _CartRepoMock.Setup(m => m.GetCart(Cart.Id)).Returns(Carts.FirstOrDefault(u => u.Id == Cart.Id));
            var CartService = new CartService(_CartRepoMock.Object);

            var actual = CartService.GetCartById(Cart.Id);

            Assert.Equal(Cart, actual);
            _CartRepoMock.Verify(m => m.GetCart(Cart.Id), Times.Once());
        }

        [Fact]
        public void TestGetCartByIdFailure()
        {
            var Carts = fixture.PrepareCartsForTest();
            var Cart = CartOM.CreateCart().WithId(11).BuildCoreModel();
            Mock<ICartRepository> _CartRepoMock = new Mock<ICartRepository>();

            _CartRepoMock.Setup(m => m.GetCart(Cart.Id)).Returns(Carts.FirstOrDefault(u => u.Id == Cart.Id));
            var CartService = new CartService(_CartRepoMock.Object);

            Assert.Throws<CartNotFoundException>(() => CartService.GetCartById(Cart.Id));
            _CartRepoMock.Verify(m => m.GetCart(Cart.Id), Times.Once());
        }

        [Fact]
        public void TestAddCartSuccess()
        {
            var Carts = fixture.PrepareCartsForTest();
            var Cart = CartOM.CreateCart().WithId(11).BuildCoreModel();
            Mock<ICartRepository> _CartRepoMock = new Mock<ICartRepository>();

            _CartRepoMock.Setup(m => m.AddCart(It.IsAny<Cart>())).Callback((Cart cart) => Carts.Add(cart)).Verifiable();
            _CartRepoMock.Setup(m => m.GetCart(Cart.Id)).Returns(Cart);
            _CartRepoMock.Setup(m => m.IsExistCart(Cart)).Returns(Carts.Contains(Cart));

            var CartService = new CartService(_CartRepoMock.Object);

            CartService.AddCart(Cart);
            var actual = CartService.GetCartById(11);
            Assert.Equivalent(Cart, actual);
            _CartRepoMock.Verify(m => m.GetCart(Cart.Id), Times.Once());
        }
        [Fact]
        public void TestAddCartFailure()
        {
            var Carts = fixture.PrepareCartsForTest();
            var Cart = Carts[0];
            Mock<ICartRepository> _CartRepoMock = new Mock<ICartRepository>();
            _CartRepoMock.Setup(m => m.IsExistCart(Cart)).Returns(Carts.Contains(Cart));
            _CartRepoMock.Setup(m => m.AddCart(It.IsAny<Cart>())).Callback((Cart cart) => Carts.Add(cart)).Verifiable();
            _CartRepoMock.Setup(m => m.GetCart(Cart.Id)).Returns(Carts.FirstOrDefault(u => u.Id == Cart.Id));
            var CartService = new CartService(_CartRepoMock.Object);

            Assert.Throws<CartExistException>(() => CartService.AddCart(Cart));
        }

        [Fact]
        public void TestDeleteCartSuccess()
        {
            var Carts = fixture.PrepareCartsForTest();
            var Cart = Carts[0];
            Mock<ICartRepository> _CartRepoMock = new Mock<ICartRepository>();
            _CartRepoMock.Setup(m => m.GetCart(Cart.Id)).Returns(Carts.FirstOrDefault(u => u.Id == Cart.Id));
            _CartRepoMock.Setup(m => m.DelCart(Cart)).Callback(() => Carts.Remove(Cart));
            var CartService = new CartService(_CartRepoMock.Object);

            CartService.DelCart(Cart.Id);

            Assert.Equal(9, Carts.Count);
            _CartRepoMock.Verify(m => m.DelCart(Cart), Times.Once());
        }
        [Fact]
        public void TestDeleteCartFailure()
        {
            var Carts = fixture.PrepareCartsForTest();
            var Cart = CartOM.CreateCart().WithId(11).BuildCoreModel();
            Mock<ICartRepository> _CartRepoMock = new Mock<ICartRepository>();
            _CartRepoMock.Setup(m => m.GetCart(Cart.Id)).Returns(Carts.FirstOrDefault(u => u.Id == Cart.Id));
            _CartRepoMock.Setup(m => m.DelCart(Cart)).Callback(() => Carts.Remove(Cart));
            var CartService = new CartService(_CartRepoMock.Object);


            Assert.Throws<CartNotFoundException>(() => CartService.DelCart(Cart.Id));
            _CartRepoMock.Verify(m => m.GetCart(Cart.Id), Times.Once());
            _CartRepoMock.Verify(m => m.DelCart(Cart), Times.Never());
        }


        [Fact]
        public void TestUpdateCartSuccess()
        {
            var Carts = fixture.PrepareCartsForTest();
            var Cart = Carts[0];
            Mock<ICartRepository> _CartRepoMock = new Mock<ICartRepository>();
            Cart.Id_user = 1;
            _CartRepoMock.Setup(m => m.UpdateCart(It.IsAny<Cart>()))
                    .Callback((Cart Cart) =>
                    {
                        Carts.Remove(item: Carts.Find(e => e.Id == Cart.Id)!);
                        Carts.Add(Cart);
                    }).Verifiable();
            _CartRepoMock.Setup(m => m.GetCart(Cart.Id)).Returns(Carts.Find(u => u.Id == Cart.Id));
            _CartRepoMock.Setup(m => m.IsExistCart(Cart)).Returns(Carts.Contains(Cart));
            var CartService = new CartService(_CartRepoMock.Object);

            CartService.UpdateCart(Cart);
            var actual = CartService.GetCartById(Cart.Id);
            Assert.Equivalent(Cart.Id_user, actual.Id_user);
        }
        [Fact]
        public void TestUpdateCartFailure()
        {
            var Carts = fixture.PrepareCartsForTest();
            var Cart = CartOM.CreateCart().WithId(11).BuildCoreModel();
            Mock<ICartRepository> _CartRepoMock = new Mock<ICartRepository>();
            Cart.Id_user = 2;
            _CartRepoMock.Setup(m => m.UpdateCart(It.IsAny<Cart>()))
                    .Callback((Cart Cart) =>
                    {
                        Carts.Remove(item: Carts.Find(e => e.Id == Cart.Id)!);
                        Carts.Add(Cart);
                    }).Verifiable();

            _CartRepoMock.Setup(m => m.GetCart(Cart.Id)).Returns(Carts.Find(p => p.Id == Cart.Id));
            var CartService = new CartService(_CartRepoMock.Object);

            Assert.Throws<CartNotFoundException>(() => CartService.UpdateCart(Cart));
        }
    }
}
