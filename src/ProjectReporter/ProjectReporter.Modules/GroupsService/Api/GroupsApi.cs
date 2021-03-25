using System.Threading.Tasks;
using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Api.Factories;
using ProjectReporter.Modules.GroupsService.Repository;

namespace ProjectReporter.Modules.GroupsService.Api
{
    public class GroupsApi : IGroupsApi
    {
        private readonly IGroupsRepository _repository;
        private readonly IRepositoryTaskModelMapper _taskModelMapper;
        private readonly IRepositoryProjectModelMapper _projectModelMapper;
        private readonly IRepositoryGroupModelMapper _groupModelMapper;
        private readonly IRepositoryReportModelMapper _reportModelMapper;

        public GroupsApi(IGroupsRepository repository,
            IRepositoryTaskModelMapper taskModelMapper,
            IRepositoryProjectModelMapper projectModelMapper,
            IRepositoryGroupModelMapper groupModelMapper,
            IRepositoryReportModelMapper reportModelMapper)
        {
            _repository = repository;
            _taskModelMapper = taskModelMapper;
            _projectModelMapper = projectModelMapper;
            _groupModelMapper = groupModelMapper;
            _reportModelMapper = reportModelMapper;
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

        public async Task EditProject(int groupId, ProjectContract contract, string userId)
        {
            //Validation
            var group = await _repository.GetGroup(groupId);
            var updatedGroup = group.UpdateProject(contract.Id, contract.Name, contract.Description,contract.GitLink);
            await _repository.UpdateGroup(updatedGroup);
        }

        public async Task CreateTask(int groupId, TaskContract contract, string userId)
        {
            //Validation
            var group = await _repository.GetGroup(groupId);
            var updatedGroup = group.CreateTask(contract.Name, contract.Description, contract.Points);
            await _repository.UpdateGroup(updatedGroup);
        }

        public Task EditTask(TaskContract contract, string ownerId)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateReport(int taskId, ReportContract contract, string ownerId)
        {
            throw new System.NotImplementedException();
        }

        public Task EditReport(int taskId, ReportContract contract, string ownerId)
        {
            throw new System.NotImplementedException();
        }

        public Task EvaluateReport(int reportId, int points, string ownerId)
        {
            throw new System.NotImplementedException();
        }
    }
}