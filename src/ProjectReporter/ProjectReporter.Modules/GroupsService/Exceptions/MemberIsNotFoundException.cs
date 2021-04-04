namespace ProjectReporter.Modules.GroupsService.Exceptions
{
    public class MemberIsNotFoundException : GroupsServiceException
    {
        public string MemberId { get; }

        public MemberIsNotFoundException(string memberId)
        {
            MemberId = memberId;
        }
    }
}