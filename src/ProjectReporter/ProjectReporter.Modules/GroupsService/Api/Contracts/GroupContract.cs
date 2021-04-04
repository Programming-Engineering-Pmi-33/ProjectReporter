namespace ProjectReporter.Modules.GroupsService.Api.Contracts
{
    public class GroupContract
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string CoOwnerId { get; set; }
        public string GitLink { get; set; }
        public string[] MembersIds { get; set; }
    }
}