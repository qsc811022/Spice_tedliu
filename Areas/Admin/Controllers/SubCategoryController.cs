using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Spice_tedliu.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice_tedliu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubCategoryController(ApplicationDbContext db)
        {
            _db=db;
        }

        public async Task<IActionResult> Index()
        {
            var subCategories = await _db.SubCategroy.Include(s=>s.Category).ToListAsync();
            return View(subCategories);
        }
    }
}
