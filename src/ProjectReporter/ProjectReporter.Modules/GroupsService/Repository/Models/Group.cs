using System;
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

        public Group AddMembers(string inviterId, params string[] ids)
        {
            var guid = Guid.NewGuid();
            var members = ids.Select(m => new GroupMember(m, inviterId, guid.ToString(), false));
            return new Group(Name, Description, Status, OwnerId, CoOwnerId, GitLink, Projects, Tasks, members, Id);
        }

        public Group Join(string userId, string invitation)
        {
            var members = Members.ToArray();
            members.First(m => m.UserId == userId && m.Guid == invitation).ActivateMember();
            return new Group(Name, Description, Status, OwnerId, CoOwnerId, GitLink, Projects, Tasks, members, Id);
        }

        public Group CreateProject(string name, string description, string gitLink)
        {
            var newProject = new Project(name, description, gitLink, new string[0]);
            var updatedProjects = Projects.ToList();
            updatedProjects.Add(newProject);
            return new Group(Name, Description, Status, OwnerId, CoOwnerId, GitLink, updatedProjects, Tasks, Members, Id);
        }

        public Group AddUsersToProject(int projectId, params string[] usersIds)
        {
            var projects = Projects.ToList();
            projects.First(p => p.Id == projectId).AddUsers(usersIds);
            return new Group(Name, Description, Status, OwnerId, CoOwnerId, GitLink, projects, Tasks, Members, Id);
        }

        public Group CreateTask(string name, string description, int points)
        {
            var newTask = new Task(name, description, points, new Report[0]);
            var updatedTasks = Tasks.ToList();
            updatedTasks.Add(newTask);
            return new Group(Name, Description, Status, OwnerId, CoOwnerId, GitLink, Projects, updatedTasks, Members, Id);
        }
    }
}