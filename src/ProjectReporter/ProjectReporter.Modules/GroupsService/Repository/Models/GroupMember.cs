using ProjectReporter.Modules.GroupsService.Exceptions;

namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public class GroupMember
    {
        public int Id { get; }
        public int GroupId { get; }
        public string UserId { get; }
        public string InviterId { get; }
        public string Guid { get; }
        public bool IsActive { get; }

        public GroupMember(int groupId, string userId, string inviterId, string guid, bool isActive, int id = 0)
        {
            Id = id;
            GroupId = groupId;
            UserId = userId;
            InviterId = inviterId;
            Guid = guid;
            IsActive = isActive;
        }

        public GroupMember ActivateMember()
        {
            if (IsActive) throw new GroupsModelException(nameof(IsActive));
            return new GroupMember(GroupId, UserId, InviterId, Guid, true, Id);
        }
    }
}