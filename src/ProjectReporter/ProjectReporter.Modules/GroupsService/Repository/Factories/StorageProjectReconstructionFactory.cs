using System.Linq;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageProjectReconstructionFactory: IStorageProjectReconstructionFactory
    {
        public Project Create(Storage.Project project) =>
            new(project.Name,
                project.Description,
                project.GitLink,
                project.Members.Select(m => m.UserId)
                    .ToArray(),
                project.Id);
    }
}