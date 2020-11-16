using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProject.Models;
using CoreProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using CoreProject.DataModels;

namespace CoreProject.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        private ICategoryRepository _repository;
        public CategoryController(ICategoryRepository repository, AppDbContext db)
        {
            _db = db;
            _repository = repository;
        }

        public ViewResult Index()
        {
            GetAllCategory();
            return View();
        }
        [HttpGet]
        public string GetAllCategory()
        {
            var list = _repository.GetAllCategory();
            string result = JsonConvert.SerializeObject(list, Formatting.None);
            return result;
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] Category obj)
        {
            _repository.AddCategory(obj);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateCategory([FromBody] Category obj)
        {
            _repository.UpdateCategory(obj);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            _repository.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
