using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public record Task
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Points { get; }
        public IReadOnlyCollection<Report> Reports { get; }

        public Task(string name, string description, int points, Report[] reports, int id = 0)
        {
            Name = name;
            Description = description;
            Points = points;
            Reports = new ReadOnlyCollection<Report>(reports);
            Id = id;
        }
    }
}