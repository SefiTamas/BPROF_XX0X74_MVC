using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class Price_SortTypeLogic
    {
		IRepository<Price_SortType> price_sortRepo;
		public Price_SortTypeLogic(IRepository<Price_SortType> price_sortRepo)
		{
			this.price_sortRepo = price_sortRepo;
		}

		public void AddPrice_SortType(Price_SortType item)
		{
			this.price_sortRepo.Add(item);
		}

		public void DeletePrice_SortType(string Id)
		{
			this.price_sortRepo.Delete(Id);
		}

		public IQueryable<Price_SortType> GetAllPrice_SortType()
		{
			return price_sortRepo.Read();
		}

		public Price_SortType GetPrice_SortType(string Id)
		{
			return price_sortRepo.Read(Id);
		}

		public void UpdatePrice_SortType(string Id, Price_SortType newitem)
		{
			price_sortRepo.Update(Id, newitem);
		}
	}
}
