namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public record Report
    {
        public int Id { get; }
        public int TaskId { get; }
        public int ProjectId { get; }
        public string Done { get; }
        public string Planned { get; }
        public string Issues { get; }
        public double? Points { get; }
        public string UserId { get; }

        public Report(int taskId,
            int projectId,
            string done,
            string planned,
            string issues,
            string userId,
            double? points = null,
            int id = 0)
        {
            TaskId = taskId;
            ProjectId = projectId;
            Id = id;
            Done = done;
            Planned = planned;
            Issues = issues;
            Points = points;
            UserId = userId;
        }

        public Report Update(string done, string planned, string issues)
        {
            return new(TaskId, ProjectId, done, planned, issues, UserId, Points, Id);
        }

        public Report Evaluate(double? points)
        {
            return new(TaskId, ProjectId, Done, Planned, Issues, UserId, points, Id);
        }
    }
}