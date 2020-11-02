using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public string UploadedFile { get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
    }
}
