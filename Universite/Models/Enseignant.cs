using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models
{
    public class Enseignant
    {
        // Clé primaire
        public int ID { get; set; }
        [Required]
        public String Nom { get; set; }
        [Required]
        public String Prenom { get; set; }
        // Lien de navigation
        [Display(Name = "Enseignement ")]
        public ICollection<Enseigne> LesEnseigne { get; set; }
    }
}