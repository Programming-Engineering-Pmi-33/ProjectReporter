using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Repository.Factories
{
    public interface IStorageAcademicGroupReconstructionFactory
    {
        AcademicGroup Create(Storage.AcademicGroup academicGroup);
    }
}