using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Index_input
    {
        public bool isGenerated { get; set; }
        public IQueryable<Product> Products { get; set; }
        public IQueryable<string> ProductLines { get; set; }   
        public IQueryable<Category> Categories { get; set; }      
        public IQueryable<Age_SortType> Age_SortTypes { get; set; }
        public IQueryable<ItemsCount_SortType> ItemsCount_SortTypes { get; set; }
        public IQueryable<FiguresCount_SortType> FiguresCount_SortTypes { get; set; }
        public IQueryable<Price_SortType> Price_SortTypes { get; set; }

    }
}
