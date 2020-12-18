using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Age_SortType
    {
        [Key]
        public string Id { get; set; }

        [Range(1, 99)]
        public int? RecommendedAge { get; set; }

        [Range(1, 99)]
        public int? RecommendedAgeMin { get; set; }

        [Range(1, 99)]
        public int? RecommendedAgeMax { get; set; }

        [NotMapped]
        public virtual ICollection<Product> Products { get; set; }

        public Age_SortType()
        {
            Products = new List<Product>();
        }
    }
}
