using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;
using WebEcommerce.Areas.Admin.Services;
using WebEcommerce.Models;
using WebEcommerce.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IUploadFile _uploadFile;
        public ProductCategoryController(ApplicationDbContext context, IUploadFile uploadFile)
        {
            _context = context;
            _uploadFile = uploadFile;
        }

        public IActionResult Index()
        {
            ViewData["Data"] = _context.ProductCategories.ToList();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Store(ProductCategory model, IFormFile? file)
        {
            if (file != null)
            {
                string imageUrl = await _uploadFile.UploadSingle(file);
                model.Image = imageUrl;
            }

            var name = !string.IsNullOrEmpty(model.Slug) ? model.Slug : model.Name;
            model.Slug = SlugifyService.Instance.GenerateSlug(name);

            _context.ProductCategories.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [Route("admin/productCategory/edit/{id}")]
        public IActionResult Edit(int id)
        {
            ProductCategory? data = _context.ProductCategories.FirstOrDefault(v => v.Id == id);
            if(data == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Data"] = data;
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductCategory model, IFormFile? file)
        {
            ProductCategory? data = _context.ProductCategories.FirstOrDefault(v =>v.Id == model.Id);
            if(data != null)
            {
                data.Name = model.Name;
                data.Description = model.Description;

                var name = !string.IsNullOrEmpty(model.Slug) ? model.Slug : model.Name;
                data.Slug = SlugifyService.Instance.GenerateSlug(name);

                if (file != null)
                {
                    string imageUrl = await _uploadFile.UploadSingle(file);
                    data.Image = imageUrl;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            ProductCategory? data = _context.ProductCategories.Find(id);
            if (data != null)
            {
                _context.ProductCategories.Remove(data);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
