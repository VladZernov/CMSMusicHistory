using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MusicHistoryCMS.Models;
using System.Collections.Generic;
using Sakura.AspNet;

namespace MusicHistoryCMS.Controllers
{
    public class PeriodsController : Controller
    {
        private ApplicationDbContext _context;

        public PeriodsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Periods
        public IActionResult Index(int? page)
        {
            var pageSize = 10;

            IEnumerable<Period> data = _context.Period;

            var pagedData = data.ToPagedList(pageSize, page ?? 1);
            return View(pagedData);
        }

        // GET: Periods/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Period period = _context.Period.Single(m => m.ID == id);
            if (period == null)
            {
                return HttpNotFound();
            }

            return View(period);
        }

        // GET: Periods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Periods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Period period)
        {
            if (ModelState.IsValid)
            {
                _context.Period.Add(period);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(period);
        }

        // GET: Periods/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Period period = _context.Period.Single(m => m.ID == id);
            if (period == null)
            {
                return HttpNotFound();
            }
            return View(period);
        }

        // POST: Periods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Period period)
        {
            if (ModelState.IsValid)
            {
                _context.Update(period);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(period);
        }

        // GET: Periods/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Period period = _context.Period.Single(m => m.ID == id);
            if (period == null)
            {
                return HttpNotFound();
            }

            return View(period);
        }

        // POST: Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Period period = _context.Period.Single(m => m.ID == id);
            _context.Period.Remove(period);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
