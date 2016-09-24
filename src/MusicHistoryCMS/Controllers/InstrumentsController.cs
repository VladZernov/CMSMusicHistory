using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MusicHistoryCMS.Models;
using System.Collections.Generic;
using Sakura.AspNet;

namespace MusicHistoryCMS.Controllers
{
    public class InstrumentsController : Controller
    {
        private ApplicationDbContext _context;

        public InstrumentsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Instruments
        public IActionResult Index(int? page, int? periodId)
        {
            var pageSize = 10;

            IEnumerable<Instrument> data;
            if (periodId != null)
                data = _context.Instrument.Include(i => i.Period).Where(i => i.PeriodID == periodId);
            else
                data = _context.Instrument.Include(i => i.Period);
            var pagedData = data.ToPagedList(pageSize, page ?? 1);
            return View(pagedData);
        }

        // GET: Instruments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Instrument instrument = _context.Instrument.Single(m => m.ID == id);
            if (instrument == null)
            {
                return HttpNotFound();
            }

            return View(instrument);
        }

        // GET: Instruments/Create
        public IActionResult Create()
        {
            ViewData["PeriodID"] = new SelectList(_context.Period, "ID", "Name");
            return View();
        }

        // POST: Instruments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                _context.Instrument.Add(instrument);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["PeriodID"] = new SelectList(_context.Period, "ID", "Name", instrument.PeriodID);
            return View(instrument);
        }

        // GET: Instruments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Instrument instrument = _context.Instrument.Single(m => m.ID == id);
            if (instrument == null)
            {
                return HttpNotFound();
            }
            ViewData["PeriodID"] = new SelectList(_context.Period, "ID", "Name", instrument.PeriodID);
            return View(instrument);
        }

        // POST: Instruments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                _context.Update(instrument);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["PeriodID"] = new SelectList(_context.Period, "ID", "Name", instrument.PeriodID);
            return View(instrument);
        }

        // GET: Instruments/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Instrument instrument = _context.Instrument.Single(m => m.ID == id);
            if (instrument == null)
            {
                return HttpNotFound();
            }

            return View(instrument);
        }

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Instrument instrument = _context.Instrument.Single(m => m.ID == id);
            _context.Instrument.Remove(instrument);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
