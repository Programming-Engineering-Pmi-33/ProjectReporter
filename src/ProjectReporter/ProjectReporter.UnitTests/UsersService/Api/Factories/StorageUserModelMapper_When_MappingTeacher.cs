using FluentAssertions;
using NUnit.Framework;
using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Api.Factories;

namespace ProjectReporter.UnitTests.UsersService.Api.Factories
{
    [TestFixture]
    public class StorageUserModelMapper_When_MappingTeacher
    {
        [Test]
        public void Then_StorageTeacherReturned()
        {
            // Arrange
            var contract = new TeacherRegisterContract()
            {
                Email = "teacher@example.com",
                FirstName = "name",
                LastName = "surname",
                DepartmentId = 1
            };
            var modelMapper = new StorageUserModelMapper();

            // Act
            var result = modelMapper.Map(contract);

            // Assert
            result.LastName.Should().Be("surname");
            result.FirstName.Should().Be("name");
            result.Email.Should().Be("teacher@example.com");
            result.UserName.Should().Be("teacher@example.com");
            result.DepartmentId.Should().Be(1);
        }
    }
}