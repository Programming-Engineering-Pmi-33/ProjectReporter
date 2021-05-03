using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Repository.Factories
{
    public interface IStorageFacultyReconstructionFactory
    {
        Faculty Create(Storage.Faculty faculty);
    }
}