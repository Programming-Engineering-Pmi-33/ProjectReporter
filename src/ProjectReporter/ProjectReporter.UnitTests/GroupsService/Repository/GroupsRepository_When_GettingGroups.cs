using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProjectReporter.Modules.GroupsService.Storage;
using ProjectReporter.Modules.GroupsService.Repository;
using ProjectReporter.Modules.GroupsService.Repository.Factories;
using System.Linq;
using NSubstitute;
using FluentAssertions;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.UnitTests.GroupsService.Repository
{
    [TestFixture]
    public class GroupsRepository_When_GettingGroups
    {
        private GroupsStorage _storage;
        private Group _storageGroup;

        [SetUp]
        public async Task Setup()
        {
            _storageGroup = new Group
            {
                Name = "name 1",
                Description = "description 1",
                OwnerId = "owner 1",
                CoOwnerId = "coOwner 1",
                Status = 1,
                GitLink = "link1",
                Id = 1
            };

            var options = new DbContextOptionsBuilder<GroupsStorage>()
                .UseInMemoryDatabase("project_reporter")
                .Options;
            _storage = new GroupsStorage(options);

            _storage.Groups.RemoveRange(_storage.Groups.ToList());
            await _storage.SaveChangesAsync();

            await _storage.Groups.AddAsync(_storageGroup);
            await _storage.SaveChangesAsync();
        }

        [TearDown]
        public void Finish()
        {
            _storage.Dispose();
        }


        [Test]
        public async Task Then_FilteredGroupModelsReturned()
        {
            // Arrange
            var wrongGroup = new Group
            {
                Name = "name 2",
                Description = "description 2",
                OwnerId = "owner 2",
                CoOwnerId = "coOwner 2",
                Status = 0,
                GitLink = "link2",
                Id = 2
            };
            await _storage.Groups.AddAsync(wrongGroup);
            await _storage.SaveChangesAsync();

            var reconstructionFactory = Substitute.For<IStorageGroupReconstructionFactory>();
            var groupModel =
                new Modules.GroupsService.Repository.Models.Group("name 1",
                "description 1",
                1,
                "owner 1",
                "coOwner 1",
                "link 1",
                null,
                null,
                null,
                1);
            reconstructionFactory.Create(_storageGroup)
                .Returns(groupModel);
            var repository = new GroupsRepository(_storage, reconstructionFactory, null, null, null, null, null, null, null, null, null);

            // Act
            var result = await repository.GetGroup(1);

            // Assert
            result.Should().Be(groupModel);
        }

        [Test]
        public async Task Then_ReconstructionFactoryCalled()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageGroupReconstructionFactory>();
            var groupModel =
                new Modules.GroupsService.Repository.Models.Group("name 1",
                "description 1",
                1,
                "owner 1",
                "coOwner 1",
                "link 1",
                null,
                null,
                null,
                1);
            reconstructionFactory.Create(_storageGroup)
                .Returns(groupModel);
            var repository = new GroupsRepository(_storage, reconstructionFactory, null, null, null, null, null, null, null, null, null);

            // Act
            await repository.GetGroup(1);

            // Assert
            reconstructionFactory.Received().Create(_storageGroup);
        }
    }
}
