using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public interface IStorageGroupMapper
    {
        Group Map(Models.Group group);
        Group Map(Group storageGroup, Models.Group updatedGroup);
    }
}