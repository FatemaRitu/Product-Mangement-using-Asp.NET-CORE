using Microsoft.AspNetCore.Mvc;
using CoreProject.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Email Address")]
        [Remote(action: "IsEmailInUse", controller:"Account")]
        [ValidEmailDomain(allowedDomain:"gmail.com",ErrorMessage ="Email domain must be gmail.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password not matched*")]
        public string ConfirmPassword { get; set; }
    }
}
