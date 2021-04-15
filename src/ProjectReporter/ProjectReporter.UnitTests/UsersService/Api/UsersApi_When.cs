using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using ProjectReporter.Modules.UsersService.Api;
using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Api.Factories;
using ProjectReporter.Modules.UsersService.Repository;
using ProjectReporter.Modules.UsersService.Storage;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.UnitTests.UsersService.Api
{
    [TestFixture]
    public class UsersApi_When
    {
        private UsersApi _usersApi;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IUsersRepository _repository;
        private IStorageUserModelMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _userManager = Substitute.For<UserManager<User>>(Substitute.For<IUserStore<User>>(),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            _signInManager = Substitute.For<SignInManager<User>>(_userManager,
                Substitute.For<IHttpContextAccessor>(),
                Substitute.For<IUserClaimsPrincipalFactory<User>>(),
                null,
                null,
                null,
                null);

            _mapper = Substitute.For<IStorageUserModelMapper>();
            _repository = Substitute.For<IUsersRepository>();
            _usersApi = new UsersApi(_userManager,
                _signInManager,
                _mapper,
                _repository);
        }

        [Test]
        public async Task GettingUsers_Then_RepositoryCalled()
        {
            // Act
            await _usersApi.GetUsers();

            // Assert
            await _repository.Received().GetUsers();
        }

        [Test]
        public async Task GettingStudents_Then_RepositoryCalled()
        {
            // Act
            await _usersApi.GetStudents(1);

            // Assert
            await _repository.Received().GetStudents(1);
        }

        [Test]
        public async Task GettingTeachers_Then_RepositoryCalled()
        {
            // Act
            await _usersApi.GetTeachers(1);

            // Assert
            await _repository.Received().GetTeachers(1);
        }

        [Test]
        public async Task LoggingOut_Then_SignInManagerCalled()
        {
            // Act
            await _usersApi.Logout();

            // Assert
            await _signInManager.Received().SignOutAsync();
        }

        [Test]
        public async Task LoggingIn_Then_SignInManagerCalled()
        {
            // Arrange
            var contract = new UserLoginContract
            {
                Email = "email@example.com",
                Password = "password",
                RememberMe = true,
                ReturnUrl = "home"
            };

            // Act
            await _usersApi.Login(contract);

            // Assert
            await _signInManager.Received().PasswordSignInAsync("email@example.com", "password", true, false);
        }

        [Test]
        public async Task RegisteringStudent_Then_MapperCalled()
        {
            // Arrange
            var contract = new StudentRegisterContract
            {
                Email = "email@example.com",
                Password = "password",
                FirstName = "name",
                LastName = "surname",
                GitLink = "gitLink",
                GroupId = 1
            };
            var student = new Student
            {
                Email = "email@example.com",
                UserName = "email@example.com",
                FirstName = "name",
                LastName = "surname",
                GroupId = 1,
                GitLink = "gitLink"
            };
            _mapper.Map(contract).Returns(student);
            _userManager.CreateAsync(student, contract.Password).Returns(IdentityResult.Success);

            // Act
            await _usersApi.Register(contract);

            // Assert
            _mapper.Received().Map(contract);
        }

        [Test]
        public async Task RegisteringStudent_Then_UserManagerCalled()
        {
            // Arrange
            var contract = new StudentRegisterContract
            {
                Email = "email@example.com",
                Password = "password",
                FirstName = "name",
                LastName = "surname",
                GitLink = "gitLink",
                GroupId = 1
            };
            var student = new Student
            {
                Email = "email@example.com",
                UserName = "email@example.com",
                FirstName = "name",
                LastName = "surname",
                GroupId = 1,
                GitLink = "gitLink"
            };
            _mapper.Map(contract).Returns(student);
            _userManager.CreateAsync(student, contract.Password).Returns(IdentityResult.Success);

            // Act
            await _usersApi.Register(contract);

            // Assert
            await _userManager.Received().CreateAsync(student, contract.Password);
        }

        [Test]
        public async Task RegisteringStudent_Then_SignInManagerCalled()
        {
            // Arrange
            var contract = new StudentRegisterContract
            {
                Email = "email@example.com",
                Password = "password",
                FirstName = "name",
                LastName = "surname",
                GitLink = "gitLink",
                GroupId = 1
            };
            var student = new Student
            {
                Email = "email@example.com",
                UserName = "email@example.com",
                FirstName = "name",
                LastName = "surname",
                GroupId = 1,
                GitLink = "gitLink"
            };
            _mapper.Map(contract).Returns(student);
            _userManager.CreateAsync(student, contract.Password).Returns(IdentityResult.Success);

            // Act
            await _usersApi.Register(contract);

            // Assert
            await _signInManager.SignInAsync(student, false);
        }

        [Test]
        public async Task RegisteringTeacher_Then_MapperCalled()
        {
            // Arrange
            var contract = new TeacherRegisterContract
            {
                Email = "email@example.com",
                Password = "password",
                FirstName = "name",
                LastName = "surname",
                DepartmentId = 1
            };
            var teacher = new Teacher
            {
                Email = "email@example.com",
                UserName = "email@example.com",
                FirstName = "name",
                LastName = "surname",
                DepartmentId = 1
            };
            _mapper.Map(contract).Returns(teacher);
            _userManager.CreateAsync(teacher, contract.Password).Returns(IdentityResult.Success);

            // Act
            await _usersApi.Register(contract);

            // Assert
            _mapper.Received().Map(contract);
        }

        [Test]
        public async Task RegisteringTeacher_Then_UserManagerCalled()
        {
            // Arrange
            var contract = new TeacherRegisterContract
            {
                Email = "email@example.com",
                Password = "password",
                FirstName = "name",
                LastName = "surname",
                DepartmentId = 1
            };
            var teacher = new Teacher
            {
                Email = "email@example.com",
                UserName = "email@example.com",
                FirstName = "name",
                LastName = "surname",
                DepartmentId = 1
            };
            _mapper.Map(contract).Returns(teacher);
            _userManager.CreateAsync(teacher, contract.Password).Returns(IdentityResult.Success);

            // Act
            await _usersApi.Register(contract);

            // Assert
            await _userManager.Received().CreateAsync(teacher, contract.Password);
        }

        [Test]
        public async Task RegisteringTeacher_Then_SignInManagerCalled()
        {
            // Arrange
            var contract = new TeacherRegisterContract
            {
                Email = "email@example.com",
                Password = "password",
                FirstName = "name",
                LastName = "surname",
                DepartmentId = 1
            };
            var teacher = new Teacher
            {
                Email = "email@example.com",
                UserName = "email@example.com",
                FirstName = "name",
                LastName = "surname",
                DepartmentId = 1
            };
            _mapper.Map(contract).Returns(teacher);
            _userManager.CreateAsync(teacher, contract.Password).Returns(IdentityResult.Success);

            // Act
            await _usersApi.Register(contract);

            // Assert
            await _signInManager.SignInAsync(teacher, false);
        }
    }
}