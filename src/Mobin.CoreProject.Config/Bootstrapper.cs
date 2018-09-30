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
using Mobin.CoreProject.Data;

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
    }
}
