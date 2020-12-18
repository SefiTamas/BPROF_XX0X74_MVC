using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class ProductCategory
    {
        [Key]
        public string Id { get; set; }

        public string ProductId { get; set; }

        [NotMapped]
        public virtual Product Product { get; set; }


        public string CategoryId { get; set; }

        [NotMapped]
        public virtual Category Category { get; set; }
    }
}
