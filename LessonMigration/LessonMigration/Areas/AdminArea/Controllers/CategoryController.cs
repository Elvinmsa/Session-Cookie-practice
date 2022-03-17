using LessonMigration.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LessonMigration.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Where(m =>!m.IsDeleted).ToListAsync();
            return View(categories);
        }
        public IActionResult Detail(int id)
        {
            var category = _context.Categories.FirstOrDefault(m => m.Id == id);
            return View(category);
           
        }
        public IActionResult Edit(int id)
        {
            return Json(new
            {
                action = "Edit",
                Id = id
            });
        }
        public IActionResult Delete(int id)
        {
            return Json(new
            {
                action = "Delete",
                Id = id
            });
        }
    }
}
