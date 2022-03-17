using LessonMigration.Data;
using LessonMigration.Models;
using LessonMigration.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonMigration.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("name", "Esger");
            Response.Cookies.Append("surname", "Esgerov", new CookieOptions { MaxAge = TimeSpan.FromMinutes(5) });

            List<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderDetail detail = await _context.SliderDetails.FirstOrDefaultAsync();
            List<Category> categories = await _context.Categories.Where(c=>c.IsDeleted == false).ToListAsync();
            List<Product> products = await _context.Products.Where(p=>p.IsDeleted == false)
                .Include(m => m.Category)
                .Include(m => m.Images)
                .OrderByDescending(m=>m.Id)
                .Take(8)
                .ToListAsync();

            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Detail = detail,
                Categories = categories,
                Products = products
            };

            return View(homeVM);
        }

        public IActionResult Test()
        {
            var session = HttpContext.Session.GetString("name");
            var cookie = Request.Cookies["surname"];
            return Json("User name" + session + "User surname" + cookie);
        }
    }
}
