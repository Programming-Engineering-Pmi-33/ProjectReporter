using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.GroupsService.Storage
{
    public class ProjectMember
    {
        [Key] public int Id { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public int ProjectId { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }

        public virtual Project Project { get; set; }
    }
}