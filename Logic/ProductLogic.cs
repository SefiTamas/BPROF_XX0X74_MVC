using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
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



		public void AddCategoryToProduct(Category category, string productId)
		{
			bool IsPartOfIt = false;
			foreach (var item in categRepo.Read())
			{
				if (item.Id == category.Id) IsPartOfIt = true;
			}
			if (!IsPartOfIt)
			{
				GetProduct(productId).ProductCategories.Add(new ProductCategory()
				{
					Id = Guid.NewGuid().ToString(),
					Category = category,
					ProductId = GetProduct(productId).Id
				});
			}
			else
			{
				GetProduct(productId).ProductCategories.Add(new ProductCategory()
				{
					Id = Guid.NewGuid().ToString(),
					CategoryId = categRepo.Read(category.Id).Id,
					ProductId = GetProduct(productId).Id
				});
			}
			Save();
		}

		public void RemoveAllCategoryFromProduct(string productId)
		{
			GetProduct(productId).ProductCategories.Clear();
			Save();
		}

		public void AddNEWPrice_SortTypeToProduct(Price_SortType price_SortType, string productId)
		{
			productRepo.Read(productId).Price_SortType = price_SortType;
			productRepo.Save();
		}
		public void RemovePrice_SortTypeFromProduct(string productId)
		{
			GetProduct(productId).Price_SortTypeId = null;
			Save();
		}

		public void AddNEWAge_SortTypeToProduct(Age_SortType age_SortType, string productId)
		{
			productRepo.Read(productId).Age_SortType = age_SortType;
			productRepo.Save();
		}
		public void RemoveAge_SortTypeFromProduct(string productId)
		{
			GetProduct(productId).Age_SortTypeId = null;
			Save();
		}

		public void AddNEWItemsCount_SortTypeToProduct(ItemsCount_SortType itemsCount_SortType, string productId)
		{
			productRepo.Read(productId).ItemsCount_SortType = itemsCount_SortType;
			productRepo.Save();
		}
		public void RemoveItemsCount_SortTypeFromProduct(string productId)
		{
			GetProduct(productId).ItemsCount_SortTypeId = null;
			Save();
		}

		public void AddNEWFiguresCount_SortTypeToProduct(FiguresCount_SortType figuresCount_SortType, string productId)
		{
			productRepo.Read(productId).FiguresCount_SortType = figuresCount_SortType;
			productRepo.Save();
		}
		public void RemoveFiguresCount_SortTypeFromProduct(string productId)
		{
			GetProduct(productId).FiguresCount_SortTypeId = null;
			Save();
		}
		public void FillDbWithSamples(bool consoleApp)
		{
			string txtpath = "";
			if (consoleApp)
				txtpath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Data\\tables.txt");
			else txtpath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Data\\tables.txt");
			string[] fulltxt = File.ReadAllLines(txtpath);
			List<Category> categories = new List<Category>();
			List<Price_SortType> price_SortTypes = new List<Price_SortType>();
			List<Age_SortType> age_SortTypes = new List<Age_SortType>();
			List<ItemsCount_SortType> itemsCount_SortTypes = new List<ItemsCount_SortType>();
			List<FiguresCount_SortType> figuresCount_SortTypes = new List<FiguresCount_SortType>();
			int i = 0;
			while (i < fulltxt.Length && fulltxt[i] != "#")
			{
				categories.Add(new Category()
				{
					Id = Guid.NewGuid().ToString(),
					Name = fulltxt[i]
				});
				i++;
			}
			i++;
			while (i < fulltxt.Length && fulltxt[i] != "#")
			{
				string[] s = fulltxt[i].Split('-');
				if (s[0] == "")
				{
					price_SortTypes.Add(new Price_SortType()
					{
						Id = Guid.NewGuid().ToString(),
						PriceMin = null,
						PriceMax = Convert.ToInt32(s[1])
					});
				}
				else if (s[1] == "")
				{
					price_SortTypes.Add(new Price_SortType()
					{
						Id = Guid.NewGuid().ToString(),
						PriceMin = Convert.ToInt32(s[0]),
						PriceMax = null
					});
				}
				else
				{
					price_SortTypes.Add(new Price_SortType()
					{
						Id = Guid.NewGuid().ToString(),
						PriceMin = Convert.ToInt32(s[0]),
						PriceMax = Convert.ToInt32(s[1])
					});
				}

				i++;
			}
			i++;
			while (i < fulltxt.Length && fulltxt[i] != "#")
			{
				if (fulltxt[i].Contains('+'))
				{
					age_SortTypes.Add(new Age_SortType()
					{
						Id = Guid.NewGuid().ToString(),
						RecommendedAge = Convert.ToInt32(fulltxt[i].Split('+')[0]),
						RecommendedAgeMin = null,
						RecommendedAgeMax = null
					});
				}
				else
				{
					age_SortTypes.Add(new Age_SortType()
					{
						Id = Guid.NewGuid().ToString(),
						RecommendedAge = null,
						RecommendedAgeMin = Convert.ToInt32(fulltxt[i].Split('-')[0]),
						RecommendedAgeMax = Convert.ToInt32(fulltxt[i].Split('-')[1])
					});
				}

				i++;
			}
			i++;
			while (i < fulltxt.Length && fulltxt[i] != "#")
			{
				string[] s = fulltxt[i].Split('-');
				if (s[0] == "")
				{
					itemsCount_SortTypes.Add(new ItemsCount_SortType()
					{
						Id = Guid.NewGuid().ToString(),
						ItemsCountMin = null,
						ItemsCountMax = Convert.ToInt32(s[1])
					});
				}
				else if (s[1] == "")
				{
					itemsCount_SortTypes.Add(new ItemsCount_SortType()
					{
						Id = Guid.NewGuid().ToString(),
						ItemsCountMin = Convert.ToInt32(s[0]),
						ItemsCountMax = null
					});
				}
				else
				{
					itemsCount_SortTypes.Add(new ItemsCount_SortType()
					{
						Id = Guid.NewGuid().ToString(),
						ItemsCountMin = Convert.ToInt32(s[0]),
						ItemsCountMax = Convert.ToInt32(s[1])
					});
				}

				i++;
			}
			i++;
			while (i < fulltxt.Length && fulltxt[i] != "#")
			{
				if (fulltxt[i].Contains('-'))
				{
					string[] s = fulltxt[i].Split('-');
					if (s[0] == "")
					{
						figuresCount_SortTypes.Add(new FiguresCount_SortType()
						{
							Id = Guid.NewGuid().ToString(),
							FiguresCount = null,
							FiguresCountMin = null,
							FiguresCountMax = Convert.ToInt32(s[1])
						});
					}
					else if (s[1] == "")
					{
						figuresCount_SortTypes.Add(new FiguresCount_SortType()
						{
							Id = Guid.NewGuid().ToString(),
							FiguresCount = null,
							FiguresCountMin = Convert.ToInt32(s[0]),
							FiguresCountMax = null
						});
					}
					else
					{
						figuresCount_SortTypes.Add(new FiguresCount_SortType()
						{
							Id = Guid.NewGuid().ToString(),
							FiguresCount = null,
							FiguresCountMin = Convert.ToInt32(s[0]),
							FiguresCountMax = Convert.ToInt32(s[1])
						});
					}
				}
				else
				{
					figuresCount_SortTypes.Add(new FiguresCount_SortType()
					{
						Id = Guid.NewGuid().ToString(),
						FiguresCount = Convert.ToInt32(fulltxt[i]),
						FiguresCountMin = null,
						FiguresCountMax = null
					});
				}

				i++;
			}
			i++;
			while (i < fulltxt.Length)
			{
				string ID = Guid.NewGuid().ToString();
				productRepo.Add(new Product()
				{
					Id = ID,
					Name = fulltxt[i].Split('\t')[0],
					ProductLine = fulltxt[i].Split('\t')[1],
					ItemsCount = Convert.ToInt32(fulltxt[i].Split('\t')[3]),
					FiguresCount = Convert.ToInt32(fulltxt[i].Split('\t')[4]),
					Description = fulltxt[i].Split('\t')[5],
					Price = Convert.ToInt32(fulltxt[i].Split('\t')[6]),
					PictureName_thumbnail = fulltxt[i].Split('\t')[7],
					PictureName_big = fulltxt[i].Split('\t')[8]
				});

				Price_SortType price_SortType = (from x in price_SortTypes
												 where (x.PriceMin == null && x.PriceMax > productRepo.Read(ID).Price) || (x.PriceMax == null && x.PriceMin <= productRepo.Read(ID).Price) || (x.PriceMin <= productRepo.Read(ID).Price && x.PriceMax > productRepo.Read(ID).Price)
												 select x).FirstOrDefault();
				AddNEWPrice_SortTypeToProduct(price_SortType, ID);


				int ageIdx = Convert.ToInt32(fulltxt[i].Split('\t')[2]);
				AddNEWAge_SortTypeToProduct(age_SortTypes[ageIdx], ID);


				ItemsCount_SortType itemsCount_SortType = (from x in itemsCount_SortTypes
														   where (x.ItemsCountMin == null && x.ItemsCountMax > productRepo.Read(ID).ItemsCount) || (x.ItemsCountMax == null && x.ItemsCountMin <= productRepo.Read(ID).ItemsCount) || (x.ItemsCountMin <= productRepo.Read(ID).ItemsCount && x.ItemsCountMax > productRepo.Read(ID).ItemsCount)
														   select x).FirstOrDefault();
				AddNEWItemsCount_SortTypeToProduct(itemsCount_SortType, ID);


				FiguresCount_SortType figuresCount_SortType = (from x in figuresCount_SortTypes
															   where x.FiguresCount == productRepo.Read(ID).FiguresCount || (x.FiguresCountMin == null && x.FiguresCountMax >= productRepo.Read(ID).FiguresCount) || (x.FiguresCountMax == null && x.FiguresCountMin <= productRepo.Read(ID).FiguresCount) || (x.FiguresCountMin <= productRepo.Read(ID).FiguresCount && x.FiguresCountMax >= productRepo.Read(ID).FiguresCount)
															   select x).FirstOrDefault();
				AddNEWFiguresCount_SortTypeToProduct(figuresCount_SortType, ID);


				string[] categs = fulltxt[i].Split('\t')[9].Split(',');
				for (int j = 0; j < categs.Length; j++)
				{
					AddCategoryToProduct(categories[Convert.ToInt32(categs[j])], ID);
				}

				i++;
			}

			foreach (var item in price_SortTypes)
			{
				var q = (from x in price_sortRepo.Read()
						 where x.Id == item.Id
						 select x).FirstOrDefault();
				if (q == null)
				{
					price_sortRepo.Add(item);
				}
			}

			foreach (var item in itemsCount_SortTypes)
			{
				var q = (from x in itemsc_sortRepo.Read()
						 where x.Id == item.Id
						 select x).FirstOrDefault();
				if (q == null)
				{
					itemsc_sortRepo.Add(item);
				}
			}

			foreach (var item in figuresCount_SortTypes)
			{
				var q = (from x in figuresc_sortRepo.Read()
						 where x.Id == item.Id
						 select x).FirstOrDefault();
				if (q == null)
				{
					figuresc_sortRepo.Add(item);
				}
			}
		}
	}
}
