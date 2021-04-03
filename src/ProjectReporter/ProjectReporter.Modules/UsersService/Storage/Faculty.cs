using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class Faculty
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }

        public virtual List<Department> Departments { get; set; }
        public virtual List<AcademicGroup> AcademicGroups { get; set; }
    }
}