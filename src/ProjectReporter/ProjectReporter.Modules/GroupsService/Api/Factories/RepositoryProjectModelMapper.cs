using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public class RepositoryProjectModelMapper : IRepositoryProjectModelMapper
    {
        public Project Map(ProjectContract contract) =>
            new(contract.GroupId,
                contract.Name,
                contract.Description,
                contract.GitLink,
                id: contract.Id);
    }
}