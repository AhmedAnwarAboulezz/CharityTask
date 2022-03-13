using Common.StandardInfrastructure.Repository;
using Orders.Data;
using Orders.Data.SeedData;
using Orders.DataAccess;
using Orders.Service.AutoMapper;
using Orders.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Orders.Service.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ServicesRegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.DatabaseConfig(configuration);
            services.AddAutoMapper(typeof(OrdersProfile));
            services.ServicesConfig();
            return services;
        }
        private static void DatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
             var connectionString = configuration.GetConnectionString("EASTIPostreq");
            services.AddDbContext<OrdersContext>
                (options => options.UseNpgsql(connectionString));
            // //Un comment if you want use sql server connection 
            // //1-Remove files in Migrations Folder
            // //2- open PackageManager Console
            // //3-Run add-migration init_Db
            // //4-Run update-database
            //var connectionStringSql = configuration.GetConnectionString("EASTISql");
            //services.AddDbContext<OrdersContext>
            //     (options => options.UseSqlServer(connectionStringSql));
            services.AddScoped<DbContext, OrdersContext>();
        }
        private static void ServicesConfig(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IDataInitialize, DataInitialize>();
            var assemblyToScan = Assembly.GetAssembly(typeof(ProductService));
            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan).Where(c => c.Name.EndsWith("Service"))
              .AsPublicImplementedInterfaces();
        }
    }
}
