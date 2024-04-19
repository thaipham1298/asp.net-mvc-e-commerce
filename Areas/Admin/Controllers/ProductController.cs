using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebEcommerce.Areas.Admin.Services;
using WebEcommerce.Models;
using WebEcommerce.Services;

namespace WebEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploadFile _uploadFile;

        public ProductController(ApplicationDbContext context, IUploadFile uploadfile)
        {
            _context = context;
            _uploadFile = uploadfile;
        }

        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            ViewData["Products"] = products;
            return View();
        }

        public IActionResult Create()
        {
            List<ProductCategory> categories = _context.ProductCategories.ToList();

            ViewData["categories"] = categories;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Store(Product model, IFormFile? file)
        {
            if (file != null)
            {
                string imageURL = await _uploadFile.UploadSingle(file);
                model.Image = imageURL;
            }

            var name = !string.IsNullOrEmpty(model.Slug) ? model.Slug : model.Name;
            model.Slug = SlugifyService.Instance.GenerateSlug(name);

            string[] productCategoryIds = Request.Form["Categories[]"].ToString().Split(",");
            List<ProductCategory> productCategories = _context.ProductCategories.Where(v => productCategoryIds.Contains(v.Id.ToString())).ToList();
            model.Categories = productCategories;

            _context.Products.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/admin/product/edit/{id}")]
        public IActionResult Edit(int id)
        {
            Product? product = _context.Products.Include(v => v.Categories).FirstOrDefault(v => v.Id == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            List<ProductCategory> categories = _context.ProductCategories.ToList();

            ViewData["categories"] = categories;
            ViewData["data"] = product;

            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product model, IFormFile? file)
        {
            Product? product = _context.Products.Include(v => v.Categories).FirstOrDefault(v => v.Id == model.Id);
            if (product != null)
            {
                if (file != null)
                {
                    string imageURL = await _uploadFile.UploadSingle(file);
                    product.Image = imageURL;
                }

                var name = !string.IsNullOrEmpty(model.Slug) ? model.Slug : model.Name;
                product.Slug = SlugifyService.Instance.GenerateSlug(name);

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.CompareAtPrice = model.CompareAtPrice;

                string[] productCategoryIds = Request.Form["Categories[]"].ToString().Split(",");
                List<ProductCategory> productCategories = _context.ProductCategories.Where(v => productCategoryIds.Contains(v.Id.ToString())).ToList();

                product.Categories.Clear();
                product.Categories = productCategories;

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