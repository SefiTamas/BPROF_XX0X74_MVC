using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Product_input
    {
        public Product Product { get; set; }         
        public IQueryable<Category> CategoriesAll { get; set; }
        public IQueryable<Category> Categories { get; set; }
        public Category[] SelectedCategories { get; set; }        
    }
}
