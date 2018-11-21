using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Egret.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator")]
    public class AccessGroupsController : BaseController
    {
        public AccessGroupsController(EgretContext context, ILogger<ItemsController> logger)
            :base(context)
        {

        }

        [HttpGet]
        public ViewResult Index()
        {
            var egretContext = Context.AccessGroups.OrderBy(x => x.Name);
            return View(egretContext.ToList());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ViewResult Index(List<Role> roles)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        for (int i = 0; i < roles.Count(); i++)
        //        {
        //            Context.Update(roles[i]);
        //        }
        //        Context.SaveChanges();
        //        TempData["SuccessMessage"] = "Save Complete";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(roles);

        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AccessGroup group)
        {
            if (ModelState.IsValid)
            {
                Context.AccessGroups.Add(group);
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Access Group Added";
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Delete(string id)
        {
            
        }

        [HttpGet]
        public void Edit(int id)
        {
            //var model = Context.AccessGroups.Where(x => x.Id == id)
            //    .Include

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Edit(RoleModificationModel model)
        {
            
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}