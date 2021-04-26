using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectReporter.Modules.UsersService.Exceptions;
using ProjectReporter.Modules.UsersService.Repository.Factories;
using ProjectReporter.Modules.UsersService.Storage;
using Student = ProjectReporter.Modules.UsersService.Repository.Models.Student;
using Teacher = ProjectReporter.Modules.UsersService.Repository.Models.Teacher;
using User = ProjectReporter.Modules.UsersService.Repository.Models.User;

namespace ProjectReporter.Modules.UsersService.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersStorage _storage;
        private readonly IStorageUserReconstructionFactory _userReconstructionFactory;
        private readonly IStorageAcademicGroupReconstructionFactory _academicGroupReconstructionFactory;
        private readonly IStorageDepartmentReconstructionFactory _departmentReconstructionFactory;
        private readonly IStorageFacultyReconstructionFactory _facultyReconstructionFactory;

        public UsersRepository(UsersStorage storage,
            IStorageUserReconstructionFactory userReconstructionFactory,
            IStorageAcademicGroupReconstructionFactory academicGroupReconstructionFactory,
            IStorageDepartmentReconstructionFactory departmentReconstructionFactory,
            IStorageFacultyReconstructionFactory facultyReconstructionFactory)
        {
            _storage = storage;
            _userReconstructionFactory = userReconstructionFactory;
            _academicGroupReconstructionFactory = academicGroupReconstructionFactory;
            _departmentReconstructionFactory = departmentReconstructionFactory;
            _facultyReconstructionFactory = facultyReconstructionFactory;
        }

        public async Task<User[]> GetUsers(params string[] ids)
        {
            var users = await _storage.Users.Where(u => ids.Contains(u.Id)).ToArrayAsync();
            if (users.Length != ids.Length)
            {
                throw new UsersNotFoundException(ids.Where(i => users.All(u => u.Id != i)).ToArray());
            }
            return users.Select(u => _userReconstructionFactory.Create(u)).ToArray();
        }

        public async Task<Student[]> GetStudents(int academicGroupId)
        {
            var students = await _storage.Students.Where(s => s.GroupId == academicGroupId).ToArrayAsync();
            return students.Select(s => _userReconstructionFactory.Create(s)).ToArray();
        }

        public async Task<Teacher[]> GetTeachers(int facultyId, string[] ids = null)
        {
            var teachers = _storage.Teachers.Include(t => t.Department)
                .Where(t => t.Department.FacultyId == facultyId);
            teachers = ids == null ? teachers : teachers.Where(t => ids.Contains(t.Id));
            if (ids != null && ids.Length != 0 && teachers.Count() != ids.Length)
            {
                throw new UsersNotFoundException(ids.Where(i => teachers.All(u => u.Id != i)).ToArray());
            }
            return (await teachers.ToArrayAsync()).Select(t => _userReconstructionFactory.Create(t)).ToArray();
        }

        public async Task<Models.Faculty[]> GetFaculties() =>
            (await _storage.FindFaculties()
                .ToArrayAsync()).Select(_facultyReconstructionFactory.Create)
            .ToArray();

        public async Task<Models.Department[]> GetDepartments(int facultyId) =>
            (await _storage.FindDepartments().Where(d => d.FacultyId == facultyId).ToArrayAsync())
            .Select(_departmentReconstructionFactory.Create).ToArray();

        public async Task<Models.AcademicGroup[]> GetAcademicGroups(int facultyId) =>
            (await _storage.FindAcademicGroups().Where(a => a.FacultyId == facultyId).ToArrayAsync())
            .Select(_academicGroupReconstructionFactory.Create).ToArray();
    }
}