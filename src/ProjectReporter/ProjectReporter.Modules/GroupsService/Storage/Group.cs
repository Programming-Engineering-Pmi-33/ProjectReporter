using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.GroupsService.Storage
{
    public class Group
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required] public int Status { get; set; }
        [Required] public string OwnerId { get; set; }
        public string CoOwnerId { get; set; }
        public string GitLink { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }

        public virtual List<Project> Projects { get; set; }
        public virtual List<Task> Tasks { get; set; }
        public virtual List<GroupMember> Members { get; set; }
    }
}
