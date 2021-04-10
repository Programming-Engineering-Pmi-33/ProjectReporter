using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using ProjectReporter.Modules.UsersService.Exceptions;
using ProjectReporter.Modules.UsersService.Repository;
using ProjectReporter.Modules.UsersService.Repository.Factories;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.UnitTests.UsersService.Repository
{
    [TestFixture]
    public class UsersRepository_When_GettingTeachers
    {
        private UsersStorage _storage;
        private Teacher _storageTeacher;

        [SetUp]
        public async Task Setup()
        {
            _storageTeacher = new Teacher
            {
                FirstName = "name 1",
                LastName = "surname 1",
                Email = "teacher1@example.com",
                UserName = "teacher1@example.com",
                Department = new Department { FacultyId = 1, Id = 1, Name = "department 1" },
                Id = "teacherId 1"
            };

            var options = new DbContextOptionsBuilder<UsersStorage>()
                .UseInMemoryDatabase("project_reporter")
                .Options;
            _storage = new UsersStorage(options);

            _storage.Teachers.RemoveRange(_storage.Teachers.ToList());
            _storage.Departments.RemoveRange(_storage.Departments.ToList());
            await _storage.SaveChangesAsync();

            await _storage.Teachers.AddAsync(_storageTeacher);
            await _storage.SaveChangesAsync();
        }

        [TearDown]
        public void Finish()
        {
            _storage.Dispose();
        }

        [Test]
        public async Task If_CalledOnlyWithFacultyId_Then_FilteredTeacherModelsReturned()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageUserReconstructionFactory>();
            var wrongTeacher = new Teacher
            {
                FirstName = "name 2",
                LastName = "surname 2",
                Email = "teacher2@example.com",
                UserName = "teacher2@example.com",
                Department = new Department { FacultyId = 2, Id = 2, Name = "department 2" },
                Id = "teacherId 2"
            };
            await _storage.AddAsync(wrongTeacher);
            await _storage.SaveChangesAsync();

            var teacherModel =
                new Modules.UsersService.Repository.Models.Teacher("teacher1@example.com",
                    "name 1",
                    "surname 1",
                    1,
                    "teacherId 1");
            reconstructionFactory.Create(_storageTeacher)
                .Returns(teacherModel);
            var repository = new UsersRepository(_storage, reconstructionFactory);

            // Act
            var result = await repository.GetTeachers(1);

            // Assert
            result.First().Should().Be(teacherModel);
        }

        [Test]
        public async Task If_CalledWithIds_Then_FilteredTeacherModelsReturned()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageUserReconstructionFactory>();
            var wrongTeacher = new Teacher
            {
                FirstName = "name 2",
                LastName = "surname 2",
                Email = "teacher2@example.com",
                UserName = "teacher2@example.com",
                DepartmentId = 1,
                Id = "teacherId 2"
            };
            await _storage.AddAsync(wrongTeacher);
            await _storage.SaveChangesAsync();

            var teacherModel =
                new Modules.UsersService.Repository.Models.Teacher("teacher1@example.com",
                    "name 1",
                    "surname 1",
                    1,
                    "teacherId 1");
            reconstructionFactory.Create(_storageTeacher)
                .Returns(teacherModel);
            var repository = new UsersRepository(_storage, reconstructionFactory);

            // Act
            var result = await repository.GetTeachers(1, new[] { "teacherId 1" });

            // Assert
            result.First().Should().Be(teacherModel);
        }

        [Test]
        public async Task If_CalledWithIdsAndSomeNotFound_Then_ExceptionIsThrown()
        {
            // Arrange
            var teacherModel =
                new Modules.UsersService.Repository.Models.Teacher("teacher1@example.com",
                    "name 1",
                    "surname 1",
                    1,
                    "teacherId 1");
            var repository = new UsersRepository(_storage, Substitute.For<IStorageUserReconstructionFactory>());

            // Act
            Func<Task> act = async () => await repository.GetTeachers(1, new[] { "teacherId 1", "teacherId 2" });

            // Assert
            await act.Should().ThrowExactlyAsync<UsersNotFoundException>();
        }

        [Test]
        public async Task Then_ReconstructionFactoryCalled()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageUserReconstructionFactory>();
            var teacherModel =
                new Modules.UsersService.Repository.Models.Teacher("teacher1@example.com",
                    "name 1",
                    "surname 1",
                    1,
                    "teacherId 1");
            reconstructionFactory.Create(_storageTeacher)
                .Returns(teacherModel);
            var repository = new UsersRepository(_storage, reconstructionFactory);

            // Act
            await repository.GetTeachers(1);

            // Assert
            reconstructionFactory.Received().Create(_storageTeacher);
        }
    }
}