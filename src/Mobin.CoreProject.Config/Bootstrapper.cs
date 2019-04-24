using System;
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
using Mobin.CoreProject.CrossCutting.Security;
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

        public static void RegisterIdentity(this IServiceCollection services, bool isWindowsAuthentication)
            => services.RegisterIdentity<AppDbContext>(isWindowsAuthentication);

    }
    
}
