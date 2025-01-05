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
    [AllureSubSuite("CartRepositoty Unit tests")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Cart.RandomCart",
        ordererAssemblyName: "UnitTests")]
    public class CartRepositoryUnitTests
    {
        private ICartRepository _CartRepository;
        private DBFixture _dbFixture;
        private CartObjectMother _CartObjectMother;
        public CartRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _CartRepository = new CartRepository(_dbFixture._dbContextFactory.get_db_context());
            _CartObjectMother = new CartObjectMother();
        }
        [Fact]
        public void TestGetCartById()
        {
            var Carts = _dbFixture.AddCarts(10);
            var expected = Carts[0];

            var actual = _CartRepository.GetCart(expected.Id);

            Assert.Equivalent(expected, actual);
        }
        
        [Fact]
        public void TestDeleteCart()
        {
            var Carts = _dbFixture.AddCarts(10);
            var Cart = Carts.First();

            _CartRepository.DelCart(Cart);

            var actual = _CartRepository.GetCart(Cart.Id);
            Assert.Null(actual);
        }
        [Fact]
        public void TestUpdateCart()
        {
            var Carts = _dbFixture.AddCarts(10);
            var Cart = _CartObjectMother.CreateCart().WithId(1).BuildCoreModel();

            _CartRepository.UpdateCart(Cart);

            var actual = _CartRepository.GetCart(Cart.Id);
            Assert.Equivalent(Cart, actual);
        }
        [Fact]
        public void TestAddCart()
        {
            var Carts = _dbFixture.AddCarts(10);
            var Cart = _CartObjectMother.CreateCart().BuildCoreModel();

            _CartRepository.AddCart(Cart);

            var actual = _CartRepository.GetCart(11);
            Assert.Equivalent(Cart, actual);
        }
        [Fact]
        public void TestGetAll()
        {
            var Carts = _dbFixture.AddCarts(10);

            var actual = _CartRepository.GetAllCarts();

            Assert.Equivalent(Carts, actual);
        }
    }
}
