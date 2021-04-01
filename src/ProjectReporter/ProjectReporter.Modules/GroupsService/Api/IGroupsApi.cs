

using System.Threading.Tasks;
using ProjectReporter.Modules.GroupsService.Api.Contracts;

namespace ProjectReporter.Modules.GroupsService.Api
{
    public interface IGroupsApi
    {
        Task CreateGroup(GroupContract contract, string ownerId);
        Task<GroupContract[]> GetGroups(string userId);
        Task AddCoOwner(int groupId, string ownerId, string coOwnerId);
        Task Invite(int groupId, string ownerId, params string[] usersIds);
        Task AcceptInvitation(int groupId, string invitation, string userId);
        Task<ProjectContract[]> GetProjects(int groupId, string userId);
        Task CreateProject(int groupId, ProjectContract contract, string userId);
        Task AddUsersToProject(int groupId, int projectId, string ownerId, params string[] usersIds);
        Task EditProject(ProjectContract contract, string userId);
        Task CreateTask(int groupId, TaskContract contract, string ownerId);
        Task<TaskContract[]> GetTasks(int groupId, string userId);
        Task EditTask(TaskContract contract, string userId);
        Task<ReportContract[]> GetReports(int groupId, string userId);
        Task CreateReport(int taskId, ReportContract contract, string userId);
        Task EditReport(int taskId, ReportContract contract, string userId);
        Task EvaluateReport(int reportId, double points, string userId);
    }
}