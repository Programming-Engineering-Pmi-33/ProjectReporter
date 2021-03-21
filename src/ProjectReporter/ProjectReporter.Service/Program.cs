using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectReporter.Service.Infrastructure;
using ProjectReporter.Service.Infrastructure.Database;

namespace ProjectReporter.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services.CreateScope();
                if (args.Contains("--update-database"))
                {
                    Console.WriteLine("Updating database.");
                    var updater = scope.ServiceProvider.GetService<IDatabaseUpdater>();
                    updater?.UpdateDatabase();
                    Console.WriteLine("Done.");
                }
                if (args.Contains("--upload-faculties"))
                {
                    Console.WriteLine("Uploading faculties.");
                    var index = args.IndexOf("--upload-faculties");
                    if (args.Length <= index + 1)
                    {
                        throw new ArgumentException("Missing parameter for file with faculties.");
                    }
                    var uploader = scope.ServiceProvider.GetService<IDatabaseUploader>();
                    uploader?.UploadFaculties(args[index + 1]);
                    Console.WriteLine("Done.");
                }
                if (args.Contains("--generate-test-data")) 
                {
                    Console.WriteLine("Generating data.");
                    var generator = scope.ServiceProvider.GetService<IDataGenerator>();
                    generator?.AddUsers(5, 5, 5);
                    Console.WriteLine("Done.");
                }

                return;
            }
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
