using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Repository.Factories
{
    public class StorageUserReconstructionFactory : IStorageUserReconstructionFactory
    {
        public User Create(Storage.User user) =>
            new(user.Email, user.FirstName, user.LastName, user.Id);

        public Teacher Create(Storage.Teacher teacher) =>
            new(teacher.Email,
                teacher.FirstName,
                teacher.LastName,
                teacher.DepartmentId,
                teacher.Id);

        public Student Create(Storage.Student student) =>
            new(student.Email,
                student.FirstName,
                student.LastName,
                student.GroupId,
                student.GitLink,
                student.Id);
    }
}