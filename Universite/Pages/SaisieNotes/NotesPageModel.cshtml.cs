using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universite.Data;
using Universite.Models;

namespace Universite.Pages.SaisieNotes
{
    public class NotesPageModel : PageModel
    {
        public Dictionary<Etudiant, String> etude = new Dictionary<Etudiant, String>();

        public void SearchNotes(ApplicationDbContext context, UE ue)
        {
            var etudiant = from et in context.Etudiant where et.FormationID == ue.FormationID select et;
            var note = from n in context.Note where n.UEID == ue.ID orderby n.EtudiantID ascending select n;
            String value;

            foreach(Etudiant e in etudiant)
            {
                value = "";
                foreach (Note n in note)
                {
                    if (n.EtudiantID == e.ID)
                        value = n.Valeur + "";
                }
                etude.Add(e, value);
            }
        }
    }
}
