using Microsoft.AspNetCore.Mvc;
using WebEcommerce.Models;
using WebEcommerce.Services;

namespace WebEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            ViewData["Products"]=products;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Store(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/admin/product/edit/{id}")]
        public IActionResult Edit(int id)
        {
            Product? product = _context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["data"] = product;

            return View("Create");
        }

        [HttpPost]
        public IActionResult Update(Product model)
        {
            Product? product = _context.Products.Find(model.Id);
            if (product != null)
            {
                product.Name = model.Name;
                product.Slug = model.Slug;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Promotion = model.Promotion;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Product? product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
