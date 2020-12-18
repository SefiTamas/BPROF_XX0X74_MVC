using Data;
using Logic;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sztf_szakirany_felevesproject
{
   class Program
    {
        static DataContext context = new DataContext();

        static ProductRepository productRepository = new ProductRepository(context);
        static Price_SortTypeRepository price_SortTypeRepository = new Price_SortTypeRepository(context);
        static ItemsCount_SortTypeRepository itemsCount_SortTypeRepository = new ItemsCount_SortTypeRepository(context);
        static FiguresCount_SortTypeRepository figuresCount_SortTypeRepository = new FiguresCount_SortTypeRepository(context);
        static CategoryRepository categoryRepository = new CategoryRepository(context);
        static Age_SortTypeRepository age_SortTypeRepository = new Age_SortTypeRepository(context);
        static ProductCategoryRepository productCategoryRepo = new ProductCategoryRepository(context);

        static Price_SortTypeLogic price_SortTypeLogic = new Price_SortTypeLogic(price_SortTypeRepository);
        static ProductLogic productLogic = new ProductLogic(productRepository, price_SortTypeRepository, itemsCount_SortTypeRepository, figuresCount_SortTypeRepository, categoryRepository, age_SortTypeRepository, productCategoryRepo);
        static CategoryLogic categoryLogic = new CategoryLogic(categoryRepository, productRepository, productCategoryRepo);
        static ProductCategoryLogic productCategoryLogic = new ProductCategoryLogic(categoryRepository, productRepository, productCategoryRepo);
        static Age_SortTypeLogic age_SortTypeLogic = new Age_SortTypeLogic(productRepository, age_SortTypeRepository);
        static FiguresCount_SortTypeLogic figuresCount_SortTypeLogic = new FiguresCount_SortTypeLogic(figuresCount_SortTypeRepository);
        static ItemsCount_SortTypeLogic itemsCount_SortTypeLogic = new ItemsCount_SortTypeLogic(itemsCount_SortTypeRepository);
        static List<Product> products = new List<Product>();        
        static void Main(string[] args)
        {
            productLogic.FillDbWithSamples(true);
            products = productLogic.GetAllProduct().ToList();
            Menu();            
        }   
        
        private static void Menu()
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Termékek szűrése", "Műveletek a szűrő típusú táblákkal", "Új termék felvétele", "Kilépés"
            };
            while (true)
            {
                Console.Clear();                
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0) ProductMenu();
                    else if (row == 1) SortTables();
                    else if (row == 2) NewProductMenu();
                    else if (row == 3) Environment.Exit(0);
                }
            }
        }

        private static void NewProductMenu()
        {
            int row = 0;
            Product p = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Új Termék",
                ProductLine = "*termékvonal*",
                Age_SortType = new Age_SortType()
                {
                    RecommendedAge = 1,
                    RecommendedAgeMin = null,
                    RecommendedAgeMax = null
                },
                ItemsCount = 0,
                FiguresCount = 0,
                PictureName_thumbnail = "kep_kicsi.jpg",
                PictureName_big = "kep_nagy.jpg",
                Description = "",
                Price = 0
            };
            List<Category> categories = new List<Category>();
            List<string> options = new List<string>()
            {
                "Név", "Ár", "Termékvonal", "Ajánlott korosztály", "Elemszám", "Figurák száma","Kategória", "Kép (kicsi)", "Kép (nagy)", "Leírás", "Mentés", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                Console.Write(ProductToString(p, 0, categories));
                Console.Write("\n\nMelyik tulajdonságot szeretné módosítani?\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        ChangeProductNameProperty(ref p);
                    }
                    else if (row == 1)
                    {
                        ChangeProductPriceProperty(ref p);
                    }
                    else if (row == 2)
                    {
                        ChangeProductProductLineProperty(ref p);
                    }
                    else if (row == 3)
                    {
                        ChangeProductAgeMenu(ref p);
                    }
                    else if (row == 4)
                    {
                        ChangeProductItemscProperty(ref p);
                    }
                    else if (row == 5)
                    {
                        ChangeProductFigurescProperty(ref p);
                    }
                    else if (row == 6)
                    {
                        ChangeProductCategoryMenu(ref p, ref categories);
                    }
                    else if (row == 7)
                    {
                        ChangeProductPictureSmallProperty(ref p);
                    }
                    else if (row == 8)
                    {
                        ChangeProductPictureBigProperty(ref p);
                    }
                    else if (row == 9)
                    {
                        ChangeProductDescriptionProperty(ref p);
                    }
                    else if (row == 10)
                    {
                        p.ItemsCount_SortType = itemsCount_SortTypeLogic.GetItemsCount_SortTypeForItemsCount(p.ItemsCount);
                        p.ItemsCount_SortTypeId = p.ItemsCount_SortType.Id;
                        p.FiguresCount_SortType = figuresCount_SortTypeLogic.GetFiguresCount_SortTypeForFiguresCount(p.FiguresCount);
                        p.FiguresCount_SortTypeId = p.FiguresCount_SortType.Id;
                        p.Price_SortType = productLogic.GetPrice_SortTypeForPrice(p.Price);
                        p.Price_SortTypeId = p.Price_SortType.Id;
                        p.Age_SortType.Id = Guid.NewGuid().ToString();
                        productLogic.AddProduct(p);
                        foreach (var item in categories)
                        {
                            productLogic.AddCategoryToProduct(item, p.Id);
                        }
                        productLogic.AddAge_SortTypeToProduct(p.Age_SortType, p.Id);
                        break;
                    }
                    else if (row == 11)
                    {
                        break;
                    }
                }
            }
        }

        private static void SortTables()
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Ár", "Elemszám", "Figurák száma", "Ajánlott korosztály", "Kategória", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                Console.Write("Melyik szűrő táblát szeretné listázni?\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0) ListPriceMenu();
                    else if (row == 1) ListItemscMenu();
                    else if (row == 2) ListFigurescMenu();
                    else if (row == 3) ListAgeMenu();
                    else if (row == 4) ListCategoryMenu();
                    else if (row == 5) break;
                }
            }
        }

        private static void ListCategoryMenu()
        {
        categsStart:
            int row = 0;
            string[,] options = new string[categoryLogic.GetAllCategory().Count() + 1, 2];
            int n = 0;
            foreach (var item in categoryLogic.GetAllCategory())
            {
                options[n, 0] = item.Name;
                options[n, 1] = item.Id;
                n++;
            }
            options[n, 0] = "Vissza";
            while (true)
            {
                Console.Clear();
                Console.Write("Melyik kategórián szeretne műveleteket végrehajtani?\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == options.GetLength(0) - 1) break;
                    else
                    {
                        EditCategoryMenu(options[row, 1]);
                        goto categsStart;
                    }
                }
            }
        }

        private static void EditCategoryMenu(string categid)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Törlés", "Módosítás", "Vissza"
            };
            Category categ = new Category()
            {
                Id = categid,
                Name = categoryLogic.GetCategory(categid).Name
            };
            while (true)
            {
                Console.Clear();
                Console.Write(categ.Name);
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        categoryLogic.DeleteCategory(categid);
                        break;
                    }
                    else if (row == 1)
                    {
                        EditCategorySort(categ);
                        break;
                    }
                    else if (row == 2)
                    {
                        break;
                    }
                }
            }
        }

        private static void EditCategorySort(Category category)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Kategória", "Mentés", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                Console.Write(category.Name);
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        string categ = category.Name;
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(categ);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && categ.Length != 0)
                            {
                                categ = categ.Substring(0, categ.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (categ != null && categ != "0" && categ != "") category.Name = categ;                               
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                categ += m.KeyChar.ToString();
                            }
                        }
                    }                   
                    else if (row == 1)
                    {
                        categoryLogic.UpdateCategory(category.Id, category);
                        break;
                    }
                    else if (row == 2) break;
                }
            }
        }
        private static void ListAgeMenu()
        {
        agesortsStart:
            int row = 0;
            string[,] options = new string[age_SortTypeLogic.GetAllAge_SortType().Count() + 1, 2];
            int n = 0;
            foreach (var item in age_SortTypeLogic.OrderByAge(age_SortTypeLogic.GetAllAge_SortType()))
            {
                if (item.RecommendedAge != null) options[n, 0] = $"{item.RecommendedAge.ToString()}+";
                else options[n, 0] = $"{item.RecommendedAgeMin.ToString()} - {item.RecommendedAgeMax.ToString()}";
                options[n, 1] = item.Id;
                n++;
            }
            options[n, 0] = "Vissza";
            while (true)
            {
                Console.Clear();
                Console.Write("Melyik életkor szűrőn szeretne műveleteket végrehajtani?\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == options.GetLength(0) - 1) break;
                    else
                    {
                        EditAgeMenu(options[row, 1]);
                        goto agesortsStart;
                    }
                }
            }
        }

        private static void EditAgeMenu(string ageid)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Törlés", "Módosítás", "Vissza"
            };
            Age_SortType ageType = new Age_SortType()
            {
                Id = ageid,
                RecommendedAge = age_SortTypeLogic.GetAge_SortType(ageid).RecommendedAge,
                RecommendedAgeMin = age_SortTypeLogic.GetAge_SortType(ageid).RecommendedAgeMin,
                RecommendedAgeMax = age_SortTypeLogic.GetAge_SortType(ageid).RecommendedAgeMax
            };
            while (true)
            {
                Console.Clear();
                if (ageType.RecommendedAge != null)
                {
                    Console.Write($"{ageType.RecommendedAge.ToString()}+");
                }
                else
                {
                    Console.Write($"{ageType.RecommendedAgeMin.ToString()} - {ageType.RecommendedAgeMax.ToString()}");
                }
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        age_SortTypeLogic.DeleteAge_SortType(ageid);
                        break;
                    }
                    else if (row == 1)
                    {
                        EditAgeSort(ageType);
                        break;
                    }
                    else if (row == 2)
                    {
                        break;
                    }
                }
            }
        }

        private static void EditAgeSort(Age_SortType ageType)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Kor", "Min", "Max", "Mentés", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                if (ageType.RecommendedAge != null)
                {
                    Console.Write($"{ageType.RecommendedAge.ToString()}+");
                }
                else
                {
                    Console.Write($"{ageType.RecommendedAgeMin.ToString()} - {ageType.RecommendedAgeMax.ToString()}");
                }
                Console.Write($"\n\nKor: {ageType.RecommendedAge.ToString()}\nMin: {ageType.RecommendedAgeMin.ToString()}\nMax: {ageType.RecommendedAgeMax.ToString()}\n\n");
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        string age = ageType.RecommendedAge.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(age);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && age.Length != 0)
                            {
                                age = age.Substring(0, age.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (age == null || age == "0" || age == "") ageType.RecommendedAge = null;
                                else ageType.RecommendedAge = Convert.ToInt32(age);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                age += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 1)
                    {
                        string min = ageType.RecommendedAgeMin.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(min);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && min.Length != 0)
                            {
                                min = min.Substring(0, min.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (min == null || min == "0" || min == "") ageType.RecommendedAgeMin = null;
                                else ageType.RecommendedAgeMin = Convert.ToInt32(min);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                min += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 2)
                    {
                        string max = ageType.RecommendedAgeMax.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(max);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && max.Length != 0)
                            {
                                max = max.Substring(0, max.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (max == null || max == "0" || max == "") ageType.RecommendedAgeMax = null;
                                else ageType.RecommendedAgeMax = Convert.ToInt32(max);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                max += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 3)
                    {
                        age_SortTypeLogic.UpdateAge_SortType(ageType.Id, ageType);
                        break;
                    }
                    else if (row == 4) break;
                }
            }
        }

        private static void ListFigurescMenu()
        {
        figuressortsStart:
            int row = 0;
            string[,] options = new string[figuresCount_SortTypeLogic.GetAllFiguresCount_SortType().Count() + 1, 2];
            int n = 0;
            foreach (var item in figuresCount_SortTypeLogic.OrderByFigures(figuresCount_SortTypeLogic.GetAllFiguresCount_SortType()))
            {
                if (item.FiguresCount != null) options[n, 0] = $"{item.FiguresCount.ToString()} db";
                else options[n, 0] = $"{item.FiguresCountMin.ToString()} db - {item.FiguresCountMax.ToString()} db";                
                options[n, 1] = item.Id;
                n++;
            }
            options[n, 0] = "Vissza";
            while (true)
            {
                Console.Clear();
                Console.Write("Melyik figurák száma szűrőn szeretne műveleteket végrehajtani?\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == options.GetLength(0) - 1) break;
                    else
                    {
                        EditFigurescMenu(options[row, 1]);
                        goto figuressortsStart;
                    }
                }
            }
        }

        private static void EditFigurescMenu(string figurescid)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Törlés", "Módosítás", "Vissza"
            };
            FiguresCount_SortType figurescType = new FiguresCount_SortType()
            {
                Id = figurescid,
                FiguresCount = figuresCount_SortTypeLogic.GetFiguresCount_SortType(figurescid).FiguresCount,
                FiguresCountMin = figuresCount_SortTypeLogic.GetFiguresCount_SortType(figurescid).FiguresCountMin,
                FiguresCountMax = figuresCount_SortTypeLogic.GetFiguresCount_SortType(figurescid).FiguresCountMax
            };
            while (true)
            {
                Console.Clear();
                if (figurescType.FiguresCount != null)
                {
                    Console.Write($"{figurescType.FiguresCount.ToString()} db");
                }
                else
                {
                    Console.Write($"{figurescType.FiguresCountMin.ToString()} db - {figurescType.FiguresCountMax.ToString()} db");
                }               
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        figuresCount_SortTypeLogic.DeleteFiguresCount_SortType(figurescid);
                        break;
                    }
                    else if (row == 1)
                    {
                        EditFigurescSort(figurescType);
                        break;
                    }
                    else if (row == 2)
                    {
                        break;
                    }
                }
            }
        }

        private static void EditFigurescSort(FiguresCount_SortType figurescType)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Figurák száma", "Min", "Max", "Mentés", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                if (figurescType.FiguresCount != null)
                {
                    Console.Write($"{figurescType.FiguresCount.ToString()} db");
                }
                else
                {
                    Console.Write($"{figurescType.FiguresCountMin.ToString()} db - {figurescType.FiguresCountMax.ToString()} db");
                }
                Console.Write($"\n\nFigurák száma: {figurescType.FiguresCount.ToString()}\nMin: {figurescType.FiguresCountMin.ToString()}\nMax: {figurescType.FiguresCountMax.ToString()}\n\n");
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        string figuresc = figurescType.FiguresCount.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(figuresc);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && figuresc.Length != 0)
                            {
                                figuresc = figuresc.Substring(0, figuresc.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (figuresc == null || figuresc == "0" || figuresc == "") figurescType.FiguresCount = null;
                                else figurescType.FiguresCount = Convert.ToInt32(figuresc);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                figuresc += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 1)
                    {
                        string min = figurescType.FiguresCountMin.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(min);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && min.Length != 0)
                            {
                                min = min.Substring(0, min.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (min == null || min == "0" || min == "") figurescType.FiguresCountMin = null;
                                else figurescType.FiguresCountMin = Convert.ToInt32(min);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                min += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 2)
                    {
                        string max = figurescType.FiguresCountMax.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(max);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && max.Length != 0)
                            {
                                max = max.Substring(0, max.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (max == null || max == "0" || max == "") figurescType.FiguresCountMax = null;
                                else figurescType.FiguresCountMax = Convert.ToInt32(max);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                max += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 3)
                    {
                        figuresCount_SortTypeLogic.GetFiguresCount_SortType(figurescType.Id).Products.Clear();
                        figuresCount_SortTypeLogic.UpdateFiguresCount_SortType(figurescType.Id, figurescType);
                        productLogic.AttachProductsToFigures_SortType(figurescType.Id);
                        break;
                    }
                    else if (row == 4) break;
                }
            }
        }

        private static void ListItemscMenu()
        {
            itemssortsStart:
            int row = 0;
            string[,] options = new string[itemsCount_SortTypeLogic.GetAllItemsCount_SortType().Count() + 1, 2];
            int n = 0;
            foreach (var item in itemsCount_SortTypeLogic.OrderByItems(itemsCount_SortTypeLogic.GetAllItemsCount_SortType()))
            {
                if (item.ItemsCountMin == null) options[n, 0] = $"{item.ItemsCountMax.ToString()} db alatt";
                else if (item.ItemsCountMax == null) options[n, 0] = $"{item.ItemsCountMin.ToString()} db fölött";
                else options[n, 0] = $"{item.ItemsCountMin.ToString()} db - {item.ItemsCountMax.ToString()} db";
                options[n, 1] = item.Id;
                n++;
            }
            options[n, 0] = "Vissza";
            while (true)
            {
                Console.Clear();
                Console.Write("Melyik elemszám szűrőn szeretne műveleteket végrehajtani?\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == options.GetLength(0) - 1) break;
                    else
                    {
                        EditItemscMenu(options[row, 1]);
                        goto itemssortsStart;
                    }
                }
            }
        }

        private static void EditItemscMenu(string itemscid)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Törlés", "Módosítás", "Vissza"
            };
            ItemsCount_SortType itemscType = new ItemsCount_SortType()
            {
                Id = itemscid,
                ItemsCountMin = itemsCount_SortTypeLogic.GetItemsCount_SortType(itemscid).ItemsCountMin,
                ItemsCountMax = itemsCount_SortTypeLogic.GetItemsCount_SortType(itemscid).ItemsCountMax
            };
            while (true)
            {
                Console.Clear();
                if (itemscType.ItemsCountMin == null)
                {
                    Console.Write($"{itemscType.ItemsCountMax.ToString()} db alatt");
                }
                else if (itemscType.ItemsCountMax == null)
                {
                    Console.Write($"{itemscType.ItemsCountMin.ToString()} db fölött");
                }
                else Console.Write($"{itemscType.ItemsCountMin.ToString()} db - {itemscType.ItemsCountMax.ToString()} db");
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        itemsCount_SortTypeLogic.DeleteItemsCount_SortType(itemscid);
                        break;
                    }
                    else if (row == 1)
                    {
                        EditItemscSort(itemscType);
                        break;
                    }
                    else if (row == 2)
                    {
                        break;
                    }
                }
            }
        }

        private static void EditItemscSort(ItemsCount_SortType itemscType)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Min", "Max", "Mentés", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                if (itemscType.ItemsCountMin == null)
                {
                    Console.Write($"{itemscType.ItemsCountMax.ToString()} db alatt");
                }
                else if (itemscType.ItemsCountMax == null)
                {
                    Console.Write($"{itemscType.ItemsCountMin.ToString()} db fölött");
                }
                else Console.Write($"{itemscType.ItemsCountMin.ToString()} db - {itemscType.ItemsCountMax.ToString()} db");
                Console.Write($"\n\nMin: {itemscType.ItemsCountMin.ToString()}\nMax: {itemscType.ItemsCountMax.ToString()}\n\n");
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        string min = itemscType.ItemsCountMin.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(min);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && min.Length != 0)
                            {
                                min = min.Substring(0, min.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (min == null || min == "0" || min == "") itemscType.ItemsCountMin = null;
                                else itemscType.ItemsCountMin = Convert.ToInt32(min);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                min += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 1)
                    {
                        string max = itemscType.ItemsCountMax.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(max);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && max.Length != 0)
                            {
                                max = max.Substring(0, max.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (max == null || max == "0" || max == "") itemscType.ItemsCountMax = null;
                                else itemscType.ItemsCountMax = Convert.ToInt32(max);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                max += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 2)
                    {
                        itemsCount_SortTypeLogic.GetItemsCount_SortType(itemscType.Id).Products.Clear();
                        itemsCount_SortTypeLogic.UpdateItemsCount_SortType(itemscType.Id, itemscType);
                        productLogic.AttachProductsToItems_SortType(itemscType.Id);
                        break;
                    }
                    else if (row == 3) break;
                }
            }
        }

        private static void ListPriceMenu()
        { pricesortsStart:
            int row = 0;
            string[,] options = new string[price_SortTypeLogic.GetAllPrice_SortType().Count() + 1, 2];
            int n = 0;
            foreach (var item in price_SortTypeLogic.OrderByPrice(price_SortTypeLogic.GetAllPrice_SortType()))
            {
                if (item.PriceMin == null) options[n, 0] = $"{item.PriceMax.ToString()} Ft alatt";
                else if (item.PriceMax == null) options[n, 0] = $"{item.PriceMin.ToString()} Ft fölött";
                else options[n, 0] = $"{item.PriceMin.ToString()} Ft - {item.PriceMax.ToString()} Ft";
                options[n, 1] = item.Id;
                n++;
            }
            options[n, 0] = "Vissza";
            while (true)
            {
                Console.Clear();
                Console.Write("Melyik ár szűrőn szeretne műveleteket végrehajtani?\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == options.GetLength(0) - 1) break;
                    else
                    {
                        EditPriceMenu(options[row, 1]);
                        goto pricesortsStart;
                    }
                }
            }
        }

        private static void EditPriceMenu(string priceid)
        {
            int row = 0;
            List<string> options = new List<string>() 
            {
                "Törlés", "Módosítás", "Vissza"
            };
            Price_SortType p = new Price_SortType()
            {
                Id = priceid,
                PriceMin = price_SortTypeLogic.GetPrice_SortType(priceid).PriceMin,
                PriceMax = price_SortTypeLogic.GetPrice_SortType(priceid).PriceMax
            };
            while (true)
            {
                Console.Clear();
                if (p.PriceMin == null)
                {
                    Console.Write($"{p.PriceMax.ToString()} Ft alatt");
                }
                else if (p.PriceMax == null)
                {
                    Console.Write($"{p.PriceMin.ToString()} Ft fölött");
                }
                else Console.Write($"{p.PriceMin.ToString()} Ft - {p.PriceMax.ToString()} Ft");                
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        price_SortTypeLogic.DeletePrice_SortType(priceid);
                        break;
                    }
                    else if (row == 1)
                    {
                        EditPriceSort(p);
                        break;
                    }
                    else if (row == 2)
                    {
                        break;
                    }
                }
            }
        }

        private static void EditPriceSort(Price_SortType p)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Min", "Max", "Mentés", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                if (p.PriceMin == null)
                {
                    Console.Write($"{p.PriceMax.ToString()} Ft alatt");
                }
                else if (p.PriceMax == null)
                {
                    Console.Write($"{p.PriceMin.ToString()} Ft fölött");
                }
                else Console.Write($"{p.PriceMin.ToString()} Ft - {p.PriceMax.ToString()} Ft");
                Console.Write($"\n\nMin: {p.PriceMin.ToString()} Ft\nMax: {p.PriceMax.ToString()} Ft\n\n");
                Console.Write("\n\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        string min = p.PriceMin.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(min);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && min.Length != 0)
                            {
                                min = min.Substring(0, min.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (min == null || min == "0" || min == "") p.PriceMin = null;
                                else p.PriceMin = Convert.ToInt32(min);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                min += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 1)
                    {
                        string max = p.PriceMin.ToString();
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(max);
                            var m = Console.ReadKey();
                            if (m.Key == ConsoleKey.Backspace && max.Length != 0)
                            {
                                max = max.Substring(0, max.Length - 1);
                            }
                            else if (m.Key == ConsoleKey.Enter)
                            {
                                if (max == null || max == "0" || max == "") p.PriceMax = null;
                                else p.PriceMax = Convert.ToInt32(max);
                                break;
                            }
                            else if (int.TryParse(m.KeyChar.ToString(), out int n))
                            {
                                max += m.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 2)
                    {
                        price_SortTypeLogic.GetPrice_SortType(p.Id).Products.Clear();
                        price_SortTypeLogic.UpdatePrice_SortType(p.Id, p);
                        productLogic.AttachProductsToPrice_SortType(p.Id);
                        break;
                    }
                    else if (row == 3) break;
                }
            }
        }

        private static void ProductMenu()
        {
            int row = 0;            
            List<string> options = new List<string>()
            {
                "Termékvonal", "Ár", "Elemszám", "Figurák száma", "Ajánlott korosztály", "Kategória", "Sorszám választás", "Frissítés", "Vissza"
            };
            products = productLogic.GetAllProduct().ToList();
            while (true)
            {
                Console.Clear();
                ListProducts();
                Console.Write("\nMi alapján szeretne szűrni?");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" (A szűrt termékeket fent találja.)");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nVálasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if ( i == row && row == options.Count - 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else if (i == options.Count - 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == options.Count - 3)
                    {
                        string msg = "";
                        while (true)
                        {
                            Console.Write("\nAdja meg a kívánt termék sorszámát. (Küldjön be egy x-et, ha vissza akar menni.)\n");
                            if (msg != "")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(msg);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\n");
                                msg = "";
                            }
                            var l = Console.ReadLine();
                            if (int.TryParse(l, out int n) && Convert.ToInt32(l) <= products.Count)
                            {
                                SelectProductMenu(Convert.ToInt32(l));
                                products = productLogic.GetAllProduct().ToList();
                                break;
                            }
                            else if (!int.TryParse(l, out int x) && l == "x")
                            {
                                break;
                            }
                            else if (!int.TryParse(l, out int d))
                            {
                                msg = "Csak számot adjon meg!";
                            }
                            else if (Convert.ToInt32(l) > products.Count)
                            {
                                msg = $"Csak {products.Count + 1} számnál kisebbet adhat meg!";
                            }
                        }

                    }
                    else if (row == options.Count - 1) break;
                    else if (row == options.Count - 2) products = productLogic.GetAllProduct().ToList();
                    else if (row == 0)
                    {
                        SortByProductLineMenu();
                    }
                    else if (row == 1) SortByPriceMenu();
                    else if (row == 2) SortByItemscMenu();
                    else if (row == 3) SortByFigurescMenu();
                    else if (row == 4) SortByAgeMenu();
                    else if (row == 5) SortByCategoryMenu();
                }
            }
        }

        private static void SortByProductLineMenu()
        {
            int row = 0;
            List<string> options = productLogic.GetAllProductLine().ToList();
            while (true)
            {
                Console.Clear();
                Console.Write("Szűrés termékvonal alapján.\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    SortByProductLine(options[row]);
                    break;
                }
            }
        }

        private static void SortByPriceMenu()
        {
            int row = 0;
            string[,] options = new string[price_SortTypeLogic.GetAllPrice_SortType().Count(), 2];
            int n = 0;
            foreach (var item in price_SortTypeLogic.OrderByPrice(price_SortTypeLogic.GetAllPrice_SortType()))
            {
                if (item.PriceMin == null) options[n, 0] = $"{item.PriceMax.ToString()} Ft alatt";
                else if (item.PriceMax == null) options[n, 0] = $"{item.PriceMin.ToString()} Ft fölött";
                else options[n, 0] = $"{item.PriceMin.ToString()} Ft - {item.PriceMax.ToString()} Ft";
                options[n, 1] = item.Id;
                n++;
            }
            while (true)
            {
                Console.Clear();
                Console.Write("Szűrés ár alapján.\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {                        
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    SortByPrice(options[row, 1]);
                    break;
                }
            }
        }

        private static void SortByItemscMenu()
        {
            int row = 0;
            string[,] options = new string[itemsCount_SortTypeLogic.GetAllItemsCount_SortType().Count(), 2];
            int n = 0;
            foreach (var item in itemsCount_SortTypeLogic.OrderByItems(itemsCount_SortTypeLogic.GetAllItemsCount_SortType()))
            {
                if (item.ItemsCountMin == null) options[n, 0] = $"{item.ItemsCountMax.ToString()} db alatt";
                else if (item.ItemsCountMax == null) options[n, 0] = $"{item.ItemsCountMin.ToString()} db fölött";
                else options[n, 0] = $"{item.ItemsCountMin.ToString()} db - {item.ItemsCountMax.ToString()} db";
                options[n, 1] = item.Id;
                n++;
            }
            while (true)
            {
                Console.Clear();
                Console.Write("Szűrés elemszám alapján.\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    SortByItemsc(options[row, 1]);
                    break;
                }
            }
        }

        private static void SortByFigurescMenu()
        {
            int row = 0;
            string[,] options = new string[figuresCount_SortTypeLogic.GetAllFiguresCount_SortType().Count(), 2];
            int n = 0;
            foreach (var item in figuresCount_SortTypeLogic.OrderByFigures(figuresCount_SortTypeLogic.GetAllFiguresCount_SortType()))
            {
                if (item.FiguresCount == null) options[n, 0] = $"{item.FiguresCountMin.ToString()} db - {item.FiguresCountMax.ToString()} db";
                else options[n, 0] = $"{item.FiguresCount.ToString()} db";
                options[n, 1] = item.Id;
                n++;
            }
            while (true)
            {
                Console.Clear();
                Console.Write("Szűrés figurák száma alapján.\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    SortByFigures(options[row, 1]);
                    break;
                }
            }
        }

        private static void SortByAgeMenu()
        {
            int row = 0;
            string[,] options = new string[age_SortTypeLogic.GetAllAge_SortType().Count(), 2];
            int n = 0;
            foreach (var item in age_SortTypeLogic.OrderByAge(age_SortTypeLogic.GetAllAge_SortType()))
            {
                if (item.RecommendedAge == null) options[n, 0] = $"{item.RecommendedAgeMin.ToString()} - {item.RecommendedAgeMax.ToString()}";
                else options[n, 0] = $"{item.RecommendedAge.ToString()}+";
                options[n, 1] = item.Id;
                n++;
            }
            while (true)
            {
                Console.Clear();
                Console.Write("Szűrés ajánlott korosztály alapján.\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    SortByAge(options[row, 1]);
                    break;
                }
            }
        }

        private static void SortByCategoryMenu()
        {
            int row = 0;
            string[,] options = new string[categoryLogic.GetAllCategory().Count(), 2];
            int n = 0;
            foreach (var item in categoryLogic.GetAllCategory())
            {
                options[n, 0] = item.Name;
                options[n, 1] = item.Id;
                n++;
            }
            while (true)
            {
                Console.Clear();
                Console.Write("Szűrés kategória alapján.\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i, 0]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i, 0]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    SortByCategory(options[row, 1]);
                    break;
                }
            }
        }

        private static void SelectProductMenu(int number)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Törlés", "Módosítás", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                Console.Write(ProductToString(products[number - 1], number, null));
                Console.Write("\n\nMilyen műveletet szeretne végrehajtani?\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        productLogic.DeleteProduct(products[number - 1].Id);
                        break;
                    }
                    else if (row == 1)
                    {
                        EditProductMenu(number);
                        break;
                    }
                    else if (row == 2) break;
                }
            }
        }

        private static void EditProductMenu(int number)
        {
            int row = 0;
            Product p = new Product() 
            { 
                Id = productLogic.GetProduct(products[number - 1].Id).Id,
                Name = productLogic.GetProduct(products[number - 1].Id).Name,
                ProductLine = productLogic.GetProduct(products[number - 1].Id).ProductLine,
                Age_SortType = new Age_SortType()
                {
                    RecommendedAge = productLogic.GetProduct(products[number - 1].Id).Age_SortType.RecommendedAge,
                    RecommendedAgeMin = productLogic.GetProduct(products[number - 1].Id).Age_SortType.RecommendedAgeMin,
                    RecommendedAgeMax = productLogic.GetProduct(products[number - 1].Id).Age_SortType.RecommendedAgeMax
                },
                ItemsCount = productLogic.GetProduct(products[number - 1].Id).ItemsCount,
                FiguresCount = productLogic.GetProduct(products[number - 1].Id).FiguresCount,
                PictureName_thumbnail = productLogic.GetProduct(products[number - 1].Id).PictureName_thumbnail,
                PictureName_big = productLogic.GetProduct(products[number - 1].Id).PictureName_big,
                Description = productLogic.GetProduct(products[number - 1].Id).Description,
                Price = productLogic.GetProduct(products[number - 1].Id).Price
            };
            List<Category> categories = categoryLogic.GetProductCategories(p.Id).ToList();
            List<string> options = new List<string>()
            {
                "Név", "Ár", "Termékvonal", "Ajánlott korosztály", "Elemszám", "Figurák száma","Kategória", "Kép (kicsi)", "Kép (nagy)", "Leírás", "Mentés", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                Console.Write(ProductToString(p, number, categories));
                Console.Write("\n\nMelyik tulajdonságot szeretné módosítani?\n");
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row == 0)
                    {
                        ChangeProductNameProperty(ref p);
                    }
                    else if (row == 1)
                    {
                        ChangeProductPriceProperty(ref p);
                    }
                    else if (row == 2)
                    {
                        ChangeProductProductLineProperty(ref p);
                    }
                    else if (row == 3)
                    {
                        ChangeProductAgeMenu(ref p);
                    }
                    else if (row == 4)
                    {
                        ChangeProductItemscProperty(ref p);
                    }
                    else if (row == 5)
                    {
                        ChangeProductFigurescProperty(ref p);
                    }
                    else if (row == 6)
                    {
                        ChangeProductCategoryMenu(ref p, ref categories);
                    }
                    else if (row == 7)
                    {
                        ChangeProductPictureSmallProperty(ref p);
                    }
                    else if (row == 8)
                    {
                        ChangeProductPictureBigProperty(ref p);
                    }
                    else if (row == 9)
                    {
                        ChangeProductDescriptionProperty(ref p);
                    }
                    else if (row == 10)
                    {
                        p.ItemsCount_SortType = itemsCount_SortTypeLogic.GetItemsCount_SortTypeForItemsCount(p.ItemsCount);
                        p.ItemsCount_SortTypeId = p.ItemsCount_SortType.Id;
                        p.FiguresCount_SortType = figuresCount_SortTypeLogic.GetFiguresCount_SortTypeForFiguresCount(p.FiguresCount);
                        p.FiguresCount_SortTypeId = p.FiguresCount_SortType.Id;
                        p.Price_SortType = productLogic.GetPrice_SortTypeForPrice(p.Price);
                        p.Price_SortTypeId = p.Price_SortType.Id;
                        p.Age_SortType.Id = Guid.NewGuid().ToString();
                        productLogic.UpdateProduct(p.Id, p, categories, p.Age_SortType);
                        break;
                    }
                    else if (row == 11)
                    {
                        break;
                    }
                }
            }
        }

        private static void ChangeProductNameProperty(ref Product product)
        {
            string name = product.Name;
            while (true)
            {
                Console.Clear();
                Console.Write(name);
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.Backspace && name.Length != 0)
                {
                    name = name.Substring(0, name.Length - 1);
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    product.Name = name;
                    break;
                }
                else
                {
                    name += c.KeyChar.ToString();
                }
            }
        }

        private static void ChangeProductPriceProperty(ref Product product)
        {
            string price = product.Price.ToString();
            while (true)
            {
                Console.Clear();
                Console.Write(price);
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.Backspace && price.Length != 0)
                {
                    price = price.Substring(0, price.Length - 1);
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (price == null || price == "0" || price == "") product.Price = 0;
                    else product.Price = Convert.ToInt32(price);
                    break;
                }
                else if (int.TryParse(c.KeyChar.ToString(), out int n))
                {
                    price += c.KeyChar.ToString();
                }
            }
        }

        private static void ChangeProductProductLineProperty(ref Product product)
        {
            string productLine = product.ProductLine;
            while (true)
            {
                Console.Clear();
                Console.Write(productLine);
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.Backspace && productLine.Length != 0)
                {
                    productLine = productLine.Substring(0, productLine.Length - 1);
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    product.ProductLine = productLine;
                    break;
                }
                else
                {
                    productLine += c.KeyChar.ToString();
                }
            }
        }

        private static void ChangeProductPictureSmallProperty(ref Product product)
        {
            string pcture_small = product.PictureName_thumbnail;
            while (true)
            {
                Console.Clear();
                Console.Write(pcture_small);
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.Backspace && pcture_small.Length != 0)
                {
                    pcture_small = pcture_small.Substring(0, pcture_small.Length - 1);
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    product.PictureName_thumbnail = pcture_small;
                    break;
                }
                else
                {
                    pcture_small += c.KeyChar.ToString();
                }
            }
        }

        private static void ChangeProductPictureBigProperty(ref Product product)
        {
            string pcture_big = product.PictureName_big;
            while (true)
            {
                Console.Clear();
                Console.Write(pcture_big);
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.Backspace && pcture_big.Length != 0)
                {
                    pcture_big = pcture_big.Substring(0, pcture_big.Length - 1);
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    product.PictureName_big = pcture_big;
                    break;
                }
                else
                {
                    pcture_big += c.KeyChar.ToString();
                }
            }
        }

        private static void ChangeProductDescriptionProperty(ref Product product)
        {
            string description = product.Description;
            while (true)
            {
                Console.Clear();
                Console.Write(description);
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.Backspace && description.Length != 0)
                {
                    description = description.Substring(0, description.Length - 1);
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    product.Description = description;
                    break;
                }
                else
                {
                    description += c.KeyChar.ToString();
                }
            }
        }

        private static void ChangeProductItemscProperty(ref Product product)
        {
            string itemsc = product.ItemsCount.ToString();
            while (true)
            {
                Console.Clear();
                Console.Write(itemsc);
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.Backspace && itemsc.Length != 0)
                {
                    itemsc = itemsc.Substring(0, itemsc.Length - 1);
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (itemsc == null || itemsc == "0" || itemsc == "") product.ItemsCount = 0;
                    else product.ItemsCount = Convert.ToInt32(itemsc);
                    break;
                }
                else if (int.TryParse(c.KeyChar.ToString(), out int n))
                {
                    itemsc += c.KeyChar.ToString();
                }
            }
        }

        private static void ChangeProductFigurescProperty(ref Product product)
        {
            string figuresc = product.FiguresCount.ToString();
            while (true)
            {
                Console.Clear();
                Console.Write(figuresc);
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.Backspace && figuresc.Length != 0)
                {
                    figuresc = figuresc.Substring(0, figuresc.Length - 1);
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (figuresc == null || figuresc == "0" || figuresc == "") product.FiguresCount = 0;
                    else product.FiguresCount = Convert.ToInt32(figuresc);
                    break;
                }
                else if (int.TryParse(c.KeyChar.ToString(), out int n))
                {
                    figuresc += c.KeyChar.ToString();
                }
            }
        }

        private static void ChangeProductAgeMenu(ref Product product)
        {
            int row = 0;
            List<string> options = new List<string>()
            {
                "Kor", "Min", "Max", "Vissza"
            };
            while (true)
            {
                Console.Clear();
                if (product.Age_SortType.RecommendedAge != null)
                {
                    Console.Write($"{product.Age_SortType.RecommendedAge}+\n\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Kor: {product.Age_SortType.RecommendedAge}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nMin - Max: 0 - 0");
                }
                else
                {                    
                    Console.Write($"{product.Age_SortType.RecommendedAgeMin} - {product.Age_SortType.RecommendedAgeMax}\n\n");
                    Console.Write("Kor: 0\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Min - Max: {product.Age_SortType.RecommendedAgeMin} - {product.Age_SortType.RecommendedAgeMax}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n\nMelyik értéket szeretné módosítani?\n");              
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.Count; i++)
                {
                    if (row == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write($"#>{options[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write($"#>{options[i]}\n");
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.Count - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    string kor = "";
                    if (product.Age_SortType.RecommendedAge == null) kor = "0";
                    else kor = product.Age_SortType.RecommendedAge.ToString();

                    string min = "";
                    if (product.Age_SortType.RecommendedAgeMin == null) min = "0";
                    else min = product.Age_SortType.RecommendedAgeMin.ToString();

                    string max = "";
                    if (product.Age_SortType.RecommendedAgeMax == null) max = "0";
                    else max = product.Age_SortType.RecommendedAgeMax.ToString();

                    if (row == 0)
                    {                        
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(kor);
                            var x = Console.ReadKey();
                            if (x.Key == ConsoleKey.Backspace && kor.Length != 0)
                            {
                                kor = kor.Substring(0, kor.Length - 1);
                            }
                            else if (x.Key == ConsoleKey.Enter)
                            {
                                if (kor == null || kor == "" || kor == "0") product.Age_SortType.RecommendedAge = null;
                                else product.Age_SortType.RecommendedAge = Convert.ToInt32(kor);
                                product.Age_SortType.RecommendedAgeMin = null;
                                product.Age_SortType.RecommendedAgeMax = null;
                                break;
                            }
                            else if (int.TryParse(x.KeyChar.ToString(), out int n))
                            {
                                kor += x.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 1)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(min);
                            var x = Console.ReadKey();
                            if (x.Key == ConsoleKey.Backspace && min.Length != 0)
                            {
                                min = min.Substring(0, min.Length - 1);
                            }
                            else if (x.Key == ConsoleKey.Enter)
                            {
                                if (min == null || min == "" || min == "0") product.Age_SortType.RecommendedAgeMin = null;
                                else product.Age_SortType.RecommendedAgeMin = Convert.ToInt32(min);
                                product.Age_SortType.RecommendedAge = null;
                                break;                               
                            }
                            else if (int.TryParse(x.KeyChar.ToString(), out int n))
                            {
                                min += x.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 2)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(max);
                            var x = Console.ReadKey();
                            if (x.Key == ConsoleKey.Backspace && max.Length != 0)
                            {
                                max = max.Substring(0, max.Length - 1);
                            }
                            else if (x.Key == ConsoleKey.Enter)
                            {
                                if (max == null || max == "" || max == "0") product.Age_SortType.RecommendedAgeMax = null;
                                else product.Age_SortType.RecommendedAgeMax = Convert.ToInt32(max);
                                product.Age_SortType.RecommendedAge = null;
                                break;
                            }
                            else if (int.TryParse(x.KeyChar.ToString(), out int n))
                            {
                                max += x.KeyChar.ToString();
                            }
                        }
                    }
                    else if (row == 3)
                    {
                        break;
                    }
                }
            }
        }

        private static void ChangeProductCategoryMenu(ref Product product, ref List<Category> categories)
        {
            int row = 0;
            string[,] options = new string[categoryLogic.GetAllCategory().Count() + 1, 2];
            int s = 0;
            foreach (var item in categoryLogic.GetAllCategory())
            {
                options[s, 0] = item.Name;
                options[s, 1] = item.Id;
                s++;
            }
            options[s, 0] = "Vissza";
            while (true)
            {
                Console.Clear();
                Console.Write("Válasszon a következő opciók közül:\n");
                for (int i = 0; i < options.GetLength(0); i++)
                {
                    if (row == i)
                    {
                        bool IsSelected = false;
                        foreach (var item in categories)
                        {
                            if (item.Id == options[i, 1])
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.Write($"#>{options[i, 0]}");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Write("\n");
                                IsSelected = true;
                                break;
                            }
                        }
                        if (!IsSelected)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write($"#>{options[i, 0]}");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("\n");
                        }
                    }
                    else
                    {
                        bool IsSelected = false;
                        foreach (var item in categories)
                        {
                            if (item.Id == options[i, 1])
                            {
                                Console.ForegroundColor = ConsoleColor.Green;                               
                                Console.Write($"#>{options[i, 0]}");
                                Console.ForegroundColor = ConsoleColor.White;                               
                                Console.Write("\n");
                                IsSelected = true;
                                break;
                            }
                        }
                        if (!IsSelected)
                        {
                            Console.Write($"#>{options[i, 0]}\n");
                        }
                    }
                }
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.DownArrow && row != options.GetLength(0) - 1)
                {
                    row++;
                }
                else if (c.Key == ConsoleKey.UpArrow && row != 0)
                {
                    row--;
                }
                else if (c.Key == ConsoleKey.Enter)
                {
                    if (row != options.GetLength(0) - 1)
                    {
                        bool isSelected = false;
                        foreach (var item in categories)
                        {
                            if (item.Id == options[row, 1]) isSelected = true;
                        }
                        if (!isSelected)
                        {
                            categories.Add(categoryLogic.GetCategory(options[row, 1]));
                        }
                        else
                        {
                            categories.Remove(categoryLogic.GetCategory(options[row, 1]));
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
       
        private static void ListProducts()
        {
            string str = "";
            int number = 1;
            foreach (var item in products)
            {
                str += ProductToString(item, number, null) + "\n\n";
                number++;
            }
            Console.Write(str);
        }
        private static string ProductToString(Product product, int number, List<Category> categories)
        {
            if (categories == null) categories = categoryLogic.GetProductCategories(product.Id).ToList();

            string str = $"#[{number.ToString()}] {product.Name} -> {product.Id}\n" +
                $"\t>Ár: {Convert.ToString(product.Price)} Ft\n" +
                $"\t>Termékvonal: {product.ProductLine}\n";
            if (product.Age_SortType.RecommendedAge != null)
                str += $"\t>Ajánlott korosztály: {Convert.ToString(product.Age_SortType.RecommendedAge)}+\n";
            else str += $"\t>Ajánlott korosztály: {Convert.ToString(product.Age_SortType.RecommendedAgeMin)} - {Convert.ToString(product.Age_SortType.RecommendedAgeMax)}\n";
            str += $"\t>Elemszám: {Convert.ToString(product.ItemsCount)} db\n" +
                $"\t>Figurák száma: {Convert.ToString(product.FiguresCount)} db\n" +
                $"\t>Kategória:\n";
            foreach (var item in categories)
            {
                str += $"\t\t{item.Name}\n";
            }
            str += $"\t>Kép (kicsi): {product.PictureName_thumbnail}\n" +
                $"\t>Kép (nagy): {product.PictureName_big}\n" +
                $"\t>Leírás:\n" +
                $"\t{product.Description}";
            return str;
        }

        private static void SortByCategory(string categoryid)
        {
            List<string> categories = new List<string>();
            categories.Add(categoryid);
            products = productLogic.SortByCategory(products, categories);           
        }
        private static void SortByProductLine(string productline)
        {
            List<string> productlines = new List<string>();
            productlines.Add(productline);
            products = productLogic.SortByProductLine(products, productlines);        
        }
        private static void SortByItemsc(string itemscid)
        {
            List<string> itemscids = new List<string>();
            itemscids.Add(itemscid);
            products = productLogic.SortByItemsCount(products, itemscids);          
        }
        private static void SortByPrice(string priceid)
        {
            List<string> priceids = new List<string>();
            priceids.Add(priceid);
            products = productLogic.SortByPrice(products, priceids);           
        }
        private static void SortByFigures(string figuresid)
        {
            List<string> figuresids = new List<string>();
            figuresids.Add(figuresid);
            products = productLogic.SortByFiguresCount(products, figuresids);           
        }
        private static void SortByAge(string ageid)
        {
            List<string> ageids = new List<string>();
            ageids.Add(ageid);
            products = productLogic.SortByAge(products, ageids);
        }
        private static void GetProductByIdx(string number)
        {
            List<Product> _products = new List<Product>();
            _products.Add(products[Convert.ToInt32(number) - 1]);
            products = (from x in _products
                        select x).ToList();           
        }
    }
}
