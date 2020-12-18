using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
