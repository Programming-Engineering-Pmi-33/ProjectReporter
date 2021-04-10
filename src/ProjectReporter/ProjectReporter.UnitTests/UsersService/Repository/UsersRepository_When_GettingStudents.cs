using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using ProjectReporter.Modules.UsersService.Repository;
using ProjectReporter.Modules.UsersService.Repository.Factories;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.UnitTests.UsersService.Repository
{
    [TestFixture]
    public class UsersRepository_When_GettingStudents
    {
        private UsersStorage _storage;
        private Student _storageStudent;

        [SetUp]
        public async Task Setup()
        {
            _storageStudent = new Student
            {
                FirstName = "name 1",
                LastName = "surname 1",
                Email = "student1@example.com",
                UserName = "student1@example.com",
                GitLink = "gitLink 1",
                GroupId = 1,
                Id = "studentId 1"
            };

            var options = new DbContextOptionsBuilder<UsersStorage>()
                .UseInMemoryDatabase("project_reporter")
                .Options;
            _storage = new UsersStorage(options);

            _storage.Students.RemoveRange(_storage.Students.ToList());
            await _storage.SaveChangesAsync();

            await _storage.Students.AddAsync(_storageStudent);
            await _storage.SaveChangesAsync();
        }

        [TearDown]
        public void Finish()
        {
            _storage.Dispose();
        }


        [Test]
        public async Task Then_FilteredStudentModelsReturned()
        {
            // Arrange
            var wrongStudent = new Student
            {
                FirstName = "name 2",
                LastName = "surname 2",
                Email = "student2@example.com",
                UserName = "student2@example.com",
                GitLink = "gitLink 2",
                GroupId = 2,
                Id = "studentId 2"
            };
            await _storage.Users.AddAsync(wrongStudent);
            await _storage.SaveChangesAsync();

            var reconstructionFactory = Substitute.For<IStorageUserReconstructionFactory>();
            var studentModel =
                new Modules.UsersService.Repository.Models.Student("student1@example.com",
                    "name 1",
                    "surname 1",
                    1,
                    "gitLink 1",
                    "studentId 1");
            reconstructionFactory.Create(_storageStudent)
                .Returns(studentModel);
            var repository = new UsersRepository(_storage, reconstructionFactory);

            // Act
            var result = await repository.GetStudents(1);

            // Assert
            result.First().Should().Be(studentModel);
        }

        [Test]
        public async Task Then_ReconstructionFactoryCalled()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageUserReconstructionFactory>();
            var studentModel =
                new Modules.UsersService.Repository.Models.Student("student1@example.com",
                    "name 1",
                    "surname 1",
                    1,
                    "studentId 1");
            reconstructionFactory.Create(_storageStudent)
                .Returns(studentModel);
            var repository = new UsersRepository(_storage, reconstructionFactory);

            // Act
            await repository.GetStudents(1);

            // Assert
            reconstructionFactory.Received().Create(_storageStudent);
        }
    }
}