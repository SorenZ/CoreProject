using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mobin.CoreProject.CrossCutting.Security.Helper;
using Mobin.CoreProject.CrossCutting.Security.Models;
using Mobin.CoreProject.CrossCutting.Security.Services;
using Mobin.CoreProject.CrossCutting.Security.SSOT;

namespace Mobin.CoreProject.CrossCutting.Security
{
    public static class IdentityBootstrapper
    {
        
        public static void RegisterIdentity<TDbContext>(this IServiceCollection services, bool isWindowsAuthentication) 
            where TDbContext : DbContext
        {
            services
                .AddIdentity<AppUser, AppRole>()
                .AddDefaultTokenProviders()
                //.AddDefaultIdentity<AppUser>()
                //.AddRoleManager<RoleManager<IdentityRole>>()
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TDbContext>();



            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+\\";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            // services.AddSingleton<IEmailSender, EmailSender>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();

            if (isWindowsAuthentication)
            {
                IdentityConfig.IsWindowsAuth = true;
                services.RegisterCustomClaims();
            }
        }

        /// <summary>
        /// use it just for windows authentication 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCustomClaims(this IServiceCollection services) =>
            services.AddScoped<IClaimsTransformation, AppClaimsTransformer>();

        
    }

    //public class EmailSender : IEmailSender
    //{
    //    public Task SendEmailAsync(string email, string subject, string message)
    //    {
    //        return Task.CompletedTask;
    //    }
    //}
}
