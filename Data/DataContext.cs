using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> opt) : base(opt)
		{

		}

		public DataContext()
		{
			this.Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.
					UseLazyLoadingProxies().
						UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\DataDB.mdf;integrated security=True;MultipleActiveResultSets=True");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ProductCategory>().HasKey(cs => new { cs.ProductId, cs.CategoryId });

			modelBuilder.Entity<ProductCategory>(entity =>
			{
				entity
				.HasOne(productcateg => productcateg.Product)
				.WithMany(product => product.ProductCategories)
				.HasForeignKey(productcateg => productcateg.ProductId)
				.OnDelete(DeleteBehavior.Cascade);
				entity
				.HasOne(productcateg => productcateg.Category)
				.WithMany(categ => categ.ProductCategories)
				.HasForeignKey(productcateg => productcateg.CategoryId)
				.OnDelete(DeleteBehavior.Cascade);
			});
			modelBuilder.Entity<Price_SortType>(entity =>
			{
				entity
				.HasMany(price_sort => price_sort.Products)
				.WithOne(product => product.Price_SortType)
				.HasForeignKey(product => product.Price_SortTypeId)
				.OnDelete(DeleteBehavior.Restrict);
			});
			modelBuilder.Entity<Age_SortType>(entity =>
			{
				entity
				.HasMany(age_sort => age_sort.Products)
				.WithOne(product => product.Age_SortType)
				.HasForeignKey(product => product.Age_SortTypeId)
				.OnDelete(DeleteBehavior.Restrict);
			});
			modelBuilder.Entity<ItemsCount_SortType>(entity =>
			{
				entity
				.HasMany(itemsc_sort => itemsc_sort.Products)
				.WithOne(product => product.ItemsCount_SortType)
				.HasForeignKey(product => product.ItemsCount_SortTypeId)
				.OnDelete(DeleteBehavior.Restrict);
			});
			modelBuilder.Entity<FiguresCount_SortType>(entity =>
			{
				entity
				.HasMany(figuresc_sort => figuresc_sort.Products)
				.WithOne(product => product.FiguresCount_SortType)
				.HasForeignKey(product => product.FiguresCount_SortTypeId)
				.OnDelete(DeleteBehavior.Restrict);
			});
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategory { get; set; }
		public DbSet<Price_SortType> Price_SortTypes { get; set; }
		public DbSet<Age_SortType> Age_SortTypes { get; set; }
		public DbSet<ItemsCount_SortType> ItemsCount_SortTypes { get; set; }
		public DbSet<FiguresCount_SortType> FiguresCount_SortTypes { get; set; }
	}
}
