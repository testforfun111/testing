using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;
using Repository;
using Exceptions;

namespace Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new Exception("Параметр пустой!");
        }

        public Product GetProductById(int id)
        {
            Product product = _productRepository.GetProduct(id);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            return product;
        }

        public Product GetProductByName(string name)
        {
            return _productRepository.GetProduct(name);
        }

        public void AddProduct(Product product)
        {
            if (_productRepository.IsExistProduct(product) == true)
            {
                throw new ProductExistException();
            }
            else 
                _productRepository.AddProduct(product);
        }

        public void DelProduct(int id)
        {
            if (GetProductById(id) != null)
                _productRepository.DelProduct(GetProductById(id));
            else
                throw new ProductNotFoundException();
        }
        public void UpdateProduct(Product _product) 
        {
            Product product = _productRepository.GetProduct(_product.Id);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            else
                _productRepository.UpdateProduct(_product);
        }

        public ICollection<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
    }
}
