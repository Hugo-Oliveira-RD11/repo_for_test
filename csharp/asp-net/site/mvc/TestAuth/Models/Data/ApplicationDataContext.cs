using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestAuth.Models.Data
{
    public class ApplicationDataContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>
    {
       public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options):base(options){

       } 
    }
}