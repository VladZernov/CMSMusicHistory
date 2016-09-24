using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MusicHistoryCMS.Models;
using System.Collections.Generic;
using Sakura.AspNet;
using System.Data;

namespace MusicHistoryCMS.Controllers
{
    public class ComposersController : Controller
    {
        private ApplicationDbContext _context;

        public ComposersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Composers
        public IActionResult Index(int? genreId, int? issueId, int? periodId, int? page)
        {

            var pageSize = 10;

            IQueryable<Composer> data;

            if (genreId != null)
            {
                data =  (from composer in _context.Composer
                         join composition in _context.Composition on composer.ID equals composition.ComposerID
                         join genre in _context.Genre on composition.GenreID equals genre.ID
                         join period in _context.Period on composer.PeriodID equals period.ID
                         where genre.ID == genreId
                         select new Composer {
                             BornYear = composer.BornYear,
                             DieYear = composer.DieYear,
                             ID = composer.ID,
                             Period = period,
                             Name = composer.Name,
                             PeriodID = composer.PeriodID
                         })
                         .Distinct()
                         .OrderBy(id => id);
            }

            else if (periodId != null)
                data = _context.Composer.Include(c => c.Period).Where(c => c.Period.ID == periodId);
            else
                data = _context.Composer.Include(c => c.Period);
            var pagedData = data.ToPagedList(pageSize, page ?? 1);
            return View(pagedData);
        }

        // GET: Composers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Composer composer = _context.Composer.Single(m => m.ID == id);
            if (composer == null)
            {
                return HttpNotFound();
            }

            return View(composer);
        }

        // GET: Composers/Create
        public IActionResult Create()
        {
            ViewData["PeriodID"] = new SelectList(_context.Period, "ID", "Name");
            return View();
        }

        // POST: Composers/Create
        [HttpPost]
        public IActionResult Create(Composer composer)
        {
                _context.Composer.Add(composer);
                _context.SaveChanges();
                return View();
        }

        // GET: Composers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Composer composer = _context.Composer.Single(m => m.ID == id);
            if (composer == null)
            {
                return HttpNotFound();
            }
            ViewData["PeriodID"] = new SelectList(_context.Period, "ID", "Name", composer.PeriodID);
            return View(composer);
        }

        // POST: Composers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Composer composer)
        {
            if (ModelState.IsValid)
            {
                _context.Update(composer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["PeriodID"] = new SelectList(_context.Period, "ID", "Name", composer.PeriodID);
            return View(composer);
        }

        // GET: Composers/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Composer composer = _context.Composer.Single(m => m.ID == id);
            if (composer == null)
            {
                return HttpNotFound();
            }

            return View(composer);
        }

        // POST: Composers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Composer composer = _context.Composer.Single(m => m.ID == id);
            _context.Composer.Remove(composer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
