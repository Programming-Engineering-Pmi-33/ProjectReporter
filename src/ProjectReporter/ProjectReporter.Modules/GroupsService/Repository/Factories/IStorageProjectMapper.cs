using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageProjectMapper
    {
        Project Map(Models.Project project);
        void Map(Models.Project modelProject, Project storageProject);
    }
}