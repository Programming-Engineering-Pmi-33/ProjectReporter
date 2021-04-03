using System.Collections.Generic;
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
        private readonly GroupsStorage _storage;

        private readonly IStorageGroupReconstructionFactory _groupReconstructionFactory;
        private readonly IStorageGroupMemberReconstructionFactory _groupMemberReconstructionFactory;
        private readonly IStorageProjectReconstructionFactory _projectReconstructionFactory;
        private readonly IStorageTaskReconstructionFactory _taskReconstructionFactory;
        private readonly IStorageReportReconstructionFactory _reportReconstructionFactory;

        private readonly IStorageGroupMapper _storageGroupMapper;
        private readonly IStorageProjectMapper _storageProjectMapper;
        private readonly IStorageTaskMapper _storageTaskMapper;
        private readonly IStorageReportMapper _storageReportMapper;
        private readonly IStorageGroupMemberMapper _storageGroupMemberMapper;

        public GroupsRepository(GroupsStorage storage,
            IStorageGroupReconstructionFactory groupReconstructionFactory,
            IStorageGroupMemberReconstructionFactory groupMemberReconstructionFactory,
            IStorageProjectReconstructionFactory projectReconstructionFactory,
            IStorageTaskReconstructionFactory taskReconstructionFactory,
            IStorageReportReconstructionFactory reportReconstructionFactory,
            IStorageGroupMapper storageGroupMapper,
            IStorageProjectMapper storageProjectMapper,
            IStorageTaskMapper storageTaskMapper,
            IStorageReportMapper storageReportMapper)
        {
            _storageGroupMapper = storageGroupMapper;
            _groupReconstructionFactory = groupReconstructionFactory;
            _storage = storage;
            _groupMemberReconstructionFactory = groupMemberReconstructionFactory;
            _projectReconstructionFactory = projectReconstructionFactory;
            _taskReconstructionFactory = taskReconstructionFactory;
            _reportReconstructionFactory = reportReconstructionFactory;
            _storageProjectMapper = storageProjectMapper;
            _storageTaskMapper = storageTaskMapper;
            _storageReportMapper = storageReportMapper;
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
            _storageGroupMapper.Map(group, storageGroupToUpdate);
            _storage.Update(storageGroupToUpdate);
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
            return _projectReconstructionFactory.Create(await _storage.GetProjects()
                .FirstAsync(p => p.Id == projectId));
        }

        public async Task UpdateProject(Project project)
        {
            //Validation
            var storageProject = await _storage.GetProjects().FirstOrDefaultAsync(p => p.Id == project.Id);
            _storageProjectMapper.Map(project, storageProject);
            _storage.Projects.Update(storageProject);
            await _storage.SaveChangesAsync();
        }

        public async Task<Models.Task> GetTask(int taskId)
        {
            //Task validation
            return _taskReconstructionFactory.Create(await _storage.GetTasks()
                .FirstOrDefaultAsync(t => t.Id == taskId));
        }

        public async Task UpdateTask(Models.Task task)
        {
            //Validation
            var storageTask = await _storage.GetTasks().FirstOrDefaultAsync(t => t.Id == task.Id);
            _storageTaskMapper.Map(task, storageTask);
            _storage.Tasks.Update(storageTask);
            await _storage.SaveChangesAsync();
        }

        public async Task<Report> GetReport(int reportId)
        {
            //Validation
            return _reportReconstructionFactory.Create(await _storage.GetReports()
                .FirstOrDefaultAsync(r => r.Id == reportId));
        }

        public async Task UpdateReport(Report report)
        {
            var storageReport = await _storage.GetReports().FirstOrDefaultAsync(r => r.Id == report.Id);
            _storageReportMapper.Map(report, storageReport);
            _storage.Reports.Update(storageReport);
            await _storage.SaveChangesAsync();
        }

        public async Task AddMembersToGroup(IEnumerable<GroupMember> members)
        {
            var storageMembers = members.Select(m => _storageGroupMemberMapper.Map(m));
            await _storage.GroupMembers.AddRangeAsync(storageMembers);
            await _storage.SaveChangesAsync();
        }

        public async Task UpdateGroupMember(GroupMember member)
        {
            var storageMember = await _storage.GroupMembers.FirstOrDefaultAsync(m => m.Id == member.Id);
            //Validation
            _storageGroupMemberMapper.Map(member, storageMember);
            await _storage.SaveChangesAsync();
        }

        public async Task AddProject(Project project)
        {
            var storageProject = _storageProjectMapper.Map(project);
            await _storage.Projects.AddAsync(storageProject);
            await _storage.SaveChangesAsync();
        }

        public async Task AddTask(Models.Task task)
        {
            var storageTask = _storageTaskMapper.Map(task);
            await _storage.Tasks.AddAsync(storageTask);
            await _storage.SaveChangesAsync();
        }

        public async Task AddReport(Report report)
        {
            var storageReport = _storageReportMapper.Map(report);
            await _storage.Reports.AddAsync(storageReport);
            await _storage.SaveChangesAsync();
        }

        public async Task<Group> GetGroup(int groupId)
        {
            //Validation
            return _groupReconstructionFactory.Create(await _storage.GetGroups()
                .FirstOrDefaultAsync(g => g.Id == groupId));
        }

        //Get all
        //Get by Id

        //Is update required.
    }
}