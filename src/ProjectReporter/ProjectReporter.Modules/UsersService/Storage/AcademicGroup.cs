using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class AcademicGroup
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int DepartmentId { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }

        public virtual Department Department { get; set; }
    }
}