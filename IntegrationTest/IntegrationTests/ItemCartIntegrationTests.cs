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
    [AllureSubSuite("ItemCartIntegrational Tests")]
    public class ItemCartIntegrationTests
    {
        private IntegrationFixture fixture;
        private ItemCartService ItemCartService;
        private ItemCartObjectMother ItemCartOM = new ItemCartObjectMother();
        public ItemCartIntegrationTests()
        {
            fixture = new IntegrationFixture();
            var ItemCartRepo = new ItemCartRepository(fixture._dbContextFactory.get_db_context());
            ItemCartService = new ItemCartService(ItemCartRepo);
        }
        [SkippableFact]
        public void TestGetItemCartById()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCarts.First();

            var actual = ItemCartService.GetItemCartById(ItemCart.Id);

            Assert.Equivalent(ItemCart, actual);
        }
       
        [SkippableFact]
        public void TestDeleteItemCart()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCarts.First();

            ItemCartService.DelItemCart(ItemCart.Id);
            Assert.Throws<ItemCartNotFoundException>(() => ItemCartService.GetItemCartById(ItemCart.Id));
        }

        [SkippableFact]
        public void TestUpdateProduc()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCarts.First();
            ItemCart.Quantity = 3;

            ItemCartService.UpdateItemCart(ItemCart);
            var actual = ItemCartService.GetItemCartById(ItemCart.Id);
            Assert.Equivalent(ItemCart.Quantity, 3);
        }

        [SkippableFact]
        public void TestGetAll()
        {
            var ItemCarts = fixture.AddItemCarts(10);
            var ItemCart = ItemCartOM.CreateItemCart().WithId(100).BuildCoreModel();

            Assert.Throws<ItemCartNotFoundException>(() => ItemCartService.UpdateItemCart(ItemCart));

        }
    }
}

