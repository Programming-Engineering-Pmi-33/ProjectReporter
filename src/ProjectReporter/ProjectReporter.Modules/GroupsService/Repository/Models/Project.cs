using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public record Project
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string GitLink { get; }

        public IReadOnlyCollection<string> MembersIds { get; }

        public Project(string name, string description, string gitLink, IEnumerable<string> membersIds = null, int id = 0)
        {
            Name = name;
            Description = description;
            GitLink = gitLink;
            MembersIds = new ReadOnlyCollection<string>(membersIds?.ToList() ?? new List<string>());
            Id = id;
        }

        public Project AddUsers(params string[] usersIds)
        {
            var ids = MembersIds.ToList();
            ids.AddRange(usersIds);
            return new Project(Name, Description, GitLink, ids, Id);
        }

        public Project Update(string name, string description, string gitLink)
        {
            return new(name, description, gitLink, MembersIds, Id);
        }
    }
}