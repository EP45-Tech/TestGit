using BarnMart_Web.Configuration;
using BarnMart_Web.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using The_Barn_Mart;
using BarnMart_Web;
using Microsoft.AspNetCore.Identity;
//using BarnMart_Web.Data;

namespace BarnMart_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ELI")); //AKI SEB ELI
            });


            //Service for Identity 
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;

            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>();

            //service for User Sessions
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //Service for IHttpContextAccessor 
            builder.Services.AddHttpContextAccessor();


            //Service for Configuring User Cookie 
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
                options.SlidingExpiration = true;
            });

            //Service for AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            //Service for 
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<IScheduleRepo, ScheduleRepo>();



            // Add services to the container.
            builder.Services.AddControllersWithViews();

           

            var app = builder.Build();         

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseIdentity();
            app.UseSession();

            //Adding Authentication Middleware
            app.UseAuthentication();

            app.UseAuthorization();
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //Data Seeding for Roles 
            using (var scope = app.Services.CreateScope()) 
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                //Array with all roles 
                var roles = new[] { "Admin", "Transportify", "Buyer", "Seller"};

                foreach (var role in roles) 
                {
                    //If no roles provided this will create roles above
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Create Admin account and assign Admin to that account by email
            using (var scope = app.Services.CreateScope())
            {
                var userManager = 
                    scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "barnmart-admin@gmail.com";
                string password = "Admin12345";

                if (await userManager.FindByEmailAsync(email) == null) 
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;
                    

                    //Register user to Admin 
                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            //Create Transportify Account
            using (var scope = app.Services.CreateScope())
            {
                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "Transportify@gmail.com";
                string password = "CarMax12345";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;


                    //Register user to Admin 
                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Transportify");
                }
            }


            //Create Seller Account
            using (var scope = app.Services.CreateScope())
            {
                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "JohnDoe@gmail.com";
                string password = "abc12345678";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;


                    
                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Seller");
                }
            }

            app.Run();
        }
    }
}
