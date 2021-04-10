using Task = ProjectReporter.Modules.GroupsService.Storage.Task;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageTaskMapper : IStorageTaskMapper
    {
        public Task Map(Models.Task task)
        {
            var storageTask = new Task();
            Map(task, storageTask);
            return storageTask;
        }

        public void Map(Models.Task modelTask, Task storageTask)
        {
            storageTask.Id = modelTask.Id;
            storageTask.GroupId = modelTask.GroupId;
            storageTask.Description = modelTask.Description;
            storageTask.Points = modelTask.Points;
            storageTask.Name = modelTask.Name;
            storageTask.StartDateTime = modelTask.StartDateTime;
            storageTask.EndDateTime = modelTask.EndDateTime;
        }
    }
}
