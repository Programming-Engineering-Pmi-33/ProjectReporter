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

        public Task AddCoOwner(int groupId, string ownerId, string coOwnerId)
        {
            throw new System.NotImplementedException();
        }

        public Task Invite(int groupId, int academicGroupId)
        {
            throw new System.NotImplementedException();
        }

        public Task Invite(int groupId, params int[] usersIds)
        {
            throw new System.NotImplementedException();
        }

        public Task AcceptInvitation(int groupId, string invitation, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateProject(int groupId, ProjectContract contract)
        {
            throw new System.NotImplementedException();
        }

        public Task AddUsersToProject(int projectId, string ownerId, params string[] usersIds)
        {
            throw new System.NotImplementedException();
        }

        public Task EditProject(ProjectContract contract, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateTask(int groupId, TaskContract contract, string userId)
        {
            throw new System.NotImplementedException();
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