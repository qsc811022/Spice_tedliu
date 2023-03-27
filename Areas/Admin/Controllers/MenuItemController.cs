using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Spice_tedliu.Data;
using Spice_tedliu.Models.ViewModels;
using Spice_tedliu.Utility;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Spice_tedliu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnviroment;

        [BindProperty]
        public MenuItemViewModel MenuItemVM { get;set;}

        public MenuItemController(ApplicationDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _db=db;
            _hostingEnviroment=hostingEnvironment;
            MenuItemVM = new MenuItemViewModel()
            {
                Category=_db.Categroy,
                MenuItem=new Models.MenuItem()
            };

        }

        public async Task<IActionResult> Index()
        {
            var menuItems = await _db.MenuItem.Include(m=>m.Category).Include(m=>m.SubCategory).ToListAsync();
            return View(menuItems);
        }


        //GET- CREATE

        public IActionResult Create()
        {

            return View(MenuItemVM);
        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            MenuItemVM.MenuItem.SubCategoryId=Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if (!ModelState.IsValid)
            {
                return View(MenuItemVM);
            }

            _db.MenuItem.Add(MenuItemVM.MenuItem);
            await _db.SaveChangesAsync();

            //Work on the image saving section
            string webRootPath = _hostingEnviroment.WebRootPath;
            var files=HttpContext.Request.Form.Files;

            var menuItemFromDb = await _db.MenuItem.FindAsync(MenuItemVM.MenuItem.Id);

            if (files.Count>0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath,"images");
                var extension=Path.GetExtension(files[0].FileName);
                using(var filesStream = new FileStream(Path.Combine(uploads, MenuItemVM.MenuItem.Id + extension), FileMode.Create)) 
                {
                    files[0].CopyTo(filesStream);
                        
                }
                menuItemFromDb.Image=@"\images\"+MenuItemVM.MenuItem.Id+extension;


            }
            else
            {
                //no files was uploaded, so use default
                var uploads=Path.Combine(webRootPath,@"images\"+SD.DefaultFoodImage);
                System.IO.File.Copy(uploads,webRootPath+@"\images"+MenuItemVM.MenuItem.Id+".png");
                menuItemFromDb.Image=@"\images\"+MenuItemVM.MenuItem.Id+".png";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }




    }
}
