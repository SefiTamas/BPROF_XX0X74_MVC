using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
	public class ProductCategoryLogic
	{
		IRepository<Category> categRepo;
		IRepository<Product> productRepo;
		IRepository<ProductCategory> productCategoryRepo;

		public ProductCategoryLogic(IRepository<Category> categRepo,
		IRepository<Product> productRepo,
		IRepository<ProductCategory> productCategoryRepo)
		{
			this.categRepo = categRepo;
			this.productRepo = productRepo;
			this.productCategoryRepo = productCategoryRepo;
		}
		public void AddProductCategory(ProductCategory item)
		{
			this.productCategoryRepo.Add(item);
		}

		public void DeleteProductCategory(string Id)
		{
			this.productCategoryRepo.Delete(Id);
		}

		public IQueryable<ProductCategory> GetAllProductCategory()
		{
			return productCategoryRepo.Read();
		}

		public ProductCategory GetProductCategory(string Id)
		{
			return productCategoryRepo.Read(Id);
		}

		public void UpdateProductCategory(string Id, ProductCategory newitem)
		{
			productCategoryRepo.Update(Id, newitem);
		}
		public void SaveCategory()
		{
			productCategoryRepo.Save();
		}
	}
}
