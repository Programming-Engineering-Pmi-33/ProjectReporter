using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using ProjectReporter.Modules.GroupsService.Repository;
using ProjectReporter.Modules.GroupsService.Repository.Factories;
using ProjectReporter.Modules.GroupsService.Storage;
using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.UnitTests.GroupsService.Repository
{
    [TestFixture]
    class GroupsRepository_When_GettingGroupMembers
    {
        private GroupsStorage _storage;
        private GroupMember _storageGroupMember;

        [SetUp]
        public async Task Setup()
        {
            _storageGroupMember = new GroupMember
            {
                UserId = "user 1",
                InviterId = "user 2",
                GroupId = 1,
                IsActive = true,
                Id = 1,
            };

            var options = new DbContextOptionsBuilder<GroupsStorage>()
                .UseInMemoryDatabase("project_reporter")
                .Options;
            _storage = new GroupsStorage(options);

            _storage.GroupMembers.RemoveRange(_storage.GroupMembers.ToList());
            await _storage.SaveChangesAsync();

            await _storage.GroupMembers.AddAsync(_storageGroupMember);
            await _storage.SaveChangesAsync();
        }

        [TearDown]
        public void Finish()
        {
            _storage.Dispose();
        }


        //[Test]
        public async Task Then_FilteredGroupMemberModelsReturned()
        {
            // Arrange
            var wrongGroupMember = new GroupMember
            {
                UserId = "user 2",
                InviterId = "user 3",
                GroupId = 2,
                IsActive = false,
                Id = 2,
            };
            await _storage.GroupMembers.AddAsync(wrongGroupMember);
            await _storage.SaveChangesAsync();

            var reconstructionFactory = Substitute.For<IStorageGroupMemberReconstructionFactory>();
            var groupMemberModel =
                new Modules.GroupsService.Repository.Models.GroupMember(1,
                "user 1",
                "user 2",
                Guid.Empty.ToString(),
                true,
                1);
            reconstructionFactory.Create(_storageGroupMember)
                .Returns(groupMemberModel);
            var repository = new GroupsRepository(_storage, null, reconstructionFactory, null, null, null, null, null, null, null, null);

            // Act
            //var result = await repository.GetGroupMember(1);

            // Assert
            //result.Should().Be(groupMemberModel);
        }

        //[Test]
        public async Task Then_ReconstructionFactoryCalled()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageGroupMemberReconstructionFactory>();
            var groupMemberModel =
                new Modules.GroupsService.Repository.Models.GroupMember(1,
                "user 1",
                "user 2",
                Guid.Empty.ToString(),
                true,
                1);
            reconstructionFactory.Create(_storageGroupMember)
                .Returns(groupMemberModel);
            var repository = new GroupsRepository(_storage, null, reconstructionFactory, null, null, null, null, null, null, null, null);

            // Act
            //await repository.Get(1);

            // Assert
            reconstructionFactory.Received().Create(_storageGroupMember);
        }
    }
}
