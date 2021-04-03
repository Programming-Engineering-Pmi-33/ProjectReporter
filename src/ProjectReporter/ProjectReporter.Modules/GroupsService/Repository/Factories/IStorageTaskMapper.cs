using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageTaskMapper
    {
        Task Map(Models.Task task);
        void Map(Models.Task modelTask, Task storageTask);
    }
}