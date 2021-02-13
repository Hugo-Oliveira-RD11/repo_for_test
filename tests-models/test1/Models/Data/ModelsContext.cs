using Microsoft.EntityFrameworkCore;
using test1.Models;

namespace test1.Models.Data
{
    public class ModelsContext:DbContext
    {
        public ModelsContext (DbContextOptions<ModelsContext> options):base(options){}
        public DbSet<usuarios> cadastro {get;set;}
    }
}