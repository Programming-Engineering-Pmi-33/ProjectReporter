using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.Modules.UsersService.Api.Factories
{
    public interface IStorageUserModelMapper
    {
        User Map(StudentRegisterContract contract);
        User Map(TeacherRegisterContract contract);
    }
}