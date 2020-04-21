using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Elwark.Extensions.AspNet
{
    public static class HostExtensions
    {
        public static IHost MigrateDbContext<TContext>(this IHost host) where TContext : DbContext =>
            MigrateDbContext<TContext>(host, (_, __) => { });

        public static IHost MigrateDbContext<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILogger<TContext>>();

            var context = services.GetService<TContext>();
            var contextName = typeof(TContext).Name;

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", contextName);

                context.Database.Migrate();

                logger.LogInformation("Migrated database associated with context {DbContextName}", contextName);

                logger.LogInformation("Seeding database with context {DbContextName}", contextName);

                seeder(context, services);

                logger.LogInformation("Seeded database with context {DbContextName}", contextName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "An error occurred while migrating the database used on context {DbContextName}", contextName);
            }

            return host;
        }
        
        public static Task<IHost> MigrateDbContextAsync<TContext>(this IHost host) where TContext : DbContext =>
            MigrateDbContextAsync<TContext>(host, (_, __) => Task.CompletedTask);
        
        public static async Task<IHost> MigrateDbContextAsync<TContext>(this IHost host, Func<TContext, IServiceProvider, Task> seeder)
            where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILogger<TContext>>();

            var context = services.GetService<TContext>();
            var contextName = typeof(TContext).Name;

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", contextName);

                await context.Database.MigrateAsync();

                logger.LogInformation("Migrated database associated with context {DbContextName}", contextName);

                logger.LogInformation("Seeding database with context {DbContextName}", contextName);

                await seeder(context, services);

                logger.LogInformation("Seeded database with context {DbContextName}", contextName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", contextName);
            }

            return host;
        }
    }
}