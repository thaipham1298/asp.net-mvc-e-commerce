﻿using Microsoft.AspNetCore.Mvc;

namespace WebEcommerce.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
