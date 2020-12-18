using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class ItemsCount_SortTypeLogic
    {
		IRepository<ItemsCount_SortType> itemsc_sortRepo;

		public ItemsCount_SortTypeLogic(IRepository<ItemsCount_SortType> itemsc_sortRepo)
		{
			this.itemsc_sortRepo = itemsc_sortRepo;
		}

		public void AddItemsCount_SortType(ItemsCount_SortType item)
		{
			this.itemsc_sortRepo.Add(item);
		}

		public void DeleteItemsCount_SortType(string Id)
		{
			this.itemsc_sortRepo.Delete(Id);
		}

		public IQueryable<ItemsCount_SortType> GetAllItemsCount_SortType()
		{
			return itemsc_sortRepo.Read();
		}

		public ItemsCount_SortType GetItemsCount_SortType(string Id)
		{
			return itemsc_sortRepo.Read(Id);
		}

		public void UpdateItemsCount_SortType(string Id, ItemsCount_SortType newitem)
		{
			itemsc_sortRepo.Update(Id, newitem);
		}

		public IQueryable<ItemsCount_SortType> OrderByItems(IQueryable<ItemsCount_SortType> items)
		{
			IQueryable<ItemsCount_SortType> itemsc_SortTypes = from x in items
															   orderby x.ItemsCountMin
															   select x;
			return itemsc_SortTypes;
		}

		public ItemsCount_SortType GetItemsCount_SortTypeForItemsCount(int itemscount)
		{
			ItemsCount_SortType itemsCount_SortType = (from x in GetAllItemsCount_SortType()
													   where (x.ItemsCountMin == null && x.ItemsCountMax > itemscount) || (x.ItemsCountMax == null && x.ItemsCountMin <= itemscount) || (x.ItemsCountMin <= itemscount && x.ItemsCountMax > itemscount)
													   select x).FirstOrDefault();
			return itemsCount_SortType;
		}
	}
}
