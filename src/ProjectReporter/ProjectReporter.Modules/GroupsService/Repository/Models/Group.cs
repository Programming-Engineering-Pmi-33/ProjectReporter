using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public record Group
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Status { get; }
        public string OwnerId { get; }
        public string CoOwnerId { get; }
        public string GitLink { get; }
        public IReadOnlyCollection<Project> Projects { get; }
        public IReadOnlyCollection<Task> Tasks { get; }
        public IReadOnlyCollection<GroupMember> Members { get; }

        public Group(string name,
            string description,
            int status,
            string ownerId,
            string coOwnerId,
            string gitLink,
            IEnumerable<Project> projects = null,
            IEnumerable<Task> tasks = null,
            IEnumerable<GroupMember> members = null,
            int id = 0)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            OwnerId = ownerId;
            CoOwnerId = coOwnerId;
            GitLink = gitLink;
            Projects = new ReadOnlyCollection<Project>(projects?.ToList() ?? new List<Project>());
            Tasks = new ReadOnlyCollection<Task>(tasks?.ToList() ?? new List<Task>());
            Members = new ReadOnlyCollection<GroupMember>(members?.ToList() ?? new List<GroupMember>());
        }

        public Group AddCoOwner(string coOwnerId)
        {
            //Validation
            return new(Name, Description, Status, OwnerId, coOwnerId, GitLink, Projects, Tasks, Members, Id);
        }

        public Group Update(Group updated) =>
            new(Name, updated.Description, updated.Status, OwnerId, CoOwnerId, updated.GitLink, Projects,
                Tasks, Members, Id);
    }
}