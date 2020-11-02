using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.DAL.Repository
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Barcode { get; set; }


        public decimal Price { get; set; }
        public string ProductImages { get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
    }
}
