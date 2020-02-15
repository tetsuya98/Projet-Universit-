using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models
{
    public class UE
    {
        // Clé primaire
        public int ID { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Intitule { get; set; }
        // Clé étrangère vers la formation
        public int FormationID { get; set; }
        // Liens de navigation
        [Display(Name = "Formation")]
        public Formation LaFormation { get; set; }
        [Display(Name = "Nom Ue")]
        public string NomComplet
        {
            get
            {
                return "INFO_" + Numero + " - " + Intitule;
            }
        }
        public ICollection<Note> LesNotes { get; set; }
        public ICollection<Enseigne> LesEnseigne { get; set; }
    }
}