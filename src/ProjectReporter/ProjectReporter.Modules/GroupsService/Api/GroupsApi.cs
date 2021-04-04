using System;
using System.Linq;
using System.Threading.Tasks;
using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Api.Factories;
using ProjectReporter.Modules.GroupsService.Exceptions;
using ProjectReporter.Modules.GroupsService.Repository;
using ProjectReporter.Modules.GroupsService.Repository.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.Modules.GroupsService.Api
{
    public class GroupsApi : IGroupsApi
    {
        private readonly IGroupsRepository _repository;
        private readonly IRepositoryGroupModelMapper _groupModelMapper;
        private readonly IRepositoryProjectModelMapper _projectModelMapper;
        private readonly IRepositoryReportModelMapper _reportModelMapper;
        private readonly IRepositoryTaskModelMapper _taskModelMapper;

        private readonly IGroupContractReconstructionFactory _groupContractReconstructionFactory;
        private readonly IProjectContractReconstructionFactory _projectContractReconstructionFactory;
        private readonly ITaskContractReconstructionFactory _taskContractReconstructionFactory;
        private readonly IReportContractReconstructionFactory _reportContractReconstructionFactory;

        public GroupsApi(IGroupsRepository repository,
            IRepositoryGroupModelMapper groupModelMapper,
            IGroupContractReconstructionFactory groupContractReconstructionFactory,
            IProjectContractReconstructionFactory projectContractReconstructionFactory,
            ITaskContractReconstructionFactory taskContractReconstructionFactory,
            IReportContractReconstructionFactory reportContractReconstructionFactory,
            IRepositoryProjectModelMapper projectModelMapper,
            IRepositoryReportModelMapper reportModelMapper,
            IRepositoryTaskModelMapper taskModelMapper)
        {
            _repository = repository;
            _groupModelMapper = groupModelMapper;
            _groupContractReconstructionFactory = groupContractReconstructionFactory;
            _projectContractReconstructionFactory = projectContractReconstructionFactory;
            _taskContractReconstructionFactory = taskContractReconstructionFactory;
            _reportContractReconstructionFactory = reportContractReconstructionFactory;
            _projectModelMapper = projectModelMapper;
            _reportModelMapper = reportModelMapper;
            _taskModelMapper = taskModelMapper;
        }

        public async Task CreateGroup(GroupContract contract, string ownerId)
        {
            if ((await _repository.GetGroups(ownerId)).Any(g => g.Name == contract.Name))
            {
                throw new NameAlreadyUsedException(contract.Name);
            }
            var group = _groupModelMapper.Map(contract, ownerId);
            await _repository.AddGroup(group);
        }

        public async Task<GroupContract[]> GetGroups(string userId)
        {
            return (await _repository.GetGroups(userId)).Select(g => _groupContractReconstructionFactory.Create(g))
                .ToArray();
        }

        public async Task UpdateGroup(GroupContract contract, string ownerId)
        {
            var group = _groupModelMapper.Map(contract, ownerId);
            var storageGroup = await GetGroup(contract.Id, ownerId);
            var updatedGroup = storageGroup.Update(group);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task AddCoOwner(int groupId, string ownerId, string coOwnerId)
        {
            var storageGroup = await GetGroup(groupId, ownerId);
            var updatedGroup = storageGroup.AddCoOwner(coOwnerId);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task RemoveCoOwner(int groupId, string ownerId)
        {
            var storageGroup = await GetGroup(groupId, ownerId);
            var updatedGroup = storageGroup.RemoveCoOwner();
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task Invite(int groupId, string ownerId, params string[] usersIds)
        {
            var storageGroup = await GetGroup(groupId, ownerId);
            if (storageGroup.Members.Any(m => usersIds.Contains(m.UserId)))
            {
                throw new UserAlreadyJoinedException(storageGroup.Members.Where(m => usersIds.Contains(m.UserId))
                    .Select(m => m.UserId));
            }

            var members = usersIds.Select(i => new GroupMember(groupId, i, ownerId, Guid.NewGuid().ToString(), false));
            await _repository.AddMembersToGroup(members);
        }

        public async Task AcceptInvitation(int groupId, string invitation, string userId)
        {
            var group = await _repository.GetGroup(groupId);
            if (group.Members.Any(m => m.UserId == userId && invitation == m.Guid && m.IsActive))
                throw new UserAlreadyJoinedException();

            var updatedGroupMember = group.Members.FirstOrDefault(g => g.UserId == userId)?.ActivateMember();
            if (updatedGroupMember == null)
            {
                throw new UserNotFoundException();
            }

            await _repository.UpdateGroupMember(updatedGroupMember);
        }

        public async Task<ProjectContract[]> GetProjects(int groupId, string userId)
        {
            var projects = await _repository.GetProjects(groupId, userId);
            return projects.Select(p => _projectContractReconstructionFactory.Create(p)).ToArray();
        }

        public async Task CreateProject(ProjectContract contract, string ownerId)
        {
            var group = await GetGroup(contract.GroupId, ownerId);
            if (group.Projects.Any(p => p.Name == contract.Name)) throw new NameAlreadyUsedException(contract.Name);
            var project = _projectModelMapper.Map(contract);
            await _repository.AddProject(project);
        }

        public async Task AddUsersToProject(int groupId, int projectId, string ownerId, params string[] usersIds)
        {
            var group = await GetGroup(groupId, ownerId);
            var project = group.Projects.First(p => p.Id == projectId);
            if (usersIds.Any(u => group.Members.All(m => m.UserId != u))) throw new UserNotFoundException();
            if (usersIds.Any(u => project.MembersIds.Contains(u))) throw new UserAlreadyJoinedException();
            var updatedProject = project.AddUsers(usersIds);
            await _repository.UpdateProject(updatedProject);
        }

        public async Task EditProject(ProjectContract contract, string userId)
        {
            var storageGroup = await GetGroup(contract.GroupId, userId);
            if (storageGroup.Projects.Any(p => p.Name == contract.Name && p.Id != contract.Id))
            {
                throw new GroupsModelException(nameof(contract.Name));
            }

            var storageProject = await _repository.GetProject(contract.Id);
            var modelProject = _projectModelMapper.Map(contract);
            var updatedProject = storageProject.Update(modelProject);
            await _repository.UpdateProject(updatedProject);
        }

        public async Task<TaskContract[]> GetTasks(int groupId, string userId)
        {
            var group = await _repository.GetGroup(groupId);
            if (!group.Members.Select(m => m.UserId).Contains(userId)) throw new UserNotFoundException();
            return group.Tasks.Select(t => _taskContractReconstructionFactory.Create(t)).ToArray();
        }

        public async Task CreateTask(TaskContract contract, string ownerId)
        {
            var group = await _repository.GetGroup(contract.GroupId);
            if (group.OwnerId != ownerId && group.CoOwnerId != ownerId) throw new UserNotOwnerException();
            if (group.Tasks.Any(t => t.Name == contract.Name)) throw new NameAlreadyUsedException(contract.Name);
            var task = _taskModelMapper.Map(contract);
            await _repository.AddTask(task);
        }

        public async Task EditTask(TaskContract contract, string userId)
        {
            var group = await GetGroup(contract.GroupId, userId);
            if (group.Tasks.Any(p => p.Name == contract.Name && p.Id != contract.Id)) throw new NameAlreadyUsedException(contract.Name);

            var storageTask = group.Tasks.FirstOrDefault(t => t.Id == contract.Id);
            if (storageTask is null) throw new TasksNotFoundException(contract.Id);
            var task = _taskModelMapper.Map(contract);
            var updatedTask = storageTask.Update(task);
            await _repository.UpdateTask(updatedTask);
        }

        public async Task<ReportContract[]> GetReports(int taskId, string userId)
        {
            var task = await _repository.GetTask(taskId);
            var reports = task.Reports.Where(r => r.UserId == userId).ToArray();
            if (reports.Length is 0) throw new ReportsNotFoundException();

            return reports.Select(r => _reportContractReconstructionFactory.Create(r)).ToArray();
        }

        public async Task CreateReport(ReportContract contract, string userId)
        {
            var task = await _repository.GetTask(contract.TaskId);
            var group = await _repository.GetGroup(task.GroupId);
            if (group.Members.All(m => m.UserId != userId || !m.IsActive)) throw new UserNotFoundException();
            var project = group.Projects.FirstOrDefault(p => p.Id == contract.ProjectId);
            if (project is null) throw new ProjectsNotFoundException(contract.Id);
            if (!project.MembersIds.Contains(userId)) throw new UserNotFoundException();

            if (task.Reports.Any(r => r.UserId == userId)) throw new UserAlreadyJoinedException();
            var report = _reportModelMapper.Map(contract, userId);
            await _repository.AddReport(report);
        }

        public async Task EditReport(ReportContract contract, string userId)
        {
            var storageReport = await _repository.GetReport(contract.Id);
            var modelReport = _reportModelMapper.Map(contract, userId);
            var updatedReport = storageReport.Update(modelReport);
            await _repository.UpdateReport(updatedReport);
        }

        public async Task EvaluateReport(int groupId, int reportId, double points, string userId)
        {
            await GetGroup(groupId, userId);
            var report = await _repository.GetReport(reportId);
            var task = await _repository.GetTask(report.TaskId);
            if (task.Points < points) throw new IncorrectPointsException();
            var updatedReport = report.Evaluate(points);
            await _repository.UpdateReport(updatedReport);
        }

        private async Task<Group> GetGroup(int groupId, string ownerId, bool canBeCoOwner = true)
        {
            var group = await _repository.GetGroup(groupId);
            if (group.OwnerId != ownerId || canBeCoOwner && group.CoOwnerId == ownerId)
            {
                throw new UserNotOwnerException();
            }

            return group;
        }
    }
}