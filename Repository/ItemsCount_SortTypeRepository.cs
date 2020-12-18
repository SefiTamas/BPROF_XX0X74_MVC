using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ItemsCount_SortTypeRepository : IRepository<ItemsCount_SortType>
    {
        DataContext context { get; set; }
        public ItemsCount_SortTypeRepository(DataContext context)
        {
            this.context = context;
        }
        public void Add(ItemsCount_SortType item)
        {
            context.ItemsCount_SortTypes.Add(item);
            context.SaveChanges();
        }

        public void Delete(ItemsCount_SortType item)
        {
            context.ItemsCount_SortTypes.Remove(item);
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            Delete(Read(Id));
        }

        public ItemsCount_SortType Read(string Id)
        {
            return context.ItemsCount_SortTypes.FirstOrDefault(t => t.Id == Id);
        }

        public IQueryable<ItemsCount_SortType> Read()
        {
            return context.ItemsCount_SortTypes.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string oldid, ItemsCount_SortType newitem)
        {
            var olditem = Read(oldid);
            olditem.ItemsCountMin = newitem.ItemsCountMin;
            olditem.ItemsCountMax = newitem.ItemsCountMax;
            context.SaveChanges();
        }
    }
}
