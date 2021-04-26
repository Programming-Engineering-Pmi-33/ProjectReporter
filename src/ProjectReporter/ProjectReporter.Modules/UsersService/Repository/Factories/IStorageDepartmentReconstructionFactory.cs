using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Repository.Factories
{
    public interface IStorageDepartmentReconstructionFactory
    {
        Department Create(Storage.Department department);
    }
}