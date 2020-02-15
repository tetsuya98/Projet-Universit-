﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Universite.Data;
using Universite.Models;

namespace Universite.Pages.Formations
{
    public class DetailsModel : PageModel
    {
        private readonly Universite.Data.ApplicationDbContext _context;

        public DetailsModel(Universite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Formation Formation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Formation = await _context.Formation.FirstOrDefaultAsync(m => m.ID == id);

            if (Formation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
