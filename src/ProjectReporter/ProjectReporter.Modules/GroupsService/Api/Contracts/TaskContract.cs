namespace ProjectReporter.Modules.GroupsService.Api.Contracts
{
    public class TaskContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
    }
}