using FluentAssertions;
using NUnit.Framework;
using ProjectReporter.Modules.UsersService.Repository.Factories;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.UnitTests.UsersService.Repository.Factories
{
    [TestFixture]
    public class StorageUserReconstructionFactory_When_Creating
    {
        [Test]
        public void If_CalledWithUserInstance_Then_UserModelReturned()
        {
            // Arrange
            var storageUser = new User
            {
                Id = "id",
                Email = "user@example.com",
                FirstName = "name",
                LastName = "surname"
            };
            var expected = new Modules.UsersService.Repository.Models.User("user@example.com", "name", "surname", "id");
            var factory = new StorageUserReconstructionFactory();

            // Act
            var result = factory.Create(storageUser);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void If_CalledWithTeacherInstance_Then_TeacherModelReturned()
        {
            // Arrange
            var storageTeacher = new Teacher
            {
                Id = "id",
                Email = "user@example.com",
                FirstName = "name",
                LastName = "surname",
                DepartmentId = 1
            };
            var expected = new Modules.UsersService.Repository.Models.Teacher("user@example.com", "name", "surname", 1, "id");
            var factory = new StorageUserReconstructionFactory();

            // Act
            var result = factory.Create(storageTeacher);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void If_CalledWithStudentInstance_Then_StudentModelReturned()
        {
            // Arrange
            var storageStudent = new Student
            {
                Id = "id",
                Email = "user@example.com",
                FirstName = "name",
                LastName = "surname",
                GitLink = "gitLink",
                GroupId = 1
            };
            var expected = new Modules.UsersService.Repository.Models.Student("user@example.com",
                "name",
                "surname",
                1,
                "gitLink",
                "id");
            var factory = new StorageUserReconstructionFactory();


            // Act
            var result = factory.Create(storageStudent);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}