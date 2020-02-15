using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universite.Data;
using Universite.Models;

namespace Universite.Pages.Enseignants
{
    public class IndexModel : PageModel
    {
        private readonly Universite.Data.ApplicationDbContext _context;

        public IndexModel(Universite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Enseignant> Enseignant { get;set; }

        public async Task OnGetAsync()
        {
            Enseignant = await _context.Enseignant
            .Include(i => i.LesEnseigne)
            .ThenInclude(i => i.LUE)
            .AsNoTracking()
            .ToListAsync();
        }
    }
}
