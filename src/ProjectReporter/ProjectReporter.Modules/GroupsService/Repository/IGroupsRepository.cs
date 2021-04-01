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
        Task<Project> GetProject(int projectId, string userId);
        Task UpdateProject(Project project);
        Task<Models.Task> GetTask(int taskId, string userId);
        Task UpdateTask(Models.Task task);
        Task<Report> GetReport(int reportId, string userId);
        Task UpdateReport(Report report);
    }
}