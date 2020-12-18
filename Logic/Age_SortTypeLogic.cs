using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class Age_SortTypeLogic
    {
		IRepository<Product> productRepo;
		IRepository<Age_SortType> age_sortRepo;

		public Age_SortTypeLogic(IRepository<Product> productRepo,
		IRepository<Age_SortType> age_sortRepo)
		{
			this.productRepo = productRepo;
			this.age_sortRepo = age_sortRepo;
		}

		public void AddAge_SortType(Age_SortType item)
		{
			this.age_sortRepo.Add(item);
		}

		public void DeleteAge_SortType(string Id)
		{
			this.age_sortRepo.Delete(Id);
		}

		public IQueryable<Age_SortType> GetAllAge_SortType()
		{
			return age_sortRepo.Read();
		}

		public Age_SortType GetAge_SortType(string Id)
		{
			return age_sortRepo.Read(Id);
		}

		public void UpdateAge_SortType(string Id, Age_SortType newitem)
		{
			age_sortRepo.Update(Id, newitem);
		}

		public IQueryable<Age_SortType> OrderByAge(IQueryable<Age_SortType> ages)
		{
			IQueryable<Age_SortType> age_SortTypes = from x in ages
													 orderby x.RecommendedAgeMin
													 select x;
			return age_SortTypes;
		}
	}
}
