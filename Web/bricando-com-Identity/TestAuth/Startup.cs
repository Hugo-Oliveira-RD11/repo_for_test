using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestAuth.Models;
using TestAuth.Models.Data;

namespace TestAuth
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
            services.AddDbContext<ApplicationDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            
            services.AddIdentity<ApplicationUser,IdentityRole<Guid>>()
              .AddEntityFrameworkStores<ApplicationDataContext>()
              .AddDefaultTokenProviders();
            
            services.Configure<IdentityOptions>(options => {
                //password
                options.Password.RequireDigit=true;
                options.Password.RequiredLength=8;
                options.Password.RequireLowercase=true;
                options.Password.RequireUppercase=true;
                options.Password.RequireNonAlphanumeric=false;
                options.Password.RequiredUniqueChars=4;                


                //lockout
                options.Lockout.AllowedForNewUsers=true;
                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts=5;


            });

            services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly=true;
                options.ExpireTimeSpan=TimeSpan.FromMinutes(10);
                options.LoginPath="/Account/Register";
                options.LogoutPath="/Account/Logout";
                options.AccessDeniedPath="/Account/AccessDenied";
                options.SlidingExpiration=true;
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            });
        }
    }
}
