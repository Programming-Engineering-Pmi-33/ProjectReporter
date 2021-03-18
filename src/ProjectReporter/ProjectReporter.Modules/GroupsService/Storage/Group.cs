using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.GroupsService.Storage
{
    public class Group
    {
        [Key] public int Id { get; set; }//
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required] public int Status { get; set; }
        [Required] public int OwnerId { get; set; }
        public int? CoOwnerId { get; set; }
        public string GitLink { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }//
        [Required] public DateTime DateTimeModified { get; set; }//

        public List<Project> Projects { get; set; }
        public List<Task> Tasks { get; set; }
        public List<GroupMember> Members { get; set; }
    }
}
