using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnviroment;

        [BindProperty]
        public MenuItemViewModel MenuItemVM { get;set;}

        public MenuItemController(ApplicationDbContext db,IHostingEnvironment hostingEnvironment)
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




    }
}
