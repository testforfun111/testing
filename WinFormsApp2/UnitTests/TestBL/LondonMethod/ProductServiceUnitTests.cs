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
using Microsoft.VisualBasic.ApplicationServices;

namespace UnitTests.UnitTests.TestBL.LondonMethod
{
    [AllureOwner("VHD")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("ProductServices Unit tests")]
    [AllureSubSuite("ProductService unit tests London Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class ProductServiceUnitTests
    {
        private ServiceFixture fixture = new ServiceFixture();
        private ProductObjectMother ProductOM = new ProductObjectMother();
        [AllureBefore]
       
        [Fact]
        public void TestGetProductByIdSuccess()
        {
            var Products = fixture.PrepareProductsForTest();
            var Product = Products[0];
            Mock<IProductRepository> _ProductRepoMock = new Mock<IProductRepository>();
            _ProductRepoMock.Setup(m => m.GetProduct(Product.Id)).Returns(Products.FirstOrDefault(u => u.Id == Product.Id));
            var ProductService = new ProductService(_ProductRepoMock.Object);

            var actual = ProductService.GetProductById(Product.Id);

            Assert.Equal(Product, actual);
            _ProductRepoMock.Verify(m => m.GetProduct(Product.Id), Times.Once());
        }

        [Fact]
        public void TestGetProductByIdFailure()
        {
            var Products = fixture.PrepareProductsForTest();
            var Product = ProductOM.CreateProduct().WithId(11).BuildCoreModel();
            Mock<IProductRepository> _ProductRepoMock = new Mock<IProductRepository>();

            _ProductRepoMock.Setup(m => m.GetProduct(Product.Id)).Returns(Products.FirstOrDefault(u => u.Id == Product.Id));
            var ProductService = new ProductService(_ProductRepoMock.Object);

            Assert.Throws<ProductNotFoundException>(() => ProductService.GetProductById(Product.Id));
            _ProductRepoMock.Verify(m => m.GetProduct(Product.Id), Times.Once());
        }

        [Fact]
        public void TestAddProductSuccess()
        {
            var Products = fixture.PrepareProductsForTest();
            var Product = ProductOM.CreateProduct().WithId(11).BuildCoreModel();
            Mock<IProductRepository> _ProductRepoMock = new Mock<IProductRepository>();

            _ProductRepoMock.Setup(m => m.AddProduct(It.IsAny<Product>())).Callback<Product>(u => Products.Add(u)).Verifiable();
            _ProductRepoMock.Setup(m => m.GetProduct(Product.Id)).Returns(Product);
            var ProductService = new ProductService(_ProductRepoMock.Object);

            ProductService.AddProduct(Product);
            var actual = ProductService.GetProductById(11);
            Assert.Equivalent(Product, actual);
            _ProductRepoMock.Verify(m => m.GetProduct(Product.Id), Times.Once());
        }
        [Fact]
        public void TestAddProductFailure()
        {
            var Products = fixture.PrepareProductsForTest();
            var Product = Products[1];
            Mock<IProductRepository> _ProductRepoMock = new Mock<IProductRepository>();
            _ProductRepoMock.Setup(m => m.AddProduct(It.IsAny<Product>())).Callback<Product>(u => Products.Add(u)).Verifiable();
            _ProductRepoMock.Setup(m => m.IsExistProduct(Product)).Returns(Products.Contains(Product));
            _ProductRepoMock.Setup(m => m.GetProduct(Product.Id)).Returns(Products.Find(u => u.Id == Product.Id));
            var ProductService = new ProductService(_ProductRepoMock.Object);

            Assert.Throws<ProductExistException>(() => ProductService.AddProduct(Product));
        }

        [Fact]
        public void TestDeleteProductSuccess()
        {
            var Products = fixture.PrepareProductsForTest();
            var Product = Products[0];
            Mock<IProductRepository> _ProductRepoMock = new Mock<IProductRepository>();
            _ProductRepoMock.Setup(m => m.GetProduct(Product.Id)).Returns(Products.FirstOrDefault(u => u.Id == Product.Id));
            _ProductRepoMock.Setup(m => m.DelProduct(Product)).Callback(() => Products.Remove(Product));
            var ProductService = new ProductService(_ProductRepoMock.Object);

            ProductService.DelProduct(Product.Id);

            Assert.Equal(9, Products.Count);
            _ProductRepoMock.Verify(m => m.DelProduct(Product), Times.Once());
        }
        [Fact]
        public void TestDeleteProductFailure()
        {
            var Products = fixture.PrepareProductsForTest();
            var Product = ProductOM.CreateProduct().WithId(11).BuildCoreModel();
            Mock<IProductRepository> _ProductRepoMock = new Mock<IProductRepository>();
            _ProductRepoMock.Setup(m => m.GetProduct(Product.Id)).Returns(Products.FirstOrDefault(u => u.Id == Product.Id));
            _ProductRepoMock.Setup(m => m.DelProduct(Product)).Callback(() => Products.Remove(Product));
            var ProductService = new ProductService(_ProductRepoMock.Object);


            Assert.Throws<ProductNotFoundException>(() => ProductService.DelProduct(Product.Id));
            _ProductRepoMock.Verify(m => m.GetProduct(Product.Id), Times.Once());
            _ProductRepoMock.Verify(m => m.DelProduct(Product), Times.Never());
        }
        [Fact]
        public void TestUpdateProductSuccess()
        {
            var Products = fixture.PrepareProductsForTest();
            var Product = Products[0];
            Mock<IProductRepository> _ProductRepoMock = new Mock<IProductRepository>();
            Product.Price = 10;
            _ProductRepoMock.Setup(m => m.UpdateProduct(It.IsAny<Product>()))
                    .Callback((Product Product) =>
                     {
                         Products.Remove(item: Products.Find(e => e.Id == Product.Id)!);
                         Products.Add(Product);
                     }).Verifiable();

            _ProductRepoMock.Setup(m => m.GetProduct(Product.Id)).Returns(Product);
            var ProductService = new ProductService(_ProductRepoMock.Object);

            ProductService.UpdateProduct(Product);
            var actual = ProductService.GetProductById(Product.Id);
            Assert.Equivalent(Product.Price, actual.Price);
        }
        [Fact]
        public void TestUpdateProductFailure()
        {
            var Products = fixture.PrepareProductsForTest();
            var Product = ProductOM.CreateProduct().WithId(11).WithPrice(10).BuildCoreModel();
            Mock<IProductRepository> _ProductRepoMock = new Mock<IProductRepository>();
            Product.Price = 10;
            _ProductRepoMock.Setup(m => m.UpdateProduct(It.IsAny<Product>()))
                    .Callback((Product Product) =>
                    {
                        Products.Remove(item: Products.Find(e => e.Id == Product.Id)!);
                        Products.Add(Product);
                    }).Verifiable();

            _ProductRepoMock.Setup(m => m.GetProduct(Product.Id)).Returns(Products.Find(p => p.Id == Product.Id));
            var ProductService = new ProductService(_ProductRepoMock.Object);

            Assert.Throws<ProductNotFoundException>(() => ProductService.UpdateProduct(Product));
        }
    }
}
