using Models;
using Repository;
using System;
using System.Linq;

namespace Logic
{
    public class ProductLogic
    {
		IRepository<Product> productRepo;
		IRepository<Price_SortType> price_sortRepo;
		IRepository<ItemsCount_SortType> itemsc_sortRepo;
		IRepository<FiguresCount_SortType> figuresc_sortRepo;
		IRepository<Category> categRepo;
		IRepository<Age_SortType> ageRepo;
		IRepository<ProductCategory> productCategRepo;
		public ProductLogic(IRepository<Product> productRepo,
			IRepository<Price_SortType> price_sortRepo,
			IRepository<ItemsCount_SortType> itemsc_sortRepo,
			IRepository<FiguresCount_SortType> figuresc_sortRepo,
			IRepository<Category> categRepo,
			IRepository<Age_SortType> ageRepo,
			IRepository<ProductCategory> productCategRepo)
		{
			this.productRepo = productRepo;
			this.price_sortRepo = price_sortRepo;
			this.itemsc_sortRepo = itemsc_sortRepo;
			this.figuresc_sortRepo = figuresc_sortRepo;
			this.categRepo = categRepo;
			this.ageRepo = ageRepo;
			this.productCategRepo = productCategRepo;
		}		

		public void AddProduct(Product item)
		{
			this.productRepo.Add(item);
		}

		public void DeleteProduct(string Id)
		{
			this.productRepo.Delete(Id);
		}

		public IQueryable<Product> GetAllProduct()
		{
			return productRepo.Read();
		}

		public Product GetProduct(string Id)
		{
			return productRepo.Read(Id);
		}

		public void UpdateProduct(string Id, Product newitem)
		{
			productRepo.Update(Id, newitem);
		}

		public void Save()
		{
			productRepo.Save();
		}
	}
}
