using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [NotMapped]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        [StringLength(200)]
        public string ProductLine { get; set; }

        public string Age_SortTypeId { get; set; }

        [NotMapped]
        public virtual Age_SortType Age_SortType { get; set; }

        [Range(1, 1000000)]
        public int ItemsCount { get; set; }

        public string ItemsCount_SortTypeId { get; set; }

        [NotMapped]
        public virtual ItemsCount_SortType ItemsCount_SortType { get; set; }

        [Range(0, 1000000)]
        public int FiguresCount { get; set; }

        public string FiguresCount_SortTypeId { get; set; }

        [NotMapped]
        public virtual FiguresCount_SortType FiguresCount_SortType { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(0, 10000000)]
        public int Price { get; set; }

        public string Price_SortTypeId { get; set; }

        [NotMapped]
        public virtual Price_SortType Price_SortType { get; set; }

        [StringLength(200)]
        public string PictureName_thumbnail { get; set; }

        [StringLength(200)]
        public string PictureName_big { get; set; }

        public Product()
        {
            ProductCategories = new List<ProductCategory>();
        }

        public override string ToString()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is Product)
            {
                Product other = obj as Product;
                return this.Id == other.Id &&
                    this.Name == other.Name &&
                    this.ProductLine == other.ProductLine &&
                    this.ProductCategories.All(other.ProductCategories.Contains) &&
                    this.Age_SortTypeId == other.Age_SortTypeId &&
                    this.Age_SortType == other.Age_SortType &&
                    this.ItemsCount == other.ItemsCount &&
                    this.ItemsCount_SortTypeId == other.ItemsCount_SortTypeId &&
                    this.ItemsCount_SortType == other.ItemsCount_SortType &&
                    this.FiguresCount == other.FiguresCount &&
                    this.FiguresCount_SortTypeId == other.FiguresCount_SortTypeId &&
                    this.FiguresCount_SortType == other.FiguresCount_SortType &&
                    this.Description == other.Description &&
                    this.Price == other.Price &&
                    this.Price_SortTypeId == other.Price_SortTypeId &&
                    this.Price_SortType == other.Price_SortType &&
                    this.PictureName_thumbnail == other.PictureName_thumbnail &&
                    this.PictureName_big == other.PictureName_big;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
