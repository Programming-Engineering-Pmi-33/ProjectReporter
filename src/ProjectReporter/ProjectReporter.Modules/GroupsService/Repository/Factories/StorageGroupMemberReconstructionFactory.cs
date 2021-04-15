using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageGroupMemberReconstructionFactory : IStorageGroupMemberReconstructionFactory
    {
        public GroupMember Create(Storage.GroupMember member) =>
            new(member.GroupId,
                member.UserId,
                member.InviterId,
                member.Guid,
                member.IsActive,
                member.Id);
    }
}