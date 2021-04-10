using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageGroupMemberReconstructionFactory
    {
        GroupMember Map(Storage.GroupMember member);
    }
}