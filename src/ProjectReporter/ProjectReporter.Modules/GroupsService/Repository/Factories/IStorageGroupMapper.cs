using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageGroupMapper
    {
        Group Map(Models.Group group);
        void Map(Models.Group modelGroup, Group storageGroup);
    }
}