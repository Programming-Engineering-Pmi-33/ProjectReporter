using FluentAssertions;
using NUnit.Framework;
using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Api.Factories;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.UnitTests.UsersService.Api.Factories
{
    [TestFixture]
    public class StorageUserModelMapper_When_MappingStudent
    {
        [Test]
        public void Then_StorageStudentReturned()
        {
            // Arrange
            var contract = new StudentRegisterContract()
            {
                Email = "student@example.com",
                FirstName = "name",
                LastName = "surname",
                GitLink = "gitLink",
                GroupId = 1
            };
            var modelMapper = new StorageUserModelMapper();

            // Act
            var result = modelMapper.Map(contract);

            // Assert
            result.LastName.Should().Be("surname");
            result.FirstName.Should().Be("name");
            result.Email.Should().Be("student@example.com");
            result.UserName.Should().Be("student@example.com");
            result.GitLink.Should().Be("gitLink");
        }
    }
}