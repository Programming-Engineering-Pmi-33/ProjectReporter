using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectReporter.Modules.UsersService.Api;
using ProjectReporter.Modules.UsersService.Api.Factories;
using ProjectReporter.Modules.UsersService.Repository;
using ProjectReporter.Modules.UsersService.Repository.Factories;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.Modules.UsersService
{
    public static class UsersServiceDependencyRegistrar
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            services.AddTransient(typeof(IUsersApi), typeof(UsersApi));
            services.AddTransient(typeof(IStorageUserModelMapper), typeof(StorageUserModelMapper));
            services.AddTransient(typeof(IStorageUserReconstructionFactory), typeof(StorageUserReconstructionFactory));
            services.AddTransient(typeof(IUsersRepository), typeof(UsersRepository));
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Storage.UsersStorage>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    mySqlOptions =>
                    {
                        mySqlOptions.MigrationsAssembly("ProjectReporter.Modules");
                        mySqlOptions.EnableRetryOnFailure(15, TimeSpan.FromMilliseconds(2000), null!);
                    }), ServiceLifetime.Transient);

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<UsersStorage>();
        }
    }
}