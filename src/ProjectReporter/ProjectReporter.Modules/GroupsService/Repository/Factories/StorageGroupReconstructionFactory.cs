using System.Linq;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageGroupReconstructionFactory : IStorageGroupReconstructionFactory
    {
        private readonly IStorageProjectReconstructionFactory _projectReconstructionFactory;
        private readonly IStorageTaskReconstructionFactory _taskReconstructionFactory;
        private readonly IStorageGroupMemberReconstructionFactory _groupMemberReconstructionFactory;
        public StorageGroupReconstructionFactory(IStorageProjectReconstructionFactory projectReconstructionFactory,
            IStorageTaskReconstructionFactory taskReconstructionFactory,
            IStorageGroupMemberReconstructionFactory groupMemberReconstructionFactory)
        {
            _projectReconstructionFactory = projectReconstructionFactory;
            _taskReconstructionFactory = taskReconstructionFactory;
            _groupMemberReconstructionFactory = groupMemberReconstructionFactory;
        }

        public Group Create(Storage.Group group) =>
            new(group.Name,
                group.Description,
                group.Status,
                group.OwnerId,
                group.CoOwnerId,
                group.GitLink,
                group.Projects.Select(p => _projectReconstructionFactory.Create(p))
                    .ToArray(),
                group.Tasks.Select(t => _taskReconstructionFactory.Create(t))
                    .ToArray(),
                group.Members.Select(m => _groupMemberReconstructionFactory.Map(m))
                    .ToArray(),
                group.Id);
    }
}