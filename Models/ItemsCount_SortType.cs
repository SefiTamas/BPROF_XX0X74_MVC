using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Models
{
    public class ItemsCount_SortType
    {
        [Key]
        public string Id { get; set; }

        [Range(1, 1000000)]
        public int? ItemsCountMin { get; set; }

        [Range(1, 1000000)]
        public int? ItemsCountMax { get; set; }

        [NotMapped]
        public virtual ICollection<Product> Products { get; set; }

        public ItemsCount_SortType()
        {
            Products = new List<Product>();
        }

        public override string ToString()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is ItemsCount_SortType)
            {
                ItemsCount_SortType other = obj as ItemsCount_SortType;
                return this.Id == other.Id &&
                    this.ItemsCountMin == other.ItemsCountMin &&
                    this.ItemsCountMax == other.ItemsCountMax &&
                    this.Products.All(other.Products.Contains) &&
                    this.Products.Count == other.Products.Count;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
