using ProjectReporter.Modules.GroupsService.Repository.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.Modules.GroupsService.Repository
{
    public interface IGroupsRepository
    {
        Task AddGroup(Group group);
    }
}