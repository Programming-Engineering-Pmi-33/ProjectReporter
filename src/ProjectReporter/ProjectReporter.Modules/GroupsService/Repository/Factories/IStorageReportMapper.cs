using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageReportMapper
    {
        Report Map(Models.Report report);
        void Map(Models.Report modelReport, Report storageReport);
    }
}