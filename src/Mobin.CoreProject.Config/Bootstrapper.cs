﻿using System;
using System.Threading.Tasks;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Sql;
using Alamut.Data.Sql.EF;
using Alamut.Data.Sql.EF.Repositories;
using Alamut.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mobin.CoreProject.CrossCutting.Security.Helper;
using Mobin.CoreProject.CrossCutting.Security.Models;
using Mobin.CoreProject.CrossCutting.Security.Services;
using Mobin.CoreProject.Data;
using Mobin.CoreProject.Service.AppServices;

namespace Mobin.CoreProject.Config
{
    public static class Bootstrapper
    {
        public static void ConfigDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("AppDbConnection")));
            services.AddScoped<DbContext, AppDbContext>();
        }

        public static void AddAlamut(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));

            Mapper.Initialize(option => option.AddProfiles(typeof(Bootstrapper).Assembly));
        }

        

        public static void AddRepositories(this IServiceCollection services)
        {

        }

        public static void AddAppServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                //.FromAssemblies(assemblies)
                .FromAssemblyOf<ForestService>()
                .AddClasses(classes => classes.AssignableTo(typeof(ICrudService<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        /// <summary>
        /// use it just for windows authentication 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCustomClaims(this IServiceCollection services) =>
            services.AddScoped<IClaimsTransformation, AppClaimsTransformer>();

        public static void AddIdentity(this IServiceCollection services, bool isWindowsAuthentication = true)
        {
            services
                .AddIdentity<AppUser, AppRole>()
                .AddDefaultTokenProviders()
                //.AddDefaultIdentity<AppUser>()
                //.AddRoleManager<RoleManager<IdentityRole>>()
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();



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

            if (isWindowsAuthentication)
            {
                services.RegisterCustomClaims();
                services.AddScoped<IUserService, UserServiceWindowsAuth>();
            }
            else
                { services.AddScoped<IUserService, UserServiceFormAuth>(); }
        }
        
    }

    //public class EmailSender : IEmailSender
    //{
    //    public Task SendEmailAsync(string email, string subject, string message)
    //    {
    //        return Task.CompletedTask;
    //    }
    //}
}
