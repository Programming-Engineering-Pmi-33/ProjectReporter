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
        private readonly IGroupContractReconstructionFactory _groupContractReconstructionFactory;
        private readonly IProjectContractReconstructionFactory _projectContractReconstructionFactory;
        private readonly ITaskContractReconstructionFactory _taskContractReconstructionFactory;
        private readonly IReportContractReconstructionFactory _reportContractReconstructionFactory;

        public GroupsApi(IGroupsRepository repository,
            IRepositoryGroupModelMapper groupModelMapper, IGroupContractReconstructionFactory groupContractReconstructionFactory, IProjectContractReconstructionFactory projectContractReconstructionFactory, ITaskContractReconstructionFactory taskContractReconstructionFactory, IReportContractReconstructionFactory reportContractReconstructionFactory)
        {
            _repository = repository;
            _groupModelMapper = groupModelMapper;
            _groupContractReconstructionFactory = groupContractReconstructionFactory;
            _projectContractReconstructionFactory = projectContractReconstructionFactory;
            _taskContractReconstructionFactory = taskContractReconstructionFactory;
            _reportContractReconstructionFactory = reportContractReconstructionFactory;
        }

        public async Task CreateGroup(GroupContract contract, string ownerId)
        {
            if ((await _repository.GetGroups(ownerId)).Any(g => g.Name == contract.Name))
            {
                throw new NameAlreadyUsedException();
            }
            var group = _groupModelMapper.Map(contract, ownerId);
            await _repository.AddGroup(group);
        }

        public async Task<GroupContract[]> GetGroups(string userId)
        {
            return (await _repository.GetGroups(userId)).Select(g => _groupContractReconstructionFactory.Create(g))
                .ToArray();
        }

        public async Task AddCoOwner(int groupId, string ownerId, string coOwnerId)
        {
            var group = await _repository.GetGroup(groupId);
            if (group.OwnerId != ownerId) throw new UserNotOwnerException();
            //Other user is coOwner?
            var updatedGroup = group.AddCoOwner(coOwnerId);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task Invite(int groupId, string ownerId, params string[] usersIds)
        {
            var group = await _repository.GetGroup(groupId);
            if (group.OwnerId != ownerId) throw new UserNotOwnerException();
            if (group.Members.Any(m => usersIds.Contains(m.UserId)))
            {
                throw new UserAlreadyJoinedException(group.Members.Where(m => usersIds.Contains(m.UserId))
                    .Select(m => m.UserId));
            }
            var updatedGroup = group.AddMembers(ownerId, usersIds);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task AcceptInvitation(int groupId, string invitation, string userId)
        {
            var group = await _repository.GetGroup(groupId);
            if (group.Members.Any(m => m.UserId == userId && m.IsActive)) throw new UserAlreadyJoinedException();

            var updatedGroup = group.Join(userId, invitation);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task<ProjectContract[]> GetProjects(int groupId, string userId)
        {
            var group = await _repository.GetGroup(groupId);
            return group.Projects.Select(p => _projectContractReconstructionFactory.Create(p)).ToArray();
        }

        public async Task CreateProject(int groupId, ProjectContract contract, string ownerId)
        {
            var group = await _repository.GetGroup(groupId);
            if (group.Projects.Any(p => p.Name == contract.Name)) throw new NameAlreadyUsedException();
            if (group.OwnerId != ownerId && group.CoOwnerId != ownerId) throw new UserNotOwnerException();
            var updatedGroup = group.CreateProject(contract.Name, contract.Description, contract.GitLink);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task AddUsersToProject(int groupId, int projectId, string ownerId, params string[] usersIds)
        {
            var group = await _repository.GetGroup(groupId);
            var project = group.Projects.First(p => p.Id == projectId);
            if (group.OwnerId != ownerId && group.CoOwnerId != ownerId) throw new UserNotOwnerException();
            if (usersIds.Any(u => group.Members.Any(m => m.UserId == u))) throw new UserNotFoundException();
            if (usersIds.Any(u => project.MembersIds.Contains(u))) throw new UserAlreadyJoinedException();
            var updatedGroup = group.AddUsersToProject(projectId, usersIds);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task EditProject(ProjectContract contract, string userId)
        {
            var project = await _repository.GetProject(contract.Id, userId);
            //Name used. User owner.
            var updatedProject = project.Update(contract.Name, contract.Description, contract.GitLink);
            await _repository.UpdateProject(updatedProject);
        }

        public async Task<TaskContract[]> GetTasks(int groupId, string userId)
        {
            var group = await _repository.GetGroup(groupId);
            return group.Tasks.Select(t => _taskContractReconstructionFactory.Create(t)).ToArray();
        }

        public async Task CreateTask(int groupId, TaskContract contract, string ownerId)
        {
            var group = await _repository.GetGroup(groupId);
            if (group.OwnerId != ownerId && group.CoOwnerId != ownerId) throw new UserNotOwnerException();
            if (group.Tasks.Any(t => t.Name == contract.Name)) throw new NameAlreadyUsedException();
            var updatedGroup = group.CreateTask(contract.Name, contract.Description, contract.Points);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task EditTask(TaskContract contract, string userId)
        {
            var task = await _repository.GetTask(contract.Id, userId);
            //Name used. User owner.
            var updatedTask = task.Update(contract.Name, contract.Description, contract.Points);
            await _repository.UpdateTask(updatedTask);
        }

        public async Task<ReportContract[]> GetReports(int taskId, string userId)
        {
            var task = await _repository.GetTask(taskId, userId);
            return task.Reports.Select(r => _reportContractReconstructionFactory.Create(r)).ToArray();
        }

        public async Task CreateReport(int taskId, ReportContract contract, string userId)
        {
            var task = await _repository.GetTask(taskId, userId);
            if (task.Reports.Any(r => r.UserId == userId)) throw new UserAlreadyJoinedException();
            var report = new Report(contract.Done, contract.Planned, userId, contract.Issues);
            var updatedTask = task.AddReport(report);
            await _repository.UpdateTask(updatedTask);
        }

        public async Task EditReport(int taskId, ReportContract contract, string userId)
        {
            var report = await _repository.GetReport(contract.Id, userId);
            if (report.Points != null) throw new AlreadyEvaluatedReportException();
            var updatedReport = report.Update(contract.Done, contract.Planned, contract.Issues);
            await _repository.UpdateReport(updatedReport);
        }

        public async Task EvaluateReport(int reportId, double points, string userId)
        {
            //Validation
            var report = await _repository.GetReport(reportId, userId);
            var updatedReport = report.Evaluate(points);
            await _repository.UpdateReport(updatedReport);
        }
    }
}