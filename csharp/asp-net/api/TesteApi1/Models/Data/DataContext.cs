using Microsoft.EntityFrameworkCore;

namespace TesteApi1.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){}

        public DbSet<Category> Categories{ get; set;}
        public DbSet<Product> Products { get; set; }

    }
}