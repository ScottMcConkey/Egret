using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egret_Dev.EF;

namespace Egret_Dev.Controllers
{
    public class ProjectItemsController : Controller
    {
        //private readonly EgretContext _context;

        //public ProjectItemsController(EgretContext context)
        //{
        //    _context = context;    
        //}

        // GET: ProjectItems
        public IActionResult Index()
        {
            return View();
            //var egretContext = _context.ProjectItems.Include(p => p.Project);
            //return View(await egretContext.ToListAsync());
        }

        // GET: ProjectItems/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    var projectItem = await _context.ProjectItems
        //        .Include(p => p.Project)
        //        .SingleOrDefaultAsync(m => m.Projectid == id);
        //    if (projectItem == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    return View(projectItem);
        //}
        //
        // GET: ProjectItems/Create
        //public IActionResult Create()
        //{
        //    ViewData["Projectid"] = new SelectList(_context.Projects, "Projectid", "Projectid");
        //    return View();
        //}

        // POST: ProjectItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Projectid,Itemid")] ProjectItem projectItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(projectItem);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["Projectid"] = new SelectList(_context.Projects, "Projectid", "Projectid", projectItem.Projectid);
        //    return View(projectItem);
        //}
        //
        // GET: ProjectItems/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    var projectItem = await _context.ProjectItems.SingleOrDefaultAsync(m => m.Projectid == id);
        //    if (projectItem == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["Projectid"] = new SelectList(_context.Projects, "Projectid", "Projectid", projectItem.Projectid);
        //    return View(projectItem);
        //}

        // POST: ProjectItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Projectid,Itemid")] ProjectItem projectItem)
        //{
        //    if (id != projectItem.Projectid)
        //    {
        //        return NotFound();
        //    }
        //
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(projectItem);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProjectItemExists(projectItem.Projectid))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["Projectid"] = new SelectList(_context.Projects, "Projectid", "Projectid", projectItem.Projectid);
        //    return View(projectItem);
        //}

        // GET: ProjectItems/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    var projectItem = await _context.ProjectItems
        //        .Include(p => p.Project)
        //        .SingleOrDefaultAsync(m => m.Projectid == id);
        //    if (projectItem == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    return View(projectItem);
        //}

        // POST: ProjectItems/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var projectItem = await _context.ProjectItems.SingleOrDefaultAsync(m => m.Projectid == id);
        //    _context.ProjectItems.Remove(projectItem);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //private bool ProjectItemExists(int id)
        //{
        //    return _context.ProjectItems.Any(e => e.Projectid == id);
        //}
    }
}
