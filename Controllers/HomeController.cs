using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebEcommerce.Models;
using WebEcommerce.Services;

namespace WebEcommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context=context;
            _logger = logger;
        }
        

        public IActionResult Index()
        {
            List<Product> featuredProducts = _context.Products.Where(v => v.IsFeatured && v.Status)
                .Include(p => p.Categories.Take(1))
                .Take(8).ToList();
            List<Product> newProducts = _context.Products.OrderByDescending(v => v.CreatedAt)
                .Include(p => p.Categories.Take(1))
                .Where(v => v.Status).Take(8).ToList();
            ViewData["featuredProducts"] = featuredProducts;
            ViewData["newProducts"] = newProducts;

            List<ProductCategory> productCategories = _context.ProductCategories.Where(v => v.Status).ToList();
            ViewData["productCategories"] = productCategories;
            return View();
        }

      
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
