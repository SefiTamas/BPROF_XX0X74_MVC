using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Data;
using Repository;
using Logic;
using WebApp.Models;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
		CategoryLogic categLogic;		
		ProductLogic productLogic;		
		Price_SortTypeLogic price_SortTypeLogic;
		Age_SortTypeLogic age_SortTypeLogic;
		ItemsCount_SortTypeLogic itemsCount_SortTypeLogic;
		FiguresCount_SortTypeLogic figuresCount_SortTypeLogic;
		ProductCategoryLogic ProductCategoryLogic;

		public HomeController(CategoryLogic categLogic,			
			ProductLogic productLogic,			
			Price_SortTypeLogic price_SortTypeLogic,
			Age_SortTypeLogic age_SortTypeLogic,
			ItemsCount_SortTypeLogic itemsCount_SortTypeLogic,
			FiguresCount_SortTypeLogic figuresCount_SortTypeLogic,
			ProductCategoryLogic ProductCategoryLogic)
		{
			this.categLogic = categLogic;			
			this.productLogic = productLogic;			
			this.price_SortTypeLogic = price_SortTypeLogic;
			this.age_SortTypeLogic = age_SortTypeLogic;
			this.itemsCount_SortTypeLogic = itemsCount_SortTypeLogic;
			this.figuresCount_SortTypeLogic = figuresCount_SortTypeLogic;
			this.ProductCategoryLogic = ProductCategoryLogic;
		}
		public IActionResult Index()
		{
			HttpContext.Session.SetString("IsGenerated", "False");
			ViewBag.Index = true;
			return View();
		}

		public IActionResult Init()
		{
			productLogic.FillDbWithSamples(false);
			HttpContext.Session.SetString("IsGenerated", "True");
			ViewBag.Index = true;
			Index_input input = new Index_input()
			{
				Products = productLogic.GetAllProduct(),
				ProductLines = productLogic.GetAllProductLine(),
				Categories = categLogic.GetAllCategory(),				
				Age_SortTypes = age_SortTypeLogic.OrderByAge(age_SortTypeLogic.GetAllAge_SortType()),
				ItemsCount_SortTypes = itemsCount_SortTypeLogic.OrderByItems(itemsCount_SortTypeLogic.GetAllItemsCount_SortType()),
				FiguresCount_SortTypes = figuresCount_SortTypeLogic.OrderByFigures(figuresCount_SortTypeLogic.GetAllFiguresCount_SortType()),
				Price_SortTypes = price_SortTypeLogic.OrderByPrice(price_SortTypeLogic.GetAllPrice_SortType())
			};			
			return View(nameof(Index), input);
		}

		public IActionResult Refresh(string id, string newsort)
        {
			if (id != null)
            {
				ViewBag.EditSort = id;
			}
			switch (newsort)
            {
				case "price":
                    {
						ViewBag.AddSort = "price";
						break;
                    }
				case "age":
                    {
						ViewBag.AddSort = "age";
						break;
                    }
				case "itemsc":
                    {
						ViewBag.AddSort = "itemsc";
						break;
                    }
				case "figuresc":
                    {
						ViewBag.AddSort = "figuresc";
						break;
                    }
				case "categ":
                    {
						ViewBag.AddSort = "categ";
						break;
                    }
				default:
                    {
						break;
                    }
            }
			if (HttpContext.Session.GetString("IsGenerated") == "True")
			{
				ViewBag.Index = true;
				Index_input input = new Index_input()
				{
					Products = productLogic.GetAllProduct(),
					ProductLines = productLogic.GetAllProductLine(),
					Categories = categLogic.GetAllCategory(),
					Age_SortTypes = age_SortTypeLogic.OrderByAge(age_SortTypeLogic.GetAllAge_SortType()),
					ItemsCount_SortTypes = itemsCount_SortTypeLogic.OrderByItems(itemsCount_SortTypeLogic.GetAllItemsCount_SortType()),
					FiguresCount_SortTypes = figuresCount_SortTypeLogic.OrderByFigures(figuresCount_SortTypeLogic.GetAllFiguresCount_SortType()),
					Price_SortTypes = price_SortTypeLogic.OrderByPrice(price_SortTypeLogic.GetAllPrice_SortType())
				};
				return View(nameof(Index), input);
			}
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Sort(List<string> products, List<string> productLines, List<string> price_sorttypeids, List<string> age_sorttypeids, List<string> itemsc_sorttypeids, List<string> figuresc_sorttypeids, List<string> categoryids)
        {
			ViewBag.Index = true;
			List<Product> Products = new List<Product>();
            foreach (var item in products)
            {
				Products.Add(productLogic.GetProduct(item));
            }

			if (productLines.Count != 0)
            {
				Products = productLogic.SortByProductLine(Products, productLines);
            }
			if (price_sorttypeids.Count != 0)
            {
				Products = productLogic.SortByPrice(Products, price_sorttypeids);
            }
			if (age_sorttypeids.Count != 0)
            {
				Products = productLogic.SortByAge(Products, age_sorttypeids);
            }
			if (itemsc_sorttypeids.Count != 0)
            {
				Products = productLogic.SortByItemsCount(Products, itemsc_sorttypeids);
            }
			if (figuresc_sorttypeids.Count != 0)
            {
				Products = productLogic.SortByFiguresCount(Products, figuresc_sorttypeids);
            }
			if (categoryids.Count != 0)
            {
				Products = productLogic.SortByCategory(Products, categoryids);
            }

			return View(nameof(Index), new Index_input()
			{
				Products = Products.AsQueryable(),
				ProductLines = productLogic.GetAllProductLine(),
				Categories = categLogic.GetAllCategory(),
				Age_SortTypes = age_SortTypeLogic.OrderByAge(age_SortTypeLogic.GetAllAge_SortType()),
				ItemsCount_SortTypes = itemsCount_SortTypeLogic.OrderByItems(itemsCount_SortTypeLogic.GetAllItemsCount_SortType()),
				FiguresCount_SortTypes = figuresCount_SortTypeLogic.OrderByFigures(figuresCount_SortTypeLogic.GetAllFiguresCount_SortType()),
				Price_SortTypes = price_SortTypeLogic.OrderByPrice(price_SortTypeLogic.GetAllPrice_SortType())
			});

		}

		public IActionResult Product(string productid)
        {
			ViewBag.Index = false;
			ViewBag.ProductViewState = "Display";
			return View(new Product_input()
			{
				Product = productLogic.GetProduct(productid),				
				Categories = categLogic.GetProductCategories(productid),
				CategoriesAll = categLogic.GetAllCategory()
			});
        }

        public IActionResult Change(string productid)
        {
            ViewBag.Index = false;
            ViewBag.ProductViewState = "Change";
            Product_input input = new Product_input()
            {
                Product = productLogic.GetProduct(productid),               
                CategoriesAll = categLogic.GetAllCategory(),
                SelectedCategories = categLogic.GetProductCategoryArray(productid)
            };
            return View(nameof(Product), input);
        }

		public IActionResult AddProduct()
        {
			ViewBag.Index = false;
			ViewBag.ProductViewState = "New";
			Product_input input = new Product_input()
			{
				Product = new Product()
				{
					Id = "",
					ProductLine = "Elementum",
					Age_SortType = null,
					ItemsCount = 0,
					FiguresCount = 0,
					Name = "Lorem ipsum",
					Description = "Quisque a ligula nec tellus malesuada varius. Integer tempus risus eu nunc vehicula, id fringilla justo vehicula. Praesent volutpat massa vel interdum interdum. Fusce ullamcorper placerat leo, ac bibendum ligula venenatis non.",
					Price = 0,
					PictureName_big = "250x200.png",
					PictureName_thumbnail = "150x120.png"
				},
				CategoriesAll = categLogic.GetAllCategory(),
				SelectedCategories = new Category[0]
			};
			return View(nameof(Product), input);
		}

        public IActionResult AddCategoryToProduct(string productid, string productline, string age, string agemin, string agemax, string itemsc, string figuresc, List<string> categories, string title, string description, string price, string thumbnail, string bigpicture)
        {
			ViewBag.Index = false;			
			ViewBag.AddCategory = "True";
			ViewBag.ProductViewState = "Change";
			Product_input input_product = new Product_input()
			{
				Product = new Product()
				{
					Id = productid,
					ProductLine = productline,
					Age_SortType = new Age_SortType()
					{
						RecommendedAge = Convert.ToInt32(age),
						RecommendedAgeMin = Convert.ToInt32(agemin),
						RecommendedAgeMax = Convert.ToInt32(agemax)
					},
					ItemsCount = Convert.ToInt32(itemsc),
					FiguresCount = Convert.ToInt32(figuresc),
					Name = title,
					Description = description,
					Price = Convert.ToInt32(price),
					PictureName_big = bigpicture,
					PictureName_thumbnail = thumbnail
				},
				CategoriesAll = categLogic.GetAllCategory(),
				SelectedCategories = categLogic.GetCategoryArray(categories)
			};
			return View(nameof(Product), input_product);
		}

		public IActionResult RemoveCategoryFromProduct(string productid, string productline, string age, string agemin, string agemax, string itemsc, string figuresc, List<string> categories, string title, string description, string price, string thumbnail, string bigpicture, string nullcateg)
		{
			ViewBag.Index = false;
			ViewBag.ProductViewState = "Change";
			if (nullcateg == "true") ViewBag.AddCategory = "True";
			else ViewBag.AddCategory = "False";
			return View(nameof(Product), new Product_input()
			{
				Product = new Product()
				{
					Id = productid,
					ProductLine = productline,
					Age_SortType = new Age_SortType()
					{
						RecommendedAge = Convert.ToInt32(age),
						RecommendedAgeMin = Convert.ToInt32(agemin),
						RecommendedAgeMax = Convert.ToInt32(agemax)
					},
					ItemsCount = Convert.ToInt32(itemsc),
					FiguresCount = Convert.ToInt32(figuresc),
					Name = title,
					Description = description,
					Price = Convert.ToInt32(price),
					PictureName_big = bigpicture,
					PictureName_thumbnail = thumbnail
				},
				CategoriesAll = categLogic.GetAllCategory(),
				SelectedCategories = categLogic.GetCategoryArray(categories)
			});
		}

        public IActionResult SaveChanges(string productid, string productline, string age, string agemin, string agemax, string itemsc, string figuresc, List<string> categories, string title, string description, string price, string thumbnail, string bigpicture, string newpro)
        {
            Product newProduct = new Product();
            newProduct.Name = title;
            newProduct.ProductLine = productline;
            newProduct.ItemsCount = Convert.ToInt32(itemsc);
            newProduct.ItemsCount_SortType = itemsCount_SortTypeLogic.GetItemsCount_SortTypeForItemsCount(Convert.ToInt32(itemsc));
            newProduct.ItemsCount_SortTypeId = newProduct.ItemsCount_SortType.Id;
            newProduct.FiguresCount = Convert.ToInt32(figuresc);
            newProduct.FiguresCount_SortType = figuresCount_SortTypeLogic.GetFiguresCount_SortTypeForFiguresCount(Convert.ToInt32(figuresc));
            newProduct.FiguresCount_SortTypeId = newProduct.FiguresCount_SortTypeId;            
            newProduct.Description = description;
            newProduct.Price = Convert.ToInt32(price);
            newProduct.Price_SortType = productLogic.GetPrice_SortTypeForPrice(Convert.ToInt32(price));
            newProduct.Price_SortTypeId = newProduct.Price_SortType.Id;
            newProduct.PictureName_thumbnail = thumbnail;
            newProduct.PictureName_big = bigpicture;

			Age_SortType newAge_sortType;

            if (agemin == null && agemax == null)
            {
				newAge_sortType = new Age_SortType()
				{
					Id = Guid.NewGuid().ToString(),
                    RecommendedAge = Convert.ToInt32(age),
                    RecommendedAgeMin = null,
                    RecommendedAgeMax = null
                };
            }
            else
            {
				newAge_sortType = new Age_SortType()
                {
					Id = Guid.NewGuid().ToString(),
					RecommendedAge = null,
                    RecommendedAgeMin = Convert.ToInt32(agemin),
                    RecommendedAgeMax = Convert.ToInt32(agemax)
                };
            }
			if (newpro != "1")
            {
				productLogic.UpdateProduct(productid, newProduct, categLogic.GetCategoriesFromIdList(categories), newAge_sortType);
				return RedirectToAction(nameof(Product), new { productid = productid });
			}
            else
            {
				newProduct.Id = Guid.NewGuid().ToString();
				productLogic.AddProduct(newProduct);
                foreach (var item in categLogic.GetCategoriesFromIdList(categories))
                {
					productLogic.AddCategoryToProduct(item, newProduct.Id);

				}
				productLogic.AddAge_SortTypeToProduct(newAge_sortType, newProduct.Id);
				return RedirectToAction(nameof(Product), new { productid = newProduct.Id });
			}			
        }

		public IActionResult DeleteProduct(string productid)
        {
			productLogic.DeleteProduct(productid);
			return RedirectToAction(nameof(Refresh));
        }

		public IActionResult DeletePriceSort(string id)
        {
			price_SortTypeLogic.DeletePrice_SortType(id);
			return RedirectToAction(nameof(Refresh));
		}

		public IActionResult DeleteItemsSort(string id)
        {
			itemsCount_SortTypeLogic.DeleteItemsCount_SortType(id);
			return RedirectToAction(nameof(Refresh));
		}

		public IActionResult DeleteFiguresSort(string id)
        {
			figuresCount_SortTypeLogic.DeleteFiguresCount_SortType(id);
			return RedirectToAction(nameof(Refresh));
		}

		public IActionResult DeleteAgeSort(string id)
        {
			age_SortTypeLogic.DeleteAge_SortType(id);
			return RedirectToAction(nameof(Refresh));
		}

		public IActionResult DeleteCategory(string id)
        {
			categLogic.DeleteCategory(id);
			return RedirectToAction(nameof(Refresh));
		}

		public IActionResult EditPriceSort(string id, string pricemin, string pricemax, string newsort)
        {
			Price_SortType newPrice_Sort;
			if ((pricemax == null || pricemax == "0") && (pricemin == null || pricemin == "0"))
				return RedirectToAction(nameof(Refresh));
			else if (pricemin == null || pricemin == "0")
            {
				newPrice_Sort = new Price_SortType()
				{
					PriceMin = null,
					PriceMax = Convert.ToInt32(pricemax)
				};
			}
			else if (pricemax == null || pricemax == "0")
            {
				newPrice_Sort = new Price_SortType()
				{
					PriceMin = Convert.ToInt32(pricemin),
					PriceMax = null
				};
			}			
			else
            {
				newPrice_Sort = new Price_SortType()
				{
					PriceMin = Convert.ToInt32(pricemin),
					PriceMax = Convert.ToInt32(pricemax)
				};
			}		
			if (newsort == "1")
            {
				newPrice_Sort.Id = Guid.NewGuid().ToString();
				price_SortTypeLogic.AddPrice_SortType(newPrice_Sort);
				productLogic.AttachProductsToPrice_SortType(newPrice_Sort.Id);

			}
            else
            {
				price_SortTypeLogic.GetPrice_SortType(id).Products.Clear();
				price_SortTypeLogic.UpdatePrice_SortType(id, newPrice_Sort);
				productLogic.AttachProductsToPrice_SortType(id);
			}			
			return RedirectToAction(nameof(Refresh));
		}		

		public IActionResult EditItemsSort(string id, string itemscmin, string itemscmax, string newsort)
        {
			ItemsCount_SortType newItemsc_Sort;
			if ((itemscmax == null || itemscmax == "0") && (itemscmin == null || itemscmin == "0"))
				return RedirectToAction(nameof(Refresh));
			else if (itemscmin == null || itemscmin == "0")
            {
				newItemsc_Sort = new ItemsCount_SortType()
				{
					ItemsCountMin = null,
					ItemsCountMax = Convert.ToInt32(itemscmax)
				};
			}
			else if (itemscmax == null || itemscmax == "0")
            {
				newItemsc_Sort = new ItemsCount_SortType()
				{
					ItemsCountMin = Convert.ToInt32(itemscmin),
					ItemsCountMax = null
				};
			}		
            else
            {
				newItemsc_Sort = new ItemsCount_SortType()
				{
					ItemsCountMin = Convert.ToInt32(itemscmin),
					ItemsCountMax = Convert.ToInt32(itemscmax)
				};
			}		
			if (newsort == "1")
            {
				newItemsc_Sort.Id = Guid.NewGuid().ToString();
				itemsCount_SortTypeLogic.AddItemsCount_SortType(newItemsc_Sort);
				productLogic.AttachProductsToItems_SortType(newItemsc_Sort.Id);
            }
            else
            {
				itemsCount_SortTypeLogic.GetItemsCount_SortType(id).Products.Clear();
				itemsCount_SortTypeLogic.UpdateItemsCount_SortType(id, newItemsc_Sort);
				productLogic.AttachProductsToItems_SortType(id);
			}			
			return RedirectToAction(nameof(Refresh));
		}

		public IActionResult EditFiguresSort(string id, string figuresc, string figurescmin, string figurescmax, string newsort)
        {
			FiguresCount_SortType newFiguresc_Sort;
			if ((figurescmax == null || figurescmax == "0") && (figurescmin == null || figurescmin == "0") && (figuresc == null || figuresc == "0"))
				return RedirectToAction(nameof(Refresh));
			else if ((figurescmax == null || figurescmax == "0") && (figurescmin == null || figurescmin == "0") && (figuresc != null || figuresc != "0"))
            {
				newFiguresc_Sort = new FiguresCount_SortType()
				{
					FiguresCount = Convert.ToInt32(figuresc),
					FiguresCountMin = null,
					FiguresCountMax = null
				};
            }
			else if (figurescmin == null || figurescmin == "0")
			{
				newFiguresc_Sort = new FiguresCount_SortType()
				{
					FiguresCount = null,
					FiguresCountMin = null,
					FiguresCountMax = Convert.ToInt32(figurescmax)
				};
			}
			else if (figurescmax == null || figurescmax == "0")
			{
				newFiguresc_Sort = new FiguresCount_SortType()
				{
					FiguresCount = null,
					FiguresCountMin = Convert.ToInt32(figurescmin),
					FiguresCountMax = null
				};
			}			
			else
			{
				newFiguresc_Sort = new FiguresCount_SortType()
				{
					FiguresCount = null,
					FiguresCountMin = Convert.ToInt32(figurescmin),
					FiguresCountMax = Convert.ToInt32(figurescmax)
				};
			}
			if (newsort == "1")
            {
				newFiguresc_Sort.Id = Guid.NewGuid().ToString();
				figuresCount_SortTypeLogic.AddFiguresCount_SortType(newFiguresc_Sort);
				productLogic.AttachProductsToFigures_SortType(newFiguresc_Sort.Id);
			}
            else
            {
				figuresCount_SortTypeLogic.GetFiguresCount_SortType(id).Products.Clear();
				figuresCount_SortTypeLogic.UpdateFiguresCount_SortType(id, newFiguresc_Sort);
				productLogic.AttachProductsToFigures_SortType(id);
			}			
			return RedirectToAction(nameof(Refresh));
		}

		public IActionResult EditAgeSort(string id, string age, string agemin, string agemax, string newsort)
        {
			Age_SortType newAge_Sort;
			if ((agemax == null || agemax == "0") && (agemin == null || agemin == "0") && (age == null || age == "0"))
				return RedirectToAction(nameof(Refresh));
			else if ((agemax == null || agemax == "0") && (agemin == null || agemin == "0") && (age != null || age != "0"))
			{
				newAge_Sort = new Age_SortType()
				{
					RecommendedAge = Convert.ToInt32(age),
					RecommendedAgeMin = null,
					RecommendedAgeMax = null
				};
			}
			else if (agemin == null || agemin == "0")
			{
				newAge_Sort = new Age_SortType()
				{
					RecommendedAge = null,
					RecommendedAgeMin = null,
					RecommendedAgeMax = Convert.ToInt32(agemax)
				};
			}
			else if (agemax == null || agemax == "0")
			{
				newAge_Sort = new Age_SortType()
				{
					RecommendedAge = null,
					RecommendedAgeMin = Convert.ToInt32(agemin),
					RecommendedAgeMax = null
				};
			}
			else
			{
				newAge_Sort = new Age_SortType()
				{
					RecommendedAge = null,
					RecommendedAgeMin = Convert.ToInt32(agemin),
					RecommendedAgeMax = Convert.ToInt32(agemax)
				};
			}	
			if (newsort == "1")
            {
				newAge_Sort.Id = Guid.NewGuid().ToString();
				age_SortTypeLogic.AddAge_SortType(newAge_Sort);
            }
            else
            {
				age_SortTypeLogic.UpdateAge_SortType(id, newAge_Sort);
			}			
			return RedirectToAction(nameof(Refresh));
		}

		public IActionResult EditCategorySort(string id, string categ, string newsort)
        {
			Category category = new Category()
			{
				Name = categ
			};
			if (newsort == "1")
            {
				category.Id = Guid.NewGuid().ToString();
				categLogic.AddCategory(category);
            }
            else
            {
				categLogic.UpdateCategory(id, category);
			}			
			return RedirectToAction(nameof(Refresh));
		}
	}
}
