using CoreProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.DataModels
{
    public static class ModuleBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Beauty Products" },
                new Category { CategoryId = 2, CategoryName = "Women's Dress & Clothing" },
                new Category { CategoryId = 3, CategoryName = "Men's Dress & Clothing" },
                new Category { CategoryId = 4, CategoryName = "Baby's Care & Dress" },
                new Category { CategoryId = 5, CategoryName = "Bag & Luggage" }
                );
            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   ProductId = 1,
                   ProductName = "Indian Sharee",
                   Price = 9000,
                   UrlImage="a1.jpg",
                   CategoryId = 2
               },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Fogg Perfume",
                    Price = 900,
                    UrlImage = "a1.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "Denim Shirt",
                    Price = 3000,
                    UrlImage = "a1.jpg",
                    CategoryId = 3
                },
                new Product
                {
                    ProductId = 4,
                    ProductName = "Jonson Body Wash",
                    Price = 1000,
                    UrlImage = "a1.jpg",
                    CategoryId = 4
                },
                new Product
                {
                    ProductId = 5,
                    ProductName = "Baby diaper",
                    Price = 500,
                    UrlImage = "a1.jpg",
                    CategoryId = 4
                },
                 new Product
                 {
                     ProductId = 6,
                     ProductName = "Ladies Bag",
                     Price = 4000,
                     UrlImage = "a1.jpg",
                     CategoryId = 5
                 }                            
             );
        }
    }
}
