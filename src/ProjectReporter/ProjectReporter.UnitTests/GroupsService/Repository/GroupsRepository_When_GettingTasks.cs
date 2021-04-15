using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using ProjectReporter.Modules.GroupsService.Repository;
using ProjectReporter.Modules.GroupsService.Repository.Factories;
using ProjectReporter.Modules.GroupsService.Storage;
using System;
using System.Linq;
using Task = ProjectReporter.Modules.GroupsService.Storage.Task;

namespace ProjectReporter.UnitTests.GroupsService.Repository
{
    [TestFixture]
    class GroupsRepository_When_GettingTasks
    {
        private GroupsStorage _storage;
        private Task _storageTask;

        [SetUp]
        public async System.Threading.Tasks.Task Setup()
        {
            _storageTask = new Task
            {
                Name = "name 1",
                Description = "description 1",
                GroupId = 1,
                Points = 1,
                Id = 1,
                StartDateTime = DateTime.MinValue,
                EndDateTime = DateTime.Today
            };

            var options = new DbContextOptionsBuilder<GroupsStorage>()
                .UseInMemoryDatabase("project_reporter")
                .Options;
            _storage = new GroupsStorage(options);

            _storage.Tasks.RemoveRange(_storage.Tasks.ToList());
            await _storage.SaveChangesAsync();

            await _storage.Tasks.AddAsync(_storageTask);
            await _storage.SaveChangesAsync();
        }

        [TearDown]
        public void Finish()
        {
            _storage.Dispose();
        }


        [Test]
        public async System.Threading.Tasks.Task Then_FilteredTaskModelsReturned()
        {
            // Arrange
            var wrongTask = new Task
            {
                Name = "name 2",
                Description = "description 2",
                GroupId = 2,
                Points = 2,
                Id = 2,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.MaxValue
            };
            await _storage.Tasks.AddAsync(wrongTask);
            await _storage.SaveChangesAsync();

            var reconstructionFactory = Substitute.For<IStorageTaskReconstructionFactory>();
            var taskModel =
                new Modules.GroupsService.Repository.Models.Task(1,
                "name 1",
                "description 1",
                1,
                DateTime.MinValue,
                DateTime.Today,
                null,
                1);
            reconstructionFactory.Create(_storageTask)
                .Returns(taskModel);
            var repository = new GroupsRepository(_storage, null, null, null, reconstructionFactory, null, null, null, null, null, null);

            // Act
            var result = await repository.GetTask(1);

            // Assert
            result.Should().Be(taskModel);
        }

        [Test]
        public async System.Threading.Tasks.Task Then_ReconstructionFactoryCalled()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageTaskReconstructionFactory>();
            var taskModel =
                new Modules.GroupsService.Repository.Models.Task(1,
                "name 1",
                "description 1",
                1,
                DateTime.MinValue,
                DateTime.Today,
                null,
                1);
            reconstructionFactory.Create(_storageTask)
                .Returns(taskModel);
            var repository = new GroupsRepository(_storage, null, null, null, reconstructionFactory, null, null, null, null, null, null);

            // Act
            await repository.GetTask(1);

            // Assert
            reconstructionFactory.Received().Create(_storageTask);
        }
    }
}
