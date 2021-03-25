namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public record Report
    {
        public int Id { get; }
        public string Done { get; }
        public string Planned { get; }
        public string Issues { get; }
        public double? Points { get; }

        public Report(string done, string planned, string issues, double? points, int id)
        {
            Id = id;
            Done = done;
            Planned = planned;
            Issues = issues;
            Points = points;
        }
    }
}