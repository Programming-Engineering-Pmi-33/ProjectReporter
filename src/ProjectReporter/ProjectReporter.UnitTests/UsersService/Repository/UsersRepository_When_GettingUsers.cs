using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using ProjectReporter.Modules.UsersService.Repository;
using ProjectReporter.Modules.UsersService.Repository.Factories;
using ProjectReporter.Modules.UsersService.Storage;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.UnitTests.UsersService.Repository
{
    [TestFixture]
    public class UsersRepository_When_GettingUsers
    {
        private UsersStorage _storage;
        private User _storageUser;

        [SetUp]
        public async Task Setup()
        {
            _storageUser = new User
            {
                FirstName = "name 1",
                LastName = "surname 1",
                Email = "user1@example.com",
                UserName = "user1@example.com",
                Id = "userId 1"
            };

            var options = new DbContextOptionsBuilder<UsersStorage>()
                .UseInMemoryDatabase("project_reporter")
                .Options;
            _storage = new UsersStorage(options);

            _storage.Users.RemoveRange(_storage.Users.ToList());
            await _storage.SaveChangesAsync();

            await _storage.Users.AddAsync(_storageUser);
            await _storage.SaveChangesAsync();
        }

        [TearDown]
        public void Finish()
        {
            _storage.Dispose();
        }

        [Test]
        public async Task Then_FilteredUserModelsReturned()
        {
            // Arrange
            var wrongUser = new User
            {
                FirstName = "name 2",
                LastName = "surname 2",
                Email = "user2@example.com",
                UserName = "user2@example.com",
                Id = "userId 2"
            };
            await _storage.Users.AddAsync(wrongUser);
            await _storage.SaveChangesAsync();
            var reconstructionFactory = Substitute.For<IStorageUserReconstructionFactory>();
            var userModel =
                new Modules.UsersService.Repository.Models.User("user1@example.com", "name 1", "surname 1", "userId 1");
            reconstructionFactory.Create(_storageUser)
                .Returns(userModel);
            var repository = new UsersRepository(_storage, reconstructionFactory);

            // Act
            var result = await repository.GetUsers("userId 1");

            // Assert
            result.First().Should().Be(userModel);
        }

        [Test]
        public async Task Then_ReconstructionFactoryCalled()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageUserReconstructionFactory>();
            var userModel =
                new Modules.UsersService.Repository.Models.User("user@example.com",
                    "name 1",
                    "surname 1",
                    "userId 1");
            reconstructionFactory.Create(_storageUser)
                .Returns(userModel);
            var repository = new UsersRepository(_storage, reconstructionFactory);

            // Act
            await repository.GetUsers("userId 1");

            // Assert
            reconstructionFactory.Received().Create(_storageUser);
        }
    }
}