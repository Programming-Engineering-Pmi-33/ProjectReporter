namespace ProjectReporter.Modules.GroupsService.Api.Contracts
{
    public class ReportContract
    {
        public int Id { get; set; }
        public string Done { get; set; }
        public string Planned { get; set; }
        public string Issues { get; set; }
    }
}