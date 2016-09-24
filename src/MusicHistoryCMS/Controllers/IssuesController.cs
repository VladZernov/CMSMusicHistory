using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MusicHistoryCMS.Models;
using System.Collections.Generic;
using Sakura.AspNet;

namespace MusicHistoryCMS.Controllers
{
    public class IssuesController : Controller
    {
        private ApplicationDbContext _context;

        public IssuesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Issues
        public IActionResult Index(int? page)
        {
            var pageSize = 10;

            IEnumerable<Issue> data = _context.Issue;

            var pagedData = data.ToPagedList(pageSize, page ?? 1);
            return View(pagedData);
        }

        // GET: Issues/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Issue issue = _context.Issue.Single(m => m.ID == id);
            if (issue == null)
            {
                return HttpNotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Issue.Add(issue);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: Issues/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Issue issue = _context.Issue.Single(m => m.ID == id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Update(issue);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: Issues/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Issue issue = _context.Issue.Single(m => m.ID == id);
            if (issue == null)
            {
                return HttpNotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Issue issue = _context.Issue.Single(m => m.ID == id);
            _context.Issue.Remove(issue);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
