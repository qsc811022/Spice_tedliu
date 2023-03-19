﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Spice_tedliu.Data;
using Spice_tedliu.Models.ViewModels;

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

        [TempData]
        public string StatusMessge { get;set;}

        public SubCategoryController(ApplicationDbContext db)
        {
            _db=db;
        }

        public async Task<IActionResult> Index()
        {
            var subCategories = await _db.SubCategroy.Include(s=>s.Category).ToListAsync();
            return View(subCategories);
        }

        //get-create
        public async Task<IActionResult> Create()
        {
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Categroy.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoryList = await _db.SubCategroy.OrderBy(p=>p.Name).Select(p=>p.Name).Distinct().ToListAsync()
            };
           return View(model);
        }

        //post-create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubCategoryExists = _db.SubCategroy.Include(s=>s.Category).Where(s=>s.Name==model.SubCategory.Name && s.Category.Id==model.SubCategory.CategoryId);


                if(doesSubCategoryExists.Count()>0)
                {
                    //Error
                    StatusMessge="Error: Sub Category exists under"+doesSubCategoryExists.First().Category.Name+"category. Please use anthoer name";

                }
                else
                {
                    _db.SubCategroy.Add(model.SubCategory);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));


                }
            }
            SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Categroy.ToListAsync(),
                SubCategory = model.SubCategory,
                SubCategoryList = await _db.SubCategroy.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync()
            };
            return View(modelVM);

        }



    }
}
