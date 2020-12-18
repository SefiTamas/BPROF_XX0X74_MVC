using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Price_SortType
    {
        [Key]
        public string Id { get; set; }

        [Range(0, 10000000)]
        public int? PriceMin { get; set; }

        [Range(0, 10000000)]
        public int? PriceMax { get; set; }

        [NotMapped]
        public virtual ICollection<Product> Products { get; set; }

        public Price_SortType()
        {
            Products = new List<Product>();
        }
    }
}
