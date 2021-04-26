using System.Linq;
using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Repository.Factories
{
    public class StorageFacultyReconstructionFactory : IStorageFacultyReconstructionFactory
    {
        private readonly IStorageAcademicGroupReconstructionFactory _groupsReconstructionFactory;
        private readonly IStorageDepartmentReconstructionFactory _departmentsReconstructionFactory;
        public StorageFacultyReconstructionFactory(
            IStorageAcademicGroupReconstructionFactory groupsReconstructionFactory,
            IStorageDepartmentReconstructionFactory departmentsReconstructionFactory)
        {
            _groupsReconstructionFactory = groupsReconstructionFactory;
            _departmentsReconstructionFactory = departmentsReconstructionFactory;
        }
        public Faculty Create(Storage.Faculty faculty)
        {
            var groups = faculty.AcademicGroups.Select(_groupsReconstructionFactory.Create);
            var departments = faculty.Departments.Select(_departmentsReconstructionFactory.Create);
            return new(faculty.Name, departments, groups, faculty.Id);
        }
    }
}