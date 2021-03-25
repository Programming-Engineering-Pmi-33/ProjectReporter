using System;
using System.Threading.Tasks;
using ProjectReporter.Modules.GroupsService.Repository.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.Modules.GroupsService.Repository
{
    public class GroupsRepository : IGroupsRepository
    {
        public async Task AddGroup(Group group)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateGroup(Group group)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<Group> GetGroup(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}