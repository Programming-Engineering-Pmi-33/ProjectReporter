using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Repository.Factories
{
    public class StorageAcademicGroupReconstructionFactory : IStorageAcademicGroupReconstructionFactory
    {
        public AcademicGroup Create(Storage.AcademicGroup academicGroup) =>
            new(academicGroup.Name, academicGroup.FacultyId, academicGroup.Id);
    }
}