using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Repository.Factories
{
    public class StorageDepartmentReconstructionFactory: IStorageDepartmentReconstructionFactory
    {
        public Department Create(Storage.Department department)
        {
            return new(department.Name, department.FacultyId, department.Id);
        }
    }
}