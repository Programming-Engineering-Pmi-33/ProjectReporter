using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectReporter.Modules.GroupsService;
using ProjectReporter.Modules.UsersService;

namespace ProjectReporter.Service.Dependencies
{
    public class DependenciesRegistrar
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly IConfiguration _configuration;

        public DependenciesRegistrar(IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            _serviceCollection = serviceCollection;
            _configuration = configuration;
        }

        public void Register()
        {
            UsersServiceDependencyRegistrar.AddServices(_serviceCollection, _configuration);
            GroupsServiceDependencyRegistrar.AddServices(_serviceCollection, _configuration);
        }
    }
}
