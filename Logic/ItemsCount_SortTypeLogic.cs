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
	}
}
