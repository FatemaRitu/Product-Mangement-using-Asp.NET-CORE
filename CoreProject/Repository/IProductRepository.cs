using CoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetDetails(int id);
        Product AddProduct(Product obj);

        Product UpdateProduct(Product changeProduct);
        void DeleteProduct(int id);
        IEnumerable<Category> GetCategories();
    }
}
