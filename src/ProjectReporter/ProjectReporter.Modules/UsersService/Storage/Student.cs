using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class Student
    {
        [Key] public int Id { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public int GroupId { get; set; }
        public string GitLink { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }

        public virtual User User { get; set; }
        public virtual AcademicGroup Group { get; set; }
    }
}