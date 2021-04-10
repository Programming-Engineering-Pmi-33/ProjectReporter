using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class Department
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int FacultyId { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual List<Teacher> Teachers { get; set; }
    }
}