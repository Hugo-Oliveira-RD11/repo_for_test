using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using test1.Models.Data;
using System;
using System.Linq;

namespace test1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider){
            using (var context = new ModelsContext(serviceProvider.GetRequiredService<DbContextOptions<ModelsContext>>()))
            {
                if(context.cadastro.Any()){
                    return;
                }
                context.cadastro.AddRange(
                    new usuarios{
                        nome="hugo",
                        email="hugo.vaiseferrar@gmail.com",
                        senha="nadamesmo"

                    }
                );
                context.SaveChanges();
            }
        }
    }
}