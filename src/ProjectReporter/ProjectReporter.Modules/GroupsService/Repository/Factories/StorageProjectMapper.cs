using System.Collections.Generic;
using System.Linq;
using ProjectReporter.Modules.GroupsService.Storage;

namespace ProjectReporter.Modules.GroupsService.Repository.Factories
{
    public class StorageProjectMapper : IStorageProjectMapper
    {
        public Project Map(Models.Project project)
        {
            var storageProject = new Project();
            Map(project, storageProject);
            return storageProject;
        }

        public void Map(Models.Project modelProject, Project storageProject)
        {
            storageProject.Id = modelProject.Id;
            storageProject.GroupId = modelProject.GroupId;
            storageProject.Name = modelProject.Name;
            storageProject.GitLink = modelProject.GitLink;
            storageProject.Description = modelProject.Description;
            MapMembers(modelProject, storageProject);
        }

        private static void MapMembers(Models.Project modelProject, Project storageProject)
        {
            var membersIds = modelProject.MembersIds ?? new string[0];
            storageProject.Members ??= new List<ProjectMember>();
            var newMembersIds = membersIds.Where(m => storageProject.Members.All(sm => sm.UserId != m));
            storageProject.Members.RemoveAll(m => !membersIds.Contains(m.UserId));

            foreach (var memberId in newMembersIds)
            {
                storageProject.Members.Add(new ProjectMember()
                {
                    ProjectId = storageProject.Id,
                    UserId = memberId
                });
            }
        }
    }
}