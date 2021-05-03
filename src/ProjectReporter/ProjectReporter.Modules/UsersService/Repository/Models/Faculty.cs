using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectReporter.Modules.UsersService.Repository.Models
{
    public record Faculty
    {
        public int Id { get; }
        public string Name { get; }
        public virtual IReadOnlyCollection<Department> Departments { get; }
        public virtual IReadOnlyCollection<AcademicGroup> AcademicGroups { get; }

        public Faculty(string name, IEnumerable<Department> departments = null,
            IEnumerable<AcademicGroup> academicGroups = null, int id = 0)
        {
            Id = id;
            Name = name;
            Departments = new ReadOnlyCollection<Department>(departments?.ToArray() ?? Array.Empty<Department>());
            AcademicGroups =
                new ReadOnlyCollection<AcademicGroup>(academicGroups?.ToArray() ?? Array.Empty<AcademicGroup>());
        }
    }
}