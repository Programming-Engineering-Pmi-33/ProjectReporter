using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.GroupsService.Storage
{
    public class GroupMember
    {
        [Key] public int Id { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public int GroupId { get; set; }
        [Required] public int InviterId { get; set; }
        [Required] public string Guid { get; set; }
        [Required] public bool IsActive { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }

        public virtual Group Group { get; set; }
    }
}