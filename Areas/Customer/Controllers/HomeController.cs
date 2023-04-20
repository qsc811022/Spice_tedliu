using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Spice_tedliu.Data;
using Spice_tedliu.Models;
using Spice_tedliu.Models.ViewModels;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Spice_tedliu.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db=db;
        }


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> Index()
        {
            IndexViewModel IndexVM =new IndexViewModel()
            {
                MenuItem = await _db.MenuItem.Include(m=>m.Category).Include(m=>m.SubCategory).ToListAsync(),
                Category = await _db.Categroy.ToListAsync(),
                Coupon = await _db.Coupon.Where(c=>c.IsActive==true).ToListAsync()
            };
            return View(IndexVM);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var menuItemFromDb = await _db.MenuItem.Include(m=>m.Category).Include(m=>m.SubCategory).Where(m=>m.Id==id).FirstOrDefaultAsync();

            ShoppingCart cartObj=new ShoppingCart()
            {
                MenuItem=menuItemFromDb,
                MenuItemId=menuItemFromDb.Id
            };

            return View(cartObj);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
