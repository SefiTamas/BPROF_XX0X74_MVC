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
	}
}
