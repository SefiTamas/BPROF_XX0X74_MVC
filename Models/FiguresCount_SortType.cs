using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class FiguresCount_SortType
    {
        [Key]
        public string Id { get; set; }

        [Range(0, 1000000)]
        public int? FiguresCount { get; set; }

        [Range(0, 1000000)]
        public int? FiguresCountMin { get; set; }

        [Range(0, 1000000)]
        public int? FiguresCountMax { get; set; }

        [NotMapped]
        public virtual ICollection<Product> Products { get; set; }

        public FiguresCount_SortType()
        {
            Products = new List<Product>();
        }
    }
}
