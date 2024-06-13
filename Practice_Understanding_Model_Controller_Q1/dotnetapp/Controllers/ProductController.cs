using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> _productList = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 999.99m, Quantity = 10, Manufacturer = "Tech Corp" },
            new Product { Id = 2, Name = "Chair", Category = "Furniture", Price = 49.99m, Quantity = 50, Manufacturer = "Furniture Inc." },
            new Product { Id = 3, Name = "Smartphone", Category = "Electronics", Price = 699.99m, Quantity = 25, Manufacturer = "Mobile World" }
        };

        public IActionResult Index()
        {
            return View(_productList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Assign a simple incremental ID
                product.Id = _productList.Count + 1;

                // Add the product to the static list
                _productList.Add(product);

                // Redirect to the product list or another action
                return RedirectToAction("Index");
            }

            // If the model state is not valid, return to the create view with validation errors
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productList.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var existingProduct = _productList.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the existing product
                existingProduct.Name = product.Name;
                existingProduct.Category = product.Category;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
                existingProduct.Manufacturer = product.Manufacturer;

                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productList.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productList.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                // Remove the product from the static list
                _productList.Remove(product);
            }

            return RedirectToAction("Index");
        }
    }
}
