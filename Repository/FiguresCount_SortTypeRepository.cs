using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class FiguresCount_SortTypeRepository : IRepository<FiguresCount_SortType>
    {
        DataContext context { get; set; }
        public FiguresCount_SortTypeRepository(DataContext context)
        {
            this.context = context;
        }
        public void Add(FiguresCount_SortType item)
        {
            context.FiguresCount_SortTypes.Add(item);
            context.SaveChanges();
        }

        public void Delete(FiguresCount_SortType item)
        {
            context.FiguresCount_SortTypes.Remove(item);
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            Delete(Read(Id));
        }

        public FiguresCount_SortType Read(string Id)
        {
            return context.FiguresCount_SortTypes.FirstOrDefault(t => t.Id == Id);
        }

        public IQueryable<FiguresCount_SortType> Read()
        {
            return context.FiguresCount_SortTypes.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string oldid, FiguresCount_SortType newitem)
        {
            var olditem = Read(oldid);
            olditem.FiguresCount = newitem.FiguresCount;
            olditem.FiguresCountMin = newitem.FiguresCountMin;
            olditem.FiguresCountMax = newitem.FiguresCountMax;
            context.SaveChanges();
        }
    }
}
