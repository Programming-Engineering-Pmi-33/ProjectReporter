using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectReporter.Modules.GroupsService.Api;
using ProjectReporter.Modules.GroupsService.Api.Factories;
using ProjectReporter.Modules.GroupsService.Repository;
using ProjectReporter.Modules.GroupsService.Repository.Factories;

namespace ProjectReporter.Modules.GroupsService
{
    public static class GroupsServiceDependencyRegistrar
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            services.AddTransient(typeof(IStorageGroupReconstructionFactory), typeof(StorageGroupReconstructionFactory));
            services.AddTransient(typeof(IStorageProjectReconstructionFactory), typeof(StorageProjectReconstructionFactory));
            services.AddTransient(typeof(IStorageTaskReconstructionFactory), typeof(StorageTaskReconstructionFactory));
            services.AddTransient(typeof(IStorageReportReconstructionFactory), typeof(StorageReportReconstructionFactory));
            services.AddTransient(typeof(IGroupsRepository), typeof(GroupsRepository));
            services.AddTransient(typeof(IGroupsApi), typeof(GroupsApi));
            services.AddTransient(typeof(IRepositoryGroupModelMapper), typeof(RepositoryGroupModelMapper));
            services.AddTransient(typeof(IRepositoryProjectModelMapper), typeof(RepositoryProjectModelMapper));
            services.AddTransient(typeof(IRepositoryReportModelMapper), typeof(RepositoryReportModelMapper));
            services.AddTransient(typeof(IRepositoryTaskModelMapper), typeof(RepositoryTaskModelMapper));
            services.AddTransient(typeof(IStorageGroupMemberReconstructionFactory),
                typeof(StorageGroupMemberReconstructionFactory));
            services.AddTransient(typeof(IGroupContractReconstructionFactory), typeof(GroupContractReconstructionFactory));
            services.AddTransient(typeof(IProjectContractReconstructionFactory), typeof(ProjectContractReconstructionFactory));
            services.AddTransient(typeof(IReportContractReconstructionFactory), typeof(ReportContractReconstructionFactory));
            services.AddTransient(typeof(ITaskContractReconstructionFactory), typeof(TaskContractReconstructionFactory));
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Storage.GroupsStorage>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    mySqlOptions =>
                    {
                        mySqlOptions.MigrationsAssembly("ProjectReporter.Modules");
                        mySqlOptions.EnableRetryOnFailure(15, TimeSpan.FromMilliseconds(2000), null!);
                    }), ServiceLifetime.Transient);
        }
    }
}