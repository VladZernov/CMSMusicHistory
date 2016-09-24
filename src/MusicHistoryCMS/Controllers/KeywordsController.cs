using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MusicHistoryCMS.Models;
using System.Collections.Generic;
using Sakura.AspNet;

namespace MusicHistoryCMS.Controllers
{
    public class KeywordsController : Controller
    {
        private ApplicationDbContext _context;

        public KeywordsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Keywords
        public IActionResult Index(int? page)
        {
            var pageSize = 10;

            IEnumerable<Keyword> data = _context.Keyword;

            var pagedData = data.ToPagedList(pageSize, page ?? 1);
            return View(pagedData);
        }

        // GET: Keywords/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Keyword keyword = _context.Keyword.Single(m => m.ID == id);
            if (keyword == null)
            {
                return HttpNotFound();
            }

            return View(keyword);
        }

        // GET: Keywords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Keywords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                _context.Keyword.Add(keyword);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyword);
        }

        // GET: Keywords/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Keyword keyword = _context.Keyword.Single(m => m.ID == id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: Keywords/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                _context.Update(keyword);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyword);
        }

        // GET: Keywords/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Keyword keyword = _context.Keyword.Single(m => m.ID == id);
            if (keyword == null)
            {
                return HttpNotFound();
            }

            return View(keyword);
        }

        // POST: Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Keyword keyword = _context.Keyword.Single(m => m.ID == id);
            _context.Keyword.Remove(keyword);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
