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
    [AllureSuite("ProductServices Unit tests")]
    [AllureSubSuite("ProductService unit tests Destroit Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class ProductServiceUnitTests
    {
        private DBFixture fixture = new DBFixture();
        private ProductObjectMother productOM = new ProductObjectMother();
        [AllureBefore]
        public ProductServiceUnitTests() { }
   
        [Fact]
        public void TestGetProductByIdSuccessDestroitMethod()
        {
            var products = fixture.AddProducts(10);
            var product = products.First();
            IProductRepository _productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            var productService = new ProductService(_productRepo);

            var actual = productService.GetProductById(product.Id);

            Assert.Equivalent(product, actual);
        }

        [Fact]
        public void TestGetProductByIdFailureDestroitMethod()
        {
            var products = fixture.AddProducts(10);
            var product = products.First();
            IProductRepository _productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            var productService = new ProductService(_productRepo);

            Assert.Throws<ProductNotFoundException>(() => productService.GetProductById(1000));
        }

        [Fact]
        public void TestAddProductSuccessDestroitMethod()
        {
            var products = fixture.AddProducts(10);
            var product = productOM.CreateProduct().WithId(11).WithName("abc").WithPrice(10).WithQuantity(20).BuildCoreModel();
            IProductRepository _productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            var productService = new ProductService(_productRepo);

            productService.AddProduct(product);
            var actual = productService.GetProductById(product.Id);
            Assert.Equivalent(product, actual);
        }
        [Fact]
        public void TestAddProductFailureDestroitMethod()
        {
            var products = fixture.AddProducts(10);
            var product = products.First();
            IProductRepository _productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            var productService = new ProductService(_productRepo);

            Assert.Throws<ProductExistException>(() => productService.AddProduct(product));
        }

        [Fact]
        public void TestDeleteProductSuccessDestroitMethod()
        {
            var products = fixture.AddProducts(10);
            var product = products.First();
            IProductRepository _productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            var productService = new ProductService(_productRepo);

            productService.DelProduct(product.Id);
            Assert.Throws<ProductNotFoundException>(() => productService.GetProductById(product.Id));
        }
        [Fact]
        public void TestDeleteProductFailureDestroitMethod()
        {
            var products = fixture.AddProducts(10);
            IProductRepository _productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            var productService = new ProductService(_productRepo);
            
            Assert.Throws<ProductNotFoundException>(() => productService.DelProduct(100));
        }
        
        public void TestUpdateProductSuccessDestroitMethod()
        {
            var products = fixture.AddProducts(10);
            var product = products.First();
            product.Name = "edf";
            IProductRepository _productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            var productService = new ProductService(_productRepo);

            productService.UpdateProduct(product);
            var actual = productService.GetProductById(product.Id);
            Assert.Equivalent(product.Name, "edf");
        }
        [Fact]
        public void TestUpdateProductFailureDestroitMethod()
        {
            var products = fixture.AddProducts(10);
            var product = productOM.CreateProduct().WithId(100).WithPrice(10).BuildCoreModel();
            IProductRepository _productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            var productService = new ProductService(_productRepo);
            
            Assert.Throws<ProductNotFoundException>(() => productService.UpdateProduct(product));
        }

        [Fact]
        public void TestGetAllDestroitMethod()
        {
            var products = fixture.AddProducts(10);
            IProductRepository _productRepo = new ProductRepository(fixture._dbContextFactory.get_db_context());
            var productService = new ProductService(_productRepo);

            var actual = productService.GetAllProducts();
            Assert.Equivalent(products, actual);

        }
    }
}
