using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestXpert.Data;

namespace TestXpert.Pages.TestPages
{
    public class EditModel : PageModel
    {
        private readonly TestXpert.Data.ApplicationDbContext _context;

        public EditModel(TestXpert.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Test Test { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Test = await _context.Tests.SingleOrDefaultAsync(m => m.Id == id);

            if (Test == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Test).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return RedirectToPage("./Index");
        }
    }
}
