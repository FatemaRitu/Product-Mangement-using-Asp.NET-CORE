using CoreProject.DataModels;
using CoreProject.Models;
using CoreProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Implementaions
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            this._context = context;
        }
        public Category AddCategory(Category obj)
        {
            _context.Categories.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Category DeleteCategory(int id)
        {
            Category delCategory = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
            if (delCategory != null)
            {
                _context.Categories.Remove(delCategory);
                _context.SaveChanges();
            }
            return delCategory;
        }

        public List<Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            Category prod = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
            return prod;
        }

        public Category UpdateCategory(Category changeCategory)
        {
            Category prod = _context.Categories.FirstOrDefault(p => p.CategoryId == changeCategory.CategoryId);
            prod.CategoryName = changeCategory.CategoryName;
            _context.SaveChanges();
            return changeCategory;
        }
    }
}
