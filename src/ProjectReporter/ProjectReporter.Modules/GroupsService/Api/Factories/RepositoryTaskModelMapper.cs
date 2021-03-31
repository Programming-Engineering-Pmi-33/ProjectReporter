using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public class RepositoryTaskModelMapper: IRepositoryTaskModelMapper
    {
        public Task Map(TaskContract contract) =>
            new(contract.Name,
                contract.Description,
                contract.Points,
                id: contract.Id);
    }
}