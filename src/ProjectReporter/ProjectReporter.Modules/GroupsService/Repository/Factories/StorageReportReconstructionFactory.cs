using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageReportReconstructionFactory: IStorageReportReconstructionFactory
    {
        public Report Create(Storage.Report report) =>
            new(report.Done,
                report.Planned,
                report.Issues,
                report.Points,
                report.Id);
    }
}