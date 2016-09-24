using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MusicHistoryCMS.Models;
using System.Collections.Generic;
using Sakura.AspNet;

namespace MusicHistoryCMS.Controllers
{
    public class GenresController : Controller
    {
        private ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Genres
        public IActionResult Index(int? page, int? composerId)
        {
            var pageSize = 10;

            IEnumerable<Genre> data;
            if (composerId != null)
                data = (from genre in _context.Genre
                        join composition in _context.Composition on genre.ID equals composition.GenreID
                        join composer in _context.Composer on composition.ComposerID equals composer.ID
                        where composer.ID == composerId
                        select genre
                        )
                        .Distinct()
                        .OrderBy(id => id);
            else
                data = _context.Genre;
            var pagedData = data.ToPagedList(pageSize, page ?? 1);
            return View(pagedData);
        }

        // GET: Genres/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Genre genre = _context.Genre.Single(m => m.ID == id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre genre)
        {
            genre.ID = 6798;
                _context.Genre.Add(genre);
                _context.SaveChanges();
            return View();
        }

        // GET: Genres/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Genre genre = _context.Genre.Single(m => m.ID == id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Update(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Genre genre = _context.Genre.Single(m => m.ID == id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Genre genre = _context.Genre.Single(m => m.ID == id);
            _context.Genre.Remove(genre);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
