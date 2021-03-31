using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public record Task
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Points { get; }
        public IReadOnlyCollection<Report> Reports { get; }

        public Task(string name, string description, int points, IEnumerable<Report> reports = null, int id = 0)
        {
            Name = name;
            Description = description;
            Points = points;
            Reports = new ReadOnlyCollection<Report>(reports?.ToList() ?? new List<Report>());
            Id = id;
        }

        public Task Update(string name, string description, int points)
        {
            return new(name, description, points, Reports, Id);
        }

        public Task AddReport(Report report)
        {
            var reports = Reports.ToList();
            reports.Add(report);
            return new Task(Name, Description, Points, reports, Id);
        }
    }
}