using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageGroupMemberMapper
    {
        GroupMember Map(Models.GroupMember member);
        void Map(Models.GroupMember modelMember, GroupMember storageMember);
    }
}