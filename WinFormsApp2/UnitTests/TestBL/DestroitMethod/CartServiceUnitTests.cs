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
    [AllureSuite("CartServices Unit tests")]
    [AllureSubSuite("CartService unit tests Destroit Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]

    public class CartServiceUnitTests
    {
        private DBFixture fixture = new DBFixture();
        private CartObjectMother CartOM = new CartObjectMother();
        [AllureBefore]
        public CartServiceUnitTests() { }
   
        [Fact]
        public void TestGetCartByIdSuccessDestroitMethod()
        {
            var Carts = fixture.AddCarts(10);
            var Cart = Carts.First();
            ICartRepository _CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            var CartService = new CartService(_CartRepo);

            var actual = CartService.GetCartById(Cart.Id);

            Assert.Equivalent(Cart, actual);
        }

        [Fact]
        public void TestGetCartByIdFailureDestroitMethod()
        {
            
            var Carts = fixture.AddCarts(10);
            var Cart = Carts.First();
            ICartRepository _CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            var CartService = new CartService(_CartRepo);

            Assert.Throws<CartNotFoundException>(() => CartService.GetCartById(1000));
        }

        [Fact]
        public void TestAddCartSuccessDestroitMethod()
        {
            var Carts = fixture.AddCarts(10);
            var Cart = CartOM.CreateCart().WithId(11).BuildCoreModel();
            ICartRepository _CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            var CartService = new CartService(_CartRepo);

            CartService.AddCart(Cart);
            var actual = CartService.GetCartById(Cart.Id);
            Assert.Equivalent(Cart, actual);

        }
        [Fact]
        public void TestAddCartFailureDestroitMethod()
        {
            var Carts = fixture.AddCarts(10);
            var Cart = Carts.First();
            ICartRepository _CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            var CartService = new CartService(_CartRepo);

            Assert.Throws<CartExistException>(() => CartService.AddCart(Cart));
        }

        [Fact]
        public void TestDeleteCartSuccessDestroitMethod()
        {
            var Carts = fixture.AddCarts(10);
            var Cart = Carts.First();
            ICartRepository _CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            var CartService = new CartService(_CartRepo);

            CartService.DelCart(Cart.Id);
            Assert.Throws<CartNotFoundException>(() => CartService.GetCartById(Cart.Id));
        }
        [Fact]
        public void TestDeleteCartFailureDestroitMethod()
        {
            var Carts = fixture.AddCarts(10);
            ICartRepository _CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            var CartService = new CartService(_CartRepo);
            
            Assert.Throws<CartNotFoundException>(() => CartService.DelCart(100));
        }
        [Fact]
        public void TestUpdateCartSuccessDestroitMethod()
        {
            var Carts = fixture.AddCarts(10);
            var Cart = Carts.First();
            Cart.Data_created = DateTime.Parse("10-10-2024");
            ICartRepository _CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            var CartService = new CartService(_CartRepo);

            CartService.UpdateCart(Cart);
            var actual = CartService.GetCartById(Cart.Id);
            Assert.Equivalent(Cart.Data_created, DateTime.Parse("10-10-2024"));
        }
        [Fact]
        public void TestUpdateCartFailureDestroitMethod()
        {
            var Carts = fixture.AddCarts(10);
            var Cart = CartOM.CreateCart().WithId(100).BuildCoreModel();
            ICartRepository _CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            var CartService = new CartService(_CartRepo);
            
            Assert.Throws<CartNotFoundException>(() => CartService.UpdateCart(Cart));
        }

        [Fact]
        public void TestGetAllDestroitMethod()
        {
            var Carts = fixture.AddCarts(10);
            ICartRepository _CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            var CartService = new CartService(_CartRepo);

            var actual = CartService.GetAllCarts();
            Assert.Equivalent(Carts, actual);

        }
    }
}
