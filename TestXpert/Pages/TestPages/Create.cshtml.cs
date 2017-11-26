using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestXpert.Data;

namespace TestXpert.Pages.TestPages
{
    public class CreateModel : PageModel
    {
        private readonly TestXpert.Data.ApplicationDbContext _context;

        public CreateModel(TestXpert.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Test Test { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tests.Add(Test);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}