using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.GroupsService.Storage
{
    public class Report
    {
        [Key] public int Id { get; set; }
        [Required] public int ProjectId { get; set; }
        [Required] public int TaskId { get; set; }
        [Required] public int UserId { get; set; }
        public string Done { get; set; }
        public string Planned { get; set; }
        public string Issues { get; set; }
        public double? Points { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }

        public virtual Project Project { get; set; }
        public virtual Task Task { get; set; }
    }
}