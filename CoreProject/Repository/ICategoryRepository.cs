using CoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategory();
        Category GetCategory(int id);
        Category AddCategory(Category obj);
        Category UpdateCategory(Category changeCategory);
        Category DeleteCategory(int id);
    }
}
