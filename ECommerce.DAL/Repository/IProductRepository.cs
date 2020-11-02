using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DAL.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int productId);
        void DeleteProduct(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
    }
}
