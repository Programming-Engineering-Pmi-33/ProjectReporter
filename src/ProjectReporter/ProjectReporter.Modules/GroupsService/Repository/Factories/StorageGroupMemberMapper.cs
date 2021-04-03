using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageGroupMemberMapper : IStorageGroupMemberMapper
    {
        public GroupMember Map(Models.GroupMember member)
        {
            var storageMember = new GroupMember();
            Map(member, storageMember);
            return storageMember;
        }

        public void Map(Models.GroupMember modelMember, GroupMember storageMember)
        {
            storageMember.GroupId = modelMember.GroupId;
            storageMember.Id = modelMember.Id;
            storageMember.Guid = modelMember.Guid;
            storageMember.InviterId = modelMember.InviterId;
            storageMember.IsActive = modelMember.IsActive;
            storageMember.UserId = modelMember.UserId;
        }
    }
}