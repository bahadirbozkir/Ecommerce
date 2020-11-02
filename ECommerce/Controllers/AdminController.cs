using ECommerce.AuthorizeAttributes;
using ECommerce.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ECommerce.Models;
using ECommerce.Service.DTO;
using PagedList;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {

        IProductService _productService;
        IUserService _userService;
        public AdminController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        [AdminAuthorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        [AdminAuthorize]
        public ActionResult Products(int? page)
        {
            var pageNumber = page ?? 1;
            var tableSize = 10;

            var products = _productService.GetProducts();

            var mappedProducts = products.Select(x => new ProductModel
            {
                Barcode = x.Barcode,
                Description = x.Description,
                Price = x.Price,
                ProductCount = x.ProductCount,
                ProductId = x.ProductId,
                ProductName = x.ProductName
            }).ToPagedList(pageNumber, tableSize);

            return View(mappedProducts);
        }

        [AdminAuthorize]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult AddProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var mappedProduct = new ProductDTO
                {
                    Barcode = product.Barcode,
                    Description = product.Description,
                    Price = product.Price,
                    ProductCount = product.ProductCount,
                    UploadedFile = product.UploadedFile != null ? EncodeProductImage(product.UploadedFile) : "",
                    ProductName = product.ProductName
                };

                _productService.AddProduct(mappedProduct);
            }

            return RedirectToAction("Products");
        }

        [AdminAuthorize]
        public ActionResult UpdateProduct(int productId)
        {
            var product = _productService.GetProductById(productId);

            var mappedProduct = new ProductUpdateModel
            {
                Barcode = product.Barcode,
                Description = product.Description,
                Price = product.Price,
                ProductCount = product.ProductCount,
                OldFile = product.UploadedFile,
                ProductName = product.ProductName,
                ProductId = product.ProductId,
            };


            if (product == null)
            {
                return View("AddProduct");
            }

            return View(mappedProduct);
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult UpdateProduct(ProductUpdateModel product)
        {
            if (ModelState.IsValid)
            {
                var mappedProduct = new ProductDTO
                {
                    Barcode = product.Barcode,
                    Description = product.Description,
                    Price = product.Price,
                    ProductCount = product.ProductCount,
                    UploadedFile = product.UploadedFile != null ? EncodeProductImage(product.UploadedFile) : product.OldFile,
                    ProductName = product.ProductName,
                    ProductId = product.ProductId,
                };

                _productService.UpdateProduct(mappedProduct);
            }

            return RedirectToAction("Products");
        }

        [AdminAuthorize]
        public ActionResult DeleteProduct(int productId)
        {
            var product = _productService.GetProductById(productId);

            if (product != null)
            {
                _productService.DeleteProduct(productId);
            }

            return RedirectToAction("Products");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult DoLogin(UserDTO loginCredentials)
        {
            if (ModelState.IsValid)
            {
                var encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(loginCredentials.Username + ":" + loginCredentials.Password));
                var isAdmin = _userService.DoLogin(encoded);

                if (isAdmin)
                {
                    Session["AuthToken"] = encoded;
                    ViewBag.LoginError = "";
                    return RedirectToAction("Products");
                }
                else
                {
                    ViewBag.LoginError = "Incorrect username or password";
                    return View("Login");
                }
            }
            return View();
        }

        private string EncodeProductImage(HttpPostedFileBase file)
        {
            byte[] binaryData;
            binaryData = new Byte[file.InputStream.Length];
            long bytesRead = file.InputStream.Read(binaryData, 0, (int)file.InputStream.Length);
            file.InputStream.Close();
            string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);

            return base64String;
        }

        public FileResult ProductImage(int productId)
        {
            var product = _productService.GetProductById(productId);
            var bytes = Convert.FromBase64String(product.UploadedFile);
            return File(bytes, "image/png");
        }
    }
}