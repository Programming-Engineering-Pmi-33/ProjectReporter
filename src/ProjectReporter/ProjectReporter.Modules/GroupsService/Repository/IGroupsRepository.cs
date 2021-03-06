using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectReporter.Modules.GroupsService.Repository.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.Modules.GroupsService.Repository
{
    public interface IGroupsRepository
    {
        Task AddGroup(Group group);
        Task<Group[]> GetGroups(string userId);
        Task<Group> GetGroup(int groupId);
        Task UpdateGroup(Group group);
        Task<GroupMember[]> GetInvites(string userId);
        Task<Project> GetProject(int projectId);
        Task<Project[]> GetProjects(int groupId, string userId);
        Task UpdateProject(Project project);
        Task<Models.Task> GetTask(int taskId);
        Task UpdateTask(Models.Task task);
        Task<Report> GetReport(int reportId);
        Task UpdateReport(Report report);
        Task AddMembersToGroup(IEnumerable<GroupMember> members);
        Task UpdateGroupMember(GroupMember member);
        Task AddProject(Project project);
        Task AddTask(Models.Task task);
        Task AddReport(Report report);
        Task<Report[]> GetReports(string userId = null);
    }
}