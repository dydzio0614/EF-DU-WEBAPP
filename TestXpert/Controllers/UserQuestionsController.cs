using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestXpert.Data;
using TestXpert.Models;

namespace TestXpert.Controllers
{
    public class UserQuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserQuestionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserQuestion
        public async Task<ActionResult> Index()
        {
            var user = await _context.Users
            .Include(u => u.UserQuestions)
            .SingleOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

            return View(user);
        }

        // GET: UserQuestion/Add
        public async Task<ActionResult> Add()
        {
            var user = await _context.Users
            .Include(u => u.UserQuestions)
            .SingleOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

            IEnumerable<SelectListItem> allQuestions = _context.Questions.Select(q =>
                new SelectListItem
                {
                    Value = q.Id.ToString(),
                    Text = q.Description
                })
                .Where(x => !user.UserQuestions.Select(q => q.Id).Contains(Convert.ToInt32(x.Value))); //final filter to not include these: "if user->questions->id's contain that item already"

            ViewBag.dbQuestions = allQuestions;
            return View();
        }

        // POST: UserQuestion/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(IFormCollection collection)
        {
            try
            {
                int id;
                var isQuestionId = int.TryParse(collection.First().Value.First(), out id);
                if(isQuestionId)
                {
                    var user = await _context.Users
                        .Include(u => u.UserQuestions)
                        .SingleOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

                    var question = await _context.Questions
                        .Include(q => q.Answers)
                        .SingleOrDefaultAsync(q => q.Id == id);

                    user.UserQuestions.Add(question);
                    _context.Update(question);
                    await _context.SaveChangesAsync();        
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserQuestion/Unlink/5
        public async Task<IActionResult> Unlink(int? id)
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

        // POST: UserQuestion/Unlink/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unlink(int id)
        {
            try
            {
                var user = await _context.Users
                .Include(u => u.UserQuestions)
                .SingleOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

                var question = user.UserQuestions.Where(x => x.Id == id).Single(); //get question from user's "related questions" collection point of view
                user.UserQuestions.Remove(question);

                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteIf(ex is InvalidOperationException, ex.Message);
                return View();
            }
        }
    }
}