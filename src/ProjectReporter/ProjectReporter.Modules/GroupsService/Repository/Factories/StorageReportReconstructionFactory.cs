using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageReportReconstructionFactory : IStorageReportReconstructionFactory
    {
        public Report Create(Storage.Report report) =>
            new(report.TaskId, 
                report.ProjectId,
                report.Done,
                report.Planned,
                report.Issues,
                report.UserId,
                report.Points,
                report.Id);
    }
}