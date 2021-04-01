using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectReporter.Modules.GroupsService.Repository.Factories;
using ProjectReporter.Modules.GroupsService.Storage;
using Group = ProjectReporter.Modules.GroupsService.Repository.Models.Group;
using GroupMember = ProjectReporter.Modules.GroupsService.Repository.Models.GroupMember;
using Project = ProjectReporter.Modules.GroupsService.Repository.Models.Project;
using Report = ProjectReporter.Modules.GroupsService.Repository.Models.Report;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.Modules.GroupsService.Repository
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly IStorageGroupMapper _storageGroupMapper;
        private readonly IStorageGroupReconstructionFactory _groupReconstructionFactory;
        private readonly IStorageGroupMemberReconstructionFactory _groupMemberReconstructionFactory;
        private readonly IStorageProjectReconstructionFactory _projectReconstructionFactory;
        private readonly GroupsStorage _storage;

        public GroupsRepository(GroupsStorage storage, IStorageGroupMapper storageGroupMapper, IStorageGroupReconstructionFactory groupReconstructionFactory, IStorageGroupMemberReconstructionFactory groupMemberReconstructionFactory, IStorageProjectReconstructionFactory projectReconstructionFactory)
        {
            _storageGroupMapper = storageGroupMapper;
            _groupReconstructionFactory = groupReconstructionFactory;
            _storage = storage;
            _groupMemberReconstructionFactory = groupMemberReconstructionFactory;
            _projectReconstructionFactory = projectReconstructionFactory;
        }

        public async Task AddGroup(Group group)
        {
            var storageGroup = _storageGroupMapper.Map(group);
            await _storage.Groups.AddAsync(storageGroup);
            await _storage.SaveChangesAsync();
        }

        public async Task<Group[]> GetGroups(string userId)
        {
            var groups = await _storage.GetGroups().Where(g =>
                g.OwnerId == userId || g.CoOwnerId == userId || g.Members.Exists(m => m.UserId == userId))
                .ToArrayAsync();

            return groups.Select(g => _groupReconstructionFactory.Create(g)).ToArray();
        }

        public async Task UpdateGroup(Group group)
        {
            var storageGroupToUpdate = await _storage.GetGroups().FirstAsync(g => g.Id == group.Id);
            var updatedStorageGroup = _storageGroupMapper.Map(storageGroupToUpdate, group);
            _storage.Update(updatedStorageGroup);
            await _storage.SaveChangesAsync();
        }

        public async Task<GroupMember[]> GetInvites(string userId)
        {
            return
                (await _storage.GetGroups().Where(g => g.Members.Exists(m => m.UserId == userId && !m.IsActive))
                    .ToArrayAsync()).Select(m =>
                    _groupMemberReconstructionFactory.Map(m.Members.First(u => u.UserId == userId)))
                .ToArray();
        }

        public async Task<Project> GetProject(int projectId, string userId)
        {
            //Group validation.
            return _projectReconstructionFactory.Create(await _storage.Projects.FirstAsync(p => p.Id == projectId));
        }

        public Task UpdateProject(Project project)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Task> GetTask(int taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTask(Models.Task task)
        {
            throw new NotImplementedException();
        }

        public Task<Report> GetReport(int reportId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReport(Report report)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> GetGroup(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}