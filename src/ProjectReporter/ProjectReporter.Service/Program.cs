using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectReporter.Service.Infrastructure.Database;

namespace ProjectReporter.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                if (args[0] == "--update-database")
                {
                    UpdateDatabase(args);
                    return;
                }
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void UpdateDatabase(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var updater = scope.ServiceProvider.GetService<IDatabaseUpdater>();
            updater?.UpdateDatabase();
        }
    }
}
