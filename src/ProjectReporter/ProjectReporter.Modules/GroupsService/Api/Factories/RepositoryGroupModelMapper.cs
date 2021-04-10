using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public class RepositoryGroupModelMapper : IRepositoryGroupModelMapper
    {
        public Group Map(GroupContract contract, string ownerId) =>
            new(contract.Name,
                contract.Description,
                contract.Status,
                ownerId,
                contract.CoOwnerId,
                contract.GitLink,
                id: contract.Id);
    }
}