using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.Modules.UsersService.Api.Factories
{
    public interface IStorageUserModelMapper
    {
        Student Map(StudentRegisterContract contract);
        Teacher Map(TeacherRegisterContract contract);
    }
}