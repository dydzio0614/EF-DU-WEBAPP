using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestXpert.Data;
using TestXpert.Models;

namespace TestXpert.Controllers
{
    public class Questions3Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Questions3Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questions3
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.ToListAsync());
        }

        // GET: Questions3/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(s => s.Answers)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions3/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions3/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Points,Answers,CorrectAnswer")] Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questions3/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(s => s.Answers)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions3/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Points,Answers,CorrectAnswer")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /* ATTEMPT 1
                    var storedQuestion = _context.Questions
                    .Include(s => s.Answers)
                    .SingleOrDefault(m => m.Id == id);

                    //without below loop answers are cloned, not updated - answer ID's are not grabbed from view
                    for (int i = 0; i < question.Answers.Count; i++) //copy answer ID - assume same amount of elements for edit as existing in db
                    {
                        question.Answers.ElementAt(i).Id = storedQuestion.Answers.ElementAt(i).Id;

                        _context.Update(question);
                        await _context.SaveChangesAsync();
                    }*/

                    /*ATTEMPT 2
                    var storedQuestion = _context.Questions
                    .Include(s => s.Answers)
                    .SingleOrDefault(m => m.Id == id);
                    _context.Remove(storedQuestion);
                    _context.SaveChanges();

                    _context.Add(question);
                    _context.SaveChanges();*/

                    var storedQuestion = _context.Questions
                   .Include(s => s.Answers)
                   .SingleOrDefault(m => m.Id == id);

                    var result = TryUpdateModelAsync<Question>(storedQuestion, "", q => q.Description, q => q.Points, q => q.Answers, q => q.CorrectAnswer);
                    await _context.SaveChangesAsync();                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
            }
            return View(question);
        }

        // GET: Questions3/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(m => m.Answers)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions
                .Include(m => m.Answers)
                .SingleOrDefaultAsync(m => m.Id == id);

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
