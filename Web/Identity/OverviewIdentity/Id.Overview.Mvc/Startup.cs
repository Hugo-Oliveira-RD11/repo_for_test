using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Id.Overview.Mvc.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Id.Overview.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SqlServer")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.Configure<IdentityOptions>(options => {

                options.Lockout.AllowedForNewUsers=true;
                options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;

                
                //e obrigado a digitar um numero de 0 a 9
                options.Password.RequireDigit=true;
                //e obrigado a senha ter no minimo 4 de tamanho
                options.Password.RequiredLength=4;
                //requere que vc diferencie a sua senha, tipo ppa2014, p se repete duas vezes, mas se a senha for assim aaaaa...
                options.Password.RequiredUniqueChars = 1;
                // e obrigado a ter uma letra minuscula na senha!
                options.Password.RequireLowercase=true;
                //requere que vc tenha uma letra maiuscula!
                options.Password.RequireUppercase=true;
                //e obrigado a ter um caracter diferente na senha(tipo *&¨%$##@!;°~~~^`{º{[.]}})
                options.Password.RequireNonAlphanumeric=true;

                //sigin
                //essa propriedade define que para entrar no site tem que ter o email confirmado
                options.SignIn.RequireConfirmedEmail=false;
                //essa define que tem quer ter o um numero de celular confirmado
                options.SignIn.RequireConfirmedPhoneNumber=false;
                //eu ainda n sei oque esse metodo faz...
                options.SignIn.RequireConfirmedAccount=false;

                //define quais caracteres pode estar no nome do usuario!
                options.User.AllowedUserNameCharacters="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.-_@+:)";
                //define se cada usuario precisao de um email, que n tenha em outro usuario!
                options.User.RequireUniqueEmail=false;
            });
            services.ConfigureApplicationCookie(options => {
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
