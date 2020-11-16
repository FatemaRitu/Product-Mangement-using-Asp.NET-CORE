using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.ViewModels
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage ="Enter Product Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Enter Product Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Choose Image")]
        public IFormFile UrlImage { get; set; }
        [Required(ErrorMessage = "Choose category")]
        public int CategoryId { get; set; }
    }
}
