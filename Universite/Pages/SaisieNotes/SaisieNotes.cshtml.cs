using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universite.Models;

namespace Universite.Pages.SaisieNotes
{
    [Authorize(Roles = "Enseignant")]
    public class SaisieNotesModel : NotesPageModel
    {
        private readonly Universite.Data.ApplicationDbContext _context;

        public SaisieNotesModel(Universite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // Les données à afficher
        public IList<UE> LesUEs { get; set; }

        // L’UE sélectionné
        [BindProperty]
        public int UEID { get; set; }

        // Contenu de la combo box
        public List<SelectListItem> SelectUEsData { get; private set; }
        public async Task OnGetAsync()
        {
            LesUEs = await _context.UE.ToListAsync();

            // Initialisation de la page. Chargement de la liste déroulante des UEs
            SelectUEsData = new List<SelectListItem>();
            SelectUEsData.Add(new SelectListItem
            {
                Text = "Choisir une UE",
                Value = ""
            });

            foreach (UE u in LesUEs)
            {
                SelectUEsData.Add(new SelectListItem
                {
                    Text = u.NomComplet,
                    Value = u.ID.ToString()
                });
            }

            // Remplissage de la vue
            ViewData["SelectUEsData"] = new SelectList(SelectUEsData, "Value", "Text");
            if (ue != null)
                SearchNotes(_context, ue);
        }

        public UE ue { get; set; }

        public async Task<IActionResult> OnPostLoadAsync()
        {
            if (UEID == 0)
            {
                await OnGetAsync();
                return Page();
            }

            ue = await _context.UE.Include(e => e.LaFormation).AsNoTracking().FirstOrDefaultAsync(m => m.ID == UEID);

            await OnGetAsync();

            return Page();
        }

        [BindProperty]
        public int[] etudiantID { get; set; }

        [BindProperty]
        public String[] Notes { get; set; }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (etudiantID != null)
            {
                for (int i = 0; i < etudiantID.Length; i++)
                { 
                    var note = from n in _context.Note where n.UEID == UEID && n.EtudiantID == etudiantID[i] select n;

                    Note Note = new Note();
                    foreach (Note n in note)
                        Note = n;

                    if (!String.IsNullOrEmpty(Notes[i]))
                    {
                        try
                        {
                            if (Note.ID == 0)
                            {
                                Note noteEtud = new Note();
                                noteEtud.ID = 0;
                                noteEtud.Valeur = float.Parse(Notes[i].Replace('.', ','));
                                noteEtud.UEID = UEID;
                                noteEtud.EtudiantID = etudiantID[i];

                                _context.Note.Add(noteEtud);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                Note.Valeur = float.Parse(Notes[i].Replace('.', ','));
                                _context.Attach(Note).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                        }catch (Exception e)
                        {
                            //rien
                        }
                        
                    }
                    else
                    {
                        if (Note.ID != 0)
                        {
                            _context.Note.Remove(Note);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }

            await OnPostLoadAsync();
            return Page();
        }
    }
}