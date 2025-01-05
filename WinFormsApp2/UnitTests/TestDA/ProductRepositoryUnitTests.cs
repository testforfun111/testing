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
    [AllureSubSuite("ProductRepositoty Unit tests")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class ProductRepositoryUnitTests
    {
        private IProductRepository _ProductRepository;
        private DBFixture _dbFixture;
        private ProductObjectMother _ProductObjectMother;
        public ProductRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _ProductRepository = new ProductRepository(_dbFixture._dbContextFactory.get_db_context());
            _ProductObjectMother = new ProductObjectMother();
        }
        [Fact]
        public void TestGetProductById()
        {
            var Products = _dbFixture.AddProducts(10);
            var expected = Products[0];

            var actual = _ProductRepository.GetProduct(expected.Id);

            Assert.Equivalent(expected, actual);
        }
        [Fact]
        public void TestGetProductByName()
        {
            var Products = _dbFixture.AddProducts(10);
            var expected = Products.First();

            var actual = _ProductRepository.GetProduct(expected.Name);

            Assert.Equivalent(expected, actual);
        }
        
        [Fact]
        public void TestDelete()
        {
            var Products = _dbFixture.AddProducts(10);
            var Product = Products.First();

            _ProductRepository.DelProduct(Product);

            var actual = _ProductRepository.GetProduct(Product.Id);
            Assert.Null(actual);
        }
        [Fact]
        public void TestUpdate()
        {
            var Products = _dbFixture.AddProducts(10);
            var Product = _ProductObjectMother.CreateProduct().WithId(1).WithPrice(10).BuildCoreModel();

            _ProductRepository.UpdateProduct(Product);

            var actual = _ProductRepository.GetProduct(Product.Id);
            Assert.Equivalent(Product, actual);
        }
        [Fact]
        public void TestAddProduct()
        {
            var Products = _dbFixture.AddProducts(10);
            var Product = _ProductObjectMother.CreateProduct().BuildCoreModel();

            _ProductRepository.AddProduct(Product);

            var actual = _ProductRepository.GetProduct(11);
            Assert.Equivalent(Product, actual);
        }
        [Fact]
        public void TestGetAll()
        {
            var Products = _dbFixture.AddProducts(10);

            var actual = _ProductRepository.GetAllProducts();

            Assert.Equivalent(Products, actual);
        }
    }
}
