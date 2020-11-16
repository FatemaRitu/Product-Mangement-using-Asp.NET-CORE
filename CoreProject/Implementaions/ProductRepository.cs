using CoreProject.DataModels;
using CoreProject.Models;
using CoreProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Implementaions
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public Product AddProduct(Product obj)
        {
            _context.Products.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public void DeleteProduct(int id)
        {
            Product product = GetDetails(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            var data = _context.Products.Select(p => new Product
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                UrlImage = p.UrlImage,
                CategoryId = p.CategoryId,
                Category = _context.Categories.Where(c => c.CategoryId == p.CategoryId).FirstOrDefault()
            }).ToList();
            return data;
        }

        public IEnumerable<Category> GetCategories()
        {
            var data = _context.Categories.ToList();
            return data;
        }

        public Product GetDetails(int id)
        {
            return _context.Products.FirstOrDefault(e => e.ProductId == id);

        }

        public Product UpdateProduct(Product changeProduct)
        {
            var obj = _context.Products.Attach(changeProduct);
            obj.State = EntityState.Modified;
            _context.SaveChanges();
            return changeProduct;
        }
    }
}
