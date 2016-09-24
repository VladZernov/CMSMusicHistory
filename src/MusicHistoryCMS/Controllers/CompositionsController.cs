using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MusicHistoryCMS.Models;
using System.Collections.Generic;
using Sakura.AspNet;

namespace MusicHistoryCMS.Controllers
{
    public class CompositionsController : Controller
    {
        private ApplicationDbContext _context;

        public CompositionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Compositions
        public IActionResult Index(int? composerId, int? genreId, int? issueId, int? periodId, int? value, int? keywordId, int? page)
        {

            var pageSize = 10;

            IEnumerable<Composition> data;
            if (composerId != null)
                data = _context.Composition.Include(c => c.Composer).Include(c => c.Genre).Include(c => c.Keyword).Where(c => c.ComposerID == composerId);
            else if (value != null)
                data = _context.Composition.Include(c => c.Composer).Include(c => c.Genre).Include(c => c.Keyword).Where(c => c.Value == value);
            else if (genreId != null)
                data = _context.Composition.Include(c => c.Composer).Include(c => c.Genre).Include(c => c.Keyword).Where(c => c.GenreID == genreId);
            else if (keywordId != null)
                data = _context.Composition.Include(c => c.Composer).Include(c => c.Genre).Include(c => c.Keyword).Where(c => c.KeywordID == keywordId);
            else 
                data = _context.Composition.Include(c => c.Composer).Include(c => c.Genre).Include(c => c.Keyword);
            var pagedData = data.ToPagedList(pageSize, page ?? 1);
            return View(pagedData);
        }

        // GET: Compositions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Composition composition = _context.Composition.Include(c => c.Composer).Include(c => c.Genre).Include(c => c.Keyword).Include(c => c.Subject).ThenInclude(s => s.Articles).ThenInclude(a => a.Author).Single(m => m.ID == id);
            if (composition == null)
            {
                return HttpNotFound();
            }

            return View(composition);
        }

        // GET: Compositions/Create
        public IActionResult Create()
        {
            ViewData["ComposerID"] = new SelectList(_context.Composer, "ID", "Name");
            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name");
            ViewData["KeywordID"] = new SelectList(_context.Keyword, "ID", "Name");
            return View();
        }

        // POST: Compositions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Composition composition)
        {
            if (ModelState.IsValid)
            {
                _context.Composition.Add(composition);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ComposerID"] = new SelectList(_context.Composer, "ID", "Name", composition.ComposerID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name", composition.GenreID);
            ViewData["KeywordID"] = new SelectList(_context.Keyword, "ID", "Name", composition.KeywordID);
            return View(composition);
        }

        // GET: Compositions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Composition composition = _context.Composition.Single(m => m.ID == id);
            if (composition == null)
            {
                return HttpNotFound();
            }
            ViewData["ComposerID"] = new SelectList(_context.Composer, "ID", "Name", composition.ComposerID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name", composition.GenreID);
            ViewData["KeywordID"] = new SelectList(_context.Keyword, "ID", "Name", composition.KeywordID);
            return View(composition);
        }

        // POST: Compositions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Composition composition)
        {
            if (ModelState.IsValid)
            {
                _context.Update(composition);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ComposerID"] = new SelectList(_context.Composer, "ID", "Name", composition.ComposerID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name", composition.GenreID);
            ViewData["KeywordID"] = new SelectList(_context.Keyword, "ID", "Name", composition.KeywordID);
            return View(composition);
        }

        // GET: Compositions/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Composition composition = _context.Composition.Single(m => m.ID == id);
            if (composition == null)
            {
                return HttpNotFound();
            }

            return View(composition);
        }

        // POST: Compositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Composition composition = _context.Composition.Single(m => m.ID == id);
            _context.Composition.Remove(composition);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
