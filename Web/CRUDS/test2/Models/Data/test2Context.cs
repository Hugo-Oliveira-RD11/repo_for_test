using Microsoft.EntityFrameworkCore;
using test2.Models;

namespace test2.Data
{
    public class Test2Context : DbContext
    {
        public Test2Context (DbContextOptions<Test2Context> options):base(options){}

        public DbSet<CreateUser> Users{get;set;}
    }
}