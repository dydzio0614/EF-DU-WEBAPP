using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestXpert.Data;

namespace TestXpert.Pages.TestPages
{
    public class DetailsModel : PageModel
    {
        private readonly TestXpert.Data.ApplicationDbContext _context;

        public DetailsModel(TestXpert.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
