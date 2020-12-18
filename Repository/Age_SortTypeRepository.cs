using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class Age_SortTypeRepository : IRepository<Age_SortType>
    {
        DataContext context { get; set; }
        public Age_SortTypeRepository(DataContext context)
        {
            this.context = context;
        }

        public void Add(Age_SortType item)
        {
            context.Age_SortTypes.Add(item);
            context.SaveChanges();
        }

        public void Delete(Age_SortType item)
        {
            context.Age_SortTypes.Remove(item);
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            Delete(Read(Id));
        }

        public Age_SortType Read(string Id)
        {
            return context.Age_SortTypes.FirstOrDefault(t => t.Id == Id);
        }

        public IQueryable<Age_SortType> Read()
        {
            return context.Age_SortTypes.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string oldid, Age_SortType newitem)
        {
            var olditem = Read(oldid);
            olditem.RecommendedAge = newitem.RecommendedAge;
            olditem.RecommendedAgeMin = newitem.RecommendedAgeMin;
            olditem.RecommendedAgeMax = newitem.RecommendedAgeMax;
            context.SaveChanges();
        }
    }
}
