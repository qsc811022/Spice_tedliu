using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Spice_tedliu.Data;
using Spice_tedliu.Models;
using Spice_tedliu.Models.ViewModels;
using Spice_tedliu.Utility;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Spice_tedliu.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
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
                SubCategoryList = await _db.SubCategroy.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                StatusMessage=StatusMessge
            };
            return View(modelVM);

        }

        [ActionName("GetSubCategory")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();

            subCategories = await (from SubCategory in _db.SubCategroy
                             where SubCategory.CategoryId == id
                             select SubCategory).ToListAsync();
            return Json(new SelectList(subCategories,"Id","Name"));

        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _db.SubCategroy.SingleOrDefaultAsync(m=>m.Id==id);


            if (subCategory==null)
            {
                return NotFound();
            }

            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Categroy.ToListAsync(),
                SubCategory = subCategory,
                SubCategoryList = await _db.SubCategroy.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };
            return View(model);
        }

        //post-create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubCategoryExists = _db.SubCategroy.Include(s => s.Category).Where(s => s.Name == model.SubCategory.Name && s.Category.Id == model.SubCategory.CategoryId);


                if (doesSubCategoryExists.Count() > 0)
                {
                    //Error
                    StatusMessge = "Error: Sub Category exists under" + doesSubCategoryExists.First().Category.Name + "category. Please use anthoer name";

                }
                else
                {
                    var subCateFromDb = await _db.SubCategroy.FindAsync(model.SubCategory.Id);
                    subCateFromDb.Name=model.SubCategory.Name;
                    //_db.SubCategroy.Add(model.SubCategory);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));


                }
            }
            SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Categroy.ToListAsync(),
                SubCategory = model.SubCategory,
                SubCategoryList = await _db.SubCategroy.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                StatusMessage = StatusMessge
            };

            /*modelVM.SubCategory.Id=id*/;
            return View(modelVM);

        }


        //GET Details
        public async Task<IActionResult> Detials(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subCategory = await _db.SubCategroy.Include(s => s.Category).SingleOrDefaultAsync(m => m.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subCategory = await _db.SubCategroy.Include(s => s.Category).SingleOrDefaultAsync(m => m.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _db.SubCategroy.SingleOrDefaultAsync(m => m.Id == id);
            _db.SubCategroy.Remove(subCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }


}

