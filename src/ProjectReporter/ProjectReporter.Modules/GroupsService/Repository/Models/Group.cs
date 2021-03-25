using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        public IReadOnlyCollection<string> MembersIds { get; }

        public Group(string name,
            string description,
            int status,
            string ownerId,
            string coOwnerId,
            string gitLink,
            IList<Project> projects,
            IList<Task> tasks,
            IList<string> membersIds,
            int id)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            OwnerId = ownerId;
            CoOwnerId = coOwnerId;
            GitLink = gitLink;
            Projects = new ReadOnlyCollection<Project>(projects);
            Tasks = new ReadOnlyCollection<Task>(tasks);
            MembersIds = new ReadOnlyCollection<string>(membersIds);
        }
    }
}