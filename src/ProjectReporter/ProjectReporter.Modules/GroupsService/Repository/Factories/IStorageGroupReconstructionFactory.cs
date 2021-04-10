using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageGroupReconstructionFactory
    {
        Group Create(Storage.Group group);
    }
}