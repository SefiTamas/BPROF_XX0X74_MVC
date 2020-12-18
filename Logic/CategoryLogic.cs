using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class CategoryLogic
    {
		IRepository<Category> categRepo;
		IRepository<Product> productRepo;
		IRepository<ProductCategory> productCategRepo;

		public CategoryLogic(IRepository<Category> categRepo,
		IRepository<Product> productRepo,
		IRepository<ProductCategory> productCategRepo)
		{
			this.categRepo = categRepo;
			this.productRepo = productRepo;
			this.productCategRepo = productCategRepo;
		}		

		public void AddCategory(Category item)
		{
			this.categRepo.Add(item);
		}

		public void DeleteCategory(string Id)
		{
			var productCategories = from x in productCategRepo.Read()
									where x.CategoryId == Id
									select x;
			foreach (var item in productCategories)
			{
				var products = from x in productRepo.Read()
							   where x.ProductCategories.Any(p => p.Id == item.Id)
							   select x;
				foreach (var p in products)
				{
					p.ProductCategories.Remove(item);
				}
			}
			SaveCategory();
			this.categRepo.Delete(Id);
		}

		public IQueryable<Category> GetAllCategory()
		{
			return categRepo.Read();
		}

		public Category GetCategory(string Id)
		{
			return categRepo.Read(Id);
		}

		public void UpdateCategory(string Id, Category newitem)
		{
			categRepo.Update(Id, newitem);
		}

		public void SaveCategory()
		{
			categRepo.Save();
		}

		public IQueryable<Category> GetProductCategories(string productid)
		{
			var categories = from y in productCategRepo.Read()
							 join categ in GetAllCategory() on y.CategoryId equals categ.Id
							 where y.ProductId == productid
							 select categ;

			return categories;
		}

		public Category[] GetCategoryArray(List<string> categories)
		{
			List<Category> categs = new List<Category>();
			foreach (var item in categories)
			{
				categs.Add(GetCategory(item));
			}
			Category[] categArray = new Category[categs.Count];
			for (int i = 0; i < categs.Count; i++)
			{
				categArray[i] = categs[i];
			}
			return categArray;
		}

		public Category[] GetProductCategoryArray(string productid)
		{
			Category[] categArray = new Category[GetProductCategories(productid).Count()];
			int count = 0;
			foreach (var item in GetProductCategories(productid))
			{
				categArray[count] = item;
				count++;
			}
			return categArray;
		}

		public List<Category> GetCategoriesFromIdList(List<string> ids)
		{
			List<Category> categories = new List<Category>();
			for (int i = 0; i < ids.Count; i++)
			{
				categories.Add(GetCategory(ids[i]));
			}
			return categories;
		}
	}
}
