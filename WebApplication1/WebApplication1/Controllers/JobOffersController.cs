using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ModelViews;

namespace WebApplication1.Controllers
{
    public class JobOffersController : Controller
    {
        private readonly WebApplication1Context _context;

        public JobOffersController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: JobOffers
       
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult Index([FromQuery(Name = "search")] string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return View(_context.JobOffer.Include(x => x.Company).ToList());
            List<JobOffer> l = _context.JobOffer.ToList();
            return View(_context
                .JobOffer.Where(o => o.JobTitle.Contains(searchString)));
        }

        // GET: JobOffers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                throw new InvalidOperationException();
            }
            var l = _context.JobOffer.ToArray();
            var x = _context.JobApplications.ToArray();
            var jobOffer = await _context.JobOffer
                .FirstOrDefaultAsync(m => m.Id == id);
           if( jobOffer.JobApplications.Count() == 0)
            {
                jobOffer.JobApplications = new List<JobApplication>();
                jobOffer.JobApplications.AddRange(_context.JobApplications.Where(j => j.OfferId == id).ToList());
            }
            if (jobOffer == null)
            {
                return NotFound();
            }

            return View(jobOffer);
        }

        // GET: JobOffers/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobTitle,Salary,Location,Description,ValidUntil")] JobOffer jobOffer)
        {
            if (ModelState.IsValid)
            {
                if(_context.JobOffer.Any(j => j.JobTitle == jobOffer.JobTitle))
                {
                    ViewBag.Message = "ERROR: Couldn't create offer. Job offer with that job title (" + jobOffer.JobTitle +") already exists.";
                    return View(jobOffer);
                }
                if(DateTime.Compare((DateTime)jobOffer.ValidUntil, DateTime.Now)<0)
                {
                    ViewBag.Message = "ERROR: Couldn't create offer. Date must be greater than today.";
                    return View(jobOffer);
                }
                _context.Add(jobOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobOffer);
        }

        // GET: JobOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffer = await _context.JobOffer.FindAsync(id);
            if (jobOffer == null)
            {
                return NotFound();
            }
            return View(jobOffer);
        }

        // POST: JobOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]//modelbinder
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobTitle,Salary,Location,Description,ValidUntil")] JobOffer jobOffer)
        {
            if (id != jobOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobOfferExists(jobOffer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobOffer);
        }

        // GET: JobOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffer = await _context.JobOffer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobOffer == null)
            {
                return NotFound();
            }

            return View(jobOffer);
        }

        // POST: JobOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobOffer = await _context.JobOffer.FindAsync(id);
            _context.JobOffer.Remove(jobOffer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobOfferExists(int id)
        {
            return _context.JobOffer.Any(e => e.Id == id);
        }
    }
}
