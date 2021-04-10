using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageGroupMapper : IStorageGroupMapper
    {
        public Group Map(Models.Group group)
        {
            var storageGroup = new Group();
            Map(group, storageGroup);
            return storageGroup;
        }

        public void Map(Models.Group modelGroup, Group storageGroup)
        {
            storageGroup.Id = modelGroup.Status;
            storageGroup.Name = modelGroup.Name;
            storageGroup.Description = modelGroup.Description;
            storageGroup.GitLink = modelGroup.GitLink;
            storageGroup.OwnerId = modelGroup.OwnerId;
            storageGroup.CoOwnerId = modelGroup.CoOwnerId;
            storageGroup.Status = modelGroup.Status;
        }
    }
}