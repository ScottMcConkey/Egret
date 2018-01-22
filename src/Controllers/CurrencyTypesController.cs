using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Npgsql;
using Npgsql.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Controllers
{
    public class CurrencyTypesController : Controller
    {
        private readonly EgretContext _context;

        public CurrencyTypesController(EgretContext context)
        {
            _context = context;
        }

        // GET: Unit
        public IActionResult Index()
        {
            var egretContext = _context.CurrencyTypes.OrderBy(x => x.Sortorder);
            return View(egretContext.ToList());
        }

        // POST: Unit/Index/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<CurrencyType> types)
        {
            //List<string> msg = new List<string>();

            if (ModelState.IsValid)
            {
                for (int i = 0; i < types.Count; i++)
                {
                    try
                    {
                        _context.Update(types[i]);
                        _context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        // innerException = e.InnerException as PostgresException;

                        //if (innerException != null && innerException.ErrorCode == -2147467259)
                        //{
                        //    //return View("Test", "There's a problem");
                        //    return View("Test", innerException.SqlState.ToString());
                        //}
                        //else
                        //{
                        //    return View("Test", innerException.Code.ToString());
                        //    //2147467259
                        //}

                        //msg.Append(e.InnerException.ToString());
                        //msg.Append(e.InnerException.Data.ToString());
                        //ViewData["Error"] = e.Message;
                        //return View("Test", e.InnerException.Message.ToString());
                        // 23505: duplicate key value violates unique constraint "currency_types_sort_key" 
                        //return View("Test", e.InnerException.Source.ToString());
                        // Npgsql
                        //return View("Test", e.Source.ToString());
                        // Microsoft.EntityFrameworkCore.Relational 

                        //return View("Test", e.InnerException.Message.ToString());

                        //RedirectToAction("Test", "CurrencyTypesController");
                    }
                    //catch (Exception e)
                    //{
                    //    return View("Test", e.InnerException.ToString());
                    //}
                }
            }
            
            return RedirectToAction(nameof(Index));
        }

        public string Test(string msg)
        {
            return msg;
        }
    }
}