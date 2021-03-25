using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageReportReconstructionFactory
    {
        Report Create(Storage.Report report);
    }
}