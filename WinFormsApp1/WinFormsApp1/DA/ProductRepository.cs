using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using System.Data;
using Interfaces;
using Data;
using Models;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public Product GetProduct(int id)
        {
            return _context.products.Where(p => p.Id == id).FirstOrDefault();
        }

        public Product GetProduct(string name)
        {
            return _context.products.Where(p => p.Name == name).FirstOrDefault();
        }

        public void AddProduct(Product product)
        {
            List<Product>? lst = _context.products.Count() > 0 ? _context.products.ToList() : null;
            int maxid = 0;
            if (lst != null)
            {
                foreach (Product temp in lst)
                    if (temp.Id > maxid)
                        maxid = temp.Id;
            }
            product.Id = maxid + 1;
            _context.products.Add(product);
            _context.SaveChanges();
        }

        public void DelProduct(Product product)
        {
            Product productToRemove = _context.products.FirstOrDefault(u => u.Id == product.Id);
            if (productToRemove != null)
            {
                _context.Remove(productToRemove);
            }
            _context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            var productToUpdate = _context.products.FirstOrDefault(u => u.Id == product.Id);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Description = product.Description;
                _context.SaveChanges();
            }
        }

        public bool IsExistProduct(Product product)
        {
            return _context.products.Any(u => u.Id == product.Id);
        }

        public ICollection<Product> GetAllProducts()
        {
            return _context.products.ToList();
        }
    }
}
