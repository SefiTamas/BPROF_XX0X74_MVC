using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class FiguresCount_SortTypeLogic
    {
		IRepository<FiguresCount_SortType> figuresc_sortRepo;

		public FiguresCount_SortTypeLogic(IRepository<FiguresCount_SortType> figuresc_sortRepo)
		{
			this.figuresc_sortRepo = figuresc_sortRepo;
		}

		public void AddFiguresCount_SortType(FiguresCount_SortType item)
		{
			this.figuresc_sortRepo.Add(item);
		}

		public void DeleteFiguresCount_SortType(string Id)
		{
			this.figuresc_sortRepo.Delete(Id);
		}

		public IQueryable<FiguresCount_SortType> GetAllFiguresCount_SortType()
		{
			return figuresc_sortRepo.Read();
		}

		public FiguresCount_SortType GetFiguresCount_SortType(string Id)
		{
			return figuresc_sortRepo.Read(Id);
		}

		public void UpdateFiguresCount_SortType(string Id, FiguresCount_SortType newitem)
		{
			figuresc_sortRepo.Update(Id, newitem);
		}

		public IQueryable<FiguresCount_SortType> OrderByFigures(IQueryable<FiguresCount_SortType> figures)
		{
			IQueryable<FiguresCount_SortType> figuresc_SortTypes = from x in figures
																   orderby x.FiguresCountMin
																   select x;
			return figuresc_SortTypes;
		}

		public FiguresCount_SortType GetFiguresCount_SortTypeForFiguresCount(int figurescount)
		{
			FiguresCount_SortType figuresCount_SortType = (from x in GetAllFiguresCount_SortType()
														   where x.FiguresCount == figurescount || (x.FiguresCountMin == null && x.FiguresCountMax >= figurescount) || (x.FiguresCountMax == null && x.FiguresCountMin <= figurescount) || (x.FiguresCountMin <= figurescount && x.FiguresCountMax >= figurescount)
														   select x).FirstOrDefault();
			return figuresCount_SortType;
		}
	}
}
