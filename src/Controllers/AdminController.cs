using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Egret.DataAccess;

namespace Egret.Controllers
{
    public class AdminController : Controller
    {
        //private readonly EgretContext _context;
        //
        //public AdminController(EgretContext context)
        //{
        //    _context = context;
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}