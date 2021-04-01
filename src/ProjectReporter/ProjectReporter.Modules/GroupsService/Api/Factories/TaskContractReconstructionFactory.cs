using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public class TaskContractReconstructionFactory : ITaskContractReconstructionFactory
    {
        public TaskContract Create(Task task) =>
            new()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Points = task.Points
            };
    }
}