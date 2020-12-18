using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class Price_SortTypeRepository : IRepository<Price_SortType>
    {
        DataContext context { get; set; }
        public Price_SortTypeRepository(DataContext context)
        {
            this.context = context;
        }
        public void Add(Price_SortType item)
        {
            context.Price_SortTypes.Add(item);
            context.SaveChanges();
        }

        public void Delete(Price_SortType item)
        {
            context.Price_SortTypes.Remove(item);
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            Delete(Read(Id));
        }

        public Price_SortType Read(string Id)
        {
            return context.Price_SortTypes.FirstOrDefault(t => t.Id == Id);
        }

        public IQueryable<Price_SortType> Read()
        {
            return context.Price_SortTypes.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string oldid, Price_SortType newitem)
        {
            var olditem = Read(oldid);
            olditem.PriceMin = newitem.PriceMin;
            olditem.PriceMax = newitem.PriceMax;
            context.SaveChanges();
        }
    }
}
