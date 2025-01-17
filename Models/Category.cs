﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [NotMapped]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public Category()
        {
            ProductCategories = new List<ProductCategory>();
        }

        public override string ToString()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is Category)
            {
                Category other = obj as Category;
                return this.Id == other.Id &&
                    this.Name == other.Name &&
                    this.ProductCategories.All(other.ProductCategories.Contains) &&
                    this.ProductCategories.Count == other.ProductCategories.Count;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
