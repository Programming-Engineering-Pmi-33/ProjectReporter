using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageTaskReconstructionFactory
    {
        Task Create(Storage.Task task);
    }
}