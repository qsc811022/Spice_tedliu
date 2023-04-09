using Microsoft.AspNetCore.Mvc;

using Spice_tedliu.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice_tedliu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db=db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
