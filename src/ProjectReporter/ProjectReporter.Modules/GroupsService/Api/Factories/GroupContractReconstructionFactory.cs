using System.Linq;
using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public class GroupContractReconstructionFactory : IGroupContractReconstructionFactory
    {
        public GroupContract Create(Group group) =>
            new()
            {
                Id = group.Id,
                Name = group.Name,
                CoOwnerId = group.CoOwnerId,
                Description = group.Description,
                GitLink = group.GitLink,
                Status = group.Status,
                MembersIds = group.Members.Select(m => m.UserId).ToArray()
            };
    }
}