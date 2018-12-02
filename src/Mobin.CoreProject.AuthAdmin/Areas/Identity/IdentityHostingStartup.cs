//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Mobin.CoreProject.AuthAdmin.Areas.Identity.Data;

//[assembly: HostingStartup(typeof(Mobin.CoreProject.AuthAdmin.Areas.Identity.IdentityHostingStartup))]
//namespace Mobin.CoreProject.AuthAdmin.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) => {
//                services.AddDbContext<AppDbContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("AppDbContextConnection")));

//                services
//                    .AddIdentity<AppUser, AppRole>()
//                    .AddDefaultTokenProviders()
//                    //.AddDefaultIdentity<AppUser>()
//                    //.AddRoleManager<RoleManager<IdentityRole>>()
//                    //.AddRoles<IdentityRole>()
//                    .AddEntityFrameworkStores<AppDbContext>();



//                services.Configure<IdentityOptions>(options =>
//                {
//                    // Password settings.
//                    options.Password.RequireDigit = false;
//                    options.Password.RequireLowercase = false;
//                    options.Password.RequireNonAlphanumeric = false;
//                    options.Password.RequireUppercase = false;
//                    options.Password.RequiredLength = 6;
//                    options.Password.RequiredUniqueChars = 0;

//                    // Lockout settings.
//                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
//                    options.Lockout.MaxFailedAccessAttempts = 5;
//                    options.Lockout.AllowedForNewUsers = true;

//                    // User settings.
//                    options.User.AllowedUserNameCharacters =
//                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+\\";
//                    options.User.RequireUniqueEmail = false;
//                });

//                services.ConfigureApplicationCookie(options =>
//                {
//                    // Cookie settings
//                    options.Cookie.HttpOnly = true;
//                    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

//                    options.LoginPath = "/Identity/Account/Login";
//                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//                    options.SlidingExpiration = true;
//                });

//                services.AddSingleton<IEmailSender, EmailSender>();


//            });
//        }
//    }

//    public class AppUser : IdentityUser<int>
//    {
        
//    }

//    public class AppRole : IdentityRole<int>
//    {
//        public AppRole()
//        {
            
//        }

//        public AppRole(string roleName)
//            : this()
//        {
//            base.Name = roleName;
//        }

//    }

//    public class EmailSender : IEmailSender
//    {
//        public Task SendEmailAsync(string email, string subject, string message)
//        {
//            return Task.CompletedTask;
//        }
//    }
//}