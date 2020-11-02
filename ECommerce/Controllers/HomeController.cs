using ECommerce.Models;
using ECommerce.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public ActionResult Index()
        {
            var products = _productService.GetProducts();

            if (products == null)
            {
                View();
            }

            var mappedProducts = products.Select(x => new ProductModel
            {
                Barcode = x.Barcode,
                Description = x.Description,
                Price = x.Price,
                ProductCount = x.ProductCount,
                ProductId = x.ProductId,
                ProductName = x.ProductName
            });

            return View(mappedProducts);
        }

        public FileResult ProductImage(int productId)
        {
            var product = _productService.GetProductById(productId);
            var bytes = Convert.FromBase64String(product.UploadedFile);
            return File(bytes, "image/png");
        }
    }
}