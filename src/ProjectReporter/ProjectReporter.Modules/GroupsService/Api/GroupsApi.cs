using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Api.Factories;
using ProjectReporter.Modules.GroupsService.Repository;
using ProjectReporter.Modules.GroupsService.Repository.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.Modules.GroupsService.Api
{
    public class GroupsApi : IGroupsApi
    {
        private readonly IGroupsRepository _repository;
        private readonly IRepositoryGroupModelMapper _groupModelMapper;

        public GroupsApi(IGroupsRepository repository,
            IRepositoryGroupModelMapper groupModelMapper)
        {
            _repository = repository;
            _groupModelMapper = groupModelMapper;
        }

        public async Task CreateGroup(GroupContract contract, string ownerId)
        {
            var group = _groupModelMapper.Map(contract, ownerId);
            await _repository.AddGroup(group);
        }

        public async Task AddCoOwner(int groupId, string ownerId, string coOwnerId)
        {
            var group = await _repository.GetGroup(groupId);
            //Validation
            var updatedGroup = group.AddCoOwner(coOwnerId);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task Invite(int groupId, string ownerId, params string[] usersIds)
        {
            //Validation
            var group = await _repository.GetGroup(groupId);
            var updatedGroup = group.AddMembers(ownerId, usersIds);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task AcceptInvitation(int groupId, string invitation, string userId)
        {
            //Validation
            var group = await _repository.GetGroup(groupId);
            var updatedGroup = group.Join(userId, invitation);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task CreateProject(int groupId, ProjectContract contract)
        {
            //Validation
            var group = await _repository.GetGroup(groupId);
            var updatedGroup = group.CreateProject(contract.Name, contract.Description, contract.GitLink);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task AddUsersToProject(int groupId, int projectId, string ownerId, params string[] usersIds)
        {
            //Validation
            var group = await _repository.GetGroup(groupId);
            var updatedGroup = group.AddUsersToProject(projectId, usersIds);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task EditProject(ProjectContract contract, string userId)
        {
            //Validation
            var project = await _repository.GetProject(contract.Id, userId);
            var updatedProject = project.Update(contract.Name, contract.Description, contract.GitLink);
            await _repository.UpdateProject(updatedProject);
        }

        public async Task CreateTask(int groupId, TaskContract contract, string userId)
        {
            //Validation
            var group = await _repository.GetGroup(groupId);
            var updatedGroup = group.CreateTask(contract.Name, contract.Description, contract.Points);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task EditTask(TaskContract contract, string userId)
        {
            //Validation
            var task = await _repository.GetTask(contract.Id, userId);
            var updatedTask = task.Update(contract.Name, contract.Description, contract.Points);
            await _repository.UpdateTask(updatedTask);
        }

        public async Task CreateReport(int taskId, ReportContract contract, string userId)
        {
            //Validation
            var task = await _repository.GetTask(taskId, userId);
            var report = new Report(contract.Done, contract.Planned, contract.Issues);
            var updatedTask = task.AddReport(report);
            await _repository.UpdateTask(updatedTask);
        }

        public async Task EditReport(int taskId, ReportContract contract, string userId)
        {
            //Validation
            var report = await _repository.GetReport(contract.Id, userId);
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