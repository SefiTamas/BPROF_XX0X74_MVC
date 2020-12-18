using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string ProductLine { get; set; }

        [Range(1, 1000000)]
        public int ItemsCount { get; set; }

        [Range(0, 1000000)]
        public int FiguresCount { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(0, 10000000)]
        public int Price { get; set; }

        [StringLength(200)]
        public string PictureName_thumbnail { get; set; }

        [StringLength(200)]
        public string PictureName_big { get; set; }
    }
}
