using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
	public class ProductRepository : IRepository<Product>
	{
		DataContext context { get; set; }
		public ProductRepository(DataContext context)
		{
			this.context = context;
		}

		public void Add(Product item)
		{
			context.Products.Add(item);
			context.SaveChanges();
		}

		public void Delete(Product item)
		{
			context.Products.Remove(item);
			context.SaveChanges();
		}

		public void Delete(string Id)
		{
			Delete(Read(Id));
		}

		public Product Read(string Id)
		{
			return context.Products.FirstOrDefault(t => t.Id == Id);
		}

		public IQueryable<Product> Read()
		{
			return context.Products.AsQueryable();
		}

		public void Update(string oldid, Product newitem)
		{
			var olditem = Read(oldid);
			olditem.Name = newitem.Name;
			olditem.ProductLine = newitem.ProductLine;
			olditem.FiguresCount = newitem.FiguresCount;
			olditem.ItemsCount = newitem.ItemsCount;
			olditem.Description = newitem.Description;
			olditem.Price = newitem.Price;
			olditem.PictureName_thumbnail = newitem.PictureName_thumbnail;
			olditem.PictureName_big = newitem.PictureName_big;
			olditem.ItemsCount_SortTypeId = newitem.ItemsCount_SortType.Id;
			olditem.FiguresCount_SortTypeId = newitem.FiguresCount_SortType.Id;
			olditem.Price_SortTypeId = newitem.Price_SortTypeId;
			context.SaveChanges();
		}

		public void Save()
		{
			context.SaveChanges();
		}
	}
}
