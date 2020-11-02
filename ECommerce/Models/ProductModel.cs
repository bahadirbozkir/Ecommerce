using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ECommerce.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Barcode { get; set; }

        public decimal Price { get; set; }
        public HttpPostedFileBase UploadedFile { get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
    }
}