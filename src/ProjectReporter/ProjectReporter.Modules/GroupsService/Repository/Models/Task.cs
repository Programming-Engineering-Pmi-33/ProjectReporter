using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectReporter.Modules.GroupsService.Repository.Models
{
    public record Task
    {
        public int Id { get; }
        public int GroupId { get; }
        public string Name { get; }
        public string Description { get; }
        public int Points { get; }
        public IReadOnlyCollection<Report> Reports { get; }
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }


        public Task(int groupId,
            string name,
            string description,
            int points,
            DateTime startDate,
            DateTime endDate,
            IEnumerable<Report> reports = null,
            int id = 0)
        {
            GroupId = groupId;
            Name = name;
            Description = description;
            Points = points;
            StartDateTime = startDate;
            EndDateTime = endDate;
            Reports = new ReadOnlyCollection<Report>(reports?.ToList() ?? new List<Report>());
            Id = id;
        }

        //Validation
        public Task Update(Task updatedTask) =>
            new(GroupId,
                updatedTask.Name,
                updatedTask.Description,
                updatedTask.Points,
                updatedTask.StartDateTime,
                updatedTask.EndDateTime,
                Reports,
                Id);
    }
}