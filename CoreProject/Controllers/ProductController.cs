using CoreProject.Models;
using CoreProject.Pagination;
using CoreProject.Repository;
using CoreProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(IProductRepository productRepository, IHostingEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            this._hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public ViewResult List()
        {
            List<Product> products = _productRepository.GetAll().ToList();
            return View(products);
        }
      
        public IActionResult Index(string sortOrder, string SearchString,
            string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string
                .IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewData["CurrentFilter"] = SearchString;

            var data = _productRepository.GetAll().ToList();
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(p => p.ProductName
                .Contains(SearchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(p => p.ProductName).ToList();
                    break;
                default:
                    data = data.OrderBy(p => p.ProductName).ToList();
                    break;
            }
            int pageSize = 3;

            return View(PaginatedList<Product>
                .Create(data.AsQueryable<Product>()
                , pageNumber ?? 1, pageSize));
        }


        [AllowAnonymous]
        public ViewResult Details(int id)
        {
            Product product = _productRepository.GetDetails(id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            ProductDetailViewModel obj = new ProductDetailViewModel()
            {
                Product = _productRepository.GetDetails(id),
                PageTitle = "Product Details"
            };
            CategoryDropDown();
            return View(obj);
        }
        public ViewResult Details1(int id)
        {
            Product product = _productRepository.GetDetails(id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            ProductDetailViewModel obj = new ProductDetailViewModel()
            {
                Product = _productRepository.GetDetails(id),
                PageTitle = "Product Details"
            };
            CategoryDropDown();
            return View(obj);
        }

        public ActionResult Create()
        {
            CategoryDropDown();
            return View();
        }
        private void CategoryDropDown(object categorySelect = null)
        {
            var categories = _productRepository.GetCategories();
            ViewBag.ListOfCategories = new SelectList(categories, "CategoryId", "CategoryName", categorySelect);
        }

        [HttpPost]

        public IActionResult Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string urlImage = "";
                var files = HttpContext.Request.Form.Files;
                foreach (var image in files)
                {
                    if (image != null && image.Length > 0)
                    {
                        var file = image;
                        var uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("_", "") + file.FileName;
                            using (var fileStream = new FileStream(
                                Path.Combine(uploadFile, fileName), FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                                urlImage = fileName;
                            }
                        }
                    }
                }
                var data = new Product()
                {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                    UrlImage = urlImage
                };
                _productRepository.AddProduct(data);
                return RedirectToAction("Index");
            }
            CategoryDropDown();
            return View();
        }

        public ViewResult Edit(int id)
        {
            Product product = _productRepository.GetDetails(id);
            CategoryDropDown();
            return View(product);
        }
        [HttpPost]

        public ActionResult Edit(int id, Product changeProduct)
        {
            if (ModelState.IsValid)
            {
                string urlImage = "";
                var files = HttpContext.Request.Form.Files;
                foreach (var image in files)
                {
                    if (image != null && image.Length > 0)
                    {
                        var file = image;
                        var uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("_", "") + file.FileName;
                            using (var fileStream = new FileStream(
                                Path.Combine(uploadFile, fileName), FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                                urlImage = fileName;
                            }
                        }
                    }
                }
                var data = _productRepository.GetDetails(id);
                data.ProductName = changeProduct.ProductName;
                data.Price = changeProduct.Price;
                data.CategoryId = changeProduct.CategoryId;
                if (data != null)
                {
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", data.UrlImage);
                    System.IO.File.Delete(filePath);
                }
                data.UrlImage = urlImage;
                _productRepository.UpdateProduct(data);
                return RedirectToAction("Index");
            }
            CategoryDropDown();
            return View();
        }


        public ActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
