using System.Threading.Tasks;
using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Api
{
    public interface IUsersApi
    {
        Task Register(StudentRegisterContract contract);
        Task Register(TeacherRegisterContract contract);
        Task Login(UserLoginContract contract);
        Task Logout();
        Task<User[]> GetUsers(params string[] ids);
        Task<Student[]> GetStudents(int academicGroupId);
        Task<Teacher[]> GetTeachers(int facultyId, string[] ids = null);
    }
}