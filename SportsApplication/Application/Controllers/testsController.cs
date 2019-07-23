using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application.Models;
using SportsApplication.Models;

namespace Application.Controllers
{
    public class testsController : Controller
    {
        // private  ApplicationContext _context;
        // private  ITestsRepository testsRepository;
        private IUnitOfWork unitOfWork;
        public testsController(IUnitOfWork unitOfWork)
        {
            //this.testsRepository = testsRepository;
            this.unitOfWork = unitOfWork;
            //  _context = context;
        }
        public ActionResult Index()
        {
            foreach(var test in unitOfWork.TestsRepository.GetTest())
            {
                test.number_participants = unitOfWork.TestsRepository.NumberOfParticipants(test.TestId);
            }
            var tests = unitOfWork.TestsRepository.GetTest();
                        
            return View(tests.OrderByDescending(s => s.Date));
        }
        public ActionResult Create()
        {
            return View();
        }
        // POST: tests/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("TestId,Date,number_participants,test_type")] test test)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TestsRepository.InsertTest(test);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            test tests =await unitOfWork.TestsRepository.GetTestID(id);
            return View(tests);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await unitOfWork.TestsRepository.DeleteTest(id);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }


        // // GET: tests
        // public async Task<IActionResult> Index()
        // {
        //     foreach (var db in _context.test)
        //     {
        //         db.number_participants = NumberOfParticipants(db.TestId);
        //     }
        //     var newList = _context.test.OrderByDescending(s => s.Date).ToList();
        //     return View(newList);

        // }

        // //GET: tests/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var test = await _context.test
        //         .SingleOrDefaultAsync(m => m.TestId == id);
        //     if (test == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(test);
        // }

        //// GET: tests/Create
        // public IActionResult Create()
        // {
        //     return View();
        // }

        // //POST: tests/Create
        // //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("TestId,Date,number_participants,test_type")] test test)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(test);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(test);
        // }

        //// GET: tests/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var test = await _context.test.SingleOrDefaultAsync(m => m.TestId == id);
        //     if (test == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(test);
        // }

        // //POST: tests/Edit/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("TestId,Date,number_participants,test_type")] test test)
        // {
        //     if (id != test.TestId)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(test);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!testExists(test.TestId))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(test);
        // }

        //// GET: tests/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var test = await _context.test
        //         .SingleOrDefaultAsync(m => m.TestId == id);
        //     if (test == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(test);
        // }

        //// POST: tests/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var test = await _context.test.SingleOrDefaultAsync(m => m.TestId == id);
        //     _context.test.Remove(test);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        // private bool testExists(int id)
        // {
        //     return _context.test.Any(e => e.TestId == id);
        // }
    }
}
