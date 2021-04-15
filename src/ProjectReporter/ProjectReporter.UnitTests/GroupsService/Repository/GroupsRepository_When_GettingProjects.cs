using NUnit.Framework;
using System.Linq;
using NSubstitute;
using FluentAssertions;
using Task = System.Threading.Tasks.Task;
using ProjectReporter.Modules.GroupsService.Storage;
using Microsoft.EntityFrameworkCore;
using ProjectReporter.Modules.GroupsService.Repository.Factories;
using ProjectReporter.Modules.GroupsService.Repository;

namespace ProjectReporter.UnitTests.GroupsService.Repository
{
    [TestFixture]
    class GroupsRepository_When_GettingProjects
    {
        private GroupsStorage _storage;
        private Project _storageProject;

        [SetUp]
        public async Task Setup()
        {
            _storageProject = new Project
            {
                Name = "name 1",
                Description = "description 1",
                GroupId = 1,
                GitLink = "link1",
                Id = 1
            };

            var options = new DbContextOptionsBuilder<GroupsStorage>()
                .UseInMemoryDatabase("project_reporter")
                .Options;
            _storage = new GroupsStorage(options);

            _storage.Projects.RemoveRange(_storage.Projects.ToList());
            await _storage.SaveChangesAsync();

            await _storage.Projects.AddAsync(_storageProject);
            await _storage.SaveChangesAsync();
        }

        [TearDown]
        public void Finish()
        {
            _storage.Dispose();
        }


        [Test]
        public async Task Then_FilteredProjectModelsReturned()
        {
            // Arrange
            var wrongProject = new Project
            {
                Name = "name 2",
                Description = "description 2",
                GroupId = 2,
                GitLink = "link2",
                Id = 2
            };
            await _storage.Projects.AddAsync(wrongProject);
            await _storage.SaveChangesAsync();

            var reconstructionFactory = Substitute.For<IStorageProjectReconstructionFactory>();
            var projectModel =
                new Modules.GroupsService.Repository.Models.Project(1,
                "name 1",
                "description 1",
                "link 1",
                null,
                1);
            reconstructionFactory.Create(_storageProject)
                .Returns(projectModel);
            var repository = new GroupsRepository(_storage, null, null, reconstructionFactory, null, null, null, null, null, null, null);

            // Act
            var result = await repository.GetProject(1);

            // Assert
            result.Should().Be(projectModel);
        }

        [Test]
        public async Task Then_ReconstructionFactoryCalled()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageProjectReconstructionFactory>();
            var projectModel =
                new Modules.GroupsService.Repository.Models.Project(1,
                "name 1",
                "description 1",
                "link 1",
                null,
                1);
            reconstructionFactory.Create(_storageProject)
                .Returns(projectModel);
            var repository = new GroupsRepository(_storage, null, null, reconstructionFactory, null, null, null, null, null, null, null);

            // Act
            await repository.GetProject(1);

            // Assert
            reconstructionFactory.Received().Create(_storageProject);
        }
    }
}
