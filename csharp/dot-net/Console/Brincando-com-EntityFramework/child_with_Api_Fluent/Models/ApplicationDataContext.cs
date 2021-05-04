using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;

namespace child_with_Api_Fluent.Models
{
    public class ApplicationDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=childFluent;Initial Catalog=apifluent;User Id=SA;Password=VoceETrouxa24");
        }
        
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options):base(options)
        {
            
        }
        public DbSet<Galaxia> Galaxias { get; set; }
        public DbSet<Planeta> Planetas { get; set; }
        public DbSet<SupersBuracosNegros> SBuracosNegros { get; set; }
    }
}