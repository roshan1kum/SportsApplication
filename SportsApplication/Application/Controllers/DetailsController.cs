using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application.Models;
using SportsApplication.Models;
using System.Data;

namespace Application.Controllers
{
    public class DetailsController : Controller
    {
        // private readonly IDetails IdetailsRepository;
        //private readonly ApplicationContext _context;
        private IUnitOfWork unitOfWork;
        public DetailsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            // this.IdetailsRepository = IdetailsRepository;
            //_context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewData["Id"] = id;
            test x= await unitOfWork.TestsRepository.GetTestID(id);
            ViewData["title"] = x.test_type;
            ViewData["Date"] = x.Date;
            var details = unitOfWork.detailRepository.GetDetails(id);
                          
            return View(details.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("TestResultId,TestId,Name,Distance,FitnessRating")] Details details, int id)
        {
            if (ModelState.IsValid)
            {
                details.TestId = id;
                unitOfWork.detailRepository.InsertAthlete(details);
                unitOfWork.Save();
                return Redirect("http://localhost:53378/Details/Index/" + details.TestId);
            }
            return View(details);

        }
        public async Task<IActionResult> Edit(int id)
        {
            Details d = await unitOfWork.detailRepository.GetDetailsID(id);
            return View(d);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("TestResultId,TestId,Name,Distance,FitnessRating")] Details details, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.detailRepository.EditAthlete(details);
                    unitOfWork.Save();
                    return Redirect("http://localhost:53378/Details/Index/" + details.TestId);
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(details);
            //return RedirectToAction("http://localhost:53378/Details/Index/" + details.TestId);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Details details = await unitOfWork.detailRepository.GetDetailsID(id);
            return View(details);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Details details = await unitOfWork.detailRepository.GetDetailsID(id);
            int testId = details.TestId;
            await unitOfWork.detailRepository.DeleteAthlete(details.TestResultId);
            unitOfWork.Save();
            return Redirect("http://localhost:53378/Details/Index/" + testId);
        }
        //.............
        // GET: Details
        //public async Task<IActionResult> Index(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var details = await _context.test
        //       .SingleOrDefaultAsync(m => m.TestId == id);
        //    ViewData["title"] = details.test_type;
        //    ViewData["Date"] = details.Date;
        //    ViewData["Id"] = id;

        //    var query = from s in _context.Details
        //                where s.TestId == id
        //                select new Details
        //                {
        //                    TestResultId = s.TestResultId,
        //                    TestId = s.TestId,
        //                    Name = s.Name,
        //                    Distance = s.Distance,
        //                    FitnessRating = s.FitnessRating
        //                };
        //    var qry = query.ToList();
        //    foreach (var v in qry)
        //    {
        //        if (v.Distance <= 1000)
        //        {
        //            v.FitnessRating = "Below Average";
        //        }
        //        else if (v.Distance > 1000 && v.Distance <= 2000)
        //        {
        //            v.FitnessRating = "Average";
        //        }
        //        else if (v.Distance > 2000 && v.Distance <= 3500)
        //        {
        //            v.FitnessRating = "Good";
        //        }
        //        else
        //        {
        //            v.FitnessRating = "Very Good";
        //        }
        //    }
        //    var newList = qry.OrderByDescending(x => x.Distance).ToList();
        //    return View(newList);
        //}

        // GET: Details/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var details = await _context.Details
        //        .SingleOrDefaultAsync(m => m.TestResultId == id);
        //    if (details == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(details);
        //}

        //// GET: Details/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Details/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TestResultId,TestId,Name,Distance,FitnessRating")] Details details,int id)
        //{
        //    details.TestId = id;
        //    if (ModelState.IsValid)
        //    {

        //        _context.Add(details);
        //        await _context.SaveChangesAsync();
        //        return Redirect("http://localhost:53378/Details/Index/"+ details.TestId);
        //    }
        //    return View(details);
        //}

        //// GET: Details/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var details = await _context.Details.SingleOrDefaultAsync(m => m.TestResultId == id);
        //    if (details == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(details);
        //}

        //// POST: Details/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit([Bind("TestResultId,TestId,Name,Distance,FitnessRating")] Details details, int id)
        //{
        //    //var testid = await _context.Details
        //    //  .SingleOrDefaultAsync(m => m.TestResultId == id);
        //    if (id != details.TestResultId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(details);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DetailsExists(details.TestResultId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return Redirect("http://localhost:53378/Details/Index/" + details.TestId);
        //    }
        //    return View(details);
        //}

        //// GET: Details/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var details = await _context.Details
        //        .SingleOrDefaultAsync(m => m.TestResultId == id);
        //    if (details == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(details);
        //}

        //// POST: Details/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var details = await _context.Details.SingleOrDefaultAsync(m => m.TestResultId == id);
        //    int testId = details.TestId;
        //    _context.Details.Remove(details);
        //    await _context.SaveChangesAsync();
        //    return Redirect("http://localhost:53378/Details/Index/"+testId);
        //}

        //private bool DetailsExists(int id)
        //    {
        //        return _context.Details.Any(e => e.TestResultId == id);
        //    }
    }
}
