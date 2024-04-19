using Microsoft.AspNetCore.Mvc;

namespace WebEcommerce.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/product/{slug}")]
        public IActionResult Detail(string slug)
        {
            return View();
        }
    }
}
