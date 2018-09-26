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
        }
    }
}
