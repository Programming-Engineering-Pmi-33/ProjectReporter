namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public record Report
    {
        public int Id { get; }
        public string Done { get; }
        public string Planned { get; }
        public string Issues { get; }
        public double? Points { get; }

        public Report(string done, string planned, string issues, double? points = null, int id = 0)
        {
            Id = id;
            Done = done;
            Planned = planned;
            Issues = issues;
            Points = points;
        }

        public Report Update(string done, string planned, string issues)
        {
            return new(done, planned, issues, Points, Id);
        }

        public Report Evaluate(double? points)
        {
            return new(Done, Planned, Issues, points, Id);
        }
    }
}