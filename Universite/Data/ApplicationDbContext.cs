using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Universite.Models;

namespace Universite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Universite.Models.Etudiant> Etudiant { get; set; }
        public DbSet<Universite.Models.Enseignant> Enseignant { get; set; }
        public DbSet<Universite.Models.Formation> Formation { get; set; }
        public DbSet<Universite.Models.UE> UE { get; set; }
        public DbSet<Universite.Models.Note> Note { get; set; }
    }
}
