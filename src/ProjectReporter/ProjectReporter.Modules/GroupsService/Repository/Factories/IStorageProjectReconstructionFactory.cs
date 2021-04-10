using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageProjectReconstructionFactory
    {
        Project Create(Storage.Project project);
    }
}