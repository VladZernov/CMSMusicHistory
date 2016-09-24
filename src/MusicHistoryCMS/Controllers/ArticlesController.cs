using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MusicHistoryCMS.Models;
using System.Collections.Generic;
using Sakura.AspNet;
using MusicHistoryCMS.ViewModels.Article;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System;

namespace MusicHistoryCMS.Controllers
{
    public class ArticlesController : Controller
    {
        private ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Articles
        public IActionResult Index(int? subjectId, string user, int? issueId, string subjectType, int? page)
        {
            var pageSize = 10;

            IEnumerable<Article> data;
            if (subjectId != null)
                data = _context.Article.Include(a => a.Author).Include(a => a.Issue)
                    .Include(a => a.Subject).ThenInclude(s => s.Composer)
                    .Include(a => a.Subject).ThenInclude(s => s.Composition)
                    .Include(a => a.Subject).ThenInclude(s => s.Genre)
                    .Include(a => a.Subject).ThenInclude(s => s.Instrument)
                    .Include(a => a.Subject).ThenInclude(s => s.Period)
                    .Where(a => a.SubjectID == subjectId);
            else if (user != null)
                data = _context.Article.Include(a => a.Author).Include(a => a.Issue)
                    .Include(a => a.Subject).ThenInclude(s => s.Composer)
                    .Include(a => a.Subject).ThenInclude(s => s.Composition)
                    .Include(a => a.Subject).ThenInclude(s => s.Genre)
                    .Include(a => a.Subject).ThenInclude(s => s.Instrument)
                    .Include(a => a.Subject).ThenInclude(s => s.Period)
                    .Where(a => a.Author.Email == user);
            else if (issueId != null)
                data = _context.Article.Include(a => a.Author).Include(a => a.Issue)
                    .Include(a => a.Subject).ThenInclude(s => s.Composer)
                    .Include(a => a.Subject).ThenInclude(s => s.Composition)
                    .Include(a => a.Subject).ThenInclude(s => s.Genre)
                    .Include(a => a.Subject).ThenInclude(s => s.Instrument)
                    .Include(a => a.Subject).ThenInclude(s => s.Period)
                    .Where(a => a.IssueID == issueId);
            else if (subjectType != null)
                data = _context.Article.Include(a => a.Author).Include(a => a.Issue)
                    .Include(a => a.Subject).ThenInclude(s => s.Composer)
                    .Include(a => a.Subject).ThenInclude(s => s.Composition)
                    .Include(a => a.Subject).ThenInclude(s => s.Genre)
                    .Include(a => a.Subject).ThenInclude(s => s.Instrument)
                    .Include(a => a.Subject).ThenInclude(s => s.Period)
                    .Where(a => a.Subject.Type == subjectType);
            else
                data = _context.Article.Include(a => a.Author).Include(a => a.Issue)
                    .Include(a => a.Subject).ThenInclude(s => s.Composer)
                    .Include(a => a.Subject).ThenInclude(s => s.Composition)
                    .Include(a => a.Subject).ThenInclude(s => s.Genre)
                    .Include(a => a.Subject).ThenInclude(s => s.Instrument)
                    .Include(a => a.Subject).ThenInclude(s => s.Period);
            var pagedData = data.ToPagedList(pageSize, page ?? 1);
            return View(pagedData);
        }

        // GET: Articles/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Article article = _context.Article.Single(m => m.ID == id);
            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        // GET: Articles/Text/5
        public IActionResult Text(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Article article = _context.Article.Single(m => m.ID == id);
            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["Types"] = new SelectList(new List<string>{ "Composers", "Compositions", "Genres", "Periods", "Instruments", "Other" });
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                int? subjectId;
                switch (article.SubjectType)
                {
                    case "Composers":
                        subjectId = _context.Composer.Where(c => c.Name == article.SubjectName).SingleOrDefault().ID;
                        break;
                    case "Compositions":
                        subjectId = _context.Composition.Where(c => c.Name == article.SubjectName).SingleOrDefault().ID;
                        break;
                    case "Genres":
                        subjectId = _context.Genre.Where(c => c.Name == article.SubjectName).SingleOrDefault().ID;
                        break;
                    case "Periods":
                        subjectId = _context.Period.Where(c => c.Name == article.SubjectName).SingleOrDefault().ID;
                        break;
                    case "Instruments":
                        subjectId = _context.Instrument.Where(c => c.Name == article.SubjectName).SingleOrDefault().ID;
                        break;
                    default:
                        subjectId = null;
                        break;
                }
                var data = new Article { AuthorID = User.GetUserId(), Date = DateTime.Now, IssueID = _context.Issue.LastOrDefault().ID, SubjectID = subjectId, Text = article.Text };
                _context.Article.Add(data);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }


        // GET: Articles/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Article article = _context.Article.Single(m => m.ID == id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Author", article.AuthorID);
            ViewData["IssueID"] = new SelectList(_context.Issue, "ID", "Issue", article.IssueID);
            return View(article);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Update(article);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Author", article.AuthorID);
            ViewData["IssueID"] = new SelectList(_context.Issue, "ID", "Issue", article.IssueID);
            return View(article);
        }

        // GET: Articles/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Article article = _context.Article.Single(m => m.ID == id);
            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Article article = _context.Article.Single(m => m.ID == id);
            _context.Article.Remove(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
