using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public string CurrentFilter { get; set; }
        public IList<Student> Students { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            CurrentFilter = searchString; 

            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;

            if (!String.IsNullOrEmpty(searchString) && searchString != "*")
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            Students = await studentsIQ.AsNoTracking().ToListAsync();
        }
    }
}
