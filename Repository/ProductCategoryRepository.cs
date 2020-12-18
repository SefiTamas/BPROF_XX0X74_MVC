using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ProductCategoryRepository : IRepository<ProductCategory>
    {
        DataContext context { get; set; }
        public ProductCategoryRepository(DataContext context)
        {
            this.context = context;
        }
        public void Add(ProductCategory item)
        {
            context.ProductCategory.Add(item);
            context.SaveChanges();
        }

        public void Delete(ProductCategory item)
        {
            context.ProductCategory.Remove(item);
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            Delete(Read(Id));
        }

        public ProductCategory Read(string Id)
        {
            return context.ProductCategory.FirstOrDefault(t => t.Id == Id);
        }

        public IQueryable<ProductCategory> Read()
        {
            return context.ProductCategory.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string oldid, ProductCategory newitem)
        {
            throw new NotImplementedException();
        }
    }
}
