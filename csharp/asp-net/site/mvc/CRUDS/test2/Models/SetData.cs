using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using test2.Data;
using System;
using System.Linq;

namespace test2.Models{
    public static class SetData{
        public static void Initialize(IServiceProvider serviceProvider){

        using (var context = new Test2Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<Test2Context>>()))
            {
                // Look for any movies.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }
                context.Users.AddRange(
                    new CreateUser{
                        NomeR="raimundo",
                        NomeU="n sei oq e isso",
                        Email="galã-de-novela@gmail.com",
                        senha="NiguemNuncaVaiAcertarIsso"
                    },
                     new CreateUser{
                        NomeR="maria",
                        NomeU="a estrela",
                        Email="maria113420.linda@gmail.com",
                        senha="senha"
                    },
                     new CreateUser{
                        NomeR="tiago",
                        NomeU="o matador de porco",
                        Email="galim.matador.de.porco@gmail.com",
                        senha="porco"
                    }
                );
                context.SaveChanges();
                
            }
        }
    }
}