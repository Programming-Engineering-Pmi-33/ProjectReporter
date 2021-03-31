

using System.Threading.Tasks;
using ProjectReporter.Modules.GroupsService.Api.Contracts;

namespace ProjectReporter.Modules.GroupsService.Api
{
    public interface IGroupsApi
    {
        Task CreateGroup(GroupContract contract, string ownerId);
        Task AddCoOwner(int groupId, string ownerId, string coOwnerId);
        Task Invite(int groupId, string ownerId, params string[] usersIds);
        Task AcceptInvitation(int groupId, string invitation, string userId);
        Task CreateProject(int groupId, ProjectContract contract);
        Task AddUsersToProject(int groupId, int projectId, string ownerId, params string[] usersIds);
        Task EditProject(ProjectContract contract, string userId);
        Task CreateTask(int groupId, TaskContract contract, string userId);
        Task EditTask(TaskContract contract, string userId);
        Task CreateReport(int taskId, ReportContract contract, string userId);
        Task EditReport(int taskId, ReportContract contract, string userId);
        Task EvaluateReport(int reportId, double points, string userId);
    }
}