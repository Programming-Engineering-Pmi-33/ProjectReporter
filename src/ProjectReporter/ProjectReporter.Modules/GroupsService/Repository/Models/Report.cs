using ProjectReporter.Modules.GroupsService.Exceptions;

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

        public Report Update(Report report)
        {
            if (UserId != report.UserId) throw new UserNotOwnerException();
            if (Points != null || report.Points != null) throw new AlreadyEvaluatedReportException();
            if (TaskId != report.TaskId) throw new GroupsModelException(nameof(TaskId));
            if (ProjectId != report.ProjectId) throw new GroupsModelException(nameof(ProjectId));
            if (Id != report.Id) throw new GroupsModelException(nameof(Id));

            return new Report(TaskId, ProjectId, report.Done, report.Planned, report.Issues, UserId, Points, Id);
        }

        public Report Evaluate(double points)
        {
            if (points < 0) throw new IncorrectPointsException();
            return new Report(TaskId, ProjectId, Done, Planned, Issues, UserId, points, Id);
        }
    }
}