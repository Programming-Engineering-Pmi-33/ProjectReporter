using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.Modules.UsersService.Api.Factories
{
    public class StorageUserModelMapper: IStorageUserModelMapper
    {
        public Student Map(StudentRegisterContract contract)
        {
            return new()
            {
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                Email = contract.Email,
                GitLink = contract.GitLink,
                UserName = contract.Email,
                GroupId = contract.GroupId
            };
        }

        public Teacher Map(TeacherRegisterContract contract)
        {
            return new()
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