using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Repository.Factories
{
    public interface IStorageUserReconstructionFactory
    {
        User Create(Storage.User user);
        Teacher Create(Storage.Teacher teacher);
        Student Create(Storage.Student student);
    }
}