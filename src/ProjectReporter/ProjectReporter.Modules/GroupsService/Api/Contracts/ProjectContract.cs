namespace ProjectReporter.Modules.GroupsService.Api.Contracts
{
    public class ProjectContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GitLink { get; set; }
    }
}