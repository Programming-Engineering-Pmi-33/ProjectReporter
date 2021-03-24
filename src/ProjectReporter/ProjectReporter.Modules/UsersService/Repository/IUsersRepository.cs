using System.Threading.Tasks;
using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Repository
{
    public interface IUsersRepository
    {
        Task<User[]> GetUsers(params string[] ids);
        Task<Student[]> GetStudents(int academicGroupId);
        Task<Teacher[]> GetTeachers(int facultyId, string[] ids = null);
    }
}