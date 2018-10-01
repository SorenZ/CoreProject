using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Sql;
using Alamut.Data.Sql.EF;
using Alamut.Data.Sql.EF.Repositories;
using Alamut.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mobin.CoreProject.Core.ServiceContracts;
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
        }

        public static void AddRepositories(this IServiceCollection services)
        {

        }

        public static void AddAppServices(this IServiceCollection services)
        {
            //services.Scan(scan => scan
            //    .FromAssemblyOf<BlogService>()
            //    .AddClasses(classes => classes.InNamespaceOf<BlogService>())
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());



            //var assemblies = Assembly
            //    .GetEntryAssembly()
            //    .GetReferencedAssemblies()
            //    .Select(Assembly.Load)
            //    .Where(a => a.FullName.StartsWith("Mobin"));

            

            //var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            //    .Where(a => a.FullName.StartsWith("Mobin"));

            //var assemblies = GetAssemblies();

            services.Scan(scan => scan
                //.FromAssemblies(assemblies)
                .FromAssemblyOf<BlogService>()
                .AddClasses(classes => classes.AssignableTo(typeof(ICrudService<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }


        public static IEnumerable<Assembly> GetAssemblies()
        {
            var list = new List<string>();
            var stack = new Stack<Assembly>();

            stack.Push(Assembly.GetEntryAssembly());

            do
            {
                var asm = stack.Pop();

                yield return asm;

                foreach (var reference in asm.GetReferencedAssemblies())
                    if (!list.Contains(reference.FullName))
                    {
                        stack.Push(Assembly.Load(reference));
                        list.Add(reference.FullName);
                    }

            }
            while (stack.Count > 0);

        }
    }
}
