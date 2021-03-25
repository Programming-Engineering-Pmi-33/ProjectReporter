namespace ProjectReporter.Modules.GroupsService.Api.Contracts
{
    public class ReportContract
    {
        public int Id { get; }
        public string Done { get; }
        public string Planned { get; }
        public string Issues { get; }
    }
}