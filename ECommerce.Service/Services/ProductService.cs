using ECommerce.DAL.Repository;
using ECommerce.Service.DTO;
using ECommerce.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public void AddProduct(ProductDTO product)
        {
            //automapper can be used for more consistent structure
            var mappedProduct = new Product
            {
                Barcode = product.Barcode,
                Description = product.Description,
                Price = product.Price,
                ProductCount = product.ProductCount,
                ProductId = product.ProductId,
                ProductImages = product.UploadedFile,
                ProductName = product.ProductName
            };

            _productRepository.AddProduct(mappedProduct);
        }

        public void DeleteProduct(int productId)
        {
            _productRepository.DeleteProduct(productId);
        }

        public ProductDTO GetProductById(int productId)
        {
            var product = _productRepository.GetProductById(productId);

            //automapper can be used for more consistent structure
            var mappedProduct = new ProductDTO
            {
                Barcode = product.Barcode,
                Description = product.Description,
                Price = product.Price,
                ProductCount = product.ProductCount,
                ProductId = product.ProductId,
                UploadedFile = product.ProductImages,
                ProductName = product.ProductName
            };

            return mappedProduct;
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var products =_productRepository.GetProducts();

            //automapper can be used for more consistent structure
            var mappedProducts = products.Select(x => new ProductDTO
            {
                Barcode = x.Barcode,
                Description = x.Description,
                Price = x.Price,
                ProductCount = x.ProductCount,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UploadedFile = x.ProductImages
            }).ToList();

            return mappedProducts;
        }

        public void UpdateProduct(ProductDTO product)
        {
            //automapper can be used for more consistent structure
            var mappedProduct = new Product
            {
                Barcode = product.Barcode,
                Description = product.Description,
                Price = product.Price,
                ProductCount = product.ProductCount,
                ProductId = product.ProductId,
                ProductImages = product.UploadedFile,
                ProductName = product.ProductName
            };

            _productRepository.UpdateProduct(mappedProduct);
        }
    }
}
