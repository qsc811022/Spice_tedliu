using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Spice_tedliu.Data;
using Spice_tedliu.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice_tedliu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db=db;
        }

        //get
        public async Task<IActionResult> Index()
        {
            return View(await _db.Categroy.ToListAsync());
        }

        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST -CAERTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.Categroy.Add(category);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(category);
        }
    }
}
