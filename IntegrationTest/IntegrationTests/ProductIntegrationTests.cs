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
    [AllureSubSuite("ProductIntegrational Tests")]
    public class ProductIntegrationTests
    {
        private IntegrationFixture fixture;
        private ProductService productService;
        private ProductObjectMother productOM = new ProductObjectMother();
        public ProductIntegrationTests()
        {
            fixture = new IntegrationFixture();
            var productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            productService = new ProductService(productRepo);
        }
        [SkippableFact]
        public void TestGetProductById()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var products = fixture.AddProducts(10);
            var product = products.First();

            var actual = productService.GetProductById(product.Id);

            Assert.Equivalent(product, actual);
        }
        [SkippableFact]
        public void TestAddProduct()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var products = fixture.AddProducts(10);
            var product = productOM.CreateProduct().WithId(11).WithName("abc").WithPrice(10).WithQuantity(20).BuildCoreModel();

            productService.AddProduct(product);

            var actual = productService.GetProductById(product.Id);
            Assert.Equivalent(product, actual);
        }
        [SkippableFact]
        public void TestDeleteProduct()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var products = fixture.AddProducts(10);
            var product = products.First();

            productService.DelProduct(product.Id);

            Assert.Throws<ProductNotFoundException>(() => productService.GetProductById(product.Id));
        }

        [SkippableFact]
        public void TestUpdateProduc()
        {
            var products = fixture.AddProducts(10);
            var product = products.First();
            product.Name = "edf";

            productService.UpdateProduct(product);

            var actual = productService.GetProductById(product.Id);
            Assert.Equivalent(product.Name, "edf");
        }

        [SkippableFact]
        public void TestGetAll()
        {
            var products = fixture.AddProducts(10);

            var actual = productService.GetAllProducts();

            Assert.Equivalent(products, actual);

        }
    }
}

