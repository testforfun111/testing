using Allure.Xunit.Attributes;
using ItegrationalTests.Fixture;
using Models;
using Services;
using Repository;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;
using Xunit;
using Interfaces;
using UnitTests.Fixture;
using Exceptions;

namespace ItegrationalTests.IntegrationalTests
{
    [AllureOwner("VHD")]
    [AllureSuite("Integrational Tests")]
    [AllureSubSuite("CartIntegrational Tests")]
    public class CartIntegrationTests
    {
        private IntegrationFixture fixture;
        private CartService CartService;
        private CartObjectMother CartOM = new CartObjectMother();
        public CartIntegrationTests()
        {
            fixture = new IntegrationFixture();
            var CartRepo = new CartRepository(fixture._dbContextFactory.get_db_context());
            CartService = new CartService(CartRepo);
        }
        [SkippableFact]
        public void TestGetCartById()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var Carts = fixture.AddCarts(10);
            var Cart = Carts.First();

            var actual = CartService.GetCartById(Cart.Id);

            Assert.Equivalent(Cart.Id_user, actual.Id_user);
            Assert.Equivalent(Cart.Id, actual.Id);
        }
        [SkippableFact]
        public void TestAddCart()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var Carts = fixture.AddCarts(10);
            var Cart = CartOM.CreateCart().WithId(11).BuildCoreModel();

            CartService.AddCart(Cart);
            var actual = CartService.GetCartById(Cart.Id);

            Assert.Equivalent(Cart.Id_user, actual.Id_user);
        }
        [SkippableFact]
        public void TestDeleteCart()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var Carts = fixture.AddCarts(10);
            var Cart = Carts.First();

            CartService.DelCart(Cart.Id);
            Assert.Throws<CartNotFoundException>(() => CartService.GetCartById(Cart.Id));
        }

        [SkippableFact]
        public void TestUpdateCart()
        {
            var Carts = fixture.AddCarts(10);
            var Cart = Carts.First();
            Cart.Data_created = DateTime.Parse("10-10-2024").ToUniversalTime();

            CartService.UpdateCart(Cart);
            var actual = CartService.GetCartById(Cart.Id);
            Assert.Equivalent(Cart.Data_created, DateTime.Parse("10-10-2024").ToUniversalTime());
        }

        [SkippableFact]
        public void TestGetAll()
        {
            var Carts = fixture.AddCarts(10);

            var actual = CartService.GetAllCarts();
            Assert.Equivalent(Carts.Count, actual.Count);

        }
    }
}

