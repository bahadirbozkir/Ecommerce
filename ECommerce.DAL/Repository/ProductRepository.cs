using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ECommerce.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {

        private List<Product> _products = new List<Product>();

        public ProductRepository()
        {
            AddProduct(new Product { ProductId = 1, Barcode = Guid.NewGuid().ToString(), Description = "Product1", Price = 100, ProductCount = 40, ProductImages = EncodeLocalTestImage(""), ProductName = "Product1" });
            AddProduct(new Product { ProductId = 2, Barcode = Guid.NewGuid().ToString(), Description = "Product2", Price = 200, ProductCount = 50, ProductImages = EncodeLocalTestImage(""), ProductName = "Product2" });
            AddProduct(new Product { ProductId = 3, Barcode = Guid.NewGuid().ToString(), Description = "Product3", Price = 300, ProductCount = 60, ProductImages = EncodeLocalTestImage(""), ProductName = "Product3" });
            AddProduct(new Product { ProductId = 4, Barcode = Guid.NewGuid().ToString(), Description = "Product4", Price = 400, ProductCount = 70, ProductImages = EncodeLocalTestImage(""), ProductName = "Product4" });
            AddProduct(new Product { ProductId = 5, Barcode = Guid.NewGuid().ToString(), Description = "Product5", Price = 500, ProductCount = 80, ProductImages = EncodeLocalTestImage(""), ProductName = "Product5" });
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product is null");

            product.ProductId = _products.Count + 1;

            _products.Add(product);
        }

        public void DeleteProduct(int productId)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);

            if (product == null)
                throw new ArgumentNullException("product is not exists");


            _products.Remove(product);
        }

        public Product GetProductById(int productId)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);

            if (product == null)
                throw new ArgumentNullException("product is not exists");

            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products.OrderBy(x => x.ProductId).ToList();
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product is null");

            var existingProduct = _products.FirstOrDefault(x => x.ProductId == product.ProductId);

            if (product == null)
                throw new ArgumentNullException("product is not exists");

            existingProduct.ProductId = product.ProductId;
            existingProduct.ProductCount = product.ProductCount;
            existingProduct.Price = product.Price;
            existingProduct.ProductImages = product.ProductImages;
            existingProduct.ProductName = product.ProductName;
            existingProduct.Barcode = product.Barcode;
            existingProduct.Description = product.Description;
        }

        private string EncodeLocalTestImage(string path)
        {
            byte[] binaryData;
            
            binaryData = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "image/testimage.png"));
            
            string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);

            return base64String;
        }
    }
}
