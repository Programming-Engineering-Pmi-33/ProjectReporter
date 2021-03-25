using System.Linq;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageTaskReconstructionFactory : IStorageTaskReconstructionFactory
    {
        private readonly IStorageReportReconstructionFactory _reportReconstructionFactory;

        public StorageTaskReconstructionFactory(IStorageReportReconstructionFactory reportReconstructionFactory)
        {
            _reportReconstructionFactory = reportReconstructionFactory;
        }
        public Task Create(Storage.Task task) =>
            new(task.Name, task.Description, task.Points,
                task.Reports.Select(r => _reportReconstructionFactory.Create(r)).ToArray(), task.Id);
    }
}