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
    public class IndexModel : PageModel
    {
        private readonly TestXpert.Data.ApplicationDbContext _context;

        public IndexModel(TestXpert.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Test> Test { get;set; }

        public async Task OnGetAsync()
        {
            Test = await _context.Tests.ToListAsync();
        }
    }
}
