using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public record Project
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string GitLink { get; }

        public IReadOnlyCollection<string> MembersIds { get; }

        public Project(string name, string description, string gitLink, string[] membersIds, int id)
        {
            Name = name;
            Description = description;
            GitLink = gitLink;
            MembersIds = new ReadOnlyCollection<string>(membersIds);
            Id = id;
        }
    }
}