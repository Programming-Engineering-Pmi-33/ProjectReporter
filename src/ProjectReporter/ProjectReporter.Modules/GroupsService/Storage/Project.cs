using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.GroupsService.Storage
{
    public class Project
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        public string GitLink { get; set; }
        [Required] public int GroupId { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }

        public virtual Group Group { get; set; }
        public virtual List<ProjectMember> Members { get; set; }
        public virtual List<Report> Reports { get; set; }
    }
}