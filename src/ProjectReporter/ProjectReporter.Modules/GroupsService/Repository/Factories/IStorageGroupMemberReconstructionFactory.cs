using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageGroupMemberReconstructionFactory
    {
        GroupMember Create(Storage.GroupMember member);
    }
}