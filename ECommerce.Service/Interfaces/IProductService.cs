using ECommerce.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetProducts();
        ProductDTO GetProductById(int productId);
        void DeleteProduct(int productId);
        void AddProduct(ProductDTO product);
        void UpdateProduct(ProductDTO product);
    }
}
