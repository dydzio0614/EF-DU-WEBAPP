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

        /// <summary>
        /// Prepares index page for User-Question management by binding currently logged user to model
        /// </summary>
        /// <returns>Page with data and Add/Unlink operations for current account, or "user not logged in" page if no registered user detected</returns>
        public async Task<ActionResult> Index()
        {
            var user = await _context.Users
            .Include(u => u.UserQuestions)
            .SingleOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

            return View(user);
        }

        /// <summary>
        /// Prepares view with dropdown list to pick question to link to user
        /// </summary>
        /// <returns>View with all questions in form usable by dropdown list in ViewBag.dbQuestions if used by registered user, or "user non logged in" page</returns>
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

        /// <summary>
        /// Add User-Question connection based on dropdown list choice
        /// </summary>
        /// <param name="collection">Collection containing data from all HTML controls, should be one item with question ID</param>
        /// <returns>Index page on successful update, stays on previous view otherwise</returns>
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

        /// <summary>
        /// Shows User-Question connection removal page, passes question object based on id to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Confirmation view, "NotFound" page if no item to delete passed or empty page on abnormal usage, including unregistered user access</returns>
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

        /// <summary>
        /// Removes User-Question connection after confirmation
        /// </summary>
        /// <param name="id">question id to be unlinked, passed from view</param>
        /// <returns>Index view on correct usage, empty page otherwise</returns>
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