using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CategoryRepository : IRepository<Category>
    {
		DataContext context { get; set; }
		public CategoryRepository(DataContext context)
		{
			this.context = context;
		}

		public void Add(Category item)
		{
			context.Categories.Add(item);
			context.SaveChanges();
		}

		public void Delete(Category item)
		{
			context.Categories.Remove(item);
			context.SaveChanges();
		}

		public void Delete(string Id)
		{
			Delete(Read(Id));
		}

		public Category Read(string Id)
		{
			return context.Categories.FirstOrDefault(t => t.Id == Id);
		}

		public IQueryable<Category> Read()
		{
			return context.Categories.AsQueryable();
		}

		public void Update(string oldid, Category newitem)
		{
			var olditem = Read(oldid);
			olditem.Name = newitem.Name;
			context.SaveChanges();
		}

		public void Save()
		{
			context.SaveChanges();
		}
	}
}
