using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectReporter.Modules.GroupsService.Exceptions;
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
            IStorageReportMapper storageReportMapper,
            IStorageGroupMemberMapper storageGroupMemberMapper)
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
            _storageGroupMemberMapper = storageGroupMemberMapper;
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
                g.OwnerId == userId || g.CoOwnerId == userId || g.Members.Any(m => m.UserId == userId))
                .ToArrayAsync();

            return groups.Length is 0
                ? throw new GroupsNotFoundException()
                : groups.Select(g => _groupReconstructionFactory.Create(g)).ToArray();
        }

        public async Task UpdateGroup(Group group)
        {
            var storageGroupToUpdate = await _storage.GetGroups().FirstAsync(g => g.Id == group.Id);
            if (storageGroupToUpdate == null) throw new GroupsNotFoundException(group.Id);
            _storageGroupMapper.Map(group, storageGroupToUpdate);
            _storage.Update(storageGroupToUpdate);
            await _storage.SaveChangesAsync();
        }

        public async Task<GroupMember[]> GetInvites(string userId)
        {
            var invites = await _storage.GetGroups()
                .Where(g => g.Members.Exists(m => m.UserId == userId && !m.IsActive))
                .ToArrayAsync();

            return invites.Length is 0
                ? throw new InvitesNotFoundException()
                : invites
                    .Select(m => _groupMemberReconstructionFactory.Map(m.Members.First(u => u.UserId == userId)))
                    .ToArray();
        }

        public async Task<Project> GetProject(int projectId)
        {
            var project = await _storage.GetProjects()
                .FirstOrDefaultAsync(p => p.Id == projectId);

            return project is null
                ? throw new ProjectsNotFoundException(projectId)
                : _projectReconstructionFactory.Create(project);
        }

        public async Task<Project[]> GetProjects(int groupId, string userId)
        {
            var group = await _storage.GetGroups().FirstOrDefaultAsync(g => g.Id == groupId);
            if (group is null) throw new GroupsNotFoundException(groupId);
            var projects = group.Projects.Where(p => p.Members.Exists(m => m.UserId == userId)).ToArray();
            if (projects.Length is 0) throw new ProjectsNotFoundException();
            return projects.Select(p => _projectReconstructionFactory.Create(p)).ToArray();
        }

        public async Task UpdateProject(Project project)
        {
            var storageProject = await _storage.GetProjects().FirstOrDefaultAsync(p => p.Id == project.Id);
            if (storageProject is null) throw new ProjectsNotFoundException(project.Id);
            _storageProjectMapper.Map(project, storageProject);
            _storage.Projects.Update(storageProject);
            await _storage.SaveChangesAsync();
        }

        public async Task<Models.Task> GetTask(int taskId)
        {
            var task = await _storage.GetTasks()
                .FirstOrDefaultAsync(t => t.Id == taskId);

            return task is null
                ? throw new TasksNotFoundException(taskId)
                : _taskReconstructionFactory.Create(task);
        }

        public async Task UpdateTask(Models.Task task)
        {
            var storageTask = await _storage.GetTasks().FirstOrDefaultAsync(t => t.Id == task.Id);
            if (storageTask is null) throw new TasksNotFoundException(task.Id);
            _storageTaskMapper.Map(task, storageTask);
            _storage.Tasks.Update(storageTask);
            await _storage.SaveChangesAsync();
        }

        public async Task<Report[]> GetReports(string userId = null)
        {
            var reports = userId is null
                ? await _storage.GetReports()
                    .ToArrayAsync()
                : await _storage.GetReports()
                    .Where(r => r.UserId == userId)
                    .ToArrayAsync();

            if (reports.Length is 0) throw new ReportsNotFoundException();

            return reports.Select(r => _reportReconstructionFactory.Create(r)).ToArray();
        }

        public async Task<Report> GetReport(int reportId)
        {
            var report = await _storage.GetReports().FirstOrDefaultAsync(r => r.Id == reportId);

            return report is null
                ? throw new ReportsNotFoundException(reportId)
                : _reportReconstructionFactory.Create(report);
        }

        public async Task UpdateReport(Report report)
        {
            var storageReport = await _storage.GetReports().FirstOrDefaultAsync(r => r.Id == report.Id);
            if (storageReport is null) throw new ReportsNotFoundException(report.Id);
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
            if (storageMember is null) throw new MemberIsNotFoundException(member.UserId);
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
            var group = await _storage.GetGroups().FirstOrDefaultAsync(g => g.Id == groupId);
            if (group is null) throw new GroupsNotFoundException(groupId);
            return _groupReconstructionFactory.Create(group);
        }

        //Is update required?
    }
}