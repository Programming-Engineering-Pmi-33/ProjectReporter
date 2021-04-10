using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageReportMapper : IStorageReportMapper
    {
        public Report Map(Models.Report report)
        {
            var storageReport = new Report();
            Map(report, storageReport);
            return storageReport;
        }

        public void Map(Models.Report modelReport, Report storageReport)
        {
            storageReport.Id = modelReport.Id;
            storageReport.UserId = modelReport.UserId;
            storageReport.TaskId = modelReport.TaskId;
            storageReport.ProjectId = modelReport.ProjectId;
            storageReport.Done = modelReport.Done;
            storageReport.Planned = modelReport.Planned;
            storageReport.Issues = modelReport.Issues;
            storageReport.Points = modelReport.Points;
        }
    }
}