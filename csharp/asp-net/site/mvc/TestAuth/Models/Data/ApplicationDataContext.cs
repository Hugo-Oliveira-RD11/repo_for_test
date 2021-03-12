using Microsoft.EntityFrameworkCore;

namespace TestAuth.Models.Data
{
    public class ApplicationDataContext : DbContext
    {
       public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options):base(options){

       } 
    }
}