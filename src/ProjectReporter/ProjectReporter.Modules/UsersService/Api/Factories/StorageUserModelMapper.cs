using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.Modules.UsersService.Api.Factories
{
    public class StorageUserModelMapper: IStorageUserModelMapper
    {
        public User Map(StudentRegisterContract contract)
        {
            return new Student
            {
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                Email = contract.Email,
                GitLink = contract.GitLink,
                UserName = contract.Email,
                GroupId = contract.GroupId
            };
        }

        public User Map(TeacherRegisterContract contract)
        {
            return new Teacher()
            {
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                Email = contract.Email,
                UserName = contract.Email,
                DepartmentId = contract.DepartmentId
            };
        }
    }
}