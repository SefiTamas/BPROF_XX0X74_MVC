using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    }
}
